using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Objetos;

namespace Autos_SCC.Interfaces
{
    public interface IViewVersion : IBaseView
    {
        Versiones oGetSetObjSelection { get; set; }
        Versiones oCatalogo { get; set; }
        object[] oArrFiltros { get; }

        void LoadObjects(DataTable dtObjCat);
        void LoadMarcas(DataTable dtMarcas);
        void LoadTiposAuto(DataTable dtTiposAuto);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eGetTiposAuto;
    }
}