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
using Autos_SCC.Clases;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

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
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                if (eLoadObjects != null)
                    eLoadObjects(sender, e);
            }
        }

        protected void imbBuscarAuto_Click(object sender, EventArgs e)
        {
            gvAutos.DataSource = null;
            gvAutos.DataBind();
            txtTextoBusqueda.Visible = false;
            ddlMarcas.Visible = false;
            ddlTipoBusqueda.SelectedValue = "0";

            mpeBuscarAuto.Show();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            Page.Validate("VCotizar");
            if (Page.IsValid)
            {
                if (eCalculaCotizacion != null)
                    eCalculaCotizacion(sender, e);

                gvCotizar.DataSource = dtHeader;
                gvCotizar.DataBind();
                fPagos.Visible = true;
                pnlAgregarPagos.Visible = true;
                ColocaPlazo();
                updCotizar.Update();

                if (dTasaPreferencial != 0)
                    HidTasa.Value = dTasaPreferencial.S();
                else
                    HidTasa.Value = oParametro.sValor.S();

                HidGenerar.Value = "1";
            }
        }

        protected void GridCotizar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt;
            try
            {
                string strPlazoddl = ddlPlazo.SelectedValue.ToString();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView gv = (GridView)e.Row.FindControl("GrdCotizaDetalle");
                    Label lblP = (Label)e.Row.FindControl("lblPlazoS");

                    int iPlazo = gvCotizar.DataKeys[e.Row.RowIndex].Value.S().I();
                    double dPagoInicial = e.Row.Cells[6].Text.Replace("$","").Replace(",","").S().Db();

                    dt = Utils.CalculaDetalleCotizacion(iPlazo, dPagoInicial);
                    if (lblP.Text == strPlazoddl)
                    {
                        dtCotizacion = dt;
                    }
                        gv.DataSource = dt;
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
            string strCantidad = string.Empty;
            try
            {
                string strPlazo = string.Empty;
                string strPlazoddl = ddlPlazo.SelectedValue.ToString();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    strPlazo = DataBinder.Eval(e.Row.DataItem, "Plazo").ToString();

                    suma1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoNormal"));
                    suma2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoAdelantado"));
                    suma3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoMora"));

                    //e.Row.Cells[1].Text = e.Row.Cells[1].Text.D().ToString("c");
                    //strCantidad = e.Row.Cells[1].Text;
                    strCantidad = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoNormal")).S();
                    e.Row.Cells[1].Text = strCantidad.D().ToString("c");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                    strCantidad = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoAdelantado")).S();
                    e.Row.Cells[2].Text = strCantidad.D().ToString("c");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                    strCantidad = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PagoMora")).S();
                    e.Row.Cells[3].Text = strCantidad.D().ToString("c");
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                }

                if (strPlazo == strPlazoddl)
                {
                    sSuma1 = suma1.ToString("c"); ;
                    sSuma2 = suma2.ToString("c"); ;
                    sSuma3 = suma3.ToString("c"); ;
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

                btnGenerar_Click(sender, e);
                updCotizar.Update();
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

            btnGenerar_Click(sender, e);
            updCotizar.Update();
        }

        protected void btnCambiarTasa_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("VCambioTasa");
                if (Page.IsValid)
                {
                    dTasaPreferencial = txtTasaInteres.Text.S().Db();
                    btnGenerar_Click(sender, e);
                    updCotizar.Update();
                    txtTasaPreferencial.Text = dTasaPreferencial.S();
                    txtTasaInteres.Text = string.Empty;
                    pnlTasaPreferencial.Visible = true;
                    mpeCambioTasa.Hide();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al cambiar la tasa --> Error: " + ex.Message, "Cambio de tasa");
            }
        }

        protected void imbTasaPreferencial_Click(object sender, EventArgs e)
        {
            try
            {
                dTasaPreferencial = 0;
                btnGenerar_Click(sender, e);
                updCotizar.Update();
                pnlTasaPreferencial.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al cambiar la tasa --> Error: " + ex.Message, "Cambio de tasa");
            }
        }

        protected void ddlTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTipoBusqueda.SelectedValue)
            {
                case "1":// PLACA
                    lblTextoBusqueda.Text = "Placa";
                    txtTextoBusqueda.Visible = true;
                    break;
                case "3":// MODELO
                    lblTextoBusqueda.Text = "Modelo";
                    txtTextoBusqueda.Visible = false;
                    break;
                case "4":// COLOR
                    ddlMarcas.Visible = false;
                    txtTextoBusqueda.Visible = true;
                    lblTextoBusqueda.Text = "Color";
                    gvAutos.DataSource = null;
                    gvAutos.DataBind();
                    break;

                case "2":// MARCA
                    ddlMarcas.Visible = true;
                    txtTextoBusqueda.Visible = false;
                    lblTextoBusqueda.Text = "Marca";

                    if (eGetMarcas != null)
                        eGetMarcas(sender, e);

                    ddlMarcas.DataSource = dtMarcas;
                    ddlMarcas.DataTextField = "sDescripcion";
                    ddlMarcas.DataValueField = "iId";
                    ddlMarcas.DataBind();

                    gvAutos.DataSource = null;
                    gvAutos.DataBind();
                    break;
                
            }

            mpeBuscarAuto.Show();
        }

        protected void btnBuscarAuto_Click(object sender, EventArgs e)
        {
            if (eGetBusquedaAuto != null)
                eGetBusquedaAuto(sender, e);

            gvAutos.DataSource = dtBusquedaAuto;
            gvAutos.DataBind();

            mpeBuscarAuto.Show();
        }

        protected void gvAutos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string sPlaca = gvAutos.Rows[e.NewEditIndex].Cells[4].Text.S();
            string sAuto = gvAutos.Rows[e.NewEditIndex].Cells[0].Text.S();
            Auto oAuto = new DBAuto().DBExistsAuto(sPlaca);

            HidAuto.Value = oAuto.iId.S();
            txtPrecio.Text = Convert.ToInt32(oAuto.dPrecio).S();
            txtAuto.Text = sAuto;
            mpeBuscarAuto.Hide();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Page.Validate("VCotizar");
            if (Page.IsValid)
            {
                if (HidGenerar.Value == "1")
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }
                else
                    MostrarMensaje("Debe generar la cotización antes de guardar, favor de verificar", "Generar Cotización");
            }
        }

        protected void btnCambioTasa_Click(object sender, EventArgs e)
        {
            mpeCambioTasa.Show();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCot = new DataTable();
                DataTable dtExtras = new DataTable();
                DataSet dsC = new DataSet();
                DataColumn column;
                DataRow rowT;

                //foreach (GridViewRow row in gvCotizar.Rows)
                //{
                //    RadioButton rb = (RadioButton)row.FindControl("rbPlazo");
                //    if (rb != null)
                //    {
                //        if (rb.Checked)
                //        {
                //            GridView gv = (GridView)row.FindControl("GrdCotizaDetalle");

                            //Response.ClearContent();
                            //Response.AddHeader("content-disposition", "attachment; filename=" + "Cotizacion.xls");
                            //Response.ContentType = "application/excel";
                            //System.IO.StringWriter sw = new System.IO.StringWriter();
                            //HtmlTextWriter htw = new HtmlTextWriter(sw);
                            //gv.RenderControl(htw);
                            //Response.Write(sw.ToString());
                            //Response.End();



                            




                //        }
                //    }
                //}

                column = new DataColumn();
                column.ColumnName = "Cliente";
                dtExtras.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Vehiculo";
                dtExtras.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Precio";
                dtExtras.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Sucursal";
                dtExtras.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Suma1";
                dtExtras.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Suma2";
                dtExtras.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "Suma3";
                dtExtras.Columns.Add(column);

                



                rowT = dtExtras.NewRow();
                rowT["Cliente"] = txtNombre.Text.Trim() + " " + txtSegNombre.Text.Trim() + " " + txtApePaterno.Text.Trim() + " " + txtApeMaterno.Text.Trim();
                rowT["Vehiculo"] = txtAuto.Text;
                rowT["Precio"] = txtPrecio.Text;
                rowT["Sucursal"] = ddlSucursal.SelectedItem.Text;
                rowT["Suma1"] = sSuma1;
                rowT["Suma2"] = sSuma2;
                rowT["Suma3"] = sSuma3;
                dtExtras.Rows.Add(rowT);

                string strPath = string.Empty;
                ReportDocument rd = new ReportDocument();
                strPath = Server.MapPath("RPT\\rptCotizacion.rpt");
                strPath = strPath.Replace("\\Views\\Principales", "");
                rd.Load(strPath, OpenReportMethod.OpenReportByDefault);

                dtCot = dtCotizacion;

                foreach(DataRow drC in dtCot.Rows)
                {
                    drC["PagoNormal"]  = drC["PagoNormal"].D().ToString("c");
                    drC["PagoAdelantado"] = drC["PagoAdelantado"].D().ToString("c");
                    drC["PagoMora"] = drC["PagoMora"].D().ToString("c");
                }
                dtCot.TableName = "Cot";
                dtExtras.TableName = "Extras";

                dsC.Tables.Add(dtCot);
                dsC.Tables.Add(dtExtras);

                rd.SetDataSource(dsC);

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Cotizacion");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al exportar: " + ex.Message, "Error al exportar");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }

        protected void rbPlazo_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectButton = (RadioButton)sender;
            GridViewRow row = (GridViewRow)selectButton.Parent.Parent;
            int a = row.RowIndex;
            foreach (GridViewRow rw in gvCotizar.Rows)
            {
                if (selectButton.Checked)
                {
                    if (rw.RowIndex != a)
                    {
                        RadioButton rd = rw.FindControl("rbPlazo") as RadioButton;
                        rd.Checked = false;
                    }
                    else
                    {
                        ddlPlazo.SelectedValue = rw.Cells[2].Text.S().Replace(" Meses", "");
                    }
                }
            }
        }

        protected void imbBuscaCliente_Click(object sender, EventArgs e)
        {
            CargaClientes(new DataTable());
            mpeBusquedaCliente.Show();
        }

        protected void gvBusClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvBusClientes.SelectedRow;
            HidClienteE.Value = gvBusClientes.DataKeys[row.RowIndex].Value.S();

            txtNombre.Text = row.Cells[0].Text.S();
            txtSegNombre.Text = row.Cells[1].Text.S().Replace("&nbsp;", "");
            txtApePaterno.Text = row.Cells[2].Text.S();
            txtApeMaterno.Text = row.Cells[3].Text.S();

            mpeBusquedaCliente.Hide();
        }

        protected void btnBuscarBusCliente_Click(object sender, EventArgs e)
        {
            if (eSearchCliente != null)
                eSearchCliente(sender, e);
            mpeBusquedaCliente.Show();
        }

        #endregion

        #region METODOS
        public void LoadObjects(DataTable dtObj)
        {
            ddlPlazo.DataSource = dtObj;
            ddlPlazo.DataValueField = "sDescripcion";
            ddlPlazo.DataTextField = "sValorDescripcion";
            ddlPlazo.DataBind();
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

        private void ColocaPlazo()
        {
            string sPlazo = ddlPlazo.SelectedValue.S();

            foreach (GridViewRow row in gvCotizar.Rows)
            {
                if (sPlazo == gvCotizar.DataKeys[row.RowIndex].Value.S())
                {
                    RadioButton rb = (RadioButton)row.FindControl("rbPlazo");
                    if (rb != null)
                    {
                        rb.Checked = true;
                    }
                }
            }
        }

        public void LimpiaDatos()
        {
            HidAuto.Value = "0";
            HidTasa.Value = "0";
            txtNombre.Text = string.Empty;
            txtSegNombre.Text = string.Empty;
            txtApePaterno.Text = string.Empty;
            txtApeMaterno.Text = string.Empty;
            txtAuto.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtEnganche.Text = string.Empty;
            txtCorreoElectronico.Text = string.Empty;
            gvCotizar.DataSource = null;
            gvCotizar.DataBind();
            fPagos.Visible = false;
            gvPagosIndividuales.DataSource = null;
            gvPagosIndividuales.DataBind();
            txtTasaPreferencial.Text = string.Empty;
            ddlPlazo.SelectedIndex = 0;
            ddlSucursal.SelectedIndex = 0;
        }

        public void CargaClientes(DataTable dt)
        {
            gvBusClientes.DataSource = dt;
            gvBusClientes.DataBind();
        }

        private List<PagoIndividual> ObtienePagosI()
        {
            List<PagoIndividual> oLst = new List<PagoIndividual>();
            try
            {
                if (dtPagosIndividuales.Rows.Count > 0)
                {
                    foreach (DataRow row in dtPagosIndividuales.Rows)
                    {
                        PagoIndividual oPago = new PagoIndividual();
                        oPago.dMonto = row["Importe"].S().D();
                        oPago.dtFechaPago = row["Fecha"].S().Dt();
                        oPago.sUsuario = Session["usuario"].S();

                        oLst.Add(oPago);
                    }

                    return oLst;
                }
                else
                    return oLst;
            }
            catch
            {
                return new List<PagoIndividual>();
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
        public event EventHandler eLoadObjects;
        public event EventHandler eGetMarcas;
        public event EventHandler eGetBusquedaAuto;
        public event EventHandler eSearchCliente;

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
                    dMonto = txtImportePago.Text.S().D(),
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
                    sNombre = txtNombre.Text.S(),
                    sSegNombre = txtSegNombre.Text.S(),
                    sApePaterno = txtApePaterno.Text.S(),
                    sApeMaterno = txtApeMaterno.Text.S(),
                    iPlazo = iGetPlazo,
                    iIdAuto = HidAuto.Value.S().I(),
                    dPrecio = txtPrecio.Text.S().D(),
                    dEnganche = txtEnganche.Text.S().D(),
                    dTasa = HidTasa.Value.S().D(),
                    iIdSucursal = ddlSucursal.SelectedValue.I(),
                    sCorreo = txtCorreoElectronico.Text.S(),
                    sUsuario = Session["usuario"].S(),
                    oLsPagoIndividual = ObtienePagosI(),
                    iIdClienteAnt = HidClienteE.Value.S().I()
                };
            }
        }

        public string sSuma1
        {
            get { return (string)ViewState["VESuma1"]; }
            set { ViewState["VESuma1"] = value; }
        }

        public string sSuma2
        {
            get { return (string)ViewState["VESuma2"]; }
            set { ViewState["VESuma2"] = value; }
        }

        public string sSuma3
        {
            get { return (string)ViewState["VESuma3"]; }
            set { ViewState["VESuma3"] = value; }
        }
        public DataTable dtCotizacion
        {
            get { return (DataTable)ViewState["VECot"]; }
            set { ViewState["VECot"] = value; }
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

        public DataTable dtMarcas
        {
            get;
            set;
        }

        public DataTable dtBusquedaAuto
        {
            get;
            set;
        }

        public int iMarca
        {
            get
            {
                return ddlMarcas.SelectedValue.S().I();
            }
        }

        public object[] oArrFiltros
        {
            get
            {
                string sPar = string.Empty;
                switch (ddlTipoBusqueda.SelectedValue)
                {
                    case "1":
                        sPar = txtTextoBusqueda.Text.S();
                        break;
                    case "2":
                        sPar = iMarca.S();
                        break;
                    case "3":
                    case "4":
                        sPar = txtTextoBusqueda.Text.S();
                        break;
                }

                return new object[]{
                    ddlTipoBusqueda.SelectedValue, sPar};
            }
        }

        public int iGetPlazo
        {
            get
            {
                int _iPlazo = 0;

                foreach (GridViewRow row in gvCotizar.Rows)
                {
                    RadioButton rb = (RadioButton)row.FindControl("rbPlazo");
                    if (rb != null)
                    {
                        if (rb.Checked)
                        {
                            _iPlazo = gvCotizar.DataKeys[row.RowIndex].Value.S().I();
                        }
                    }
                }

                return _iPlazo;
            }
        }

        public string sNombreCli
        {
            get { return txtBusCliNombre.Text; }
        }

        public bool bSinIntereses
        {
            get { return chkSinIntereses.Checked; }
        }
        #endregion
                
    }
}