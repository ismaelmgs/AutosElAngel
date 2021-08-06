/*****************************************************************
 * Nombre:  Ismael Morato Gallegos (Grupo Devtic) 
 * Fecha de creacion: 19 de diciembre de 2013
 *****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using Autos_SCC.Presenter;
using NucleoBase.Core;
using System.Data;
using System.Drawing;
using Autos_SCC.Views.ControlesUsuario;

namespace Autos_SCC.Views.Catalogos
{
    public partial class frmVersiones : System.Web.UI.Page, IViewVersion
    {
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new CatVersion_Presenter(this, new DBVersion());

            omb.OkButtonPressed += new ucModalConfirm.OkButtonPressedHandler(omb_OkButtonPressed);
            omb.CancelButtonPressed += new ucModalConfirm.CancelButtonPressedHandler(omb_CancelButtonPressed);

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
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CatalogoVersiones.xls");
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
                if(e.Row.RowType == DataControlRowType.DataRow)
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

        void omb_CancelButtonPressed(object sender, EventArgs args)
        {
            omb.Hide();
        }

        void omb_OkButtonPressed(object sender, EventArgs e)
        {
            if (eDeleteObj != null)
                eDeleteObj(sender, e);
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetTiposAuto != null)
                eGetTiposAuto(sender, e);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(sender, e);
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

        private void LimpiaControles()
        {
            txtId.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtBuqueda.Text = string.Empty;
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
        }

        public void LoadTiposAuto(DataTable dtObj)
        {
            if (dtObj.Rows.Count > 0)
            {
                ddlTipoAuto.DataSource = dtObj;
                ddlTipoAuto.DataValueField = "fi_Id";
                ddlTipoAuto.DataTextField = "fc_Descripcion";
                ddlTipoAuto.DataBind();
            }
            else
                ddlTipoAuto.DataSource = null;
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        #endregion

        #region "Vars y Propiedades"
        CatVersion_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetTiposAuto;

        public Versiones oCatalogo
        {
            get
            {
                return new Versiones
                {
                    iId = txtId.Text.S() == string.Empty ? -1 : txtId.Text.I(),
                    iIdMarca = ddlMarca.SelectedValue.S().I(),
                    iIdTipoAuto = ddlTipoAuto.SelectedValue.S().I(),
                    sDescripcion = txtDescripcion.Text.S(),
                    iActivo = chkActivo.Checked ? 1 : 0,
                    dtFechaUltMov = DateTime.Now,
                    sUsuario = Session["usuario"].S()
                };
            }
            set
            {
                Versiones oCat = value as Versiones;
                if (oCat != null)
                {
                    txtId.Text = oCat.iId == -1 ? string.Empty : oCat.iId.S();
                    ddlMarca.SelectedValue = oCat.iIdMarca.S();
                    if (eGetTiposAuto != null)
                        eGetTiposAuto(null, EventArgs.Empty);
                    ddlTipoAuto.SelectedValue = oCat.iIdTipoAuto.S();
                    txtDescripcion.Text = oCat.sDescripcion.S();
                    chkActivo.Checked = oCat.iActivo == 1 ? true : false;
                }
            }
        }
        public Versiones oGetSetObjSelection
        {
            get
            {
                Versiones oCat = null;
                
                int iFila = gvCatalogo.SelectedIndex;
                if (iFila >= 0)
                {
                    oCat = new Versiones();
                    oCat.iId = gvCatalogo.Rows[iFila].Cells[0].Text.S().I();
                    oCat.sDescripcion = gvCatalogo.Rows[iFila].Cells[1].Text.S();
                    oCat.iActivo = gvCatalogo.Rows[iFila].Cells[2].Text.S().I();
                    oCat.sUsuario = gvCatalogo.Rows[iFila].Cells[3].Text.S();
                }

                return oCat;
            }
            set
            {
                Versiones oCat = value;
                //gvCatalogo.FocusedRowHandle = gvCatalogo.LocateByValue("iId", oCat.iId, null);
            }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[]{
                    "@fc_Desc", "%" + txtBuqueda.Text.S() + "%",
                    "@fi_IdActivo", rblActivo.SelectedValue.S().I()
                };
            }
        }
        public string sCaptionBtnGuardar
        {
            set
            {
                string valor = value;
            }
        }
        public string sNumRegistros
        {
            get
            {
                return "Registro: 1";
            }
            set
            {
                string valor = value.S();
            }
        }
        #endregion
    }
}