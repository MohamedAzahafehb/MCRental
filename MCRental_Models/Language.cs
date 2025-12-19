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

        public static List<Language> seedingData(MCRentalDBContext context)
        {
            if (!context.Languages.Any())
            {
                context.Languages.AddRange(
                    new Language { Code = "- ", Name = "?", isSystemLanguage = false, isActive = DateTime.UtcNow },
                    new Language { Code = "en", Name = "English", isSystemLanguage = true, isActive = DateTime.UtcNow },
                    new Language { Code = "nl", Name = "Nederlands", isSystemLanguage = true, isActive = DateTime.UtcNow },
                    new Language { Code = "fr", Name = "français", isSystemLanguage = true, isActive = DateTime.UtcNow }
                );
                context.SaveChanges();
            }
            Languages = context.Languages.Where(l => l.isActive < DateTime.Now).OrderBy(l => l.Name).ToList();
            Dummy = Languages.FirstOrDefault(l => l.Code == "-");

            return Languages;
        }
    }
}
