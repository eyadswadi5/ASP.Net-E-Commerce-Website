<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard/Dashboard.Master" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="E_Commerce_Website.dashboard.sections.employees.EditEmployee" %>
<%@ MasterType VirtualPath="~/dashboard/Dashboard.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!-- Page Header -->
        <div class="page-header page-header-compact">
            <div>
                <h1>Edit Employee</h1>
                <p class="subtitle">Update the required details to employee</p>
                <asp:Label ID="debuglbl" Text="" runat="server" />
            </div>
            <div class="header-actions">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/dashboard/sections/employees/Employees.aspx" runat="server" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            </div>
        </div>

        <!-- Main Form Card -->
        <div class="card form-card">
            
            <h2 class="form-section-header">Employee Details</h2>
            <!-- file input -->
            <div class="form-group profile-upload-group">
                <label>Profile Picture</label>
                <div class="d-flex align-items-center">
                    <div class="avatar-placeholder">
                        <i class="bi bi-person-fill"></i>
                    </div>
                    <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="custom-file-input" />
                    <label for="<%= fileUploadImage.ClientID %>" class="btn btn-upload-image">
                        <i class="bi bi-cloud-upload"></i> Upload Image
                    </label>
                </div>
                <asp:Label ID="lblFileUploadMessage" Text="" runat="server" CssClass="text-danger" />
            </div>
            <div class="row g-4">
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="<%= txtFirstName.ClientID %>">First Name</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Required="true" placeholder="e.g., John"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtLastName.ClientID %>">Last Name</label>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Required="true" placeholder="e.g., Doe"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtMother.ClientID %>">Mother Name</label>
                        <asp:TextBox ID="txtMother" runat="server" CssClass="form-control" Required="true" placeholder="e.g., Mama"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtMobile.ClientID %>">Mobile Number</label>
                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" TextMode="Phone" placeholder="e.g., +971 50 XXX XXXX"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label for="<%= txtEmail.ClientID %>">Personal Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" Required="true" placeholder="e.g., john.doe@mail.com"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtAddress.ClientID %>">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="1" placeholder="Street, City, Country"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtFather.ClientID %>">Father Name</label>
                        <asp:TextBox ID="txtFather" runat="server" CssClass="form-control" Required="true" placeholder="e.g., Baba"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= ddlGender.ClientID %>">Gender</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select form-control">
                            <asp:ListItem Text="Select Gender" Value="" Selected="True" />
                            <asp:ListItem Text="Male" Value="male" />
                            <asp:ListItem Text="Female" Value="female" />
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
            
            <h2 class="form-section-header with-margin-top">System Information</h2>
            <div class="row g-4">
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="<%= txtEmployeeIDNumber.ClientID %>">ID Number</label>
                        <asp:TextBox ID="txtEmployeeIDNumber" runat="server" CssClass="form-control" Required="true" placeholder="e.g. 2200778855"></asp:TextBox>
                    </div>
                    
                    <div class="form-group">
                        <label for="<%= ddlDepartment.ClientID %>">Department</label>
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-select form-control" Required="true" DataSourceID="Departments_Sql_DS" DataTextField="name" DataValueField="id">
                            <asp:ListItem Text="Select Department" Value="" Selected="True" />
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="Departments_Sql_DS" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT * FROM [departments]"></asp:SqlDataSource>
                    </div>

                    <div class="form-group">
                        <label for="<%= ddlJobTitle.ClientID %>">Job Title</label>
                        <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="form-select form-control" Required="true" DataSourceID="Roles_Sql_DS" DataTextField="type" DataValueField="id">
                            <asp:ListItem Text="Select Job Title" Value="" Selected="True" />
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="Roles_Sql_DS" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT * FROM [roles]"></asp:SqlDataSource>
                    </div>
                </div>

                <div class="col-md-6">

                    <div class="form-group">
                        <label for="<%= txtSalary.ClientID %>">Basic Salary</label>
                        <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" TextMode="Number" placeholder="e.g., 5000.00"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtBithDate.ClientID %>">BithDate</label>
                        <asp:TextBox ID="txtBithDate" runat="server" CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= ddlStatus.ClientID %>">Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select form-control" DataSourceID="employee_status_Sql_DS" DataTextField="status" DataValueField="id">
                            <asp:ListItem Text="Select Employee Status" Value="" Selected="True" />
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="employee_status_Sql_DS" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT * FROM [employee_status]"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
