using EmailAPI.Models;
using EmailAPI.Settings;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;
        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            _emailSetting = emailSettings.Value;
        }
        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\welcome.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(_emailSetting.EMail, _emailSetting.DisplayName);
            message.To.Add(new MailAddress(request.ToEmail));
            message.Subject = $"Welcome {request.UserName}";
            message.IsBodyHtml = true;
            message.Body = MailText;
            smtp.Port = _emailSetting.Port;
            smtp.Host = _emailSetting.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_emailSetting.EMail, _emailSetting.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }
}
