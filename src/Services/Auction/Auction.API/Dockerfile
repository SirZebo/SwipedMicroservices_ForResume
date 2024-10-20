# Use the .NET SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the BuildingBlocks project first
COPY src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/BuildingBlocks/
COPY src/Services/Bidding/Bidding.API/Bidding.API.csproj src/Services/Bidding/Bidding.API/

# Restore dependencies for both projects
RUN dotnet restore "src/Services/Bidding/Bidding.API/Bidding.API.csproj"

# Copy all source files for the entire solution
COPY . .

# Set the working directory and build the Bidding.API project
WORKDIR /app/src/Services/Bidding/Bidding.API
RUN dotnet build "Bidding.API.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "Bidding.API.csproj" -c Release -o /app/publish

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bidding.API.dll"]
