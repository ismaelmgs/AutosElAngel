using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewFormalizar : IBaseView
    {
        DataTable dtCotizacion { get; set; }
        DataTable dtCliente { get; set; }
        int iIdCotizacion { get; }
        int iIdSucursal { get; }
        object[] oArrFormalizacion { get; }
        string sUsuarioForm { get; }
        DataTable dtPagosTemp { get; set; }
        int iIdTipoPago { get; set; }
        int iIdClienteC { get; set; }

        DataTable dtAcreedor { get; set; }
        DataTable dtDireccion { get; set; }

        DataTable dtDatosC { get; set; }


        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadSucursales(DataTable dtSuc);
        
        event EventHandler eGetCotizaciones;
        event EventHandler eGetCliente;
        event EventHandler eSavePagos;
        event EventHandler eGetAcreedores;
        event EventHandler eGetDirecciones;
        event EventHandler eGetDatosContrato;
    }
}