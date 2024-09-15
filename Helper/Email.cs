using System.Net;
using System.Net.Mail;

namespace CrudMVC.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string nome = _configuration.GetValue<string>("SMTP:Nome");
                string userName = _configuration.GetValue<string>("SMTP:UserName");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, nome), //de: email, displayName
                };
                mail.To.Add(email); //para: email
                mail.Subject = assunto; //assunto
                mail.Body = mensagem; //mensagem
                mail.IsBodyHtml = true; //permite passar códigos html para dentro do email
                mail.Priority = MailPriority.High; //tenta mandar o email o mais rápido possível

                using(SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(userName, senha); //passa as credenciais do email
                    smtp.EnableSsl = true; //passa o email de forma segura

                    smtp.Send(mail); //envia o email

                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
