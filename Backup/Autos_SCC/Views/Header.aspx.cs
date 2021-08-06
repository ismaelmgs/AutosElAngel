using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autos_SCC.Views
{
    public partial class Header : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNomUsuario.Text = "Ismael Morato";
            lblPerfil.Text = "PERFIL: Administrador";
        }

        protected void lknEndSession_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            //Response.Redirect("login.aspx");
        }
    }
}