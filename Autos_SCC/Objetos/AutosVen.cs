using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Autos_SCC.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class AutosVen : BaseObjeto
    {
        private int _iIdSucursal = 0;
        private DateTime? _dtFechaInicio = null;
        private DateTime? _dtFechaFin = null;

        public int iIdSucursal 
        { 
            get { return _iIdSucursal; } 
            set { _iIdSucursal = value; }
        }

        public DateTime? dtFechaInicio
        {
            get { return _dtFechaInicio; }
            set { _dtFechaInicio = value; }
        }

        public DateTime? dtFechaFin
        {
            get { return _dtFechaFin; }
            set { _dtFechaFin = value; }
        }

    }
}