FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY BackendTest.sln .
COPY BackendTest.API/*.csproj BackendTest.API/
COPY BackendTest.Database/*.csproj BackendTest.Database/
COPY BackendTest.Domain/*.csproj BackendTest.Domain/

RUN dotnet restore BackendTest.sln

COPY . .

WORKDIR /src/BackendTest.API

RUN dotnet publish "BackendTest.API.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BackendTest.API.dll"]