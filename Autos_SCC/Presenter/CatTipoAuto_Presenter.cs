using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Presenter
{
    public class CatTipoAuto_Presenter: BasePresenter<IViewTipoAuto>
    {
        private readonly DBTipoAuto oIGestCat;

        public CatTipoAuto_Presenter(IViewTipoAuto oView, DBTipoAuto oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            //NewObj_Presenter(this, EventArgs.Empty);
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadMarcas(new DBMarca().dtObjsCat);
            oIView.LoadObjects(oIGestCat.dtObjCat);            
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            LoadObjects_Presenter();
            oIView.oCatalogo = new TipoAuto();
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            if (oIView.oGetSetObjSelection != null)
            {
                if (!oIGestCat.DBObjExists(oIView.oGetSetObjSelection.iId))
                    return;

                TipoAuto oTempCat = oIGestCat.DBGetObj(oIView.oGetSetObjSelection.iId);
                oIView.oCatalogo = oTempCat;
            }
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            TipoAuto oTempCat = oIView.oCatalogo;
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
                    //if (Utils.ConfirmacionDlg("Eliminación", "¿Esta seguro de eliminar el siguiente Registro? => " +
                    //        oIView.oCatalogo.sDescripcion))
                    //{
                    TipoAuto oTempCat = oIView.oCatalogo;
                    oIGestCat.DBDeleteObj(ref oTempCat);
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError,"Eliminación ID:" + oTempCat.iId);

                    NewObj_Presenter(sender, e);
                    //}
                }
                else
                {
                    oIView.MostrarMensaje("Debe seleccionar un registro para efectuar esta operación", "Eliminar");
                }
            }   // end if oCat != null
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
    }
}