namespace ApplicationCore.Constants;

public class EmailSendingConstants
{
    public static readonly string EmailAddress = Environment.GetEnvironmentVariable("EmailAddress")!;
    public static readonly string Password = Environment.GetEnvironmentVariable("Password")!;
}
