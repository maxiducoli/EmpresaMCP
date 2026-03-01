using ModelContextProtocol.Server;
namespace EmpresaMCP.McpServer.Services
{
    public class McpToolHandler
    {
        private readonly EmpleadoService _empleadoService;

        public McpToolHandler(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // Registrar las herramientas en el servidor MCP
        public void RegisterTools(McpServerPrompt server)
        {
            // Herramienta 1: Listar empleados activos
            server.Add(
                name: "listar_empleados_activos",
                description: "Obtiene la lista de todos los empleados activos de la empresa",
                handler: async (context, cancellationToken) =>
                {
                    var empleados = await _empleadoService.ObtenerEmpleadosActivosAsync();
                    return new ToolResult
                    {
                        Content = new List<Content>
                        {
                            new TextContent { Text = $"Se encontraron {empleados.Count} empleados activos." }
                        }
                    };
                });

            // Herramienta 2: Buscar empleado por nombre
            server.AddTool(
                name: "buscar_empleado_por_nombre",
                description: "Busca empleados por nombre o apellido. Requiere un parámetro 'termino'.",
                handler: async (context, cancellationToken) =>
                {
                    var termino = context.Arguments?.GetValueOrDefault("termino")?.ToString() ?? "";
                    var empleados = await _empleadoService.BuscarEmpleadosPorNombreAsync(termino);
                    return new ToolResult
                    {
                        Content = new List<Content>
                        {
                            new TextContent { Text = $"Se encontraron {empleados.Count} empleados que coinciden con '{termino}'." }
                        }
                    };
                });

            // Herramienta 3: Obtener empleado por ID
            server.AddTool(
                name: "obtener_empleado_por_id",
                description: "Obtiene los datos de un empleado específico por su ID. Requiere un parámetro 'empleadoId'.",
                handler: async (context, cancellationToken) =>
                {
                    if (context.Arguments?.GetValueOrDefault("empleadoId") is int id)
                    {
                        var empleado = await _empleadoService.ObtenerEmpleadoPorIdAsync(id);
                        if (empleado != null)
                        {
                            return new ToolResult
                            {
                                Content = new List<Content>
                                {
                                    new TextContent { Text = $"Empleado: {empleado.Nombre} {empleado.Apellido}, Legajo: {empleado.Legajo}" }
                                }
                            };
                        }
                        return new ToolResult
                        {
                            Content = new List<Content>
                            {
                                new TextContent { Text = "Empleado no encontrado." }
                            }
                        };
                    }
                    return new ToolResult
                    {
                        Content = new List<Content>
                        {
                            new TextContent { Text = "Error: Se requiere el parámetro 'empleadoId'." }
                        }
                    };
                });
        }
    }
}