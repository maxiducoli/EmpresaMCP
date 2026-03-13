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
    public class CargosRepository : ICargosRepository
    {

        private readonly EmpresaDbContext _context;

        public CargosRepository(EmpresaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cargos>> GetAllCargosAsync()
        {
            return await _context.Cargos
                        .Where(e => e.Activo == true)
                        .ToListAsync();
        }


        public async Task<IEnumerable<Cargos>> GetCargoByNameAsync(string name)
        {
            return await _context.Cargos
                         .Where(e => e.Activo == true && (e.Nombre.Contains(name)))
                         .ToListAsync();
        }

        public async Task<Cargos?> GetCargoByIdAsync(int id)
        {
            return await _context.Cargos
               .Where(e => e.CargoID == id && e.Activo == true)
               .FirstOrDefaultAsync();
        }
    }
}
