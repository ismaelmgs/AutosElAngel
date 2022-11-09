using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autos_SCC.Objetos
{
    public class Autorizador : BaseObjeto
    {
        private int _fi_id = -1;
        private string _PriNombre = string.Empty;
        private string _SegNombre = string.Empty;
        private string _PatApellido = string.Empty;
        private string _MatApellido = string.Empty;
        private int _fi_IdPerfil = -1;
        private string _NomPerfil = string.Empty;
        private int _fi_IdSucursal = -1;
        private string _NomSucurlal = string.Empty;

        public int fi_id { get { return _fi_id; } set { _fi_id = value; } }
        public string PriNombre { get { return _PriNombre; } set { _PriNombre = value; } }
        public string SegNombre { get { return _SegNombre; } set { _SegNombre = value; } }
        public string PatApellido { get { return _PatApellido; } set { _PatApellido = value; } }
        public string MatApellido { get { return _MatApellido; } set { _MatApellido = value; } }
        public int fi_IdPerfil { get { return _fi_IdPerfil; } set { _fi_IdPerfil = value; } }
        public string NomPerfil { get { return _NomPerfil; } set { _NomPerfil = value; } }
        public int fi_IdSucursal { get { return _fi_IdSucursal; } set { _fi_IdSucursal = value; } }
        public string NomSucurlal { get { return _NomSucurlal; } set { _NomSucurlal = value; } }

    }
}