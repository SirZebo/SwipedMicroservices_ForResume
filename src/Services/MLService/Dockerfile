# Use the .NET 8.0 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY ["MLService.csproj", "./"]
RUN dotnet restore "./MLService.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet build "MLService.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MLService.csproj" -c Release -o /app/publish

# Use the .NET 8.0 ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MLService.dll"]
