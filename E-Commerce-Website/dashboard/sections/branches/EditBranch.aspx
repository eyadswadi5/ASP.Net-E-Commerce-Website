<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard/Dashboard.Master" AutoEventWireup="true" CodeBehind="EditBranch.aspx.cs" Inherits="E_Commerce_Website.dashboard.sections.branches.EditBranch" %>
<%@ MasterType VirtualPath="~/dashboard/Dashboard.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header page-header-compact">
            <div>
                <h1>Edit Branch</h1>
                <p class="subtitle">Update the required details to branch</p>
                <asp:Label ID="lblStatusMessage" Text="" runat="server" CssClass="text-secondary" />
            </div>
            <div class="header-actions">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/dashboard/sections/branches/Branches.aspx" runat="server" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            </div>
        </div>

        <div class="card form-card">
            
            <h2 class="form-section-header">Branch Details</h2>
            <div class="row g-4">
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="<%= txtName.ClientID %>">Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Required="true" placeholder="e.g., Some Place"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtCity.ClientID %>">City</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Required="true" placeholder="e.g., Homs"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtAddress.ClientID %>">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="Phone" placeholder="e.g., Somewhere in the world"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-group">
                        <label for="<%= ddlManager.ClientID %>">Manager</label>
                        <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-select" DataSourceID="Managers_Sql_DS" DataTextField="full_name" DataValueField="id"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="Managers_Sql_DS" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT personal_information.id, (first_name + ' ' + last_name) as full_name FROM personal_information Join roles ON personal_information.role_id = roles.id WHERE roles.type = 'manager'"></asp:SqlDataSource>
                        <asp:Label ID="lblMangerStatus" Text="" CssClass="text-danger" runat="server" />
                    </div>

                    <div class="form-group">
                        <label for="<%= txtPhone.ClientID %>">Phone Number</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Required="true" placeholder="e.g., +963 958 456 266"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <div class="mb-3">
                            <label for="<%= fileUploadBrochure.ClientID %>">Brochure File</label>
                            <asp:FileUpload ID="fileUploadBrochure" CssClass="form-control" runat="server" />
                            <asp:Label ID="lblFileUploadMessage" Text="" CssClass="text-danger" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
           
        </div>
    </div>
</asp:Content>
