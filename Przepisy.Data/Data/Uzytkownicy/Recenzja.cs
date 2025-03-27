using Przepisy.Data.Data.Przepisy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Uzytkownicy
{
    public class Recenzja
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Treść recenzji jest wymagana")]
        [Display(Name = "Recenzja")]
        [MaxLength(1000)]
        public string Tresc { get; set; } = string.Empty;

        [Display(Name = "Data dodania")]
        public DateTime DataDodania { get; set; } = DateTime.Now;

        [Required]
        public int UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }

        [Required]
        public int PrzepisId { get; set; }
        public Przepis? Przepis { get; set; }
    }
}
