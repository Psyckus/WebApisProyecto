using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Capa_Entidad
{
    public class Autorizaciones
    {
      
        public int Codigo { get; set; }

        [Required(ErrorMessage = "La Entidad Origen es obligatorio.")]
        public string Entidad_Origen { get; set;}

        [Required(ErrorMessage = "La Entidad Destino es obligatorio.")]
        public string Entidad_Destino { get; set; }

        [Required(ErrorMessage = "La cuenta de Origen es obligatoria.")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La cuenta de Origen solo debe contener números y  numeros.")]
        public string Cuenta_Origen { get; set; }

        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La cuenta de Destino solo debe contener números.")]
        public string Cuenta_Destino { get; set; }

        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = " El Cliente Autoriza solo debe contener letras.")]
        public string identificacion_Origen { get; set; }

        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = " El Cliente Solicita solo debe contener letras.")]
        public string identificacion_Destino { get; set; }


        [DataType(DataType.Date, ErrorMessage = "El campo Fecha_Inicio debe ser una fecha válida.")]
        public DateTime Fecha_Inicio { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El campo Fecha_Finalizacion debe ser una fecha válida.")]
        public DateTime Fecha_Finalizacion { get; set; }

        public string Estado { get; set; }

    }

}
