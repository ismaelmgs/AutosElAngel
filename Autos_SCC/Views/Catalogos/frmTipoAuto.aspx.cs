using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Objetos;
using Autos_SCC.Presenter;
using NucleoBase.Core;
using Autos_SCC.DomainModel;
using Autos_SCC.Interfaces;
using System.Data;
using System.Drawing;
using Autos_SCC.Views.ControlesUsuario;

namespace Autos_SCC.Views.Catalogos
{
    public partial class frmTipoAuto : System.Web.UI.Page, IViewTipoAuto
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new CatTipoAuto_Presenter(this, new DBTipoAuto());

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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ttlRegTipo.InnerText = "Registro de Tipo de Auto";
            btnGuardar.Text = "GUARDAR";
            btnLimpiar.Visible = true;
            UpaAgregarTipoAuto.Update();
            if (eNewObj != null)
                eNewObj(sender, e);
            mpeAgregarTipoAuto.Show();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (eSaveObj != null)
                eSaveObj(sender, e);
            mpeAgregarTipoAuto.Hide();
            upaTab.Update();
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
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CatalogoTiposAuto.xls");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvCatalogo.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //
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

        protected void gvCatalogo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            bool ban = false;
            foreach (GridViewRow row in gvCatalogo.Rows)
            {
                if (row.RowIndex == gvCatalogo.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                    //i = Convert.ToInt32(gvCatalogo.Rows[row.RowIndex].Cells[0].Text);
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
                ttlRegTipo.InnerText = "Edición de Tipo de Auto";
                btnGuardar.Text = "EDITAR";
                btnLimpiar.Visible = false;
                UpaAgregarTipoAuto.Update();
                mpeAgregarTipoAuto.Show();
            }
        }
        protected void gvCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EliminarTipoAuto")
                {

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mpeAgregarTipoAuto.Hide();
        }

        void omb_CancelButtonPressed(object sender, EventArgs args)
        {
            omb.Hide();
        }

        void omb_OkButtonPressed(object sender, EventArgs e)
        {
            if (eObjSelected != null)
                eObjSelected(sender, e);
            if (eDeleteObj != null)
                eDeleteObj(sender, e);
        }

        void omb_Ok2ButtonPressed(object sender, EventArgs e)
        {
            omb2.Hide();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(sender, e);
            upaTab.Update();
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

            ddlMarcaBusqueda.DataSource = dtObjet;
            ddlMarcaBusqueda.DataValueField = "iId";
            ddlMarcaBusqueda.DataTextField = "sDescripcion";
            ddlMarcaBusqueda.DataBind();
        }

        private void LimpiaControles()
        {
            txtId.Text = string.Empty;
            ddlMarca.SelectedValue = "0";
            txtDescripcion.Text = string.Empty;
            txtBuqueda.Text = string.Empty;
            UpaAgregarTipoAuto.Update();
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            omb2.ShowMessage(sMensaje, sCaption);
            //string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        #endregion

        #region "Vars y Propiedades"
        CatTipoAuto_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public TipoAuto oCatalogo
        {
            get
            {
                return new TipoAuto
                {
                    iId = txtId.Text.S() == string.Empty ? -1 : txtId.Text.I(),
                    iMarca = ddlMarca.SelectedValue.S().I(),
                    sDescripcion = txtDescripcion.Text.S(),
                    iActivo = chkActivo.Checked ? 1 : 0,
                    dtFechaUltMov = DateTime.Now,
                    sUsuario = Session["usuario"].S()
                };
            }
            set
            {
                TipoAuto oCat = value as TipoAuto;
                if (oCat != null)
                {
                    txtId.Text = oCat.iId == -1 ? string.Empty : oCat.iId.S();
                    ddlMarca.SelectedValue = oCat.iMarca.S();
                    txtDescripcion.Text = oCat.sDescripcion.S();
                    chkActivo.Checked = oCat.iActivo == 1 ? true : false;
                }
            }
        }
        public TipoAuto oGetSetObjSelection
        {
            get
            {
                TipoAuto oCat = null;

                int iFila = gvCatalogo.SelectedIndex;
                if (iFila >= 0)
                {
                    oCat = new TipoAuto();
                    //oCat.iId = gvCatalogo.Rows[iFila].Cells[0].Text.S().I();
                    oCat.iId = gvCatalogo.DataKeys[iFila]["fi_Id"].S().I();
                    oCat.sDescripcion = gvCatalogo.Rows[iFila].Cells[2].Text.S();
                    oCat.iActivo = gvCatalogo.Rows[iFila].Cells[3].Text.S().I();
                    oCat.sUsuario = gvCatalogo.Rows[iFila].Cells[4].Text.S();
                }

                return oCat;
            }
            set
            {
                TipoAuto oCat = value;
                //gvCatalogo.FocusedRowHandle = gvCatalogo.LocateByValue("iId", oCat.iId, null);
            }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[]{
                    "@fc_Desc", "%" + txtBuqueda.Text.S() + "%",
                    "@fi_IdMarca", ddlMarcaBusqueda.SelectedValue.S().I(),
                    "@fi_IdActivo", rblActivo.SelectedValue.S().I()
                };
            }
        }
        #endregion

    }
}