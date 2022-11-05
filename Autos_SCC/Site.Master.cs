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
                    List<DataUserMenu> oUserMenu = (List<DataUserMenu>)Session["MenuUser"];
                    lblUser2.Text = oUser.sNombre + " " + oUser.sApellidos;
                    CargarMenu(oUserMenu);
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
            Response.Redirect("~/frmLogin.aspx");
        }

        
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }
        
        #endregion
        
        #region METODOS
        public void CargarMenu(List<DataUserMenu> LstMenu)
        {
            LtrMenu.Text = "<ul class=\"navbar-nav\" style=\"margin:0 auto;\">";
            foreach (DataUserMenu opMenu in LstMenu)
            {
                if(opMenu.iIdModulo == 0)
                {
                    LtrMenu.Text += "   <li class=\"nav-item active\">";
                    LtrMenu.Text += "       <a class=\"nav-link\" href='"+ ResolveUrl(opMenu.sUrl) + "'> " + opMenu.sDescripcion + " <span class=\"sr-only\">(current)</span></a>";
                    LtrMenu.Text += "   </li>";
                }
                else if(opMenu.iIdModulo == 20)
                {
                    LtrMenu.Text += "   <li class=\"nav-item\">";
                    LtrMenu.Text += "       <a class=\"nav-link\" href='" + ResolveUrl(opMenu.sUrl) + "'>" + opMenu.sDescripcion + "</a>";
                    LtrMenu.Text += "   </li>";
                }
                else if(opMenu.iIdPadre == -1)
                {
                    List<DataUserMenu> LstMenuHijos = LstMenu.Where(l => l.iIdPadre == opMenu.iIdModulo).ToList();
                    LtrMenu.Text += "   <li class=\"nav-item dropdown\">";
                    LtrMenu.Text += "       <a class=\"nav-link dropdown-toggle\" href=\"\" id=\"dropdown01\" data-toggle=\"dropdown\" role=\"button\" aria-haspopup=\"true\" aria-expanded=\"false\">" + opMenu.sDescripcion + "</a>";
                    LtrMenu.Text += "       <div class=\"dropdown-menu\" aria-labelledby=\"dropdown01\">";
                    foreach (DataUserMenu opMenuHijos in LstMenuHijos)
                    {
                        LtrMenu.Text += "       <a class=\"dropdown-item\" href='" + ResolveUrl(opMenuHijos.sUrl) + "'> " + opMenuHijos.sDescripcion + " </a>";
                    }
                    LtrMenu.Text += "       </div>";
                    LtrMenu.Text += "   </li>";
                }
            }
            LtrMenu.Text += "</ul>";
        }

        #endregion

        #region PROPIEDADES
        
        #endregion
    }
}
