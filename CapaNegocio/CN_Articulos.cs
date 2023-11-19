using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Articulos
    {
        private CD_Articulos objcd_Articulos = new CD_Articulos();

        public List<Articulos> Listar()
        {
            return objcd_Articulos.Listar();
        }

        public int Registrar(Articulos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Detalle == "")
            {
                Mensaje += "Es necesario que el detalle del articulo no este vacio >: \n";
            }

            if (obj.Presentacion == "")
            {
                Mensaje += "Es necesario que la presentacion del articulo no este vacio >: \n";
            }

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario que el codigo del articulo no este vacio >: \n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Articulos.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Articulos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Detalle == "")
            {
                Mensaje += "Es necesario que el detalle del articulo no este vacio >: \n";
            }

            if (obj.Presentacion == "")
            {
                Mensaje += "Es necesario que la presentacion del articulo no este vacio >: \n";
            }

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario que el codigo del articulo no este vacio >: \n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Articulos.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Articulos obj, out string Mensaje)
        {
            return objcd_Articulos.ELiminar(obj, out Mensaje);
        }

    }
}
