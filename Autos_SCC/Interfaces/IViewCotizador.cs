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
        double dPagosIndividuales { get; }
        Cotizacion oCotizacion { get; }
        double dTasaPreferencial { get; set; }

        event EventHandler eCalculaCotizacion;
        event EventHandler eAgregaPagoIndividual;
    }
}