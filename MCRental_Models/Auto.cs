using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Models
{
    public class Auto //: INotifyPropertyChanged
    {
        public int Id { get; set; }
        [Required]
        public string Merk { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Nummerplaat { get; set; }
        [Required]
        public double DagPrijs { get; set; }
        [Required]
        public bool Beschikbaar { get; set; }
        [Required]
        public string type { get; set; }

        [ForeignKey("Filiaal")]
        public int FiliaalId { get; set; }
        public Filiaal? Filiaal { get; set; }

        public ICollection<Reservatie>? Reservaties { get; set; }

        //public event PropertyChangedEventHandler? PropertyChanged;

        public static List<Auto> seedingData()
        {
            var list = new List<Auto>
            {
                new Auto
                {
                    Merk = "Dumcar",
                    Model = "Dummola",
                    Nummerplaat = "1-ABC-123",
                    DagPrijs = 100.0,
                    Beschikbaar = true,
                    type = "Sedan",
                    FiliaalId = 1
                },
                new Auto
                {
                    Merk = "Toyota",
                    Model = "Corolla",
                    Nummerplaat = "1-ABC-123",
                    DagPrijs = 70.0,
                    Beschikbaar = true,
                    type = "Sedan",
                    FiliaalId = 2
                },
                new Auto
                {
                    Merk = "Ford",
                    Model = "Focus",
                    Nummerplaat = "1-DEF-456",
                    DagPrijs = 65.0,
                    Beschikbaar = true,
                    type = "Hatchback",
                    FiliaalId = 3
                },
                new Auto
                {
                    Merk = "BMW",
                    Model = "X3",
                    Nummerplaat = "1-GHI-789",
                    DagPrijs = 95.0,
                    Beschikbaar = false,
                    type = "SUV",
                    FiliaalId = 4
                },
                new Auto
                {
                    Merk = "Audi",
                    Model = "A4",
                    Nummerplaat = "1-JKL-012",
                    DagPrijs = 90.0,
                    Beschikbaar = true,
                    type = "Sedan",
                    FiliaalId = 2
                },
                new Auto
                {
                    Merk = "Volkswagen",
                    Model = "Golf",
                    Nummerplaat = "1-MNO-345",
                    DagPrijs = 75.0,
                    Beschikbaar = true,
                    type = "Hatchback",
                    FiliaalId = 3
                },
                new Auto
                {
                    Merk = "Mercedes",
                    Model = "GLC",
                    Nummerplaat = "1-PQR-678",
                    DagPrijs = 100.0,
                    Beschikbaar = false,
                    type = "SUV",
                    FiliaalId = 4
                }
            };
            return list;
        }
    }
}
