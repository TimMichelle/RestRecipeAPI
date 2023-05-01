FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
ENV APPLICATION_URL=http://0.0.0.0:8000
ENV DB_HOST=database
ENV DB_PORT=5432
ENV DB_DATABASENAME=restRecipesDB
ENV DB_USERNAME=develop
ENV DB_PASSWORD=lekkereten

RUN sh ./configure-appsettings.sh

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