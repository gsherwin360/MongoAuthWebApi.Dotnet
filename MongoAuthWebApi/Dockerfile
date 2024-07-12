# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project files and restore dependencies
COPY ["MongoAuthWebApi.csproj", "./"]
RUN dotnet restore "./MongoAuthWebApi.csproj"

# Copy the entire application code
COPY . .

# Build the application
RUN dotnet build "./MongoAuthWebApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./MongoAuthWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET base image for runtime
FROM base AS final
WORKDIR /app
EXPOSE 8080

# Copy the published artifacts from the build stage
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "MongoAuthWebApi.dll"]
