using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Permisos
    {
        public List<Permisos> Listar(int UsuariosID)
        {
            List<Permisos> lista = new List<Permisos>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.RolesID,p.NombreMenu from Permisos p");
                    query.AppendLine("inner join Roles r on r.RolesID = p.RolesID");
                    query.AppendLine("inner join Usuarios u on u.RolesID = r.RolesID");
                    query.AppendLine("where u.UsuariosID = @UsuariosID");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@UsuariosID", UsuariosID);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permisos()
                            {
                                oRoles = new Roles() { RolesID = Convert.ToInt32(dr["RolesID"])},
                                NombreMenu = dr["NombreMenu"].ToString(),
                            });
                        }
                    }
                }
                catch
                {
                    lista = new List<Permisos>();
                }
            }
            return lista;
        }
    }
}
