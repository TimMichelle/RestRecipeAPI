FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
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
ENTRYPOINT ["dotnet", "RestRecipeApp.Api.dll"]