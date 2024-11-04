# Use the .NET SDK image for building and restoring packages
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy only the .csproj file and restore dependencies with explicit source
COPY Services/MLService/MLService.csproj ./
RUN dotnet restore MLService.csproj --source https://api.nuget.org/v3/index.json

# Copy the rest of the application files and build
COPY Services/MLService/. ./
RUN dotnet publish -c Release -o /app/publish

# Use the ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MLService.dll"]
