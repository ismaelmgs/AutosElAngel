using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Cotizacion : BaseObjeto
    {
        private int _iId = 0;
        private string _sNombre = string.Empty;
        private string _sApeMaterno = string.Empty;
        private string _sSegNombre = string.Empty;
        private string _sApePaterno = string.Empty;
        private int _iPlazo = 0;
        private int _iIdAuto = 0;
        private decimal _dPrecio = 0;
        private decimal _dEnganche = 0;
        private decimal _dTasa = 0;
        private int _iIdSucursal = 0;
        private string _sCorreo = string.Empty;
        private int _iEstatus = 1;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;
        private List<PagoIndividual> _oLsPagoIndividual = new List<PagoIndividual>();
        private int _iIdClienteAnt = 0;

        public int iId { get { return _iId; } set { _iId = value; } }

        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }

        public string sSegNombre { get { return _sSegNombre; } set { _sSegNombre = value; } }

        public string sApePaterno { get { return _sApePaterno; } set { _sApePaterno = value; } }

        public string sApeMaterno { get { return _sApeMaterno; } set { _sApeMaterno = value; } }

        public int iPlazo { get { return _iPlazo; } set { _iPlazo = value; } }

        public int iIdAuto { get { return _iIdAuto; } set { _iIdAuto = value; } }

        public decimal dPrecio { get { return _dPrecio; } set { _dPrecio = value; } }

        public decimal dEnganche { get { return _dEnganche; } set { _dEnganche = value; } }

        public decimal dTasa { get { return _dTasa; } set { _dTasa = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La Sucursal es obligatoria")]
        public int iIdSucursal { get { return _iIdSucursal; } set { _iIdSucursal = value; } }

        public string sCorreo { get { return _sCorreo; } set { _sCorreo = value; } }

        [Display(Name = "¿Activo? "), Required]
        public int iEstatus { get { return _iEstatus; } set { _iEstatus = value; } }

        [Display(Name = "Usuario"), Required]
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        [Display(Name = "Fecha DT"), Required]
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }

        public List<PagoIndividual> oLsPagoIndividual { get { return _oLsPagoIndividual; } set { _oLsPagoIndividual = value; } }

        public int iIdClienteAnt { get { return _iIdClienteAnt; } set { _iIdClienteAnt = value; } }
    }

}