namespace ApplicationCore.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string destinationEmailAddr, string subject, string emailBody);
}
