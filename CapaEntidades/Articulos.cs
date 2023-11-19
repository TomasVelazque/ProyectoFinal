using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Articulos
    {
        public int ArticulosID { get; set;}
        public string Detalle { get; set; }
        public string Presentacion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta{ get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        public string FechaCreacion { get; set; }

        public string Codigo { get; set; }


    }
}
