using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBSucursales: DBBase
    {
        public DataTable dtObj
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaSucursales]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }
        public DataTable dtSucursalesPorUsuario(int IdUsuario)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaSucursalesPorUsuario]", "@IdUsuario", IdUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}