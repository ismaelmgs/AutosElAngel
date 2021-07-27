using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Auto: BaseObjeto
    {
        private int _iId = -1;
        private int _iIdMarca = 0;
        private int _iIdVersion = 0;
        private int _iIdTipoAuto = 0;
        private string _sPlaca = string.Empty;
        private string _sNoSerie = string.Empty;
        private string _sColor = string.Empty;
        private int _iIdSucursal = 0;
        private decimal _dPrecio = 0;
        private int _iStatus = 0;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;
        
        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La Marca es obligatoria")]
        public int iIdMarca { get { return _iIdMarca; } set { _iIdMarca = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La versión es obligatoria")]
        public int iIdVersion { get { return _iIdVersion; } set { _iIdVersion = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "El Tipo de Auto es obligatorio")]
        public int iIdTipoAuto { get { return _iIdTipoAuto; } set { _iIdTipoAuto = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Número de Placa es obligatorio.")]
        public string sPlaca { get { return _sPlaca; } set { _sPlaca = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Número de Serie es obligatorio.")]
        public string sNoSerie { get { return _sNoSerie; } set { _sNoSerie = value; } }

        public string sColor { get { return _sColor; } set { _sColor = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La sucursal es obligatoria")]
        public int iIdSucursal { get { return _iIdSucursal; } set { _iIdSucursal = value; } }

        public decimal dPrecio { get { return _dPrecio; } set { _dPrecio = value; } }

        [Display(Name = "Estatus"), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        [Display(Name = "Usuario"), Required]
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        [Display(Name = "Fecha DT"), Required]
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }

    }
}