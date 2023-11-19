using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public  class CD_Articulos
    {
        public List<Articulos> Listar()
        {
            List<Articulos> lista = new List<Articulos>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT ArticulosID, Detalle, Presentacion, PrecioCompra, PrecioVenta, Stock, Codigo, Estado from Articulos \n");



                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Articulos()
                            {
                                ArticulosID = Convert.ToInt32(dr["ArticulosID"]),
                                Detalle = dr["Detalle"].ToString(),
                                Presentacion = dr["Presentacion"].ToString(),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Stock = Convert.ToInt32(dr["Stock"].ToString()),
                                Codigo = dr["Codigo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch
                {
                    lista = new List<Articulos>();
                }
            }
            return lista;
        }


        public int Registrar(Articulos obj, out string Mensaje)
        {
            int idarticulogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_RegistrarArticulo", oconexion);
                    //PARAMETROS DE ENTRADA
                    cmd.Parameters.AddWithValue("Detalle", obj.Detalle);
                    cmd.Parameters.AddWithValue("Presentacion", obj.Presentacion);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    //PARAMETROS DE SALIDA
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idarticulogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();


                }
            }
            catch (Exception ex)
            {
                idarticulogenerado = 0;
                Mensaje = ex.Message;

            }
            return idarticulogenerado;

        }


        public bool Editar(Articulos obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ModificarArticulo", oconexion);
                    //PARAMETROS DE ENTRADA
                    cmd.Parameters.AddWithValue("ArticulosID", obj.ArticulosID);
                    cmd.Parameters.AddWithValue("Detalle", obj.Detalle);
                    cmd.Parameters.AddWithValue("Presentacion", obj.Presentacion);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    //PARAMETROS DE SALIDA
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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

        public bool ELiminar(Articulos obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EliminarArticulo", oconexion);
                    //PARAMETROS DE ENTRADA
                    cmd.Parameters.AddWithValue("ArticulosID", obj.ArticulosID);
                    //PARAMETROS DE SALIDA
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

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
