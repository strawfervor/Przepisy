using Przepisy.Data.Data.Przepisy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Uzytkownicy
{
    public class UlubionyPrzepis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }

        [Required]
        public int PrzepisId { get; set; }
        public Przepis? Przepis { get; set; }
    }
}
