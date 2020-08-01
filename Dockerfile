FROM mcr.microsoft.com/dotnet/core/sdk:5.0 as build-env
WORKDIR /app
COPY . .
RUN dotnet restore CatchAllApi.sln --source https://api.nuget.org/v3/index.json --source http://nuget/nuget
RUN dotnet publish CatchAllApi.WebApplication/CatchAllApi.WebApplication.csproj --configuration Release --output /app/publish --runtime linux-x64

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
EXPOSE 80/tcp 443/tcp
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "CatchAllApi.WebApplication.dll"]
