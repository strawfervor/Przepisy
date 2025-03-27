using Przepisy.Data.Data.Przepisy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Uzytkownicy
{
    public class Uzytkownik
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [MaxLength(30)]
        public string NazwaUzytkownika { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public required string Haslo { get; set; }

        [MaxLength(1000, ErrorMessage = "Maksymalna długość wynosi 1000 znaków.")]
        [Display(Name = "Opis (bio)")]
        public string? Opis { get; set; }

        [Display(Name = "URL awatara")]
        public string? UrlAwataru { get; set; }

        [ForeignKey("Rola")]
        public int RolaId { get; set; }
        public Rola? Rola { get; set; }

        public ICollection<Przepis> DodanePrzepisy { get; set; } = new List<Przepis>();
        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();
        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
        public ICollection<UlubionyPrzepis> UlubionePrzepisy { get; set; } = new List<UlubionyPrzepis>();
    }
}
