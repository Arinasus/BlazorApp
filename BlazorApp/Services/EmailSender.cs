using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace BlazorApp.Services
{
    public class EmailSender : IEmailSender, IEmailSender<ApplicationUser>
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailSettings = _config.GetSection("EmailSettings");

            using (var client = new SmtpClient(mailSettings["SmtpServer"], int.Parse(mailSettings["SmtpPort"])))
            {
                client.Credentials = new NetworkCredential(mailSettings["Username"], mailSettings["Password"]);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mailSettings["SenderEmail"], "BlazorAppSupport"),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки почты: {ex.Message}");
                    throw;
                }
            }
        }
            public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            string subject = "Подтверждение регистрации";
            string message = $"Пожалуйста, подтвердите ваш аккаунт, перейдя по <a href='{confirmationLink}'>ссылке</a>.";

            await SendEmailAsync(email, subject, message);
        }

        public Task SendTwoFactorCodeAsync(ApplicationUser user, string email, string twoFactorCode)
        {
            string subject = "Код подтверждения входа (2FA)";
            string message = $" Ваш одноразовый код для входа в систему: <strong>{twoFactorCode}</strong>. Никому не сообщайте его.";

            return SendEmailAsync(email, subject, message);
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            SendEmailAsync(email, "Сброс пароля", $"Для сброса пароля перейдите по <a href='{resetLink}'>ссылке</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            SendEmailAsync(email, "Код сброса пароля", $"Ваш код для сброса пароля: {resetCode}");
    }
}
    

