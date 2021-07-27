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

namespace Autos_SCC.Views.Catalogos
{
    public partial class frmAutos : System.Web.UI.Page, IViewAuto
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Auto_Presenter(this, new DBAuto());
            Session["usuario"] = "iMorato";

            omb.OkButtonPressed += new ucModalConfirm.OkButtonPressedHandler(omb_OkButtonPressed);
            omb.CancelButtonPressed += new ucModalConfirm.CancelButtonPressedHandler(omb_CancelButtonPressed);

            if (!IsPostBack)
            {
                //Response.Expires = 0;

                if (Session["usuario"] == null)
                {
                    Response.Redirect("login.aspx");
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
                    i = Convert.ToInt32(gvCatalogo.Rows[row.RowIndex].Cells[0].Text);
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
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetTiposAuto != null)
                eGetTiposAuto(sender, e);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (eNewObj != null)
                eNewObj(sender, e);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (eSaveObj != null)
                eSaveObj(sender, e);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            omb.ShowMessage("¿Realmente esta seguro de eliminar el registro?", "Elimina");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiaControles();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void can_Click(object sender, EventArgs e)
        {
            mpeAgregarGasto.Hide();
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
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "Problemas en el sistema");
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

        #endregion

        #region METODOS
        public void LoadObjects(DataTable dtObject)
        {
            gvCatalogo.DataSource = null;

            if (dtObject.Rows.Count > 0)
            {
                gvCatalogo.DataSource = dtObject;
                gvCatalogo.DataBind();
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
                ddlTipoAuto.DataSource = dtObj;
                ddlTipoAuto.DataValueField = "fi_Id";
                ddlTipoAuto.DataTextField = "fc_Descripcion";
                ddlTipoAuto.DataBind();

                ddlTipoAuto_SelectedIndexChanged(null, EventArgs.Empty);
            }
            else
                ddlTipoAuto.DataSource = null;
        }

        public void LoadVersiones(DataTable dtObj)
        {
            ddlVersion.DataSource = dtObj;
            ddlVersion.DataValueField = "fi_Id";
            ddlVersion.DataTextField = "fc_Descripcion";
            ddlVersion.DataBind();
        }

        public void LoadSucursales(DataTable dtObj)
        {
            ddlSucursal.DataSource = dtObj;
            ddlSucursal.DataValueField = "fi_Id";
            ddlSucursal.DataTextField = "fc_Descripcion";
            ddlSucursal.DataBind();
        }

        public void LoadEstatusAuto(DataTable dtObj)
        {
            ddlEstatus.DataSource = dtObj;
            ddlEstatus.DataValueField = "iId";
            ddlEstatus.DataTextField = "sDescripcion";
            ddlEstatus.DataBind();
        }

        private void LimpiaControles()
        {
            txtId.Text = string.Empty;
            ddlMarca.SelectedValue = "0";
            ddlVersion.SelectedValue = "0";
            ddlSucursal.SelectedValue = "0";

            ddlTipoAuto.Items.Clear();

            txtPlaca.Text = string.Empty;
            txtNoSerie.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtBuqueda.Text = string.Empty;
            ddlEstatus.SelectedValue = "0";
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
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

        public Auto oAuto
        {
            get
            {
                return new Auto
                {
                    iId = txtId.Text.S() == string.Empty ? -1 : txtId.Text.I(),
                    iIdMarca = ddlMarca.SelectedValue.S().I(),
                    iIdVersion = ddlVersion.DataSource == null ? 0 : ddlVersion.SelectedValue.S().I(),
                    iIdTipoAuto = ddlTipoAuto.SelectedValue.S().I(),
                    sPlaca = txtPlaca.Text.S(),
                    sNoSerie = txtNoSerie.Text.S(),
                    sColor = txtColor.Text.S(),
                    iIdSucursal = ddlSucursal.SelectedValue.S().I(),
                    dPrecio = txtPrecio.Text.S().D(),
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
                    ddlMarca.SelectedValue = oCat.iIdMarca.S();
                    
                    if (eGetTiposAuto != null)
                        eGetTiposAuto(null, EventArgs.Empty);
                    ddlTipoAuto.SelectedValue = oCat.iIdTipoAuto.S();
                    if(eGetVersiones != null)
                        eGetVersiones(null,EventArgs.Empty);
                    ddlVersion.SelectedValue = oCat.iIdVersion.S();
                                        
                    txtPlaca.Text = oCat.sPlaca;
                    txtNoSerie.Text = oCat.sNoSerie;
                    txtColor.Text = oCat.sColor;
                    ddlSucursal.SelectedValue = oCat.iIdSucursal.S();
                    txtPrecio.Text = oCat.dPrecio.S();
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
                    oCat.iId = gvCatalogo.Rows[iFila].Cells[0].Text.S().I();
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
                    "@fcDesc", "%" + txtBuqueda.Text.S() + "%",
                    "@fcActivo", 1
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
        #endregion
    }
}