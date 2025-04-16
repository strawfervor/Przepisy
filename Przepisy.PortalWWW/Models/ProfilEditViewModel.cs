namespace Przepisy.PortalWWW.Models
{
    public class ProfilEditViewModel
    {
        public int Id { get; set; }
        public string NazwaUzytkownika { get; set; }
        public string? Opis { get; set; }
        public string? UrlAwataru { get; set; }

        public IFormFile? Avatar { get; set; }
    }
}
