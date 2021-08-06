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
    public class DBMarca : DBBase, IDBCat<Marca>
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    List<Marca> oLst = new List<Marca>();

                    oLst = oDB.tbc_MarcaAuto.Select(r => new Marca()
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

        public Marca DBGetObj(int iId)
        {
            try
            {
                return oDB.tbc_MarcaAuto.Where(r => r.fi_Id == iId)
                                       .Select(r => new Marca()
                                       {
                                           iId = r.fi_Id,
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
                return new Marca();
            }
        }

        public bool DBObjExists(int iId)
        {
            try
            {
                return oDB.tbc_MarcaAuto.Where(r => r.fi_Id == iId).Count() > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public void DBSaveObj(ref Marca oCat)
        {
            try
            {
                if (oCat != null)
                {
                    Marca oTemp = oCat;
                    tbc_MarcaAuto tbCat = oDB.tbc_MarcaAuto.SingleOrDefault(r => r.fi_Id == oTemp.iId);

                    if (tbCat != null)   //actualizamos
                    {
                        tbCat.fc_Descripcion = oCat.sDescripcion;
                        tbCat.fi_Activo = oCat.iActivo;
                        tbCat.fd_FechaUltMovimiento = oCat.dtFechaUltMov;
                        tbCat.fc_Usuario = oCat.sUsuario;
                        oDB.SubmitChanges();
                    }
                    else
                    {
                        if (DBExisteDesc(oCat.sDescripcion.Trim().ToUpper()) == null)
                        {
                            tbCat = new tbc_MarcaAuto();
                            tbCat.fc_Descripcion = oCat.sDescripcion;
                            tbCat.fi_Activo = oCat.iActivo;
                            tbCat.fd_FechaUltMovimiento = oCat.dtFechaUltMov;
                            tbCat.fc_Usuario = oCat.sUsuario;
                            oDB.tbc_MarcaAuto.InsertOnSubmit(tbCat);
                            oDB.SubmitChanges();
                        }
                        else
                        {
                            oCat.oErr.bExisteError = true;
                            oCat.oErr.sMsjError = "Ya se encuentra una Descripción con este nombre: " + oCat.sDescripcion.ToUpper();
                        }
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

        public void DBDeleteObj(ref Marca oCat)
        {
            try
            {
                if (oCat != null)
                {
                    int iId = oCat.iId;
                    tbc_MarcaAuto tbMat = oDB.tbc_MarcaAuto.SingleOrDefault(r => r.fi_Id == iId);

                    if (tbMat != null)   //Eliminamos
                    {
                        oDB.tbc_MarcaAuto.DeleteOnSubmit(tbMat);
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaMarcasBusqueda]", oArrFiltros);
            }
            catch
            {
                return new DataTable();
            }
        }

        public Marca DBExisteDesc(string sDesc)
        {
            try
            {
                return oDB.tbc_MarcaAuto.Where(r => r.fc_Descripcion.ToString().Trim()
                                                                .ToUpper() == sDesc).
                    Select(r => new Marca()
                    {
                        iId = r.fi_Id,
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

    }
}