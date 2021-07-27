using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NucleoBase.BaseDeDatos;
using Autos_SCC.Clases;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using NucleoBase.Core;

namespace Autos_SCC.DomainModel
{
    public class DBTipoAuto : DBBase
    {
        public DataTable dtObjCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaTiposAuto]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }

        public TipoAuto DBGetObj(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaTipoAutoId]", "@fi_Id", iId).AsEnumerable().Select(r => new TipoAuto()
                                                                                                            {
                                                                                                                iId = r["fi_Id"].S().I(),
                                                                                                                iMarca = r["fi_IdMarca"].S().I(),
                                                                                                                sDescripcion = r["fc_Descripcion"].S(),
                                                                                                                iActivo = r["fi_Activo"].S().I(),
                                                                                                                sUsuario = r["fc_Usuario"].S(),
                                                                                                                dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                                                                                                                sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                                                                                                            }).First();
            }
            catch
            {
                return null;
            }
        }

        public DataTable DBGetObjMarca(int iIdMarca)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaTiposPorMarca]", "@fi_IdMarca", iIdMarca);
            }
            catch
            {
                return new DataTable();
            }
        }

        public void DBSaveObj(ref TipoAuto oEjecut)
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
                        if (DBExistsUsuario(oEjecut.sDescripcion) == null)
                        {
                            vNew = oDB_SP.EjecutarValor("[Catalogos].[spI_InsertaTipoAuto]", "@fi_IdMarca", oEjecut.iMarca,
                                                                                    "@fc_Descripcion", oEjecut.sDescripcion,
                                                                                    "@fi_Activo", oEjecut.iActivo,
                                                                                    "@fc_Usuario", oEjecut.sUsuario);
                        }
                        else
                        {
                            oEjecut.oErr.bExisteError = true;
                            oEjecut.oErr.sMsjError = "Ya existe otro usuario con este nombre: '" + oEjecut.sDescripcion + "', favor de verificar.";
                        }
                    }
                    else
                    {
                        //Actualiza
                        TipoAuto oUs = DBExistsUsuario(oEjecut.sDescripcion);
                        if (oUs == null)
                        {
                            oEjecut.oErr.bExisteError = true;
                            oEjecut.oErr.sMsjError = "El tipo de auto: '" + oEjecut.sDescripcion + "' no existe, favor de verificar.";

                            return;
                        }

                        if (oUs.iId == oEjecut.iId)
                        {
                            vNew = oDB_SP.EjecutarValor("[Catalogos].[spU_ActualizaTipoAuto]", "@fi_Id", oEjecut.iId,
                                                                                "@fi_IdMarca", oEjecut.iMarca,
                                                                                "@fc_Descripcion", oEjecut.sDescripcion,
                                                                                "@fi_Activo", oEjecut.iActivo,
                                                                                "@fc_Usuario", oEjecut.sUsuario);

                        }
                        else
                        {
                            oEjecut.oErr.bExisteError = true;
                            oEjecut.oErr.sMsjError = "Ya existe otro tipo de auto con este nombre: '" + oEjecut.sDescripcion + "', favor de verificar.";
                        }

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
        
        public bool DBObjExists(int iId)
        {
            DataTable dtTemp = oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaTipoAutoId]", "@fi_Id", iId);
            TipoAuto oTempEjecut = null;

            if (dtTemp.Rows.Count > 0)
            {
                oTempEjecut = dtTemp.AsEnumerable().Select(r => new TipoAuto()
                {
                    iId = r["fi_Id"].S().I(),
                    iMarca = r["fi_IdMarca"].S().I(),
                    sDescripcion = r["fc_Descripcion"].S(),
                    iActivo = r["fi_Activo"].S().I(),
                    sUsuario = r["fc_Usuario"].S(),
                    dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt()
                }).First();
            }

            return oTempEjecut.iId > 0 ? true : false;
        }

        public void DBDeleteObj(ref TipoAuto oCat)
        {
            try
            {
                if (oCat != null)
                {
                    oDB_SP.EjecutarSP("[Catalogos].[spD_EliminaTipoAutoId]", "@fi_Id", oCat.iId);                    
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

        public List<TipoAuto> DBSearchObj(object[] oArrFiltros)
        {
            try
            {
            //    DataTable dtTemp = oDB_SP.EjecutarDT("[Security].[spS_DescUsuario]", oArrFiltros); //"@fc_User", sDesc);
            //    List<TipoAuto> oTempEjecut = null;

            //    if (dtTemp.Rows.Count > 0)
            //    {
            //        oTempEjecut = dtTemp.AsEnumerable().Select(r => new TipoAuto()
            //        {
            //            iId = r["fi_Id"].S().I(),
            //            sUser = r["fc_User"].S(),
            //            sPassword = r["fc_Password"].S(),
            //            iIdPerfil = r["fi_IdPerfil"].S().I(),
            //            sDescIdPerfil = r["fc_DesPerfil"].S(),
            //            sNombre = r["fc_Nombre"].S(),
            //            sNombre2 = r["fc_Nombre2"].S(),
            //            sApePaterno = r["fc_ApePaterno"].S(),
            //            sApeMaterno = r["fc_ApeMaterno"].S(),
            //            sDominio = r["fc_Dominio"].S(),
            //            iActivo = r["fi_Activo"].S().I(),
            //            sUsuario = r["fc_Usuario"].S(),
            //            dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt()
            //        }).ToList();

            //        return oTempEjecut;
            //    }
            //    else
                    return new List<TipoAuto>();

            }
            catch
            {
                return new List<TipoAuto>();
            }
        }

        public TipoAuto DBExistsUsuario(string sUser)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaTipoAutoDesc]", "@fc_Descripcion", sUser).AsEnumerable()
                    .Select(r => new TipoAuto()
                    {
                        iId = r["fi_Id"].S().I(),
                        iMarca = r["fi_IdMarca"].S().I(),
                        sDescripcion = r["fc_Descripcion"].S(),
                        iActivo = r["fi_Activo"].S().I(),
                        sUsuario = r["fc_Usuario"].S(),
                        dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                        sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                    }).SingleOrDefault();
            }
            catch
            {
                return new TipoAuto();
            }
        }

    }
}