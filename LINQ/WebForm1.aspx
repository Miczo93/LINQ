 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LINQ.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
        <asp:Button ID="btnGetData" runat="server" OnClick="btnGetData_Click" Text="Get Data" />
        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" />
        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
        <br />
        <asp:Button ID="btnLazy" runat="server" OnClick="btnLazy_Click" Text="LazyLoading" />
        <asp:Button ID="btnEager" runat="server" OnClick="btnEager_Click" Text="EagerLoading" />
        <p>
            <asp:Button ID="btnGetEmpByDep" runat="server" OnClick="btnGetEmpByDep_Click" Text="Get Employee By Departmetn" />
        </p>
        <p>
            <asp:Label ID="lblDept" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
            <Columns>
                <asp:BoundField HeaderText="Department" DataField="Name" />
                <asp:TemplateField HeaderText="Employees">
                    <ItemTemplate>
                                <asp:GridView ID="GridView3" runat="server"  DataSource ='<%#Eval("EmployeesLINQ") %>' AutoGenerateColumns="false">
                                       <Columns>
                                              <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                              <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                                 </Columns>
                                </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
        <div style="font-family: Arial">
<asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"
    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
    <asp:ListItem Text="Load all Employees (common)" Value="Common"></asp:ListItem>
        <asp:ListItem Text="Load all Employees (all info)" Value="All"></asp:ListItem>
    <asp:ListItem Text="Load Permanent Employees" Value="Permanent"></asp:ListItem>
    <asp:ListItem Text="Load Contract Employees" Value="Contract"></asp:ListItem>
</asp:RadioButtonList>
<asp:GridView ID="GridView4" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
    <AlternatingRowStyle BackColor="#F7F7F7" />
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    <SortedAscendingCellStyle BackColor="#F4F4FD" />
    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
    <SortedDescendingCellStyle BackColor="#D8D8F0" />
    <SortedDescendingHeaderStyle BackColor="#3E3277" />
</asp:GridView>
</div>
        <asp:Button ID="btnAddEmp" runat="server" OnClick="btnAddEmp_Click" Text="AddEmployee" />
        <asp:Button ID="btnCompQuery" runat="server" OnClick="btnCompQuery_Click" Text="Compiled Query" />
        <asp:Button ID="btnDireQuery" runat="server" OnClick="btnDirect_Click" Text="DirectExecuteQuery" />
        <asp:Button ID="btnExecute" runat="server" OnClick="btnExecute_Click" Text="ExecuteComand" />
        <asp:Button ID="btnCache" runat="server" OnClick="btnCache_Click" Text="Cache" />
    </form>
</body>
</html>
