using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Presenter
{
    public class Monitor_Presenter : BasePresenter<IViewMonitor>
    {
        private readonly DBMonitor oIGestCat;

        public Monitor_Presenter(IViewMonitor oView, DBMonitor oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetCotizaciones += eGetCotizaciones_Presenter;
            oIView.eGetCliente += eGetCliente_Presenter;

        }
        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtObj);
        }

        private void eGetCotizaciones_Presenter(object sender, EventArgs e)
        {
            oIView.dtCotizacion = oIGestCat.dtGetCreditosSucursal(oIView.iIdSucursal);
        }

        private void eGetCliente_Presenter(object sender, EventArgs e)
        {
            oIView.dtCliente = new DBCliente().oGetCliente(oIView.iIdCotizacion);
        }
    }
}