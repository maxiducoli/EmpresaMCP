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
Eres un asistente inteligente que consulta información de empleados de una empresa.

Tienes acceso a estos datos (ya consultados):
- Empleados activos
- Búsqueda por nombre
- Datos por ID de empleado

Instrucciones:
- Respondé siempre en español
- Usá los datos proporcionados para responder
- Si no tenés datos, decí que no podés consultar en este momento
- Sé claro y conciso
";
        }
    }
}