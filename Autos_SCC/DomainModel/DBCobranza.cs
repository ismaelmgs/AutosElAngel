using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBCobranza : DBBase
    {
        public DataTable dtGetCreditosSucursal(int iIdSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ObtieneCobranzaPorSucursal]", "@fi_IdSucursal", iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable dtGetPagosPorCredito(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaPagosPorCredito]", "@fi_IdCotizacion", iIdCotizacion);
            }
            catch
            {
                return new DataTable();
            }
        }
        
    }
}