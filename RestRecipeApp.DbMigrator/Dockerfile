FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestRecipeApp.DbMigrator/RestRecipeApp.DbMigrator.csproj", "RestRecipeApp.DbMigrator/"]
RUN dotnet restore "RestRecipeApp.DbMigrator/RestRecipeApp.DbMigrator.csproj"
COPY . .
WORKDIR "/src/RestRecipeApp.DbMigrator"
RUN dotnet build "RestRecipeApp.DbMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestRecipeApp.DbMigrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestRecipeApp.DbMigrator.dll"]
