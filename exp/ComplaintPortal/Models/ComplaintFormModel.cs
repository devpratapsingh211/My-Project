using System.ComponentModel.DataAnnotations;

namespace ComplaintPortal.Models;

public class ComplaintFormModel
{
    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Mobile Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Village / City")]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Complaint Source")]
    public string ComplaintSource { get; set; } = "Website";

    [Required]
    [StringLength(150)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Complaint Details")]
    [StringLength(4000)]
    public string Message { get; set; } = string.Empty;
}
