# Set the base image to use
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Create a final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port
EXPOSE 5000

# Start the app
ENTRYPOINT ["dotnet", "BusinessEconomyManager.dll"]

