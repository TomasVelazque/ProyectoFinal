using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Ventas
    {
        public int VentasID { get; set; }
        public Usuarios oUsuarios { get; set; }
        public string NumeroFactura { get; set; }
        public decimal MontoPago { get; set; }
        public string NombreUsuario { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }

        public List <DetalleVenta> oDetalleVenta { get; set; }
        public string FechaCreacion { get; set; }
    }
}
