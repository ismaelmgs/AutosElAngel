using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Autos_SCC.Objetos;

namespace Autos_SCC.Interfaces
{
    public interface IViewAutorizador : IBaseView
    {
        object[] oArrFiltros { get; }
        Autorizador oGetSetObjSelection { get; set; }
        Autorizador oAutorizador { get; set; }

        void LoadUsuarios(DataTable dtUsuario);
        void LoadSucursales(DataTable dtSucursales);
        void LoadPerfiles(DataTable dtPerfiles);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eGetUsuarios;
        event EventHandler eGetSucursales;
        event EventHandler eSearchAdministradores;
        event EventHandler eGetPerfiles;
    }
}
