# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files first (layer cache)
COPY src/Services/UserService/UserService.Api/UserService.Api.csproj UserService.Api/
COPY src/Services/UserService/UserService.Application/UserService.Application.csproj UserService.Application/
COPY src/Services/UserService/UserService.Domain/UserService.Domain.csproj UserService.Domain/
COPY src/Services/UserService/UserService.Infrastructure/UserService.Infrastructure.csproj UserService.Infrastructure/
COPY src/Services/UserService/UserService.Persistence/UserService.Persistence.csproj UserService.Persistence/

# Restore nuget
RUN dotnet restore UserService.Api/UserService.Api.csproj

# Copy all source code
COPY src/Services/UserService/ ./

# Build
WORKDIR /src/UserService.Api
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
