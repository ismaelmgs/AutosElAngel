using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using Autos_SCC.Clases;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBVersion : DBBase, IDBCat<Versiones>
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {

                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaVersiones]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }

        public Versiones DBGetObj(int iId)
        {
            try
            {
                return oDB.tbc_Version.Where(r => r.fi_Id == iId)
                                       .Select(r => new Versiones()
                                       {
                                           iId = r.fi_Id,
                                           iIdTipoAuto = r.fi_IdTipoAuto,
                                           iIdMarca = oDB.tbc_TipoAuto.SingleOrDefault(d => d.fi_Id  == r.fi_IdTipoAuto).fi_IdMarca,
                                           sDescripcion = r.fc_Descripcion,
                                           iActivo = r.fi_Activo,
                                           dtFechaUltMov = r.fd_FechaUltMovimiento,
                                           sFechaUltMov = r.fd_FechaUltMovimiento.Day.ToString().PadLeft(2, '0') + "/" + r.fd_FechaUltMovimiento.Month.ToString().PadLeft(2, '0')
                                                                                                                 + "/" + r.fd_FechaUltMovimiento.Year,
                                           sUsuario = r.fc_Usuario
                                       }).SingleOrDefault();
            }
            catch
            {
                return new Versiones();
            }
        }

        public bool DBObjExists(int iId)
        {
            try
            {
                return oDB.tbc_Version.Where(r => r.fi_Id == iId).Count() > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public void DBSaveObj(ref Versiones oCat)
        {
            try
            {
                if (oCat != null)
                {
                    Versiones oTemp = oCat;
                    tbc_Version tbCat = oDB.tbc_Version.SingleOrDefault(r => r.fi_Id == oTemp.iId);

                    if (tbCat != null)   //actualizamos
                    {
                        tbCat.fi_IdTipoAuto = oCat.iIdTipoAuto;
                        tbCat.fc_Descripcion = oCat.sDescripcion;
                        tbCat.fi_Activo = oCat.iActivo;
                        tbCat.fd_FechaUltMovimiento = oCat.dtFechaUltMov;
                        tbCat.fc_Usuario = oCat.sUsuario;
                        oDB.SubmitChanges();
                    }
                    else
                    {
                        //if (DBExisteDesc(oCat.sDescripcion.Trim().ToUpper()) == null)
                        //{
                            tbCat = new tbc_Version();
                            tbCat.fi_IdTipoAuto = oCat.iIdTipoAuto;
                            tbCat.fc_Descripcion = oCat.sDescripcion;
                            tbCat.fi_Activo = oCat.iActivo;
                            tbCat.fd_FechaUltMovimiento = oCat.dtFechaUltMov;
                            tbCat.fc_Usuario = oCat.sUsuario;
                            oDB.tbc_Version.InsertOnSubmit(tbCat);
                            oDB.SubmitChanges();
                        //}
                        //else
                        //{
                        //    oCat.oErr.bExisteError = true;
                        //    oCat.oErr.sMsjError = "Ya se encuentra una Descripción con este nombre: " + oCat.sDescripcion.ToUpper();
                        //}
                    }

                    oCat.iId = tbCat != null ? tbCat.fi_Id : -1; // regresamos el ID para su selección en GV
                }
            }
            catch (Exception ex)
            {
                oCat.oErr.bExisteError = true;
                oCat.oErr.sMsjError = "ERROR al guardar (DBSaveObj) => " + ex.Message;
            }
        }

        public void DBDeleteObj(ref Versiones oCat)
        {
            try
            {
                if (oCat != null)
                {
                    int iId = oCat.iId;
                    tbc_Version tbMat = oDB.tbc_Version.SingleOrDefault(r => r.fi_Id == iId);

                    if (tbMat != null)   //Eliminamos
                    {
                        oDB.tbc_Version.DeleteOnSubmit(tbMat);
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

        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaVersionesBusqueda]", oArrFiltros);
            }
            catch
            {
                return new DataTable();
            }
        }

        public Versiones DBExisteDesc(string sDesc)
        {
            try
            {
                return oDB.tbc_Version.Where(r => r.fc_Descripcion.ToString().Trim()
                                                                .ToUpper() == sDesc).
                    Select(r => new Versiones()
                    {
                        iId = r.fi_Id,
                        iIdTipoAuto = r.fi_IdTipoAuto,
                        iActivo = r.fi_Activo,
                        dtFechaUltMov = r.fd_FechaUltMovimiento,
                        sFechaUltMov = r.fd_FechaUltMovimiento.Day + "/" + r.fd_FechaUltMovimiento.Month + "/" + r.fd_FechaUltMovimiento.Year,
                        sUsuario = r.fc_Usuario
                    }).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public DataTable dtGetVersionesActivas(int iIdTipoAuto)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaVersionesTipoAuto]", "@fi_IdTipoAuto", iIdTipoAuto);
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}