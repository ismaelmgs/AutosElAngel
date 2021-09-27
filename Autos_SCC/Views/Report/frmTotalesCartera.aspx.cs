using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Interfaces;
using Autos_SCC.Presenter;
using System.Data;
using Autos_SCC.Clases;
using NucleoBase.Core;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;
using System.IO;
using System.Text;

namespace Autos_SCC.Views.Report
{
    public partial class frmTotalesCartera : System.Web.UI.Page, IViewTotalesCartera
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new TotalesCartera_Presenter(this, new DBReporte());
            
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                oPresenter.LoadObjects_Presenter();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaDatos();

                if (eSearchReporte != null)
                    eSearchReporte(sender, e);


                //MuestraFechas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio el siguiente ERROR: " + ex.Message, "Búsqueda");
            }
        }

        protected void gvReporte_PreRender(object sender, EventArgs e)
        {
            if (gvReporte.Rows.Count > 0)
            {
                lblRepText.Visible = true;
                gvReporte.FooterRow.Cells[1].Text = "Total:";
                gvReporte.FooterRow.Cells[1].Font.Bold = true;
                gvReporte.FooterRow.Cells[1].Font.Size = 14;


                gvReporte.FooterRow.Cells[2].Text = sTotalGrid.D().ToString("c");//dtTotalCon.Rows[0][row["ClaveContrato"].S()].S().D().ToString("c");
                gvReporte.FooterRow.Cells[2].Font.Bold = true;
                gvReporte.FooterRow.Cells[2].Font.Size = 14;
            }
            else
                lblRepText.Visible = false;
        }

        public void CargaDatos()
        {
            
            if (ddlSucursal.SelectedValue == "0")
            {
                sSucursal = "0";
            }
            else
                sSucursal = ddlSucursal.SelectedValue;
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        public void LoadGrid(DataSet ds)
        {
            try
            {
                gvReporte.DataSource = ds.Tables[0];
                gvReporte.DataBind();

                pnlReporte.Visible = true;

                sTotalGrid = ds.Tables[1].Rows[0][0].S();
                lbltotalT.Text = ds.Tables[1].Rows[0][0].S().D().ToString("c");

                lblTotalvehi.Text = ds.Tables[2].Rows[0][0].S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadSucursales(DataTable dtSuc)
        {
            try
            {
                ddlSucursal.DataSource = dtSuc;
                ddlSucursal.DataValueField = "fi_Id";
                ddlSucursal.DataTextField = "fc_Descripcion";
                ddlSucursal.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Vars y Propiedades"
        TotalesCartera_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchReporte;

        

        public string sSucursal
        {
            get { return (string)ViewState["sSucursalV"]; }
            set { ViewState["sSucursalV"] = value; }
        }

        public string sTotalGrid
        {
            get { return (string)ViewState["sTotalGridV"]; }
            set { ViewState["sTotalGridV"] = value; }
        }
        #endregion
    }
}