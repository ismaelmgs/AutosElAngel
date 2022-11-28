using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autos_SCC.ViewModels
{
    public class DetallesCreditos
    {
        public int fi_IdAMortizacion { set; get; }
        public int fi_IdCotizacion { set; get; }
        public int fi_NoPago { set; get; }
        public decimal fm_MontoCompromiso { set; get; }
        public decimal fm_MontoPagado { set; get; }
        public string fd_FechaCompromiso { set; get; }
        public string fd_FechaCompromiso2 { set; get; }
        public string fd_FechaPago { set; get; }
        public int fi_Estatus { set; get; }
        public string fc_UsuarioRegistroPago { set; get; }
        public string fc_UsuarioGenero { set; get; }
        public string fd_FechaRegistroPago { set; get; }
        public string fd_FechaUltMovimiento { set; get; }
    }
}