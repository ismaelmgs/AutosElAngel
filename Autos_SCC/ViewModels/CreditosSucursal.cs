using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autos_SCC.ViewModels
{
    public class CreditosSucursal
    {
        public int fi_Id { set; get; }
        public string fc_Nombre { set; get; }
        public string fc_Nombre2 { set; get; }
        public string fc_ApePaterno { set; get; }
        public string fc_ApeMaterno { set; get; }
        public string NombreCompleto { set; get; }
        public int fi_Plazo { set; get; }
        public string DescPlazo { set; get; }
        public int fi_IdAuto { set; get; }
        public decimal fi_Enganche { set; get; }
        public decimal fd_Tasa { set; get; }
        public string fc_Correo { set; get; }
        public int fi_Sucursal { set; get; }
        public int fi_Estatus { set; get; }
        public string FechaProxPago { set; get; }
        public decimal MontoProxPago { set; get; }
        public string TipoAuto { set; get; }
        public decimal fm_Precio { set; get; }
        public string fc_Usuario { set; get; }
        public DateTime fd_FechaUltMovimiento { set; get; }
        public List<DetallesCreditos> lstDetalleCreditos { set; get; }
    }
}