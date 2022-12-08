using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Presenter
{
    public class Abonos_Presenter : BasePresenter<IViewAbonos>
    {
        private readonly DBAbonos oIGestCat;

        public Abonos_Presenter(IViewAbonos oView, DBAbonos oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetCliente += eGetCliente_Presenter;
            oIView.eGetCotizaciones += eGetCotizaciones_Presenter;
            oIView.eSetInsertaTran += eSetInsertaTran_Presenter;
            oIView.eSetInsertaPayInd += eSetInsertaPayInd_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            //oIView.LoadSucursales(new DBSucursales().dtObj);
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
        }

        private void eGetCliente_Presenter(object sender, EventArgs e)
        {
            DataSet ds = oIGestCat.dtGetCliente(oIView.iIdCotizacion);
            oIView.dtCliente = ds.Tables[0];
            oIView.dtPagosInd = ds.Tables[1];
        }

        private void eGetCotizaciones_Presenter(object sender, EventArgs e)
        {
            oIView.dtCotizacion = oIGestCat.dtGetCreditosSucursal(oIView.iIdSucursal);
        }

        private void eSetInsertaTran_Presenter(object sender, EventArgs e)
        {
            Transaccion oT = oIView.oTran;
            oIGestCat.SetInsertaTransacciones(oT);

            if (!oT.oErr.bExisteError)
            {
                oIView.MostrarMensaje("La transacción se guardo de manera correcta.", "Movimientos");
                oIGestCat.SetActualizaTransacciones(oIView.iIdCotizacion, oT.sUsuario);
                
                // Flujo de mensaje de fin de contrato
                oIView.dtCliente = oIGestCat.dtGetCliente(oIView.iIdCotizacion).Tables[0];
                oIView.ConsultaPagos();
            }
            else
                oIView.MostrarMensaje("Ocurrio un error en la aplicación del pago.", "Movimientos");
        }

        private void eSetInsertaPayInd_Presenter(object sender, EventArgs e)
        {
            Transaccion oT = oIView.oTran;
            oIGestCat.SetInsertaPayIndividual(oT);

            if (!oT.oErr.bExisteError)
            {
                oIView.MostrarMensaje("La transacción se guardo de manera correcta.", "Movimientos");
                DataSet ds = oIGestCat.dtGetCliente(oT.iIdCotizacion);
                oIView.dtCliente = ds.Tables[0];
                oIView.dtPagosInd = ds.Tables[1];
                oIView.ConsultaPagos();
            }
            else
                oIView.MostrarMensaje("Ocurrio un error en la aplicación del pago Individual.", "Movimientos");
        }

    }
}