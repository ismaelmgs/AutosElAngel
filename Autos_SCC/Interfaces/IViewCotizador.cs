using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewCotizador: IBaseView
    {
        Parametro oParametro { get; set; }
        PagoIndividual oPagoI { get; }
        DataTable dtHeader { get; set; }
        DataTable dtPagosIndividuales { get; set; }        
        Cotizacion oCotizacion { get; }        
        DataTable dtMarcas { get; set; }
        DataTable dtBusquedaAuto { get; set; }

        double dPagosIndividuales { get; }
        double dTasaPreferencial { get; set; }
        int iMarca { get; }
        object[] oArrFiltros { get; }
        string sNombreCli { get; }
        bool bSinIntereses { get; }
        bool bTrajeMedida { get; }
        int iIdUsuario { get; }

        void LoadObjects(DataTable dtObj);
        void LoadSucursales(DataTable dtSuc);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LimpiaDatos();
        void CargaClientes(DataTable dtClientes);

        event EventHandler eCalculaCotizacion;
        event EventHandler eAgregaPagoIndividual;
        event EventHandler eLoadObjects;
        event EventHandler eGetMarcas;
        event EventHandler eGetBusquedaAuto;
        event EventHandler eSaveObj;
        event EventHandler eSearchCliente;
    }
}