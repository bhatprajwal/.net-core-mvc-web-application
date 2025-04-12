# Google Authentication Support
Use this document to configure the Google authentication with .net core MVC application
This document guide to how to integrate google auth integration with .Net MVC app

## Service Configuration
Set the below code in your service configuration of Program.cs
```
// Google Authentication
service.AddGoogleAuthentication(webApplicationBuilder);
```

## Add a block to appsettings.json
```
"Authentication": {
    "Google": {
      "ClientId": "",
      "ClientSecret": ""
    }
}
```

Note: Get this ClientId and ClientSecrete by registering 
Steps
1. Login with https://console.cloud.google.com/apis/credentials
2. Create Credentials
3. Select OAuth ClientId
4. Select Web Application
5. Provide Name
6. Authorised JS Origins - https://localhost:7041
7. Authorised redirect URLs - https://localhost:7041/signin-google
8. Save

Note: Replace localhost with your actual URL dev/prod URL
Update the ClientId and ClientSecrete to appsettings.json 

## User
User is updated with below props
```
using Microsoft.AspNetCore.Identity;

namespace Entity;

/// <summary>
/// Application User
/// </summary>
[Serializable]
public class ApplicationUser : IdentityUser
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    public ApplicationUser()
    {} 
    #endregion

    /// <summary>
    /// Full Name
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Profile Picture
    /// </summary>
    public string ProfilePictureUrl { get; set; }
}
```

Note: Please run the migration script with this integration

## UI
Login Partial page of MVC is updated with below code block to showcase signed-in Google User
``` _LoginPartial.cshtml
@using Microsoft.AspNetCore.Identity;
@using Entity;
@inject SignInManager<ApplicationUser> SignInManager
@inject UserManager<ApplicationUser> UserManager

@if (SignInManager.IsSignedIn(User))
{
    var user = await UserManager.GetUserAsync(User);
    <div class="d-flex align-items-center gap-2">
        @if (!string.IsNullOrEmpty(user?.ProfilePictureUrl))
        {
            <img src="@user.ProfilePictureUrl" alt="Profile" width="32" height="32" class="rounded-circle" />
        }
        <span class="text-dark">@user?.FullName</span>

        <form asp-controller="GoogleAuth" asp-action="Logout" method="post" class="ms-2">
            <button type="submit" class="text-dark">Logout</button>
        </form>
    </div>
}
else
{
    <partial name="_ExternalLogin" />
}
```

## Navbar !Navbar.cshtml
Call the partial view to inject login actions
```
 @await Html.PartialAsync("_LoginPartial")
```

## Partial View - External Login
``` _ExternalLogin.cshtml
<a asp-controller="GoogleAuth" asp-action="ExternalLogin" asp-route-provider="Google">
    <i class="fab fa-google"></i>
</a>
```

## Controller with Action
```
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
```

## app configs in Program.cs
Follow the order to support Google/External Logins
```
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
        
app.MapControllers();
app.MapRazorPages();
```
