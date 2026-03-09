using EmpresaMCP.Core.Data;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmpresaMCP.Core.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly EmpresaDbContext _context;

        public EmpleadoRepository(EmpresaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> GetAllEmpleadosAsync()
        {
            return await _context.Empleados
                        .Where(e => e.Activo == true)
                        .ToListAsync();
        }

        public async Task<Empleado?> GetEmpleadoByIdAsync(int id)
        {
            return await _context.Empleados
                        .Where(e => e.EmpleadoID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Empleado>> GetEmployeByNameAsync(string name)
        {
            return await _context.Empleados
                        .Where(e => e.Activo == true && (e.Nombre.Contains(name) || e.Apellido.Contains(name)))
                        .ToListAsync();
        }
    }
}
