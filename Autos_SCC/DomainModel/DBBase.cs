using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;

namespace Autos_SCC.DomainModel
{
    public abstract class DBBase: IDisposable
    {
        protected AAPDataContextDataContext oDB = new AAPDataContextDataContext();
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;
        
        public DBBase()
        {
            oDB_SP.sConexionSQL = Globales.GetConfigConnection("SqlDefault");
        }
        ~DBBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            try
            {
                if (!bDisposed)
                {
                    if (bDisposing)
                    {
                        oDB.Dispose();
                    }
                    bDisposed = true;
                }
            }
            catch
            {
            }
        }
    }
}