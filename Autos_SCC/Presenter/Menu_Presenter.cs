using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.DomainModel;
using Autos_SCC.Interfaces;

namespace Autos_SCC.Presenter
{
    public class Menu_Presenter: BasePresenter<IViewMenu>
    {
        private readonly DBMenu oIGestCat;

        public Menu_Presenter(IViewMenu oView, DBMenu oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            LoadObjects();
        }

        public void LoadObjects()
        {
            oIView.LoadObjects(oIGestCat.ObtieneItemsMenu(oIView.sUsuario));
        }
    }
}