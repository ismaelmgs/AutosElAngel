using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using NucleoBase.Core;
using Autos_SCC.Objetos;

namespace Autos_SCC
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SUser"] != null)
                {
                    DataUserIndetity oUser = (DataUserIndetity)Session["SUser"];
                    lblUser2.Text = oUser.sNombre + " " + oUser.sApellidos;
                }
                else
                    Response.Redirect("..//Default.aspx");
            }
        }
        
        protected void Menu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }
        
        protected void lkSalir_Click(object sender, EventArgs e)
        {
            Session["SUser"] = null;
            Session["usuario"] = null;
            Response.Redirect("..//Default.aspx");
        }

        
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }
        
        #endregion
        
        #region METODOS
        
        #endregion

        #region PROPIEDADES

        #endregion
    }
}
