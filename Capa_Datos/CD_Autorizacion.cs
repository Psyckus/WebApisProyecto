using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Autorizacion
    {
        private string connectionString = Conexion.cn;


        public void CrearAutorizacion(Autorizaciones autorizacion)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Autorizacion(Entidad_Origen,Entidad_Destino,Cuenta_Origen,Cuenta_Destino" +
                    ",identificacion_Origen, identificacion_Destino,Fecha_Inicio,Fecha_Finalizacion,Estado) VALUES(@Entidad_Origen,@Entidad_Destino,@Cuenta_Origen,@Cuenta_Destino" +
                    ",@Cliente_Autoriza,@Cliente_Solicita,@Fecha_Inicio,@Fecha_Finalizacion,@Estado)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Entidad_Origen", autorizacion.Entidad_Origen);
                    command.Parameters.AddWithValue("@Entidad_Destino", autorizacion.Entidad_Destino);
                    command.Parameters.AddWithValue("@Cuenta_Origen", autorizacion.Cuenta_Origen);
                    command.Parameters.AddWithValue("@Cuenta_Destino", autorizacion.Cuenta_Destino);
                    command.Parameters.AddWithValue("@Cliente_Autoriza", autorizacion.identificacion_Origen);
                    command.Parameters.AddWithValue("@Cliente_Solicita", autorizacion.identificacion_Destino);
                    command.Parameters.AddWithValue("@Fecha_Inicio", autorizacion.Fecha_Inicio);
                    command.Parameters.AddWithValue("@Fecha_Finalizacion", autorizacion.Fecha_Finalizacion);
                    command.Parameters.AddWithValue("@Estado", autorizacion.Estado);
            

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }



        }


        public void ActualizarAutorizacion(int codigo, String estado)
        {
            // Código para actualizar el cliente en la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Autorizacion SET Estado = @Estado " +
                    "WHERE Codigo = @Codigo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);
                    command.Parameters.AddWithValue("@Estado", estado);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool VerificarExistencia(int codigo)
        {
            // Código para actualizar el cliente en la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "Select COUNT(*) from Autorizacion where Codigo = @Codigo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigo);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public List<Autorizaciones> GetAutorizacionesPendientes()
        {
            List<Autorizaciones> autorizacionesPendientes = new List<Autorizaciones>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Autorizacion WHERE Estado = 'Pendiente de autorizar'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Autorizaciones autorizacion = new Autorizaciones
                        {
                            Codigo = Convert.ToInt32(reader["Codigo"]),
                            Entidad_Origen = reader["Entidad_Origen"].ToString(),
                            Entidad_Destino = reader["Entidad_Destino"].ToString(),
                            Cuenta_Origen = reader["Cuenta_Origen"].ToString(),
                            Cuenta_Destino = reader["Cuenta_Destino"].ToString(),
                            identificacion_Origen = reader["identificacion_Origen"].ToString(),
                            identificacion_Destino = reader["identificacion_Destino"].ToString(),
                            Fecha_Inicio = (DateTime)reader["Fecha_Inicio"],
                            Fecha_Finalizacion = (DateTime)reader["Fecha_Finalizacion"],
                            Estado = reader["Estado"].ToString()
                        };

                        autorizacionesPendientes.Add(autorizacion);
                    }
                }
            }

            return autorizacionesPendientes;
        }

        public Autorizaciones GetAutorizacionVigente(string cuentaOrigen, string identificacionOrigen, string cuentaDestino, string identificacionDestino)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Autorizacion WHERE Cuenta_Origen = @cuentaOrigen AND identificacion_origen = @identificacionOrigen AND Cuenta_Destino = @cuentaDestino AND identificacion_destino = @identificacionDestino AND Fecha_Inicio <= GETDATE() AND Fecha_Finalizacion >= GETDATE()", connection))
                {
                    command.Parameters.AddWithValue("@cuentaOrigen", cuentaOrigen);
                    command.Parameters.AddWithValue("@identificacionOrigen", identificacionOrigen);
                    command.Parameters.AddWithValue("@cuentaDestino", cuentaDestino);
                    command.Parameters.AddWithValue("@identificacionDestino", identificacionDestino);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Autorizaciones autorizacion = new Autorizaciones
                        {
                            Codigo = (int)reader["Codigo"],
                            Entidad_Origen = reader["Entidad_Origen"].ToString(),
                            Entidad_Destino = reader["Entidad_Destino"].ToString(),
                            Cuenta_Origen = reader["Cuenta_Origen"].ToString(),
                            Cuenta_Destino = reader["Cuenta_Destino"].ToString(),
                            identificacion_Origen = reader["identificacion_origen"].ToString(),
                            identificacion_Destino = reader["identificacion_destino"].ToString(),
                            Fecha_Inicio = (DateTime)reader["Fecha_Inicio"],
                            Fecha_Finalizacion = (DateTime)reader["Fecha_Finalizacion"],
                            Estado = reader["Estado"].ToString()
                        };
                        return autorizacion;
                    }
                }
            }
            return null;
        }

    }
}




