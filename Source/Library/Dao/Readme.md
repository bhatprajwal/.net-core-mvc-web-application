# Dao Helper
### Add below code to Program.cs of the start-up project

# Choose between Database | MS SQL | My SQL
### Use HelperExtensions to set the default configuration
### 1. Delete the initial Migration folder/files
### 2. Re-run the migrations for the updated/configured EF

### Add to service configurations
```csharp
service.AddDao(builder);
```

### Use below configuration inside HelperExtension to choose different DB - MSSQL | MySQL
```
services.AddMSSQLContext(builder);
```
## OR
```
services.AddMySQLContext(builder);
```

### Add to appsettings.json - My SQL
```json
"ConnectionStrings": {
	"Prod": "Server=localhost;Port=3306;Database=DatabaseName;User=username;Password=password;CharSet=utf8;"
	"Dev": "Server=localhost;Port=3306;Database=DatabaseName;User=username;Password=password;CharSet=utf8;"
}
```
## OR
### Add to appsettings.json - MSSQL
```json
"ConnectionStrings": {
	"Prod": "Data Source=localhost;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
	"Dev": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebDb;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=True;User Instance=False;"
}
```

## Running commands
### Switch to DaoHelper library in Package Manager Console (always)

## Commands
### Create Migration
```
Add-Migration -Name <Name> -Context ApplicationDbContext;
```

### Update Database
```
update-database;
```

## Add below models in the start-up project or Model library
### UserRole.cs
```csharp
[Serializable]
public class UserRole : IdentityRole
{

}
```

### ApplicationUser.cs
```csharp
[Serializable]
public class ApplicationUser : IdentityUser
{

}
```
