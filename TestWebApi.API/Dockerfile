# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file first (important!)
COPY *.csproj ./

# Restore NuGet packages with diagnostic verbosity
RUN dotnet restore --verbosity diagnostic

# Copy the rest of the source code
COPY . ./

# Publish the project
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the port environment variable so Render knows which port to use
ENV ASPNETCORE_URLS=http://+:$PORT

# Start the API
ENTRYPOINT ["dotnet", "TestWebApi.API.dll"]