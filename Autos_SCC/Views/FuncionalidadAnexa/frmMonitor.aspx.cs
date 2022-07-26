using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Autos_SCC.Interfaces;
using NucleoBase.Core;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;

namespace Autos_SCC.Views.FuncionalidadAnexa
{
    public partial class frmMonitor : System.Web.UI.Page, IViewMonitor
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Monitor_Presenter(this, new DBMonitor());

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

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
            gvClientes.Visible = false;

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
            gvClientes.Visible = true;
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
        }

        protected void imbBuscaCliente_Click(object sender, EventArgs e)
        {
            ddlCotizacion_SelectedIndexChanged(sender, e);
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

        #endregion

        #region VARIABLES Y PROPIEDADES

        Monitor_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetCotizaciones;
        public event EventHandler eGetCliente;

        public int iIdSucursal
        {
            get
            {
                return ddlSucursal.SelectedValue.S().I();
            }
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

        #endregion

    }
} 