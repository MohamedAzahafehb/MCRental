using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRental_Models
{
    public class Language
    {
        public static List<Language> Languages = new List<Language>();
        public static Language Dummy = null;
        [Key]
        public String Code {  get; set; }
        public String Name { get; set; }
        public bool isSystemLanguage { get; set; }
        public DateTime isActive { get; set; }

        public static List<Language> seedingData()
        {
            Languages = new List<Language>
            {
                new Language
                {
                    Code = "-",
                    Name = "dummy",
                    isSystemLanguage = false,
                    isActive = DateTime.MaxValue
                },
                new Language
                {
                    Code = "fr",
                    Name = "francais",
                    isSystemLanguage = true,
                    isActive = DateTime.Now
                },
                new Language
                {
                    Code = "nl",
                    Name = "nederlands",
                    isSystemLanguage = true,
                    isActive = DateTime.Now
                },
                new Language
                {
                    Code = "en",
                    Name = "english",
                    isSystemLanguage = true,
                    isActive = DateTime.Now
                },
            };

            return Languages;
        }
    }
}
