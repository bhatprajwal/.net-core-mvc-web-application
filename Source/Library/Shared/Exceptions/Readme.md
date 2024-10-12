# Exception Helper
Add below code to Program.cs of the start-up project

### Add to service configurations
```csharp
service.AddTransient<ExceptionMiddleware>();
```

### Add to app configurations
```csharp
app.UseMiddleware<ExceptionMiddleware>();
```
