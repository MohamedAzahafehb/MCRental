using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Models
{
    public class Klant
    {
        public int Id { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public string Telefoon { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime GeboorteDatum { get; set; }
        [Required]
        public string Adres { get; set; }
        [ForeignKey("Stad")]
        public int StadId { get; set; }
        public Stad? Stad { get; set; }
        public ICollection<Reservatie>? Reservaties { get; set; }


        public static List<Klant> seedingData()
        {
            var list = new List<Klant>
            {
                new Klant
                {
                    Id = 1,
                    Voornaam = "Jan",
                    Achternaam = "Janssens",
                    Telefoon = "0470 123 456",
                    Email = "jan.janssens@hotmail.com",
                    GeboorteDatum = new DateTime(1985, 5, 15),
                    Adres = "Kerkstraat 10",
                    StadId = 2
                },
                new Klant
                {
                    Id = 2,
                    Voornaam = "Piet",
                    Achternaam = "Pieters",
                    Telefoon = "0471 654 321",
                    Email = "piet.pieters@hotmail.com",
                    GeboorteDatum = new DateTime(1990, 8, 20),
                    Adres = "Markt 5",
                    StadId = 1
                },
                new Klant
                {
                    Id = 3,
                    Voornaam = "Klaas",
                    Achternaam = "Klaassen",
                    Telefoon = "0472 789 012",
                    Email = "klaas.klaassen@hotmail.com",
                    GeboorteDatum = new DateTime(1978, 12, 30),
                    Adres = "Bondgenotenlaan 3",
                    StadId = 3
                }
            };
            return list;
        }
    }
}
