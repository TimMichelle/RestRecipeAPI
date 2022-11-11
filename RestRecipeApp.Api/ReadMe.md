# Recipes App

### Run app
Add your connection string to appsettings.Development.json

First build docker image
```bash
 docker build --no-cache -t app -f Dockerfile .
```
Run app on localhost:8080
```bash
 docker run -p 8080:80 -t -i app
```

### Migrations
```bash
 dotnet ef migrations add <MigrationName>  --verbose --project RestRecipeApp.Persistence --startup-project RestRecipeApp.Api
```

```bash
dotnet ef database update --verbose --project RestRecipeApp.Persistence --startup-project RestRecipeApp.Api
```
