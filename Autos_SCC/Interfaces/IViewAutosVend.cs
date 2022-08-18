using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewAutosVend : IBaseView
    {
        AutosVen oRep { get; }

        int iIdUsuario { get; }

        void LoadSucursales(DataTable dtSuc);
        void LoadGrid(DataTable dt);

        event EventHandler eSearchAutos;
    }
}