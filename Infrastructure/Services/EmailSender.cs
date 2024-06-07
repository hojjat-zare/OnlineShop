using ApplicationCore.Interfaces;
namespace Infrastructure.Services;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Text;

// This class is used by the application to send email for account confirmation and password reset.
// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
public class EmailSender : IEmailSender
{
    private string _password { get; set; }
    private string _userName { get; set; }
    private int _port { get; set; }
    private string _hostAddress { get; set; }

    public EmailSender(string userName,string password, string hostAddress = "smtp.gmail.com", int port = 465)
    {
        _userName = userName;
        _password = "karj vpgw aond khen";
        _hostAddress = hostAddress;
        _port = port;
    }
    public async Task SendEmailAsync(string destinationEmailAddr, string subject, string emailBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("",_userName));
        message.To.Add(new MailboxAddress("",destinationEmailAddr));
        message.Subject = subject;

        message.Body = new TextPart(TextFormat.Html)
        {
            Text = emailBody
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_hostAddress,_port, true);

            // Note: only needed if the SMTP server requires authentication
            await client.AuthenticateAsync(_userName, _password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
