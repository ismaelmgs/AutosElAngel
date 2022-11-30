using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using NucleoBase.Core;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBCotizador : DBBase
    {
        public DataTable DBGetObj(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCotizacionId]", "@fi_Id", iIdCotizacion);
            }
            catch
            {
                return new DataTable();
            }
        }

        public void DBSaveObj(ref Cotizacion oCat)
        {
            try
            {
                object vNew = null;

                if (oCat != null)
                {
                    if (oCat.iIdClienteAnt == 0)
                    {
                        vNew = oDB_SP.EjecutarValor("[Autos].[spI_InsertaCotizacion]", "@fc_Nombre", oCat.sNombre,
                                                                                        "@fc_Nombre2", oCat.sSegNombre,
                                                                                        "@fc_ApePaterno", oCat.sApePaterno,
                                                                                        "@fc_ApeMaterno", oCat.sApeMaterno,
                                                                                        "@fi_Plazo", oCat.iPlazo,
                                                                                        "@fi_IdAuto", oCat.iIdAuto,
                                                                                        "@fi_Enganche", oCat.dEnganche,
                                                                                        "@fd_Tasa", oCat.dTasa,
                                                                                        "@fi_Sucursal", oCat.iIdSucursal,
                                                                                        "@fc_Correo", oCat.sCorreo,
                                                                                        "@fc_Usuario", oCat.sUsuario,
                                                                                        "@fb_Traje", oCat.bTrajeMedida);
                    }
                    else
                    {
                        vNew = oDB_SP.EjecutarValor("[Autos].[spI_InsertaCotizacionConCliente]", "@fc_Nombre", oCat.sNombre,
                                                                                        "@fc_Nombre2", oCat.sSegNombre,
                                                                                        "@fc_ApePaterno", oCat.sApePaterno,
                                                                                        "@fc_ApeMaterno", oCat.sApeMaterno,
                                                                                        "@fi_Plazo", oCat.iPlazo,
                                                                                        "@fi_IdAuto", oCat.iIdAuto,
                                                                                        "@fi_Enganche", oCat.dEnganche,
                                                                                        "@fd_Tasa", oCat.dTasa,
                                                                                        "@fi_Sucursal", oCat.iIdSucursal,
                                                                                        "@fc_Correo", oCat.sCorreo,
                                                                                        "@fc_Usuario", oCat.sUsuario,
                                                                                        "@fi_IdClienteAnt",oCat.iIdClienteAnt,
                                                                                        "@fb_Traje", oCat.bTrajeMedida);
                    }

                    oCat.iId = vNew != null ? vNew.S().I() : -1;

                    if (oCat.iId > 0)
                    {
                        DBSavePagosIndividuales(ref oCat);
                    }
                }
            }
            catch (Exception ex)
            {
                oCat.oErr.bExisteError = true;
                oCat.oErr.sMsjError = "ERROR al guardar (DBSaveObj) => " + ex.Message;
            }
        }

        private void DBSavePagosIndividuales(ref Cotizacion oCat)
        {
            try
            {
                foreach (PagoIndividual oPago in oCat.oLsPagoIndividual)
                {
                    DataTable dt = oDB_SP.EjecutarDT("[Autos].[spI_InsertaPagosIndividuales]", "@fi_IdCotizacion", oCat.iId,
                                                                                "@fc_Monto", oPago.dMonto,
                                                                                "@fd_FechaPago", oPago.dtFechaPago,
                                                                                "@fi_Activo", oPago.iActivo,
                                                                                "@fc_Usuario", oPago.sUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al guardar los pagos individuales Error: " + ex.Message);
            }
        }

        public DataTable DBSearchClientes(string sNombre)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaClientesNombre]", "@NOMBRE_CLI", sNombre);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable DBGetPagosIndividuales(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaPagosIndividuales]", "@fi_IdCotizacion", iIdCotizacion);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}