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
    public class SectoresRepository : ISectoresRepository
    {

        private readonly EmpresaDbContext _context;

        public SectoresRepository(EmpresaDbContext context)
        {
            _context = context;
        }

        public async Task<Sectores?> GetSectorByIdAsync(int id)
        {
            return await _context.Sectores
                        .Where(e => e.SectorID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Sectores>> GetSectorByNameAsync(string name)
        {
            return await _context.Sectores
                         .Where(e => e.Activo == true && (e.Nombre == name))
                         .ToListAsync();
        }

        public async Task<IEnumerable<Sectores>> GetAllSectorsAsync()
        {
            return await _context.Sectores
             .Where(e => e.Activo == true)
             .ToListAsync();
        }
    }
}
