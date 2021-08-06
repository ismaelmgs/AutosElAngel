using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class GastosAuto: BaseObjeto
    {
        private int _iId = -1;
        private int _iIdAuto = 0;
        public string _sDescripcion = string.Empty;
        private decimal _iMonto = 1;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La Auto es obligatorio")]
        public int iIdAuto { get { return _iIdAuto; } set { _iIdAuto = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Descripción es obligatorio")]
        [Display(Name = "Tipo de Auto", AutoGenerateField = true)]
        public virtual string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }

        //[Range(1, Int32.MaxValue, ErrorMessage = "La Monto es obligatorio")]
        public decimal iMonto { get { return _iMonto; } set { _iMonto = value; } }

        [Display(Name = "Usuario"), Required]
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        [Display(Name = "Fecha DT"), Required]
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
    }
}