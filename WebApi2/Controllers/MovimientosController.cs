using Capa_Entidad;
using Capa_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi2.Controllers
{
    public class MovimientosController : ApiController
    {

        private CN_RegistrarMovimiento cn_movimientos;

        public MovimientosController()
        {
            cn_movimientos = new CN_RegistrarMovimiento();
        }

        [Route("api/moviregistrarMovimientoCuentaAhorro")]
        [HttpPost]
        public IHttpActionResult RegistrarMovimiento(Movimiento movimiento)
        {
            try
            { 
                //if (!ModelState.IsValid)
                //{
                //    // Si la validación falla, se retorna un error de BadRequest con los mensajes de validación
                //    return Content(HttpStatusCode.BadRequest, ModelState);
                //}
                cn_movimientos.RegistrarMovimientos(movimiento);
                //return CreatedAtRoute("hola", new { id = movimiento.ID}, movimiento);
                //return Created(new Uri(Request.RequestUri, new { id = movimiento.ID }, movimiento));
                return Content(HttpStatusCode.Created, new { mensaje = "Movimiento registrado." });



            }
            catch (Exception ex)
            {
                if(ex.Message == "La cuenta especificada no existe.")
                {
                   return Content(HttpStatusCode.NotFound, new { mensaje = "La cuenta especificada no existe." });

                }
                else
                {
                    // Manejar cualquier error y retornar una respuesta con el código de error apropiado
                    return Content(HttpStatusCode.BadRequest, new { mensaje = ex.Message });
                } 
            }
        }
        [HttpPost]
        [Route("api/movimientos/registrarMovimientoCuentaCorriente")]
        public IHttpActionResult RegistrarMovimientoCorriente(MovimientoCorriente movimientoCorriente)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    // Si la validación falla, se retorna un error de BadRequest con los mensajes de validación
                //    return Content(HttpStatusCode.BadRequest, ModelState);
                //}
                cn_movimientos.RegistrarMovimientoCorriente(movimientoCorriente);
                return Content(HttpStatusCode.Created, new { mensaje = "Movimiento registrado." });
            }
            catch (Exception ex)
            {
                if (ex.Message == "La cuenta especificada no existe.")
                {
                    return Content(HttpStatusCode.NotFound, new { mensaje = "La cuenta especificada no existe." });
                }
                else
                {
                    // Manejar cualquier error y retornar una respuesta con el código de error apropiado
                    return Content(HttpStatusCode.BadRequest,new { mensaje=  ex.Message });
                }
            }
        }
          
        [HttpGet]
        [Route("api/movimientos/obtener/{numCuenta}")]
        public IHttpActionResult ObtenerMovimientosPorCuenta(string numCuenta)
        {
            try
            {
                List<Movimiento> movimientos = cn_movimientos.ObtenerMovimientosPorCuenta(numCuenta);
                if (movimientos.Count > 0)
                {
                    return Ok(movimientos);
                }
                else
                {
                 return Content(HttpStatusCode.NotFound, new { mensaje = "Cuenta a solicitar no existe" });

                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los movimientos por cuenta: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("api/movimientos/obtenercorrientes/{numCuenta}")]
        public IHttpActionResult ObtenerMovimientosCorrientesPorCuenta(string numCuenta)
        {
            try
            {
                List<MovimientoCorriente> movimientosCorrientes = cn_movimientos.ObtenerMovimientosCorrientesPorCuenta(numCuenta);

                if (movimientosCorrientes.Count > 0)
                {
                    return Ok(movimientosCorrientes);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { mensaje = "Cuenta a solicitar no existe" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los movimientos corrientes por cuenta: " + ex.Message);
            }
        }
        [HttpGet]
        [Route("api/movimientos/Moneda/{numCuenta}/")]
        public IHttpActionResult ObtenerMonedaCuenta(string numCuenta)
        {
            string moneda =  cn_movimientos.ObtenerMonedaCuenta(numCuenta);

            if (moneda == null)
            {
                return NotFound(); // Cuenta no encontrada
            }

            return Ok(moneda); // Devuelve la moneda en formato JSON
        }
    }
}
