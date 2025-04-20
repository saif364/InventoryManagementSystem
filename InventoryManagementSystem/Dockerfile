# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["InventoryManagementSystem/InventoryManagementSystem.csproj", "."]
RUN dotnet restore "InventoryManagementSystem/InventoryManagementSystem.csproj"

# Copy the rest of the application code and build it
COPY . .
WORKDIR "/src/YourProject"
RUN dotnet build "InventoryManagementSystem.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "InventoryManagementSystem.csproj" -c Release -o /app/publish

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published application from the previous stage
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "InventoryManagementSystem.dll"]