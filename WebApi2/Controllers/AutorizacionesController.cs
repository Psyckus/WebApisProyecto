using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Capa_Entidad;
using Capa_Negocio;

namespace WebApi2.Controllers
{
    public class AutorizacionesController : ApiController
    {

        private CN_Autorizacion cn_autorizacion;


        public AutorizacionesController()
        {
            cn_autorizacion = new CN_Autorizacion();
        }


        //Registrar una autorizacion

        [HttpPost]
        public IHttpActionResult CrearAutorizacion(Autorizaciones autorizacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Content(HttpStatusCode.NotFound, ModelState);

                }

                cn_autorizacion.CrearAutorizacion(autorizacion);
                return CreatedAtRoute("DefaultApi", new { id = autorizacion.Codigo }, autorizacion);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        //Actualizar el estado de las actualizaciones
        [HttpPut]
        public IHttpActionResult ActualizarAutorizacion(int codigo, string estado)
        {
            try
            {
                bool codigoexists = cn_autorizacion.VerificarExistancia(codigo);

                if (codigoexists == true)
                {
                    cn_autorizacion.ActualizarAutorizacion(codigo, estado);
                    return Content(HttpStatusCode.OK, new { codigo = codigo, estado = estado });
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, new { mensaje = "El codigo de la autorizacion no existe" });
                }

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, new { mensaje = "El codigo ingresado no existe" });

            }

        }

        [HttpGet]
        [Route("api/Autorizaciones/Pendientes")]
        public IHttpActionResult GetAutorizacionesPendientes()
        {
            var autorizacionesPendientes = cn_autorizacion.GetAutorizacionesPendientes();
            return Ok(autorizacionesPendientes);
        }

        [HttpGet]
        [Route("api/Autorizaciones/vigente")]
        public IHttpActionResult GetAutorizacionVigente(string cuentaOrigen, string identificacionOrigen, string cuentaDestino, string identificacionDestino)
        {
            Autorizaciones autorizacion = cn_autorizacion.ObtenerAutorizacionVigente(cuentaOrigen, identificacionOrigen, cuentaDestino, identificacionDestino);
            if (autorizacion != null)
            {
                return Ok(autorizacion);
            }
            else
            {
                return NotFound();
            }
        }
    }
}