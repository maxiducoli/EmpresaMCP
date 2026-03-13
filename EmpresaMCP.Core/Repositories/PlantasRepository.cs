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
    public class PlantasRepository : IPlantasRepository
    {
        private readonly EmpresaDbContext _context;

        public PlantasRepository(EmpresaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Plantas>> GetAllPlantasAsync()
        {
            return await _context.Plantas
                        .Where(e => e.Activo == true)
                        .ToListAsync();
        }

        public async Task<Plantas?> GetPlantaByIdAsync(int id)
        {
            return await _context.Plantas
                        .Where(e => e.PlantaID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Plantas>> GetPlantaByNameAsync(string name)
        {
            return await _context.Plantas
              .Where(e => e.Activo == true && (e.Nombre.Contains(name)))
              .ToListAsync();
        }


    }
}
