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

        public DataTable GetConsultaReporteIngresosProyectados(int iReporte, string sFecha)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaRepProyecciones]", "@Reporte", iReporte,
                                                                                "@Fecha", sFecha);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}