using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;
using System.ComponentModel.DataAnnotations;
using NucleoBase.Core;
using System.Data;
using Autos_SCC.Clases;

namespace Autos_SCC.Presenter
{
    public class Cliente_Presenter : BasePresenter<IViewCliente>
    {
        private readonly DBCliente oIGestCat;

        public Cliente_Presenter(IViewCliente oView, DBCliente oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetCotizaciones += eGetCotizaciones_Presenter;
            oIView.eGetCliente += eGetCliente_Presenter;
            oIView.eGetMunicipio += eGetMunicipio_Presenter;
            oIView.eGetColonias += eGetColonias_Presenter;
            oIView.eGetCodigoP += eGetCodigoP_Presenter;
            oIView.eGetMunicipioAval += eGetMunicipioAval_Presenter;
            oIView.eGetColoniasAval += eGetColoniasAval_Presenter;
            oIView.eGetCodigoPAval += eGetCodigoPAval_Presenter;
            oIView.eSaveAval += eSaveAval_Presenter;
            oIView.eGetAval += eGetAval_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
            oIView.LoadEstados(new DBCliente().dtGetEstados);
        }

        private void eGetCotizaciones_Presenter(object sender, EventArgs e)
        {
            oIView.dtCotizacion = oIGestCat.dtGetCotizacionesSucursal(oIView.iIdSucursal);
        }

        private void eGetCliente_Presenter(object sender, EventArgs e)
        {
            oIView.dtCliente = oIGestCat.oGetCliente(oIView.iIdCotizacion);
            if(oIView.dtCliente.Rows.Count > 0)
            {
                int iCl = oIView.dtCliente.Rows[0]["fi_IdCliente"].S().I();
                if (iCl > 0)
                {
                    oIView.oCliente = oIGestCat.DBGetObj(iCl);
                    oIView.CargaAval(oIGestCat.DBGetTablaAvalPorCotizacion(oIView.iIdCotizacion));
                }
                else
                    oIView.LimpiaCampos();
            }
        }

        private void eGetMunicipio_Presenter(object sender, EventArgs e)
        {
            oIView.dtMunicipios = oIGestCat.dtGetMunicipios(oIView.oDireccion.iIdEstado);
        }

        private void eGetColonias_Presenter(object sender, EventArgs e)
        {
            oIView.dtColonias = oIGestCat.dtGetColonias(oIView.oDireccion.iIdEstado, oIView.oDireccion.sMunicipio);
        }

        private void eGetCodigoP_Presenter(object sender, EventArgs e)
        {
            oIView.dtCP = oIGestCat.dtGetCodigoPostal(oIView.oDireccion.iIdEstado, oIView.oDireccion.sMunicipio, oIView.oDireccion.sColonia);
        }

        private void eGetMunicipioAval_Presenter(object sender, EventArgs e)
        {
            oIView.dtMunicipios = oIGestCat.dtGetMunicipios(oIView.oDireccionAval.iIdEstado);
        }

        private void eGetColoniasAval_Presenter(object sender, EventArgs e)
        {
            oIView.dtColonias = oIGestCat.dtGetColonias(oIView.oDireccionAval.iIdEstado, oIView.oDireccionAval.sMunicipio);
        }

        private void eGetCodigoPAval_Presenter(object sender, EventArgs e)
        {
            oIView.dtCP = oIGestCat.dtGetCodigoPostal(oIView.oDireccionAval.iIdEstado, oIView.oDireccionAval.sMunicipio, oIView.oDireccionAval.sColonia);
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            Cliente oTempCat = oIView.oCliente;
            var oVldResults = new List<ValidationResult>();
            var oVldContext = new ValidationContext(oTempCat, null, null);

            if (Validator.TryValidateObject(oTempCat, oVldContext, oVldResults, true))
            {
                oIGestCat.DBSaveObj(ref oTempCat);
                if (oTempCat.oErr.bExisteError)
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError, "GUARDAR");
                else
                {
                    oIView.bEsCorrectoCliente = true;
                    oIView.MostrarMensaje("Guardado exitoso", "GUARDAR");
                }
            }
            else
            {
                var sVldErrors = String.Join("\n", oVldResults.Select(t => String.Format("- {0}", t.ErrorMessage)));
                oIView.MostrarMensaje(sVldErrors, "ERRORES EN VALIDACIONES ");
            }
        }

        private void eSaveAval_Presenter(object sender, EventArgs e)
        {
            Aval oTempCat = oIView.oAval;
            var oVldResults = new List<ValidationResult>();
            var oVldContext = new ValidationContext(oTempCat, null, null);
            DataTable dt = new DataTable();

            if (Validator.TryValidateObject(oTempCat, oVldContext, oVldResults, true))
            {

                dt = oIGestCat.DBSaveObjAval(ref oTempCat);
                if (oTempCat.oErr.bExisteError)
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError, "GUARDAR");
                else
                {
                    oIView.bEsCorrectoAval = true;
                    oIView.MostrarMensaje("Guardado exitoso", "GUARDAR");
                    oIView.dtAvalSaved = dt;
                }
            }
            else
            {
                var sVldErrors = String.Join("\n", oVldResults.Select(t => String.Format("- {0}", t.ErrorMessage)));
                oIView.MostrarMensaje(sVldErrors, "ERRORES EN VALIDACIONES ");
            }
        }

        private void eGetAval_Presenter(object sender, EventArgs e)
        {
            oIView.oAval = oIGestCat.DBGetObjAvalPorCotizacion(oIView.iIdCotizacion);
        }

        private void eSaveFormalizacion_Presenter(object sender, EventArgs e)
        {
            //DataTable dtCot = new DBCotizador().DBGetObj(oIView.iIdCotizacion);

            //if (dtCot.Rows.Count > 0)
            //{
            //    double dTasa = dtCot.Rows[0]["fd_Tasa"].S().Db();
            //    decimal dPrecio = dtCot.Rows[0]["fm_Precio"].S().D();
            //    decimal dEnganche = dtCot.Rows[0]["fi_Enganche"].S().D();
            //    double dPagosIndividuales = dtCot.Rows[0]["dPagosInd"].S().Db();
            //    int iPlazo = dtCot.Rows[0]["fi_Plazo"].S().I();

            //    DataTable dtHeader = Utils.CalculaCotizacion(dTasa, dPrecio, dEnganche, dPagosIndividuales, iPlazo);
            //    DataTable dtPagosInd = new DBCotizador().DBGetPagosIndividuales(oIView.iIdCotizacion);

            //    DataTable dtDetalle = new DataTable();
            //    int iOpc = oIView.iOpcPagosInd;

            //    // 1 MESES CON PAGOS DOBLES
            //    // 2 SEPARAR PAGOS DE MENSUALIDADES

            //    if (dtHeader.Rows.Count > 0)
            //    {
            //        DateTime dFechaHoy= DateTime.Now;

            //        DataTable dtPagos = new DataTable();
            //        dtPagos.Columns.Add("iCotizacion");
            //        dtPagos.Columns.Add("iNoPago");
            //        dtPagos.Columns.Add("dMonto");
            //        dtPagos.Columns.Add("dtFecha", typeof(DateTime));

            //        int iSumaMes = 1;
            //        for (int i = 0; i < iPlazo; i++)
            //        {
            //            DateTime dtFechaPagare = dFechaHoy.AddMonths(iSumaMes);

            //            if (ExisteFechaEnMes(dtPagosInd, dtFechaPagare))
            //            {
            //                DataRow dr = dtPagos.NewRow();

            //                switch (iOpc)
            //                {
            //                    case 1: //MESES CON PAGOS DOBLES
            //                        dr["iCotizacion"] = oIView.iIdCotizacion.S();
            //                        dr["iNoPago"] = (i + 1).S();
            //                        dr["dMonto"] = dtHeader.Rows[0]["PrimerPago"].S();
            //                        dr["dtFecha"] = dtFechaPagare;

            //                        dtPagos.Rows.Add(dr);
            //                        break;
            //                    case 2: //SEPARAR PAGOS DE MENSUALIDADES
            //                        iSumaMes++;
            //                        DateTime FechaPagareInd = dFechaHoy.AddMonths(iSumaMes);

            //                        dr["iCotizacion"] = oIView.iIdCotizacion.S();
            //                        dr["iNoPago"] = (i + 1).S();
            //                        dr["dMonto"] = dtHeader.Rows[0]["PrimerPago"].S();
            //                        dr["dtFecha"] = FechaPagareInd;

            //                        dtPagos.Rows.Add(dr);
            //                        break;
            //                }
            //            }
            //            else
            //            {
            //                DataRow dr = dtPagos.NewRow();
            //                dr["iCotizacion"] = oIView.iIdCotizacion.S();
            //                dr["iNoPago"] = (i + 1).S();
            //                dr["dMonto"] = dtHeader.Rows[0]["PrimerPago"].S();
            //                dr["dtFecha"] = dtFechaPagare;

            //                dtPagos.Rows.Add(dr);
            //            }

            //            iSumaMes++;
            //        }

            //        oIView.dtPagares = dtPagos.Copy();
            //    }
            //}

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
        
        
    }
}