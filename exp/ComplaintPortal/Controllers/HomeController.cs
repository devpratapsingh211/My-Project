using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ComplaintPortal.Models;
using ComplaintPortal.Services;

namespace ComplaintPortal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IComplaintEmailService _complaintEmailService;

    public HomeController(ILogger<HomeController> logger, IComplaintEmailService complaintEmailService)
    {
        _logger = logger;
        _complaintEmailService = complaintEmailService;
    }

    public IActionResult Index()
    {
        return View(new ComplaintFormModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ComplaintFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _complaintEmailService.SendComplaintAsync(model);
            TempData["StatusMessage"] = "Complaint submitted successfully. It has been sent to the personal email inbox.";
            TempData["StatusType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Complaint email sending failed for {Name}", model.FullName);
            TempData["StatusMessage"] = "Complaint could not be sent right now. Please check the mail settings and try again.";
            TempData["StatusType"] = "error";
            return View(model);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
