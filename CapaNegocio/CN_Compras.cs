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
    public class CN_Compras
    {
        private CD_Compras objcd_Compras = new CD_Compras();

        public int ObtenerCorrelativo()
        {
            return objcd_Compras.ObtenerCorrelativo();
        }

        public bool Registrar(Compras obj, DataTable DetalleCompra ,out string Mensaje)
        {
            return objcd_Compras.Registrar(obj, DetalleCompra, out Mensaje);           
        }

        public Compras ObtenerCompra(string numero)
        {
            Compras oCompra = objcd_Compras.ObtenerCompra(numero); 

            if (oCompra.ComprasID != 0)
            {
                List<DetalleCompra> oDetalleCompra = objcd_Compras.ObtenerDetalleCompra(oCompra.ComprasID);

                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }

    }
}
