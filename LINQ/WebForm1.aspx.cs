using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LINQ
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SampleDataContext dbContext = new SampleDataContext();
            GridView1.DataSource = dbContext.EmployeesLINQ;//all
            GridView1.DataSource = from EmployeesLINQ in dbContext.EmployeesLINQ
                                   where EmployeesLINQ.Gender == "Male"
                                   orderby EmployeesLINQ.Salary descending
                                   select EmployeesLINQ;
            //linq query
            //Linq to sql zamienia na jezyk sql i potem zamienia wyniki na objecty
            GridView1.DataBind();
        }
    }
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
*/
}