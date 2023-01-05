using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using Autos_SCC.Views.ControlesUsuario;
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

            omb.OkButtonPressed += new ucModalConfirm.OkButtonPressedHandler(omb_OkButtonPressed);
            omb.CancelButtonPressed += new ucModalConfirm.CancelButtonPressedHandler(omb_CancelButtonPressed);
            omb2.OkButtonPressed += new ucModalAlert.OkButtonPressedHandler(omb_Ok2ButtonPressed);
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
        protected void ddlTipoMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (sTipoAbono)
            {
                case "PAY":
                    lblTituloBuscaAuto.Text = "Cargar Abono Normal";
                    lblTextDebe.Text = "Monto Compromiso:";
                    dMontoDescuento = 0.0m;
                    lblDebe.Text = string.Format("{0:C2}",dMontoCompromiso); break;
                case "RPAY":
                    lblTituloBuscaAuto.Text = "Cargar Abono Normal";
                    lblTextDebe.Text = "Monto Compromiso:";
                    dMontoDescuento = 0.0m;
                    lblDebe.Text = "-" + string.Format("{0:C2}", dMontoCompromiso); break;
                case "APAY":
                    lblTituloBuscaAuto.Text = "Cargar Abono Anticipado";
                    lblTextDebe.Text = "Monto con descuento:";
                    dMontoDescuento = decimal.Round(dMontoCompromiso * .10m, 2, MidpointRounding.AwayFromZero);
                    lblDebe.Text = string.Format("{0:C2}", dMontoCompromiso - dMontoDescuento); break;
            }
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
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvClientes.Rows[index];
                string sTraje = gvClientes.DataKeys[index].Values["fb_TrajeMedida"].S() == "" ? "False" : gvClientes.DataKeys[index].Values["fb_TrajeMedida"].S();
                switch (e.CommandName.S())
                {
                    case "Abonar":
                        eTipoPagos = eTipoPago.PagoMensual;
                        ddlTipoMov.SelectedIndex = 0;
                        lblTituloBuscaAuto.Text = "Cargar Abono Normal";
                        lblTextDebe.Text = "Monto Compromiso:";
                        lblDebe.Text = row.Cells[7].Text;
                        MontoDecimal(row.Cells[7].Text);
                        bTrajeMedida = Convert.ToBoolean(sTraje);
                        txtImporte.Text = string.Empty;
                        dMontoDescuento = 0.0m;
                        mpePagos.Show();
                        break;
                        //case "AbonoA":
                        //    eTipoPagos = eTipoPago.PagoMensual;
                        //    ddlTipoMov.SelectedIndex = 2;
                        //    lblTituloBuscaAuto.Text = "Cargar Abono Anticipado";
                        //    lblTextDebe.Text = "Monto con descuento:";
                        //    lblDebe.Text = row.Cells[7].Text;
                        //    txtImporte.Text = string.Empty;
                        //    mpePagos.Show();
                        //    break;
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
                        {
                            if (ddlTipoMov.SelectedValue.S() == "APAY" && bTrajeMedida)
                            {
                                MostrarMensaje("Este cliente no puede abonar un pago anticipado.", "Pago Anticipado");
                            }
                            else if(ddlTipoMov.SelectedValue.S() == "APAY" && ((dMontoCompromiso - dMontoDescuento) > txtImporte.Text.S().D()))
                            {
                                MostrarMensaje("El pago anticipado no puede ser menor a :" + string.Format("{0:C2}", dMontoCompromiso - dMontoDescuento), "Pago Anticipado");
                            }
                            else
                            {

                                eSetInsertaTran(sender, e);
                                mpePagos.Hide();
                                if (strBandera == "T")
                                {
                                    MostrarMensaje("Este fue el ultimo pago por procesar de esta cotización.", "Movimientos");
                                }
                            }
                        }
                        break;
                    case eTipoPago.PagoIndividual:
                        if (eSetInsertaPayInd != null)
                        {
                            eSetInsertaPayInd(sender, e);
                            mpePagos.Hide();
                        }
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
            catch (Exception ex)
            {
                MostrarMensaje("Error al abonar en el pago Individual --> Error: " + ex.Message, "Abonos");
            }
        }
        void omb_CancelButtonPressed(object sender, EventArgs args)
        {
            omb.Hide();
        }

        void omb_OkButtonPressed(object sender, EventArgs e)
        {
            Response.Redirect("frmAbonos.aspx");
        }
        void omb_Ok2ButtonPressed(object sender, EventArgs e)
        {
            if (bTrajeMedida)
            {
                omb2.Hide();
            }
            else if (ddlTipoMov.SelectedValue.S() == "APAY" && ((dMontoCompromiso - dMontoDescuento) > txtImporte.Text.S().D()))
            {
                omb2.Hide();
            }
            else
            {
                Response.Redirect("frmAbonos.aspx");
            }
        }

        #endregion

        #region METODOS
        public void MontoDecimal(string strCadenaFormato)
        {
            string strCadenaSinFormato = strCadenaFormato.Replace("$", "");
            dMontoCompromiso = decimal.Parse(strCadenaSinFormato);
        }
        public void LoadSucursales(DataTable dtSuc)
        {
            ddlSucursal.DataSource = dtSuc;
            ddlSucursal.DataValueField = "fi_Id";
            ddlSucursal.DataTextField = "fc_Descripcion";
            ddlSucursal.DataBind();
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            //string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
            omb2.ShowMessage(sMensaje, sCaption);
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
        protected bool IsEnabled(string statusValue)
        {
            if (statusValue == "" || statusValue == "False")
            {
                return true;
            }
            else
            {
                return false;
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
        public string sTipoAbono
        {
            get
            {
                return ddlTipoMov.SelectedValue.S();
            }
        }
        public bool bTrajeMedida
        {
            get { return (bool)ViewState["VSbTrajeMedida"]; }
            set { ViewState["VSbTrajeMedida"] = value; }
        }
        public decimal dMontoCompromiso
        {
            get { return (decimal)ViewState["VSdMontoCompromiso"]; }
            set { ViewState["VSdMontoCompromiso"] = value; }
        }
        public decimal dMontoDescuento
        {
            get { return (decimal)ViewState["VSdMontoDescuento"]; }
            set { ViewState["VSdMontoDescuento"] = value; }
        }
        public string strBandera
        {
            set;
            get;
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
                    dMonto = ddlTipoMov.SelectedValue.S() == "PAY" || ddlTipoMov.SelectedValue.S() == "APAY" ? txtImporte.Text.S().D() : txtImporte.Text.S().D() * -1,
                    dMontoDescuento = dMontoDescuento,
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