using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = producto.Nombre;
                        collection[1] = new SqlParameter("Marca", SqlDbType.VarChar);
                        collection[1].Value = producto.Marca;
                        collection[2] = new SqlParameter("Costo", SqlDbType.Decimal);
                        collection[2].Value = producto.Costo;
                        collection[3] = new SqlParameter("PrecioVenta", SqlDbType.Decimal);
                        collection[3].Value = producto.PrecioVenta;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();
                        int rowsAfected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (rowsAfected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al agregar información!";
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

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[5];

                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = producto.IdProducto;
                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = producto.Nombre;
                        collection[2] = new SqlParameter("Marca", SqlDbType.VarChar);
                        collection[2].Value = producto.Marca;
                        collection[3] = new SqlParameter("Costo", SqlDbType.Decimal);
                        collection[3].Value = producto.Costo;
                        collection[4] = new SqlParameter("PrecioVenta", SqlDbType.Decimal);
                        collection[4].Value = producto.PrecioVenta;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();
                        int rowsAfected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (rowsAfected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al actualizar información!";
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

        public static ML.Result Delete(int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = idProducto;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();
                        int rowsAfected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (rowsAfected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al eliminar información!";
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tablaProducto = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaProducto);

                        result.Objects = new List<object>();

                        if (tablaProducto.Rows.Count > 0)
                        {
                            foreach (DataRow row in tablaProducto.Rows)
                            {
                                ML.Producto producto = new ML.Producto();
                                producto.IdProducto= int.Parse(row[0].ToString());
                                producto.Nombre = row[1].ToString();
                                producto.Marca = row[2].ToString();
                                producto.Costo = decimal.Parse(row[3].ToString());
                                producto.PrecioVenta = decimal.Parse(row[4].ToString());                                

                                result.Objects.Add(producto);
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

        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ProductoGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = idProducto;

                        cmd.Parameters.AddRange(collection);

                        DataTable tablaProducto = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaProducto);

                        if (tablaProducto.Rows.Count > 0)
                        {
                            DataRow row = tablaProducto.Rows[0];

                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = int.Parse(row[0].ToString());
                            producto.Nombre = row[1].ToString();
                            producto.Marca = row[2].ToString();
                            producto.Costo = decimal.Parse(row[3].ToString());
                            producto.PrecioVenta = decimal.Parse(row[4].ToString());

                            result.Object = producto;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al obtener registro!";
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
