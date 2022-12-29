using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TipoAuto : BaseObjeto
    {
        private int _iId = -1;
        private int _iMarca = 0;
        public string _sDescripcion = string.Empty;
        private int _iActivo = 1;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }
        
        [Range(1, Int32.MaxValue, ErrorMessage = "La Marca es obligatoria")]
        public int iMarca { get { return _iMarca; } set { _iMarca = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Tipo de Auto es obligatorio")]
        [Display(Name = "Tipo de Auto", AutoGenerateField = true)]
        public virtual string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }

        [Display(Name = "¿Activo? "), Required]
        public int iActivo { get { return _iActivo; } set { _iActivo = value; } }

        [Display(Name = "Usuario"), Required]
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        [Display(Name = "Fecha DT"), Required]
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
    }
}