FROM mcr.microsoft.com/dotnet/core/sdk:5.0 as build-env
WORKDIR /app

COPY . .
RUN dotnet restore CatchAllApi.sln -s https://api.nuget.org/v3/index.json -s http://nuget/nuget
RUN dotnet publish CatchAllApi.WebApplication/CatchAllApi.WebApplication.csproj -c Release -o /app/publish -r linux-x64

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "CatchAllApi.WebApplication.dll"]
