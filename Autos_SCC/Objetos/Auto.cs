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
        private int _iModelo = 0;
        private int _iIdSucursal = 0;
        private decimal _dPrecio = 0;
        private int _iKilometraje = 0;
        private int _iStatus = 0;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;

        private int _iEstado = 0;
        private string _sNumMotor = string.Empty;
        private string _sTenencia = string.Empty;
        private string _sFactura = string.Empty;
        private string _sNumero = string.Empty;

        private string _sCveConsecutivo = string.Empty;
        private int _iConsecutivo = 0;
        private DateTime _dtFechaIngreso = new DateTime();
        private int _iIdSucursalIngreso = 0;
        private int _iIdSucursalExpediente = 0;
        private int _iCls = 0;
        private int _iIdTransmision = 0;
        private string _sProveedor = string.Empty;
        private decimal _dPrecioToma = 0;
        private int _iIdTipoFactura = 0;
        private int _iIdEstadoPlaca = 0;
        private string _sDuplicado = string.Empty;
        private string _sTarjeta = string.Empty;
        private int _iNoDuenios = 0;
        private string _sINE = string.Empty;
        private string _sContratoCV = string.Empty;



        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La Marca es obligatoria")]
        public int iIdMarca { get { return _iIdMarca; } set { _iIdMarca = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La Versión es obligatoria")]
        public int iIdVersion { get { return _iIdVersion; } set { _iIdVersion = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "El Tipo de Auto es obligatorio")]
        public int iIdTipoAuto { get { return _iIdTipoAuto; } set { _iIdTipoAuto = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Número de Placa es obligatorio.")]
        public string sPlaca { get { return _sPlaca; } set { _sPlaca = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Número de Serie es obligatorio.")]
        public string sNoSerie { get { return _sNoSerie; } set { _sNoSerie = value; } }

        public int iModelo { get { return _iModelo; } set { _iModelo = value; } }

        public string sColor { get { return _sColor; } set { _sColor = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La sucursal es obligatoria")]
        public int iIdSucursal { get { return _iIdSucursal; } set { _iIdSucursal = value; } }

        public decimal dPrecio { get { return _dPrecio; } set { _dPrecio = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "El kilometraje es obligatorio")]
        public int iKilometraje { get { return _iKilometraje; } set { _iKilometraje = value; } }

        [Display(Name = "Estatus"), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        [Display(Name = "Usuario"), Required]
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        [Display(Name = "Fecha DT"), Required]
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }

        public int IEstado { get => _iEstado; set => _iEstado = value; }

        public string SNumMotor { get => _sNumMotor; set => _sNumMotor = value; }

        public string STenencia { get => _sTenencia; set => _sTenencia = value; }

        public string SFactura { get => _sFactura; set => _sFactura = value; }

        public string SNumero { get => _sNumero; set => _sNumero = value; }

        public string sCveConsecutivo { get => _sCveConsecutivo; set => _sCveConsecutivo = value; }

        public int iConsecutivo { get => _iConsecutivo; set => _iConsecutivo = value; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha de ingreso es obligatoria. <br />")]
        public DateTime dtFechaIngreso { get => _dtFechaIngreso; set => _dtFechaIngreso = value; }

        [Range(1, Int32.MaxValue, ErrorMessage = "La sucursal de ingreso es obligatoria. <br />")]
        public int iIdSucursalIngreso { get => _iIdSucursalIngreso; set => _iIdSucursalIngreso = value; }

        [Range(1, Int32.MaxValue, ErrorMessage = "La ubicación del expediente es obligatoria. <br />")]
        public int iIdSucursalExpediente { get => _iIdSucursalExpediente; set => _iIdSucursalExpediente = value; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cls obligatorio. <br />")]
        public int iCls { get => _iCls; set => _iCls = value; }

        [Range(1, Int32.MaxValue, ErrorMessage = "La transmisión obligatoria. <br />")]
        public int iIdTransmision { get => _iIdTransmision; set => _iIdTransmision = value; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El proveedor es obligatorio. <br />")]
        public string sProveedor { get => _sProveedor; set => _sProveedor = value; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El precio toma es obligatorio. <br />")]
        public decimal dPrecioToma { get => _dPrecioToma; set => _dPrecioToma = value; }

        [Range(1, Int32.MaxValue, ErrorMessage = "El tipo de factura es obligatoria. <br />")]
        public int iIdTipoFactura { get => _iIdTipoFactura; set => _iIdTipoFactura = value; }

        [Range(1, Int32.MaxValue, ErrorMessage = "El estado al que pertenecen las placas es obligatorio. <br />")]
        public int iIdEstadoPlaca { get => _iIdEstadoPlaca; set => _iIdEstadoPlaca = value; }

        [RegularExpression("Si|No", ErrorMessage = "El duplicado es obligatorio. Si ó No <br />")]
        public string sDuplicado { get => _sDuplicado; set => _sDuplicado = value; }

        [RegularExpression("Si|No", ErrorMessage = "La tarjeta de circulación es obligatoria. Si ó No <br />")]
        public string sTarjeta { get => _sTarjeta; set => _sTarjeta = value; }

        [Range(1, Int32.MaxValue, ErrorMessage = "El numero de dueños anteriores es obligatorio. <br />")]
        public int iNoDuenios { get => _iNoDuenios; set => _iNoDuenios = value; }

        [RegularExpression("Si|No", ErrorMessage = "La INE es obligatoria. Si ó No <br />")]
        public string sINE { get => _sINE; set => _sINE = value; }

        [RegularExpression("Si|No", ErrorMessage = "Contratos de compra/venta es obligatorio. Si ó No <br />")]
        public string sContratoCV { get => _sContratoCV; set => _sContratoCV = value; }

    }
}