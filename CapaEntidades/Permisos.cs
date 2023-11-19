using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Permisos
    {
        public int PermisosID { get; set; }
        public Roles oRoles { get; set; }
        public string NombreMenu { get; set; }
        public string FechaCreacion { get; set; }

    }
}
