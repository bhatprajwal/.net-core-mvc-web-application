# I18N Helper
Add below code to Program.cs of the start-up project

### Add to service configurations
```csharp
service.AddI18NLocalizer();
```

### Add to app configurations
```csharp
app.AddAppI18NLocalizer(builder);
```

### Add language files under Resource folder in start-up project
Resources/en-US.json
```json
	{
	  "common-label": {
		"name": "",
		"title": ""
	  }
	}
```

Resources/fr-FR.json
```json
	{
	  "common-label": {
		"name": "",
		"title": ""
	  }
	}
```

# Usage with UI
### Home\index.cshtml
```
@using Microsoft.Extensions.Localization
@using Web.Controllers
@inject IStringLocalizer<HomeController> Localizer

@{
    ViewData["Title"] = "Home Page";
}

<h1>@Localizer["name"]</h1>
```

# Usage with Controller
### HomeController.cs
```
public class HomeController : Controller
{
    #region Private Member
    private readonly IStringLocalizer _localizer;
    #endregion

    #region Constructor
    public HomeController(
        IStringLocalizerFactory factory
    )
    {
        _localizer = factory.Create(typeof(HomeController));
    }
    #endregion

    public IActionResult Index()
    {
        var test = _localizer["name"];
        return View();
    }
}
```
