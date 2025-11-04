<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="E_Commerce_Website.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    
    <title>Store Management - Login</title>

    <link href="~/styles/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
</head>
<body>

    <form id="form1" runat="server" class="container-fluid">
        <div class="login-card mx-auto text-center">
            <div class="icon-circle mb-3">
                <i class="bi bi-shop"></i>
            </div>
            <h4 class="fw-semibold">Store Management System</h4>
            <p class="text-muted mb-4">Sign in to your account</p>

            <div class="mb-3 text-start">
                <label for="txtUsername" class="form-label">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter your username"></asp:TextBox>
            </div>

            <div class="mb-3 text-start">
                <label for="txtPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password"></asp:TextBox>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-dark w-100 mb-3" OnClick="btnLogin_Click"/>

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mt-3"></asp:Label>
        </div>
    </form>

    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />

</body>
</html>
