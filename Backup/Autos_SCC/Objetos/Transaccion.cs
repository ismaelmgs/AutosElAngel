using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Transaccion : BaseObjeto
    {
        private int _iIdCotizacion = 0;
        private Int64 _iIdAmortizacion = 0;
        private string _sCodigo = string.Empty;
        private decimal _dMonto = 0;
        private string _sUsuario = string.Empty;

        public int iIdCotizacion
        {
            get { return _iIdCotizacion; }
            set { _iIdCotizacion = value; }
        }

        public Int64 iIdAmortizacion
        {
            get { return _iIdAmortizacion; }
            set { _iIdAmortizacion = value; }
        }
        
        public string sCodigo
        {
            get { return _sCodigo; }
            set { _sCodigo = value; }
        }
        
        public decimal dMonto
        {
            get { return _dMonto; }
            set { _dMonto = value; }
        }

        public string sUsuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }

    }
}