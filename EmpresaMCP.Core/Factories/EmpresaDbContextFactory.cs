using EmpresaMCP.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmpresaMCP.Core.Factories
{
    public class EmpresaDbContextFactory : IDesignTimeDbContextFactory<EmpresaDbContext>
    {
        public EmpresaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmpresaDbContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmpresaDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new EmpresaDbContext(optionsBuilder.Options);
        }
    }
}
