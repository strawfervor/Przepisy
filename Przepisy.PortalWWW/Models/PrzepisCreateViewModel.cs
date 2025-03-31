namespace Przepisy.PortalWWW.Models
{
    public class PrzepisCreateViewModel
    {
        public string Tytul { get; set; }
        public string KrotkiOpis { get; set; }
        public string OpisWykonania { get; set; }
        public string? UrlZdjecia { get; set; }
        public int KuchniaId { get; set; }
        public int GrupaPrzepisuId { get; set; }
        public string Trudnosc { get; set; }

        public List<SkladnikVM> Skladniki { get; set; } = new();
    }

    public class SkladnikVM
    {
        public int SkladnikId { get; set; }
        public double IloscGram { get; set; }
    }
}
