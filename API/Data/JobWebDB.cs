using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class JobWebDB : DbContext
    {
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Compañia> Compañia { get; set; }
        public virtual DbSet<PuestoTrabajo> PuestoTrabajo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<configx> configx { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=localhost;port=3306;user=root;password=;database=jobweb.db")
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .AddFilter(level => level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();


        }
    }
}
