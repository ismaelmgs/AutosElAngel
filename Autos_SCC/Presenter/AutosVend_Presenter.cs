using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Presenter
{
    public class AutosVend_Presenter : BasePresenter<IViewAutosVend>
    {
        private readonly DBReporte oIGestCat;

        public AutosVend_Presenter(IViewAutosVend oView, DBReporte oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchAutos += eSearchAutos_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
        }

        private void eSearchAutos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrid(oIGestCat.GetConsultaReporteAutosVendidos(oIView.oRep));
        }
    }
}