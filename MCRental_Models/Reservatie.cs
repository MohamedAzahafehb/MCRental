using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCRental_Models
{
    public class Reservatie
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        [ForeignKey("Klant")]
        public int KlantId { get; set; }
        //public Gebruiker? Klant { get; set; }
        //[ForeignKey("Auto")]
        public int AutoId { get; set; }
        public Auto? Auto { get; set; }


        public static List<Reservatie> seedingData()
        {
            var list = new List<Reservatie>
            {
                new Reservatie
                {
                    Id = 1,
                    StartDatum = new DateTime(2024, 7, 1),
                    EindDatum = new DateTime(2024, 7, 10),
                    KlantId = 1,
                    AutoId = 2
                },
                new Reservatie
                {
                    Id = 2,
                    StartDatum = new DateTime(2024, 8, 5),
                    EindDatum = new DateTime(2024, 8, 15),
                    KlantId = 1,
                    AutoId = 1
                },
                new Reservatie
                {
                    Id = 3,
                    StartDatum = new DateTime(2024, 9, 10),
                    EindDatum = new DateTime(2024, 9, 20),
                    KlantId = 3,
                    AutoId = 5
                }
            };
            return list;
        }

    }
}
