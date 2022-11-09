using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using Autos_SCC.Objetos;
using Autos_SCC.Clases;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using Autos_SCC.Interfaces;
using Autos_SCC.Views.ControlesUsuario;
using System.Drawing;

namespace Autos_SCC.Views.Catalogos
{
    public partial class frmAutorizadores : System.Web.UI.Page, IViewAutorizador
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Autorizador_Presenter(this, new DBAutorizador());

            omb2.OkButtonPressed += new ucModalAlert.OkButtonPressedHandler(omb_Ok2ButtonPressed);

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                if (eGetUsuarios != null)
                    eGetUsuarios(sender, e);
                if (eGetSucursales != null)
                    eGetSucursales(sender, e);
            }
        }
        void omb_Ok2ButtonPressed(object sender, EventArgs e)
        {
            omb2.Hide();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eSearchAdministradores != null)
                eSearchAdministradores(sender, e);
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

                if (ban)
                {
                    if (eObjSelected != null)
                        eObjSelected(sender, e);
                    if (eGetPerfiles != null)
                        eGetPerfiles(sender, e);
                    btnGuardar.Text = "EDITAR";
                    UpaEdicionPerfil.Update();
                    mpeEdicionPerfil.Show();
                }
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
        protected void gvCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void ddlPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            mpeEdicionPerfil.Hide();
        }
        #endregion
        #region METODOS
        public void LoadUsuarios(DataTable dtUsuarios)
        {
            gvCatalogo.DataSource = null;

            if (dtUsuarios.Rows.Count > 0)
            {
                gvCatalogo.DataSource = dtUsuarios;
                gvCatalogo.DataBind();
            }
            else
            {
                gvCatalogo.DataBind();
            }
        }
        public void LoadSucursales(DataTable dtSucursal)
        {
            ddlSucursal.DataSource = dtSucursal;
            ddlSucursal.DataValueField = "fi_Id";
            ddlSucursal.DataTextField = "fc_Descripcion";
            ddlSucursal.DataBind();
        }
        public void LoadPerfiles(DataTable dtPerfiles)
        {
            ddlPerfil.DataSource = dtPerfiles;
            ddlPerfil.DataValueField = "fi_id";
            ddlPerfil.DataTextField = "fc_Descripcion";
            ddlPerfil.DataBind();
        }
       
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            omb2.ShowMessage(sMensaje, sCaption);
        }
        #endregion

        #region "Vars y Propiedades"
        public Autorizador oGetSetObjSelection
        {
            get
            {
                Autorizador oCat = null;

                int iFila = gvCatalogo.SelectedIndex;
                if (iFila >= 0)
                {
                    oCat = new Autorizador();
                    oCat.fi_id = gvCatalogo.DataKeys[iFila]["fi_Id"].S().I();
                    oCat.fi_IdSucursal = gvCatalogo.DataKeys[iFila]["fi_IdSucursal"].S().I();
                }
                return oCat;
            }
            set
            {
                Autorizador oCat = value;
            }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[]{
                    "@fc_Desc", "%" + txtBusqueda.Text.S() + "%",
                    "@fi_Sucursal", ddlSucursal.SelectedValue.S(),
                };
            }
        }
        Autorizador_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetUsuarios;
        public event EventHandler eGetSucursales;
        public event EventHandler eSearchAdministradores;
        public event EventHandler eGetPerfiles;

        public Autorizador oAutorizador
        {
            get
            {
                return new Autorizador
                {
                    fi_IdPerfil = ddlPerfil.SelectedValue == null ? 0 : ddlPerfil.SelectedValue.S().I(),
                };
            }
            set
            {
                Autorizador oCat = value as Autorizador;
                if (oCat != null)
                {
                    ddlPerfil.SelectedValue = oCat.fi_IdPerfil.S() != "0" ? oCat.fi_IdPerfil.S() : ddlPerfil.SelectedValue;
                    txtSucursal.Text = oCat.NomSucurlal.S();
                    txtUser.Text = oCat.PriNombre + " " + oCat.SegNombre + " " + oCat.PatApellido + " " + oCat.MatApellido;
                }
            }
        }
        #endregion
    }
}