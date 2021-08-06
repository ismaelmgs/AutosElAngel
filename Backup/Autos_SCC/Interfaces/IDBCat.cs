using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.Interfaces
{
    public interface IDBCat<TObjCat>
    {
        //List<TObjCat> oLstObjsCat { get; }
        DataTable dtObjsCat { get; }

        TObjCat DBGetObj(int iId);
        bool DBObjExists(int iId);
        void DBSaveObj(ref TObjCat oCat);
        void DBDeleteObj(ref TObjCat oCat);
        DataTable DBSearchObj(object[] oArrFiltros);
    }
}