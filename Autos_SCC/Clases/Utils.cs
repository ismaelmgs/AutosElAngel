using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using Autos_SCC.DomainModel;
using NucleoBase.Core;

namespace Autos_SCC.Clases
{
    public static class Utils
    {
        public static DataTable ConvertListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static void CambiaEstatus(int iIdCredito, Autos_SCC.Clases.Enumeraciones.eEstatus eStatus)
        {
            new DBUtils().DBUpdateEstatus(iIdCredito, eStatus);
        }

        public static string ObtieneFechaServidor()
        {
            try
            {
                return new DBUtils().DBGetFecha();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BuscaTexoEnGrid(GridView gv, string sTexto)
        {
            if (gv != null)
            {
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    for (int j = 0; j < gv.Columns.Count; j++)
                    {
                        if (gv.Rows[i].Cells[j].Text.S().Contains(sTexto))
                        {

                        }
                    }
                }
            }
        }

        public static DataTable CalculaCotizacion(double dTasa, decimal dPrecio, decimal dEnganche, double dPagosIndividuales, int iPlazo)
        {
            try
            {
                DataTable dtPlazo = null;

                if (iPlazo == 0)
                    dtPlazo = new DBPlazo().dtObjsCat;
                else
                {
                    dtPlazo = new DataTable();
                    dtPlazo.Columns.Add("sDescripcion");

                    DataRow row = dtPlazo.NewRow();
                    row["sDescripcion"] = iPlazo.S();

                    dtPlazo.Rows.Add(row);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Plazo");
                dt.Columns.Add("DescPlazo");
                dt.Columns.Add("Importe", typeof(double));
                dt.Columns.Add("Intereses", typeof(double));
                dt.Columns.Add("TotalPagar", typeof(double));
                dt.Columns.Add("PrimerPago", typeof(double));
                dt.Columns.Add("Ahorro", typeof(double));

                double dImporte = (dPrecio - dEnganche).Db();
                double dPorcentajeI = dTasa / 100;
                double dInteresMensual = dImporte * dPorcentajeI;
                
                foreach (DataRow row in dtPlazo.Rows)
                {
                    DataRow drFila = dt.NewRow();
                    iPlazo = row["sDescripcion"].S().I();

                    double dTotalPagar = 0;
                    double dTotalPagarOri = 0;

                    dTotalPagar = dImporte + (dInteresMensual * iPlazo);
                    dTotalPagarOri = dTotalPagar;
                    if (dPagosIndividuales != 0)
                    {
                        dTotalPagar = dTotalPagar - dPagosIndividuales;
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

        public static DataTable CalculaDetalleCotizacion(int iPlazo, double dPrimerPago)
        {
            try
            {
                DataTable dtDetalle = new DataTable();
                dtDetalle.Columns.Add("Plazo");
                dtDetalle.Columns.Add("NoPago");
                dtDetalle.Columns.Add("PagoNormal");
                dtDetalle.Columns.Add("PagoAdelantado");
                dtDetalle.Columns.Add("PagoMora");

                for (int i = 0; i < iPlazo; i++)
                {
                    DataRow row = dtDetalle.NewRow();
                    row["Plazo"] = iPlazo;
                    row["NoPago"] = (i + 1).S();
                    row["PagoNormal"] = Math.Round(dPrimerPago, 2);
                    row["PagoAdelantado"] = Math.Round((dPrimerPago * .90), 2);
                    row["PagoMora"] = Math.Round((dPrimerPago * 1.10), 2);

                    dtDetalle.Rows.Add(row);
                }

                return dtDetalle;
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}