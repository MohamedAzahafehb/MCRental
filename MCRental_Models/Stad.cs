using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Models
{
    public class Stad
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Postcode { get; set; }
        public ICollection<Filiaal>? Filialen { get; set; }
        public ICollection<Klant>? Klanten { get; set; }

        public static List<Stad> seedingData()
        {
            var list = new List<Stad>
            {
                new Stad
                {
                    Id = 1,
                    Naam = "Brussel",
                    Postcode = "1000"
                },
                new Stad
                {
                    Id = 2,
                    Naam = "Antwerpen",
                    Postcode = "2000"
                },
                new Stad
                {
                    Id = 3,
                    Naam = "Leuven",
                    Postcode = "3000"
                },
                new Stad
                {
                    Id = 4,
                    Naam = "Luik",
                    Postcode = "4000"
                },
                new Stad
                {
                    Id = 5,
                    Naam = "Namen",
                    Postcode = "5000"
                },
                new Stad
                {
                    Id = 6,
                    Naam = "Charleroi",
                    Postcode = "6000"
                },
                new Stad
                {
                    Id = 7,
                    Naam = "Bergen",
                    Postcode = "7000"
                },
                new Stad
                {
                    Id = 8,
                    Naam = "Brugge",
                    Postcode = "8000"
                },
                new Stad
                {
                    Id = 9,
                    Naam = "Gent",
                    Postcode = "9000"
                },
            };
            return list;
        }
    }
}
