using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objcd_usuarios = new CD_Usuarios();

        public List<Usuarios> Listar()
        {
            return objcd_usuarios.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario que el nombre del usuario no este vacio >: \n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesario que la clave del usuario no este vacio >: \n";
            }

            if (obj.Email == "")
            {
                Mensaje += "Es necesario que el email del usuario no este vacio >: \n";
            }

            if (obj.Celular == "")
            {
                Mensaje += "Es necesario que el celular del usuario no este vacio >: \n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_usuarios.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario que el nombre del usuario no este vacio >: \n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesario que la clave del usuario no este vacio >: \n";
            }

            if (obj.Email == "")
            {
                Mensaje += "Es necesario que el email del usuario no este vacio >: \n";
            }

            if (obj.Celular == "")
            {
                Mensaje += "Es necesario que el celular del usuario no este vacio >: \n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_usuarios.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Usuarios obj, out string Mensaje)
        {
            return objcd_usuarios.ELiminar(obj, out Mensaje);
        }
    }
}
