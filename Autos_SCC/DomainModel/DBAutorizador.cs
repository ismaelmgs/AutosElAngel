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
        public bool DBObjExists(int iId)
        {
            DataTable dtTemp = oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAdministradorId]", "@fi_Id", iId);
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
        public Autorizador DBGetObj(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaAdministradorId]", "@fi_Id", iId).AsEnumerable().Select(r => new Autorizador()
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
    }
}