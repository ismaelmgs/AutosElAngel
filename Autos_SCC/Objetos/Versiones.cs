using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class  Versiones : BaseObjeto
    {
        private int _iId = -1;
        private int _iIdMarca = 0;
        private int _iTipoAuto = 0;
        public string _sDescripcion = string.Empty;
        private string _sUsuario = string.Empty;
        private int _iActivo = 1;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "La Marca es obligatoria")]
        public int iIdMarca { get { return _iIdMarca; } set { _iIdMarca = value; } }

        [Range(1, Int32.MaxValue, ErrorMessage = "El tipo de auto es obligatorio")]
        public int iIdTipoAuto { get { return _iTipoAuto; } set { _iTipoAuto = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es obligatorio")]
        [Display(Name = "Version", AutoGenerateField = true)]
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