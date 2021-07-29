using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBMonitor: DBBase
    {
        public DataTable dtGetCreditosSucursal(int iIdSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCreditosSucursal]", "@fi_Sucursal", iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}