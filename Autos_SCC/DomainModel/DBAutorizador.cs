using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using Autos_SCC.Interfaces;
using Autos_SCC.Clases;
using System.Data;
using NucleoBase.Core;

namespace Autos_SCC.DomainModel
{
    public class DBAutorizador : DBBase
    {
        public DataTable dtObjCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAdministradores]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }
        public DataTable dtObjSuc
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaSucursales]", "");
                }
                catch 
                {
                    return new DataTable();
                }
            }
        }
        public DataTable dtObjPer
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaPerfiles]", "");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }
        public DataTable DBSearchAdministradores(object[] oArra)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaFiltroAdministradores]", oArra);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
        public bool DBObjExists(int iId, int iIdSuc)
        {
            DataTable dtTemp = oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAdministradorId]", "@fi_Id", iId, "@fi_IdSucursal", iIdSuc);
            Autorizador oTempEjecut = null;

            if (dtTemp.Rows.Count > 0)
            {
                oTempEjecut = dtTemp.AsEnumerable().Select(r => new Autorizador()
                {
                    fi_id = r["fi_Id"].S().I(),
                    PriNombre = r["fc_PrimNombre"].S(),
                    SegNombre = r["fc_SegNombre"].S(),
                    PatApellido = r["fc_PrimApellido"].S(),
                    MatApellido = r["fc_SegApellido"].S(),
                    fi_IdPerfil = r["fi_IdPerfil"].S().I(),
                    NomPerfil = r["fc_Descripcion"].S(),
                    fi_IdSucursal = r["fi_IdSucursal"].S().I(),
                    NomSucurlal = r["fc_Descripcion_Sucursal"].S(),
                }).First();
            }

            return oTempEjecut.fi_id > 0 ? true : false;
        }
        public Autorizador DBGetObj(int iId, int iIdSuc)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAdministradorId]", "@fi_Id", iId, "@fi_IdSucursal", iIdSuc).AsEnumerable().Select(r => new Autorizador()
                {
                    fi_id = r["fi_Id"].S().I(),
                    PriNombre = r["fc_PrimNombre"].S(),
                    SegNombre = r["fc_SegNombre"].S(),
                    PatApellido = r["fc_PrimApellido"].S(),
                    MatApellido = r["fc_SegApellido"].S(),
                    fi_IdPerfil = r["fi_IdPerfil"].S().I(),
                    NomPerfil = r["fc_Descripcion"].S(),
                    fi_IdSucursal = r["fi_IdSucursal"].S().I(),
                    NomSucurlal = r["fc_Descripcion_Sucursal"].S(),
                }).First();
            }
            catch
            {
                return null;
            }
        }
        public void DBSaveObj(ref Autorizador oEjecut)
        {
            try
            {
                object vNew = null;

                oEjecut.oErr.bExisteError = false;
                oEjecut.oErr.sMsjError = "Guardado exitoso";

                if (oEjecut != null)
                {
                    if (DBGetObj(oEjecut.fi_id,oEjecut.fi_IdSucursal) != null)
                    { 
                        ////Actualiza
                        vNew = oDB_SP.EjecutarValor("[Catalogos].[spU_ActualizaPerfilUsuario]", "@fi_Id", oEjecut.fi_id,
                                                                                                "@fi_IdSucursal", oEjecut.fi_IdSucursal,
                                                                                                "@fi_IdPerfil", oEjecut.fi_IdPerfil);

                    }
                    oEjecut.fi_id = vNew != null ? vNew.S().I() : -1;
                }
            }
            catch (Exception ex)
            {
                oEjecut.oErr.bExisteError = true;
                oEjecut.oErr.sMsjError = "ERROR al guardar (DBSaveObj) => " + ex.Message;
            }
        }
    }
}