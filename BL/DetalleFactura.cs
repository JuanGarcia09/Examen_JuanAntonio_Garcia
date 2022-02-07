using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ML;

namespace BL
{
    public class DetalleFactura
    {
        public static ML.Result Add(int idFactura, int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "FacturaDetalleAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];
                        collection[0] = new SqlParameter("IdFactura", SqlDbType.Int);
                        collection[0].Value = idFactura;
                        collection[1] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[1].Value = idProducto;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al registrar información";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetByIdFactura(int idFactura)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "DetalleGetByIdFactura";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdFactura", SqlDbType.Int);
                        collection[0].Value = idFactura;

                        cmd.Parameters.AddRange(collection);

                        DataTable tableFactura = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tableFactura);

                        result.Objects = new List<object>();

                        if (tableFactura.Rows.Count > 0)
                        {
                            foreach (DataRow row in tableFactura.Rows)
                            {
                                ML.DetalleFactura detalleFactura = new ML.DetalleFactura();
                                detalleFactura.IdDetalleFactura = int.Parse(row[0].ToString());
                                detalleFactura.Producto = new ML.Producto();
                                detalleFactura.Producto.IdProducto = int.Parse(row[1].ToString());
                                detalleFactura.Producto.Nombre = row[2].ToString();
                                detalleFactura.Producto.Marca = row[3].ToString();
                                detalleFactura.Producto.Costo = Decimal.Parse(row[4].ToString());
                                detalleFactura.Producto.PrecioVenta = Decimal.Parse(row[5].ToString());

                                result.Objects.Add(detalleFactura);
                                result.Object = detalleFactura.IdDetalleFactura;
                            }                           
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al obtener la información!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result ProductoNoAsignadoByIdDetalle(int idDetalle) {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    var query = "ProductoNoAsignadoByIdFactura";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdFactura", SqlDbType.Int);
                        collection[0].Value = idDetalle;

                        cmd.Parameters.AddRange(collection);

                        DataTable tableProductosNoAsignados = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tableProductosNoAsignados);

                        result.Objects = new List<object>();

                        if (tableProductosNoAsignados.Rows.Count > 0)
                        {
                            foreach (DataRow row in tableProductosNoAsignados.Rows)
                            {
                                ML.DetalleFactura detalleFactura = new ML.DetalleFactura();

                                detalleFactura.Producto = new ML.Producto();
                                detalleFactura.Producto.IdProducto = int.Parse(row[0].ToString());
                                detalleFactura.Producto.Nombre = row[1].ToString();
                                detalleFactura.Producto.Marca = row[2].ToString();
                                detalleFactura.Producto.Costo = decimal.Parse(row[3].ToString());
                                detalleFactura.Producto.PrecioVenta = decimal.Parse(row[4].ToString());

                                result.Objects.Add(detalleFactura);
                            }

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "La factura, ya tiene todos los productos asignados";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result ProductoFacturaAdd(int idFactura, int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoFacturaAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("IdFactura", SqlDbType.Int);
                        collection[0].Value = idFactura;

                        collection[1] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[1].Value = idProducto;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al agregar producto al cliente";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result ProductoDeleteByIdDetalleFactura(int idDetalleFactura)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoDeleteByIdDetalleFactura";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdDetalle", SqlDbType.Int);
                        collection[0].Value = idDetalleFactura;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al eliminar el producto";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
