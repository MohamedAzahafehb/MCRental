using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MCRental_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MCRental_Models
{
    public class MCRentalDBContext : DbContext
    {
        public DbSet<Stad> Steden { get; set; }
        public DbSet<Filiaal> Filialen { get; set; }
        //public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "Server=(localdb)\\mssqllocaldb; Database=MCRentalDB;Trusted_Connection=true;MultipleActiveResultSets=true;";
            //optionsBuilder.UseSqlServer(connectionString);

            if(!optionsBuilder.IsConfigured)
            {
                    var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddUserSecrets<MCRentalDBContext>(optional: true)
                        .Build();

                    var connStr = config.GetConnectionString("DefaultConnection");
                    optionsBuilder.UseSqlServer(connStr);
            }
        }

        public static void seeder(MCRentalDBContext context)
        {
            if (!context.Steden.Any())
            {
                context.Steden.AddRange(Stad.seedingData());
                context.SaveChanges();
            }
            if (!context.Filialen.Any())
            {
                context.Filialen.AddRange(Filiaal.seedingData());
                context.SaveChanges();
            }
            //Gebruiker.seedingData(); // Ensure seeding data is generated
            if (!context.Autos.Any())
            {
                context.Autos.AddRange(Auto.seedingData());
                context.SaveChanges();
            }
            if (!context.Reservaties.Any())
            {
                context.Reservaties.AddRange(Reservatie.seedingData());
                context.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Console.WriteLine("OnModelCreating called - seeding data...");

            modelBuilder.Entity<Stad>().HasData(Stad.seedingData());
            Console.WriteLine("Steden done");
            modelBuilder.Entity<Filiaal>().HasData(Filiaal.seedingData());
            Console.WriteLine("Filialen done");
            //modelBuilder.Entity<Gebruiker>().HasData(Gebruiker.seedingData());
            //Console.WriteLine("Klanten done");
            modelBuilder.Entity<Auto>().HasData(Auto.seedingData());
            Console.WriteLine("Autos done");
            modelBuilder.Entity<Reservatie>().HasData(Reservatie.seedingData());
            Console.WriteLine("Reservaties done");

        }
    }
}
