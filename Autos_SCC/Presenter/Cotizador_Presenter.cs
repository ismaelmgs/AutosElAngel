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
    public class Cotizador_Presenter: BasePresenter<IViewCotizador>
    {
        private readonly DBCotizador oIGestCat;

        public Cotizador_Presenter(IViewCotizador oView, DBCotizador oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eCalculaCotizacion += eCalculaCotizacion_Presenter;
            oIView.eAgregaPagoIndividual += eAgregaPagoIndividual_Presenter;
            CreaEstructuraTabla();
        }

        private void CreaEstructuraTabla()
        {
            if (oIView.dtPagosIndividuales == null)
            {
                oIView.dTasaPreferencial = 0;

                oIView.dtPagosIndividuales = new DataTable();
                oIView.dtPagosIndividuales.Columns.Add("Importe");
                oIView.dtPagosIndividuales.Columns.Add("Fecha", typeof(DateTime));
            }
        }

        private void eCalculaCotizacion_Presenter(object sender, EventArgs e)
        {
            oIView.oParametro = new DBParametro().ConsultaParametro(1);
            oIView.dtHeader = dtGetHeaderCotizacion();
        }

        private void eAgregaPagoIndividual_Presenter(object sender, EventArgs e)
        {
            DataRow row = oIView.dtPagosIndividuales.NewRow();
            row["Importe"] = oIView.oPagoI.dImporte.S();
            row["Fecha"] = oIView.oPagoI.dtFechaPago;

            oIView.dtPagosIndividuales.Rows.Add(row);
        }

        private DataTable dtGetHeaderCotizacion()
        {
            try
            {
                DataTable dtPlazo = new DBPlazo().dtObjsCat;
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Plazo");
                dt.Columns.Add("DescPlazo");
                dt.Columns.Add("Importe", typeof(double));
                dt.Columns.Add("Intereses",typeof(double));
                dt.Columns.Add("TotalPagar", typeof(double));
                dt.Columns.Add("PrimerPago", typeof(double));
                dt.Columns.Add("Ahorro", typeof(double));

                Cotizacion oCot = oIView.oCotizacion;
                double dTasa = 0;

                if(oIView.dTasaPreferencial != 0)
                    dTasa = oIView.dTasaPreferencial;
                else
                    dTasa = oIView.oParametro.sValor.I().Db();

                double dImporte = (oCot.dPrecio - oCot.dEnganche).Db();
                double dPorcentajeI = dTasa / 100;
                double dInteresMensual = dImporte * dPorcentajeI;

                foreach (DataRow row in dtPlazo.Rows)
                {
                    DataRow drFila = dt.NewRow();
                    int iPlazo = row["sDescripcion"].S().I();

                    double dTotalPagar = 0;
                    double dTotalPagarOri = 0;

                    dTotalPagar = dImporte + (dInteresMensual * iPlazo);
                    dTotalPagarOri = dTotalPagar;
                    if (oIView.dPagosIndividuales != 0)
                    {
                        dTotalPagar = dTotalPagar - oIView.dPagosIndividuales;
                    }
                    
                    drFila["Plazo"] = iPlazo.S();
                    drFila["DescPlazo"] = iPlazo.S() + " Meses";
                    drFila["Importe"] = Math.Round(dImporte, 2);
                    drFila["Intereses"] = Math.Round((dInteresMensual * iPlazo), 2);
                    drFila["TotalPagar"] = dTotalPagarOri;
                    drFila["PrimerPago"] = Math.Round((dTotalPagar / iPlazo), 2);
                    drFila["Ahorro"] = Math.Round(((dTotalPagar / iPlazo) * (0.10)), 2);
                    
                    dt.Rows.Add(drFila);
                }

                return dt;
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}