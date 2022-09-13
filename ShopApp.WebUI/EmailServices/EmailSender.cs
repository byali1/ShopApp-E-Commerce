using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ShopApp.WebUI.EmailServices
{
    public class EmailSender:IEmailSender
    {
        private const string sendGridKey = "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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(sendGridKey,subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("unknown.shra@gmail.com", "Shop App"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
