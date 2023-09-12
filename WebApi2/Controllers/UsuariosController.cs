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
    public class UsuariosController : ApiController
    {
        private CN_Usuarios cn_usuarios;
        public UsuariosController()
        {
            cn_usuarios = new CN_Usuarios();
        }
        [HttpPost]
        [Route("usuarios/login")]
        public IHttpActionResult Login(Credenciales request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Si la validación falla, se retorna un error de BadRequest con los mensajes de validación
                    return BadRequest(ModelState);
                }
                // Validar datos de entrada
                bool usuarioExiste = cn_usuarios.VerificarUsuarioExistente(request.Username);

                if (!usuarioExiste)
                {
                    return Content(HttpStatusCode.NotFound, new { mensaje = "El usuario ingresado no existe." });

                }



                // Llamar al método de lógica de negocio para realizar el login
                bool loginResult = cn_usuarios.Login(request.Username, request.Password);

                if (loginResult)
                {
                    return Content(HttpStatusCode.OK, new { mensaje = "Login exitoso" });
                    

                }
                else
                {
                    return BadRequest("Usuario y/o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar errores no controlados
                return InternalServerError(ex);
            }

        }
        [Route("api/correo/enviar")]
        [HttpPost]
        public IHttpActionResult EnviarCorreo(correo correoDTO)
        {
            bool resultado = cn_usuarios.EnviarCorreo(correoDTO.Correo, correoDTO.Asunto, correoDTO.Mensaje);

            if (resultado)
            {
                return Ok("Correo enviado exitosamente.");
            }
            else
            {
                return BadRequest("Error al enviar el correo.");
            }
        }

    }
}
