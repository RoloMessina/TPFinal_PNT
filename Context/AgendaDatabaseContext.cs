using Microsoft.EntityFrameworkCore;
using TPFinal_PNT1.Models;

namespace TPFinal_PNT1.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Profesional>().ToTable("Profesionales");

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.TurnosAsignados)
                .WithOne(t => t.Profesional)
                .HasForeignKey(t => t.ProfesionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Turnos)
                .WithOne(t => t.Paciente)
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Tratamientos)
                .WithOne(t => t.Paciente)
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.TratamientosAsignados)
                .WithOne(t => t.Profesional)
                .HasForeignKey(t => t.ProfesionalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
