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
        int iIdCotizacion { get; set; }
        int iIdCalificacion { get; set; }
        string sIdsCalificacion { get; set; }
        string sUsuario { get; }

        void LoadCotizaciones(DataTable dtCotizaciones);
        void LoadSucursales(DataTable dtSuc);
        void MostrarMensaje(string sMessage, string sCaption);

        event EventHandler eSetInsertaCalificacion;
    }
}
