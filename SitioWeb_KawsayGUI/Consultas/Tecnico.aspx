<%@ Page Title="" Language="C#" MasterPageFile="~/MasterModelo.Master" AutoEventWireup="true" CodeBehind="Tecnico.aspx.cs" Inherits="SitioWeb_KawsayGUI.Consultas.Tecnico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content">
        <div class="page-header">
            <h1 class="page-title">Historial de Instalaciones y Mantenimientos por Técnico</h1>
            <p class="page-subtitle">Consulta el desempeño y servicios realizados por cada técnico</p>
        </div>

        <!-- Sección de Búsqueda -->
        <div class="search-section">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-row">
                        <label class="form-label">Código del Técnico</label>
                        <asp:TextBox ID="txtCodigoTecnico" runat="server" CssClass="form-control"
                                        placeholder="Ingrese el código" MaxLength="6"></asp:TextBox>
                        <asp:Label ID="lblErrorCodigo" runat="server" ForeColor="Red" CssClass="validation-error" Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="form-row">
                        <label class="form-label">Fecha de Inicio</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        <asp:Label ID="lblErrorFechaInicio" runat="server" ForeColor="Red" CssClass="validation-error" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-row">
                        <label class="form-label">Fecha de Fin</label>
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        <asp:Label ID="lblErrorFechaFin" runat="server" ForeColor="Red" CssClass="validation-error" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>

            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn-consultar" OnClick="btnConsultar_Click" />
        </div>

        <!-- Sección de Información del Técnico -->
        <asp:Panel ID="pnlInfo" runat="server" Visible="false">
            <div class="info-section">
                <div class="section-title">
                    <i class="bi bi-person-badge"></i>
                    Información del Técnico
                </div>
                <div class="info-grid">
                    <div class="info-item">
                        <span class="info-label">Nombres y Apellidos</span>
                        <asp:Label ID="lblNombres" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">DNI</span>
                        <asp:Label ID="lblDNI" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Número de Celular</span>
                        <asp:Label ID="lblCelular" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Licencia</span>
                        <asp:Label ID="lblLicencia" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Especialidad</span>
                        <asp:Label ID="lblEspecialidad" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Ubicacion</span>
                        <asp:Label ID="lblUbicacion" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Total de Instalaciones</span>
                        <asp:Label ID="lblInstalaciones" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Total de Mantenimientos</span>
                        <asp:Label ID="lblMantenimientos" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Direccion</span>
                        <asp:Label ID="lblDireccion" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                    <div class="info-item">
                        <span class="info-label">Estado</span>
                        <asp:Label ID="lblEstado" runat="server" CssClass="info-value"></asp:Label>
                    </div>
                </div>

            </div>

            <!-- Tabla de Resultados -->
            <div class="table-section">
                <div class="section-title">
                    <i class="bi bi-list-ul"></i>
                    Detalle de Servicios Realizados
                </div>
                <asp:GridView ID="gvHistorial" runat="server" CssClass="data-table" AutoGenerateColumns="False"
                    EmptyDataText="No se encontraron registros para el período seleccionado">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                                <span class='<%# Eval("Tipo").ToString() == "Instalación" ? "badge-instalacion" : "badge-mantenimiento" %>'>
                                    <%# Eval("Tipo") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" />
                        <asp:BoundField DataField="Detalle" HeaderText="Detalle" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </div>

</asp:Content>
