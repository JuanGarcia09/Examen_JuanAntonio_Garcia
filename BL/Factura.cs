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
    public class Factura
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "FacturaGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tablaFactura = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaFactura);

                        result.Objects = new List<object>();

                        if (tablaFactura.Rows.Count > 0)
                        {
                            foreach (DataRow row in tablaFactura.Rows)
                            {
                                ML.Factura factura = new ML.Factura();
                                factura.IdFactura = int.Parse(row[0].ToString());
                                factura.NumeroFactura = row[1].ToString();
                                factura.FechaHora = row[2].ToString();

                                factura.Cliente = new ML.Cliente();
                                factura.Cliente.IdCliente = int.Parse(row[3].ToString());
                                factura.Cliente.NombreCompleto = row[4].ToString();
                                factura.IdDetalle = int.Parse(row[5].ToString());

                                result.Objects.Add(factura);
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

        public static ML.Result GetById(int idFactura)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "FacturaGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdFactura",SqlDbType.Int);
                        collection[0].Value = idFactura;

                        cmd.Parameters.AddRange(collection);

                        DataTable tableFactura = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tableFactura);                        

                        if (tableFactura.Rows.Count > 0)
                        {
                            DataRow row = tableFactura.Rows[0];
                            ML.Factura factura = new ML.Factura();
                            factura.IdFactura = int.Parse(row[0].ToString());
                            factura.NumeroFactura = row[1].ToString();
                            factura.FechaHora = row[2].ToString();
                            factura.Cliente = new ML.Cliente();
                            factura.Cliente.NombreCompleto = row[3].ToString();
                            factura.IdDetalle = int.Parse(row[4].ToString());

                            result.Object = factura;
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

        public static ML.Result Add(ML.Factura factura)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "FacturaAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];
                        collection[0] = new SqlParameter("NumeroFactura",SqlDbType.VarChar);
                        collection[0].Value = factura.NumeroFactura;                       
                        collection[1] = new SqlParameter("IdCliente",SqlDbType.Int);
                        collection[1].Value = factura.Cliente.IdCliente;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        result.Object = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Connection.Close();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;                            
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al agregar información";
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

        public static ML.Result Delete(int idFactura)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "FacturaDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdFactura", SqlDbType.Int);
                        collection[0].Value = idFactura;

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
                            result.ErrorMessage = "Error al eliminar Factura!";
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
