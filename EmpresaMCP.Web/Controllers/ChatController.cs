using Microsoft.AspNetCore.Mvc;
using EmpresaMCP.Web.Services;
using System.Text.Json;

namespace EmpresaMCP.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly OllamaService _ollamaService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(OllamaService ollamaService, IHttpClientFactory httpClientFactory)
        {
            _ollamaService = ollamaService;
            _httpClientFactory = httpClientFactory;
        }

        // GET: Chat
        public IActionResult Index()
        {
            return View();
        }

        // POST: Chat/Consultar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Consultar(string pregunta)
        {
            if (string.IsNullOrEmpty(pregunta))
            {
                return BadRequest("La pregunta no puede estar vacía");
            }

            // Crear HttpClient para llamar a las APIs internas
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri($"{Request.Scheme}://{Request.Host}");

            // Paso 1: Detectar qué datos necesita el LLM
            var datosContexto = await ObtenerDatosSegunPreguntaAsync(pregunta, client);

            // Paso 2: Armar el prompt con los datos reales
            var promptSistema = _ollamaService.ObtenerPromptSistema();
            var promptCompleto = $@"
{promptSistema}

---
DATOS DISPONIBLES:
{datosContexto}

---
PREGUNTA DEL USUARIO:
{pregunta}

---
Respondé la pregunta usando los datos de arriba. Si los datos están vacíos, decí que no se encontró información.
";

            // Paso 3: Consultar a Ollama
            var respuesta = await _ollamaService.GenerarRespuestaAsync(promptCompleto);

            ViewBag.Pregunta = pregunta;
            ViewBag.Respuesta = respuesta;
            ViewBag.DatosCrudos = datosContexto; // Para debugging

            return View("Index");
        }

        // Método auxiliar: detectar qué API llamar según la pregunta
        private async Task<string> ObtenerDatosSegunPreguntaAsync(string pregunta, HttpClient client)
        {
            var preguntaLower = pregunta.ToLower();

            // Detectar si pregunta por un ID específico
            if (preguntaLower.Contains("empleado") && preguntaLower.Contains("id"))
            {
                // Extraer número de la pregunta (simple)
                var numeros = System.Text.RegularExpressions.Regex.Matches(pregunta, @"\d+");
                if (numeros.Count > 0)
                {
                    var id = numeros[0].Value;
                    var response = await client.GetAsync($"/api/EmpleadosApi/{id}");
                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                }
            }

            // Detectar si pregunta por búsqueda de nombre
            if (preguntaLower.Contains("busc") || preguntaLower.Contains("llam") || preguntaLower.Contains("nombre"))
            {
                // Extraer posible nombre (simplificado: usamos la primera palabra después de verbos)
                var termino = "Carlos"; // Por defecto para prueba
                var response = await client.GetAsync($"/api/EmpleadosApi/buscar?termino={termino}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
            }

            // Por defecto: listar empleados activos
            var responseDefault = await client.GetAsync("/api/EmpleadosApi/activos");
            if (responseDefault.IsSuccessStatusCode)
                return await responseDefault.Content.ReadAsStringAsync();

            return "No se pudieron obtener datos";
        }
    }
}