using Przepisy.Data.Data.Uzytkownicy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Przepisy
{
    public class Przepis
    {
        [Key]
        public int IdPrzepisu { get; set; }

        [Required(ErrorMessage = "Tytuł przepisu jest wymagany")]
        [Display(Name = "Tytuł przepisu")]
        [MaxLength(100)]
        public required string Tytul { get; set; }

        [MaxLength(300)]
        [Display(Name = "Krótki opis przepisu")]
        public string KrotkiOpis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis przepisu jest wymagany")]
        [Display(Name = "Opis przygotowania")]
        [Column(TypeName = "nvarchar(MAX)")]
        public required string OpisWykonania { get; set; }

        [Required(ErrorMessage = "Proszę wybrać trudność")]
        public Trudnosc Trudnosc { get; set; } = Trudnosc.Sredni;//tutaj będzie komboboks

        [Display(Name = "Zdjęcie potrawy")]
        public string UrlZdjecia { get; set; } = string.Empty;

        public bool CzyAktywny { get; set; } = false;

        [Display(Name = "Średnia ocena")]
        public double SredniaOcena { get; set; } = 0.0;

        [ForeignKey("Autor")]
        public int AutorId { get; set; }
        public Uzytkownik Autor { get; set; } = null!;

        [ForeignKey("Kuchnia")]
        public int KuchniaId { get; set; }
        public Kuchnia Kuchnia { get; set; } = null!;

        [ForeignKey("GrupaPrzepisu")]
        public int GrupaPrzepisuId { get; set; }
        public GrupaPrzepisu GrupaPrzepisu { get; set; } = null!;

        public ICollection<PrzepisSkladnik> Skladniki { get; set; } = new List<PrzepisSkladnik>();
        public ICollection<Ocena> Oceny { get; set; } = new List<Ocena>();
        public ICollection<Recenzja> Recenzje { get; set; } = new List<Recenzja>();
        public ICollection<UlubionyPrzepis> Ulubione { get; set; } = new List<UlubionyPrzepis>();
    }
}
