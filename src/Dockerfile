FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY **/*.csproj ./
RUN dotnet restore Checkin.Api.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out Checkin.Api

# Build runtime image
EXPOSE 80
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Checkin.Api.dll"]
