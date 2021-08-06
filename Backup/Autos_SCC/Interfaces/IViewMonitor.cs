using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewMonitor : IBaseView
    {
        DataTable dtCotizacion { get; set; }
        DataTable dtCliente { get; set; }
        int iIdSucursal { get; }
        int iIdCotizacion { get; }

        void LoadSucursales(DataTable dtSuc);

        event EventHandler eGetCotizaciones;
        event EventHandler eGetCliente;
    }
}