using EmpresaMCP.Core.Data;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmpresaMCP.Core.Repositories
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly EmpresaDbContext _context;

        public EmpleadosRepository(EmpresaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleados>> GetAllEmpleadosAsync()
        {
            return await _context.Empleados
                        .Where(e => e.Activo == true)
                        .ToListAsync();
        }

        public async Task<Empleados?> GetEmpleadoByIdAsync(int id)
        {
            return await _context.Empleados
                        .Where(e => e.EmpleadoID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Empleados>> GetEmployeByNameAsync(string name)
        {
            return await _context.Empleados
                        .Where(e => e.Activo == true && (e.Nombre.Contains(name) || e.Apellido.Contains(name)))
                        .ToListAsync();
        }
    }
}
