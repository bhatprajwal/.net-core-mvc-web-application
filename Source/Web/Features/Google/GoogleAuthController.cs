using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Features.Controllers;

/// <summary>
/// Google Controller
/// </summary>
public class GoogleAuthController : Controller
{
    #region Private Members
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    #endregion

    #region Constructor
    public GoogleAuthController (
        SignInManager<ApplicationUser> signInManager
        , UserManager<ApplicationUser> userManager
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    #endregion

    /// <summary>
    ///  External Login
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult ExternalLogin(string provider = "Google", string returnUrl = "/")
    {
        if (string.IsNullOrWhiteSpace(provider))
            return BadRequest("Login provider is missing.");

        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "GoogleAuth", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return Challenge(properties, provider);
    }

    /// <summary>
    /// External Login Callback
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = "/")
    {
        var error = Request.Query["error"].ToString();
        if (error == "access_denied")
        {
            return RedirectToAction("LoginCancelled");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("ExternalLogin", new { provider = "Google", returnUrl }); 
        //return RedirectToAction("LoginCancelled");

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl ?? "/");
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var name = info.Principal.FindFirstValue(ClaimTypes.Name);
        var picture = info.Principal.FindFirstValue("picture");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = name,
                ProfilePictureUrl = picture
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return RedirectToAction("ExternalLogin", "GoogleAuth");
            }
        }

        var existingLogins = await _userManager.GetLoginsAsync(user);
        var hasGoogleLogin = existingLogins.Any(l => l.LoginProvider == info.LoginProvider);

        if (!hasGoogleLogin)
        {
            var loginResult = await _userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
            {
                return RedirectToAction("ExternalLogin", "GoogleAuth");
            }
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return LocalRedirect(returnUrl ?? "/");
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Login attempt is cancelled
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult LoginCancelled()
    {
        TempData["Error"] = "Google login was cancelled.";
        return RedirectToAction("Index", "Home");
    }
}
