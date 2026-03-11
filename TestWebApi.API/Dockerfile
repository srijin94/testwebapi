# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Fix here:
COPY --from=build /app .   # Copy published app from build stage to runtime

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "TestWebApi.dll"]