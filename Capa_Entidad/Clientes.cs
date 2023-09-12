using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.ComponentModel;

namespace Capa_Entidad
{
    public class Clientes
    {
        public string Codigo { get; set; }


        [Required(ErrorMessage = "El numero de identificacion Es Obligatorio.")]
        public string Num_Identificacion { get; set; }
        [Required(ErrorMessage = "El tipo de identificacion Es Obligatorio.")]
        public string Tipo_Identificacion { get; set; }

        [Required(ErrorMessage = "El Perfil_Transaccional Es Obligatorio.")]
        public string Nombre { get; set; }


       
        public string Primer_Apellido { get; set; }

        
        public string Segundo_Apellido { get; set; }
     
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = " La Direccion solo debe contener letras.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Perfil_Transaccional Es Obligatorio.")]
        public string Perfil_Transaccional { get; set; }

        [Required(ErrorMessage = "El Nombre del Pais Es Obligatorio.")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "El Estado_Civil Es Obligatorio.")]
        public string Estado_Civil { get; set; }

        [Required(ErrorMessage = "La Profesion Es Obligatoria.")]
        public string Profesion { get; set; }

        [Required(ErrorMessage = "El Lugar De Trabajo Es Obligatorio.")]
        public string Lugar_Trabajo { get; set; }

        [Required(ErrorMessage = "El tipo de cliente Es Obligatorio.")]
        public string Tipo_Cliente { get; set; }


        [Required(ErrorMessage = "Los numeros de telefono son Obligatoria.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El formato del numero de telefono con coincide.")]
        public string Num_Telefono1 { get; set; }

        [Required(ErrorMessage = "Los numeros de telefono son Obligatoria.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El formato del numero de telefono con coincide.")]
        public string Num_Telefono2 { get; set; }

        [Required(ErrorMessage = "Los campos de correos son Obligatoria.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email1 { get; set; }

        [Required(ErrorMessage = "Los campos de correos son Obligatoria.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email2 { get; set; }







        // Propiedades de navegación
        [JsonIgnore]
        public List<CuentaCorriente> CuentasCorriente { get; set; }
        [JsonIgnore]

        public List<CuentaAhorro> CuentasAhorro { get; set; }
    }
}
