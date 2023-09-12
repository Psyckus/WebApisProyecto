using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Clientes
    {
        private CD_Usuarios Usuarios;
        private CD_Clientes clientes;
        public CN_Clientes()
        {
            Usuarios = new CD_Usuarios();
            clientes = new CD_Clientes();

        }

        public void CrearCliente(Clientes cliente)
        {
            // Validaciones y reglas de negocio

            // Llamada al método en la capa de datos
            try
            {
                clientes.CrearCliente(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarCliente(Clientes cliente)
        {
            // Validaciones y reglas de negocio

            // Llamada al método en la capa de datos
            clientes.ActualizarCliente(cliente);
        }

        public void EliminarCliente(string identificacion)
        {
            // Validaciones y reglas de negocio

            // Llamada al método en la capa de datos
            clientes.EliminarCliente(identificacion);
        }

        public Clientes ObtenerClientePorIdentificacion(string identificacion)
        {
            // Validaciones y reglas de negocio

            // Llamada al método en la capa de datos
            return clientes.ObtenerClientePorIdentificacion(identificacion);
        }

        public List<Clientes> ObtenerClientesPorNombre(string nombre)
        {
            // Validaciones y reglas de negocio

            // Llamada al método en la capa de datos
            return clientes.ObtenerClientesPorNombre(nombre);
        }

        public List<Clientes> ObtenerClientesPorApellidos(string apellido1,string apellido2)
        {
            // Validaciones y reglas de negocio

            // Llamada al método en la capa de datos
            return clientes.ObtenerClientesPorApellidos(apellido1,apellido2);
        }

        public string ConsultarNombreCliente(string numCuenta)
        {

            return clientes.ConsultarNombreCliente(numCuenta);
        
        
        }
        public bool VerificarUsuarioExistente(string username)
        {
            bool usuarioExiste = Usuarios.VerificarUsuarioExistente(username);
            return usuarioExiste;
        }
        public List<Cuenta> ObtenerCuentasActivas(string NCuenta)
        {
         
            return clientes.ObtenerCuentasActivas(NCuenta);
        }
    }
}
