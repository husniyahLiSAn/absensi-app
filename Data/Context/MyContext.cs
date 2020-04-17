using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data.Context
{
    public class MyContext : IdentityDbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Presence> Presences { get; set; }

        public MyContext() { }

        public MyContext(DbContextOptions<MyContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionStrings = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(connectionStrings);
            }
        }
    }
}
