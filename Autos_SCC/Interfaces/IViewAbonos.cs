using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Objetos;

namespace Autos_SCC.Interfaces
{
    public interface IViewAbonos : IBaseView
    {
        DataTable dtCotizacion { get; set; }
        DataTable dtCliente { get; set; }
        DataTable dtPagosInd { get; set; }
        int iIdUsuario { get; }
        int iIdSucursal { get; }
        int iIdCotizacion { get; }
        string sTipoAbono { get; }
        string strBandera { get; set; }
        bool bTrajeMedida { get; set; }
        Transaccion oTran { get; }

        void LoadSucursales(DataTable dtSuc);
        void MostrarMensaje(string sMessage, string sCaption);
        void ConsultaPagos();

        event EventHandler eGetCotizaciones;
        event EventHandler eGetCliente;
        event EventHandler eSetInsertaTran;
        event EventHandler eSetInsertaPayInd;
    }
}