<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SitioWeb_KawsayGUI.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login | Kawsay System</title>

    <link href="CSS/Login.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" />
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css" />
</head>
<body class="bg-dark">

    <form id="form2" runat="server" class="login-container">

        <div class="sidebar-left">
            <div class="d-flex align-items-center mb-4">
                <i class="fas fa-tint fa-2x"></i><span class="logo-text"><i class="bi bi-droplet-fill water-icon"></i>KAWSAY WEB</span>
            </div>

            <p class="mb-4">
                Sistema de Gestión para Purificadores de Agua
               
                <br />
                Controla, monitorea y optimiza tu negocio de agua purificada
            </p>

            <div class="sidebar-list">
                <div class="sidebar-item">
                    <div class="sidebar-icon"><i class="bi bi-award-fill"></i></div>
                    <span>Calidad Certificada</span>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-icon"><i class="bi bi-droplet-half"></i></div>
                    <span>Monitoreo de purificación en tiempo real</span>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-icon"><i class="bi bi-graph-up-arrow"></i></div>
                    <span>Control de inventario y ventas</span>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-icon"><i class="bi bi-people-fill"></i></div>
                    <span>Gestión de clientes y pedidos</span>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-icon"><i class="bi bi-shield-fill-check"></i></div>
                    <span>Reportes de calidad del agua</span>
                </div>
            </div>
        </div>

        <div class="login-form-area">
            <div class="login-header">
                <h2>Bienvenido</h2>
                <p class="mb-4 text-muted">Ingresa tus credenciales para acceder al sistema</p>
            </div>

            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-person-fill me-2"></i></span>
                    <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Usuario" />
                </div>
            </div>

            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-lock-fill me-2"></i></span>
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" placeholder="Contraseña" />
                </div>
            </div>

            <div class="form-check mb-4">
                <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" Checked="true" />
                <label class="form-check-label" for="chkRememberMe">Mantener sesión iniciada</label>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar al Sistema" CssClass="btn btn-kawsay btn-lg w-100" OnClick="btnLogin_Click" />

            <div class="text-center mt-3">
                <a href="#" class="text-decoration-none">
                    <i class="fas fa-key"></i>¿Olvidaste tu contraseña?
                </a>
            </div>

            <div class="mt-4">
                <asp:Literal ID="litMsg" runat="server" />
            </div>
        </div>

    </form>
</body>
</html>
