using Microsoft.EntityFrameworkCore;
using EmpresaMCP.Core.Entities;

namespace EmpresaMCP.Core.Data
{
    public class EmpresaDbContext : DbContext
    {
        // Constructor que recibe opciones de configuración
        public EmpresaDbContext(DbContextOptions<EmpresaDbContext> options)
            : base(options)
        {
        }

        // DbSet = cada tabla en la base de datos
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        // Configuración adicional de las tablas y relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Empleado
            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.EmpleadoID);
                entity.ToTable("Empleados");

                entity.Property(e => e.Legajo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                // Relación con Departamento
                entity.HasOne<Departamento>()
                    .WithMany()
                    .HasForeignKey(e => e.DepartamentoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Departamento
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.DepartamentoID);
                entity.ToTable("Departamentos");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}