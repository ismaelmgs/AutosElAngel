using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Objetos;

namespace Autos_SCC.Interfaces
{
    public interface IViewCliente : IBaseView
    {
        Direcciones oDireccion { get; }
        Cliente oCliente { get; set; }
        Direcciones oDireccionAval { get; }
        DataTable dtCotizacion { get; set; }
        DataTable dtCliente { get; set; }
        DataTable dtMunicipios { get; set; }
        DataTable dtColonias { get; set; }
        DataTable dtCP { get; set; }
        int iIdSucursal { get; }
        int iIdCotizacion { get; }
        Cliente oAval { get; set; }
        bool bEsCorrectoAval { get; set; }
        bool bEsCorrectoCliente { get; set; }

        void LoadSucursales(DataTable dtSuc);
        void LoadEstados(DataTable dtEst);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LimpiaCampos();
        void CargaAval(DataTable dtAval);

        event EventHandler eGetCotizaciones;
        event EventHandler eGetCliente;
        event EventHandler eGetMunicipio;
        event EventHandler eGetColonias;
        event EventHandler eGetCodigoP;
        event EventHandler eGetMunicipioAval;
        event EventHandler eGetColoniasAval;
        event EventHandler eGetCodigoPAval;
        event EventHandler eSaveAval;
        event EventHandler eGetAval;
    }
}