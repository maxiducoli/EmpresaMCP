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
    public class SalariosRepository : ISalariosRepository
    {
        private readonly EmpresaDbContext _context;

        public SalariosRepository(EmpresaDbContext context)
        {
            _context = context;
        }


        public async Task<Salarios?> GetSalarioByIdAsync(int id)
        {
            return await _context.Salarios
                        .Where(e => e.SalarioID == id && e.Activo == true)
                        .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Salarios>> GetAllSalariosAsync()
        {
            return await _context.Salarios
             .Where(e => e.Activo == true)
             .ToListAsync();
        }



        public  async Task<IEnumerable<Salarios>> GetSalarioByAmountAsync(decimal amount)
        {
            return await _context.Salarios
             .Where(e => e.Activo == true && (e.SalarioNeto == amount))
             .ToListAsync();
        }
    }
}
