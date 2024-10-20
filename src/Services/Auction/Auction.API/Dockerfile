# Use the .NET SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the BuildingBlocks project first
COPY src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/BuildingBlocks/
COPY src/Services/Auction/Auction.API/Auction.API.csproj src/Services/Auction/Auction.API/

# Restore dependencies for both projects
RUN dotnet restore "src/Services/Auction/Auction.API/Auction.API.csproj"

# Copy all source files for the entire solution
COPY . .

# Set the working directory and build the Auction.API project
WORKDIR /app/src/Services/Auction/Auction.API
RUN dotnet build "Auction.API.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "Auction.API.csproj" -c Release -o /app/publish

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auction.API.dll"]
