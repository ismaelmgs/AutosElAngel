using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autos_SCC.Interfaces
{
    public interface IBaseView
    {
        event EventHandler eNewObj;
        event EventHandler eObjSelected;
        event EventHandler eSaveObj;
        event EventHandler eDeleteObj;
        event EventHandler eSearchObj;
    }
}