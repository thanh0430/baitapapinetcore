using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.SendEmailSevice
{
    public class SendEmailSevice : ISendEmail
    {
        private readonly IConfiguration _configuration;

        public SendEmailSevice(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(EmailViewModel emailViewModel)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            string smtpServer = emailSettings["SmtpServer"];
            int smtpPort = int.Parse(emailSettings["SmtpPort"]);
            string smtpUsername = emailSettings["SmtpUsername"];
            string smtpPassword = emailSettings["SmtpPassword"];
            string fromEmail = emailSettings["FromEmail"];

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail);
                mailMessage.To.Add(emailViewModel.ToEmail);
                mailMessage.Subject = emailViewModel.Subject;
                mailMessage.Body = emailViewModel.Body;
                mailMessage.IsBodyHtml = true;

                await client.SendMailAsync(mailMessage);}
        }
    }
}
