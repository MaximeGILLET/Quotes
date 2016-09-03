using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Quotes.Models
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {
            var sg = new SendGridAPIClient("e5bbc4d0-c13b-49ae-8e72-ed449c248ce3");
            Email from = new Email("test@example.com");
            string subject = "Hello World from the SendGrid CSharp Library!";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Hello, Email!");
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
          
            Trace.TraceError("Failed to create Web transport.");
            await Task.FromResult(0);
            
        }
    }
}