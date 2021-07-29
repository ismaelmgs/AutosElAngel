using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewCobranza : IBaseView
    {
        DataTable dtCreditos { get; set; }
        int iIdSucursal { get; }

        void LoadSucursales(DataTable dtSuc);

        event EventHandler eGetCreditos;
    }
}