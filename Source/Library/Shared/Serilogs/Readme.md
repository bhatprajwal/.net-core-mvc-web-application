# Serilog Helper
Add below code to Program.cs of the start-up project

### Add to service configurations
```csharp
builder.AddSerilog();
```

### Usage guide in Controller
```csharp
public class HomeController : ControllerBase
{
    #region Private Member
    private readonly ILogger<HomeController> _logger;
    #endregion

    #region Constructor
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    #endregion

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("SeriLog is configured");
    }
}
```

## Note
1. seri-log-config.json -> Properties -> Set Copy to Output Directory to Copy if newer
2. Choose the path to save the log file in seri-log-config.json