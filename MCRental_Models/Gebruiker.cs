using MCRental_Models;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Models
{
    public class Gebruiker : IdentityUser
    {
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public DateTime GeboorteDatum { get; set; }
        [Required]
        public string Adres { get; set; }
        [ForeignKey("Stad")]
        public int StadId { get; set; }
        public Stad? Stad { get; set; }
        public ICollection<Reservatie>? Reservaties { get; set; }


        public static Gebruiker dummy = new Gebruiker
        {
            Voornaam = "Dummy",
            Achternaam = "Dumpfries",
            GeboorteDatum = new DateTime(1999, 1, 1),
            Adres = "Dummystraat 1",
            StadId = 1,
            UserName = "Dummy",
            NormalizedUserName = "DUMMY",
            Email = "Dummy@mcrental.be",
            EmailConfirmed = true,
            PhoneNumber = "0000000000"
        };

        public override string ToString()
        {
            return $"{Voornaam} {Achternaam}";
        }

        public static async Task Seeder()
        {
            MCRentalDBContext context = new MCRentalDBContext();

            // Voeg de nodige rollen toe
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new List<IdentityRole>
                {
                    new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "Klant", Name = "Klant", NormalizedName = "KLANT" }
                });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Add(dummy);
                context.SaveChanges();
                Gebruiker hans = new Gebruiker
                {
                    Voornaam = "Hans",
                    Achternaam = "De Beer",
                    GeboorteDatum = new DateTime(1990, 6, 15),
                    Adres = "Berenstraat 5",
                    StadId = 2,
                    UserName = "hansb",
                    Email = "hans.debeer@mcrental.be",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456789"
                };
                Console.WriteLine("Creating users...");
                Gebruiker admin = new Gebruiker
                {
                    Voornaam = "Admin",
                    Achternaam = "Istrator",
                    GeboorteDatum = new DateTime(1985, 3, 20),
                    Adres = "Adminlaan 10",
                    StadId = 1,
                    UserName = "admin",
                    Email = "admin@mcrental.be",
                    EmailConfirmed = true,
                    PhoneNumber = "0987654321"
                };
                Gebruiker grietje = new Gebruiker
                {
                    Voornaam = "Grietje",
                    Achternaam = "Peeters",
                    GeboorteDatum = new DateTime(1995, 11, 30),
                    Adres = "Peeterstraat 8",
                    StadId = 3,
                    UserName = "grietjep",
                    Email = "grietjep@hotmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0112233445"
                };
                Gebruiker jans = new Gebruiker
                {
                    Voornaam = "JanS",
                    Achternaam = "Smet",
                    GeboorteDatum = new DateTime(1978, 9, 5),
                    Adres = "Smetlaan 12",
                    StadId = 2,
                    UserName = "janss",
                    Email = "janss@hotmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0223344556"
                };

                // Seeding halverwege mislukt, niet alle gebruikers aanwezig + geen rollen toegewezen

                UserManager<Gebruiker> userManager = new UserManager<Gebruiker>(
                    new UserStore<Gebruiker>(context),
                    null, new PasswordHasher<Gebruiker>(),
                    null, null, null, null, null, null);

                await userManager.CreateAsync(hans, "12345678");
                await userManager.CreateAsync(admin, "12345678");
                await userManager.CreateAsync(grietje, "12345678");
                await userManager.CreateAsync(jans, "12345678");

                while (context.Users.Count() < 4)
                {
                    await Task.Delay(100);
                }

                await userManager.AddToRoleAsync(hans, "Klant");
                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(grietje, "Klant");
                await userManager.AddToRoleAsync(jans, "Klant");
            }

            dummy = context.Users.First(u => u.UserName == "Dummy");
        }
        }
}
