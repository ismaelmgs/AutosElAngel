using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autos_SCC.Interfaces
{
    public interface IViewCalificacion : IBaseView
    {
        int iIdUsuario { get; }
        int iIdSucursal { get; }
        string sIdsCalificacion { get; set; }

        void LoadCotizaciones(DataTable dtCotizaciones);
        void LoadSucursales(DataTable dtSuc);
    }
}
