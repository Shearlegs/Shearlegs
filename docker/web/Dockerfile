FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /src/web/Shearlegs.Web/Shearlegs.Web.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish src/web/Shearlegs.Web/Shearlegs.Web.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80

RUN apt-get update -y
RUN apt-get install -y libgdiplus

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Shearlegs.Web.dll"]