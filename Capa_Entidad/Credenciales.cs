using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Capa_Entidad
{
    public class Credenciales
    {

        [Required(ErrorMessage = "El campo de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El usuario debe tener como máximo {1} caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo de contraseña es obligatorio.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres.")]
        public string Password { get; set; }


    }
}
