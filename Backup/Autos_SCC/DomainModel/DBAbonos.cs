using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Objetos;

namespace Autos_SCC.DomainModel
{
    public class DBAbonos : DBBase
    {
        public DataTable dtGetCreditosSucursal(int iIdSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCreditosAbonos]", "@fi_Sucursal", iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataSet dtGetCliente(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Autos].[spS_ConsultaDatosPagosCredito]", "@IdCotizacion", iId);
            }
            catch
            {
                return new DataSet();
            }
        }

        public DataTable dtGetObtieneTransacciones(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaTransacionesPorCredito]", "@fi_IdCotizacion", iIdCotizacion);
            }
            catch
            {
                return new DataTable();
            }
        }

        public void SetInsertaTransacciones(Transaccion oTran)
        {
            try
            {
                oDB_SP.EjecutarSP("[Autos].[spI_InsertaTransaccion]", "@fi_IdCotizacion", oTran.iIdCotizacion,
                                                                        "@fc_Codigo", oTran.sCodigo,
                                                                        "@fc_Monto", oTran.dMonto,
                                                                        "@fc_UsuarioCreacion", oTran.sUsuario);
            }
            catch(Exception ex)
            {
                oTran.oErr.bExisteError = true;
                oTran.oErr.sMsjError = ex.Message;
            }
        }

        public void SetActualizaTransacciones(int iIdCotizacion, string sUsuario)
        {
            try
            {
                oDB_SP.EjecutarSP("[Autos].[spU_ActualizaAmortizacionesPagos]", "@fi_IdCotizacion", iIdCotizacion,
                                                                                "@fc_Usuario", sUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetInsertaPayIndividual(Transaccion oTran)
        {
            try
            {
                oDB_SP.EjecutarSP("[Autos].[spI_InsertaTransaccionPayInd]", "@fi_IdAmortizacion", oTran.iIdAmortizacion,
                                                                            "@fc_Codigo", oTran.sCodigo,
                                                                            "@fc_Monto", oTran.dMonto,
                                                                            "@fc_UsuarioCreacion", oTran.sUsuario);
            }
            catch (Exception ex)
            {
                oTran.oErr.bExisteError = true;
                oTran.oErr.sMsjError = ex.Message;
            }
        }
    }
}