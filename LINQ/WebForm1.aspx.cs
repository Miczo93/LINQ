using System;
using System.Linq;
using System.Data.Linq;

namespace LINQ
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private void GetData()
        {
            SampleDataContext dbContext = new SampleDataContext();


            // GridView1.DataSource = dbContext.EmployeesLINQ;//all
            /*
            var linqQuery = from EmployeesLINQ in dbContext.EmployeesLINQ
                            select EmployeesLINQ;          
            GridView1.DataSource = linqQuery;
             */

            Log(dbContext, 0);

            /*
            GridView1.DataSource = from EmployeesLINQ in dbContext.EmployeesLINQ
                                   where EmployeesLINQ.Gender == "Male"
                                   orderby EmployeesLINQ.Salary descending
                                   select EmployeesLINQ;
            */
            //linq query
            //Linq to sql zamienia na jezyk sql i potem zamienia wyniki na objecty
            GridView1.DataSource = dbContext.GetEmployeesLINQ();//procedura
            GridView1.DataBind();
        }

        private void GetDataLazyLoading()
        {
            //nie wczytuje poki nie dojdziemy do nich
            //select emp where id 1,2,3
            //n+1 problem select * dept, select * emp from dept 1,2,...,n
            //wiecej zapytan (gorszy performence), mniej miejsca tracimy
            using (SampleDataContext dbContext = new SampleDataContext())
            {
                dbContext.Log = Response.Output;
                GridView2.DataSource = dbContext.DepartmentsLINQ;
                GridView2.DataBind();
            }
        }
        private void GetDataEagerLoading(int choice)
        {
            //wczytuje dodatkowe wraz z wczytaniem wybranych
            //left join jest
            //tracimy miejsce w pamieci ale mniej zapytan (lepszy performence)
            using (SampleDataContext dbContext = new SampleDataContext())
            {
                dbContext.Log = Response.Output;
                switch (choice)
                {
                    case 1:
                        DataLoadOptions loadOptions = new DataLoadOptions();
                        loadOptions.LoadWith<DepartmentsLINQ>(d => d.EmployeesLINQ); //wraz z dept wczytaj emp
                        dbContext.LoadOptions = loadOptions;
                        GridView2.DataSource = dbContext.DepartmentsLINQ;
                        break;
                    case 2:
                        GridView2.DataSource = from DepartmentsLINQ in dbContext.DepartmentsLINQ
                                               select new {Name = DepartmentsLINQ.Name, EmployeesLINQ = DepartmentsLINQ.EmployeesLINQ };
                        break;
                }
                GridView2.DataBind();
            }
        }


        private void Log(SampleDataContext dbContext, int choice)
        {
            switch (choice)
            {
                case 1:
                    dbContext.Log = Response.Output; //wyrzuca komende na strone
                    //dbContext.Log = Console.Out; //na konsole
                    break;
                case 2:
                    var linqQuery = from EmployeesLINQ in dbContext.EmployeesLINQ
                                    select EmployeesLINQ;
                    Response.Write(dbContext.GetCommand(linqQuery).CommandText);
                    break;
                case 3:
                    var linqQuery2 = from EmployeesLINQ in dbContext.EmployeesLINQ
                                     select EmployeesLINQ;
                    string sqlQuetry = linqQuery2.ToString();
                    Response.Write(sqlQuetry);
                    break;
                    //jest jeszcze profiler w sql serverze
            }
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            GetData();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            using (SampleDataContext dbContext = new SampleDataContext())
            {
                EmployeesLINQ emp = new EmployeesLINQ
                {
                    FirstName = "Tim",
                    LastName = "T",
                    Salary = 55000,
                    DepartmentId = 1
                };
                dbContext.EmployeesLINQ.InsertOnSubmit(emp);
                dbContext.SubmitChanges();
            }
            GetData();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SampleDataContext dbContext = new SampleDataContext())
            {
                EmployeesLINQ emp = dbContext.EmployeesLINQ.SingleOrDefault(x => x.ID == 1003);
                emp.Salary = 65000;
                dbContext.SubmitChanges();
            }
            GetData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SampleDataContext dbContext = new SampleDataContext())
            {
                EmployeesLINQ emp = dbContext.EmployeesLINQ.SingleOrDefault(x => x.ID == 1003);
                dbContext.EmployeesLINQ.DeleteOnSubmit(emp);
                dbContext.SubmitChanges();
            }
            GetData();
        }

        protected void btnGetEmpByDep_Click(object sender, EventArgs e)
        {
            using (SampleDataContext dbContext = new SampleDataContext())
            {
                string deptName = string.Empty;
                GridView1.DataSource = dbContext.GetEmployeesByDepartmentLINQ(1, ref deptName);
                GridView1.DataBind();
                lblDept.Text = "Department Name = " + deptName;
            }
        }

        protected void btnLazy_Click(object sender, EventArgs e)
        {
            GetDataLazyLoading();
        }

        protected void btnEager_Click(object sender, EventArgs e)
        {
            GetDataEagerLoading(2);
        }
    }
    #region SQL
    /*
            Create table DepartmentsLINQ
        (
             ID int primary key identity,
             Name nvarchar(50),
             Location nvarchar(50)
        )
        GO

        Create table EmployeesLINQ
        (
             ID int primary key identity,
             FirstName nvarchar(50),
             LastName nvarchar(50),
             Gender nvarchar(50),
             Salary int,
             DepartmentId int foreign key references DepartmentsLINQ(Id)
        )
        GO

        Insert into DepartmentsLINQ values ('IT', 'New York')
        Insert into DepartmentsLINQ values ('HR', 'London')
        Insert into DepartmentsLINQ values ('Payroll', 'Sydney')
        GO

        Insert into EmployeesLINQ values ('Mark', 'Hastings', 'Male', 60000, 1)
        Insert into EmployeesLINQ values ('Steve', 'Pound', 'Male', 45000, 3)
        Insert into EmployeesLINQ values ('Ben', 'Hoskins', 'Male', 70000, 1)
        Insert into EmployeesLINQ values ('Philip', 'Hastings', 'Male', 45000, 2)
        Insert into EmployeesLINQ values ('Mary', 'Lambeth', 'Female', 30000, 2)
        Insert into EmployeesLINQ values ('Valarie', 'Vikings', 'Female', 35000, 3)
        Insert into EmployeesLINQ values ('John', 'Stanmore', 'Male', 80000, 1)
        GO

        --Select procedure
        Create procedure GetEmployeesLINQ
        as
        Begin
             Select ID, FirstName, LastName, Gender, Salary, DepartmentId
             from EmployeesLINQ
        End

        -- Insert  Procedure
        Create procedure InsertEmployeeLINQ
        @FirstName nvarchar(50),
        @LastName nvarchar(50),
        @Gender nvarchar(50),
        @Salary int,
        @DepartmentId int
        as
        Begin
             Insert into EmployeesLINQ(FirstName, LastName, Gender, Salary, DepartmentId)
             values (@FirstName, @LastName, @Gender, @Salary, @DepartmentId)
        End
        GO

    -- Update Procedure
        Create procedure UpdateEmployeeLINQ
        @ID int,
        @FirstName nvarchar(50),
        @LastName nvarchar(50),
        @Gender nvarchar(50),
        @Salary int,
        @DepartmentId int
        as
        Begin
            Update EmployeesLINQ Set
            FirstName = @FirstName, LastName = @LastName, Gender = @Gender,
            Salary = @Salary, DepartmentId = @DepartmentId
            where ID = @ID
        End
        GO

    -- Delete Procedure
        Create procedure DeleteEmployeeLINQ
        @ID int
        as
        Begin
             Delete from EmployeesLINQ where ID = @ID
        End
        GO

         -- Procedure With Output 
        Create procedure GetEmployeesByDepartmentLINQ
        @DepartmentId int,
        @DepartmentName nvarchar(50) out
        as
        Begin
             Select @DepartmentName = Name
             from DepartmentsLINQ where ID = @DepartmentId

             Select * from EmployeesLINQ
             where DepartmentId = @DepartmentId

        End
    */
    #endregion
}