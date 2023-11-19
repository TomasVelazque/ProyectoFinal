using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidades;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Collections;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuarios> Listar()
        {
            List<Usuarios> lista = new List<Usuarios>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.UsuariosID, u.Nombre, u.Email, u.Clave, u.Celular, u.Estado, r.RolesID, r.Descripcion FROM Usuarios u");
                    query.AppendLine("inner join Roles r on r.RolesID = u.RolesID");



                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios()
                            {
                                UsuariosID = Convert.ToInt32(dr["UsuariosID"]),
                                Nombre = dr["Nombre"].ToString(),
                                Email = dr["Email"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Celular = dr["Celular"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRoles = new Roles() { RolesID = Convert.ToInt32(dr["RolesID"]), Descripcion = dr["Descripcion"].ToString()}

                            }) ;
                        }
                    }
                }
                catch
                {
                    lista = new List<Usuarios>();
                }
            }
            return lista;
        }


        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {



                    //              CREATE PROC SP_REGISTRARUSUARIO(


                    //@Nombre varchar(50),
                    //@Email varchar(50),
                    //@Clave varchar(50),
                    //@Celular varchar(50),
                    //@RolesID int,
                    //@Estado bit,
                    //@IDUsuarioResultado int output,
                    //@Mensaje varchar(500) output
                    //)

                    //as


                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);
                    //PARAMETROS DE ENTRADA
                    cmd.Parameters.AddWithValue("Nombre",obj.Nombre);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Celular", obj.Celular);
                    cmd.Parameters.AddWithValue("RolesID", obj.oRoles.RolesID);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //PARAMETROS DE SALIDA
                    cmd.Parameters.Add("IDUsuarioResultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IDUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;

            }
            return idusuariogenerado;

        }


        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", oconexion);
                    //PARAMETROS DE ENTRADA
                    cmd.Parameters.AddWithValue("UsuariosID", obj.UsuariosID);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Email", obj.Email);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Celular", obj.Celular);
                    cmd.Parameters.AddWithValue("RolesID", obj.oRoles.RolesID);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    //PARAMETROS DE SALIDA
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }
            return respuesta;

        }

        public bool ELiminar(Usuarios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", oconexion);
                    //PARAMETROS DE ENTRADA
                    cmd.Parameters.AddWithValue("UsuariosID", obj.UsuariosID);
                    //PARAMETROS DE SALIDA
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;

            }
            return respuesta;

        }


    }
}
