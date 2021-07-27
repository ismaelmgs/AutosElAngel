using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewCat<TObjCat> : IBaseView
    {
        TObjCat oGetSetObjSelection { get; set; }
        TObjCat oCatalogo { get; set; }
        object[] oArrFiltros { get; }
        string sCaptionBtnGuardar { set; }
        string sNumRegistros { get; set; }

        void LoadObjects(DataTable dtObjects);
        void MostrarMensaje(string sMensaje, string sCaption);

    }
}