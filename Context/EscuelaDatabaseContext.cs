using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TPFinal_PNT1.Models;

namespace TPFinal_PNT1.Context
{
    public class EscuelaDatabaseContext : DbContext
    {
        public EscuelaDatabaseContext(DbContextOptions<EscuelaDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Estudiante> Estudiantes { get; set; }
    }
}