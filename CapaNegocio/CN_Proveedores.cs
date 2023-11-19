using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Proveedores
    {
        private CD_Proveedores objcd_Proveedores = new CD_Proveedores();

        public List<Proveedores> Listar()
        {
            return objcd_Proveedores.Listar();
        }

        public int Registrar(Proveedores obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario que el nombre del Proveedor no este vacio >: \n";
            }

            if (obj.CUIT == "")
            {
                Mensaje += "Es necesario que el CUIT del Proveedor no este vacio >: \n";
            }

            if (obj.Email == "")
            {
                Mensaje += "Es necesario que el email del Proveedor no este vacio >: \n";
            }

            if (obj.Celular == "")
            {
                Mensaje += "Es necesario que el celular del Proveedor no este vacio >: \n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Proveedores.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Proveedores obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario que el nombre del Proveedor no este vacio >: \n";
            }

            if (obj.CUIT == "")
            {
                Mensaje += "Es necesario que la clave del Proveedor no este vacio >: \n";
            }

            if (obj.Email == "")
            {
                Mensaje += "Es necesario que el email del Proveedor no este vacio >: \n";
            }

            if (obj.Celular == "")
            {
                Mensaje += "Es necesario que el celular del Proveedor no este vacio >: \n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Proveedores.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Proveedores obj, out string Mensaje)
        {
            return objcd_Proveedores.ELiminar(obj, out Mensaje);
        }
    }
}
