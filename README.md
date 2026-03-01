
markdown
# 🤖 EmpresaMCP - Consulta de Empleados con IA Local

> Un sistema que combina **ASP.NET Core 8**, **SQL Server** y **LLM local (Ollama)** para consultar información de empleados mediante lenguaje natural.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL_Server-2019-CC2927?logo=microsoft-sql-server)
![Ollama](https://img.shields.io/badge/Ollama-Qwen_2.5-FF6B35?logo=ollama)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Descripción

**EmpresaMCP** es una aplicación web que permite consultar información de empleados de una empresa utilizando **inteligencia artificial local**. El usuario puede hacer preguntas en lenguaje natural y el sistema consulta la base de datos para responder con datos reales.

### ✨ Características principales

- 🗣️ **Chat en lenguaje natural**: Preguntá como si hablaras con una persona
- 🤖 **IA 100% local**: Usa Ollama con Qwen 2.5 Coder (sin APIs externas)
- 🔒 **Datos seguros**: La IA no accede directamente a la BD, usa APIs controladas
- ️ **Arquitectura limpia**: MVC + API REST + Entity Framework Core
- 📊 **Base de datos real**: SQL Server con tablas normalizadas

---

## 🏗️ Arquitectura del Sistema


┌─────────────────────────────────────────────────────────────┐
│ EMPRESA MCP │
├─────────────────────────────────────────────────────────────┤
│ │
│ ┌──────────────┐ ┌──────────────┐ ┌─────────────┐ │
│ │ Frontend │────▶│ Backend │────▶│ Ollama │ │
│ │ (MVC) │ │ (API) │ │ (Qwen) │ │
│ └──────────────┘ └──────────────┘ └─────────────┘ │
│ │ │ │ │
│ │ ▼ │ │
│ │ ┌──────────────┐ │ │
│ └──────────▶│ SQL Server │◀──────────────┘ │
│ │ (EmpresaDB) │ │
│ └──────────────┘ │
│ │
└─────────────────────────────────────────────────────────────┘

### Flujo de una consulta

1. **Usuario** escribe: *"¿Cuántos empleados hay en la empresa?"*
2. **MVC** recibe la pregunta y la envía al **servicio de Ollama**
3. **Ollama** analiza la pregunta y detecta que necesita datos
4. **Backend** consulta la API: `GET /api/EmpleadosApi/activos`
5. **SQL Server** devuelve los 20 empleados activos
6. **Ollama** genera la respuesta: *"Hay 20 empleados activos en la empresa"*
7. **Usuario** recibe la respuesta en el chat

---

## 🛠️ Tecnologías Utilizadas

| Categoría | Tecnología | Versión |
|-----------|-----------|---------|
| **Framework** | ASP.NET Core | 8.0 |
| **Lenguaje** | C# | 12.0 |
| **ORM** | Entity Framework Core | 8.0 |
| **Base de Datos** | SQL Server (LocalDB) | 17.0 |
| **IA Local** | Ollama | Latest |
| **Modelo LLM** | Qwen 2.5 Coder | 7B |
| **Frontend** | Razor Views + Bootstrap | 5.x |
| **IDE** | Visual Studio 2022 | 17.x |

---

## 📦 Requisitos Previos

Antes de ejecutar el proyecto, aseguráte de tener instalado:

- [ ] **.NET 8 SDK** ([Descargar](https://dotnet.microsoft.com/download/dotnet/8.0))
- [ ] **Visual Studio 2022** con workload *ASP.NET and web development*
- [ ] **SQL Server LocalDB** (incluido con VS)
- [ ] **Ollama** ([Descargar](https://ollama.com/download))
- [ ] **Modelo Qwen 2.5 Coder** (`ollama pull qwen2.5-coder:7b`)

---

## 🚀 Instalación y Configuración

### 1️⃣ Clonar el repositorio

```bash
git clone https://github.com/TU_USUARIO/EmpresaMCP.git
cd EmpresaMCP

2️⃣ Configurar la base de datos
sql
-- La base de datos debe llamarse "EmpresaDB"
-- Asegurate de que las tablas Empleados y Departamentos existan

3️⃣ Configurar la cadena de conexión
"ConnectionStrings": {
  "EmpresaDB": "Server=(localdb)\\MSSQLLocalDB;Database=EmpresaDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

4️⃣ Iniciar Ollama
# En una terminal separada
ollama serve

# Verificar que el modelo esté disponible
ollama list

5️⃣ Ejecutar la aplicación
# Desde Visual Studio: F5
# O desde terminal:
dotnet run --project EmpresaMCP.Web

La aplicación estará disponible en: https://localhost:7001 (o el puerto que configure VS)

📸 Capturas de Pantalla

![Chat de la aplicación](docs/screenshots/chat.png)


![API Response](docs/screenshots/json.png)
![Estructura del Proyecto](docs/screenshots/estructura.png)

🎯 Endpoints de la API[Uploading activos.json…]()

Método
Endpoint
Descripción
GET
/api/EmpleadosApi/activos
Lista todos los empleados activos
GET
/api/EmpleadosApi/buscar?termino={nombre}
Busca empleados por nombre
GET
/api/EmpleadosApi/{id}
Obtiene un empleado por ID
Ejemplo de respuesta
json
1234567891011121314

💡 Ejemplos de Consultas
Podés probar con estas preguntas en el chat:
Pregunta
Resultado esperado
"¿Cuántos empleados hay en la empresa?"
Cantidad total de activos
"Buscá empleados llamados Carlos"
Lista de coincidencias
"Mostrame los datos del empleado 1"
Datos completos del empleado
"¿Qué empleados ingresaron en 2015?"
Filtrado por fecha
📁 Estructura del Proyecto
EmpresaMCP/
├── docs/
│   └── screenshots/
│       ├── chat.png
│       ├── api-response.png
│       └── project-structure.png
├── EmpresaMCP.Web/
├── EmpresaMCP.Core/
└── README.md

🔐 Seguridad y Consideraciones
✅ La IA no tiene acceso directo a la BD: Usa APIs controladas
✅ Solo empleados activos: Las consultas filtran por defecto
✅ Validación de parámetros: Los IDs y términos se validan antes de consultar
⚠️ Desarrollo local: No usar en producción sin autenticación y HTTPS
🧠 Lo que aprendí construyendo este proyecto
Integración de LLMs locales con aplicaciones .NET
Implementación de function calling sin SDKs externos
Arquitectura limpia con separación de responsabilidades
Uso de Entity Framework Core con SQL Server
Creación de APIs REST en ASP.NET Core
Prompt engineering para guiar al LLM
🚧 Próximas Mejoras (Roadmap)
Agregar más herramientas (Departamentos, Cargos, Salarios)
Historial de conversaciones
Autenticación de usuarios
Logging de consultas para auditoría
Deploy en Azure/AWS
Implementar MCP protocol nativo cuando esté estable
📄 Licencia
Este proyecto está bajo la licencia MIT. Ver el archivo LICENSE para más detalles.
🤝 Contacto
Autor: [Tu Nombre]
LinkedIn: Tu Perfil
Email: tu.email@ejemplo.com

<div align="center">

¿Te gustó este proyecto? ⭐ Dale una estrella y compartilo!
</div>
