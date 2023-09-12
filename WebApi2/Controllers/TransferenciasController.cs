
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
   
 
    public class TransferenciasController : ApiController
    {
        private CN_Transaccion gestorTransacciones;

        public TransferenciasController()
        {
            gestorTransacciones = new CN_Transaccion();
        }
        [HttpGet]
        [Route("api/moneda/obtenerTiposMoneda")]
        public IHttpActionResult ObtenerTiposMoneda()
        {
            List<string> tiposMoneda = gestorTransacciones.ObtenerTiposMoneda();
            return Ok(tiposMoneda);
        }

        //[Route("api/transferencias")]

        [HttpPost]
        public IHttpActionResult RegistrarTransferencia(Transaccion transaccion)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                // Validar los datos de la transacción
                // ...

                // Registrar la transferencia interbancaria

                gestorTransacciones.RegistrarTransferencia(transaccion);
                //gestorTransacciones.ActualizarEstado(transaccion.codigoRefencia, "En proceso");


                return Content(HttpStatusCode.Created, new { mensaje = "Tranferencia Interbancaria Exitosa"});

            }
            catch (Exception ex)
            {
                if(ex.Message == "No hay suficientes fondos en la cuenta de destino.")
                {
                   return Content(HttpStatusCode.BadRequest, new { mensaje = "No hay suficientes fondos en la cuenta de destino." });
                }
                if (ex.Message == "No hay suficientes fondos en la cuenta de origen.")
                {
                    return Content(HttpStatusCode.BadRequest, new { mensaje = "No hay suficientes fondos en la cuenta de origen." });
                }
                if (ex.Message == "La cuenta de origen no está activa.")
                {
                    return Content(HttpStatusCode.BadRequest, new { mensaje = "La cuenta de origen no está activa." });
                }
                return Content(HttpStatusCode.InternalServerError, new { mensaje = ex });

            }
        }

        //Actualizar el estado de las transferencia interbancarias
        [HttpPut]
        public IHttpActionResult ActualizarEstado(String codigo,String estado)
        {
            try
            {
                // Validar los datos de la transacción
                // ...

                // Registrar la transferencia interbancaria

                bool codigoexists = gestorTransacciones.VerificarExisrencia(codigo);

                if (codigoexists == true)
                {
                    gestorTransacciones.ActualizarEstado(codigo, estado);

                    return Ok(new { Codigo = codigo, Estado = estado });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { mensaje = "El codigo de la transaccion no existe" });
                }

               
            }
            catch (Exception ex)
            {


                return Content(HttpStatusCode.BadRequest, new { mensaje = "Hubo un error con los datos ingresados, por favor revisarlos" });

            }
        }






    }
}
