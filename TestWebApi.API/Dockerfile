# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj from relative path from repo root
COPY TestWebApi.API/*.csproj ./
RUN dotnet restore

# Copy all source code
COPY TestWebApi.API/. ./
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:$PORT
ENTRYPOINT ["dotnet", "TestWebApi.dll"]