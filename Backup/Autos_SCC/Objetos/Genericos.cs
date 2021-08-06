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
    public class Direcciones
    {
        private int _iIdEstado = 0;

        public int iIdEstado
        {
            get { return _iIdEstado; }
            set { _iIdEstado = value; }
        }

        private string _sMunicipio = string.Empty;

        public string sMunicipio
        {
            get { return _sMunicipio; }
            set { _sMunicipio = value; }
        }

        private string _sColonia = string.Empty;

        public string sColonia
        {
            get { return _sColonia; }
            set { _sColonia = value; }
        }

    }

    [Serializable]
    public class DataUserIndetity : BaseObjeto
    {
        private string _sUser = string.Empty;
        private string _sEstatus = string.Empty;
        private string _sNombre = string.Empty;
        private string _sApellidos = string.Empty;
        private string _sDominio = string.Empty;
        private string _sPerfil = string.Empty;
        private bool _bEncontrado = false;

        public string sUser
        {
            get { return _sUser; }
            set { _sUser = value; }
        }

        public string sNombre
        {
            get { return _sNombre; }
            set { _sNombre = value; }
        }

        public string sApellidos
        {
            get { return _sApellidos; }
            set { _sApellidos = value; }
        }

        public string sEstatus
        {
            get { return _sEstatus; }
            set { _sEstatus = value; }
        }

        public string sPerfil
        {
            get { return _sPerfil; }
            set { _sPerfil = value; }
            }

        public bool bEncontrado
        {
            get { return _bEncontrado; }
            set { _bEncontrado = value; }
        }
    }
}