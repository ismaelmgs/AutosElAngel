using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autos_SCC.Views.ControlesUsuario
{
    public partial class ucModalAlert : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnOk2.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk2.UniqueID, "");
        }
        public void ShowMessage(string Message, string Caption)
        {
            lblMessage.Text = Message;
            lblCaption.Text = Caption;
            tdCaption.Visible = true;
            mpext2.Show();
        }

        public void Hide()
        {
            lblMessage.Text = "";
            lblCaption.Text = "";
            mpext2.Hide();
        }

        public void btnOk2_Click(object sender, EventArgs e)
        {
            OnOkButtonPressed(e);
        }
        public delegate void OkButtonPressedHandler(object sender, EventArgs args);

        public event OkButtonPressedHandler OkButtonPressed;

        protected virtual void OnOkButtonPressed(EventArgs e)
        {
            if (OkButtonPressed != null)
                OkButtonPressed(btnOk2, e);
        }
    }
}