using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Models;

namespace TPFinal_PNT1.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones entre las entidades

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.TurnosAsignados)
                .WithOne(t => t.Profesional)
                .HasForeignKey(t => t.ProfesionalId);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Turnos)
                .WithOne(t => t.Paciente)
                .HasForeignKey(t => t.PacienteId);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Tratamientos)
                .WithOne(t => t.Paciente)
                .HasForeignKey(t => t.PacienteId);

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.TratamientosAsignados)
                .WithOne(t => t.Profesional)
                .HasForeignKey(t => t.ProfesionalId);

            // Configurar Fecha como entidad sin clave
            modelBuilder.Entity<Fecha>().HasNoKey();
        }
    }
}
