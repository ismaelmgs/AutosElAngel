using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TipoTransmision : BaseObjeto
    {
        private int _iId = -1;
        private string _sDescripcion = string.Empty;
        private int _iActivo = 1;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();

        public int iId { get { return _iId; } set { _iId = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public int iActivo { get { return _iActivo; } set { _iActivo = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }
    }
}