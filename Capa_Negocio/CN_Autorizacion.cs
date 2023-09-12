using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Autorizacion
    {

        private CD_Autorizacion autorizaciones;

        public CN_Autorizacion()
        {
            autorizaciones = new CD_Autorizacion();
        }


        public void CrearAutorizacion(Autorizaciones autorizacion)
        {
            autorizaciones.CrearAutorizacion(autorizacion);
        }
        public void ActualizarAutorizacion(int Codigo,String Estado)
        {
            autorizaciones.ActualizarAutorizacion(Codigo, Estado);
        }

        public bool VerificarExistancia(int Codigo)
        {
            return autorizaciones.VerificarExistencia(Codigo);
        }

        public List<Autorizaciones> GetAutorizacionesPendientes()
        {
            return autorizaciones.GetAutorizacionesPendientes();
        }
        public Autorizaciones ObtenerAutorizacionVigente(string cuentaOrigen, string identificacionOrigen, string cuentaDestino, string identificacionDestino)
        {
          

            return autorizaciones.GetAutorizacionVigente(cuentaOrigen, identificacionOrigen, cuentaDestino, identificacionDestino);
        }
    }
}
