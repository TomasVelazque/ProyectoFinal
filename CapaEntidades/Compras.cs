using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Compras
    {
        public int ComprasID { get; set; }
        public Usuarios oUsuarios { get; set; }
        public Proveedores pProveedores { get; set;}
        public string NumeroFactura { get; set; }
        public decimal MontoTotal { get; set; }
        public List<DetalleCompra> oDetalleCompra { get; set; }
        public string FechaCreacion { get; set; }
    }
}
