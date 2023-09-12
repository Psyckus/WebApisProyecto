using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CuentaAhorro
    {

        public string NumCuenta { get; set; }
        public string Identificacion { get; set; }
        public int Monto { get; set; }
        public int Congelado { get; set; }
        public int Embargado { get; set; }
        public int Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }

        // Propiedades de navegación
        public Clientes Cliente { get; set; }
        public List<Movimiento> Movimientos { get; set; }
    }
}
