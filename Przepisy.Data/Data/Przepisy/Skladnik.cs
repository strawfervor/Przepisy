using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data.Przepisy
{
    public class Skladnik
    {
        [Key]
        public int IdSkladnika { get; set; }

        [Required(ErrorMessage = "Nazwa składnika jest wymagana")]
        [MaxLength(150, ErrorMessage = "Nazwa składnika nie może być dłuższa niż 150 znaków")]
        public required string Nazwa { get; set; }

        [Display(Name = "Kaloryczność (na 100g)")]
        [Required(ErrorMessage = "Kaloryczność jest wymagana")]
        [Range(0, 1000)]
        public double KalorycznoscNa100g { get; set; }

        [Display(Name = "Przelicznik na sztukę (gramy)")]
        public double? PrzelicznikNaSztuke { get; set; }

        public bool CzyAktywny { get; set; } = false;

        [Display(Name = "Zdjęcie")]
        public string UrlZdjecia { get; set; } = string.Empty;

        public ICollection<PrzepisSkladnik> Przepisy { get; set; } = new List<PrzepisSkladnik>();
    }
}