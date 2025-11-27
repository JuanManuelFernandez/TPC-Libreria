using System;
using System.Net;
using System.Net.Mail;

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
                Credentials = new NetworkCredential("utnfrgplibreria@gmail.com", "vvpt zzcf ytpb reuf"), // Clave de Google App Libreria UTN FRGP
                EnableSsl = true,
                Port = 587,
                Host = "smtp.gmail.com"
            };
        }
        public void ArmarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage
            {
                From = new MailAddress("libreriautnfrgp@gmail.com", "Libreria UTN FRGP")
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
