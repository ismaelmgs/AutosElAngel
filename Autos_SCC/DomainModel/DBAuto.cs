using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Autos_SCC.Objetos;
using Autos_SCC.Clases;
using NucleoBase.Core;

namespace Autos_SCC.DomainModel
{
    public class DBAuto : DBBase
    {
        public DataTable dtObjCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAutosPorSucursal]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }

        public DataTable dtObjCatStatus
        {
            get
            {
                try
                {
                    List<EstatusAuto> oLst = new List<EstatusAuto>();

                    oLst = oDB.tbc_StatusAuto.Where(r => r.fi_Activo == 1).Select(r => new EstatusAuto()
                    {
                        iId = r.fi_Id,
                        sDescripcion = r.fc_Descripcion,
                        iActivo = r.fi_Activo,
                        dtFechaUltMov = r.fd_FechaUltMovimiento,
                        sFechaUltMov = r.fd_FechaUltMovimiento.Day.ToString().PadLeft(2, '0') + "/" + r.fd_FechaUltMovimiento.Month.ToString().PadLeft(2, '0')
                                                                                              + "/" + r.fd_FechaUltMovimiento.Year,
                        sUsuario = r.fc_Usuario
                    }).ToList();

                    return oLst.ConvertListToDataTable();
                }
                catch
                {
                    return new DataTable();
                }
            }
        }
        public DataTable dtObjCatTipoFacturas
        {
            get
            {
                try
                {
                    List<TipoFactura> oLst = new List<TipoFactura>();
                    oLst = oDB.tbc_TipoFacturas.Where(r => r.fi_Activo == 1).Select(r => new TipoFactura()
                    {
                        iId = r.fi_Id,
                        sDescripcion = r.fc_Descripcion,
                        iActivo = r.fi_Activo,
                        dtFechaUltMov = r.fd_FechaUltMovimiento,
                        sUsuario = r.fc_Usuario
                    }).ToList();

                    return oLst.ConvertListToDataTable();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public DataTable dtObjCatTipoTransmision
        {
            get
            {
                try
                {
                    List<TipoTransmision> oLst = new List<TipoTransmision>();
                    oLst = oDB.tbc_TipoTransmision.Where(r => r.fi_Activo == 1).Select(r => new TipoTransmision()
                    {
                        iId = r.fi_Id,
                        sDescripcion = r.fc_Descripcion,
                        iActivo = r.fi_Activo,
                        dtFechaUltMov = r.fd_FechaUltMovimiento,
                        sUsuario = r.fc_Usuario
                    }).ToList();

                    return oLst.ConvertListToDataTable();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool DBObjExists(int iId)
        {
            DataTable dtTemp = oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAutoId]", "@fi_Id", iId);
            Auto oTempEjecut = null;

            if (dtTemp.Rows.Count > 0)
            {
                oTempEjecut = dtTemp.AsEnumerable().Select(r => new Auto()
                {
                    iId = r["fi_Id"].S().I(),
                    iIdMarca = r["fi_IdMarca"].S().I(),
                    iIdVersion = r["fi_IdVersion"].S().I(),
                    iIdTipoAuto = r["fi_IdTipoAuto"].S().I(),
                    sPlaca = r["fc_Placa"].S(),
                    sNoSerie = r["fc_NoSerie"].S(),
                    sColor = r["fc_Color"].S(),
                    iIdSucursal = r["fi_Sucursal"].S().I(),
                    dPrecio = r["fm_Precio"].S().D(),
                    iKilometraje = r["fi_Kilometraje"].S().I(),
                    iStatus = r["fi_Status"].S().I(),
                    sUsuario = r["fc_Usuario"].S(),
                    dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                    IEstado = r["IdEstado"].S().I(),
                    SNumMotor = r["NumMotor"].S(),
                    STenencia = r["TenenciaHasta"].S(),
                    SFactura = r["Factura"].S(),
                    SNumero = r["Numero"].S()
                }).First();
            }

            return oTempEjecut.iId > 0 ? true : false;
        }

        public Auto DBGetObj(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAutoId]", "@fi_Id", iId).AsEnumerable().Select(r => new Auto()
                {
                    iId = r["fi_Id"].S().I(),
                    iIdMarca = r["fi_IdMarca"].S().I(),
                    iIdVersion = r["fi_IdVersion"].S().I(),
                    iIdTipoAuto = r["fi_IdTipoAuto"].S().I(),
                    sPlaca = r["fc_Placa"].S(),
                    sNoSerie = r["fc_NoSerie"].S(),
                    sColor = r["fc_Color"].S(),
                    iIdSucursal = r["fi_Sucursal"].S().I(),
                    dPrecio = r["fm_Precio"].S().D(),
                    iKilometraje = r["fi_Kilometraje"].S().I(),
                    iModelo = r["fi_Modelo"].S().I(),
                    iStatus = r["fi_Status"].S().I(),
                    sUsuario = r["fc_Usuario"].S(),
                    dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                    sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year,
                    IEstado = r["IdEstado"].S().I(),
                    SNumMotor = r["NumMotor"].S(),
                    STenencia = r["TenenciaHasta"].S(),
                    SFactura = r["Factura"].S(),
                    SNumero = r["Numero"].S(),
                    sCveConsecutivo = r["fc_CveConsecutivo"].S(),
                    iConsecutivo = r["fi_Consecutivo"].S().I(),
                    dtFechaIngreso = r["fd_FechaIngreso"].S().Dt(),
                    iIdSucursalIngreso = r["fi_SucursalIngreso"].S().I(),
                    iIdSucursalExpediente = r["fi_SucursalExpediente"].S().I(),
                    iCls = r["fi_Cls"].S().I(),
                    iIdTransmision = r["fi_Transmision"].S().I(),
                    sProveedor = r["fc_Proveedor"].S(),
                    dPrecioToma = r["fm_PrecioToma"].S().D(),
                    iIdTipoFactura = r["fi_TipoFactura"].S().I(),
                    iIdEstadoPlaca = r["fi_EdoPlaca"].S().I(),
                    sDuplicado = r["fc_Duplicado"].S(),
                    sTarjeta = r["fc_Tarjeta"].S(),
                    iNoDuenios = r["fi_NoDuenios"].S().I(),
                    sINE = r["fc_Ine"].S(),
                    sContratoCV = r["fc_ContratoCV"].S(),
                }).First();
            }
            catch
            {
                return null;
            }
        }

        public void DBSaveObj(ref Auto oEjecut)
        {
            try
            {
                object vNew = null;

                oEjecut.oErr.bExisteError = false;
                oEjecut.oErr.sMsjError = "Guardado exitoso";

                if (oEjecut != null)
                {
                    if (DBGetObj(oEjecut.iId) == null)
                    {
                        //Inserta
                        if (DBExistsAuto(oEjecut.sPlaca) == null)
                        {
                            vNew = oDB_SP.EjecutarValor("[Catalogos].[spI_InsertaAuto]", "@fi_IdMarca", oEjecut.iIdMarca,
                                                                                            "@fi_IdVersion", oEjecut.iIdVersion,
                                                                                            "@fi_IdTipoAuto", oEjecut.iIdTipoAuto,
                                                                                            "@fc_Placa", oEjecut.sPlaca,
                                                                                            "@fc_NoSerie", oEjecut.sNoSerie,
                                                                                            "@fi_Modelo", oEjecut.iModelo,
                                                                                            "@fc_Color", oEjecut.sColor,
                                                                                            "@fi_Sucursal", oEjecut.iIdSucursal,
                                                                                            "@fm_Precio", oEjecut.dPrecio,
                                                                                            "@fi_Status", oEjecut.iStatus,
                                                                                            "@fc_Usuario", oEjecut.sUsuario,
                                                                                            "@fi_Kilometraje", oEjecut.iKilometraje,
                                                                                            "@IdEstado", oEjecut.IEstado,
                                                                                            "@NumMotor", oEjecut.SNumMotor,
                                                                                            "@Tenencia", oEjecut.STenencia,
                                                                                            "@Factura", oEjecut.SFactura,
                                                                                            "@Numero", oEjecut.SNumero,
                                                                                            "@fc_CveConsecutivo", oEjecut.sCveConsecutivo,
                                                                                            "@fi_Consecutivo", oEjecut.iConsecutivo,
                                                                                            "@fd_FechaIngreso", oEjecut.dtFechaIngreso,
                                                                                            "@fi_SucursalIngreso", oEjecut.iIdSucursalIngreso,
                                                                                            "@fi_SucursalExpediente", oEjecut.iIdSucursalExpediente,
                                                                                            "@fi_Cls", oEjecut.iCls,
                                                                                            "@fi_Transmision", oEjecut.iIdTransmision,
                                                                                            "@fc_Proveedor", oEjecut.sProveedor,
                                                                                            "@fm_PrecioToma", oEjecut.dPrecioToma,
                                                                                            "@fi_TipoFactura", oEjecut.iIdTipoFactura,
                                                                                            "@fi_EdoPlaca", oEjecut.iIdEstadoPlaca,
                                                                                            "@fc_Duplicado", oEjecut.sDuplicado,
                                                                                            "@fc_Tarjeta", oEjecut.sTarjeta,
                                                                                            "@fi_NoDuenios", oEjecut.iNoDuenios,
                                                                                            "@fc_Ine", oEjecut.sINE,
                                                                                            "@fc_ContratoCV", oEjecut.sContratoCV);
                        }
                        else
                        {
                            oEjecut.oErr.bExisteError = true;
                            oEjecut.oErr.sMsjError = "Ya existe otro Auto con este número de placa: '" + oEjecut.sPlaca + "', favor de verificar.";
                        }
                    }
                    else
                    {
                        ////Actualiza
                        //Auto oUs = DBExistsAuto(oEjecut.iId);
                        //if (oUs == null)
                        //{
                        //    oEjecut.oErr.bExisteError = true;
                        //    oEjecut.oErr.sMsjError = "El auto con número de placa: '" + oEjecut.sPlaca + "' no existe, favor de verificar.";

                        //    return;
                        //}

                        //if (oUs.iId == oEjecut.iId)
                        //{
                        vNew = oDB_SP.EjecutarValor("[Catalogos].[spU_ActualizaAuto]", "@fi_Id", oEjecut.iId,
                                                                            "@fi_IdMarca", oEjecut.iIdMarca,
                                                                            "@fi_IdVersion", oEjecut.iIdVersion,
                                                                            "@fi_IdTipoAuto", oEjecut.iIdTipoAuto,
                                                                            "@fc_Placa", oEjecut.sPlaca,
                                                                            "@fc_NoSerie", oEjecut.sNoSerie,
                                                                            "@fi_Modelo", oEjecut.iModelo,
                                                                            "@fc_Color", oEjecut.sColor,
                                                                            "@fi_Sucursal", oEjecut.iIdSucursal,
                                                                            "@fm_Precio", oEjecut.dPrecio,
                                                                            "@fi_Status", oEjecut.iStatus,
                                                                            "@fc_Usuario", oEjecut.sUsuario,
                                                                            "@fi_Kilometraje", oEjecut.iKilometraje,
                                                                            "@IdEstado", oEjecut.IEstado,
                                                                            "@NumMotor", oEjecut.SNumMotor,
                                                                            "@Tenencia", oEjecut.STenencia,
                                                                            "@Factura", oEjecut.SFactura,
                                                                            "@Numero", oEjecut.SNumero,
                                                                            "@fc_CveConsecutivo", oEjecut.sCveConsecutivo,
                                                                            "@fi_Consecutivo", oEjecut.iConsecutivo,
                                                                            "@fd_FechaIngreso", oEjecut.dtFechaIngreso,
                                                                            "@fi_SucursalIngreso", oEjecut.iIdSucursalIngreso,
                                                                            "@fi_SucursalExpediente", oEjecut.iIdSucursalExpediente,
                                                                            "@fi_Cls", oEjecut.iCls,
                                                                            "@fi_Transmision", oEjecut.iIdTransmision,
                                                                            "@fc_Proveedor", oEjecut.sProveedor,
                                                                            "@fm_PrecioToma", oEjecut.dPrecioToma,
                                                                            "@fi_TipoFactura", oEjecut.iIdTipoFactura,
                                                                            "@fi_EdoPlaca", oEjecut.iIdEstadoPlaca,
                                                                            "@fc_Duplicado", oEjecut.sDuplicado,
                                                                            "@fc_Tarjeta", oEjecut.sTarjeta,
                                                                            "@fi_NoDuenios", oEjecut.iNoDuenios,
                                                                            "@fc_Ine", oEjecut.sINE,
                                                                            "@fc_ContratoCV", oEjecut.sContratoCV);

                        //}
                        //else
                        //{
                        //    oEjecut.oErr.bExisteError = true;
                        //    oEjecut.oErr.sMsjError = "Ya existe otro Auto con este número de placa: '" + oEjecut.sPlaca + "', favor de verificar.";
                        //}

                    }
                    oEjecut.iId = vNew != null ? vNew.S().I() : -1;
                }
            }
            catch (Exception ex)
            {
                oEjecut.oErr.bExisteError = true;
                oEjecut.oErr.sMsjError = "ERROR al guardar (DBSaveObj) => " + ex.Message;
            }
        }

        public void DBDeleteObj(ref Auto oCat)
        {
            try
            {
                if (oCat != null)
                {
                    oDB_SP.EjecutarSP("[Catalogos].[spD_EliminaAutoId]", "@fi_Id", oCat.iId);
                    oCat.oErr.bExisteError = false;
                    oCat.oErr.sMsjError = "Registro Eliminado Correctamente";
                }
            }
            catch (Exception ex)
            {
                oCat.oErr.bExisteError = true;
                oCat.oErr.sMsjError = "ERROR al eliminar (DBDeleteObj) => " + ex.Message;
            }
        }

        public Auto DBExistsAuto(string sPlaca)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAutoPorPlaca]", "@fc_Placa", sPlaca).AsEnumerable()
                    .Select(r => new Auto()
                    {
                        iId = r["fi_Id"].S().I(),
                        iIdMarca = r["fi_IdMarca"].S().I(),
                        iIdVersion = r["fi_IdVersion"].S().I(),
                        iIdTipoAuto = r["fi_IdTipoAuto"].S().I(),
                        sPlaca = r["fc_Placa"].S(),
                        sNoSerie = r["fc_NoSerie"].S(),
                        sColor = r["fc_Color"].S(),
                        iIdSucursal = r["fi_Sucursal"].S().I(),
                        dPrecio = r["fm_Precio"].S().D(),
                        iKilometraje = r["fi_Kilometraje"].S().I(),
                        iStatus = r["fi_Status"].S().I(),
                        sUsuario = r["fc_Usuario"].S(),
                        dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                        sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                    }).SingleOrDefault();
            }
            catch
            {
                return new Auto();
            }
        }

        public DataTable dtGetGastos(int iIdAuto)
        {
            try
            {
                List<GastosAuto> oLst = new List<GastosAuto>();

                oLst = oDB.tbr_GastosAuto.Where(r => r.fi_IdAuto == iIdAuto).Select(r => new GastosAuto()
                {
                    iId = r.fi_Id,
                    iIdAuto = r.fi_IdAuto,
                    sDescripcion = r.fc_Descripcion,
                    iMonto = Convert.ToDecimal(r.fm_Monto),
                    dtFechaUltMov = r.fd_FechaUltMovimiento,
                    sFechaUltMov = r.fd_FechaUltMovimiento.Day.ToString().PadLeft(2, '0') + "/" + r.fd_FechaUltMovimiento.Month.ToString().PadLeft(2, '0')
                                                                                          + "/" + r.fd_FechaUltMovimiento.Year,
                    sUsuario = r.fc_Usuario
                }).ToList();

                return oLst.ConvertListToDataTable();
            }
            catch
            {
                return new DataTable();
            }
        }

        public void DBSaveGasto(ref GastosAuto oCat)
        {
            try
            {
                tbr_GastosAuto tbCat = new tbr_GastosAuto();

                tbCat.fi_IdAuto = oCat.iIdAuto;
                tbCat.fc_Descripcion = oCat.sDescripcion;
                tbCat.fm_Monto = oCat.iMonto;
                tbCat.fd_FechaUltMovimiento = oCat.dtFechaUltMov;
                tbCat.fc_Usuario = oCat.sUsuario;
                oDB.tbr_GastosAuto.InsertOnSubmit(tbCat);
                oDB.SubmitChanges();
            }
            catch (Exception ex)
            {
                oCat.oErr.bExisteError = true;
                oCat.oErr.sMsjError = "Error al guardar (DBSaveGasto) => " + ex.Message;
            }
        }

        public void DBDeleteGasto(ref GastosAuto oCat)
        {
            try
            {
                if (oCat != null)
                {
                    int iId = oCat.iId;
                    tbr_GastosAuto tbMat = oDB.tbr_GastosAuto.SingleOrDefault(r => r.fi_Id == iId);

                    if (tbMat != null)   //Eliminamos
                    {
                        oDB.tbr_GastosAuto.DeleteOnSubmit(tbMat);
                        oDB.SubmitChanges();
                        oCat.oErr.bExisteError = false;
                        oCat.oErr.sMsjError = "Registro Eliminado Correctamente";
                    }
                    else
                    {
                        oCat.oErr.bExisteError = true;
                        oCat.oErr.sMsjError = "No fue Eliminado el registro con ID: " + oCat.iId + " porque no fue encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                oCat.oErr.bExisteError = true;
                oCat.oErr.sMsjError = "ERROR al eliminar (DBDeleteObj) => " + ex.Message;
            }
        }

        public DataTable DBGetBusquedaAuto(string sOpcion, string sParametro)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaAutos]", "@fc_Opcion", sOpcion
                                                                        , "@fc_Parametro", sParametro);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable DBSearchAutos(object[] oArra)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAutosBusqueda]", oArra);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
    }
}