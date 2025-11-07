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

        //Hans: dfb77b1e-87df-4474-a679-763fe1a8af08
        //Grietje: 9d5f0aa9-190b-407b-b4bc-c97b15503eb3


        public static List<Reservatie> seedingData()
        {
            var list = new List<Reservatie>
            {
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 7, 1),
                    EindDatum = new DateTime(2024, 7, 10),
                    GebruikerId = "6e22436b-285e-4a51-874d-1442e4f33a9d",
                    AutoId = 2
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 8, 5),
                    EindDatum = new DateTime(2024, 8, 15),
                    GebruikerId = "6e22436b-285e-4a51-874d-1442e4f33a9d",
                    AutoId = 1
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 9, 10),
                    EindDatum = new DateTime(2024, 9, 20),
                    GebruikerId = "6e22436b-285e-4a51-874d-1442e4f33a9d",
                    AutoId = 5
                },
                new Reservatie
                {
                    StartDatum = new DateTime(2024, 10, 10),
                    EindDatum = new DateTime(2024, 10, 20),
                    GebruikerId = "6e22436b-285e-4a51-874d-1442e4f33a9d",
                    AutoId = 5
                }
            };
            return list;
        }

    }
}
