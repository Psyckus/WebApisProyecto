using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;

namespace WebApi2.Controllers
{
    public class MantenimientoController : ApiController
    {
        private CN_Clientes cn_clientes;
        public MantenimientoController()
        {
            cn_clientes = new CN_Clientes();
        }

        [HttpPost]
        //[Route("api/Mantenimiento/RegistrarCliente")]
        public IHttpActionResult CrearCliente(Clientes cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Clientes ClientExists = cn_clientes.ObtenerClientePorIdentificacion(cliente.Num_Identificacion);

                if (ClientExists != null)
                {

                    return Content(HttpStatusCode.Conflict, new { mensaje = "El numero de identificacion ingresado ya existe" });
                }
                else
                {
                    cn_clientes.CrearCliente(cliente);
                    return CreatedAtRoute("DefaultApi", new { id = cliente.Codigo }, cliente);
                }               
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/Mantenimiento/ModificarCliente")]
        public IHttpActionResult ActualizarCliente(Clientes cliente)
        {
            try
            {
                Clientes ClientExists = cn_clientes.ObtenerClientePorIdentificacion(cliente.Num_Identificacion);

                if (ClientExists != null)
                {
                   
                    cn_clientes.ActualizarCliente(cliente);
                    return Ok(cliente);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { mensaje = "El usuario a modificar no existe." });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            

            
        }

        [HttpDelete]
        //[Route("api/Mantenimiento/EliminarCliente")]
        public IHttpActionResult EliminarCliente(string identificacion)
        {
            Clientes ClientExists = cn_clientes.ObtenerClientePorIdentificacion(identificacion);

            if (ClientExists == null)
            {
                return NotFound();
            }
            else
            {
                cn_clientes.EliminarCliente(identificacion);
                return Ok(new { Cliente_elimando = identificacion });
            }   
        }

        [HttpGet]
        [Route("api/Mantenimiento/ObtenerClientesPorIdentificacion")]
        public IHttpActionResult ObtenerClientePorIdentificacion(string identificacion)
        {
            var cliente = cn_clientes.ObtenerClientePorIdentificacion(identificacion);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpGet]
        [Route("api/Mantenimiento/ObtenerClientesPorNombre")]
        public IHttpActionResult ObtenerClientesPorNombre(string nombre)
        {
            var clientes = cn_clientes.ObtenerClientesPorNombre(nombre);
            if (clientes != null)
            {
               
                return Ok(clientes);
            }
            else
            {
                return NotFound();
            }
        }




        [HttpGet]
        [Route("api/Mantenimiento/ObtenerClientesPorApellido")]
        public IHttpActionResult ObtenerClientesPorApellido(string apellido1,string apellido2)
        {
            var clientes = cn_clientes.ObtenerClientesPorApellidos(apellido1,apellido2);
            if (clientes != null)
            {
                
                return Ok(clientes);
            }
            else
            {
                return NotFound();
            }


        }
        [HttpGet]
        [Route("api/Cliente/{numCuenta}")]
        public IHttpActionResult GetNombreCliente(string numCuenta)
        {
            // Consultar la tabla de Cuenta_Corriente y Cuentas_Ahorro para obtener el nombre del cliente
            string nombreCliente = cn_clientes.ConsultarNombreCliente(numCuenta);

            if (nombreCliente != null)
            {
                // Devolver el nombre del cliente en la respuesta con código 200 (Ok)
                return Ok(nombreCliente);
            }
            else
            {
                // Si la cuenta no existe, devolver un mensaje de error con código 404 (Not Found)
                return Content(HttpStatusCode.NotFound, new { mensaje = "Cuenta no existe." });
    
            }
        }


        [HttpGet]
        [Route("api/clientes/ObtenerCuentasActivas")]
        public IHttpActionResult ObtenerCuentasActivas(string N_Identificacion)
        {
            try
            {
                // Verificar si el cliente existe
                if (!cn_clientes.VerificarUsuarioExistente(N_Identificacion))
                {
                    return NotFound();
                }

                // Obtener las cuentas activas del cliente desde la base de datos
                List<Cuenta> cuentasActivas = cn_clientes.ObtenerCuentasActivas(N_Identificacion);

                // Mapear las cuentas a un objeto con la información requerida (número de cuenta, saldo, tipo de cuenta)
                List<Cuenta> cuentasInfo = cuentasActivas.Select(cuenta => new Cuenta
                {
                    NumeroCuenta = cuenta.NumeroCuenta,
                    Saldo = cuenta.Saldo,
                    TipoCuenta = cuenta.TipoCuenta
                }).ToList();

                return Ok(cuentasInfo);
            }
            catch (Exception ex)
            {
                // Manejar errores no controlados
                return InternalServerError(ex);
            }
        }









    }
}
