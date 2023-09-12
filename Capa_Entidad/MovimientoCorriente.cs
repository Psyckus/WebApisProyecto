using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class MovimientoCorriente
    {
        [JsonIgnore]
        public int ID { get; set; }
  
        public string cuentaDestino { get; set; }

        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "El numero de cuenta solo debe contener números y  numeros.")]
        [Required(ErrorMessage = "El numero de cuenta es obligatorio.")]
        public string CuentaCorriente { get; set; }

        [Required(ErrorMessage = "La fecha de la transaccion obligatoria.")]
        public DateTime Fecha { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = " La tipo de movimiento solo debe contener letras.")]
        [Required(ErrorMessage = "El tipo de movimiento es obligatorio.")]
        public string TipoMovimiento { get; set; }

        //[Required(ErrorMessage = "El monto del movimiento es obligatorio.")]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "El monto solo debe contener numeros.")]

        public int Monto { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = " El tipo de transaccion solo debe contener letras.")]
        [Required(ErrorMessage = "El tipo de transaccion es obligatorio.")]
        public string TipoTransaccion { get; set; }


        public string Identificador { get; set; }

        // [RegularExpression("^[a-zA-Z]+$", ErrorMessage = " La descripcion solo debe contener letras.")]
        //[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "La descripcion solo debe contener números y  numeros.")]
        //[Required(ErrorMessage = "La descripcion del movimiento es obligatorio.")]
        public string Descripcion { get; set; }

        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "EL canal solo debe contener letras.")]
        //[Required(ErrorMessage = "El canal por donde se realiza el movimiento eso bligatorio.")]
        public string Canal { get; set; }

        // Propiedades de navegación
        [JsonIgnore]
        public CuentaCorriente Cuenta { get; set; }
    }
}
