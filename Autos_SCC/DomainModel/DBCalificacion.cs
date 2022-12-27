using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using NucleoBase.Core;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBCalificacion : DBBase
    {
        public int SetActualizaCalificacion(int iIdCotizacion, int iIdCalificacion, string sUsuario)
        {
            try
            {
                object vNew = null;
                if (new DBCotizador().DBGetObj(iIdCotizacion) != null)
                {

                    vNew = oDB_SP.EjecutarValor("[Autos].[spU_ActualizaCalificacionCotizacion]", "@fi_Id", iIdCotizacion,
                                                                                "@fi_IdCalificacion", iIdCalificacion,
                                                                                "@fc_UsuarioCalifico", sUsuario,
                                                                                "@fd_FechaCalifico", DateTime.Now);

                }
                return iIdCotizacion = vNew != null ? vNew.S().I() : -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}