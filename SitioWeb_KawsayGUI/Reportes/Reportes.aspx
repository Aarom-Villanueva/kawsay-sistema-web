<%@ Page Title="" Language="C#" MasterPageFile="~/MasterModelo.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="SitioWeb_KawsayGUI.Reportes.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content">
    <div class="page-header">
        <h1 class="page-title">Historial de Reportes de Viajes (Entre Fechas)</h1>
        <p class="page-subtitle">Consulta los reportes realizados</p>
    </div>

    <!-- Sección de Búsqueda -->
    <div class="search-section">
        <div class="row">
            <div class="col-md-4">
                <div class="form-row">
                    <label class="form-label">Código de Vehiculo</label>
                   <asp:TextBox ID="txtCodigoVehiculo" runat="server" CssClass="form-control" placeholder="Ingrese el código" MaxLength="6"></asp:TextBox>
                   <asp:Label ID="lblErrorCodigo" runat="server" CssClass="validation-error" ForeColor="Red" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-row">
                    <label class="form-label">Fecha de Inicio</label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="lblErrorFechaInicio" runat="server" CssClass="validation-error" ForeColor="Red" Visible="false"></asp:Label>

                </div>
            </div>
            <div class="col-md-4">
                <div class="form-row">
                    <label class="form-label">Fecha de Fin</label>
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="lblErrorFechaFin" runat="server" CssClass="validation-error" ForeColor="Red" Visible="false"></asp:Label>
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
                Información de Reporte
            </div>
            <div class="info-grid">
                <div class="info-item">
                    <span class="info-label">Placa</span>
                    <asp:Label ID="lblPlaca" runat="server" CssClass="info-value"></asp:Label>
                </div>
                <div class="info-item">
                    <span class="info-label">Tecnico</span>
                    <asp:Label ID="lblTecnico" runat="server" CssClass="info-value"></asp:Label>
                </div>
                <div class="info-item">
                    <span class="info-label">Marca</span>
                    <asp:Label ID="lblMarca" runat="server" CssClass="info-value"></asp:Label>
                </div>
                <div class="info-item">
                    <span class="info-label">DNI</span>
                    <asp:Label ID="lblDNI" runat="server" CssClass="info-value"></asp:Label>
                </div>
                <div class="info-item">
                    <span class="info-label">Modelo</span>
                    <asp:Label ID="lblModelo" runat="server" CssClass="info-value"></asp:Label>
                </div>
                <div class="info-item">
                    <span class="info-label">Licencia</span>
                    <asp:Label ID="lblLicencia" runat="server" CssClass="info-value"></asp:Label>
                </div>
                <div class="info-item">
                    <span class="info-label">Numero de Serie</span>
                    <asp:Label ID="lblNSerie" runat="server" CssClass="info-value"></asp:Label>
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
                 <asp:GridView ID="gvHistorial" runat="server"
    CssClass="data-table"
    AutoGenerateColumns="False"
    EmptyDataText="No se encontraron registros para el período seleccionado">

    <Columns>

        <%-- Código del reporte --%>
        <asp:BoundField DataField="Codigo" HeaderText="Código" />

        <%-- Fecha del viaje --%>
        <asp:BoundField DataField="Fecha"
                        HeaderText="Fecha"
                        DataFormatString="{0:dd/MM/yyyy}" />

        <%-- Incidencia del viaje --%>
        <asp:BoundField DataField="Incidencia"
                        HeaderText="Incidencia" />

        <%-- Estado del viaje --%>
        <asp:TemplateField HeaderText="Estado de Viaje">
            <ItemTemplate>
                <span class='<%# Eval("EstadoViaje").ToString() == "Acabado"
                                ? "badge-instalacion"
                                : "badge-mantenimiento" %>'>
                    <%# Eval("EstadoViaje") %>
                </span>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
        </div>
    </asp:Panel>
</div>

</asp:Content>
