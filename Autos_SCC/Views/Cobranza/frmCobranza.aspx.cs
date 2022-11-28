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
using Autos_SCC.Objetos;
using Autos_SCC.ViewModels;
using System.IO;

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
            if(dtCreditos != null && dtCreditos.Rows.Count > 0)
            {
                btnExportar.Visible = true;
            }
            else
            {
                btnExportar.Visible = false;
            }
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

        protected void btnCerrarMensaje_Click(object sender, EventArgs e)
        {
            mpeMensaje.Hide();
        }

            protected void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            List<recipient> olst = new List<recipient>();
            recipient oRec1 = new recipient();
            oRec1.msisdn = "5540532207";

            recipient oRec2 = new recipient();
            oRec2.msisdn = "2461763089";

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
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                List<CreditosSucursal> lstCreditos = ObtenerListaCreditos(sender, e);
                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel; charset=utf-8;";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteCobranza.xls");
                string strTable = GenerarExcel(lstCreditos);
                var utf8 = Encoding.UTF8;
                byte[] utfBytes = utf8.GetBytes(strTable);
                strTable = utf8.GetString(utfBytes, 0, utfBytes.Length);
                Response.Write(strTable);
                Response.End();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "Exporta a XLS");
            }
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
        public string GenerarExcel(List<CreditosSucursal> lstCreditos)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                int iRow = 1;
                sb.AppendLine("<table border=\"2\" style=\"border-width:0px; border-style:None; font-size:Small; width: 100 %; border-collapse:collapse;\">");
                sb.AppendLine("<thead>");
                sb.AppendLine("<tr>");
                foreach (CreditosSucursal Credito in lstCreditos)
                {
                    if (iRow == 1)
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            sb.AppendLine("<th scope=\"col\" bgcolor=\"#01609F\"style=\"color: #ffffff; background-color:#01609F; font-weight: bold\">" + NombreColumnas("Principal", i) + "</th>");
                        }
                        sb.AppendLine("</tr>");
                        sb.AppendLine("</thead>");
                        sb.AppendLine("<tbody>");
                        iRow++;
                    }
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td>" + Credito.NombreCompleto + "</td>");
                    sb.AppendLine("<td>" + Credito.TipoAuto + "</td>");
                    sb.AppendLine("<td>" + Credito.DescPlazo + "</td>");
                    sb.AppendLine("<td>" + Credito.fm_Precio.ToString("C2") + "</td>");
                    string[] sFecha = Credito.FechaProxPago.Split('/');
                    string[] sFechaServ = Utils.ObtieneFechaServidor().Split('/');

                    DateTime dtProxPago = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                    DateTime dtFechaHoy = new DateTime(sFechaServ[2].S().I(), sFechaServ[1].S().I(), sFechaServ[0].S().I());
                    if (dtFechaHoy > dtProxPago)
                    {
                        sb.AppendLine("<td bgcolor=\"red\"></td>");
                    }
                    else if (dtProxPago > dtFechaHoy)
                    {
                        TimeSpan ts = dtProxPago - dtFechaHoy;
                        if (ts.Days > 50)
                        {
                            sb.AppendLine("<td bgcolor=\"blue\"></td>");
                        }
                        else if (ts.Days <= 3)
                        {
                            sb.AppendLine("<td bgcolor=\"yellow\"></td>");
                        }
                        else
                            sb.AppendLine("<td bgcolor=\"green\"></td>");
                    }
                    sb.AppendLine("<td>" + Credito.FechaProxPago + "</td>");
                    sb.AppendLine("<td>" + Credito.MontoProxPago.ToString("C2") + "</td>");
                    sb.AppendLine("</tr>");
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td></td>");
                    sb.AppendLine("<td colspan=\"7\">");
                    iRow++;
                    sb.AppendLine("<table border=\"2\" style=\"border-width:0px; border-style:None; font-size:Small; width: 100 %; border-collapse:collapse;\">");
                    sb.AppendLine("<thead>");
                    sb.AppendLine("<tr>");
                    for (int i = 1; i < 8; i++)
                    {
                        sb.AppendLine("<th scope=\"col\" bgcolor=\"#01609F\"style=\"color: #ffffff; background-color:#01609F; font-weight: bold\">" + NombreColumnas("Detalle", i) + "</th>");
                        iRow++;
                    }
                    sb.AppendLine("</tr>");
                    sb.AppendLine("</thead>");
                    sb.AppendLine("<tbody>");
                    foreach (DetallesCreditos Detalle in Credito.lstDetalleCreditos)
                    {
                        iRow++;
                        sb.AppendLine("<tr>");
                        sb.AppendLine("<td>" + Detalle.fi_NoPago + "</td>");
                        sb.AppendLine("<td>" + Detalle.fm_MontoCompromiso.ToString("C2") + "</td>");
                        sb.AppendLine("<td>" + Detalle.fm_MontoPagado.ToString("C2") + "</td>");
                        sb.AppendLine("<td>" + Detalle.fd_FechaCompromiso + "</td>");
                        if (Detalle.fm_MontoPagado.S().Db() > 0)
                        {
                            if (Detalle.fm_MontoCompromiso.S() == Detalle.fm_MontoPagado.S())
                            {
                                sb.AppendLine("<td bgcolor=\"green\"></td>");
                            }
                            else
                            {
                                sb.AppendLine("<td bgcolor=\"yellow\"></td>");
                            }
                        }
                        else
                        {
                            sb.AppendLine("<td bgcolor=\"yellow\"></td>");
                        }
                        sb.AppendLine("<td>" + Detalle.fd_FechaPago + "</td>");
                        sb.AppendLine("<td>" + Detalle.fc_UsuarioRegistroPago.ToString() + "</td>");
                        sb.AppendLine("</tr>");
                    }
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td></td>");
                    sb.AppendLine("<td>" + Credito.lstDetalleCreditos.Select(s => s.fm_MontoCompromiso).Sum().ToString("C") + "</td>");
                    sb.AppendLine("<td>" + Credito.lstDetalleCreditos.Select(s => s.fm_MontoPagado).Sum().ToString("C") + "</td>");
                    sb.AppendLine("<td colspan=\"4\"></td>");
                    sb.AppendLine("</tr>");
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");
                    sb.AppendLine("</td>");
                    sb.AppendLine("</tr>");
                }
                sb.AppendLine("</tbody>");
                sb.AppendLine("</table>");
                return sb.ToString();


                sb.AppendLine("<tbody>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<tb>body Ejemplo</tb>");
                sb.AppendLine("<tb>body Ejemplo2</tb>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</tbody>");
                sb.AppendLine("</table>");
                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CreditosSucursal> ObtenerListaCreditos(object sender, EventArgs e)
        {
            try
            {
                if (eGetCreditos != null)
                    eGetCreditos(sender, e);
                List<CreditosSucursal> lstCreditos = new List<CreditosSucursal>();
                lstCreditos = (from DataRow dr in dtCreditos.Rows
                               select new CreditosSucursal()
                               {
                                   fi_Id = Convert.ToInt32(dr["fi_Id"]),
                                   fc_Nombre = dr["fc_Nombre"].ToString(),
                                   fc_Nombre2 = dr["fc_Nombre2"].ToString(),
                                   fc_ApePaterno = dr["fc_ApePaterno"].ToString(),
                                   fc_ApeMaterno = dr["fc_ApeMaterno"].ToString(),
                                   NombreCompleto = dr["NombreCompleto"].ToString(),
                                   fi_Plazo = Convert.ToInt32(dr["fi_Plazo"].ToString()),
                                   DescPlazo = dr["DescPlazo"].ToString(),
                                   fi_IdAuto = Convert.ToInt32(dr["fi_IdAuto"].ToString()),
                                   fi_Enganche = Convert.ToDecimal(dr["fi_Enganche"].ToString()),
                                   fd_Tasa = Convert.ToDecimal(dr["fd_Tasa"].ToString()),
                                   fc_Correo = dr["fc_Correo"].ToString(),
                                   fi_Sucursal = Convert.ToInt32(dr["fi_Sucursal"].ToString()),
                                   fi_Estatus = Convert.ToInt32(dr["fi_Estatus"].ToString()),
                                   FechaProxPago = dr["FechaProxPago"].ToString(),
                                   MontoProxPago = Convert.ToDecimal(dr["MontoProxPago"].ToString()),
                                   TipoAuto = dr["TipoAuto"].ToString(),
                                   fm_Precio = Convert.ToDecimal(dr["fm_Precio"].ToString()),
                                   fc_Usuario = dr["fc_Usuario"].ToString(),
                                   fd_FechaUltMovimiento = Convert.ToDateTime(dr["fd_FechaUltMovimiento"].ToString())
                               }).ToList();
                foreach (CreditosSucursal Credito in lstCreditos)
                {
                    DataTable dtDetalle = new DBCobranza().dtGetPagosPorCredito(Credito.fi_Id);
                    List<DetallesCreditos> lstDetalle = new List<DetallesCreditos>();
                    lstDetalle = (from DataRow dr in dtDetalle.Rows
                                  select new DetallesCreditos()
                                  {
                                      fi_IdAMortizacion = Convert.ToInt32(dr["fi_IdAMortizacion"]),
                                      fi_IdCotizacion = Convert.ToInt32(dr["fi_IdCotizacion"]),
                                      fi_NoPago = Convert.ToInt32(dr["fi_NoPago"]),
                                      fm_MontoCompromiso = Convert.ToDecimal(dr["fm_MontoCompromiso"].ToString()),
                                      fm_MontoPagado = Convert.ToDecimal(dr["fm_MontoPagado"].ToString()),
                                      fd_FechaCompromiso = dr["fd_FechaCompromiso"].ToString(),
                                      fd_FechaCompromiso2 = dr["fd_FechaCompromiso2"].ToString(),
                                      fd_FechaPago = dr["fd_FechaPago"].ToString(),
                                      fi_Estatus = Convert.ToInt32(dr["fi_Estatus"].ToString()),
                                      fc_UsuarioRegistroPago = dr["fc_UsuarioRegistroPago"].ToString(),
                                      fc_UsuarioGenero = dr["fc_UsuarioGenero"].ToString(),
                                      fd_FechaRegistroPago = dr["fd_FechaRegistroPago"].ToString(),
                                      fd_FechaUltMovimiento = dr["fd_FechaUltMovimiento"].ToString()
                                  }).ToList();
                    Credito.lstDetalleCreditos = lstDetalle;
                }
                return lstCreditos;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string NombreColumnas(string TipoTabla, int iIndex)
        {
            string sNombre = string.Empty;
            switch (TipoTabla)
            {
                case "Principal":
                    switch (iIndex)
                    {
                        case 1: sNombre = "Nombre Completo"; break;
                        case 2: sNombre = "tipo de Auto"; break;
                        case 3: sNombre = "Plazo"; break;
                        case 4: sNombre = "Precio Auto"; break;
                        case 5: sNombre = "Estatus"; break;
                        case 6: sNombre = "Fecha Prox. Pago"; break;
                        case 7: sNombre = "Monto Pago"; break;
                    }
                    break;
                case "Detalle":
                    switch (iIndex)
                    {
                        case 1: sNombre = "No. Pago"; break;
                        case 2: sNombre = "Monto Compromiso"; break;
                        case 3: sNombre = "Monto Pagado"; break;
                        case 4: sNombre = "Fecha Compromiso"; break;
                        case 5: sNombre = "Estatus"; break;
                        case 6: sNombre = "Fecha de Pago"; break;
                        case 7: sNombre = "Ultimo Reg. Pago"; break;
                    }
                    break;
            }
            return sNombre;
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
        public int iIdUsuario
        {
            get
            {
                int iIdUsu = 0;
                if (Session["oUserData"] != null)
                {
                    DataUserIndetity oUser = (DataUserIndetity)Session["oUserData"];
                    iIdUsu = oUser.IIdUsuario;
                }
                return iIdUsu;
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