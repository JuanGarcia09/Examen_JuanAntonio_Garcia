using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Cliente
    {
        public static ML.Result Add(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ClienteAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[5];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = cliente.Nombre;
                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = cliente.ApellidoPaterno;
                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = cliente.ApellidoMaterno;
                        collection[3] = new SqlParameter("Domicilio", SqlDbType.VarChar);
                        collection[3].Value = cliente.Domicilio;
                        collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[4].Value = cliente.Email;

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

        public static ML.Result Update(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ClienteUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[6];

                        collection[0] = new SqlParameter("IdCliente", SqlDbType.Int);
                        collection[0].Value = cliente.IdCliente;
                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = cliente.Nombre;
                        collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = cliente.ApellidoPaterno;
                        collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = cliente.ApellidoMaterno;
                        collection[4] = new SqlParameter("Domicilio", SqlDbType.VarChar);
                        collection[4].Value = cliente.Domicilio;
                        collection[5] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[5].Value = cliente.Email;

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

        public static ML.Result Delete(int idCliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ClienteDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdCliente", SqlDbType.Int);
                        collection[0].Value = idCliente;

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
                    string query = "ClienteGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tablaCliente = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaCliente);

                        result.Objects = new List<object>();

                        if (tablaCliente.Rows.Count > 0)
                        {
                            foreach (DataRow row in tablaCliente.Rows)
                            {
                                ML.Cliente cliente = new ML.Cliente();
                                cliente.IdCliente = int.Parse(row[0].ToString());
                                cliente.Nombre = row[1].ToString();
                                cliente.ApellidoPaterno = row[2].ToString();
                                cliente.ApellidoMaterno = row[3].ToString();
                                cliente.Domicilio = row[4].ToString();
                                cliente.Email = row[5].ToString();
                                cliente.NombreCompleto = row[6].ToString();

                                result.Objects.Add(cliente);
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

        public static ML.Result GetById(int idCliente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.getConnectionString()))
                {
                    string query = "ClienteGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdCliente", SqlDbType.Int);
                        collection[0].Value = idCliente;

                        cmd.Parameters.AddRange(collection);

                        DataTable tablaCliente = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tablaCliente);

                        if (tablaCliente.Rows.Count > 0)
                        {
                            DataRow row = tablaCliente.Rows[0];

                            ML.Cliente cliente = new ML.Cliente();
                            cliente.IdCliente = int.Parse(row[0].ToString());
                            cliente.Nombre = row[1].ToString();
                            cliente.ApellidoPaterno = row[2].ToString();
                            cliente.ApellidoMaterno = row[3].ToString();
                            cliente.Domicilio = row[4].ToString();
                            cliente.Email = row[5].ToString();
                            cliente.NombreCompleto = row[6].ToString();

                            result.Object = cliente;
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
