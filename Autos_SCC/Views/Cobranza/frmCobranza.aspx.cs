using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NucleoBase.Core;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using Autos_SCC.Interfaces;
using Autos_SCC.Clases;
using RestSharp;
using RestSharp.Authenticators;
using System.Text;
using Newtonsoft.Json;

namespace Autos_SCC.Views.Cobranza
{
    public partial class frmCobranza : System.Web.UI.Page, IViewCobranza
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Cobranza_Presenter(this, new DBCobranza());

            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                oPresenter.LoadObjects_Presenter();
            }
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(eGetCreditos != null)
                eGetCreditos(sender, e);

            gvCreditos.DataSource = dtCreditos;
            gvCreditos.DataBind();
        }

        protected void gvCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView gv = (GridView)e.Row.FindControl("gvDetalle");
                    int iIdCotizacion = gvCreditos.DataKeys[e.Row.RowIndex].Value.S().I();

                    gv.DataSource = new DBCobranza().dtGetPagosPorCredito(iIdCotizacion);
                    gv.DataBind();

                    Image img = (Image)e.Row.FindControl("imEstatus");
                    if (img != null)
                    {
                        string[] sFecha = e.Row.Cells[6].Text.S().Split('/');
                        string[] sFechaServ = Utils.ObtieneFechaServidor().Split('/');

                        DateTime dtProxPago = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                        DateTime dtFechaHoy = new DateTime(sFechaServ[2].S().I(), sFechaServ[1].S().I(), sFechaServ[0].S().I());

                        if (dtFechaHoy > dtProxPago)
                        {
                            img.ImageUrl = "~/Images/Iconos/Rojo.png";
                        }
                        else if (dtProxPago > dtFechaHoy)
                        {
                            TimeSpan ts = dtProxPago - dtFechaHoy;
                            if (ts.Days > 50)
                            {
                                img.ImageUrl = "~/Images/Iconos/Azul.png";
                            }
                            else if (ts.Days <= 3)
                            {
                                img.ImageUrl = "~/Images/Iconos/Amarillo.png";
                            }
                            else
                                img.ImageUrl = "~/Images/Iconos/Verde.png";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al llenar la tabla de pagos " + ex.Message, "Consulta de créditos");
            }
        }


        private decimal suma1 = 0;
        private decimal suma2 = 0;
        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    suma1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "fm_MontoCompromiso"));
                    suma2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "fm_MontoPagado"));

                    Image img = (Image)e.Row.FindControl("imEstatus");
                    if (img != null)
                    {
                        if (e.Row.Cells[2].Text.S().Replace("$", "").Replace(",", "").S().Db() > 0)
                        {
                            if (e.Row.Cells[1].Text.S() == e.Row.Cells[2].Text.S())
                            {
                                img.ImageUrl = "~/Images/Iconos/acept.ico";
                                img.ToolTip = "Pago realizado con exito";
                            }
                            else
                            {
                                img.ImageUrl = "~/Images/Iconos/Warning.ico";
                                img.ToolTip = "Pago pendiente";
                            }
                        }
                        else
                        {
                            img.ImageUrl = "~/Images/Iconos/Warning.ico";
                            img.ToolTip = "Pago pendiente";
                        }
                    }
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = suma1.ToString("c");
                    e.Row.Cells[2].Text = suma2.ToString("c");
                    e.Row.Font.Bold = true;

                    suma1 = 0;
                    suma2 = 0;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al llenar la tabla de pagos " + ex.Message, "Consulta de créditos");
            }
        }

        protected void imbMensaje_Click(object sender, ImageClickEventArgs e)
        {
            mpeMensaje.Show();
        }

        protected void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            List<recipient> olst = new List<recipient>();
            recipient oRec1 = new recipient();
            oRec1.msisdn = "5540532207";

            recipient oRec2 = new recipient();
            oRec2.msisdn = "2221844016";

            olst.Add(oRec1);
            olst.Add(oRec2);

            Mensaje oMen = new Mensaje();
            oMen.message = txtMensaje.Text;
            oMen.tpoa = "Autos el Angel";
            oMen.recipient = olst;

            var client = new RestClient("https://api.labsmobile.com/json/send");
            //client.Authenticator = new HttpBasicAuthenticator("ismael.morato@morvelit.com", "ZXdCn3938H9w");
            client.Authenticator = new HttpBasicAuthenticator("ivan.morato@morvelit.com", "KkmKX7sS9w2c");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            string sJSON = JsonConvert.SerializeObject(oMen);
            request.AddParameter("undefined", sJSON, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            mpeMensaje.Hide();
        }

        #region METODOS

        public void LoadSucursales(DataTable dtSuc)
        {
            if (dtSuc.Rows.Count > 0)
            {
                ddlSucursal.DataSource = dtSuc;
                ddlSucursal.DataValueField = "fi_Id";
                ddlSucursal.DataTextField = "fc_Descripcion";
                ddlSucursal.DataBind();
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        #endregion


        #region "Vars y Propiedades"

        Cobranza_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetCreditos;

        public int iIdSucursal
        {
            get
            {
                return ddlSucursal.SelectedValue.S().I();
            }
        }

        public DataTable dtCreditos
        {
            set;
            get;
        }

        public DataTable dtPagos
        {
            set;
            get;
        }

        public string sUsuarioForm
        {
            get
            {
                return Session["usuario"].S();
            }
        }


        #endregion
        
    }

    public class Mensaje
    {
        public string message { set; get; }
        public string tpoa { set; get; }
        public List<recipient> recipient { set; get; }

    }

    public class recipient
    {
        public string msisdn { set; get; }
    }
}