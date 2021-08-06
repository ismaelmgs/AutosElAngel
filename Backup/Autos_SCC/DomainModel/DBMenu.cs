using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NucleoBase.Core;

namespace Autos_SCC.DomainModel
{
    public class DBMenu : DBBase
    {
        public DataTable ObtieneItemsMenu(string sPerfil)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaItemsMenuUsuario]", "@iIdPerfil", sPerfil.I());

                #region COMENTADO
                //DataTable dt = new DataTable();
                //dt.Columns.Add("fiIdModulo");
                //dt.Columns.Add("fcDescModulo");
                //dt.Columns.Add("fcURL");
                //dt.Columns.Add("fiUrlIco");
                //dt.Columns.Add("fiParentId");
                
                //DataRow dr = dt.NewRow();
                //dr["fiIdModulo"] = "1";
                //dr["fcDescModulo"] = "Inicio";
                //dr["fcURL"] = "";
                //dr["fiUrlIco"] = "";
                //dr["fiParentId"] = DBNull.Value;

                //DataRow dr2 = dt.NewRow();
                //dr2["fiIdModulo"] = "2";
                //dr2["fcDescModulo"] = "Cotizador";
                //dr2["fcURL"] = "";
                //dr2["fiUrlIco"] = "";
                //dr2["fiParentId"] = "1";

                //DataRow dr3 = dt.NewRow();
                //dr3["fiIdModulo"] = "3";
                //dr3["fcDescModulo"] = "Datos de clientes";
                //dr3["fcURL"] = "";
                //dr3["fiUrlIco"] = "";
                //dr3["fiParentId"] = "1";

                //DataRow dr4 = dt.NewRow();
                //dr4["fiIdModulo"] = "4";
                //dr4["fcDescModulo"] = "Formalizacion";
                //dr4["fcURL"] = "";
                //dr4["fiUrlIco"] = "";
                //dr4["fiParentId"] = "1";

                //dt.Rows.Add(dr);
                //dt.Rows.Add(dr2);
                //dt.Rows.Add(dr3);
                //dt.Rows.Add(dr4);

                //return dt;
                #endregion
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}