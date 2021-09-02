using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewIngresosProy : IBaseView
    {
        int iReporte { get; set; }
        string sFecha { get; set; }
        void LoadGrid(DataTable dt);

        event EventHandler eSearchReporte;
    }
}
