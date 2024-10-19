using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add service configurations
builder.Services.AddServiceConfigurations(builder);

var app = builder.Build();

// Add app configurations
app.AddAppConfigurations(builder);

app.Run();
