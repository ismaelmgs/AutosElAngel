using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Objetos;

namespace Autos_SCC.Interfaces
{
    public interface IViewReporte : IBaseView
    {
        AutosVen oRep { get; }
        int iIdUsuario { get; }

        void LoadSucursales(DataTable dtSuc);
        void LoadGrid(DataTable dt);

        event EventHandler eSearchPagos;
    }
}