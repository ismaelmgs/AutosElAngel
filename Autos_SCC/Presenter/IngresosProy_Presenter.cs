using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Presenter
{
    public class IngresosProy_Presenter : BasePresenter<IViewIngresosProy>
    {
        private readonly DBReporte oIGestCat;
        public IngresosProy_Presenter(IViewIngresosProy oView, DBReporte oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchReporte += eSearchReporte_Presenter;
        }

        private void eSearchReporte_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrid(oIGestCat.GetConsultaReporteIngresosProyectados(oIView.iReporte, oIView.sFecha));
        }

    }
}