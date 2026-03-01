using EmpresaMCP.Core.Data;
using EmpresaMCP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpresaMCP.McpServer.Services
{
    public class EmpleadoService
    {
        private readonly EmpresaDbContext _context;

        // Constructor con inyección de dependencias
        public EmpleadoService(EmpresaDbContext context)
        {
            _context = context;
        }

        // Herramienta 1: Obtener todos los empleados activos
        public async Task<List<Empleado>> ObtenerEmpleadosActivosAsync()
        {
            return await _context.Empleados
                .Where(e => e.Activo == true)
                .ToListAsync();
        }

        // Herramienta 2: Obtener empleado por ID
        public async Task<Empleado?> ObtenerEmpleadoPorIdAsync(int empleadoId)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(e => e.EmpleadoID == empleadoId);
        }

        // Herramienta 3: Buscar empleados por nombre o apellido
        public async Task<List<Empleado>> BuscarEmpleadosPorNombreAsync(string termino)
        {
            return await _context.Empleados
                .Where(e => e.Activo == true &&
                      (e.Nombre.Contains(termino) || e.Apellido.Contains(termino)))
                .ToListAsync();
        }

        // Herramienta 4: Obtener empleados por departamento
        public async Task<List<Empleado>> ObtenerEmpleadosPorDepartamentoAsync(int departamentoId)
        {
            return await _context.Empleados
                .Where(e => e.Activo == true && e.DepartamentoID == departamentoId)
                .ToListAsync();
        }
    }
}