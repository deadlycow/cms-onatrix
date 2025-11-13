namespace Onatrix.Interfaces;
public interface IEmailService
{
  Task<bool> SendConfirmationEmailAsync(string email);
}