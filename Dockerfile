# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY productsrad-bc/*.csproj ./productsrad-bc/
RUN dotnet restore ./productsrad-bc/productsrad-bc.csproj

# Copiar el resto de los archivos y compilar
COPY . .
WORKDIR /app/productsrad-bc
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/productsrad-bc/out .

# Exponer el puerto
EXPOSE 80
ENTRYPOINT ["dotnet", "productsrad-bc.dll"]
