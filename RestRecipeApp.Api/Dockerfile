FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestRecipeApp/RestRecipeApp.Api.csproj", "RestRecipeApp/"]
RUN dotnet restore "RestRecipeApp/RestRecipeApp.csproj"
COPY . .
WORKDIR "/src/RestRecipeApp"
RUN dotnet build "RestRecipeApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestRecipeApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestRecipeApp.dll"]