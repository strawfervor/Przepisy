using Microsoft.AspNetCore.Mvc;
using Przepisy.Data.Data.Uzytkownicy;
using Przepisy.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Przepisy.PortalWWW.Controllers
{
    public class UlubioneController : Controller
    {
        private readonly PrzepisyContext _context;

        public UlubioneController(PrzepisyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1; //user do zmiany potem, jak już będzie logowanie (jeśli xD)

            ViewBag.ModelStrony = await _context.Strona
                .Where(s => s.Pozycja > 0)
                .OrderBy(s => s.Pozycja)
                .ToListAsync();

            var ulubionePrzepisy = await _context.UlubionyPrzepis
                .Where(up => up.UzytkownikId == userId)
                .Include(up => up.Przepis)
                    .ThenInclude(p => p.Kuchnia)
                .Include(up => up.Przepis)
                    .ThenInclude(p => p.GrupaPrzepisu)
                .Select(up => up.Przepis)
                .ToListAsync();

            return View(ulubionePrzepisy);
        }

        [HttpPost]
        public async Task<IActionResult> DodajUlubiony(int przepisId)
        {
            int userId = 1; //nie ma logowania wieć user id jest zawsze 1

            var istnieje = await _context.UlubionyPrzepis
                .AnyAsync(u => u.UzytkownikId == userId && u.PrzepisId == przepisId);

            if (!istnieje)
            {
                _context.UlubionyPrzepis.Add(new UlubionyPrzepis
                {
                    UzytkownikId = userId,
                    PrzepisId = przepisId
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Przepisy", new { id = przepisId });
        }

        [HttpPost]
        public async Task<IActionResult> UsunUlubiony(int przepisId)
        {
            int userId = 1; //nie ma logowania wieć user id jest zawsze 1

            var wpis = await _context.UlubionyPrzepis
                .FirstOrDefaultAsync(u => u.UzytkownikId == userId && u.PrzepisId == przepisId);

            if (wpis != null)
            {
                _context.UlubionyPrzepis.Remove(wpis);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Przepisy", new { id = przepisId });
        }
    }
}
