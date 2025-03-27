using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Przepisy
{
    public class Kuchnia
    {
        //nazwa Kuchni, np. polska, włoska, bombaska
        [Key]
        public int IdKuchni { get; set; }

        [Required(ErrorMessage = "Nazwa kuchni jest wymagana")]
        [MaxLength(30, ErrorMessage = "Nazwa nie może zawierać więcej niż 30 znaków")]//maksymalna ilośc znaków
        [Display(Name = "Kuchnia")]
        public required string Nazwa { get; set; }

        //dodać relacje jeden do wielu z przepis.
        public ICollection<Przepis> Przepis { get; } = new List<Przepis>();
    }
}
