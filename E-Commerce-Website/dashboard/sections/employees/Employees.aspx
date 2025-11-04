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
                
                <asp:DropDownList ID="ddlDepartments" runat="server" CssClass="form-select bootstrap-dropdown-override">
                    <asp:ListItem Text="All Departments" Value="All" Selected="True" />
                    <asp:ListItem Text="IT" Value="IT" />
                    <asp:ListItem Text="HR" Value="HR" />
                    <asp:ListItem Text="Sales" Value="Sales" />
                </asp:DropDownList>
            </div>

            <div class="data-table">
                <div class="table-header">
                    <div class="table-cell">Employee</div>
                    <div class="table-cell">Email</div>
                    <div class="table-cell">Department</div>
                    <div class="table-cell">Job Title</div>
                    <div class="table-cell">Status</div>
                    <div class="table-cell">Actions</div>
                </div>

                <div class="table-row">
                    <div class="table-cell employee-cell">
                        <div class="employee-avatar bg-blue">JD</div>
                        <div class="employee-info">
                            <span class="employee-name">John Doe</span>
                            <span class="employee-subtext">+1234567890</span>
                        </div>
                    </div>
                    <div class="table-cell">john.doe@company.com</div>
                    <div class="table-cell">IT</div>
                    <div class="table-cell">Software Engineer</div>
                    <div class="table-cell">
                        <span class="status-badge status-active">active</span>
                    </div>
                    <div class="table-cell action-icons">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="action-icon"><i class="bi bi-eye"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="action-icon"><i class="bi bi-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="action-icon"><i class="bi bi-trash"></i></asp:LinkButton>
                    </div>
                </div>

                <div class="table-row">
                    <div class="table-cell employee-cell">
                        <div class="employee-avatar bg-purple">JS</div>
                        <div class="employee-info">
                            <span class="employee-name">Jane Smith</span>
                            <span class="employee-subtext">+1234567891</span>
                        </div>
                    </div>
                    <div class="table-cell">jane.smith@company.com</div>
                    <div class="table-cell">HR</div>
                    <div class="table-cell">HR Manager</div>
                    <div class="table-cell">
                        <span class="status-badge status-active">active</span>
                    </div>
                    <div class="table-cell action-icons">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="action-icon"><i class="bi bi-eye"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="action-icon"><i class="bi bi-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="action-icon"><i class="bi bi-trash"></i></asp:LinkButton>
                    </div>
                </div>

                 <div class="table-row">
                    <div class="table-cell employee-cell">
                        <div class="employee-avatar bg-orange">MJ</div>
                        <div class="employee-info">
                            <span class="employee-name">Mike Johnson</span>
                            <span class="employee-subtext">+1234567892</span>
                        </div>
                    </div>
                    <div class="table-cell">mikej@company.com</div>
                    <div class="table-cell">Sales</div>
                    <div class="table-cell">Sales Representative</div>
                    <div class="table-cell">
                        <span class="status-badge status-active">active</span>
                    </div>
                    <div class="table-cell action-icons">
                        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="action-icon"><i class="bi bi-eye"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton8" runat="server" CssClass="action-icon"><i class="bi bi-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton9" runat="server" CssClass="action-icon"><i class="bi bi-trash"></i></asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
