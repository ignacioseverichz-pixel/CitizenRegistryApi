\# Citizen Registry API



Práctica 2 de Certificación 1.



\## Descripción

API desarrollada en ASP.NET Core Web API para administrar ciudadanos mediante operaciones CRUD, integración con API externa, persistencia en CSV, Swagger, logging y uso de Git Flow.



\## Endpoints

\- POST /api/Citizen

\- GET /api/Citizen

\- GET /api/Citizen/{ci}

\- PUT /api/Citizen/{ci}

\- DELETE /api/Citizen/{ci}



\## Git Flow

Ramas utilizadas:

\- main

\- develop

\- P2-001



\## 12 Factor App Principles



\### 1. Codebase

El proyecto usa una sola base de código versionada con Git y alojada en GitHub.



\### 2. Dependencies

Las dependencias se manejan explícitamente mediante NuGet y el archivo `.csproj`.



\### 3. Config

La configuración se maneja fuera del código con `appsettings.json`, incluyendo:

\- URL de la API externa

\- ruta del archivo CSV

\- logging



\### 4. Backing Services

La API externa `https://api.restful-api.dev/objects` se usa como servicio adjunto para asignar el `PersonalAsset`.



\### 5. Build, Release, Run

El proyecto sigue separación entre compilación, versión y ejecución usando comandos de .NET y control de versiones con Git.



\### 6. Processes

La API se ejecuta como un proceso stateless. Los datos persistentes se guardan en CSV.



\### 7. Port Binding

La aplicación se expone mediante un puerto HTTP local a través de ASP.NET Core.



\### 8. Concurrency

La solución puede escalar ejecutando múltiples instancias del proceso si fuera necesario.



\### 9. Disposability

La aplicación inicia y se detiene rápidamente, permitiendo pruebas y reinicios ágiles.



\### 10. Dev/Prod Parity

Se mantiene una estructura similar entre desarrollo y producción, usando el mismo tipo de configuración y dependencias.



\### 11. Logs

Los logs se generan mediante el sistema de logging de .NET para eventos y errores.



\### 12. Admin Processes

Las tareas administrativas como revisión del CSV o mantenimiento pueden ejecutarse de forma separada del proceso principal.

