FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Bidding/Bidding.API/Bidding.API.csproj", "Services/Bidding/Bidding.API/"]
RUN dotnet restore "Services/Bidding/Bidding.API/Bidding.API.csproj"
COPY . .
WORKDIR "/src/Services/Bidding/Bidding.API"
RUN dotnet build "Bidding.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bidding.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bidding.API.dll"]
