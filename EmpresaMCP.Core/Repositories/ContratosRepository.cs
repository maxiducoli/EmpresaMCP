using EmpresaMCP.Core.Data;
using EmpresaMCP.Core.Entities;
using EmpresaMCP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmpresaMCP.Core.Repositories
{
    public class ContratosRepository : IContratosRepository
    {

        private readonly EmpresaDbContext _context;

        public ContratosRepository(EmpresaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contratos>> GetAllContratosAsync()
        {
            return await _context.Contratos
                        .Where(e => e.Activo == true)
                        .ToListAsync();
        }

        public async Task<Contratos?> GetContratoByIdAsync(int id)
        {
            return await _context.Contratos
                        .Where(e => e.ContratoID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contratos>> GetContratoByNameAsync(string name)
        {
            return await _context.Contratos
                      .Where(e => e.Activo == true && (e.Observaciones.Contains(name)))
                      .ToListAsync();
        }


    }

}
