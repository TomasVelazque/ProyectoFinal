using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Usuarios
    {
        public int UsuariosID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set;}
        public string Clave { get; set; }
        public string Celular { get; set; }
        public Roles oRoles { get; set; }
        public bool Estado { get; set; }
        public string FechaCreacion { get; set; }

       
    }
}
