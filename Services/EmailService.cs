using Azure.Communication.Email;
using Onatrix.Interfaces;

namespace Onatrix.Services;

public class EmailService(EmailClient emailClient, IConfiguration config): IEmailService
{
  private readonly EmailClient _emailClient = emailClient;
  private readonly IConfiguration _config = config;

  public async Task<bool> SendConfirmationEmailAsync(string email)
  {
    if (string.IsNullOrEmpty(email))
      throw new ArgumentException("Email cannot be null or empty.");

    var subject = "Email Confirmation";
    var plainTextContent = $@"
      Thank you for reaching out to us. We have received your request and will get back to you as soon as possible. {email}
      © 2025 Your Company. All rights reserved.";
    var htmlContent = $@"
      <body style=""font-family: Arial, sans-serif; background-color: #f4f4f7; color: #333333; margin: 0; padding: 0;"">
      <div class=""container"" style=""max-width: 600px; margin: 40px auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); padding: 20px;"">
      <div style=""background-color: #4F5955; color: white; padding: 20px; border-radius: 8px 8px 0 0; text-align: center; font-size: 24px; font-weight: bold;"">
      Email Confirmation
      </div>
      <div style=""padding: 20px; line-height: 1.6;"">
      <p style=""margin: 0 0 15px 0;"">Thank you for reaching out to us. We have received your request and will get back to you as soon as possible. {email}</p>
      </div>
      <div style=""font-size: 12px; color: #888888; text-align: center; padding: 15px;"">
      © 2025 Your Company. All rights reserved.
      </div>
      </div>
      </body>";

    var emailMessage = new EmailMessage(
      senderAddress: _config["ACS:Sender"],
      recipients: new EmailRecipients([new(email)]),
      content: new EmailContent(subject)
      {
        PlainText = plainTextContent,
        Html = htmlContent
      });

    var result = await _emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);
    if (result.Value.Status != EmailSendStatus.Succeeded)
      throw new Exception($"Failed to send email to {email}. Status: {result.Value.Status}");

    return true;
  }
  
}
