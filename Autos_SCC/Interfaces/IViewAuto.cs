using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewAuto: IBaseView
    {
        Auto oGetSetObjSelection { get; set; }
        Auto oAuto { get; set; }
        GastosAuto oGastoAuto { get; set; }
        object[] oArrFiltros { get; }
        DataTable dtGastosAuto { get; set; }
        int iAuto { get; set; }
        DataTable dtDataSource { get; set; }

        void LoadObjects(DataTable dtObjCat);
        void LoadMarcas(DataTable dtMarcas);
        void LoadVersiones(DataTable dtVersiones);
        void LoadTiposAuto(DataTable dtTiposAuto);
        void LoadSucursales(DataTable dtSuc);
        void LoadEstatusAuto(DataTable dtEst);
        void LoadEstados(DataTable dtEstados);
        void MostrarMensaje(string sMensaje, string sCaption);
        
        event EventHandler eGetTiposAuto;
        event EventHandler eGetVersiones;
        event EventHandler eGetGastosPorAuto;
        event EventHandler eSaveGastoAuto;
        event EventHandler eDeleteGastoAuto;
        event EventHandler eSearchAutos;
    }
}