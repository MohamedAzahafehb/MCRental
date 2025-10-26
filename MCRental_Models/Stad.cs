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
        public ICollection<Gebruiker>? Gebruikers { get; set; }
        //public ICollection<Gebruiker>? Klanten { get; set; }

        public static List<Stad> seedingData()
        {
            var list = new List<Stad>
            {
                new Stad
                {
                    Naam = "Brussel",
                    Postcode = "1000"
                },
                new Stad
                {
                    Naam = "Antwerpen",
                    Postcode = "2000"
                },
                new Stad
                {
                    Naam = "Leuven",
                    Postcode = "3000"
                },
                new Stad
                {
                    Naam = "Luik",
                    Postcode = "4000"
                },
                new Stad
                {
                    Naam = "Namen",
                    Postcode = "5000"
                },
                new Stad
                {
                    Naam = "Charleroi",
                    Postcode = "6000"
                },
                new Stad
                {
                    Naam = "Bergen",
                    Postcode = "7000"
                },
                new Stad
                {
                    Naam = "Brugge",
                    Postcode = "8000"
                },
                new Stad
                {
                    Naam = "Gent",
                    Postcode = "9000"
                },
            };
            return list;
        }
    }
}
