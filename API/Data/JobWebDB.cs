﻿using API.Models;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string pass = Environment.GetEnvironmentVariable("AWS_PASS");
            optionsBuilder
                .UseMySql($"server=jobwebdb.c7e2jzuup8ra.us-east-2.rds.amazonaws.com;port=3306;user=admin;password={pass};database=jobweb.db")
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .AddFilter(level => level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();


        }
    }
}
