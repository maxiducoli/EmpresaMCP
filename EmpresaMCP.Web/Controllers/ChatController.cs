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


        #region Código de respaldo

        // Método auxiliar: detectar qué API llamar según la pregunta
        //private async Task<string> ObtenerDatosSegunPreguntaAsync(string pregunta, HttpClient client)
        //{
        //    var preguntaLower = pregunta.ToLower();

        //    // === EMPLEADOS ===
        //    if (preguntaLower.Contains("empleado") && preguntaLower.Contains("id")) 
        //    { 
        //        var response = await client.GetAsync("/api/EmpleadosApi/activos");
        //    }

        //    if (preguntaLower.Contains("empleado") && preguntaLower.Contains("sexo"))
        //    {
        //        var response = await client.GetAsync("/api/EmpleadosApi/activos");
        //    }

        //    if (preguntaLower.Contains("busc") || preguntaLower.Contains("llam"))
        //    {
        //        var response = await client.GetAsync("/api/EmpleadosApi/buscar");
        //    }
        //    if (preguntaLower.Contains("empleado") || preguntaLower.Contains("id"))
        //    {
        //        var response = await client.GetAsync("/api/EmpleadosApi/EmpleadosApi/");
        //        if (response.IsSuccessStatusCode)
        //            return await response.Content.ReadAsStringAsync();
        //    }

        //    // === SECTORES ===
        //    if (preguntaLower.Contains("sector") || preguntaLower.Contains("sectores"))
        //    {
        //        var response = await client.GetAsync("/api/SectoresApi");
        //        if (response.IsSuccessStatusCode)
        //            return await response.Content.ReadAsStringAsync();
        //    }

        //    // === CARGOS ===
        //    if (preguntaLower.Contains("cargo") || preguntaLower.Contains("cargos") || preguntaLower.Contains("puesto"))
        //    {
        //        var response = await client.GetAsync("/api/CargosApi");
        //        if (response.IsSuccessStatusCode)
        //            return await response.Content.ReadAsStringAsync();
        //    }

        //    // === DEPARTAMENTOS ===
        //    if (preguntaLower.Contains("departamento") || preguntaLower.Contains("departamentos") || preguntaLower.Contains("área"))
        //    {
        //        var response = await client.GetAsync("/api/DepartamentosApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            return await response.Content.ReadAsStringAsync();
        //    }

        //    // ... repetir para Plantas, Salarios, Contratos, Asistencias

        //    // Por defecto: Empleados activos
        //    var responseDefault = await client.GetAsync("/api/EmpleadosApi/activos");
        //    if (responseDefault.IsSuccessStatusCode)
        //        return await responseDefault.Content.ReadAsStringAsync();

        //    return "No se pudieron obtener datos";
        //}



        // Método auxiliar: detectar qué API llamar según la pregunta del usuario
        //private async Task<string> ObtenerDatosSegunPreguntaAsync(string pregunta, HttpClient client)
        //{
        //    var preguntaLower = pregunta.ToLower();
        //    var datosObtenidos = new List<string>();

        //    // ============================================
        //    // 📋 DETECCIÓN DE ENTIDADES POR PALABRAS CLAVE
        //    // ============================================

        //    // 👥 EMPLEADOS
        //    if (preguntaLower.Contains("empleado") || preguntaLower.Contains("empleados") ||
        //        preguntaLower.Contains("trabajador") || preguntaLower.Contains("personal") ||
        //        preguntaLower.Contains("legajo"))
        //    {
        //        // ¿Busca por ID específico?
        //        if (preguntaLower.Contains("id") || preguntaLower.Contains("número") || preguntaLower.Contains("legajo"))
        //        {
        //            var numeros = System.Text.RegularExpressions.Regex.Matches(pregunta, @"\d+");
        //            if (numeros.Count > 0)
        //            {
        //                var id = numeros[0].Value;
        //                var response = await client.GetAsync($"/api/EmpleadosApi/{id}");
        //                if (response.IsSuccessStatusCode)
        //                    datosObtenidos.Add($"[EMPLEADO POR ID {id}]: " + await response.Content.ReadAsStringAsync());
        //            }
        //        }
        //        // ¿Busca por nombre?
        //        else if (preguntaLower.Contains("busc") || preguntaLower.Contains("llam") || preguntaLower.Contains("nombre"))
        //        {
        //            // Extraer término de búsqueda (simplificado: última palabra que parezca nombre)
        //            var partes = pregunta.Split(' ');
        //            var termino = partes.Length > 3 ? partes[^1] : "Carlos";
        //            var response = await client.GetAsync($"/api/EmpleadosApi/buscar?termino={termino}");
        //            if (response.IsSuccessStatusCode)
        //                datosObtenidos.Add($"[BÚSQUEDA EMPLEADOS '{termino}']: " + await response.Content.ReadAsStringAsync());
        //        }
        //        // Por defecto: lista de activos
        //        else
        //        {
        //            var response = await client.GetAsync("/api/EmpleadosApi/activos");
        //            if (response.IsSuccessStatusCode)
        //                datosObtenidos.Add("[EMPLEADOS ACTIVOS]: " + await response.Content.ReadAsStringAsync());
        //        }
        //    }

        //    // 💼 CARGOS
        //    if (preguntaLower.Contains("cargo") || preguntaLower.Contains("cargos") ||
        //        preguntaLower.Contains("puesto") || preguntaLower.Contains("puestos") ||
        //        preguntaLower.Contains("rol") || preguntaLower.Contains("roles") ||
        //        preguntaLower.Contains("jerarquía") || preguntaLower.Contains("jerarquia"))
        //    {
        //        var response = await client.GetAsync("/api/CargosApi");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[CARGOS]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // 🏢 DEPARTAMENTOS
        //    if (preguntaLower.Contains("departamento") || preguntaLower.Contains("departamentos") ||
        //        preguntaLower.Contains("área") || preguntaLower.Contains("areas") ||
        //        preguntaLower.Contains("sector") && !preguntaLower.Contains("sectores")) // "sector" solo = departamento en algunos contextos
        //    {
        //        var response = await client.GetAsync("/api/DepartamentosApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[DEPARTAMENTOS]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // 📍 SECTORES
        //    if (preguntaLower.Contains("sectores") || preguntaLower.Contains("sector") ||
        //        preguntaLower.Contains("división") || preguntaLower.Contains("division") ||
        //        preguntaLower.Contains("unidad"))
        //    {
        //        var response = await client.GetAsync("/api/SectoresApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[SECTORES]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // 🏭 PLANTAS / SUCURSALES
        //    if (preguntaLower.Contains("planta") || preguntaLower.Contains("plantas") ||
        //        preguntaLower.Contains("sucursal") || preguntaLower.Contains("sucursales") ||
        //        preguntaLower.Contains("sede") || preguntaLower.Contains("sedes") ||
        //        preguntaLower.Contains("ubicación") || preguntaLower.Contains("ubicacion"))
        //    {
        //        var response = await client.GetAsync("/api/PlantasApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[PLANTAS]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // 💰 SALARIOS / SUELDOS
        //    if (preguntaLower.Contains("salario") || preguntaLower.Contains("salarios") ||
        //        preguntaLower.Contains("sueldo") || preguntaLower.Contains("sueldos") ||
        //        preguntaLower.Contains("paga") || preguntaLower.Contains("pago") ||
        //        preguntaLower.Contains("bonificación") || preguntaLower.Contains("bonificacion") ||
        //        preguntaLower.Contains("deducción") || preguntaLower.Contains("deduccion") ||
        //        preguntaLower.Contains("neto") || preguntaLower.Contains("bruto"))
        //    {
        //        var response = await client.GetAsync("/api/SalariosApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[SALARIOS]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // 📄 CONTRATOS
        //    if (preguntaLower.Contains("contrato") || preguntaLower.Contains("contratos") ||
        //        preguntaLower.Contains("modalidad") || preguntaLower.Contains("tipo de contrato") ||
        //        preguntaLower.Contains("período de prueba") || preguntaLower.Contains("periodo de prueba"))
        //    {
        //        var response = await client.GetAsync("/api/ContratosApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[CONTRATOS]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // 📅 ASISTENCIAS / HORARIOS
        //    if (preguntaLower.Contains("asistencia") || preguntaLower.Contains("asistencias") ||
        //        preguntaLower.Contains("faltas") || preguntaLower.Contains("ausente") ||
        //        preguntaLower.Contains("entrada") || preguntaLower.Contains("salida") ||
        //        preguntaLower.Contains("horas trabajadas") || preguntaLower.Contains("horario") ||
        //        preguntaLower.Contains("llegó tarde") || preguntaLower.Contains("llego tarde") ||
        //        preguntaLower.Contains("registro"))
        //    {
        //        var response = await client.GetAsync("/api/AsistenciasApi/activos");
        //        if (response.IsSuccessStatusCode)
        //            datosObtenidos.Add("[ASISTENCIAS]: " + await response.Content.ReadAsStringAsync());
        //    }

        //    // ============================================
        //    // 🔙 RETORNAR DATOS O MENSAJE POR DEFECTO
        //    // ============================================

        //    if (datosObtenidos.Any())
        //    {
        //        return string.Join("\n\n", datosObtenidos);
        //    }

        //    // Si no detectó ninguna entidad específica, devolver empleados activos por defecto
        //    var responseDefault = await client.GetAsync("/api/EmpleadosApi/activos");
        //    if (responseDefault.IsSuccessStatusCode)
        //        return "[EMPLEADOS ACTIVOS - por defecto]: " + await responseDefault.Content.ReadAsStringAsync();

        //    return "No se pudieron obtener datos de la base de datos.";
        //}

        #endregion

        // Método auxiliar: detectar qué API llamar según la pregunta del usuario
        private async Task<string> ObtenerDatosSegunPreguntaAsync(string pregunta, HttpClient client)
        {
            var preguntaLower = pregunta.ToLower();
            var datosObtenidos = new List<string>();

            // ============================================
            // 🧠 CONSULTAS COMPLEJAS (MÚLTIPLES ENTIDADES)
            // ============================================

            // 💰 SUELDO/SALARIO + CARGOS + EMPLEADOS (para cálculos de sueldos)
            if (preguntaLower.Contains("sueldo") || preguntaLower.Contains("salario") ||
                preguntaLower.Contains("promedio") || preguntaLower.Contains("calcular") ||
                preguntaLower.Contains("cuánto gana") || preguntaLower.Contains("ganancia") ||
                preguntaLower.Contains("gerente") || preguntaLower.Contains("gerentes"))
            {
                await FetchMultipleEndpointsAsync(client, datosObtenidos,
                    "/api/EmpleadosApi/activos", "[EMPLEADOS]",
                    "/api/CargosApi/activos", "[CARGOS]",
                    "/api/SalariosApi", "[SALARIOS]");
            }

            // 🏢 DEPARTAMENTO + EMPLEADOS (para preguntas cruzadas)
            else if (preguntaLower.Contains("departamento") && (preguntaLower.Contains("empleados") || preguntaLower.Contains("trabajadores")))
            {
                await FetchMultipleEndpointsAsync(client, datosObtenidos,
                    "/api/EmpleadosApi/activos", "[EMPLEADOS]",
                    "/api/DepartamentosApi/activos", "[DEPARTAMENTOS]");
            }

            // 📍 SECTOR + EMPLEADOS
            else if (preguntaLower.Contains("sector") && (preguntaLower.Contains("empleados") || preguntaLower.Contains("trabajadores")))
            {
                await FetchMultipleEndpointsAsync(client, datosObtenidos,
                    "/api/EmpleadosApi/activos", "[EMPLEADOS]",
                    "/api/SectoresApi/activos", "[SECTORES]");
            }

            // 🏭 PLANTA + EMPLEADOS
            else if (preguntaLower.Contains("planta") && (preguntaLower.Contains("empleados") || preguntaLower.Contains("trabajadores")))
            {
                await FetchMultipleEndpointsAsync(client, datosObtenidos,
                    "/api/EmpleadosApi/activos", "[EMPLEADOS]",
                    "/api/PlantasApi/activos", "[PLANTAS]");
            }

            // ============================================
            // 📋 CONSULTAS SIMPLES (UNA ENTIDAD)
            // ============================================

            // 👥 EMPLEADOS
            else if (preguntaLower.Contains("empleado") || preguntaLower.Contains("empleados") ||
                preguntaLower.Contains("trabajador") || preguntaLower.Contains("personal") ||
                preguntaLower.Contains("legajo"))
            {
                if (preguntaLower.Contains("id") || preguntaLower.Contains("número") || preguntaLower.Contains("legajo"))
                {
                    var numeros = System.Text.RegularExpressions.Regex.Matches(pregunta, @"\d+");
                    if (numeros.Count > 0)
                    {
                        var id = numeros[0].Value;
                        var response = await client.GetAsync($"/api/EmpleadosApi/{id}");
                        if (response.IsSuccessStatusCode)
                            datosObtenidos.Add($"[EMPLEADO POR ID {id}]: " + await response.Content.ReadAsStringAsync());
                    }
                }
                else if (preguntaLower.Contains("busc") || preguntaLower.Contains("llam") || preguntaLower.Contains("nombre"))
                {
                    var partes = pregunta.Split(' ');
                    var termino = partes.Length > 3 ? partes[^1] : "Carlos";
                    var response = await client.GetAsync($"/api/EmpleadosApi/buscar?termino={termino}");
                    if (response.IsSuccessStatusCode)
                        datosObtenidos.Add($"[BÚSQUEDA EMPLEADOS '{termino}']: " + await response.Content.ReadAsStringAsync());
                }
                else
                {
                    var response = await client.GetAsync("/api/EmpleadosApi/activos");
                    if (response.IsSuccessStatusCode)
                        datosObtenidos.Add("[EMPLEADOS ACTIVOS]: " + await response.Content.ReadAsStringAsync());
                }
            }

            // 💼 CARGOS
            else if (preguntaLower.Contains("cargo") || preguntaLower.Contains("cargos") ||
                preguntaLower.Contains("puesto") || preguntaLower.Contains("puestos") ||
                preguntaLower.Contains("rol") || preguntaLower.Contains("roles"))
            {
                var response = await client.GetAsync("/api/CargosApi/activos");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[CARGOS]: " + await response.Content.ReadAsStringAsync());
            }

            // 🏢 DEPARTAMENTOS
            else if (preguntaLower.Contains("departamento") || preguntaLower.Contains("departamentos") ||
                preguntaLower.Contains("área") || preguntaLower.Contains("areas"))
            {
                var response = await client.GetAsync("/api/DepartamentosApi/activos");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[DEPARTAMENTOS]: " + await response.Content.ReadAsStringAsync());
            }

            // 📍 SECTORES
            else if (preguntaLower.Contains("sectores") || preguntaLower.Contains("sector") ||
                preguntaLower.Contains("división") || preguntaLower.Contains("division"))
            {
                var response = await client.GetAsync("/api/SectoresApi/activos");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[SECTORES]: " + await response.Content.ReadAsStringAsync());
            }

            // 🏭 PLANTAS
            else if (preguntaLower.Contains("planta") || preguntaLower.Contains("plantas") ||
                preguntaLower.Contains("sucursal") || preguntaLower.Contains("sucursales") ||
                preguntaLower.Contains("sede") || preguntaLower.Contains("sedes"))
            {
                var response = await client.GetAsync("/api/PlantasApi/activos");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[PLANTAS]: " + await response.Content.ReadAsStringAsync());
            }

            // 💰 SALARIOS (solo si no fue capturado por la consulta compleja de arriba)
            else if (preguntaLower.Contains("salario") || preguntaLower.Contains("salarios") ||
                preguntaLower.Contains("sueldo") || preguntaLower.Contains("sueldos"))
            {
                var response = await client.GetAsync("/api/SalariosApi");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[SALARIOS]: " + await response.Content.ReadAsStringAsync());
            }

            // 📄 CONTRATOS
            else if (preguntaLower.Contains("contrato") || preguntaLower.Contains("contratos") ||
                preguntaLower.Contains("modalidad"))
            {
                var response = await client.GetAsync("/api/ContratosApi/activos");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[CONTRATOS]: " + await response.Content.ReadAsStringAsync());
            }

            // 📅 ASISTENCIAS
            else if (preguntaLower.Contains("asistencia") || preguntaLower.Contains("asistencias") ||
                preguntaLower.Contains("faltas") || preguntaLower.Contains("entrada") ||
                preguntaLower.Contains("salida") || preguntaLower.Contains("horas"))
            {
                var response = await client.GetAsync("/api/AsistenciasApi/activos");
                if (response.IsSuccessStatusCode)
                    datosObtenidos.Add("[ASISTENCIAS]: " + await response.Content.ReadAsStringAsync());
            }

            // ============================================
            // 🔙 RETORNAR DATOS O MENSAJE POR DEFECTO
            // ============================================

            if (datosObtenidos.Any())
            {
                return string.Join("\n\n", datosObtenidos);
            }

            var responseDefault = await client.GetAsync("/api/EmpleadosApi/activos");
            if (responseDefault.IsSuccessStatusCode)
                return "[EMPLEADOS ACTIVOS - por defecto]: " + await responseDefault.Content.ReadAsStringAsync();

            return "No se pudieron obtener datos de la base de datos.";
        }


        // Método auxiliar para consultar múltiples endpoints
        private async Task FetchMultipleEndpointsAsync(HttpClient client, List<string> datosObtenidos, params string[] endpointYLabel)
        {
            for (int i = 0; i < endpointYLabel.Length; i += 2)
            {
                var endpoint = endpointYLabel[i];
                var label = endpointYLabel[i + 1];

                try
                {
                    var response = await client.GetAsync(endpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        datosObtenidos.Add($"{label}: {content}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Error en {endpoint}: {ex.Message}");
                }
            }
        }
    }
}