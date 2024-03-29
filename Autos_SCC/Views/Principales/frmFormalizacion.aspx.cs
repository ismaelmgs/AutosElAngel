﻿using System;
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
using System.Threading;
using Autos_SCC.Objetos;

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
                ComprobarLista();

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
                lblIdCotizacion.Text = "Cotización: " + dtCliente.Rows[0]["fi_Id"].S();

                if (!string.IsNullOrEmpty(dtCliente.Rows[0]["Referencia"].S()))
                {
                    pnlReferencia.Visible = true;
                    txtReferencia.Text = dtCliente.Rows[0]["Referencia"].S();
                    chkReferencia.Checked = true;
                }
                else
                {
                    pnlReferencia.Visible = true;
                    txtReferencia.Text = string.Empty;
                    chkReferencia.Checked = false;
                }

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
                lblIdCotizacion.Text = string.Empty;

                fsImprimir.Visible = false;
                pnlReferencia.Visible = false;
            }

            dtPagosIndividuales = null;
            dtPagosIndividuales = new DBCotizador().DBGetPagosIndividuales(iIdCotizacion);

            if (dtPagosIndividuales != null && dtPagosIndividuales.Rows.Count > 0)
                btnImprimirPagosInd.Enabled = true;
            else
                btnImprimirPagosInd.Enabled = false;
        }

        protected void btnImprimirMes_Click(object sender, EventArgs e)
        {
            if (iIdCotizacion != 0)
            {
                imgImprimirMes.Visible = true;
                mpePagosInd.Show();
            }
        }

        protected void btnImprimirPagosInd_Click(object sender, EventArgs e)
        {
            int iIdCotizacion = ddlCotizacion.SelectedValue.S().I();
            string strMonto = string.Empty;
            double dbMonto = 0;
            DataTable dtPagosInd = dtPagosIndividuales;

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
                    string sTelCliente = dtCot.Rows[0]["NoTelefonoCli"].S();
                    string sTelAval = dtCot.Rows[0]["NoTelefonoAval"].S();

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
                        dtPagos.Columns.Add("NoTelefonoCli");
                        dtPagos.Columns.Add("NoTelefonoAval");

                        foreach (DataRow row in dtPagosInd.Rows)
                        {
                            DataRow dr = dtPagos.NewRow();
                            dr["iCotizacion"] = iIdCotizacion.S();
                            dr["iNoPago"] = (1).S();

                            strMonto = row["fc_Monto"].ToString();
                            dbMonto = double.Parse(strMonto);
                            strMonto = dbMonto.ToString("c", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));

                            dr["dMonto"] = strMonto;
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
                            dr["NoTelefonoCli"] = sTelCliente;
                            dr["NoTelefonoAval"] = sTelAval;

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
            string strMonto = string.Empty;
            double dbMonto = 0;
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
                string sTelCliente = dtCot.Rows[0]["NoTelefonoCli"].S();
                string sTelAval = dtCot.Rows[0]["NoTelefonoAval"].S();


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
                    dtPagos.Columns.Add("NoTelefonoCli");
                    dtPagos.Columns.Add("NoTelefonoAval");

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

                                    strMonto = dtHeader.Rows[0]["PrimerPago"].ToString();
                                    dbMonto = double.Parse(strMonto);
                                    strMonto = dbMonto.ToString("c", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));

                                    dr["dMonto"] = strMonto;
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

                                    dr["NoTelefonoCli"] = sTelCliente;
                                    dr["NoTelefonoAval"] = sTelAval;

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

                            strMonto = dtHeader.Rows[0]["PrimerPago"].ToString();
                            dbMonto = double.Parse(strMonto);
                            strMonto = dbMonto.ToString("c", System.Globalization.CultureInfo.CreateSpecificCulture("es-MX"));


                            dr["dMonto"] = strMonto;
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

                            dr["NoTelefonoCli"] = sTelCliente;
                            dr["NoTelefonoAval"] = sTelAval;

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
            if (iIdCotizacion != 0)
            {
                imgImprimirContrato.Visible = true;
                ComprobarLista();
                mpeContratosCreditos.Show();
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
                
                if (!string.IsNullOrEmpty(sAcreedor))
                    imgAcredor.Visible = true;
                else
                    imgAcredor.Visible = false;

                ComprobarLista();
                mpeAcreedor.Hide();
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

                if (!string.IsNullOrEmpty(sDireccion))
                    imgSucursal.Visible = true;
                else
                    imgSucursal.Visible = false;

                ComprobarLista();
                mpeSucursales.Hide();
            }
            else
                lblErrorDir.Text = "Debe seleccionar al menos una dirección, favor de verificar";
        }

        protected void btnCancelarModalPagInd_Click(object sender, EventArgs e)
        {
            imgImprimirMes.Visible = false;
            ComprobarLista();
            mpePagosInd.Hide();
        }

        protected void btnAceptarImpresion_Click(object sender, EventArgs e)
        {
            ComprobarLista();
            mpeContratosCreditos.Hide();
            ImprimirContrato(); 
        }

        protected void btnCancelarImpresion_Click(object sender, EventArgs e)
        {
            imgImprimirContrato.Visible = false;
            ComprobarLista();
            mpeContratosCreditos.Hide();
        }

        #endregion

        #region METODOS
        public void ImprimirContrato()
        {
            DataSet ds = new DataSet();
            DataTable dtExtras = new DataTable();
            DataColumn column;
            DataRow rowT;

            #region  Table Extras
            column = new DataColumn();
            column.ColumnName = "Acreedor";
            dtExtras.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "ImpLetra";
            dtExtras.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "FechaContrato";
            dtExtras.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "DirSucursal";
            dtExtras.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Plazo";
            dtExtras.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "ImpLetraUnidad";
            dtExtras.Columns.Add(column);
            #endregion

            ReportDocument rd = new ReportDocument();

            

            if (eGetDatosContrato != null)
                eGetDatosContrato(null, null);

            DataTable dtc = new DataTable();

            string strPath = string.Empty;
            strPath = Server.MapPath("Reports\\InfContrato.rpt");
            strPath = strPath.Replace("\\Views\\Principales", "");
            rd.Load(strPath, OpenReportMethod.OpenReportByDefault);

            dtc = dtDatosC;
            dtc.TableName = "dtCont";

            #region ajuste de Valores DT Extras


            //decimal dImporte = 200000.00m;
            decimal dImporte = dtDatosC.Rows[0]["Enganche"].S().D();
            string strPrecioUnidad = lblPrecio.Text.Replace("Precio: $ ","");
            decimal dImporteUnidad = strPrecioUnidad.D();
            rowT = dtExtras.NewRow();
            rowT["Acreedor"] = lblRespAcreedor.Text;
            string strNumeroLetra = dImporte.NumeroALetras();
            rowT["ImpLetra"] = strNumeroLetra;
            rowT["FechaContrato"] = DateTime.Now.Day + " de " + mesLetra(DateTime.Now.Month) + " de " + DateTime.Now.Year;
            rowT["DirSucursal"] = lblRespSucursal.Text;
            rowT["Plazo"] = lblPlazo.Text.Replace("Plazo: ","").Replace("Meses","");
            rowT["ImpLetraUnidad"] = dImporteUnidad.NumeroALetras();
            dtExtras.Rows.Add(rowT);
            dtExtras.TableName = "dtExtras";
            #endregion

            ds.Tables.Add(dtc);
            ds.Tables.Add(dtExtras);
            rd.SetDataSource(ds);
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ContratoCredito");
            rd.Dispose();
        }

        private static string mesLetra(int mes)
        {
            var m = "";

            switch (mes)
            {
                case 1:
                    m = "ENERO";
                    break;
                case 2:
                    m = "FEBRERO";
                    break;
                case 3:
                    m = "MARZO";
                    break;
                case 4:
                    m = "ABRIL";
                    break;
                case 5:
                    m = "MAYO";
                    break;
                case 6:
                    m = "JUNIO";
                    break;
                case 7:
                    m = "JULIO";
                    break;
                case 8:
                    m = "AGOSTO";
                    break;
                case 9:
                    m = "SEPTIEMBRE";
                    break;
                case 10:
                    m = "OCTUBRE";
                    break;
                case 11:
                    m = "NOVIEMBRE";
                    break;
                case 12:
                    m = "DICIEMBRE";
                    break;
            }

            return m;
        }

        public void ComprobarLista()
        {
            try
            {
                if (imgAcredor.Visible == true && imgSucursal.Visible == true && imgImprimirContrato.Visible == true && imgImprimirMes.Visible == true)
                    btnEntregarAuto.Enabled = true;
                else
                    btnEntregarAuto.Enabled = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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
        public string sReferenciaBancaria
        {
            get
            {
                return txtReferencia.Text.S();
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

        public DataTable dtPagosIndividuales
        {
            get { return (DataTable)ViewState["VSPagosIndividuales"]; }
            set { ViewState["VSPagosIndividuales"] = value; }
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

        protected void chkReferencia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkReferencia.Checked)
                {
                    txtReferencia.Visible = true;
                }
                else
                {
                    txtReferencia.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}