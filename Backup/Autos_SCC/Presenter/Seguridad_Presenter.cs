using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;
using System.Data;
using Autos_SCC.Objetos;
using NucleoBase.Core;

namespace Autos_SCC.Presenter
{
    public class Seguridad_Presenter : BasePresenter<IViewSeguridad>
    {
        private readonly DBSeguridad oIGestCat;

        public Seguridad_Presenter(IViewSeguridad oView, DBSeguridad oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetUsuario += eGetUsuario_Presenter;
        }

        private void eGetUsuario_Presenter(object sender, EventArgs e)
        {
            DataTable dt = oIGestCat.DBGetObtieneUsuario(oIView.oArrFiltros);
            DataUserIndetity oUs = new DataUserIndetity();

            if (dt.Rows.Count == 0)
            {
                oUs.bEncontrado = false;
                oUs.sEstatus = "El usuario no se encontró, favor de verificar";
            }
            else if (dt.Rows.Count == 1)
            {
                oUs.bEncontrado = true;

                if (dt.Rows[0]["Valida"].S() == "0")
                {
                    oUs.sEstatus = "La contraseña es incorrecta, favor de verificar";
                }
                else
                {
                    if (dt.Rows[0]["fi_Estatus"].S() == "0")
                        oUs.sEstatus = "El usuario se encuentra deshabilitado, favor de verificar";
                }

                oUs.sPerfil = dt.Rows[0]["fc_Perfil"].S();
                oUs.sNombre = (dt.Rows[0]["fc_PrimNombre"].S() + " "+ dt.Rows[0]["fc_SegNombre"].S()).Replace("  ", " ");
                oUs.sApellidos = (dt.Rows[0]["fc_PrimApellido"].S() + " " + dt.Rows[0]["fc_SegApellido"].S()).Replace("  ", " ");
                oUs.sUser = oIView.oArrFiltros[1].S();
            }

            oIView.oUser = oUs;
        }
    }
}