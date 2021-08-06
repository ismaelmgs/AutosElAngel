using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using NucleoBase.Core;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBParametro : DBBase
    {
        public Parametro ConsultaParametro(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaParametro]", "@fi_Id", iId).AsEnumerable().Select( r => new Parametro()
                    {
                        iId = r["fi_Id"].S().I(),
                        sDescripcion = r["fc_Descripcion"].S(),
                        sValor = r["fc_Valor"].S(),
                        iActivo = r["fi_Activo"].S().I(),
                        sUsuario = r["fc_Usuario"].S(),
                        dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                        sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                    }).First();
            }
            catch
            {
                return new Parametro();
            }
        }
    }
}