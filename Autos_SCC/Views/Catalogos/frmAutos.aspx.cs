using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using Autos_SCC.Views.ControlesUsuario;
using Autos_SCC.DomainModel;
using System.Drawing;
using System.Data;
using Autos_SCC.Objetos;
using NucleoBase.Core;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Autos_SCC.Clases;

namespace Autos_SCC.Views.Catalogos
{
    public partial class frmAutos : System.Web.UI.Page, IViewAuto
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Auto_Presenter(this, new DBAuto());

            omb.OkButtonPressed += new ucModalConfirm.OkButtonPressedHandler(omb_OkButtonPressed);
            omb.CancelButtonPressed += new ucModalConfirm.CancelButtonPressedHandler(omb_CancelButtonPressed);
            omb2.OkButtonPressed += new ucModalAlert.OkButtonPressedHandler(omb_Ok2ButtonPressed);

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                oPresenter.LoadObjects_Presenter();
            }
        }

        protected void gvCatalogo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("OnMouseOver", "On(this);");
                    e.Row.Attributes.Add("OnMouseOut", "Off(this);");
                    e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(this.gvCatalogo, "Select$" + e.Row.RowIndex.ToString());
                }
            }
        }

        protected void gvCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            bool ban = false;
            foreach (GridViewRow row in gvCatalogo.Rows)
            {
                if (row.RowIndex == gvCatalogo.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    //i = Convert.ToInt32(gvCatalogo.Rows[row.RowIndex].Cells[1].Text);
                    i = Convert.ToInt32(gvCatalogo.DataKeys[row.RowIndex]["fi_Id"].ToString());
                    ban = true;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Clic para seleccionar esta fila.";
                }
            }

            if (ban)
            {
                if (eObjSelected != null)
                    eObjSelected(sender, e);
                ttlAuto.InnerText = "Edición de Autos";
                btnGuardar.Text = "EDITAR";
                btnLimpiar.Visible = false;
                UpaAgregarVehiculo.Update();
                mpeAgregarVehiculo.Show();
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetTiposAuto != null)
                eGetTiposAuto(sender, e);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ttlAuto.InnerText = "Registro de Autos";
            btnGuardar.Text = "GUARDAR";
            btnLimpiar.Visible = true;
            UpaAgregarVehiculo.Update();
            if (eNewObj != null)
                eNewObj(sender, e);
            //mpprueba.Show();
            mpeAgregarVehiculo.Show();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (eSaveObj != null)
                eSaveObj(sender, e);
            //mpeAgregarVehiculo.Hide();
            upaTab.Update();
            

        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    omb.ShowMessage("¿Realmente esta seguro de eliminar el registro?", "Elimina");
        //}

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            oPresenter.LoadObjects_Presenter();
            LimpiaControles();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("fi_Id");
                dt.Columns.Add("fc_Marca");
                dt.Columns.Add("fc_Version");
                dt.Columns.Add("fc_TipoAuto");
                dt.Columns.Add("fm_Precio");
                dt.Columns.Add("fc_Placa");
                dt.Columns.Add("fc_Estado");
                dt.Columns.Add("fi_Modelo");
                dt.Columns.Add("fc_NoSerie");
                dt.Columns.Add("fc_Sucursal");
                dt.Columns.Add("fc_Color");
                dt.Columns.Add("fi_Kilometraje");

                dt.Columns.Add("fc_NumMotor");
                dt.Columns.Add("fc_TenenciaHasta");
                dt.Columns.Add("fc_Factura");
                dt.Columns.Add("fc_Numero");

                dt.Columns.Add("fc_Usuario");
                dt.Columns.Add("fd_FechaUltMovimiento");

                foreach (GridViewRow row in gvCatalogo.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["fi_Id"] = row.Cells[1].Text.S();
                    dr["fc_Marca"] = row.Cells[2].Text.S();
                    dr["fc_Version"] = row.Cells[3].Text.S();
                    dr["fc_TipoAuto"] = row.Cells[4].Text.S();
                    dr["fm_Precio"] = row.Cells[5].Text.S();
                    dr["fc_Placa"] = row.Cells[6].Text.S();
                    dr["fc_Estado"] = row.Cells[16].Text.S();
                    dr["fi_Modelo"] = row.Cells[7].Text.S();
                    dr["fc_NoSerie"] = row.Cells[8].Text.S();
                    dr["fc_Sucursal"] = row.Cells[9].Text.S();
                    dr["fc_Color"] = row.Cells[10].Text.S();
                    dr["fi_Kilometraje"] = row.Cells[11].Text.S();

                    dr["fc_NumMotor"] = row.Cells[17].Text.S();
                    dr["fc_TenenciaHasta"] = row.Cells[18].Text.S();
                    dr["fc_Factura"] = row.Cells[19].Text.S();
                    dr["fc_Numero"] = row.Cells[20].Text.S();

                    dr["fc_Usuario"] = row.Cells[13].Text.S();
                    dr["fd_FechaUltMovimiento"] = row.Cells[14].Text.S();

                    dt.Rows.Add(dr);
                }

                gvCopia.DataSource = dt;
                gvCopia.DataBind();

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + "CatalogoAutos.xls");
                Response.ContentType = "application/excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gvCopia.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                omb2.ShowMessage("Ocurrio el siguiente error: " + ex.Message, "Error al exportar");
                //MostrarMensaje("Ocurrio el siguiente error: " + ex.Message,"Error al exportar");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }
      
        protected void can_Click(object sender, EventArgs e)
        {
            mpeAgregarGasto.Hide();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mpeAgregarVehiculo.Hide();
        }

        void omb_CancelButtonPressed(object sender, EventArgs args)
        {
            omb.Hide();
        }

        void omb_OkButtonPressed(object sender, EventArgs e)
        {
            if (eDeleteObj != null)
                eDeleteObj(sender, e);
        }
        void omb_Ok2ButtonPressed(object sender, EventArgs e)
        {
            omb2.Hide();
        }

        protected void ddlTipoAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetVersiones != null)
                eGetVersiones(sender, e);
        }

        protected void gvCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)        
        {
            try
            {
                if (e.CommandName == "AgregarGasto")
                {
                    iAuto = gvCatalogo.DataKeys[e.CommandArgument.I()].Value.S().I();

                    if (eGetGastosPorAuto != null)
                        eGetGastosPorAuto(sender, e);

                    gvGastos.DataSource = dtGastosAuto;
                    gvGastos.DataBind();

                    mpeAgregarGasto.Show();
                }
                if(e.CommandName == "EliminarAuto")
                {

                    iAuto = gvCatalogo.DataKeys[e.CommandArgument.I()].Value.S().I();
                    gvCatalogo.SelectedIndex = e.CommandArgument.I();
                    if (eObjSelected != null)
                        eObjSelected(sender, e);
                    omb.ShowMessage("¿Realmente esta seguro de eliminar el registro?", "Elimina");
                }
            }
            catch (Exception ex)
            {
                omb2.ShowMessage(ex.Message, "Problemas en el sistema");
                //MostrarMensaje(ex.Message, "Problemas en el sistema");
            }
        }

        protected void btnGuardarGasto_Click(object sender, EventArgs e)
        {
            iGastoAuto = 0;

            if (eSaveGastoAuto != null)
                eSaveGastoAuto(sender, e);

            gvGastos.DataSource = dtGastosAuto;
            gvGastos.DataBind();

            txtDescripcionGasto.Text = string.Empty;
            txtMonto.Text = string.Empty;

            mpeAgregarGasto.Show();
        }
        
        protected void gvGastos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            iGastoAuto = gvGastos.DataKeys[e.RowIndex].Value.S().I();

            if (eDeleteGastoAuto != null)
                eDeleteGastoAuto(sender, e);

            gvGastos.DataSource = dtGastosAuto;
            gvGastos.DataBind();

            mpeAgregarGasto.Show();
        }

        protected void gvGastos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
             {
                 subtotal += e.Row.Cells[1].Text.S().Replace("$","").D();
             }

             if (e.Row.RowType == DataControlRowType.Footer)
             {
                 e.Row.Cells[0].Text = "Totales:";
                 e.Row.Cells[1].Text = "$ " + subtotal.S();
             }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eSearchAutos != null)
                eSearchAutos(sender, e);

            gvCatalogo.DataSource = dtDataSource;
            gvCatalogo.DataBind();
        }

        #endregion

        #region METODOS
        public void LoadObjects(DataTable dtObject)
        {
            gvCatalogo.DataSource = null;

            if (dtObject.Rows.Count > 0)
            {
                gvCatalogo.DataSource = dtObject;
                gvCatalogo.DataBind();
                btnExportar.Visible = true;
            }
            else
            {
                gvCatalogo.DataBind();
                btnExportar.Visible = false;
            }
        }

        public void LoadMarcas(DataTable dtObjet)
        {
            ddlMarca.DataSource = dtObjet;
            ddlMarca.DataValueField = "iId";
            ddlMarca.DataTextField = "sDescripcion";
            ddlMarca.DataBind();

            ddlTipoAuto.DataSource = null;
            ListItem oItem = new ListItem();
            oItem.Text = "Seleccione";
            oItem.Value = "0";
            ddlTipoAuto.Items.Add(oItem);

            ddlVersion.DataSource = null;
            ddlVersion.Items.Add(oItem);
        }

        public void LoadTiposAuto(DataTable dtObj)
        {
            if (dtObj.Rows.Count > 0)
            {
                ddlTipoAuto.Items.Clear();

                ddlTipoAuto.DataSource = dtObj;
                ddlTipoAuto.DataValueField = "fi_Id";
                ddlTipoAuto.DataTextField = "fc_Descripcion";
                ddlTipoAuto.DataBind();

                ddlTipoAuto_SelectedIndexChanged(null, EventArgs.Empty);
            }
            else
            {
                ddlTipoAuto.Items.Clear();
                ddlTipoAuto.Items.Add(new ListItem("Seleccione", "0"));
            }

        }

        public void LoadVersiones(DataTable dtObj)
        {
            if (dtObj.Rows.Count > 0)
            {
                ddlVersion.Items.Clear();

                ddlVersion.DataSource = dtObj;
                ddlVersion.DataValueField = "fi_Id";
                ddlVersion.DataTextField = "fc_Descripcion";
                ddlVersion.DataBind();
            }
            else
            {
                ddlVersion.Items.Clear();
                ddlVersion.Items.Add(new ListItem("Seleccione", "0"));
            }
        }

        public void LoadSucursales(DataTable dtObj)
        {
            ddlSucursal.DataSource = dtObj;
            ddlSucursal.DataValueField = "fi_Id";
            ddlSucursal.DataTextField = "fc_Descripcion";
            ddlSucursal.DataBind();

            ddlSucursalBus.DataSource = dtObj;
            ddlSucursalBus.DataValueField = "fi_Id";
            ddlSucursalBus.DataTextField = "fc_Descripcion";
            ddlSucursalBus.DataBind();
        }

        public void LoadEstatusAuto(DataTable dtObj)
        {
            ddlEstatus.DataSource = dtObj;
            ddlEstatus.DataValueField = "iId";
            ddlEstatus.DataTextField = "sDescripcion";
            ddlEstatus.DataBind();

            ddlEstatusBus.DataSource = dtObj;
            ddlEstatusBus.DataValueField = "iId";
            ddlEstatusBus.DataTextField = "sDescripcion";
            ddlEstatusBus.DataBind();
        }

        public void LoadEstados(DataTable dtEstados)
        {
            if (dtEstados.Rows.Count > 0)
            {
                ddlEstado.DataSource = dtEstados;
                ddlEstado.DataValueField = "fi_Id";
                ddlEstado.DataTextField = "fc_Descripcion";
                ddlEstado.DataBind();
            }
        }

        private void LimpiaControles()
        {
            txtId.Text = string.Empty;
            ddlMarca.SelectedValue = "0";
            ddlVersion.SelectedValue = "0";
            ddlSucursal.SelectedValue = "0";

            ddlTipoAuto.Items.Clear();

            txtPrecio.Text = string.Empty;
            txtKilometraje.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtNoSerie.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtBuqueda.Text = string.Empty;
            ddlEstatus.SelectedValue = "0";

            ddlEstado.SelectedValue = "0";
            txtNumMotor.Text = string.Empty;
            txtTenencia.Text = string.Empty;
            txtFactura.Text = string.Empty;
            txtNumero.Text = string.Empty;
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            omb2.ShowMessage(sMensaje, sCaption);
            //string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        #endregion

        #region "Vars y Propiedades"
        Auto_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetTiposAuto;
        public event EventHandler eGetVersiones;
        public event EventHandler eGetGastosPorAuto;
        public event EventHandler eSaveGastoAuto;
        public event EventHandler eDeleteGastoAuto;
        public event EventHandler eSearchAutos;

        public Auto oAuto
        {
            get
            {
                return new Auto
                {
                    iId = txtId.Text.S() == string.Empty ? -1 : txtId.Text.I(),
                    iIdMarca = ddlMarca.SelectedValue.S().I(),
                    iIdVersion = ddlVersion.SelectedValue == null ? 0 : ddlVersion.SelectedValue.S().I(),
                    iIdTipoAuto = ddlTipoAuto.SelectedValue.S().I(),
                    sPlaca = txtPlaca.Text.S(),
                    IEstado = ddlEstado.SelectedItem.Value.I(),
                    sNoSerie = txtNoSerie.Text.S(),
                    iModelo = txtModelo.Text.S().I(),
                    sColor = txtColor.Text.S(),
                    iIdSucursal = ddlSucursal.SelectedValue.S().I(),
                    dPrecio = txtPrecio.Text.S().D(),
                    iKilometraje = txtKilometraje.Text.S().I(),
                    SNumMotor = txtNumMotor.Text.S(),
                    STenencia = txtTenencia.Text.S(),
                    SFactura = txtFactura.Text.S(),
                    SNumero = txtNumero.Text.S(),
                    iStatus = ddlEstatus.SelectedValue.S().I(),
                    sUsuario = Session["usuario"].S()
                };
            }
            set
            {
                Auto oCat = value as Auto;
                if (oCat != null)
                {
                    txtId.Text = oCat.iId == -1 ? string.Empty : oCat.iId.S();
                    ddlMarca.SelectedValue = oCat.iIdMarca.S() != "0" ? oCat.iIdMarca.S() : ddlMarca.SelectedValue;
                    
                    if (eGetTiposAuto != null)
                        eGetTiposAuto(null, EventArgs.Empty);
                    ddlTipoAuto.SelectedValue = oCat.iIdTipoAuto.S();
                    if(eGetVersiones != null)
                        eGetVersiones(null,EventArgs.Empty);
                    ddlVersion.SelectedValue = oCat.iIdVersion.S();
                                        
                    txtPlaca.Text = oCat.sPlaca;

                    ddlEstado.SelectedValue = oCat.IEstado.S();

                    txtNoSerie.Text = oCat.sNoSerie;
                    txtModelo.Text = oCat.iModelo.S();
                    txtColor.Text = oCat.sColor;
                    ddlSucursal.SelectedValue = oCat.iIdSucursal.S();
                    txtPrecio.Text = oCat.dPrecio.S();
                    txtKilometraje.Text = oCat.iKilometraje.S();

                    txtNumMotor.Text = oCat.SNumMotor.S();
                    txtTenencia.Text = oCat.STenencia.S();
                    txtFactura.Text = oCat.SFactura.S();
                    txtNumero.Text = oCat.SNumero.S();

                    ddlEstatus.SelectedValue = oCat.iStatus.S();
                }
            }
        }

        public GastosAuto oGastoAuto
        {
            get
            {
                return new GastosAuto
                {
                    iId = iGastoAuto.S().I(),
                    iIdAuto = iAuto,
                    sDescripcion = txtDescripcionGasto.Text,
                    iMonto = txtMonto.Text.I(),
                    sUsuario = Session["usuario"].S(),
                    dtFechaUltMov = DateTime.Now
                };
            }
            set
            {
                GastosAuto oGastoA = value as GastosAuto;
                if (oGastoA != null)
                {
                    iGastoAuto = oGastoA.iId;
                    iAuto = oGastoA.iIdAuto;                    
                }
            }
        }

        public Auto oGetSetObjSelection
        {
            get
            {
                Auto oCat = null;

                int iFila = gvCatalogo.SelectedIndex;
                if (iFila >= 0)
                {
                    oCat = new Auto();
                    oCat.iId = gvCatalogo.DataKeys[iFila]["fi_Id"].S().I();
                    //oCat.iId = gvCatalogo.Rows[iFila].Cells[1].Text.S().I();
                }

                return oCat;
            }
            set
            {
                Auto oCat = value;
                //gvCatalogo.FocusedRowHandle = gvCatalogo.LocateByValue("iId", oCat.iId, null);
            }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[]{
                    "@fc_Desc", "%" + txtBuqueda.Text.S() + "%",
                    "@fi_Sucursal", ddlSucursalBus.SelectedValue.S(),
                    "@fi_Status", ddlEstatusBus.SelectedValue.S()
                };
            }
        }

        private decimal subtotal = 0;

        private DataTable _dtGastosAuto = new DataTable();
        public DataTable dtGastosAuto
        {
            get { return _dtGastosAuto; }
            set { _dtGastosAuto = value; }
        }

        public int iAuto
        {
            get { return (int)ViewState["Auto"]; }
            set { ViewState["Auto"] = value; }
        }

        public int iGastoAuto
        {
            get { return (int)ViewState["GastoAuto"]; }
            set { ViewState["GastoAuto"] = value; }
        }

        public DataTable dtDataSource
        {
            get;
            set;
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