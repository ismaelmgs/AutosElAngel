using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;

namespace Autos_SCC.Presenter
{
    public class Autorizador_Presenter : BasePresenter<IViewAutorizador>
    {
        private readonly DBAutorizador oIGestCat;
        public Autorizador_Presenter(IViewAutorizador oView, DBAutorizador oGC): base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetUsuarios += eGetUsuarios_Presenter;
            oIView.eGetSucursales += eGetSucursales_Presenter;
            oIView.eSearchAdministradores += eSearchAdministradores_Presenter;
            oIView.eGetPerfiles += eGetPerfiles_Presenter;
        }
        public void eGetUsuarios_Presenter(object sender, EventArgs e)
        {
            oIView.LoadUsuarios(new DBAutorizador().dtObjCat);
        }
        public void eGetSucursales_Presenter(object sender, EventArgs e)
        {
            oIView.LoadSucursales(new DBAutorizador().dtObjSuc);
        }
        public void eGetPerfiles_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPerfiles(new DBAutorizador().dtObjPer);
        }
        public void eSearchAdministradores_Presenter(object sender, EventArgs e)
        {
            oIView.LoadUsuarios(new DBAutorizador().DBSearchAdministradores(oIView.oArrFiltros));
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            if (oIView.oGetSetObjSelection != null)
            {
                if (!oIGestCat.DBObjExists(oIView.oGetSetObjSelection.fi_id))
                    return;

                Autorizador oTempCat = oIGestCat.DBGetObj(oIView.oGetSetObjSelection.fi_id);
                oIView.oAutorizador = oTempCat;
            }
        }
    }
}