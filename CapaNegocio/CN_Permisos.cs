using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaNegocio
{
    public class CN_Permisos
    {
        private CD_Permisos objcd_permiso = new CD_Permisos();

        public List<Permisos> Listar(int UsuariosID)
        {
            return objcd_permiso.Listar(UsuariosID);
        }
    }
}
