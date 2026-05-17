using ComplaintPortal.Models;

namespace ComplaintPortal.Services;

public interface IComplaintEmailService
{
    Task SendComplaintAsync(ComplaintFormModel complaint);
}
