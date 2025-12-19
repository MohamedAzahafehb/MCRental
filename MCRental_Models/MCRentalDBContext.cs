using MCRental_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Buffers.Text;
using System.Text.Json;

namespace MCRental_Models
{
    public class MCRentalDBContext : IdentityDbContext<Gebruiker>
    {
        public DbSet<Stad> Steden { get; set; }
        public DbSet<Filiaal> Filialen { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }
        public DbSet<Language>  Languages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
                if (!optionsBuilder.IsConfigured)
                {
                    var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddUserSecrets<MCRentalDBContext>(optional: true)
                        .Build();

                    var connStr = config.GetConnectionString("TestConnection");
                try
                {
                    optionsBuilder.UseSqlServer(connStr);
                }
                catch (Exception ex)
                {
                    throw new Exception("Er is een fout opgetreden bij het configureren van de database: " + ex.Message);
                }
            }
            }
            
        

        public static async Task seeder(MCRentalDBContext context)
        {
            try
            {
                Console.WriteLine("seedingg...");
                if (true)
                {
                    //context.Steden.AddRange(Stad.seedingData());
                    //context.SaveChanges();
                    //seedSteden(context);
                }
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
                Language.seedingData(context);
                await Gebruiker.Seeder(context);
                context.SaveChanges();
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
            catch (Exception ex)
            {
                throw new Exception("Er is een fout opgetreden bij het seeden van de Database: " + ex.InnerException.Message);
            }
        }

        private static async void seedSteden(MCRentalDBContext context)
        {
            var client = new HttpClient();
            int limit = 100;
            int offset = 0;
            bool nogData = true;
            List<Stad> steden = new List<Stad>();

            while (nogData)
            {
                string url = $"https://www.odwb.be/api/explore/v2.1/catalog/datasets/postal-codes-belgium/records?select=post_code%2C%20sub_municipality_name_dutch%2C%20sub_municipality_name_french&where=sub_municipality_name_dutch%20is%20not%20null%20or%20sub_municipality_name_french%20is%20not%20null&order_by=post_code&limit={limit}&offset={offset}";

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                var records = doc.RootElement.GetProperty("results");

                if (records.GetArrayLength() == 0)
                {
                    nogData = false;
                    break;
                }

                foreach (var record in records.EnumerateArray())
                {
                    string dutch = record.GetProperty("sub_municipality_name_dutch").GetString();
                    string french = record.GetProperty("sub_municipality_name_french").GetString();
                    string postC = record.GetProperty("post_code").ToString();

                    string cityName = !string.IsNullOrEmpty(dutch) ? dutch :
                              !string.IsNullOrEmpty(french) ? french : null;

                    if (!string.IsNullOrEmpty(cityName))
                    {
                        var stad = new Stad
                        {
                            Naam = cityName,
                            Postcode = postC
                        };
                        context.Steden.Add(stad);
                    }
                }
                context.SaveChanges();
                offset += limit;
            }
        }
    }
}
