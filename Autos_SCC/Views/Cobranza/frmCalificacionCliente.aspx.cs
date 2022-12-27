using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;
using NucleoBase.Core;
using Autos_SCC.Views.ControlesUsuario;

namespace Autos_SCC.Views.Cobranza
{
    public partial class frmCalificacionCliente : System.Web.UI.Page, IViewCalificacion
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Calificacion_Presenter(this, new DBCalificacion());
            omb.OkButtonPressed += new ucModalConfirm.OkButtonPressedHandler(omb_OkButtonPressed);
            omb.CancelButtonPressed += new ucModalConfirm.CancelButtonPressedHandler(omb_CancelButtonPressed);
            omb2.OkButtonPressed += new ucModalAlert.OkButtonPressedHandler(omb_Ok2ButtonPressed);
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//frmLogin.aspx");
                }

                oPresenter.LoadObjectsddl_Presenter();
                rdbNoCalificado.Checked = true;
            }
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            oPresenter.LoadObjectsCredit_Presenter();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if(rdbNoCalificado.Checked)
            {
                sIdsCalificacion = "0";
            }
            else
            {
                sIdsCalificacion = "1,2,3";
            }
            oPresenter.LoadObjects_PresenterFilter();
        }
        protected void ibtnBueno_Click(object sender, ImageClickEventArgs e)
        {
            iIdCalificacion = 1;
            if (eSetInsertaCalificacion != null)
                eSetInsertaCalificacion(sender, e);
        }

        protected void ibtnRegular_Click(object sender, ImageClickEventArgs e)
        {
            iIdCalificacion = 2;
            if (eSetInsertaCalificacion != null)
                eSetInsertaCalificacion(sender, e);
        }

        protected void ibtnMalo_Click(object sender, ImageClickEventArgs e)
        {
            iIdCalificacion = 3;
            if (eSetInsertaCalificacion != null)
                eSetInsertaCalificacion(sender, e);
        }
        protected void gvClientesCal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                iIdCotizacion = gvClientesCal.DataKeys[e.CommandArgument.S().I()]["fi_Id"].S().I();
                switch (e.CommandName.S())
                {
                    case "Calificar":
                        mpeCalificacion.Show();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        void omb_CancelButtonPressed(object sender, EventArgs args)
        {
            omb.Hide();
        }
        void omb_OkButtonPressed(object sender, EventArgs e)
        {
            Response.Redirect("frmCalificacionCliente.aspx");
        }
        void omb_Ok2ButtonPressed(object sender, EventArgs e)
        {
            omb2.Hide();
            mpeCalificacion.Hide();
            oPresenter.LoadObjectsCredit_Presenter();
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
        public void LoadCotizaciones(DataTable dtCotizaciones)
        {
            gvClientesCal.DataSource = dtCotizaciones;
            gvClientesCal.DataBind();
        }
        protected bool IsEditEnabled(string statusValue)
        {
            if(statusValue != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            omb2.ShowMessage(sMensaje, sCaption);
        }
        #endregion
        #region VARIABLES Y PROPIEDADES

        Calificacion_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSetInsertaCalificacion;

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
        public string sUsuario
        {
            get
            {
                return Session["usuario"].S();
            }
        }
        public int iIdCotizacion
        {
            get { return (int)ViewState["VSiIdCotizacion"]; }
            set { ViewState["VSiIdCotizacion"] = value; }
        }
        public int iIdCalificacion
        {
            get { return (int)ViewState["VSiIdCalificacion"]; }
            set { ViewState["VSiIdCalificacion"] = value; }
        }
        public int iIdSucursal
        {
            get
            {
                return ddlSucursal.SelectedValue.S().I();
            }
        }
        public string sIdsCalificacion
        {
            set;
            get;
        }
        #endregion
    }
}