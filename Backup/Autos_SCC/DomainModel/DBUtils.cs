using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NucleoBase.Core;

namespace Autos_SCC.DomainModel
{
    public class DBUtils : DBBase
    {
        public void DBUpdateEstatus(int iIdCredito, Autos_SCC.Clases.Enumeraciones.eEstatus eStatus)
        {
            DataTable dt = oDB_SP.EjecutarDT("[Autos].[spU_ActualizaEstatus]", "@fi_Id", iIdCredito,
                                                                "@fi_Estatus", (int)eStatus);
        }

        public string DBGetFecha()
        {
            try
            {
                DataTable dt = oDB_SP.EjecutarDT("[Autos].[spS_ObtieneFechaServidor]","");
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Fecha"].S();
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBCambiaEstatusAuto(int iIdCotizacion, int iIdStatus)
        {
            try
            {
                oDB_SP.EjecutarSP("[Catalogos].[spU_ActualizaStatusAuto]", "@fi_IdCotizacion", iIdCotizacion,
                                                                           "@fi_IdStatus", iIdStatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}