using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MCRental_Models
{
    public class Reservatie
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        //[ForeignKey("Klant")]
        //public int KlantId { get; set; }
        [ForeignKey("Gebruiker")]
        public string GebruikerId { get; set; }
        public Gebruiker? Gebruiker { get; set; }
        [ForeignKey("Auto")]
        public int AutoId { get; set; }
        public Auto? Auto { get; set; }
        [Required]
        public DateTime Aanmaking { get; set; } = DateTime.Now;
        public DateTime? Annulatie { get; set; }

        [NotMapped]
        public List<Auto> OnbeschikbareAutos { get; set; } = new List<Auto>();

        // ik kan geen seeddata voorzien voor reservaties want de ID van de gebruiker wordt gegenereerd door IF en ik ken die niet op voorhand


        public static List<Reservatie> seedingData()
        {
            var list = new List<Reservatie>
            {
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 7, 1),
                    EindDatum = new DateTime(2024, 7, 10),
                    GebruikerId = "2",
                    AutoId = 2
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 8, 5),
                    EindDatum = new DateTime(2024, 8, 15),
                    GebruikerId = "2",
                    AutoId = 4
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 9, 10),
                    EindDatum = new DateTime(2024, 9, 20),
                    GebruikerId = "2",
                    AutoId = 5
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 10, 10),
                    EindDatum = new DateTime(2024, 10, 20),
                    GebruikerId = "2",
                    AutoId = 1
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 11, 1),
                    EindDatum = new DateTime(2024, 11, 10),
                    GebruikerId = "5",
                    AutoId = 3
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 12, 15),
                    EindDatum = new DateTime(2024, 12, 25),
                    GebruikerId = "4",
                    AutoId = 5
                }
            };
            return list;
        }

    }
}
