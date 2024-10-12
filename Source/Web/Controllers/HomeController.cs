using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using Web.Dtos;

namespace Web.Controllers;

public class HomeController : Controller
{

    #region Private Member
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer _localizer;
    #endregion

    #region Constructor
    public HomeController(
        ILogger<HomeController> logger
        , IStringLocalizerFactory factory
    )
    {
        _logger = logger;
        _localizer = factory.Create(typeof(HomeController));
    }
    #endregion

    public IActionResult Index()
    {
        _logger.LogInformation("App starteds");
        return View();
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
