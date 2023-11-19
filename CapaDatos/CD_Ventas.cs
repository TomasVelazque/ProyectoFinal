using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_Ventas
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count (*) + 1 from Ventas");
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

        public bool RestarStock (int ArticulosID, int Cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Articulos set Stock = Stock - @Cantidad where ArticulosID = @ArticulosID");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@Cantidad",Cantidad);
                    cmd.Parameters.AddWithValue("@ArticulosID", ArticulosID);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool SumarStock (int ArticulosID, int Cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Articulos set Stock = Stock + @Cantidad where ArticulosID = @ArticulosID");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@ArticulosID", ArticulosID);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Registrar(Ventas obj, DataTable DetalleVenta, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = String.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("sp_RegistarVentas".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("UsuariosID", obj.oUsuarios.UsuariosID);
                    cmd.Parameters.AddWithValue("NumeroFactura", obj.NumeroFactura);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("NombreUsuario", obj.NombreUsuario);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleVenta",DetalleVenta);

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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

        public Ventas ObtenerVenta(string numero)
        {
            Ventas obj = new Ventas();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select v.VentasID, v.NombreUsuario,");
                    query.AppendLine("v.NumeroFactura,");
                    query.AppendLine("v.MontoPago,v.MontoCambio,v.MontoTotal,");
                    query.AppendLine("CONVERT(char(10),v.FechaCreacion,103)[FechaCreacion]");
                    query.AppendLine("from Ventas v");
                    query.AppendLine("inner join Usuarios u on u.UsuariosID = v.UsuariosID");
                    query.AppendLine("where v.NumeroFactura = @numero");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Ventas()
                            {
                                VentasID = int.Parse(dr["VentasID"].ToString()),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                NumeroFactura = dr["NumeroFactura"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaCreacion = dr["FechaCreacion"].ToString()
                            };
                        }
                    }

                }
                catch
                {
                    obj = new Ventas();
                }
            }
            return obj;
        }


        public List<DetalleVenta> ObtenerDetalleVenta(int VentaID)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select a.Detalle,dv.PrecioVenta,dv.Cantidad,dv.SubTotal from DetalleVenta dv");
                    query.AppendLine("inner join Articulos a on a.ArticulosID = dv.ArticuloID");
                    query.AppendLine("where dv.VentasID = @VentaID");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@VentaID", VentaID);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new DetalleVenta(){

                                oArticulos = new Articulos() { Detalle = dr["Detalle"].ToString()},
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString()),
                            });
                        }
                    }
                }
                catch
                {
                    oLista = new List<DetalleVenta>();
                }
            }
                return oLista;
        }
    } 
}
