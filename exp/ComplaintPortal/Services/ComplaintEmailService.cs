using System.Net;
using System.Net.Mail;
using System.Text;
using ComplaintPortal.Models;
using Microsoft.Extensions.Options;

namespace ComplaintPortal.Services;

public class ComplaintEmailService : IComplaintEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public ComplaintEmailService(IOptions<SmtpSettings> smtpOptions)
    {
        _smtpSettings = smtpOptions.Value;
    }

    public async Task SendComplaintAsync(ComplaintFormModel complaint)
    {
        ValidateSettings();

        using var message = new MailMessage();
        message.From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName);
        message.To.Add(_smtpSettings.RecipientEmail);
        message.Subject = $"Complaint: {complaint.Subject}";
        message.Body = BuildBody(complaint);
        message.IsBodyHtml = false;
        message.ReplyToList.Add(new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName));

        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            EnableSsl = _smtpSettings.EnableSsl,
            Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.SenderPassword)
        };

        await client.SendMailAsync(message);
    }

    private void ValidateSettings()
    {
        if (string.IsNullOrWhiteSpace(_smtpSettings.Host) ||
            string.IsNullOrWhiteSpace(_smtpSettings.SenderEmail) ||
            string.IsNullOrWhiteSpace(_smtpSettings.SenderPassword) ||
            string.IsNullOrWhiteSpace(_smtpSettings.RecipientEmail))
        {
            throw new InvalidOperationException("SMTP settings are incomplete.");
        }
    }

    private static string BuildBody(ComplaintFormModel complaint)
    {
        var builder = new StringBuilder();
        builder.AppendLine("New complaint received from the public complaint portal.");
        builder.AppendLine();
        builder.AppendLine($"Full Name: {complaint.FullName}");
        builder.AppendLine($"Mobile Number: {complaint.PhoneNumber}");
        builder.AppendLine($"Village / City: {complaint.Location}");
        builder.AppendLine($"Complaint Source: {complaint.ComplaintSource}");
        builder.AppendLine($"Subject: {complaint.Subject}");
        builder.AppendLine();
        builder.AppendLine("Complaint Details:");
        builder.AppendLine(complaint.Message);
        return builder.ToString();
    }
}
