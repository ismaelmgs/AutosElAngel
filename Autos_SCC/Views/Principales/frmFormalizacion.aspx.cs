using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using Autos_SCC.Interfaces;
using NucleoBase.Core;
using Autos_SCC.DomainModel;
using Autos_SCC.Clases;
using Autos_SCC.Presenter;

namespace Autos_SCC.Views.Principales
{
    public partial class frmFormalizacion : System.Web.UI.Page, IViewFormalizar
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Formalizar_Presenter(this, new DBFormalizar());

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
            if (eGetCotizaciones != null)
                eGetCotizaciones(sender, e);

            ddlCotizacion.DataSource = dtCotizacion;
            ddlCotizacion.DataTextField = "NombreCompleto";
            ddlCotizacion.DataValueField = "fi_Id";
            ddlCotizacion.DataBind();

            ddlCotizacion_SelectedIndexChanged(null, EventArgs.Empty);
        }

        protected void ddlCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetCliente != null)
                eGetCliente(sender, e);

            if (dtCliente.Rows.Count > 0)
            {
                HidIdCliente.Value = dtCliente.Rows[0]["fi_IdCliente"].S();
                lblCliente.Text = "Cliente: " + dtCliente.Rows[0]["NombreCompleto"].S();
                lblTipoAuto.Text = "Tipo auto: " + dtCliente.Rows[0]["fc_TipoAuto"].S();
                lblPrecio.Text = "Precio: $ " + Math.Round(dtCliente.Rows[0]["fm_Precio"].S().D(), 0).S();
                lblPlazo.Text = "Plazo: " + dtCliente.Rows[0]["fc_Plazo"].S();

                fsImprimir.Visible = true;
                iIdClienteC = HidIdCliente.Value.S().I();
            }
            else
            {
                HidIdCliente.Value = string.Empty;
                lblCliente.Text = string.Empty;
                lblTipoAuto.Text = string.Empty;
                lblPrecio.Text = string.Empty;
                lblPlazo.Text = string.Empty;

                fsImprimir.Visible = false;
            }
        }

        protected void btnImprimirMes_Click(object sender, EventArgs e)
        {
            if(iIdCotizacion != 0)
                mpePagosInd.Show();
        }

        protected void btnImprimirPagosInd_Click(object sender, EventArgs e)
        {
            int iIdCotizacion = ddlCotizacion.SelectedValue.S().I();
            DataTable dtPagosInd = new DBCotizador().DBGetPagosIndividuales(iIdCotizacion);

            if (dtPagosInd.Rows.Count > 0)
            {
                DataTable dtCot = new DBCotizador().DBGetObj(iIdCotizacion);

                if (dtCot.Rows.Count > 0)
                {
                    double dTasa = dtCot.Rows[0]["fd_Tasa"].S().Db();
                    decimal dPrecio = dtCot.Rows[0]["fm_Precio"].S().D();
                    decimal dEnganche = dtCot.Rows[0]["fi_Enganche"].S().D();
                    double dPagosIndividuales = dtCot.Rows[0]["dPagosInd"].S().Db();
                    int iPlazo = dtCot.Rows[0]["fi_Plazo"].S().I();
                    string sIdentificacionCli = dtCot.Rows[0]["fc_IdentificacionCli"].S();
                    string sIdentificacionAva = dtCot.Rows[0]["fc_IdentificacionAva"].S();
                    string sNoIdentificacionCli = dtCot.Rows[0]["NoIdentificacionCli"].S();
                    string sNoIdentificacionAva = dtCot.Rows[0]["NoIdentificacionAva"].S();
                    string sNombreCliente = dtCot.Rows[0]["fc_NombreCliente"].S();
                    string sNombreAval = dtCot.Rows[0]["fc_NombreAval"].S();
                    string sDirCliente = dtCot.Rows[0]["fc_DirCliente"].S();
                    string sDirAval = dtCot.Rows[0]["fc_DirAval"].S();

                    DataTable dtHeader = Utils.CalculaCotizacion(dTasa, dPrecio, dEnganche, dPagosIndividuales, iPlazo);

                    if (dtHeader.Rows.Count > 0)
                    {
                        DateTime dFechaHoy = DateTime.Now;

                        DataTable dtAmort = new DataTable();
                        dtAmort.Columns.Add("IdCotizacion");
                        dtAmort.Columns.Add("NoPago");
                        dtAmort.Columns.Add("Monto");
                        dtAmort.Columns.Add("FechaPago");

                        DataTable dtPagos = new DataTable();
                        dtPagos.Columns.Add("iCotizacion");
                        dtPagos.Columns.Add("iNoPago");
                        dtPagos.Columns.Add("dMonto");
                        dtPagos.Columns.Add("dtFecha");
                        dtPagos.Columns.Add("sAcreedor");
                        dtPagos.Columns.Add("sDireccion");
                        dtPagos.Columns.Add("sTelefono");
                        dtPagos.Columns.Add("fc_IdentificacionCli");
                        dtPagos.Columns.Add("fc_IdentificacionAva");
                        dtPagos.Columns.Add("NoIdentificacionCli");
                        dtPagos.Columns.Add("NoIdentificacionAva");
                        dtPagos.Columns.Add("fc_NombreCliente");
                        dtPagos.Columns.Add("fc_NombreAval");
                        dtPagos.Columns.Add("fc_DirCliente");
                        dtPagos.Columns.Add("fc_DirAval");

                        foreach (DataRow row in dtPagosInd.Rows)
                        {
                            DataRow dr = dtPagos.NewRow();
                            dr["iCotizacion"] = iIdCotizacion.S();
                            dr["iNoPago"] = (1).S();
                            dr["dMonto"] = row["fc_Monto"].S();
                            dr["dtFecha"] = Convert.ToDateTime(row["fd_FechaPago"]).ToLongDateString();
                            dr["sAcreedor"] = lblRespAcreedor.Text.S();
                            dr["sDireccion"] = lblRespSucursal.Text.S();
                            dr["sTelefono"] = sTelefonoSucursal;
                            dr["fc_IdentificacionCli"] = sIdentificacionCli;
                            dr["fc_IdentificacionAva"] = sIdentificacionAva;
                            dr["NoIdentificacionCli"] = sNoIdentificacionCli;
                            dr["NoIdentificacionAva"] = sNoIdentificacionAva;
                            dr["fc_NombreCliente"] = sNombreCliente;
                            dr["fc_NombreAval"] = sNombreAval;
                            dr["fc_DirCliente"] = sDirCliente;
                            dr["fc_DirAval"] = sDirAval;

                            dtPagos.Rows.Add(dr);


                            DataRow drA = dtAmort.NewRow();
                            drA["IdCotizacion"] = iIdCotizacion.S();
                            drA["NoPago"] = (1).S();
                            drA["Monto"] = row["fc_Monto"].S();
                            drA["FechaPago"] = Convert.ToDateTime(row["fd_FechaPago"]).Day.S() + "/" + Convert.ToDateTime(row["fd_FechaPago"]).Month.S() + "/" + Convert.ToDateTime(row["fd_FechaPago"]).Year.S();

                            dtAmort.Rows.Add(drA);
                        }

                        dtPagosTemp = dtAmort.Copy();
                        iIdTipoPago = 2;

                        if (eSavePagos != null)
                            eSavePagos(sender, e);

                        ReportDocument rd = new ReportDocument();

                        string strPath = string.Empty;
                        strPath = Server.MapPath("Reports\\InfPagaresInd.rpt");
                        strPath = strPath.Replace("\\Views\\Principales", "");
                        rd.Load(strPath);

                        if (dtPagos.Rows.Count > 0)
                        {
                            rd.SetDataSource(dtPagos);

                            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PagosIndividuales");
                        }

                    }                    
                }
                else
                {
                    MostrarMensaje("No se encontraron pagos individuales, favor de verificar", "Pagos individuales");
                }
            }
        }

        protected void btnAceptarPagosInd_Click(object sender, EventArgs e)
        {
            int iIdCotizacion = ddlCotizacion.SelectedValue.S().I();

            DataTable dtCot = new DBCotizador().DBGetObj(iIdCotizacion);

            if (dtCot.Rows.Count > 0)
            {
                double dTasa = dtCot.Rows[0]["fd_Tasa"].S().Db();
                decimal dPrecio = dtCot.Rows[0]["fm_Precio"].S().D();
                decimal dEnganche = dtCot.Rows[0]["fi_Enganche"].S().D();
                double dPagosIndividuales = dtCot.Rows[0]["dPagosInd"].S().Db();
                int iPlazo = dtCot.Rows[0]["fi_Plazo"].S().I();
                string sIdentificacionCli = dtCot.Rows[0]["fc_IdentificacionCli"].S();
                string sIdentificacionAva = dtCot.Rows[0]["fc_IdentificacionAva"].S();
                string sNoIdentificacionCli = dtCot.Rows[0]["NoIdentificacionCli"].S();
                string sNoIdentificacionAva = dtCot.Rows[0]["NoIdentificacionAva"].S();
                string sNombreCliente = dtCot.Rows[0]["fc_NombreCliente"].S();
                string sNombreAval = dtCot.Rows[0]["fc_NombreAval"].S();
                string sDirCliente = dtCot.Rows[0]["fc_DirCliente"].S();
                string sDirAval = dtCot.Rows[0]["fc_DirAval"].S();


                DataTable dtHeader = Utils.CalculaCotizacion(dTasa, dPrecio, dEnganche, dPagosIndividuales, iPlazo);
                DataTable dtPagosInd = new DBCotizador().DBGetPagosIndividuales(iIdCotizacion);


                string sAcreedor = string.Empty;
                string sDireccion = string.Empty;
                string sTelefono = string.Empty;

                //switch (ddlSucursal.SelectedValue.S())
                //{
                //    case "1": // AMOZOC
                sAcreedor = lblRespAcreedor.Text.S(); //new DBParametro().ConsultaParametro(4).sValor;
                sDireccion = lblRespSucursal.Text.S(); //new DBParametro().ConsultaParametro(3).sValor;
                sTelefono = sTelefonoSucursal; //new DBParametro().ConsultaParametro(6).sValor;
                //        break;
                //    case "2": // XALAPA
                //        break;
                //    case "3": // TECAMACHALCO
                //        break;
                //}


                DataTable dtDetalle = new DataTable();
                int iOpc = rblPagosInd.SelectedValue.S().I();

                // 1 MESES CON PAGOS DOBLES
                // 2 SEPARAR PAGOS DE MENSUALIDADES

                if (dtHeader.Rows.Count > 0)
                {
                    DateTime dFechaHoy = DateTime.Now;

                    DataTable dtAmort = new DataTable();
                    dtAmort.Columns.Add("IdCotizacion");
                    dtAmort.Columns.Add("NoPago");
                    dtAmort.Columns.Add("Monto");
                    dtAmort.Columns.Add("FechaPago");

                    DataTable dtPagos = new DataTable();
                    dtPagos.Columns.Add("iCotizacion");
                    dtPagos.Columns.Add("iNoPago");
                    dtPagos.Columns.Add("dMonto");
                    dtPagos.Columns.Add("dtFecha");
                    dtPagos.Columns.Add("sAcreedor");
                    dtPagos.Columns.Add("sDireccion");
                    dtPagos.Columns.Add("sTelefono");
                    dtPagos.Columns.Add("fc_IdentificacionCli");
                    dtPagos.Columns.Add("fc_IdentificacionAva");
                    dtPagos.Columns.Add("NoIdentificacionCli");
                    dtPagos.Columns.Add("NoIdentificacionAva");
                    dtPagos.Columns.Add("fc_NombreCliente");
                    dtPagos.Columns.Add("fc_NombreAval");
                    dtPagos.Columns.Add("fc_DirCliente");
                    dtPagos.Columns.Add("fc_DirAval");

                    int iSumaMes = 1;
                    for (int i = 0; i < iPlazo; i++)
                    {
                        DateTime dtFechaPagare = dFechaHoy.AddMonths(iSumaMes);

                        if (ExisteFechaEnMes(dtPagosInd, dtFechaPagare))
                        {
                            DataRow dr = dtPagos.NewRow();
                            DataRow drA = dtAmort.NewRow();
                            
                            switch (iOpc)
                            {
                                case 1: //MESES CON PAGOS DOBLES
                                    dr["iCotizacion"] = iIdCotizacion.S();
                                    dr["iNoPago"] = (i + 1).S() + " / " + iPlazo.S();
                                    dr["dMonto"] = "$" + dtHeader.Rows[0]["PrimerPago"].S();
                                    dr["dtFecha"] = dtFechaPagare.ToLongDateString();
                                    dr["sAcreedor"] = sAcreedor;
                                    dr["sDireccion"] = sDireccion;
                                    dr["sTelefono"] = sTelefono;
                                    dr["fc_IdentificacionCli"] = sIdentificacionCli;
                                    dr["fc_IdentificacionAva"] = sIdentificacionAva;
                                    dr["NoIdentificacionCli"] = sNoIdentificacionCli;
                                    dr["NoIdentificacionAva"] = sNoIdentificacionAva;
                                    dr["fc_NombreCliente"] = sNombreCliente;
                                    dr["fc_NombreAval"] = sNombreAval;
                                    dr["fc_DirCliente"] = sDirCliente;
                                    dr["fc_DirAval"] = sDirAval;

                                    dtPagos.Rows.Add(dr);


                                    drA["IdCotizacion"] = iIdCotizacion.S();
                                    drA["NoPago"] = (i + 1).S();
                                    drA["Monto"] = dtHeader.Rows[0]["PrimerPago"].S();
                                    drA["FechaPago"] = dtFechaPagare.Day.S() + "/" + dtFechaPagare.Month.S() + "/" + dtFechaPagare.Year.S();

                                    dtAmort.Rows.Add(drA);

                                    break;
                                case 2: //SEPARAR PAGOS DE MENSUALIDADES
                                    iSumaMes++;
                                    DateTime FechaPagareInd = dFechaHoy.AddMonths(iSumaMes);

                                    dr["iCotizacion"] = iIdCotizacion.S();
                                    dr["iNoPago"] = (i + 1).S() + " / " + iPlazo.S();
                                    dr["dMonto"] = "$" + dtHeader.Rows[0]["PrimerPago"].S();
                                    dr["dtFecha"] = FechaPagareInd.ToLongDateString();
                                    dr["sAcreedor"] = sAcreedor;
                                    dr["sDireccion"] = sDireccion;
                                    dr["sTelefono"] = sTelefono;
                                    dr["fc_IdentificacionCli"] = sIdentificacionCli;
                                    dr["fc_IdentificacionAva"] = sIdentificacionAva;
                                    dr["NoIdentificacionCli"] = sNoIdentificacionCli;
                                    dr["NoIdentificacionAva"] = sNoIdentificacionAva;
                                    dr["fc_NombreCliente"] = sNombreCliente;
                                    dr["fc_NombreAval"] = sNombreAval;
                                    dr["fc_DirCliente"] = sDirCliente;
                                    dr["fc_DirAval"] = sDirAval;

                                    dtPagos.Rows.Add(dr);


                                    drA["IdCotizacion"] = iIdCotizacion.S();
                                    drA["NoPago"] = (i + 1).S();
                                    drA["Monto"] = dtHeader.Rows[0]["PrimerPago"].S();
                                    drA["FechaPago"] = FechaPagareInd.Day.S() + "/" + FechaPagareInd.Month.S() + "/" + FechaPagareInd.Year.S();

                                    dtAmort.Rows.Add(drA);
                                    break;
                            }
                        }
                        else
                        {
                            DataRow dr = dtPagos.NewRow();
                            dr["iCotizacion"] = iIdCotizacion.S();
                            dr["iNoPago"] = (i + 1).S() + " / " + iPlazo.S();
                            dr["dMonto"] = "$" + dtHeader.Rows[0]["PrimerPago"].S();
                            dr["dtFecha"] = dtFechaPagare.ToLongDateString();
                            dr["sAcreedor"] = sAcreedor;
                            dr["sDireccion"] = sDireccion;
                            dr["sTelefono"] = sTelefono;
                            dr["fc_IdentificacionCli"] = sIdentificacionCli;
                            dr["fc_IdentificacionAva"] = sIdentificacionAva;
                            dr["NoIdentificacionCli"] = sNoIdentificacionCli;
                            dr["NoIdentificacionAva"] = sNoIdentificacionAva;
                            dr["fc_NombreCliente"] = sNombreCliente;
                            dr["fc_NombreAval"] = sNombreAval;
                            dr["fc_DirCliente"] = sDirCliente;
                            dr["fc_DirAval"] = sDirAval;

                            dtPagos.Rows.Add(dr);


                            DataRow drA = dtAmort.NewRow();
                            drA["IdCotizacion"] = iIdCotizacion.S();
                            drA["NoPago"] = (i + 1).S();
                            drA["Monto"] = dtHeader.Rows[0]["PrimerPago"].S();
                            drA["FechaPago"] = dtFechaPagare.Day.S() + "/" + dtFechaPagare.Month.S() + "/" + dtFechaPagare.Year.S();

                            dtAmort.Rows.Add(drA);
                        }

                        iSumaMes++;
                    }

                    dtPagosTemp = dtAmort.Copy();
                    iIdTipoPago = 1;

                    if(eSavePagos != null)
                        eSavePagos(sender, e);

                    ReportDocument rd = new ReportDocument();

                    string strPath = string.Empty;
                    strPath = Server.MapPath("Reports\\InfPagares.rpt");
                    strPath = strPath.Replace("\\Views\\Principales", "");
                    rd.Load(strPath);

                    if (dtPagos.Rows.Count > 0)
                    {
                        rd.SetDataSource(dtPagos);

                        rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Pagare");
                    }
                }
            }
        }

        protected void btnImprimirContratoCred_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dtExtras = new DataTable();
            DataColumn column;
            DataRow rowT;
            #region  Table Extras
            column = new DataColumn();
            column.ColumnName = "Acreedor";
            dtExtras.Columns.Add(column);

            

            #endregion
            try
            {
                if (eGetDatosContrato != null)
                    eGetDatosContrato(sender, e);

                ReportDocument rd = new ReportDocument();
                DataTable dtc = new DataTable();

                string strPath = string.Empty;
                strPath = Server.MapPath("Reports\\InfContrato.rpt");
                strPath = strPath.Replace("\\Views\\Principales", "");
                rd.Load(strPath, OpenReportMethod.OpenReportByDefault);

                dtc = dtDatosC;
                dtc.TableName = "dtCont";

                #region ajuste de Valores DT Extras

                column = new DataColumn();
                column.ColumnName = "ImpLetra";
                dtExtras.Columns.Add(column);

                rowT = dtExtras.NewRow();
                rowT["Acreedor"] = lblRespAcreedor.Text;
                rowT["ImpLetra"] = "---";
                dtExtras.Rows.Add(rowT);
                dtExtras.TableName = "dtExtras";
                #endregion

                ds.Tables.Add(dtc);
                ds.Tables.Add(dtExtras);

                rd.SetDataSource(ds);

                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ContratoCredito");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrio un error al exportar: " + ex.Message, "Error al exportar");
            }
        }
            public void GeneraContratoCredito()
        {
            
        }

        protected void btnEntregarAuto_Click(object sender, EventArgs e)
        {
            if (eSaveObj != null)
                eSaveObj(sender, e);

            Response.Redirect(@"..\Cobranza\frmCobranza.aspx");
        }

        protected void btnSelAcreedor_Click(object sender, EventArgs e)
        {
            if (eGetAcreedores != null)
                eGetAcreedores(sender, e);

            gvAcreedor.DataSource = dtAcreedor;
            gvAcreedor.DataBind();

            mpeAcreedor.Show();
        }

        protected void btnSelSucursal_Click(object sender, EventArgs e)
        {
            if (eGetDirecciones != null)
                eGetDirecciones(sender, e);

            gvDirecciones.DataSource = dtDireccion;
            gvDirecciones.DataBind();

            mpeSucursales.Show();
        }

        protected void btnAceptarAcreedor_Click(object sender, EventArgs e)
        {
            bool ban = false;
            string sAcreedor = string.Empty;
            foreach (GridViewRow row in gvAcreedor.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("rbSelecciona");
                if (rb != null)
                {
                    ban = rb.Checked;
                    if (rb.Checked)
                    {
                        sAcreedor = row.Cells[1].Text.S();
                        break;
                    }
                }
            }

            if (ban)
            {
                lblRespAcreedor.Text = sAcreedor;
            }
            else
                lblErrorAcreedor.Text = "Debe seleccionar al menos un acreedor, favor de verificar";
        }

        protected void btnAceptarDir_Click(object sender, EventArgs e)
        {
            bool ban = false;
            string sDireccion = string.Empty;
            foreach (GridViewRow row in gvDirecciones.Rows)
            {
                RadioButton rb = (RadioButton)row.FindControl("rbSelecciona");
                if (rb != null)
                {
                    ban = rb.Checked;
                    if (rb.Checked)
                    {
                        sDireccion = row.Cells[1].Text.S();
                        sTelefonoSucursal = row.Cells[2].Text.S();
                        break;
                    }
                }
            }

            if (ban)
            {
                lblRespSucursal.Text = sDireccion;
            }
            else
                lblErrorDir.Text = "Debe seleccionar al menos una dirección, favor de verificar";
        }

        #endregion
        
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

        private bool ExisteFechaEnMes(DataTable dtPagosInd, DateTime dtFechaPagare)
        {
            bool ban = false;
            foreach (DataRow row in dtPagosInd.Rows)
            {
                DateTime dtFechaPago = Convert.ToDateTime(row["fd_FechaPago"]);

                if (dtFechaPago.Year == dtFechaPagare.Year && dtFechaPago.Month == dtFechaPagare.Month)
                {
                    ban = true;
                    break;
                }
            }

            return ban;
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        #endregion
        
        #region "Vars y Propiedades"

        Formalizar_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetCotizaciones;
        public event EventHandler eGetCliente;
        public event EventHandler eSavePagos;
        public event EventHandler eSavePagosInd;
        public event EventHandler eGetAcreedores;
        public event EventHandler eGetDirecciones;
        public event EventHandler eGetDatosContrato;

        public int iIdClienteC
        {
            get { return (int)ViewState["ViIdClienteC"]; }
            set { ViewState["ViIdClienteC"] = value; }
        }

        public int iIdSucursal
        {
            get
            {
                return ddlSucursal.SelectedValue.S().I();
            }
        }

        public int iIdCotizacion
        {
            get
            {
                return ddlCotizacion.SelectedValue.S().I();
            }
        }

        public DataTable dtCotizacion
        {
            set;
            get;
        }

        public DataTable dtCliente
        {
            get { return (DataTable)ViewState["dtClienteV"]; }
            set { ViewState["dtClienteV"] = value; }
        }

        public DataTable dtDatosC
        {
            get { return (DataTable)ViewState["dtDatosCV"]; }
            set { ViewState["dtDatosCV"] = value; }
        }

        public decimal iMontoCompromiso
        {
            set;
            get;
        }

        public object[] oArrFormalizacion
        {
            get
            {
                return new object[]
                {
                    "@fi_IdCotizacion", iIdCotizacion,
                    "@fm_MontoCompromiso", iMontoCompromiso,
                    "@fc_UsuarioGenero",Session["usuario"].S()
                };
            }
        }

        public string sUsuarioForm
        {
            get
            {
                return Session["usuario"].S();
            }
        }

        public DataTable dtPagosTemp
        {
            get { return (DataTable)ViewState["VPagos"]; }
            set { ViewState["VPagos"] = value; }
        }

        public int iIdTipoPago
        {
            get { return (int)ViewState["ViIdTipoPago"]; }
            set { ViewState["ViIdTipoPago"] = value; }
        }

        public DataTable dtAcreedor
        {
            get { return (DataTable)ViewState["VdtAcreedor"]; }
            set { ViewState["VdtAcreedor"] = value; }
        }

        public DataTable dtDireccion
        {
            get { return (DataTable)ViewState["VdtDireccion"]; }
            set { ViewState["VdtDireccion"] = value; }
        }

        public string sTelefonoSucursal
        {
            get { return (string)ViewState["VsTelefonoSucursal"]; }
            set { ViewState["VsTelefonoSucursal"] = value; }
        }

        #endregion
    }
}