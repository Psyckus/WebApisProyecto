using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Usuarios
    {

        private string connectionString = Conexion.cn;
        //Le falta poner consulta para saber si esta activo o no
        public bool ValidarCredenciales(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Credenciales WHERE Username = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        public bool VerificarUsuarioExistente(string username)
        {
            bool existe = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("VerificarNumIdentificacionExistente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NumIdentificacion", username);
                    command.Parameters.Add("@Existe", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    existe = (bool)command.Parameters["@Existe"].Value;
                }
            }

            return existe;
        }

        public  bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool restultado = false;

            try
            {
                //Objeto de mail
                MailMessage mail = new MailMessage();
                //Correo a donde se va a enviar 
                mail.To.Add(correo);
                //De donde se va a enviar
                mail.From = new MailAddress("unidadesunidadesr@gmail.com");
                //El asunto del mensaje
                mail.Subject = asunto;
                //El mensaje que se va a enviar
                mail.Body = mensaje;
                //Se indica que es html
                mail.IsBodyHtml = true;

                //Una variable de tipo servidor que se encarga de enviar al mensaje 
                var smtp = new SmtpClient()
                {
                    //Se agrega nuestro correo y contraseña
                    Credentials = new NetworkCredential("unidadesunidadesr@gmail.com", "ibwgmqagmmycguit"),
                    //El srvidor que utiliza gmail para los correos 
                    Host = "smtp.gmail.com",
                    //  El puerto por el que se envian 
                    Port = 587,
                    //Certificado de seguridad 
                    EnableSsl = true
                };
                //Para que envie el coreo
                smtp.Send(mail);

                //Resultado pasa a ser true
                restultado = true;




            }
            catch (Exception)
            {
                restultado = false;

            }

            return restultado;
        }
    }
}
