using EmpresaMCP.Core.Data;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaMCP.Core.Repositories
{
    public class DepartamentosRepository : IDepartamentosRepository
    {
        private readonly EmpresaDbContext _context;

        public DepartamentosRepository(EmpresaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departamentos>> GetAllDepartamentosAsync()
        {
            return await _context.Departamentos
                  .Where(e => e.Activo == true)
                  .ToListAsync();
        }

        public async Task<Departamentos?> GetDepartamentoByIdAsync(int id)
        {
            return await _context.Departamentos
                        .Where(e => e.DepartamentoID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Departamentos>> GetDepartamentoByNameAsync(string name)
        {
            return await _context.Departamentos
             .Where(e => e.Activo == true && (e.Nombre.Contains(name)))
             .ToListAsync();
        }

    }
}
