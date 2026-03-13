using System.Text;
using System.Text.Json;

namespace EmpresaMCP.Web.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _ollamaUrl = "http://localhost:11434";
        private readonly string _modelName = "qwen2.5-coder:7b";

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método simple: enviar pregunta y recibir respuesta
        public async Task<string> GenerarRespuestaAsync(string prompt)
        {
            var request = new
            {
                model = _modelName,
                prompt = prompt,
                stream = false,
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_ollamaUrl}/api/generate", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonDocument.Parse(responseJson);
                return result.RootElement.GetProperty("response").GetString() ?? "Sin respuesta";
            }

            return "Error al conectar con Ollama";
        }

        // Prompt del sistema con las herramientas disponibles
        public string ObtenerPromptSistema()
        {
            return @"
Eres un asistente inteligente que consulta información de una empresa.

📊 DATOS DISPONIBLES (vía API):
[Se inyectarán dinámicamente según la pregunta]

🔧 CAPACIDADES:
- Podés recibir datos de: Empleados, Cargos, Salarios, Departamentos, Sectores, Plantas, Contratos, Asistencias
- Si necesitás más datos para responder, decí: 'Necesito consultar [entidad] para responder eso'
- Si te piden cálculos (promedios, totales, diferencias), hacelos vos con los datos que tenés
- Si los datos no son suficientes, explicá qué falta

💬 INSTRUCCIONES:
- Respondé siempre en español
- Sé claro, conciso y profesional
- Si no tenés datos suficientes, explicá qué necesitarías

👥 EMPLEADOS:
- Lista de empleados activos
- Búsqueda por nombre o apellido
- Datos detallados por ID de empleado

💼 CARGOS:
- Lista de cargos disponibles
- Salario mínimo y máximo por cargo
- Nivel jerárquico y descripción

🏢 DEPARTAMENTOS:
- Lista de departamentos
- Presupuesto anual por departamento
- Departamento al que pertenece un empleado

📍 SECTORES:
- Lista de sectores
- Sector dentro de un departamento
- Jefe responsable de cada sector

🏭 PLANTAS:
- Lista de plantas/sucursales
- Dirección, ciudad y contacto de cada planta

💰 SALARIOS:
- Salario base, bonificaciones y deducciones
- Salario neto por empleado
- Moneda de pago

📄 CONTRATOS:
- Tipo de contrato y modalidad de trabajo
- Fecha de inicio y fin
- Período de prueba

📅 ASISTENCIAS:
- Registro de entradas y salidas
- Horas trabajadas
- Estado (presente, ausente, tarde)

📋 INSTRUCCIONES:
- Respondé siempre en español
- Usá los datos proporcionados para responder
- Si no tenés datos para una consulta, decí que no podés consultar en este momento
- Sé claro, conciso y profesional
- Si te piden un cálculo (ej: salario neto = base + bonif - deducciones), hacelo vos
";
        }
    }
}