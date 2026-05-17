namespace ComplaintPortal.Models;

public class SmtpSettings
{
    public const string SectionName = "SmtpSettings";

    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string SenderPassword { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
}
