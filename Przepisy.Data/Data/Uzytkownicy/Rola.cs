using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Uzytkownicy
{
    public class Rola
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa roli jest wymagana")]
        [MaxLength(30)]
        [Display(Name = "Rola")]
        public required string Nazwa { get; set; }

        [MaxLength(200)]
        [Display(Name = "Opis roli")]
        public string? Opis { get; set; }

        [Display(Name = "Cena abonamentu (zł)")]
        [Range(0, 2000)]
        [Column(TypeName = "money")]
        public decimal CenaAbonamentu { get; set; }

        public ICollection<Uzytkownik> Uzytkownicy { get; set; } = new List<Uzytkownik>();
    }
}
