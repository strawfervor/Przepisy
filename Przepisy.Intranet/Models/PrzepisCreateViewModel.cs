using Przepisy.Data.Data.Przepisy;

namespace Przepisy.Intranet.Models
{
    public class PrzepisCreateViewModel
    {
        public Przepis Przepis { get; set; }
        public List<PrzepisSkladnikInput> Skladniki { get; set; } = new();
    }

    public class PrzepisSkladnikInput
    {
        public int SkladnikId { get; set; }
        public double IloscGram { get; set; }
    }
}
