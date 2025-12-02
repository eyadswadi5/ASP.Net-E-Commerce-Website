<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard/Dashboard.Master" AutoEventWireup="true" CodeBehind="Warehouses.aspx.cs" Inherits="E_Commerce_Website.dashboard.sections.warehouses.Warehouses" %>
<%@ MasterType VirtualPath="~/dashboard/Dashboard.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header">
            <div>
                <h1>Warehouses</h1>
                <p class="subtitle">Manage your organization's warehouses</p>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/dashboard/sections/warehouses/AddWarehouses.aspx" runat="server" CssClass="btn btn-primary">+ Add Warehouses</asp:HyperLink>
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

            <asp:GridView ID="WarehousesGridView" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="No Warehouses found."
                OnRowCommand="WarehousesGridView_RowCommand"
                DataKeyNames="id" DataSourceID="Warehouses_DS_SQL"
                CssClass="data-table"
                GridLines="None"
                HeaderStyle-CssClass="table-header"
                RowStyle-CssClass="table-row"
                CellPadding="0">
                <Columns>
                    <asp:BoundField DataField="name" ItemStyle-CssClass="table-cell" HeaderText="name" SortExpression="name"></asp:BoundField>
                    <asp:BoundField DataField="city" ItemStyle-CssClass="table-cell" HeaderText="city" SortExpression="city"></asp:BoundField>
                    <asp:BoundField DataField="address" ItemStyle-CssClass="table-cell" HeaderText="address" SortExpression="address"></asp:BoundField>
                    <asp:BoundField DataField="phone" ItemStyle-CssClass="table-cell" HeaderText="phone" SortExpression="phone"></asp:BoundField>
                    <asp:BoundField DataField="ManagerName" ItemStyle-CssClass="table-cell" HeaderText="ManagerName" ReadOnly="True" SortExpression="ManagerName"></asp:BoundField>

                    <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="d-flex gap-3">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewLinkButton" runat="server" CssClass="action-icon"><i class="bi bi-eye"></i></asp:LinkButton>
                            <asp:HyperLink 
                                ID="EditHypLnk" 
                                runat="server" 
                                CssClass="action-icon"
                                NavigateUrl='<%# "~/dashboard/sections/warehouses/EditWarehouse.aspx?id=" + Eval("id") %>' >
                                <i class="bi bi-pencil"></i>
                            </asp:HyperLink>

                            <asp:LinkButton
                                ID="DeleteLinkButton"
                                runat="server"
                                CssClass="action-icon"
                                CommandName="DeleteWarehouse"
                                CommandArgument='<%# Eval("id") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this warehouse?');">
                                <i class="bi bi-trash"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>


            <asp:SqlDataSource runat="server" ID="Warehouses_DS_SQL" ConnectionString='<%$ ConnectionStrings:DBConnectionString %>' SelectCommand="SELECT TOP (1000) wh.[id]
              ,wh.[name]
              ,wh.[city]
              ,wh.[address]
              ,wh.[phone]
              ,peri.first_name + ' ' + peri.last_name as ManagerName
          FROM [STORE_DB].[dbo].[warehouses] as wh JOIN personal_information as peri
          ON wh.manager_id = peri.user_id;"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
