using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Transaccion
    {

        private CD_Transaccion datosTransaccion;
        private CD_Movimientos MoviTransaccion;
        private CN_Recursos recursos;

        public CN_Transaccion()
        {
            datosTransaccion = new CD_Transaccion();
            MoviTransaccion = new CD_Movimientos();
            recursos = new CN_Recursos();
        }

  
        public void RegistrarTransferencia(Transaccion transferencia)
        {
            // Validar datos de la transferencia
         
   
                if (transferencia.BancoDestino == "001" && transferencia.BancoOrigen == "001")
                {
                if (transferencia.Tipo_Transaccion_ID == "Solicitud")
                {
                    if (!recursos.VerificarEstadoActivo(transferencia.CuentaDestino))
                    {
                        throw new Exception("La cuenta destino no está activa.");
                    }

                    if (!RealizarSolicitudDinero(transferencia.CuentaOrigen, transferencia.Monto, transferencia.CuentaDestino))
                    {
                        throw new Exception("No hay suficientes fondos en la cuenta de destino.");
                    }

                }
                else if(transferencia.Tipo_Transaccion_ID == "Envio")
                {
                    if (!recursos.VerificarEstadoActivo(transferencia.CuentaDestino))
                    {
                        throw new Exception("La cuenta de origen no está activa.");
                    }
                    if (!VerificarSaldoSuficiente(transferencia.CuentaDestino, transferencia.Monto, transferencia.CuentaDestino))
                    {
                        throw new Exception("No hay suficientes fondos en la cuenta de destino.");
                    }
                }
                // Guardar la transacción en la base de datos
                datosTransaccion.RegistrarTransferencia(transferencia);
                }
                else if(transferencia.BancoDestino != "001" && transferencia.BancoOrigen == "001") 
                {

                if (transferencia.Tipo_Transaccion_ID == "Envio")
                {
                    if (!recursos.VerificarEstadoActivo(transferencia.CuentaOrigen))
                    {
                        throw new Exception("La cuenta de origen no está activa.");

                    }
                    if (!VerificarSaldoSuficiente(transferencia.CuentaOrigen, transferencia.Monto,transferencia.CuentaDestino))
                    {
                        throw new Exception("No hay suficientes fondos en la cuenta de origen.");
                    }
                   
                    datosTransaccion.RegistrarTransferencia(transferencia);
                }else if(transferencia.Tipo_Transaccion_ID == "Solicitud")
                {
                    if (!recursos.VerificarEstadoActivo(transferencia.CuentaOrigen))
                    {
                        throw new Exception("La cuenta de origen no está activa.");

                    }
                    if (!RealizarSolicitudDinero(transferencia.CuentaOrigen, transferencia.Monto, transferencia.CuentaDestino))
                    {
                        throw new Exception("No hay suficientes fondos en la cuenta de Destino.");
                    }
                }

                // Guardar la transacción en el core y en el sistema de transferencias
       

                // Guardar la transacción en la base de datos
                datosTransaccion.RegistrarTransferencia(transferencia);
            }else if(transferencia.BancoOrigen == "001" && transferencia.BancoDestino == "001" || transferencia.BancoOrigen != "001" && transferencia.BancoDestino != "001")
            {
                throw new Exception("Error No se puede procesar la solicitud ..Porfavor revisar el origen o el destino.");
            }

            //datosTransaccion.RegistrarTransferencia(transferencia);
        }



        public bool VerificarSaldoSuficiente(string cuentaOrigen, int monto, string CuentaDestino)
        {
            //Realizar la lógica de verificación del saldo en la cuenta de origen
            // Obtener el saldo actual de la cuenta

            bool cuentaAhorro= MoviTransaccion.EsCuentaAhorro(cuentaOrigen);

            if (cuentaAhorro)
            {
                // Realizar la lógica para cuentas de ahorro
                // Obtener el saldo actual de la cuenta
                decimal saldoActual = datosTransaccion.ObtenerSaldoCuenta(cuentaOrigen);

                // Verificar si el saldo es suficiente para realizar la transferencia
                if (saldoActual >= monto)
                {
                    // Restar el monto de la cuenta de ahorro
                    
                    datosTransaccion.ActualizarCuenta(cuentaOrigen, saldoActual-monto);
                    int montoActual = datosTransaccion.ObtenerSaldoCuenta(CuentaDestino);

                    datosTransaccion.ActualizarCuenta(CuentaDestino, montoActual+monto);
                    return true; // El saldo es suficiente
                }
                else
                {
                    return false; // El saldo es insuficiente
                }
            }
            else
            {
                // Realizar la lógica para cuentas corrientes
                // Agrega aquí la lógica específica para cuentas corrientes
                // ...
                decimal saldoActual = datosTransaccion.ObtenerSaldoCuenta(cuentaOrigen);
                // Retornar true o false según corresponda
                if (saldoActual >= monto)
                {
                    datosTransaccion.ActualizarCuenta(cuentaOrigen, saldoActual - monto);
                    int montoActual = datosTransaccion.ObtenerSaldoCuenta(CuentaDestino);
                    datosTransaccion.ActualizarCuenta(CuentaDestino, montoActual + monto);

                    return true; // El saldo es suficiente
                }
                else
                {
                    return false; // El saldo es insuficiente
                }
            }
        }

        private bool RealizarSolicitudDinero(string cuentaOrigen, int monto, string cuentaDestino)
        {
            bool cuentaAhorro = MoviTransaccion.EsCuentaAhorro(cuentaOrigen);

            if (cuentaAhorro)
            {
                decimal saldoActual = datosTransaccion.ObtenerSaldoCuenta(cuentaDestino);
                decimal saldoActual2 = datosTransaccion.ObtenerSaldoCuenta(cuentaOrigen);

                if (saldoActual >= monto)
                {
                    // Restar el monto de la cuenta de ahorro de la cuenta origen
                    datosTransaccion.ActualizarCuenta(cuentaOrigen, saldoActual2 + monto);


                    datosTransaccion.ActualizarCuenta(cuentaDestino, saldoActual - monto);

                    return true; // El saldo es suficiente para realizar la solicitud de dinero
                }
                else
                {
                    return false; // El saldo es insuficiente para realizar la solicitud de dinero
                }
            }
            else
            {
                // Realizar la lógica para cuentas corrientes
                // Agrega aquí la lógica específica para cuentas corrientes
                // ...
                decimal saldoActual = datosTransaccion.ObtenerSaldoCuenta(cuentaDestino);
                decimal saldoActual2 = datosTransaccion.ObtenerSaldoCuenta(cuentaOrigen);

                if (saldoActual >= monto)
                {
                    // Restar el monto de la cuenta corriente de la cuenta origen
                    datosTransaccion.ActualizarCuenta(cuentaOrigen, saldoActual2 + monto);

                  
                    datosTransaccion.ActualizarCuenta(cuentaDestino, saldoActual - monto);

                    return true; // El saldo es suficiente para realizar la solicitud de dinero
                }
                else
                {
                    return false; // El saldo es insuficiente para realizar la solicitud de dinero
                }
            }
        }


        public List<string> ObtenerTiposMoneda()
        {
            return datosTransaccion.ObtenerTiposMoneda();
        }


        public void ActualizarEstado(String codigo, String estado)
        {
            datosTransaccion.ActualizarEstado(codigo, estado);
        }

        public bool VerificarExisrencia(String codigo)
        {
            return datosTransaccion.VerificarExistencia(codigo);

        }




    }
}
