FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RestRecipeApp/RestRecipeApp.csproj", "RestRecipeApp/"]
RUN dotnet restore "RestRecipeApp/RestRecipeApp.csproj"
COPY . .
WORKDIR "/src/RestRecipeApp"
RUN dotnet build "RestRecipeApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestRecipeApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestRecipeApp.dll"]
