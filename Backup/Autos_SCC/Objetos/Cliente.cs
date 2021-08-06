using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Cliente : BaseObjeto
    {
        private int _iId = -1;
        private int _iIdCotizacion = 0;
        private string _sNombre = string.Empty;
        private string _sNombre2 = string.Empty;
        private string _sApePaterno = string.Empty;
        private string _sApeMaterno = string.Empty;
        private int _iIdCP = 0;
        private int _iEstado = 0;
        private string _sMunicipio = string.Empty;
        private string _sColonia = string.Empty;
        private string _sCP = string.Empty;
        private string _sCalle = string.Empty;
        private int _iNoExt = 0;
        private int _iNoInt = 0;
        private int _iNacionalidad = 0;
        private int _iTipoIdentificacion = 0;
        private string _sNumeroIFE = string.Empty;
        private string _sCURP = string.Empty;
        private string _sRFC = string.Empty;
        private DateTime _dtFechaNacimiento = new DateTime();
        private int _iSexo = 0;
        private int _iEdoCivil = 0;
        private string _sLada = string.Empty;
        private string _sTelefono = string.Empty;
        private string _sTelCel = string.Empty;
        private int _iTiempoVivir = 0;
        private int _iActivo = 0;
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaUltMov = new DateTime();
        private string _sFechaUltMov = string.Empty;
        private string _sFechaNac = string.Empty;

        public string sFechaNac
        {
            get { return _sFechaNac; }
            set { _sFechaNac = value; }
        }


        public int iId { get { return _iId; } set { _iId = value; } }

        public int iIdCotizacion { get { return _iIdCotizacion; } set { _iIdCotizacion = value; } }

        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }

        public string sNombre2 { get { return _sNombre2; } set { _sNombre2 = value; } }

        public string sApePaterno { get { return _sApePaterno; } set { _sApePaterno = value; } }

        public string sApeMaterno { get { return _sApeMaterno; } set { _sApeMaterno = value; } }

        public int iIdCP { get { return _iIdCP; } set { _iIdCP = value; } }

        public int iEstado { get { return _iEstado; } set { _iEstado = value; } }

        public string sMunicipio { get { return _sMunicipio; } set { _sMunicipio = value; } }

        public string sColonia { get { return _sColonia; } set { _sColonia = value; } }

        public string sCP { get { return _sCP; } set { _sCP = value; } }

        public string sCalle { get { return _sCalle; } set { _sCalle = value; } }
        
        public int iNoExt { get { return _iNoExt; } set { _iNoExt = value; } }
        
        public int iNoInt { get { return _iNoInt; } set { _iNoInt = value; } }
        
        public int iNacionalidad { get { return _iNacionalidad; } set { _iNacionalidad = value; } }

        public int iTipoIdentificacion { get { return _iTipoIdentificacion; } set { _iTipoIdentificacion = value; } }

        public string sNumeroIFE { get { return _sNumeroIFE; } set { _sNumeroIFE = value; } }

        public string sCURP { get { return _sCURP; } set { _sCURP = value; } }

        public string sRFC { get { return _sRFC; } set { _sRFC = value; } }
        
        public DateTime dtFechaNacimiento { get { return _dtFechaNacimiento; } set { _dtFechaNacimiento = value; } }
        
        public int iSexo { get { return _iSexo; } set { _iSexo = value; } }
        
        public int iEdoCivil { get { return _iEdoCivil; } set { _iEdoCivil = value; } }
        
        public string sLada { get { return _sLada; } set { _sLada = value; } }
        
        public string sTelefono { get { return _sTelefono; } set { _sTelefono = value; } }
        
        public string sTelCel { get { return _sTelCel; } set { _sTelCel = value; } }
        
        public int iTiempoVivir { get { return _iTiempoVivir; } set { _iTiempoVivir = value; } }

        public int iActivo { get { return _iActivo; } set { _iActivo = value; } }

        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        public DateTime dtFechaUltMov { get { return _dtFechaUltMov; } set { _dtFechaUltMov = value; } }

        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
    }
}