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
    }
}