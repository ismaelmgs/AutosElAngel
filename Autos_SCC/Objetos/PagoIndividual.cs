using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class PagoIndividual
    {
        private int _iId = 0;
        private int _iIdCotizacion = 0;
        private decimal _dMonto = 0;
        private DateTime _dtFechaPago = new DateTime();
        private int _iActivo = 1;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;

        public int iId { get { return _iId; } set { _iId = value; } }

        public int iIdCotizacion { get { return _iIdCotizacion; } set { _iIdCotizacion = value; } }

        public decimal dMonto { get { return _dMonto; } set { _dMonto = value; } }

        public DateTime dtFechaPago { get { return _dtFechaPago; } set { _dtFechaPago = value; } }

        [Display(Name = "¿Activo? "), Required]
        public int iActivo { get { return _iActivo; } set { _iActivo = value; } }

        [Display(Name = "Usuario"), Required]
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        [Display(Name = "Fecha DT"), Required]
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
    }
}