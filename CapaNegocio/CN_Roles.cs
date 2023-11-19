using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Roles
    {
        private CD_Roles objcd_rol = new CD_Roles();

        public List<Roles> Listar()
        {
            return objcd_rol.Listar();
        }
    }
}
