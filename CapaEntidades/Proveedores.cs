using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Proveedores
    {
        public int ProveedoresID { get; set; }
        public string Nombre { get; set; }
        public string CUIT { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public bool Estado { get; set; }
        public string FechaCreacion { get; set; }
    }
}
