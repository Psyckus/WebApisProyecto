using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Transaccion
    {
        //public string codigo { get; set;}

        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El banco de origen solo debe contener letras.")]
        [Required(ErrorMessage = "El banco de origen Es Obligatorio.")]

        public string BancoOrigen { get; set; }

        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El banco de destino solo debe contener letras.")]
        [Required(ErrorMessage = "El banco de destino Es Obligatorio.")]
        public string BancoDestino { get; set; }


        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La cuenta de origen solo debe contener letras y numeros.")]
        [Required(ErrorMessage = "La cuenta de origen es Obligatorio.")]
        public string CuentaOrigen { get; set; }


        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La cuenta de destino solo debe contener letras y numeros.")]
        [Required(ErrorMessage = "La cuenta de destino Es Obligatorio.")]
        public string CuentaDestino { get; set; }


        [RegularExpression("^[0-9]+$", ErrorMessage = "El monto solo debe contener numeros.")]
        [Required(ErrorMessage = "El monto Es Obligatorio.")]
        public int Monto { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = " La cedula de origen solo debe contener numeros.")]
        [Required(ErrorMessage = "La cedula de origen es obligatorio.")]
        public string CedulaOrigen { get; set; }


        [RegularExpression("^[0-9]+$", ErrorMessage = "La cedulad de destino solo debe contener numeros.")]
        [Required(ErrorMessage = "La cedulad de destino Es Obligatorio.")]
        public string CedulaDestino { get; set; }


        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El Tipo de cedula solo debe contener letras.")]
        [Required(ErrorMessage = "El Tipo de cedula es Obligatorio.")]
        public string TipoCedulaOrigen { get; set; }


        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El Tipo de cedula solo debe contener letras.")]
        [Required(ErrorMessage = "El Tipo de cedula Es Obligatorio.")]
        public string TipoCedulaDestino { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "La moneda solo debe contener letras.")]
        [Required(ErrorMessage = "La moneda Es Obligatorio.")]
        public string Moneda { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "La descripcion solo debe contener letras.")]
        [Required(ErrorMessage = "La descripcion Es Obligatorio.")]
        public string Descripcion { get; set; }

        
        //[RegularExpression("^[0-9]+$", ErrorMessage = "El estado solo debe contener letras.")]
        //[Required(ErrorMessage = "El estado Es Obligatorio.")]
        //public int Estado { get; set; }


        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = " El motivo solo debe contener letras.")]
        [Required(ErrorMessage = "El motivo Es Obligatorio.")]
        public string Motivo { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El canal solo debe contener letras.")]
        [Required(ErrorMessage = "El canal Es Obligatorio.")]
        public String Canal { get; set; }


        public string Tipo_Transaccion_ID { get; set; }
        public string codigoRefencia { get; set; }



    }
}
