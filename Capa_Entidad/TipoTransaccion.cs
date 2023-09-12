using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class TipoTransaccion
    {

        public int ID { get; set; }
        public string Tipo_Transaccion { get; set; }

        public TipoTransaccion(int id, string tipo)
        {
            ID = id;
            Tipo_Transaccion = tipo;
        }

    }
}
