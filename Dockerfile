FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
#ENV ASPNETCORE_URLS=http://0.0.0.0:8000
#ENV ConnectionStrings__DbConnection=Server=database;Port=5432;Database=restRecipesDB;Username=develop;Password=lekkereten

RUN dotnet restore
RUN dotnet build "RestRecipeApp.Api/RestRecipeApp.Api.csproj" -c Release -o /app/build
RUN dotnet build "RestRecipeApp.Core/RestRecipeApp.Core.csproj" -c Release -o /app/build
RUN dotnet build "RestRecipeApp.Persistence/RestRecipeApp.Persistence.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestRecipeApp.Api/RestRecipeApp.Api.csproj" -c Release -o /app/publish
RUN dotnet publish "RestRecipeApp.Core/RestRecipeApp.Core.csproj" -c Release -o /app/publish
RUN dotnet publish "RestRecipeApp.Persistence/RestRecipeApp.Persistence.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY run-rest-api.sh /app
COPY wait-for-it.sh /app
RUN apt-get update && apt-get install -y netcat 
ENTRYPOINT ["sh","/app/run-rest-api.sh"]
