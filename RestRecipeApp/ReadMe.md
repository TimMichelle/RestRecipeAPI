# Recipes App

### Run app

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
dotnet ef migrations add Add<Name>
```

```bash
dotnet ef database update
```
