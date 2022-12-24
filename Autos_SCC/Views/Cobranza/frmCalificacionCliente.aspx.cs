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

namespace Autos_SCC.Views.Cobranza
{
    public partial class frmCalificacionCliente : System.Web.UI.Page, IViewCalificacion
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Calificacion_Presenter(this, new DBCalificacion());
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//frmLogin.aspx");
                }

                oPresenter.LoadObjects_Presenter();
                rdbNoCalificado.Checked = true;
            }
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            if(statusValue != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region VARIABLES Y PROPIEDADES

        Calificacion_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

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