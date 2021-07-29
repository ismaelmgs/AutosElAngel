using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;

namespace Autos_SCC
{
    public partial class frmLogin : System.Web.UI.Page, IViewSeguridad
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Seguridad_Presenter(this, new DBSeguridad());

            if (!IsPostBack)
            {
                
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.S() == string.Empty)
            {
                alert("El Usuario es requerido");
            }
            else if (txtPassword.Text.S() == string.Empty)
            {
                alert("La contraseña es requerida");
            }
            else
            {
                if (eGetUsuario !=  null)
                    eGetUsuario(sender, e);

                if (!oUser.bEncontrado)
                {
                    alert(oUser.sEstatus);
                }
                else if (oUser.sEstatus != string.Empty)
                    alert(oUser.sEstatus);
                else
                {
                    Session["usuario"] = oUser.sUser;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirecciona", "window.location.href = '../Views/Default.aspx'", true);
                }
            }
        }
        #endregion

        #region METODOS
        private void alert(string m)
        {
            m = "alert('" + m + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", m, true);
        }

        public string ConvertRelativeUrlToAbsoluteUrl(string relativeUrl)
        {
            if (Request.IsSecureConnection)
                return string.Format("https://{0}{1}{2}", Request.Url.Host,
                    Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString(),
                    Page.ResolveUrl(relativeUrl));
            else
                return string.Format("http://{0}{1}{2}", Request.Url.Host,
                    Request.Url.Port == 80 ? "" : ":" + Request.Url.Port.ToString(),
                    Page.ResolveUrl(relativeUrl));
        }
        #endregion

        #region "Vars y Propiedades"

        Seguridad_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetUsuario;

        public DataUserIndetity oUser
        {
            get { return (DataUserIndetity)Session["SUser"]; }
            set { Session["SUser"] = value; }
        }

        public object[] oArrFiltros
        {
            get
            {
                return new object[]{
                    "@fc_Usuario", txtUsuario.Text.S(),
                    "@fc_Password", txtPassword.Text.S()
                };
            }
        }

        #endregion
    }
}