using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Movimientos
    {

        private string connectionString = Conexion.cn;
        public void RegistrarMovimiento(Movimiento movimiento)
        {
           
            // Insertar el movimiento en la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string query = "INSERT INTO Movimientos (Cuenta, Fecha, Tipo_Movimiento, Monto, Tipo_Transaccion, Identificador, Descripcion, Canal) " +
                               //"VALUES (@Cuenta,@Fecha, @Tipo_Movimiento, @Monto, @Tipo_Transaccion, @Identificador, @Descripcion, @Canal)";

                using (SqlCommand command = new SqlCommand("Sp_RegistrarMovimientoAhorro", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cuenta", movimiento.Cuenta);
                    command.Parameters.AddWithValue("@Fecha", movimiento.Fecha);
                    command.Parameters.AddWithValue("@tipo_movimiento", movimiento.TipoMovimiento);
                    command.Parameters.AddWithValue("@Monto", movimiento.Monto);
                    command.Parameters.AddWithValue("@tipo_transaccion", movimiento.TipoTransaccion);
                    command.Parameters.AddWithValue("@identificador", movimiento.Identificador);
                    command.Parameters.AddWithValue("@Descripcion", movimiento.Descripcion);
                    command.Parameters.AddWithValue("@canal", movimiento.Canal);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Aquí puedes realizar cualquier otra operación relacionada con la inserción del movimiento en la base de datos
        }

        public void RegistrarMovimientoCorriente(MovimientoCorriente movimientoCorriente)
        {
            // Insertar el movimiento en la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string query = "INSERT INTO Movimientos_Corrientes (Cuenta_Corriente, Fecha, Tipo_Movimiento, Monto, Tipo_Transaccion, Identificador, Descripcion, Canal) " +
                              //"VALUES (@Cuenta_Corriente,@Fecha, @Tipo_Movimiento, @Monto, @Tipo_Transaccion, @Identificador, @Descripcion, @Canal)";

                using (SqlCommand command = new SqlCommand("Sp_RegistrarMovimientoCorriente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cuenta", movimientoCorriente.CuentaCorriente);
                    command.Parameters.AddWithValue("@Fecha", movimientoCorriente.Fecha);
                    command.Parameters.AddWithValue("@tipo_movimiento", movimientoCorriente.TipoMovimiento);
                    command.Parameters.AddWithValue("@Monto", movimientoCorriente.Monto);
                    command.Parameters.AddWithValue("@tipo_transaccion", movimientoCorriente.TipoTransaccion);
                    command.Parameters.AddWithValue("@identificador", movimientoCorriente.Identificador);
                    command.Parameters.AddWithValue("@Descripcion", movimientoCorriente.Descripcion);
                    command.Parameters.AddWithValue("@canal", movimientoCorriente.Canal);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Aquí puedes realizar cualquier otra operación relacionada con la inserción del movimiento en la base de datos
        
        }

        public bool ExisteCuenta(string numCuenta)
        {
            bool cuentaExiste = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Cuentas_Ahorro WHERE Num_Cuenta = @NumCuenta " +
                               "UNION " +
                               "SELECT COUNT(*) FROM Cuenta_Corriente WHERE Num_Cuenta = @NumCuenta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    int cuentaCount = 0;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cuentaCount += (int)reader[0];
                    }

                    cuentaExiste = cuentaCount > 0;
                }
            }

            return cuentaExiste;
        }
        public bool EsCuentaAhorro(string numCuenta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Cuentas_Ahorro WHERE Num_Cuenta = @NumCuenta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    int cuentaCount = (int)command.ExecuteScalar();

                    return cuentaCount > 0;
                }
            }
        }

        public bool EsCuentaCorriente(string numCuenta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Cuenta_Corriente WHERE Num_Cuenta = @NumCuenta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    int cuentaCount = (int)command.ExecuteScalar();

                    return cuentaCount > 0;
                }
            }
        }

        public string ObtenerMoneda(string numCuenta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 Moneda.Tipo_Moneda " +
                               "FROM Cuentas_Ahorro " +
                               "INNER JOIN Moneda ON Cuentas_Ahorro.Moneda = Moneda.ID " +
                               "WHERE Cuentas_Ahorro.Num_Cuenta = @NumCuenta " +
                               "UNION " +
                               "SELECT TOP 1 Moneda.Tipo_Moneda " +
                               "FROM Cuenta_Corriente " +
                               "INNER JOIN Moneda ON Cuenta_Corriente.Moneda = Moneda.ID " +
                               "WHERE Cuenta_Corriente.Num_Cuenta = @NumCuenta";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    var moneda = command.ExecuteScalar();

                    return moneda != null ? moneda.ToString() : null;
                }
            }
        }








        //metodos para obtener los movimientos 
        //Ya tiene un procedimiento almacenado
        public List<Movimiento> ObtenerMovimientosPorCuenta(string numCuenta)
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT m.*, tm.Tipo_Movimiento AS NombreTipoMovimiento, tt.Tipo_Transaccion AS NombreTipoTransaccion, c.Canal AS NombreCanal " +
                               "FROM Movimientos m " +
                               "INNER JOIN Tipo_Movimiento tm ON m.Tipo_Movimiento = tm.ID " +
                               "INNER JOIN Tipo_Transacion tt ON m.Tipo_Transaccion = tt.ID " +
                               "INNER JOIN Canal c ON m.Canal = c.ID " +
                               "WHERE m.Cuenta = @NumCuenta " +
                               "ORDER BY m.Fecha DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Movimiento movimiento = new Movimiento();
                        movimiento.ID = (int)reader["ID"];
                        movimiento.Cuenta = (string)reader["Cuenta"];
                        movimiento.Fecha = (DateTime)reader["Fecha"];
                        movimiento.Monto = (int)reader["Monto"];
                        movimiento.Identificador = (string)reader["Identificador"];
                        movimiento.Descripcion = (string)reader["Descripcion"];
                        movimiento.Canal = (string)reader["NombreCanal"];
                        movimiento.TipoMovimiento = (string)reader["NombreTipoMovimiento"];
                        movimiento.TipoTransaccion = (string)reader["NombreTipoTransaccion"];

                        movimientos.Add(movimiento);
                    }
                }
            }

            return movimientos;
        }


        //Ya tiene procedimiento almacenado
        public List<MovimientoCorriente> ObtenerMovimientosCorrientesPorCuenta(string numCuenta)
        {
            List<MovimientoCorriente> movimientosCorrientes = new List<MovimientoCorriente>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT mc.ID,mc.Cuenta_Corriente,mc.Fecha,tm.Tipo_Movimiento AS NombreTipoMovimiento,mc.Monto,tt.Tipo_Transaccion AS NombreTipoTransaccion,mc.Identificador,mc.Descripcion,c.Canal AS NombreCanal FROM Movimientos_Corrientes mc " +
                                "Inner join Tipo_Movimiento tm " +
                                "on mc.Tipo_Movimiento = tm.ID " +
                                "Inner join Tipo_Transacion tt " +
                                "on mc.Tipo_Transaccion = tt.ID " +
                                "Inner join Canal c " +
                                "on mc.Canal = c.ID " +
                                "WHERE mc.Cuenta_Corriente = @NumCuenta " +
                                "ORDER BY mc.Fecha DESC "; 
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NumCuenta", numCuenta);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MovimientoCorriente movimientoCorriente = new MovimientoCorriente();
                        movimientoCorriente.ID = (int)reader["ID"];
                        movimientoCorriente.CuentaCorriente = (string)reader["Cuenta_Corriente"];
                        movimientoCorriente.Fecha = (DateTime)reader["Fecha"];
                        movimientoCorriente.Monto = (int)reader["Monto"];
                        movimientoCorriente.Identificador = reader["Identificador"] is DBNull ? null : (string)reader["Identificador"];
                        movimientoCorriente.Descripcion = (string)reader["Descripcion"];
                        movimientoCorriente.Canal = (string)reader["NombreCanal"];
                        movimientoCorriente.TipoMovimiento = (string)reader["NombreTipoMovimiento"];
                        movimientoCorriente.TipoTransaccion = (string)reader["NombreTipoTransaccion"];

                        movimientosCorrientes.Add(movimientoCorriente);
                    }
                }
            }

            return movimientosCorrientes;
        }


     


    }

}
