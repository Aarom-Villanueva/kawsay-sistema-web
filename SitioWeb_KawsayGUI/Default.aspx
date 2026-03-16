<%@ Page Title="Kawsay Web" Language="C#" MasterPageFile="~/MasterModelo.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SitioWeb_KawsayGUI.Default" %>

<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content">
        <%--HEADER--%>
        <div class="top-bar">
            <div class="welcome-text">
                <h2 class="mb-2">Bienvenido - Sistema de Purificación de Agua</h2>
                <asp:Label ID="lblFechaActual" runat="server"></asp:Label>
            </div>
        </div>

        <%--CARDS--%>
        <div class="dashboard-section">
            <h4 class="mt-3 mb-4">Reportes y Graficos Estadisticos Principales</h4>

            <div class="row mb-5 g-4">

                <div class="col-xl-4 col-md-6">
                    <div class="card border-0 shadow-sm h-100 p-3 dashboard-card">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="icon-circle bg-primary-soft me-3">
                                    <i class="bi bi-person-badge-fill text-primary"></i>
                                </div>
                                <div>
                                    <h2 class="text-muted small fw-bold text-uppercase mb-1">Administradores activos</h2>
                                    <h3 class="mb-0">
                                        <asp:Literal ID="litAdmins" runat="server" />
                                    </h3>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4 col-md-6">
                    <div class="card border-0 shadow-sm h-100 p-3 dashboard-card">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="icon-circle bg-primary-soft me-3">
                                    <i class="bi bi-people-fill text-success"></i>
                                </div>
                                <div>
                                    <div class="text-muted small fw-bold text-uppercase mb-1">Clientes Activos</div>
                                    <h3 class="mb-0">
                                        <asp:Literal ID="litClientes" runat="server" />
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4 col-md-6">
                    <div class="card border-0 shadow-sm h-100 p-3 dashboard-card">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="icon-circle bg-primary-soft me-3">
                                    <i class="bi bi-tools text-info"></i>
                                </div>
                                <div>
                                    <div class="text-muted small fw-bold text-uppercase mb-1">Técnicos activos</div>
                                    <h3 class="mb-0">
                                        <asp:Literal ID="litTecnicos" runat="server" />
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4 col-md-6">
                    <div class="card border-0 shadow-sm h-100 p-3 dashboard-card">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="icon-circle bg-primary-soft me-3">
                                    <i class="bi bi-droplet-half text-danger"></i>
                                </div>
                                <div>
                                    <div class="text-muted small fw-bold text-uppercase mb-1">Total instalaciones</div>
                                    <h3 class="mb-0">
                                        <asp:Literal ID="litInst" runat="server" />
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4 col-md-6">
                    <div class="card border-0 shadow-sm h-100 p-3 dashboard-card">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="icon-circle bg-primary-soft me-3">
                                    <i class="bi bi-gear-fill text-info"></i>
                                </div>
                                <div>
                                    <div class="text-muted small fw-bold text-uppercase mb-1">Total servicios</div>
                                    <h3 class="mb-0">
                                        <asp:Literal ID="litServ" runat="server" />
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4 col-md-6">
                    <div class="card border-0 shadow-sm h-100 p-3 dashboard-card">
                        <div class="card-body">
                            <div class="d-flex align-items-center">
                                <div class="icon-circle bg-primary-soft me-3">
                                    <i class="bi bi-wrench-adjustable text-danger"></i>
                                </div>
                                <div>
                                    <div class="text-muted small fw-bold text-uppercase mb-1">Total mantenimientos</div>
                                    <h3 class="mb-0">
                                        <asp:Literal ID="litMant" runat="server" />
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <%--GRAFICOS--%>
        <div class="row g-3">
            <div class="col-md-4">
                <div class="card full-width-card">
                    <div class="card-header">
                        <div>
                            <div class="card-title">Instalaciones</div>
                            <div class="card-subtitle">Últimos Años</div>
                        </div>
                    </div>

                    <div class="card-body chart-container">
                        <asp:Chart ID="chInstalaciones" runat="server" Height="300px">
                            <Series>
                                <asp:Series Name="S1" ChartType="Column" IsValueShownAsLabel="true"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="CA1"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                </div>
            </div>

            <div class="col-md-4">
                <div class="card full-width-card">
                    <div class="card-header">
                        <div>
                            <div class="card-title">Mantenimientos</div>
                            <div class="card-subtitle">Últimos Años</div>
                        </div>
                    </div>

                    <div class="card-body chart-container">
                        <asp:Chart ID="chMantenimientos" runat="server" Height="300px">
                            <Series>
                                <asp:Series Name="S1" ChartType="Column" IsValueShownAsLabel="true"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="CA1"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                </div>
            </div>

            <div class="col-md-4">
                <div class="card full-width-card">
                    <div class="card-header">
                        <div>
                            <div class="card-title">Distribución de instalaciones por purificador</div>
                            <div class="card-subtitle">Últimos 5 años</div>
                        </div>
                    </div>

                    <div class="card-body chart-container">
                        <asp:Chart ID="chPurificadores" runat="server" Height="300px">

                            <Series>
                                <asp:Series
                                    Name="S1"
                                    ChartType="Pie"
                                    IsValueShownAsLabel="true"
                                    Label="#VAL"
                                    LegendText="#VALX">
                                </asp:Series>
                            </Series>

                            <ChartAreas>
                                <asp:ChartArea Name="CA1"></asp:ChartArea>
                            </ChartAreas>

                            <Legends>
                                <asp:Legend
                                    Name="Legend1"
                                    Docking="Right"
                                    Alignment="Center" />
                            </Legends>

                            <%--<Titles>
                                <asp:Title Name="Modelos">
                                </asp:Title>
                            </Titles>--%>

                        </asp:Chart>
                    </div>

                </div>
            </div>

        </div>

    </div>

</asp:Content>
