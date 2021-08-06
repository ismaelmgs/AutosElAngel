using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Interfaces;
using Autos_SCC.DomainModel;

namespace Autos_SCC.Presenter
{
    public class Cobranza_Presenter : BasePresenter<IViewCobranza>
    {
        private readonly DBCobranza oIGestCat;

        public Cobranza_Presenter(IViewCobranza oView, DBCobranza oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetCreditos += eGetCreditos_Presenter;

        }

        private void eGetCreditos_Presenter(object sender, EventArgs e)
        {
            oIView.dtCreditos = oIGestCat.dtGetCreditosSucursal(oIView.iIdSucursal);
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadSucursales(new DBSucursales().dtObj);
        }
    }
}