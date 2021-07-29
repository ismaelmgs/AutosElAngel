using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using NucleoBase.Core;
using System.ComponentModel.DataAnnotations;
using Autos_SCC.DomainModel;
using System.Data;

namespace Autos_SCC.Presenter
{
    public class CatVersion_Presenter : BasePresenter<IViewVersion>
    {
        private readonly IDBCat<Versiones> oIGestCat;

        public CatVersion_Presenter(IViewVersion oView, IDBCat<Versiones> oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetTiposAuto += eGetTiposAuto_Presenter;

            //NewObj_Presenter(this, EventArgs.Empty);
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.dtObjsCat);
            oIView.LoadMarcas(new DBMarca().dtObjsCat);
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            LoadObjects_Presenter();
            oIView.oCatalogo = new Versiones();
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            if (oIView.oGetSetObjSelection != null)
            {
                if (!oIGestCat.DBObjExists(oIView.oGetSetObjSelection.iId))
                    return;

                Versiones oTempCat = oIGestCat.DBGetObj(oIView.oGetSetObjSelection.iId);
                oIView.oCatalogo = oTempCat;
            }
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            Versiones oTempCat = oIView.oCatalogo;
            var oVldResults = new List<ValidationResult>();
            var oVldContext = new ValidationContext(oTempCat, null, null);

            if (Validator.TryValidateObject(oTempCat, oVldContext, oVldResults, true))
            {
                oIGestCat.DBSaveObj(ref oTempCat);
                if (oTempCat.oErr.bExisteError)
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError,"GUARDAR");
                else
                    oIView.MostrarMensaje("Guardado exitoso","GUARDAR");

                NewObj_Presenter(sender, e);
                oIView.oGetSetObjSelection = oTempCat;
            }
            else
            {
                var sVldErrors = String.Join("\n", oVldResults.Select(t => String.Format("- {0}", t.ErrorMessage)));
                oIView.MostrarMensaje(sVldErrors,"ERRORES EN VALIDACIONES ");
            }
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if (oIView.oCatalogo != null)
            {
                if (!string.IsNullOrEmpty(oIView.oCatalogo.sDescripcion)) // revisar si se match contra el -1 o un obj limpio
                {
                    Versiones oTempCat = oIView.oCatalogo;
                    oIGestCat.DBDeleteObj(ref oTempCat);
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError,"Eliminación ID:" + oTempCat.iId);

                    NewObj_Presenter(sender, e);
                }
                else
                {
                    oIView.MostrarMensaje("Debe seleccionar un registro para efectuar esta operación", "Eliminar");
                }
            }
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        private void eGetTiposAuto_Presenter(object sender, EventArgs e)
        {
            if (oIView.oCatalogo != null)
            {
                oIView.LoadTiposAuto(new DBTipoAuto().DBGetObjMarca(oIView.oCatalogo.iIdMarca));
            }
        }

    }
}