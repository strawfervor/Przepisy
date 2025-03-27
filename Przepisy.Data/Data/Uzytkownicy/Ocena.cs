using Przepisy.Data.Data.Przepisy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Uzytkownicy
{
    public class Ocena
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        [Required(ErrorMessage = "Ocena musi być w przedziale 1–5")]
        public int Wartosc { get; set; }

        [Required]
        public int UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }

        [Required]
        public int PrzepisId { get; set; }
        public Przepis? Przepis { get; set; }
    }
}
