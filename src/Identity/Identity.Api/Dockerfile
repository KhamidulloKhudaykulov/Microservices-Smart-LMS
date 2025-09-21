# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files first (layer cache)
COPY src/Identity/Identity.Api/Identity.Api.csproj Identity.Api/
COPY src/Identity/Identity.Application/Identity.Application.csproj Identity.Application/
COPY src/Identity/Identity.Domain/Identity.Domain.csproj Identity.Domain/
COPY src/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj Identity.Infrastructure/

# Restore nuget
RUN dotnet restore Identity.Api/Identity.Api.csproj

# Copy all source code
COPY src/Identity/ ./

# Build
WORKDIR /src/Identity.Api
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
 