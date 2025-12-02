<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard/Dashboard.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="E_Commerce_Website.dashboard.sections.employees.Employees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div>
                <h1>Employees</h1>
                <p class="subtitle">Manage your organization's employees</p>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/dashboard/sections/employees/AddEmployees.aspx" runat="server" CssClass="btn btn-primary">+ Add Employee</asp:HyperLink>
            </div>
        </div>

        <div class="card">
            <div class="table-controls-row">
                <div class="search-bar-container">
                    <i class="bi bi-search"></i>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search employees..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>


            <asp:GridView ID="EmployeesGridView" runat="server" DataSourceID="Employees_SQL_DS" AutoGenerateColumns="False" AllowPaging="True"
                EmptyDataText="No Employees found."
                OnRowDataBound="EmployeesGridView_RowDataBound"
                CssClass="data-table"
                GridLines="None"
                HeaderStyle-CssClass="table-header"
                RowStyle-CssClass="table-row"
                CellPadding="1" DataKeyNames="id">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" ItemStyle-CssClass="table-cell" InsertVisible="False" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="Employee" HeaderText="Employee" SortExpression="Employee" ItemStyle-CssClass="table-cell" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ItemStyle-CssClass="table-cell"></asp:BoundField>
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" ItemStyle-CssClass="table-cell"></asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-CssClass="table-cell"></asp:BoundField>
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource runat="server" ID="Employees_SQL_DS" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT TOP (1000) peri.[id]
      ,peri.[first_name] + ' ' + peri.[last_name] as Employee
      ,peri.[email] as Email
	  ,d.[name] as Department
	  ,emps.[status] as [Status]
  FROM [STORE_DB].[dbo].[personal_information] as peri
  JOIN departments as d ON d.id = peri.department_id 
  JOIN employee_status as emps ON emps.id = peri.employee_status_id"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
