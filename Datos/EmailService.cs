using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EmailService
    {

        private MailMessage email;
        private SmtpClient server;
        public EmailService()
        {
            server = new SmtpClient
            {
                Credentials = new NetworkCredential("libreriautn@gmail.com", "???"), // Clave de AppLibreria
                EnableSsl = true,
                Port = 587,
                Host = "smtp.gmail.com"
            };
        }
        public void ArmarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage
            {
                From = new MailAddress("libreriautn@gmail.com", "CallCenter UTN") // Cambiar
            };
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }
        public void EnviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
