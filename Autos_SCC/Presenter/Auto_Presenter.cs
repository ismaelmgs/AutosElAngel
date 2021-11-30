using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using Autos_SCC.Objetos;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Presenter
{
    public class Auto_Presenter : BasePresenter<IViewAuto>
    {
        private readonly DBAuto oIGestCat;

        public Auto_Presenter(IViewAuto oView, DBAuto oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetTiposAuto += eGetTiposAuto_Presenter;
            oIView.eGetVersiones += eGetVersiones_Presenter;
            oIView.eGetGastosPorAuto += eGetGastosPorAuto_Presenter;
            oIView.eSaveGastoAuto += eSaveGastoAuto_Presenter;
            oIView.eDeleteGastoAuto += eDeleteGastoAuto_Presenter;
            oIView.eSearchAutos += eSearchAutos_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadMarcas(new DBMarca().dtObjsCat);
            oIView.LoadSucursales(new DBSucursales().dtObj);
            oIView.LoadEstatusAuto(new DBAuto().dtObjCatStatus.Select("iActivo = 1").CopyToDataTable());
            oIView.LoadObjects(oIGestCat.dtObjCat);
        }
                
        private void eGetTiposAuto_Presenter(object sender, EventArgs e)
        {
            if (oIView.oAuto != null)
            {
                oIView.LoadTiposAuto(new DBTipoAuto().DBGetObjMarca(oIView.oAuto.iIdMarca));
            }
        }

        private void eGetVersiones_Presenter(object sender, EventArgs e)
        {
            if (oIView.oAuto != null)
            {
                oIView.LoadVersiones(new DBVersion().dtGetVersionesActivas(oIView.oAuto.iIdTipoAuto));
            }
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            LoadObjects_Presenter();
            oIView.oAuto = new Auto();
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            if (oIView.oGetSetObjSelection != null)
            {
                if (!oIGestCat.DBObjExists(oIView.oGetSetObjSelection.iId))
                    return;

                Auto oTempCat = oIGestCat.DBGetObj(oIView.oGetSetObjSelection.iId);
                oIView.oAuto = oTempCat;
            }
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            Auto oTempCat = oIView.oAuto;
            var oVldResults = new List<ValidationResult>();
            var oVldContext = new ValidationContext(oTempCat, null, null);

            if (Validator.TryValidateObject(oTempCat, oVldContext, oVldResults, true))
            {
                oIGestCat.DBSaveObj(ref oTempCat);
                if (oTempCat.oErr.bExisteError)
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError, "GUARDAR");
                else
                    oIView.MostrarMensaje("Guardado exitoso", "GUARDAR");

                NewObj_Presenter(sender, e);
                oIView.oGetSetObjSelection = oTempCat;
                //oIView.LoadObjects(oIGestCat.dtObjCat);
            }
            else
            {
                var sVldErrors = String.Join("\n", oVldResults.Select(t => String.Format("- {0}", t.ErrorMessage)));
                oIView.MostrarMensaje(sVldErrors, "ERRORES EN VALIDACIONES ");
            }
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            if (oIView.oAuto != null)
            {
                if (!string.IsNullOrEmpty(oIView.oAuto.sPlaca)) // revisar si se match contra el -1 o un obj limpio
                {
                    Auto oTempCat = oIView.oAuto;
                    oIGestCat.DBDeleteObj(ref oTempCat);
                    oIView.MostrarMensaje(oTempCat.oErr.sMsjError, "Eliminación ID:" + oTempCat.iId);

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
            //List<Modelo> oLstRes = oIGestCat.DBSearchObj(oIView.oArrFiltros);
            //oIView.LoadObjects(oLstRes);

            //if (oLstRes.Count > 0)
            //    oIView.oCatalogo = oLstRes.FirstOrDefault();
            //else
            //{
            //    oIView.oCatalogo = new Modelo();
            //    //Utils.MostrarMensajes("Búsqueda", "No se encontraron registros con los filtros solicitados. Por favor verfique su búsqueda.");
            //}
        }

        protected void eGetGastosPorAuto_Presenter(object sender, EventArgs e)
        {
            oIView.dtGastosAuto = oIGestCat.dtGetGastos(oIView.iAuto);
        }

        protected void eSaveGastoAuto_Presenter(object sender, EventArgs e)
        {
            GastosAuto oGasto = oIView.oGastoAuto;
            if (oGasto != null)
            {
                oIGestCat.DBSaveGasto(ref oGasto);
                if (oGasto.oErr.bExisteError)
                    oIView.MostrarMensaje(oGasto.oErr.sMsjError, "GUARDAR");

                eGetGastosPorAuto_Presenter(sender, e);
            }
        }

        protected void eDeleteGastoAuto_Presenter(object sender, EventArgs e)
        {
            GastosAuto oGasto = oIView.oGastoAuto;
            oIGestCat.DBDeleteGasto(ref oGasto);

            eGetGastosPorAuto_Presenter(sender, e);
        }

        private void eSearchAutos_Presenter(object sender, EventArgs e)
        {
            oIView.dtDataSource = oIGestCat.DBSearchAutos(oIView.oArrFiltros);
        }
    }
}