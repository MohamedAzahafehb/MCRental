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
        public string Naam { get; set; } = string.Empty;
        static Gebruiker dummy = new Gebruiker
        {
            Naam = "-",
            UserName = "Dummy",
            NormalizedUserName = "DUMMY",
            Email = "Dummy@Agenda.be"
        };


        public static async Task Seeder()
        {
            MCRentalDBContext context = new MCRentalDBContext();

            // Voeg de nodige rollen toe
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new List<IdentityRole>
                {
                    new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" }
                });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Add(dummy);
                context.SaveChanges();
                Gebruiker user = new Gebruiker { UserName = "user", Naam = "User", Email = "user.Test@Agenda.be", EmailConfirmed = true };
                Gebruiker admin = new Gebruiker { UserName = "admin", Naam = "Admin", Email = "admin.Test@Agenda.be", EmailConfirmed = true };
                Gebruiker systemAdmin = new Gebruiker { UserName = "systemA", Naam = "SystemA", Email = "systemA.Test@Agenda.be", EmailConfirmed = true };
                Gebruiker UserAdmin = new Gebruiker { UserName = "userA", Naam = "UserA", Email = "userA.Test@Agenda.be", EmailConfirmed = true };
                UserManager<Gebruiker> userManager = new UserManager<Gebruiker>(
                    new UserStore<Gebruiker>(context),
                    null, new PasswordHasher<Gebruiker>(),
                    null, null, null, null, null, null);

                await userManager.CreateAsync(user, "12345678");
                await userManager.CreateAsync(admin, "12345678");
                await userManager.CreateAsync(systemAdmin, "12345678");
                await userManager.CreateAsync(UserAdmin, "12345678");

                while (context.Users.Count() < 5)
                {
                    await Task.Delay(100);
                }

                await userManager.AddToRoleAsync(user, "User");
                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(systemAdmin, "Admin");
                await userManager.AddToRoleAsync(UserAdmin, "User");
            }

            dummy = context.Users.First(u => u.UserName == "Dummy");
        }
            //        public string Voornaam { get; set; }
            //        [Required]
            //        public string Achternaam { get; set; }
            //        [Required]
            //        public DateTime GeboorteDatum { get; set; }
            //        [Required]
            //        public string Adres { get; set; }
            //        [ForeignKey("Stad")]
            //        public int StadId { get; set; }
            //        public Stad? Stad { get; set; }
            //        public ICollection<Reservatie>? Reservaties { get; set; }


            //        public static async Task seedingData()
            //        {
            //            MCRentalDBContext context = new MCRentalDBContext();
            //            if (!context.Roles.Any())
            //            {
            //                context.Roles.AddRange(new List<IdentityRole>
            //                {
            //                    new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
            //                    new IdentityRole { Id = "Klant", Name = "Klant", NormalizedName = "KLANT" }
            //                });
            //                context.SaveChanges();
            //            }

            //            if (!context.Users.Any())
            //            {
            //                Gebruiker Jan = new Gebruiker
            //                {
            //                    Voornaam = "Jan",
            //                    Achternaam = "Janssens",
            //                    UserName = "Jan_Janssens",
            //                    PhoneNumber = "0470 123 456",
            //                    Email = "jan.janssens@hotmail.com",
            //                    EmailConfirmed = true,
            //                    GeboorteDatum = new DateTime(1985, 5, 15),
            //                    Adres = "Kerkstraat 10",
            //                    StadId = 2
            //                };
            //                Gebruiker Piet = new Gebruiker
            //                {
            //                    Voornaam = "Piet",
            //                    Achternaam = "Pieters",
            //                    UserName = "Piet_Pieters",
            //                    PhoneNumber = "0471 654 321",
            //                    Email = "piet.pieters@hotmail.com",
            //                    EmailConfirmed = true,
            //                    GeboorteDatum = new DateTime(1990, 8, 20),
            //                    Adres = "Markt 5",
            //                    StadId = 1
            //                };
            //                Gebruiker Klaas = new Gebruiker
            //                {
            //                    Voornaam = "Klaas",
            //                    Achternaam = "Klaassen",
            //                    UserName = "Klaas_Klaassen",
            //                    PhoneNumber = "0472 789 012",
            //                    Email = "klaas.klaassen@hotmail.com",
            //                    EmailConfirmed = true,
            //                    GeboorteDatum = new DateTime(1978, 12, 30),
            //                    Adres = "Bondgenotenlaan 3",
            //                    StadId = 3
            //                };

            //                UserManager<Gebruiker> userManager = new UserManager<Gebruiker>(
            //                new UserStore<Gebruiker>(context),
            //                null, new PasswordHasher<Gebruiker>(),
            //                null, null, null, null, null, null
            //                );

            //                await userManager.CreateAsync(Jan, "P@ssword1");
            //                await userManager.CreateAsync(Piet, "P@ssword1");
            //                await userManager.CreateAsync(Klaas, "P@ssword1");

            //                //zeker zijn dat de gebruikers zijn aangemaakt voor je rollen toekent
            //                while (context.Users.Count() < 4)
            //                {
            //                    await Task.Delay(100);
            //                }

            //                await userManager.AddToRoleAsync(Jan, "Klant");
            //                await userManager.AddToRoleAsync(Piet, "Klant");
            //                await userManager.AddToRoleAsync(Klaas, "Admin");
            //                context.SaveChanges();
            //            }
            //        }
        }
}
