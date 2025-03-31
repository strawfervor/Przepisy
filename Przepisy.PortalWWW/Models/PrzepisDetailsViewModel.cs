using Przepisy.Data.Data.Przepisy;
using Przepisy.Data.Data.Uzytkownicy;

namespace Przepisy.PortalWWW.Models
{
    public class PrzepisDetailsViewModel
    {
        public Przepis Przepis { get; set; }
        public List<PrzepisSkladnik>? Skladniki { get; set; }
        public List<Recenzja>? Recenzje { get; set; }
        public bool JuzOcenione { get; set; }
    }
}
