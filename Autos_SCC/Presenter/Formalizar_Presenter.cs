using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using System.Data;
using Autos_SCC.Clases;
using NucleoBase.Core;

namespace Autos_SCC.Presenter
{
    public class Formalizar_Presenter : BasePresenter<IViewFormalizar>
    {
        private readonly DBFormalizar oIGestCat;

        public Formalizar_Presenter(IViewFormalizar oView, DBFormalizar oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetCotizaciones += eGetCotizaciones_Presenter;
            oIView.eGetCliente += eGetCliente_Presenter;
            oIView.eSavePagos += eSavePagos_Presenter;
            oIView.eGetAcreedores += eGetAcreedores_Presenter;
            oIView.eGetDirecciones += eGetDirecciones_Presenter;
            oIView.eGetDatosContrato += eGetDatosContrato_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
        }

        private void eGetCotizaciones_Presenter(object sender, EventArgs e)
        {
            oIView.dtCotizacion = oIGestCat.dtGetCotizacionesSucursal(oIView.iIdSucursal);
        }

        private void eGetCliente_Presenter(object sender, EventArgs e)
        {
            oIView.dtCliente = oIGestCat.oGetCliente(oIView.iIdCotizacion);
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                switch (oIGestCat.GetExistenPagosTemporales(oIView.iIdCotizacion))
                {
                    case 0:
                        oIView.MostrarMensaje("Debe imprimir los pagares mensuales y pagos individuales antes de entregar la unidad, favor de verificar.", "Entrega de auto");
                        break;

                    case 1:
                        oIView.MostrarMensaje("Debe imprimir los pagares individuales, favor de verificar.", "Entrega de auto");
                        break;

                    case 2:
                        oIGestCat.DBSaveObj(oIView.iIdCotizacion, oIView.sUsuarioForm);
                        LoadObjects_Presenter();
                        new DBUtils().DBCambiaEstatusAuto(oIView.iIdCotizacion, 2);
                        new DBUtils().DBUpdateEstatus(oIView.iIdCotizacion, Enumeraciones.eEstatus.Cobranza);
                        new DBUtils().DBInsertaReferenciaBancaria(oIView.iIdCotizacion, oIView.sReferenciaBancaria);


                        oIView.MostrarMensaje("El auto se entregó de forma correcta, ahora se encuentra en cobranza.", "Entrega de auto");
                        break;
                }


            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Ocurrio el siguiente error al entregar el auto: " + ex.Message, "Entrega de auto");
            }
        }

        protected void eSavePagos_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iIdCotizacion = oIView.iIdCotizacion;
                int iIdTipoPago = oIView.iIdTipoPago;

                oIGestCat.DBDelEliminaPagos(iIdCotizacion, iIdTipoPago);
                oIGestCat.DBSavePagos(oIView.dtPagosTemp, iIdTipoPago);
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Ocurrio el siguiente error al entregar el auto: " + ex.Message, "Entrega de auto");
            }
        }

        protected void eGetAcreedores_Presenter(object sender, EventArgs e)
        {
            oIView.dtAcreedor = oIGestCat.dtGetAcreedor;
        }

        protected void eGetDirecciones_Presenter(object sender, EventArgs e)
        {
            oIView.dtDireccion = oIGestCat.dtGetDirecciones;
        }

        protected void eGetDatosContrato_Presenter(object sender, EventArgs e)
        {
            oIView.dtDatosC = oIGestCat.oGetDatosContrato(oIView.iIdClienteC);
        }
    }
}