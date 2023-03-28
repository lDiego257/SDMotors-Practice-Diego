# SDMotors API Practice 

El presente proyecto es un web API con el proposito de gestionar vehiculos, programado con la tecnologia de .Net Core 6.

## Prerrequisitos
* .NET 6.0.X + SDK
* SQL Server

## Instalación

1. Clonar repositorio
```
git clone https://github.com/lDiego257/SDMotors-Practice-Diego.git
```
2. Agregar el Connection String de la base de datos local a utilizar en SDMotros.Api/appsettings.json
```JSON
  "ConnectionStrings": {
    "SDMotorsApiContext": "{AQUI VA EL CONNECTION STRING}"
  }
```
3. Crear la base de datos
```
dotnet ef database update
```
4. Correr el programa
```
dotnet run program.cs
```
5. Por default el puerto donde el servidor esta escuchando es el 7147
```
https://localhost:7147/swagger/index.html
```