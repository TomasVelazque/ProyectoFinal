using CapaEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Compras
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count (*) + 1 from Compras");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    idcorrelativo = 0;
                }
            }
            return idcorrelativo;
        }

        public bool Registrar(Compras obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("sp_RegistrarCompra".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("UsuariosID",obj.oUsuarios.UsuariosID);
                    cmd.Parameters.AddWithValue("ProveedoresID",obj.pProveedores.ProveedoresID);
                    cmd.Parameters.AddWithValue("NumeroFactura",obj.NumeroFactura);
                    cmd.Parameters.AddWithValue("MontoTotal",obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleCompra",DetalleCompra);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta= Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }

            return Respuesta;
        }


        public Compras ObtenerCompra(string numero)
        {
            Compras obj = new Compras();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine(" select c.ComprasID,");
                    query.AppendLine("u.Nombre,");
                    query.AppendLine("pr.Nombre, pr.CUIT,");
                    query.AppendLine("c.NumeroFactura, c.MontoTotal, convert(char(10), c.FechaCreacion, 103)[FechaCreacion]");
                    query.AppendLine("from Compras c");
                    query.AppendLine("inner join Usuarios u on u.UsuariosID = c.UsuariosID");
                    query.AppendLine("inner join Proveedores pr on pr.ProveedoresID = c.ProveedoresID");
                    query.AppendLine("where c.NumeroFactura = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            obj = new Compras()
                            {
                                ComprasID = Convert.ToInt32(dr["ComprasID"]),
                                oUsuarios = new Usuarios() { Nombre = dr["Nombre"].ToString() },
                                pProveedores = new Proveedores() { Nombre = dr["Nombre"].ToString(), CUIT = dr["CUIT"].ToString()},
                                NumeroFactura = dr["NumeroFactura"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaCreacion = dr["FechaCreacion"].ToString(),
                            };
                        }
                    }
                }
                catch
                {
                    obj = new Compras();
                }
            }


            return obj;
        }

        public List<DetalleCompra> ObtenerDetalleCompra(int comprasid)
        {
            List<DetalleCompra> oLista = new List<DetalleCompra>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select a.Detalle,dc.PrecioCompra,dc.Cantidad,dc.MontoTotal from DetalleCompra dc");
                    query.AppendLine("inner join Articulos a on a.ArticulosID = dc.ArticuloID");
                    query.AppendLine("where dc.ComprasID = @comprasid");
            
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@comprasid", comprasid);
                    cmd.CommandType = System.Data.CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            oLista.Add(new DetalleCompra()
                            {
                                
                                oArticulos = new Articulos() { Detalle = dr["Detalle"].ToString() },
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),

                            });
                        }
                    }
                }
                catch
                {
                    oLista = new List<DetalleCompra>();
                }
                return oLista;
            }
        }

    }
}
