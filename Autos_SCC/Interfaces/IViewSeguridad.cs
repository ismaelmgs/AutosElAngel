using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;

namespace Autos_SCC.Interfaces
{
    public interface IViewSeguridad : IBaseView
    {
        DataUserIndetity oUser { get; set; }
        object[] oArrFiltros { get; }

        event EventHandler eGetUsuario;
    }
}