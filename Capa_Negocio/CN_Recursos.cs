using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Recursos
    {
        private string connectionString = Conexion.cn;

        public bool VerificarEstadoActivo(string numCuenta)
        {
            bool cuentaActiva = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Estado FROM Cuentas_Ahorro WHERE Num_Cuenta = @NumCuenta " +
                               "UNION " +
                               "SELECT Estado FROM Cuenta_Corriente WHERE Num_Cuenta = @NumCuenta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        cuentaActiva = (bool)reader["Estado"];
                    }

                    reader.Close();
                }
            }

            return cuentaActiva;
        }



    }
}
