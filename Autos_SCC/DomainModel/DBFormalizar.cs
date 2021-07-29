using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NucleoBase.Core;

namespace Autos_SCC.DomainModel
{
    public class DBFormalizar : DBBase
    {
        public DataTable dtGetCotizacionesSucursal(int iIdSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCotizacionesFormalizar]", "@fi_Sucursal", iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable oGetCliente(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCotizacion]", "@fi_Id", iId);
            }
            catch
            {
                return new DataTable();
            }
        }

        public void DBSaveObj(int iIdCotizacion, string sUsuario)
        {
            try
            {
                oDB_SP.EjecutarSP("[Autos].[spI_InsertaTablaAmortizacion]", "@fi_IdCotizacion", iIdCotizacion,
                                                                            "@fc_UsuarioGenero", sUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetExistenPagosTemporales(int iIdCotizacion)
        {
            try
            {
                DataTable dt = oDB_SP.EjecutarDT("[dbo].[spS_ConsultaExistenPagares]", "@fi_IdCotizacion", iIdCotizacion);

                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0][0].ToString());
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSavePagos(DataTable dt, int iIdTipoPago)
        {
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string[] sFecha = dr["FechaPago"].S().Split('/');
                    DateTime dtPago = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());

                    oDB_SP.EjecutarSP("[Autos].[spI_InsertaPagosTemporales]", "@fi_IdCotizacion", dr["IdCotizacion"].S().I(),
                                                                             "@fi_NoPago", dr["NoPago"].S().I(),
                                                                             "@fm_MontoCompromiso", dr["Monto"].S().D(),
                                                                             "@fd_FechaCompromiso", dtPago,
                                                                             "@fi_IdTipoPago",iIdTipoPago);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBDelEliminaPagos(int iIdCotizacion, int iIdTipoPago)
        {
            try
            {
                oDB_SP.EjecutarSP("[Autos].[spD_EliminaAmortizacionesTemp]", "@fi_IdCotizacion", iIdCotizacion,
                                                                             "@fi_IdTipoPago", iIdTipoPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtGetAcreedor
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAcreedores]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }

        public DataTable dtGetDirecciones
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaDireccionesSucursales]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }
    }
}