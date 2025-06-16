# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar todo el repositorio
COPY . .

# Cambiar al proyecto API
WORKDIR /src/BackendTest.API

# Restaurar y publicar
RUN dotnet restore "BackendTest.API.csproj"
RUN dotnet publish "BackendTest.API.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copiar resultados del build
COPY --from=build /app/publish .

# Exponemos el puerto (importante para Render)
EXPOSE 80

# Comando de arranque
ENTRYPOINT ["dotnet", "BackendTest.API.dll"]
