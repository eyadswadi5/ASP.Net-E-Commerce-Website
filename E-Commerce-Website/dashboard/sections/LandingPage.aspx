<%@ Page Title="" Language="C#" MasterPageFile="~/dashboard/Dashboard.Master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="E_Commerce_Website.dashboard.sections.LandingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Dashboard</h1>
        <p class="subtitle">Welcome back! Here's what's happening with your business.</p>

        <div class="row g-4 mb-4">
            <div class="col-lg-3 col-md-6">
                <div class="card dashboard-stat-card">
                    <div class="stat-icon bg-blue">
                        <i class="bi bi-people-fill"></i> 
                    </div>
                    <div class="stat-info">
                        <span class="stat-title">Total Employees</span>
                        <div class="stat-value">3</div>
                    </div>
                    <div class="stat-change text-success-percent">
                        +12%
                    </div>
                </div>
            </div>
            
            <div class="col-lg-3 col-md-6">
                <div class="card dashboard-stat-card">
                    <div class="stat-icon bg-green">
                        <i class="bi bi-shop-window"></i>
                    </div>
                    <div class="stat-info">
                        <span class="stat-title">Active Branches</span>
                        <div class="stat-value">2</div>
                    </div>
                    <div class="stat-change text-success-percent">
                        +5%
                    </div>
                </div>
            </div>
            
            <div class="col-lg-3 col-md-6">
                <div class="card dashboard-stat-card">
                    <div class="stat-icon bg-purple">
                        <i class="bi bi-building"></i>
                    </div>
                    <div class="stat-info">
                        <span class="stat-title">Warehouses</span>
                        <div class="stat-value">2</div>
                    </div>
                    <div class="stat-change text-success-percent">
                        +2%
                    </div>
                </div>
            </div>
            
            <div class="col-lg-3 col-md-6">
                <div class="card dashboard-stat-card">
                    <div class="stat-icon bg-orange">
                        <i class="bi bi-box-seam"></i>
                    </div>
                    <div class="stat-info">
                        <span class="stat-title">Total Products</span>
                        <div class="stat-value">3</div>
                    </div>
                    <div class="stat-change text-success-percent">
                        +18%
                    </div>
                </div>
            </div>
        </div>

        <div class="row g-4">
            
            <div class="col-lg-6">
                <div class="card">
                    <h2 class="card-header-title">Recent Activity</h2>
                    <ul class="activity-list">
                        <li class="activity-item">
                            <div class="activity-dot bg-blue"></div>
                            <div class="activity-info">
                                <span class="activity-text">New employee added</span>
                                <span class="activity-subtext">By John Doe • 2 hours ago</span>
                            </div>
                        </li>
                        <li class="activity-item">
                            <div class="activity-dot bg-purple"></div>
                            <div class="activity-info">
                                <span class="activity-text">Product updated</span>
                                <span class="activity-subtext">By Jane Smith • 4 hours ago</span>
                            </div>
                        </li>
                        <li class="activity-item">
                            <div class="activity-dot bg-green"></div>
                            <div class="activity-info">
                                <span class="activity-text">Branch created</span>
                                <span class="activity-subtext">By Mike Johnson • 1 day ago</span>
                            </div>
                        </li>
                        <li class="activity-item">
                            <div class="activity-dot bg-orange"></div>
                            <div class="activity-info">
                                <span class="activity-text">Warehouse inventory updated</span>
                                <span class="activity-subtext">By Sarah Williams • 2 days ago</span>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="card">
                    <h2 class="card-header-title">Quick Stats</h2>
                    <ul class="quick-stats-list">
                        <li class="quick-stat-item stat-revenue">
                            <span class="stat-name">Total Revenue</span>
                            <span class="stat-value">$124,500</span>
                        </li>
                        <li class="quick-stat-item stat-orders">
                            <span class="stat-name">Active Orders</span>
                            <span class="stat-value">247</span>
                        </li>
                        <li class="quick-stat-item stat-satisfaction">
                            <span class="stat-name">Customer Satisfaction</span>
                            <span class="stat-value">94.5%</span>
                        </li>
                        <li class="quick-stat-item stat-inventory">
                            <span class="stat-name">Inventory Value</span>
                            <span class="stat-value">$89,320</span>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    </asp:Content>