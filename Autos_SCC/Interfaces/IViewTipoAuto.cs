using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewTipoAuto : IBaseView
    {
        TipoAuto oGetSetObjSelection { get; set; }
        TipoAuto oCatalogo { get; set; }
        object[] oArrFiltros { get; }

        void LoadObjects(DataTable dtObjCat);
        void LoadMarcas(DataTable dtMarcas);
        void MostrarMensaje(string sMensaje, string sCaption);
    }
}