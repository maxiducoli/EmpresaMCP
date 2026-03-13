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
    public class AsistenciasRepository : IAsistenciasRepository
    {
        private readonly EmpresaDbContext _context;

        public AsistenciasRepository(EmpresaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Asistencias>> GetAllAsistemciasAsync()
        {
            return await _context.Asistencias
                        .Where(e => e.Activo == true)
                        .ToListAsync();
        }

        public async Task<Asistencias?> GetAsistemciaByIdAsync(int id)
        {
            return await _context.Asistencias
                        .Where(e => e.AsistenciaID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Asistencias>> GetAsistemciaByObsAsync(string name)
        {
            return await _context.Asistencias
                         .Where(e => e.Activo == true && (e.Observaciones.Contains(name)))
                         .ToListAsync();
        }
    }
}
