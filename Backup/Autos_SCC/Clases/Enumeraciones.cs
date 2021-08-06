using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autos_SCC.Clases
{
    public class Enumeraciones
    {
        public enum eEstatus
        {
            Cotizador = 1,
            CapturaCliente = 2,
            CapturaAval = 3,
            Formalizar = 4,
            Cobranza = 5
        }
    }
}