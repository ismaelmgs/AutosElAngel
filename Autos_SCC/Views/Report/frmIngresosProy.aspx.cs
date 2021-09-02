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
    public partial class frmIngresosProy : System.Web.UI.Page, IViewIngresosProy
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new IngresosProy_Presenter(this, new DBReporte());

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                //oPresenter.LoadObjects_Presenter();
            }

            if (Request[txtFechaInicial.UniqueID] != null)
            {
                if (Request[txtFechaInicial.UniqueID].Length > 0)
                {
                    txtFechaInicial.Text = Request[txtFechaInicial.UniqueID];
                }
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

        public void CargaDatos()
        {
            iReporte = rbReporte.SelectedValue.S().I();
            sFecha = txtFechaInicial.Text;
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        public void LoadGrid(DataTable dt)
        {
            try
            {
                gvReporte.DataSource = dt;
                gvReporte.DataBind();

                pnlReporte.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=Reporte_AutosVendidos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                this.EnableViewState = false;

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                upaReporte.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio el siguiente ERROR: " + ex.Message, "Exporta a excel");
            }
        }



        #region "Vars y Propiedades"
        IngresosProy_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchReporte;

        public int iReporte
        {
            get { return (int)ViewState["iReporteV"]; }
            set { ViewState["iReporteV"] = value; }
        }

        public string sFecha
        {
            get { return (string)ViewState["sFechaV"]; }
            set { ViewState["sFechaV"] = value; }
        }
        #endregion
    }
}