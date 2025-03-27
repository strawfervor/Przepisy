using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Przepisy
{
    public class GrupaPrzepisu
    {
        //Czyli np. śniadanie, obiad, kolacja, launch
        [Key]
        public int IdGrupyPrzepisu { get; set; }

        [Required(ErrorMessage = "Nazwa grupy przepisów jest wymagana")]//to co niżej jest wymagane
        [MaxLength(20, ErrorMessage = "Nazwa nie może zawierać więcej niż 20 znaków")]//maksymalna ilośc znaków
        [Display(Name = "Nazwa grupy przepisów")]//tak ma nazywać się pole widoczne na interface
        public required string Nazwa { get; set; }

        //dodać relacje jeden do wielu z przepis:
        public ICollection<Przepis> Przepis { get; } = new List<Przepis>();
    }
}
