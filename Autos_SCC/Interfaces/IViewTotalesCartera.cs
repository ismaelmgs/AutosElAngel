using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewTotalesCartera : IBaseView
    {
        string sSucursal { get; set; }
        void LoadGrid(DataSet dt);
        void LoadSucursales(DataTable dtSuc);

        event EventHandler eSearchReporte;

    }
}
