using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;

namespace Autos_SCC.Presenter
{
    public class Calificacion_Presenter : BasePresenter<IViewCalificacion>
    {
        private readonly DBCalificacion oIGestCat;
        public Calificacion_Presenter(IViewCalificacion oView, DBCalificacion oGC)
            : base(oView)
        {
            oIGestCat = oGC;

        }
        public void LoadObjects_Presenter()
        {
            oIView.LoadCotizaciones(new DBCotizador().DBGetCotizacionesTerminadas());
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
        }
        public void LoadObjects_PresenterFilter()
        {
            oIView.LoadCotizaciones(new DBCotizador().DBGetCotizacionesTerminadasFiltro(oIView.iIdSucursal, oIView.sIdsCalificacion));
        }
    }
}