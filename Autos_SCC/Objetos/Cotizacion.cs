using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Cotizacion : BaseObjeto
    {
        private int _iPlazo = 0;
        private decimal _dPrecio = 0;
        private decimal _dEnganche = 0;
        private decimal _dIntereses = 0;
        private decimal _dPrimerPago = 0;
        private decimal _dAhorro = 0;

        public int iPlazo { get { return _iPlazo; } set { _iPlazo = value; } }

        public decimal dPrecio { get { return _dPrecio; } set { _dPrecio = value; } }

        public decimal dEnganche { get { return _dEnganche; } set { _dEnganche = value; } }
        
        public decimal dIntereses { get { return _dIntereses; } set { _dIntereses = value; } }
        
        public decimal dPrimerPago { get { return _dPrimerPago; } set { _dPrimerPago = value; } }
        
        public decimal dAhorro { get { return _dAhorro; } set { _dAhorro = value; } }
    }
}