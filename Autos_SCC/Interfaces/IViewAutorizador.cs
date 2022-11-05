using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Autos_SCC.Interfaces
{
    public interface IViewAutorizador : IBaseView
    {
        object[] oArrFiltros { get; }

        void LoadUsuarios(DataTable dtUsuario);
        void LoadSucursales(DataTable dtSucursales);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eGetUsuarios;
        event EventHandler eGetSucursales;
        event EventHandler eSearchAdministradores;
    }
}
