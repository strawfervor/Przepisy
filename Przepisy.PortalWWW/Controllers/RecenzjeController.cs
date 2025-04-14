using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data;

namespace Przepisy.PortalWWW.Controllers
{
    public class RecenzjeController : Controller
    {
        private readonly PrzepisyContext _context;
        public RecenzjeController(PrzepisyContext context)
        {
            _context = context;
        }

        //index będzie wyświetlał recnzje danego użytkownika, domyślnie userId = 1 bo nie ma logowania
        public async Task<IActionResult> Index()
        {
            int userId = 1;
            ViewBag.ModelStrony = await _context.Strona
                .Where(s => s.Pozycja > 0)
                .OrderBy(s => s.Pozycja)
                .ToListAsync();

            var recenzje = await _context.Recenzja
                .Include(r => r.Przepis)
                .Where(r => r.UzytkownikId == userId)
                .OrderByDescending(r => r.DataDodania)
                .ToListAsync();

            return View(recenzje);
        }

    }
}
