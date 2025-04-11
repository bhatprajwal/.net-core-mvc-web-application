using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using Web.Dtos;

namespace Web.Features.Controllers;

/// <summary>
/// Home Controller
/// </summary>
public class HomeController : Controller
{

    #region Private Member
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer _localizer;
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="factory"></param>
    public HomeController(
        ILogger<HomeController> logger
        , IStringLocalizerFactory factory
    )
    {
        _logger = logger;
        _localizer = factory.Create(typeof(HomeController));
    }
    #endregion

    /// <summary>
    /// Index
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        _logger.LogInformation("App starteds");
        return View();
    }

    /// <summary>
    /// Privacy
    /// </summary>
    /// <returns></returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Error
    /// </summary>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
