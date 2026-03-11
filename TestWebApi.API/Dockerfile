# ------------------------------------
# Stage 1: Build
# ------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory inside container
WORKDIR /src

# Copy csproj and restore dependencies (caching)
COPY *.csproj ./
RUN dotnet restore

# Copy all source code and publish app
COPY . ./
RUN dotnet publish -c Release -o /app

# ------------------------------------
# Stage 2: Runtime
# ------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy published app from build stage
COPY --from=build /app .

# Bind to Render's PORT environment variable
ENV ASPNETCORE_URLS=http://+:$PORT

# Run the app
ENTRYPOINT ["dotnet", "TestWebApi.dll"]