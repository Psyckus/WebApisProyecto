using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_RegistrarMovimiento
    {
        private CN_Transaccion datosTransaccion;
        private CD_Movimientos datosMovimientos;
        public CN_RegistrarMovimiento()
        {
            datosTransaccion = new CN_Transaccion();
            datosMovimientos = new CD_Movimientos();

        }
        public void RegistrarMovimientos(Movimiento movimiento)
        {
            // Realizar validaciones según los requisitos mencionados
            // (existencia de la cuenta, fecha correcta, monto no negativo, etc.)

        
         

            if (movimiento.TipoTransaccion == "Interbancaria")
            {
                if (string.IsNullOrEmpty(movimiento.Identificador.ToString()))
                {
                    throw new Exception("Se requiere un identificador único para las transacciones interbancarias.");
                }
            }
            if (!datosTransaccion.VerificarSaldoSuficiente(movimiento.Cuenta, movimiento.Monto, movimiento.cuentaDestino))
            {
                throw new Exception("No hay suficientes fondos en la cuenta de origen.");
            }
            // Validación de la cuenta existente
            if (!datosMovimientos.ExisteCuenta(movimiento.Cuenta))
            {
                // Si la cuenta no existe, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("La cuenta especificada no existe.");
            }

            // Validación de la fecha actual
            if (movimiento.Fecha.Date != DateTime.Now.Date)
            {
                // Si la fecha no es igual a la fecha actual, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("La fecha debe ser igual a la fecha actual.");
            }

            // Validación del monto no negativo
            if (movimiento.Monto < 0)
            {
                // Si el monto es negativo, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("El monto no puede ser negativo.");
            }

            //// Validación de la transacción interbancaria
            //if (movimiento.TipoTransaccion == TipoTransaccion.Interbancaria && movimiento.Identificador == 0)
            //{
            //    // Si es una transacción interbancaria pero no se especificó un identificador único, lanzar una excepción o manejar el error de alguna forma adecuada
            //    throw new Exception("Se requiere un identificador único para las transacciones interbancarias.");
            //}
            //if (movimiento.TipoTransaccion.ToLower() == "interbancaria" && movimiento.Identificador == 0)
            //{
            //    throw new Exception("Se requiere un identificador único para las transacciones interbancarias.");
            //}

            // Validación de la descripción no nula ni vacía
            if (string.IsNullOrEmpty(movimiento.Descripcion))
            {
                // Si la descripción es nula o vacía, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("La descripción no puede ser nula ni vacía.");
            }
            // Llamar al método en la capa de datos para registrar el movimiento
            datosMovimientos.RegistrarMovimiento(movimiento);
        }

        public void RegistrarMovimientoCorriente(MovimientoCorriente movimientoCorriente)
        {
            // Realizar validaciones según los requisitos mencionados
            // (existencia de la cuenta, fecha correcta, monto no negativo, etc.)
           

            if (movimientoCorriente.TipoTransaccion == "Interbancaria")
            {
                if (string.IsNullOrEmpty(movimientoCorriente.Identificador.ToString()))
                {
                    throw new Exception("Se requiere un identificador único para las transacciones interbancarias.");
                }
            }
            if (!datosTransaccion.VerificarSaldoSuficiente(movimientoCorriente.CuentaCorriente, movimientoCorriente.Monto, movimientoCorriente.cuentaDestino))
            {
                throw new Exception("No hay suficientes fondos en la cuenta de origen.");
            }
            // Validación de la cuenta existente
            if (!datosMovimientos.ExisteCuenta(movimientoCorriente.CuentaCorriente))
            {
                // Si la cuenta no existe, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("La cuenta especificada no existe.");
            }

            // Validación de la fecha actual
            if (movimientoCorriente.Fecha.Date != DateTime.Now.Date)
            {
                // Si la fecha no es igual a la fecha actual, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("La fecha debe ser igual a la fecha actual.");
            }

            // Validación del monto no negativo
            if (movimientoCorriente.Monto < 0)
            {
                // Si el monto es negativo, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("El monto no puede ser negativo.");
            }

            //// Validación de la transacción interbancaria
            //if (movimiento.TipoTransaccion == TipoTransaccion.Interbancaria && movimiento.Identificador == 0)
            //{
            //    // Si es una transacción interbancaria pero no se especificó un identificador único, lanzar una excepción o manejar el error de alguna forma adecuada
            //    throw new Exception("Se requiere un identificador único para las transacciones interbancarias.");
            //}
            //if (movimientoCorriente.TipoTransaccion.ToLower() == "interbancaria" && movimientoCorriente.Identificador == 0)
            //{
            //    throw new Exception("Se requiere un identificador único para las transacciones interbancarias.");
            //}

            // Validación de la descripción no nula ni vacía
            if (string.IsNullOrEmpty(movimientoCorriente.Descripcion))
            {
                // Si la descripción es nula o vacía, lanzar una excepción o manejar el error de alguna forma adecuada
                throw new Exception("La descripción no puede ser nula ni vacía.");
            }
            // Llamar al método en la capa de datos para registrar el movimiento
            datosMovimientos.RegistrarMovimientoCorriente(movimientoCorriente);
        }

        public List<Movimiento> ObtenerMovimientosPorCuenta(string numCuenta)
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            try
            {
                movimientos = datosMovimientos.ObtenerMovimientosPorCuenta(numCuenta);
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error de alguna forma adecuada
                Console.WriteLine("Error al obtener los movimientos por cuenta: " + ex.Message);
            }

            return movimientos;
        }

        public List<MovimientoCorriente> ObtenerMovimientosCorrientesPorCuenta(string numCuenta)
        {
            List<MovimientoCorriente> movimientosCorrientes = new List<MovimientoCorriente>();

            try
            {
                movimientosCorrientes = datosMovimientos.ObtenerMovimientosCorrientesPorCuenta(numCuenta);
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error de alguna forma adecuada
                Console.WriteLine("Error al obtener los movimientos corrientes por cuenta: " + ex.Message);
            }

            return movimientosCorrientes;
        }
        public string ObtenerMonedaCuenta(string numCuenta)
        {
            return datosMovimientos.ObtenerMoneda(numCuenta);
        }
    }
}
