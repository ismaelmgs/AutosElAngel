using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NucleoBase.Core;
using Autos_SCC.Objetos;

namespace Autos_SCC.DomainModel
{
    public class DBReporte : DBBase
    {
        public DataTable GetConsultaReporteAutosVendidos(AutosVen oRep)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaAutosVendidos]", "@FECHA_INICIO", oRep.dtFechaInicio,
                                                                                "@FECHA_FIN", oRep.dtFechaFin,
                                                                                "@FI_SUCURSAL", oRep.iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable GetConsultaReportePagosPeriodo(AutosVen oRep)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaPagosPeriodo]", "@FECHA_INICIO", oRep.dtFechaInicio,
                                                                                "@FECHA_FIN", oRep.dtFechaFin,
                                                                                "@FI_SUCURSAL", oRep.iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataSet GetConsultaReporteIngresosProyectados(int iReporte, string sFecha, string sSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Autos].[spS_ConsultaRepProyecciones]", "@Reporte", iReporte,
                                                                                "@Fecha", sFecha,
                                                                                "@Sucursal", sSucursal);
            }
            catch
            {
                return new DataSet();
            }
        }

        public DataSet GetConsultaReporteTotalesCartera(string sSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Autos].[spS_ConsultaAdeudoTotalCartera]", "@Sucursal", sSucursal);
            }
            catch
            {
                return new DataSet();
            }
        }
    }
}