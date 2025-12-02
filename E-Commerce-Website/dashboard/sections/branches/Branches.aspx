<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard/Dashboard.Master" AutoEventWireup="true" CodeBehind="Branches.aspx.cs" Inherits="E_Commerce_Website.dashboard.sections.branches.Branches" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div>
                <h1>Branches</h1>
                <p class="subtitle">Manage your organization's branches</p>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/dashboard/sections/branches/AddBranches.aspx" runat="server" CssClass="btn btn-primary">+ Add Branch</asp:HyperLink>
            </div>
        </div>

        <div class="card">
            <div class="table-controls-row">
                <div class="search-bar-container">
                    <i class="bi bi-search"></i>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search branches..."></asp:TextBox>
                </div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>

            <asp:GridView ID="BranchesGridView" runat="server" DataSourceID="Branches_SQL_DS" AutoGenerateColumns="False" AllowPaging="True"
                EmptyDataText="No branches found."
                OnRowDataBound="BranchesGridView_RowDataBound"
                CssClass="data-table"
                GridLines="None"
                HeaderStyle-CssClass="table-header"
                RowStyle-CssClass="table-row"
                CellPadding="1">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" ItemStyle-CssClass="table-cell"></asp:BoundField>
                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ItemStyle-CssClass="table-cell" ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="phone" HeaderText="phone" SortExpression="phone" ItemStyle-CssClass="table-cell"></asp:BoundField>
                    <asp:BoundField DataField="ManagerName" HeaderText="ManagerName" SortExpression="ManagerName" ItemStyle-CssClass="table-cell" ReadOnly="True"></asp:BoundField>
                </Columns>
            </asp:GridView>



            <asp:SqlDataSource runat="server" ID="Branches_SQL_DS" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT TOP (1000)
              stores.[name]
              ,stores.[address] + ', ' + stores.[city] as Location
              ,stores.[phone]
              ,personal_information.first_name + ' ' + personal_information.last_name as ManagerName
          FROM [STORE_DB].[dbo].[stores] JOIN personal_information ON personal_information.user_id = stores.manager_id;"></asp:SqlDataSource>
        </div>
    </div>

</asp:Content>
