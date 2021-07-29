using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Presenter
{
    public class Reportes_Presenter : BasePresenter<IViewReporte>
    {
        private readonly DBReporte oIGestCat;

        public Reportes_Presenter(IViewReporte oView, DBReporte oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchPagos += eSearchPagos_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtObj);
        }

        private void eSearchPagos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrid(oIGestCat.GetConsultaReportePagosPeriodo(oIView.oRep));
        }
    }
}