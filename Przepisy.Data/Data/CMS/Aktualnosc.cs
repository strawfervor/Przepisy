using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.CMS
{
    public class Aktualnosc
    {
        [Key]//to co niżej będzie podstawowym kluczem tabeli
        public int IdAktualnosci { get; set; }

        [Required(ErrorMessage = "Tytuł odnośnika jest wymagany")]//to co niżej jest wymagane
        [MaxLength(20, ErrorMessage = "Link może zawierać max. 20 znaków")]//maksymalna ilośc znaków
        [Display(Name = "Tytuł odnośnika")]//tak ma nazywać się pole widoczne na interface
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł aktualnosci jest wymagany")]
        [MaxLength(50, ErrorMessage = "Tytuł aktualnosci może zawierać max. 50 znaków")]
        [Display(Name = "Tytuł aktualnosci")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]//można zadecydować jaki będzie typ tego pola w bazie danych
        [Required(ErrorMessage = "Treść strony jest wymagana")]
        public required string Tresc { get; set; }

        [Display(Name = "Pozycja wyświetlania")]
        [Required(ErrorMessage = "Wpisz pozycje wyświetlania.")]
        public int Pozycja { get; set; }
    }
}
