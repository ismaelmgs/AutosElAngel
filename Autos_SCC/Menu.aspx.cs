using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using NucleoBase.Core;

namespace Autos_SCC.Views
{
    public partial class Menu : System.Web.UI.Page, IViewMenu
    {
        public Menu()
        {
            
        }                

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["usuario"] = "1";
            if (!Page.IsPostBack)
            {
                Response.Expires = 0;

                if (Session["usuario"] == null)
                {
                    Response.Redirect("login.aspx");
                }

                oPresenter = new Menu_Presenter(this, new DBMenu());
            }
        }
        
        public void LoadObjects(DataTable nodos)
        {
            DataSet ds = new DataSet();
            
            ds.Tables.Add(nodos);

            ds.Relations.Add("NodeRelation", ds.Tables[0].Columns["fi_Id"], ds.Tables[0].Columns["fi_PadreId"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("fi_PadreId"))
                {
                    TreeNode node = new TreeNode(row["fc_Descripcion"].S(), row["fi_Id"].S(), row["fc_URL"].S());                    
                    node.ImageUrl = row["fi_UrlIco"].S();
                    
                    trMenu.Nodes.Add(node);
                    AgregaHijos(row, node);
                }
            }

        }

        private void AgregaHijos(DataRow dbRow, TreeNode node)
        {
            foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
            {
                TreeNode childNode = new TreeNode(childRow["fc_Descripcion"].S(), childRow["fi_Id"].S(), childRow["fc_URL"].S());

                childNode.Target = childRow["fc_URL"].S();
                childNode.ImageUrl = childRow["fi_UrlIco"].S();
                node.ChildNodes.Add(childNode);
                AgregaHijos(childRow, childNode);
            }
        }

        protected void trMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeView tv = (TreeView)sender;
            string sPath = tv.SelectedNode.Target;

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirecciona", "window.parent.basefrm.location.href = '"+ sPath +"'", true);
        }

        #region VARIABLES Y PROPIEDADES
        Menu_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public string sUsuario
        {
            get
            {
                return "1";// Session["usuario"].ToString().Trim();
            }
        }
        #endregion

    }
}