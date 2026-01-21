# Trivia Game - Guía Rápida

Juego de trivia desarrollado en **C# y .NET 8**, siguiendo principios de **arquitectura limpia**. Permite registrar usuarios, iniciar sesión, elegir categorías y jugar partidas con preguntas aleatorias.

---

## Funcionalidades principales
- Registro y login de usuarios con validaciones básicas.  
- Selección de **categorías de trivia**.  
- Cada partida muestra **3 preguntas** con 3 respuestas posibles (1 correcta, 2 incorrectas).  
- Toda la información se maneja en **SQL Server** mediante **Stored Procedures**.  
- Puede ejecutarse usando **Docker y .NET Aspire** para levantar API y cliente juntos.  

---

## Tecnologías
- **Backend / API**: .NET 8, ASP.NET Core Web API, JWT, ADO.NET, Stored Procedures  
- **DTOs**: Estructuras para enviar/recibir datos sin exponer entidades de base  
- **Frontend / Cliente**: ASP.NET Core MVC, Bootstrap / CSS3  
- **Infraestructura**: SQL Server, SqlConnectionFactory, Docker, .NET Aspire  

---

##  Estructura del proyecto
- **Domain** → Entidades y clases base  
- **DTOs** → Modelos de transferencia de datos  
- **Application** → Lógica de negocio y contratos  
- **Infrastructure** → Repositorios y manejo de base de datos  
- **Api** → Endpoints para usuarios y preguntas  
- **AppWeb** → Cliente MVC  
- **AspireApp** → Orquestador para levantar API y AppWeb juntos  

---

##  Cómo ejecutar

### Requisitos
- .NET 8 SDK  
- SQL Server (LocalDB o instancia completa)  
- Docker (opcional)  

### Pasos
1. **Base de datos**  
   Ejecutar scripts en `TriviaGame.Infrastructure/Persistence/Scripts/`:  
   - `script_tables.sql`  
   - `Stored_Procedures.sql`  
   - `seed_preguntas.sql`  

2. **Levantar la app**  
   - **Opción 1:** Usar Docker y `TriviaGame.AspireApp.AppHost`  
   - **Opción 2:** Ejecutar primero `TriviaGame.Api` y luego `TriviaGame.AppWeb` desde Visual Studio  

---

##  Notas
- Cada categoría tiene mínimo 10 preguntas  
- Contraseñas seguras con **BCrypt hashing**  
- La app está lista para probar localmente
