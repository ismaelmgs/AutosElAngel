using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using System.Data;
using NucleoBase.Core;
using Autos_SCC.Objetos;

namespace Autos_SCC.Views.Pays
{
    public partial class frmAbonos : System.Web.UI.Page, IViewAbonos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Abonos_Presenter(this, new DBAbonos());

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//frmLogin.aspx");
                }

                iIdAmortizacion = 0;
                oPresenter.LoadObjects_Presenter();
            }
        }

        protected void ddlOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNoCotizacion.Visible = false;
            lblSucursal.Visible = false;
            txtNoCotizacion.Visible = false;
            imbBuscaCliente.Visible = false;
            ddlSucursal.Visible = false;
            lblCotizacion.Visible = false;
            ddlCotizacion.Visible = false;
            divResultado.Visible = false;
            divPagosI.Visible = false;
            gvClientes.Visible = false;
            gvPagosInd.Visible = false;

            switch (ddlOpcion.SelectedValue)
            {
                case "1":
                    lblNoCotizacion.Visible = true;
                    txtNoCotizacion.Visible = true;
                    imbBuscaCliente.Visible = true;
                    break;
                case "2":
                    lblSucursal.Visible = true;
                    lblCotizacion.Visible = true;
                    ddlSucursal.Visible = true;
                    ddlCotizacion.Visible = true;
                    break;
            }
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetCotizaciones != null)
                eGetCotizaciones(sender, e);

            ddlCotizacion.DataSource = dtCotizacion;
            ddlCotizacion.DataTextField = "NombreCompleto";
            ddlCotizacion.DataValueField = "fi_Id";
            ddlCotizacion.DataBind();

            ddlCotizacion_SelectedIndexChanged(null, EventArgs.Empty);
        }

        protected void ddlCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetCliente != null)
                eGetCliente(sender, e);
            divResultado.Visible = true;
            divPagosI.Visible = true;
            gvClientes.Visible = true;
            gvPagosInd.Visible = true;
            ConsultaPagos();
        }

        protected void imbBuscaCliente_Click(object sender, EventArgs e)
        {
            ddlCotizacion_SelectedIndexChanged(sender, e);
        }

        protected void gvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView gv = (GridView)e.Row.FindControl("gvDetalle");
                    int iIdCotizacion = gvClientes.DataKeys[0]["fi_IdCotizacion"].S().I();

                    gv.DataSource = new DBAbonos().dtGetObtieneTransacciones(iIdCotizacion);
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al generar la cotización --> Error: " + ex.Message, "Cotizador");
            }
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.S())
                {
                    case "Abonar":
                        eTipoPagos = eTipoPago.PagoMensual;
                        ddlTipoMov.SelectedIndex = 0;
                        txtImporte.Text = string.Empty;
                        mpePagos.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al generar la cotización --> Error: " + ex.Message, "Cotizador");
            }
        }

        decimal dSuma = 0;
        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dSuma += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "fc_Monto"));
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "Total: ";
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;

                    e.Row.Cells[1].Text = dSuma.ToString("c");
                    e.Row.Font.Bold = true;
                    dSuma = 0;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al llenar la tabla de pagos " + ex.Message, "Consulta de créditos");
            }
        }

        protected void btnAceptarPago_Click(object sender, EventArgs e)
        {
            try
            {
                switch (eTipoPagos)
                {
                    case eTipoPago.PagoMensual:
                        if (eSetInsertaTran != null)
                            eSetInsertaTran(sender, e);
                        break;
                    case eTipoPago.PagoIndividual:
                        if (eSetInsertaPayInd != null)
                            eSetInsertaPayInd(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al abonar --> btnAceptarPago_Click Error: " + ex.Message, "Abonos");
            }
        }

        protected void btnCancelarPago_Click(object sender, EventArgs e)
        {
            mpePagos.Hide();
        }

        protected void gvPagosInd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                iIdAmortizacion = (Int64)gvPagosInd.DataKeys[e.CommandArgument.S().I()]["fi_IdAMortizacion"].S().L();

                switch (e.CommandName.S())
                {
                    case "Abonar":
                        eTipoPagos = eTipoPago.PagoIndividual;
                        ddlTipoMov.SelectedIndex = 0;
                        txtImporte.Text = string.Empty;
                        lblDebe.Text = "3000";
                        mpePagos.Show();
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al abonar --> Error: " + ex.Message, "Abonos");
            }
        }

        protected void gvPagosInd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Image img = (Image)e.Row.FindControl("imStatus");
                    if (img != null)
                    {
                        if (dtPagosInd.Rows[e.Row.RowIndex]["fm_MontoCompromiso"].S() == dtPagosInd.Rows[e.Row.RowIndex]["fm_MontoPagado"].S())
                        {
                            img.ImageUrl = "~/Images/Iconos/acept.ico";
                            img.ToolTip = "Pago realizado con exito";
                        }
                        else
                        {
                            img.ImageUrl = "~/Images/Iconos/Warning.ico";
                            img.ToolTip = "Pago pendiente";
                        }
                    }

                }

            }
            catch(Exception ex)
            {
                MostrarMensaje("Error al abonar en el pago Individual --> Error: " + ex.Message, "Abonos");
            }
        }


        #endregion

        #region METODOS

        public void LoadSucursales(DataTable dtSuc)
        {
            ddlSucursal.DataSource = dtSuc;
            ddlSucursal.DataValueField = "fi_Id";
            ddlSucursal.DataTextField = "fc_Descripcion";
            ddlSucursal.DataBind();
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        public void ConsultaPagos()
        {
            try
            {
                if (dtCliente.Rows.Count > 0)
                {
                    gvClientes.DataSource = dtCliente;
                    gvClientes.DataBind();
                }
                else
                {
                    gvClientes.DataSource = null;
                    gvClientes.DataBind();
                }

                if (dtPagosInd.Rows.Count > 0)
                {
                    gvPagosInd.DataSource = dtPagosInd;
                    gvPagosInd.DataBind();
                }
                else
                {
                    gvPagosInd.DataSource = null;
                    gvPagosInd.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        Abonos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetCotizaciones;
        public event EventHandler eGetCliente;
        public event EventHandler eSetInsertaTran;
        public event EventHandler eSetInsertaPayInd;

        public enum eTipoPago
        {
            PagoMensual,
            PagoIndividual
        }

        public int iIdSucursal
        {
            get
            {
                return ddlSucursal.SelectedValue.S().I();
            }
        }

        public int iIdCotizacion
        {
            get
            {
                if (ddlOpcion.SelectedValue == "2")
                    return ddlCotizacion.SelectedValue.S().I();
                else if (ddlOpcion.SelectedValue == "1")
                    return txtNoCotizacion.Text.S().I();
                else
                    return 0;
            }
        }

        public DataTable dtCotizacion
        {
            set;
            get;
        }

        public DataTable dtCliente
        {
            get { return (DataTable)ViewState["dtClienteV"]; }
            set { ViewState["dtClienteV"] = value; }
        }

        public DataTable dtPagosInd
        {
            get { return (DataTable)ViewState["VSPagosInd"]; }
            set { ViewState["VSPagosInd"] = value; }
        }

        public Transaccion oTran
        {
            get
            {
                return new Transaccion
                {
                    iIdCotizacion = iIdCotizacion,
                    iIdAmortizacion = iIdAmortizacion,
                    dMonto = ddlTipoMov.SelectedValue.S() == "PAY" ? txtImporte.Text.S().D() : txtImporte.Text.S().D() * -1,
                    sCodigo = ddlTipoMov.SelectedValue.S(),
                    sUsuario = Session["usuario"].S()
                };
            }
        }

        public eTipoPago eTipoPagos
        {
            get { return (eTipoPago)ViewState["VSTipoPago"]; }
            set { ViewState["VSTipoPago"] = value; }
        }

        public Int64 iIdAmortizacion
        {
            get { return (Int64)ViewState["VSIdAmortizacion"]; }
            set { ViewState["VSIdAmortizacion"] = value; }
        }

        public int iIdUsuario
        {
            get
            {
                int iIdUsu = 0;
                if (Session["oUserData"] != null)
                {
                    DataUserIndetity oUser = (DataUserIndetity)Session["oUserData"];
                    iIdUsu = oUser.IIdUsuario;
                }
                return iIdUsu;
            }
        }

        #endregion

    }
}