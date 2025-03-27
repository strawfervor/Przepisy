using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Przepisy
{
    public class PrzepisSkladnik
    {
        [Key]
        public int IdPrzepisSkladnik { get; set; }

        [Required(ErrorMessage = "Id przepisu jest wymagane")]
        public int PrzepisId { get; set; }
        public Przepis Przepis { get; set; } = null!;

        [Required(ErrorMessage = "Id składnika jest wymagane")]
        public int SkladnikId { get; set; }
        public Skladnik Skladnik { get; set; } = null!;

        [Display(Name = "Ilość (gram)")]
        [Required(ErrorMessage = "Waga składnika jest wymagana")]
        [Range(1, 10000)]
        public double IloscGram { get; set; }
    }
}
