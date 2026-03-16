using ProyKawsay_BE;
using ProyKawsay_BL;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SitioWeb_KawsayGUI.Transacciones
{
    public partial class Programacion : System.Web.UI.Page
    {
        private const string KEY_DET = "PROG_DET";
        private const string KEY_TEC_LOCK = "PROG_TEC_LOCK";

        private List<ProgramacionDetalleBE> Detalles
        {
            get
            {
                if (Session[KEY_DET] == null)
                    Session[KEY_DET] = new List<ProgramacionDetalleBE>();

                return (List<ProgramacionDetalleBE>)Session[KEY_DET];
            }
        }

        private string TecBloqueado
        {
            get => Session[KEY_TEC_LOCK]?.ToString();
            set => Session[KEY_TEC_LOCK] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();

                // Fechas por defecto
                txtIni.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtFin.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

            // SIEMPRE (en postback también): refrescar grid y aplicar bloqueo
            gvDet.DataSource = Detalles;
            gvDet.DataBind();

            AplicarBloqueoTecnico();
        }

        private void AplicarBloqueoTecnico()
        {
            // si hay detalles => no se puede cambiar técnico
            ddlTec.Enabled = (Detalles.Count == 0);

            // si ya no hay detalles => suelto el bloqueo
            if (Detalles.Count == 0)
                TecBloqueado = null;
        }

        private void CargarCombos()
        {
            var bl = new ProgramacionBL();

            ddlTec.DataSource = bl.ListarTecnicosActivos();
            ddlTec.DataTextField = "Texto";
            ddlTec.DataValueField = "Codigo";
            ddlTec.DataBind();
            ddlTec.Items.Insert(0, new ListItem("----- Seleccione -----", ""));

            ddlCli.DataSource = bl.ListarClientesActivos();
            ddlCli.DataTextField = "Texto";
            ddlCli.DataValueField = "Codigo";
            ddlCli.DataBind();
            ddlCli.Items.Insert(0, new ListItem("----- Seleccione -----", ""));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = !pnlAdd.Visible;
        }

        protected void btnGuardarDet_Click(object sender, EventArgs e)
        {
            // 1) OBLIGAR a elegir técnico antes de meter detalles
            if (string.IsNullOrEmpty(ddlTec.SelectedValue))
            {
                litMsg.Text = "<div class='alert alert-danger mt-3'>Selecciona un técnico antes de agregar detalles.</div>";
                return;
            }

            // 2) Bloquear técnico desde el primer detalle
            if (Detalles.Count == 0)
            {
                TecBloqueado = ddlTec.SelectedValue; // se fija el técnico
            }
            else
            {
                // seguridad 
                if (!string.Equals(ddlTec.SelectedValue, TecBloqueado, StringComparison.OrdinalIgnoreCase))
                {
                    ddlTec.SelectedValue = TecBloqueado; // lo regreso al técnico correcto
                    litMsg.Text = "<div class='alert alert-warning mt-3'>Ya agregaste detalles: no puedes cambiar de técnico.</div>";
                    return;
                }
            }

            if (string.IsNullOrEmpty(ddlCli.SelectedValue))
            {
                litMsg.Text = "<div class='alert alert-danger mt-3'>Selecciona un cliente.</div>";
                return;
            }

            if (!DateTime.TryParse(txtVisita.Text, out DateTime visita))
            {
                litMsg.Text = "<div class='alert alert-danger mt-3'>Ingresa una fecha/hora válida para la visita.</div>";
                return;
            }

            Detalles.Add(new ProgramacionDetalleBE
            {
                IdTemp = Guid.NewGuid(),
                CodCli = ddlCli.SelectedValue,
                FecVisita = visita,
                TipServ = Convert.ToChar(ddlTipo.SelectedValue),
                ObsDet = txtObsDet.Text.Trim()
            });

            gvDet.DataSource = Detalles;
            gvDet.DataBind();

            // al tener detalles, se bloquea visualmente
            AplicarBloqueoTecnico();

            txtObsDet.Text = "";
            pnlAdd.Visible = false;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlTec.SelectedValue))
                    throw new Exception("Selecciona un técnico.");

                if (Detalles.Count == 0)
                    throw new Exception("Agrega al menos 1 detalle antes de grabar.");

                // validación extra: si ya había técnico bloqueado, debe coincidir
                if (!string.IsNullOrEmpty(TecBloqueado) &&
                    !string.Equals(TecBloqueado, ddlTec.SelectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("No puedes grabar con un técnico diferente al que inició la programación.");
                }

                var cab = new ProgramacionBE
                {
                    CodTec = ddlTec.SelectedValue,
                    FecIni = DateTime.Parse(txtIni.Text),
                    FecFin = DateTime.Parse(txtFin.Text),
                    ObsProg = txtObs.Text.Trim(),
                    UsuRegistro = Session["Usuario"]?.ToString()
                };

                var bl = new ProgramacionBL();
                string cod = bl.Registrar(cab, Detalles);

                LimpiarFormulario();

                litMsg.Text = $"<div class='alert alert-success mt-3'>Se registró la programación {cod} exitosamente.</div>";
            }
            catch (Exception ex)
            {
                litMsg.Text = $"<div class='alert alert-danger mt-3'>{ex.Message}</div>";
            }
        }

        protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DEL")
            {
                var id = Guid.Parse(e.CommandArgument.ToString());
                var item = Detalles.Find(x => x.IdTemp == id);
                if (item != null) Detalles.Remove(item);

                gvDet.DataSource = Detalles;
                gvDet.DataBind();

                // si ya no quedan detalles, se desbloquea
                AplicarBloqueoTecnico();
            }
        }

        private void LimpiarFormulario()
        {
            Detalles.Clear();
            Session.Remove(KEY_DET);
            Session.Remove(KEY_TEC_LOCK);

            gvDet.DataSource = Detalles;
            gvDet.DataBind();

            ddlTec.SelectedValue = "";
            txtIni.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtFin.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtObs.Text = "";

            ddlCli.SelectedValue = "";
            txtVisita.Text = "";
            txtObsDet.Text = "";
            ddlTipo.SelectedIndex = 0;

            pnlAdd.Visible = false;

            AplicarBloqueoTecnico();
        }
    }
}
