using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCRental_Models
{
    public class Filiaal
    {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Telefoon { get; set; }
        [Required]
        public string Email { get; set; }
        [ForeignKey("Stad")]
        public int StadId { get; set; }
        public Stad? Stad { get; set; }

        public ICollection<Auto>? Autos { get; set; }

        public static List<Filiaal> seedingData()
        {
            var list = new List<Filiaal>
            {
                new Filiaal
                {
                    Naam = "Filiaal Dummy",
                    Adres = "Dumstraat 1",
                    Telefoon = "03 123 123 23",
                    Email = "dum.dummy@mcrental.be",
                    StadId = 1
                },
                new Filiaal
                {
                    Naam = "Filiaal Antwerpen",
                    Adres = "Kerkstraat 1",
                    Telefoon = "03 123 45 67",
                    Email = "info.antwerpen@mcrental.be",
                    StadId = 2
                },
                new Filiaal
                {
                    Naam = "Filiaal Brussel",
                    Adres = "Grote Markt 1",
                    Telefoon = "02 123 45 67",
                    Email = "info.brussel@mcrental.be",
                    StadId = 4
                },
                new Filiaal
                {
                    Naam = "Filiaal Leuven",
                    Adres = "Bondgenotenlaan 1",
                    Telefoon = "016 123 45 67",
                    Email = "info.leuven@mcrental.be",
                    StadId = 3
                }
            };
            return list;
        }
    }
}
