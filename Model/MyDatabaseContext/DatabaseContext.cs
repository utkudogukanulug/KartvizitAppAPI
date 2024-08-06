using Microsoft.EntityFrameworkCore;
using RestApiProject.Model.Entities;

namespace RestApiProject.Model.MyDatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<CardVisit> CardVisits { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CardVisit>()
                .HasData(
                    new CardVisit
                    {
                        Id = 1,
                        Title = "Dogukan",
                        Phone = "123",
                        Name = "Dogukan",
                        Email = "dogukan@gmail.com",
                        Address = "Esenyurt"
                    },
                    new CardVisit
                    {
                        Id = 2,
                        Title = "Hakan",
                        Phone = "999",
                        Name = "Hakan",
                        Email = "hakan@gmail.com",
                        Address = "Esenyurt"
                    }
                );
        }

    }
}

