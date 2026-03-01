using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using EmpresaMCP.Core.Data;
using Microsoft.EntityFrameworkCore;
using EmpresaMCP.McpServer.Services;
using ModelContextProtocol.Server;

namespace EmpresaMCP.McpServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<EmpresaDbContext>(options =>
                        options.UseSqlServer(
                            "Server=(localdb)\\MSSQLLocalDB;Database=EmpresaDB;Trusted_Connection=True;TrustServerCertificate=True;"));

                    services.AddSingleton<EmpleadoService>();
                    services.AddSingleton<McpToolHandler>();
                })
                .Build();

            // Obtener el handler de herramientas
            var toolHandler = host.Services.GetRequiredService<McpToolHandler>();

            // Crear y configurar el servidor MCP
            var server = McpServer.Create("EmpresaMCP", "1.0.0");
             
            // Registrar las herramientas
            toolHandler.RegisterTools(server);

            Console.WriteLine("✅ MCP Server iniciado correctamente!");
            Console.WriteLine("📡 Escuchando herramientas MCP...");
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}