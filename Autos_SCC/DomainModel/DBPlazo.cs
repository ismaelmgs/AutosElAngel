using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.Objetos;
using System.Data;
using Autos_SCC.Clases;

namespace Autos_SCC.DomainModel
{
    public class DBPlazo : DBBase, IDBCat<Plazo>
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    List<Plazo> oLst = new List<Plazo>();

                    oLst = oDB.tbc_Plazo.Where(r => r.fi_Activo == 1).Select(r => new Plazo()
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

        public Plazo DBGetObj(int iId)
        {
            try
            {
                return oDB.tbc_Plazo.Where(r => r.fi_Id == iId)
                                       .Select(r => new Plazo()
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
                return new Plazo();
            }
        }

        public bool DBObjExists(int iId)
        {
            try
            {
                return oDB.tbc_Plazo.Where(r => r.fi_Id == iId).Count() > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public void DBSaveObj(ref Plazo oCat)
        {
            try
            {
                if (oCat != null)
                {
                    Plazo oTemp = oCat;
                    tbc_Plazo tbCat = oDB.tbc_Plazo.SingleOrDefault(r => r.fi_Id == oTemp.iId);

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
                            tbCat = new tbc_Plazo ();
                            tbCat.fc_Descripcion = oCat.sDescripcion;
                            tbCat.fi_Activo = oCat.iActivo;
                            tbCat.fd_FechaUltMovimiento = oCat.dtFechaUltMov;
                            tbCat.fc_Usuario = oCat.sUsuario;
                            oDB.tbc_Plazo.InsertOnSubmit(tbCat);
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

        public void DBDeleteObj(ref Plazo oCat)
        {
            try
            {
                if (oCat != null)
                {
                    int iId = oCat.iId;
                    tbc_Plazo tbMat = oDB.tbc_Plazo.SingleOrDefault(r => r.fi_Id == iId);

                    if (tbMat != null)   //Eliminamos
                    {
                        oDB.tbc_Plazo.DeleteOnSubmit(tbMat);
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

        public List<Plazo> DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                //DataTable dtTemp = oDB_SP.EjecutarDT("[Catalogos].[spS_FiltrosPonentes]", oArrFiltros);
                List<Plazo> oLst = new List<Plazo>();

                //if (dtTemp.Rows.Count > 0)
                //{
                //    oLst = dtTemp.AsEnumerable().Select(r => new Ponente()
                //    {
                //        iId = r["fiId"].S().I(),
                //        iParte = r["fiParte"].S().I(),
                //        sDescripcion = r["fcDescripcion"].S(),
                //        iOrdenVisualizar = r["fiOrdenVisualizacion"].S().I(),
                //        iActivo = r["fiActivo"].S().I(),
                //        sLetraInicial = r["fiLetraIni"].S(),
                //        sUsuario = r["fcUsuario"].S(),
                //        dtFechaUltMov = r["fdFechaUltMovimiento"].Dt(),
                //        sFechaUltMov = r["fdFechaUltMovimiento"].Dt().Day.ToString().PadLeft(2, '0') + "/" + r["fdFechaUltMovimiento"].Dt().Month.ToString().PadLeft(2, '0')
                //                                                                                     + "/" + r["fdFechaUltMovimiento"].Dt().Year,
                //    }).ToList();
                //}

                return oLst;
            }
            catch
            {
                return new List<Plazo>();
            }
        }

        public Plazo DBExisteDesc(string sDesc)
        {
            try
            {
                return oDB.tbc_Plazo.Where(r => r.fc_Descripcion.ToString().Trim()
                                                                .ToUpper() == sDesc).
                    Select(r => new Plazo()
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