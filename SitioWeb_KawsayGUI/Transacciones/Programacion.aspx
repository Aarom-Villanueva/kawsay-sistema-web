<%@ Page Title="Kawsay Web" Language="C#" MasterPageFile="~/MasterModelo.Master" AutoEventWireup="true"
    CodeBehind="Programacion.aspx.cs" Inherits="SitioWeb_KawsayGUI.Transacciones.Programacion" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="main-content">

        <div class="page-header">
            <h2>Programación del Técnico</h2>
        </div>

        <div class="modern-card">
            <div class="modern-card-header">
                <span>Información General</span>
            </div>
            <div class="modern-card-body">
                <div class="row g-2">

                    <div class="col-md-6">
                        <label class="form-label">Técnico Asignado</label>
                        <asp:DropDownList ID="ddlTec" runat="server" CssClass="form-select" />
                    </div>

                    <div class="d-flex gap-3">

                        <div class="col-md-3">
                            <label class="form-label">Fecha Inicio</label>
                            <asp:TextBox ID="txtIni" runat="server" TextMode="Date" CssClass="form-control" />
                        </div>

                        <div class="col-md-3">
                            <label class="form-label">Fecha Fin</label>
                            <asp:TextBox ID="txtFin" runat="server" TextMode="Date" CssClass="form-control" />
                        </div>

                    </div>



                    <div class="col-md-8">
                        <label class="form-label">Observaciones Generales</label>
                        <asp:TextBox ID="txtObs" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>

        <div class="modern-card">
            <div class="modern-card-header">
                <span>Detalle de Visitas</span>
                <asp:Button ID="btnAdd" runat="server" Text="+ Agregar Visita" CssClass="btn-add" OnClick="btnAdd_Click" />
            </div>
            <div class="modern-card-body">

                <asp:Panel ID="pnlAdd" runat="server" Visible="false" CssClass="detail-panel">
                    <h6 class="mb-3" style="color: #1e40af; font-weight: 600;">Nueva Visita</h6>
                    <div class="row g-3">
                        <div class="col-md-5">
                            <label class="form-label">Cliente</label>
                            <asp:DropDownList ID="ddlCli" runat="server" CssClass="form-select" />
                        </div>

                        <div class="col-md-3">
                            <label class="form-label">Fecha y Hora de Visita</label>
                            <asp:TextBox ID="txtVisita" runat="server" TextMode="DateTimeLocal" CssClass="form-control" />
                        </div>

                        <div class="col-md-2">
                            <label class="form-label">Tipo de Servicio</label>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Instalación" Value="I" />
                                <asp:ListItem Text="Mantenimiento" Value="M" />
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2 d-flex align-items-end">
                            <asp:Button ID="btnGuardarDet" runat="server" Text="✓ Guardar" CssClass="btn btn-success btn-modern w-100" OnClick="btnGuardarDet_Click" />
                        </div>

                        <div class="col-md-12">
                            <label class="form-label">Observaciones del Detalle</label>
                            <asp:TextBox ID="txtObsDet" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="section-divider"></div>

                <asp:GridView ID="gvDet" runat="server" CssClass="table-modern"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdTemp"
                    OnRowCommand="gvDet_RowCommand"
                    GridLines="None">

                    <Columns>
                        <asp:BoundField HeaderText="Cliente" DataField="CodCli" />
                        <asp:BoundField HeaderText="Fecha y Hora" DataField="FecVisita" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField HeaderText="Tipo" DataField="TipServ" />
                        <asp:BoundField HeaderText="Observaciones" DataField="ObsDet" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" runat="server"
                                    CssClass="btn btn-outline-danger btn-sm btn-modern"
                                    CommandName="DEL"
                                    CommandArgument='<%# Eval("IdTemp") %>'
                                    OnClientClick="return confirm('¿Está seguro de eliminar este detalle?');">
                                    Eliminar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <asp:Button ID="btnGrabar" runat="server" Text="Grabar Programación" CssClass="btn btn-dark mt-4" OnClick="btnGrabar_Click" />
                <asp:Literal ID="litMsg" runat="server" />
            </div>
        </div>

    </div>
</asp:Content>
