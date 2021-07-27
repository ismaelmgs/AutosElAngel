using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IViewMenu: IBaseView
    {
        string sUsuario { get; }
        void LoadObjects(DataTable dtNodos);
    }
}