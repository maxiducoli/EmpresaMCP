using EmpresaMCP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Runtime.InteropServices;

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
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Asistencias> Asistencias { get; set; }
        public DbSet<Cargos> Cargos { get; set; }
        public DbSet<Contratos> Contratos { get; set; }
        public DbSet<Salarios> Salarios { get; set; }
        public DbSet<Sectores> Sectores { get; set; }
        public DbSet<Plantas> Plantas { get; set; }


        // Configuración adicional de las tablas y relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================================
            // EMPLEADOS
            // ============================================
            modelBuilder.Entity<Empleados>(entity =>
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

                entity.Property(e => e.Email)
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20);

                entity.Property(e => e.DNI)
                    .HasMaxLength(20);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(50);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(50);

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10);

                // Relaciones con otras tablas
                entity.HasOne<Cargos>()
                    .WithMany()
                    .HasForeignKey(e => e.CargoID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Sectores>()
                    .WithMany()
                    .HasForeignKey(e => e.SectorID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Departamentos>()
                    .WithMany()
                    .HasForeignKey(e => e.DepartamentoID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Plantas>()
                    .WithMany()
                    .HasForeignKey(e => e.PlantaID)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación recursiva (JefeDirectoID → otro Empleado)
                entity.HasOne<Empleados>()
                    .WithMany()
                    .HasForeignKey(e => e.JefeDirectoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // ASISTENCIAS
            // ============================================
            modelBuilder.Entity<Asistencias>(entity =>
            {
                entity.HasKey(e => e.AsistenciaID);
                entity.ToTable("Asistencias");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500);

                entity.HasOne<Empleados>()
                    .WithMany()
                    .HasForeignKey(e => e.EmpleadoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // CARGOS
            // ============================================
            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.HasKey(e => e.CargoID);
                entity.ToTable("Cargos");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500);

                entity.Property(e => e.SalarioMinimo)
                    .HasPrecision(18, 2);

                entity.Property(e => e.SalarioMaximo)
                    .HasPrecision(18, 2);
            });

            // ============================================
            // CONTRATOS
            // ============================================
            modelBuilder.Entity<Contratos>(entity =>
            {
                entity.HasKey(e => e.ContratoID);
                entity.ToTable("Contratos");

                entity.Property(e => e.TipoContrato)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModalidadTrabajo)
                    .HasMaxLength(50);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(500);

                entity.HasOne<Empleados>()
                    .WithMany()
                    .HasForeignKey(e => e.EmpleadoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // DEPARTAMENTOS
            // ============================================
            modelBuilder.Entity<Departamentos>(entity =>
            {
                entity.HasKey(e => e.DepartamentoID);
                entity.ToTable("Departamentos");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500);

                entity.Property(e => e.PresupuestoAnual)
                    .HasPrecision(18, 2);

                entity.HasOne<Plantas>()
                    .WithMany()
                    .HasForeignKey(e => e.PlantaID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // PLANTA
            // ============================================
            modelBuilder.Entity<Plantas>(entity =>
            {
                entity.HasKey(e => e.PlantaID);
                entity.ToTable("Plantas");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(50);

                entity.Property(e => e.Provincia)
                    .HasMaxLength(50);

                entity.Property(e => e.Pais)
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .HasMaxLength(100);
            });

            // ============================================
            // SALARIOS
            // ============================================
            modelBuilder.Entity<Salarios>(entity =>
            {
                entity.HasKey(e => e.SalarioID);
                entity.ToTable("Salarios");

                entity.Property(e => e.Moneda)
                    .HasMaxLength(10);

                entity.Property(e => e.SalarioBase)
                    .IsRequired()
                    .HasPrecision(18, 2);

                entity.Property(e => e.Bonificaciones)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Deducciones)
                    .HasPrecision(18, 2);

                entity.Property(e => e.SalarioNeto)
                    .HasPrecision(18, 2);

                entity.HasOne<Empleados>()
                    .WithMany()
                    .HasForeignKey(e => e.EmpleadoID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ============================================
            // SECTORES
            // ============================================
            modelBuilder.Entity<Sectores>(entity =>
            {
                entity.HasKey(e => e.SectorID);
                entity.ToTable("Sectores");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500);

                entity.HasOne<Departamentos>()
                    .WithMany()
                    .HasForeignKey(e => e.DepartamentoID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Empleados>()
                    .WithMany()
                    .HasForeignKey(e => e.JefeSectorID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}