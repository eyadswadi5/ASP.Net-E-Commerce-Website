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
                
            </div>

            <div class="data-table">
                <div class="table-header">
                    <div class="table-cell">Branch Name</div>
                    <div class="table-cell">Location</div>
                    <div class="table-cell">Contact</div>
                    <div class="table-cell">Employees</div>
                    <div class="table-cell">Manager</div>
                    <div class="table-cell">Actions</div>
                </div>

                <div class="table-row">
                    <div class="table-cell">
                        Abo Remane
                    </div>
                    <div class="table-cell">
                        <div class="d-flex flex-row gap-2 align-items-center" style="display: flex; flex-flow: row;gap: 2px">
                            <div class="d-flex align-items-center justify-content-center opacity-50">
                                <img src="/assets/icons/location.svg" alt="location" />
                            </div>
                            <div>
                                Syria, Damascus
                            </div>
                        </div>
                    </div>
                    <div class="table-cell">+963 958 555 666</div>
                    <div class="table-cell">10</div>
                    <div class="table-cell">Ayham Jbili</div>
                    <div class="table-cell action-icons">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="action-icon"><i class="bi bi-eye"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="action-icon"><i class="bi bi-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="action-icon"><i class="bi bi-trash"></i></asp:LinkButton>
                    </div>
                </div>

                <div class="table-row">
                    <div class="table-cell">
                        Jaramanah
                    </div>
                    <div class="table-cell">
                        <div class="d-flex flex-row gap-2 align-items-center" style="display: flex; flex-flow: row;gap: 2px">
                            <div class="d-flex align-items-center justify-content-center opacity-50">
                                <img src="/assets/icons/location.svg" alt="location" />
                            </div>
                            <div>
                                Syria, Damascus
                            </div>
                        </div>
                    </div>
                    <div class="table-cell">+963 958 555 888</div>
                    <div class="table-cell">20</div>
                    <div class="table-cell">Ayham Jbili</div>
                    <div class="table-cell action-icons">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="action-icon"><i class="bi bi-eye"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="action-icon"><i class="bi bi-pencil"></i></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="action-icon"><i class="bi bi-trash"></i></asp:LinkButton>
                    </div>
                </div>

                <div class="table-row">
                    <div class="table-cell">
                        El Malki
                    </div>
                    <div class="table-cell">
                        <div class="d-flex flex-row gap-2 align-items-center" style="display: flex; flex-flow: row;gap: 2px">
                            <div class="d-flex align-items-center justify-content-center opacity-50">
                                <img src="/assets/icons/location.svg" alt="location" />
                            </div>
                            <div>
                                Syria, Damascus
                            </div>
                        </div>
                    </div>
                    <div class="table-cell">+963 999 999 999</div>
                    <div class="table-cell">25</div>
                    <div class="table-cell">Ayham Jbili</div>
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
