using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autos_SCC.Objetos
{
    [Serializable]
    public class ErrorController
    {
        private bool _bExisteError;
        private string _sMsjError = string.Empty;

        public bool bExisteError
        {
            get { return _bExisteError; }
            set { _bExisteError = value; ; }
        }
        public string sMsjError
        {
            get { return _sMsjError; }
            set { _sMsjError = value; ; }
        }
    }

    [Serializable]
    public class PagoIndividual
    {
        private decimal _dImporte;
        private DateTime _dtFechaPago = new DateTime();

        public decimal dImporte
        {
            get { return _dImporte; }
            set { _dImporte = value; ; }
        }
        public DateTime dtFechaPago
        {
            get { return _dtFechaPago; }
            set { _dtFechaPago = value; }
        }
    }
}