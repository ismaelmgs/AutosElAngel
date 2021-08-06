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
    public partial class frmAutosVen : System.Web.UI.Page, IViewAutosVend
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new AutosVend_Presenter(this, new DBReporte());

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                oPresenter.LoadObjects_Presenter();
            }

            if (Request[txtFechaInicial .UniqueID] != null)
            {
                if (Request[txtFechaInicial.UniqueID].Length > 0)
                {
                    txtFechaInicial.Text = Request[txtFechaInicial.UniqueID];
                }
            }

            if (Request[txtFechaFinal.UniqueID] != null)
            {
                if (Request[txtFechaFinal.UniqueID].Length > 0)
                {
                    txtFechaFinal.Text = Request[txtFechaFinal.UniqueID];
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchAutos != null)
                    eSearchAutos(sender, e);

                pnlReporte.Visible = true;
                MuestraFechas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio el siguiente ERROR: " + ex.Message, "Búsqueda");
            }
        }

        protected void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTodas.Checked)
                {
                    txtFechaInicial.Text = string.Empty;
                    txtFechaFinal.Text = string.Empty;
                }

                imbFechaInicial.Enabled = !chkTodas.Checked;
                imbFechaFinal.Enabled = !chkTodas.Checked;
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio el siguiente ERROR: " + ex.Message, "Búsqueda");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
        #endregion

        #region METODOS
        private string NombreMes(int iMes)
        {
            try
            {
                string sMes = string.Empty;
                switch (iMes)
                {
                    case 1:
                        sMes = "Enero";
                        break;
                    case 2:
                        sMes = "Febrero";
                        break;
                    case 3:
                        sMes = "Marzo";
                        break;
                    case 4:
                        sMes = "Abril";
                        break;
                    case 5:
                        sMes = "Mayo";
                        break;
                    case 6:
                        sMes = "Junio";
                        break;
                    case 7:
                        sMes = "Julio";
                        break;
                    case 8:
                        sMes = "Agosto";
                        break;
                    case 9:
                        sMes = "Septiembre";
                        break;
                    case 10:
                        sMes = "Octubre";
                        break;
                    case 11:
                        sMes = "Noviembre";
                        break;
                    case 12:
                        sMes = "Diciembre";
                        break;
                }

                return sMes.ToUpper();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadGrid(DataTable dt)
        {
            try
            {
                gvReporte.DataSource = dt;
                gvReporte.DataBind();
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

        public void MuestraFechas()
        {
            try
            {
                lblDiaInicial.Text = string.Empty;
                lblMesInicial.Text = string.Empty;
                LblAnioInicial.Text = string.Empty;
                lblAl.Text = string.Empty;
                lblDiaFinal.Text = string.Empty;
                lblMesFinal.Text = string.Empty;
                lblAnioFinal.Text = string.Empty;


                if (txtFechaInicial.Text.S() != string.Empty && txtFechaFinal.Text.S() != string.Empty)
                {
                    lblDiaInicial.Text = "DEL " + oRep.dtFechaInicio.Value.Day + " DE " + NombreMes(oRep.dtFechaInicio.Value.Month) + " DE " + oRep.dtFechaInicio.Value.Year;
                    lblMesInicial.Text = " AL " + oRep.dtFechaFin.Value.Day + " DE " + NombreMes(oRep.dtFechaFin.Value.Month) + " DE " + oRep.dtFechaFin.Value.Year; ;
                }
                else if (txtFechaInicial.Text.S() != string.Empty && txtFechaFinal.Text.S() == string.Empty)
                {
                    lblDiaInicial.Text = "A PARTIR DEL " + oRep.dtFechaInicio.Value.Day + " DE " + NombreMes(oRep.dtFechaInicio.Value.Month) + " DE " + oRep.dtFechaInicio.Value.Year;
                }
                else if (txtFechaInicial.Text.S() == string.Empty && txtFechaFinal.Text.S() != string.Empty)
                {
                    lblDiaInicial.Text = "HASTA EL " + oRep.dtFechaFin.Value.Day + " DE " + NombreMes(oRep.dtFechaFin.Value.Month) + " DE " + oRep.dtFechaFin.Value.Year;
                }
                else if (txtFechaInicial.Text.S() == string.Empty && txtFechaFinal.Text.S() == string.Empty)
                {
                    lblDiaInicial.Text = "DESDE EL INICIO DE OPERACIONES";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }
        #endregion

        #region "Vars y Propiedades"
        AutosVend_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchAutos;

        public AutosVen oRep
        {
            get
            {
                AutosVen o = new AutosVen();

                if (txtFechaInicial.Text.S() == string.Empty)
                    o.dtFechaInicio = null;
                else
                {
                    string[] sFecha = txtFechaInicial.Text.S().Split('/');
                    o.dtFechaInicio = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                }

                if (txtFechaFinal.Text.S() == string.Empty)
                    o.dtFechaFin = null;
                else
                {
                    string[] sFecha = txtFechaFinal.Text.S().Split('/');
                    o.dtFechaFin = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                }
                o.iIdSucursal = ddlSucursal.SelectedValue.S().I();

                return o;
                    
            }
        }

        #endregion
    }
}