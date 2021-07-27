using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Objetos;
using Autos_SCC.Presenter;
using Autos_SCC.Interfaces;
using NucleoBase.Core;
using System.Data;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Views.Principales
{
    public partial class Cotizador : System.Web.UI.Page, IViewCotizador
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Cotizador_Presenter(this, new DBCotizador());

            if (!IsPostBack)
            {                
                Session["usuario"] = "iMorato";
                if (Session["usuario"] == null)
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

        protected void imbBuscarAuto_Click(object sender, EventArgs e)
        {

        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            if (eCalculaCotizacion != null)
                eCalculaCotizacion(sender, e);

            gvCotizar.DataSource = dtHeader;
            gvCotizar.DataBind();
            pnlAgregarPagos.Visible = true;
        }

        protected void GridCotizar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView gv = (GridView)e.Row.FindControl("GrdCotizaDetalle");

                    int iPlazo = gvCotizar.DataKeys[e.Row.RowIndex].Value.S().I();
                    double dPagoInicial = e.Row.Cells[6].Text.Replace("$","").Replace(",","").S().Db();

                    gv.DataSource = CalculaDetalleCotizacion(iPlazo,dPagoInicial);
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al generar la cotización --> Error: " + ex.Message, "Cotizador");
            }
        }

        protected void GrdCotizaDetalle_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    suma1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoNormal"));
                    suma2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoAdelantado"));
                    suma3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoMora"));

                    e.Row.Cells[1].Text = e.Row.Cells[1].Text.D().ToString("c");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.D().ToString("c");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.D().ToString("c");
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "Total:";
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[1].Text = suma1.ToString("c");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].Text = suma2.ToString("c");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].Text = suma3.ToString("c");
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;

                    suma1 = 0;
                    suma2 = 0;
                    suma3 = 0;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al generar la cotización --> Error: " + ex.Message, "Cotizador");
            }
        }

        protected void btnAgregarPago_Click(object sender, EventArgs e)
        {
            mpeAgregarPago.Show();
        }

        protected void btnAgregarPagoModal_Click(object sender, EventArgs e)
        {
            Page.Validate("PagoIndividual");
            if (Page.IsValid)
            {
                if(eAgregaPagoIndividual != null)
                    eAgregaPagoIndividual(sender, e);

                gvPagosIndividuales.DataSource = dtPagosIndividuales;
                gvPagosIndividuales.DataBind();
                LimpiaModal(1);
                mpeAgregarPago.Hide();
            }
        }

        protected void btnCancelarPagoModal_Click(object sender, EventArgs e)
        {
            LimpiaModal(1);
            mpeAgregarPago.Hide();
        }

        protected void gvPagosIndividuales_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dtPagosIndividuales.Rows.RemoveAt(e.RowIndex);
            gvPagosIndividuales.DataSource = dtPagosIndividuales;
            gvPagosIndividuales.DataBind();
        }

        protected void btnCambiarTasa_Click(object sender, EventArgs e)
        {
            try
            {
                btnGenerar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al cambiar la tasa --> Error: " + ex.Message, "Cambio de tasa");
            }
        }

        #endregion

        #region METODOS
        private DataTable CalculaDetalleCotizacion(int iPlazo, double dPrimerPago)
        {
            try
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle.Columns.Add("Plazo");
                dtDetalle.Columns.Add("NoPago");
                dtDetalle.Columns.Add("PagoNormal");
                dtDetalle.Columns.Add("PagoAdelantado");
                dtDetalle.Columns.Add("PagoMora");

                for (int i = 0; i < iPlazo; i++)
                {
                    DataRow row = dtDetalle.NewRow();
                    row["Plazo"] = iPlazo;
                    row["NoPago"] = (i + 1).S();
                    row["PagoNormal"] = Math.Round(dPrimerPago,2);
                    row["PagoAdelantado"] = Math.Round((dPrimerPago * .90),2);
                    row["PagoMora"] = Math.Round((dPrimerPago * 1.10),2);

                    dtDetalle.Rows.Add(row);
                }

                return dtDetalle;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        private void LimpiaModal(int iNumModal)
        {
            switch (iNumModal)
            {
                case 1:
                    txtImportePago.Text = string.Empty;
                    txtFechaPago.Text = string.Empty;
                    break;
            }
        }
        #endregion

        #region "Vars y Propiedades"
        private decimal suma1 = 0;
        private decimal suma2 = 0;
        private decimal suma3 = 0;

        Cotizador_Presenter oPresenter;
        public event EventHandler eCalculaCotizacion;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eAgregaPagoIndividual;

        public Parametro oParametro
        {
            set;
            get;
        }

        public DataTable dtHeader
        {
            set;
            get;
        }

        public PagoIndividual oPagoI
        {
            get
            {
                return new PagoIndividual
                {
                    dImporte = txtImportePago.Text.S().D(),
                    dtFechaPago = txtFechaPago.Text.S().Dt()
                };
            }
        }

        public Cotizacion oCotizacion
        {
            get
            {
                return new Cotizacion
                {
                    dPrecio = txtPrecio.Text.S().D(),
                    dEnganche = txtEnganche.Text.S().D()
                };
            }
        }

        public DataTable dtPagosIndividuales
        {
            get { return (DataTable)ViewState["PagoIndividual"]; }
            set { ViewState["PagoIndividual"] = value; }
        }

        public double dPagosIndividuales
        {            
            get
            {
                double total = 0;

                if (gvPagosIndividuales.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvPagosIndividuales.Rows)
                    {
                        total = total + row.Cells[0].Text.S().Db();
                    }

                    return total;
                }
                else
                    return 0;
            }
        }

        public double dTasaPreferencial
        {
            get { return (Double)ViewState["TasaPreferencial"]; }
            set { ViewState["TasaPreferencial"] = value; }
        }

        #endregion
                
    }
}