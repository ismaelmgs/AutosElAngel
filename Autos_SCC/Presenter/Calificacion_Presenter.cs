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
            oIView.eSetInsertaCalificacion += eSetInsertaCalificacion_Presenter;

        }
        public void LoadObjectsddl_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtSucursalesPorUsuario(oIView.iIdUsuario));
        }
        public void LoadObjectsCredit_Presenter()
        {
            oIView.LoadCotizaciones(new DBCotizador().DBGetCotizacionesTerminadas(oIView.iIdSucursal));
        }
        public void LoadObjects_PresenterFilter()
        {
            oIView.LoadCotizaciones(new DBCotizador().DBGetCotizacionesTerminadasFiltro(oIView.iIdSucursal, oIView.sIdsCalificacion));
        }
        private void eSetInsertaCalificacion_Presenter(object sender, EventArgs e)
        {
            int iResult = oIGestCat.SetActualizaCalificacion(oIView.iIdCotizacion,oIView.iIdCalificacion, oIView.sUsuario);

            if (iResult > -1)
            {
                oIView.MostrarMensaje("La calificación se guardo correctamente.", "Calificación");
            }
            else
                oIView.MostrarMensaje("Ocurrio un error al aplicar la calificación.", "Calificación");
        }
    }
}