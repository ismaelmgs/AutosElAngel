using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBSeguridad : DBBase
    {
        public DataTable DBGetObtieneUsuario(object [] oArr)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaUsuarioAcceso]", oArr);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}