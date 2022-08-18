using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Presenter
{
    public class TotalesCartera_Presenter : BasePresenter<IViewTotalesCartera>
    {
        private readonly DBReporte oIGestCat;
        public TotalesCartera_Presenter(IViewTotalesCartera oView, DBReporte oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchReporte += eSearchReporte_Presenter;
        }

        private void eSearchReporte_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrid(oIGestCat.GetConsultaReporteTotalesCartera(oIView.sSucursal));
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
        }
    }
}