using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Ventas objcd_Ventas = new CD_Ventas();
        public bool RestarStock(int ArticulosID, int Cantidad)
        {
            return objcd_Ventas.RestarStock(ArticulosID, Cantidad);
        }
        public bool SumarStock(int ArticulosID, int Cantidad)
        {
            return objcd_Ventas.SumarStock(ArticulosID, Cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_Ventas.ObtenerCorrelativo();
        }

        public bool Registrar(Ventas obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_Ventas.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public Ventas ObtenerVenta(string numero)
        {
            Ventas oVenta = objcd_Ventas.ObtenerVenta(numero);

            if (oVenta.VentasID != 0)
            {
                List<DetalleVenta> oDetalleVenta = objcd_Ventas.ObtenerDetalleVenta(oVenta.VentasID);
                oVenta.oDetalleVenta = oDetalleVenta;
            }
            return oVenta;
        }


    }
}
