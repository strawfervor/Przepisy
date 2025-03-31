using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data;
using Przepisy.Data.Data.Przepisy;

namespace Przepisy.PortalWWW.Controllers
{
    public class PrzepisyController : Controller
    {
        private readonly PrzepisyContext _context;
        public PrzepisyController(PrzepisyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? kuchniaId, int? grupaId, int page = 1)
        {
            int pageSize = 12;

            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            ViewBag.ModelRodzaje = await _context.Kuchnia.ToListAsync();
            ViewBag.ModelGrupy = await _context.GrupaPrzepisu.ToListAsync();

            var przepisyQuery = _context.Przepis
                .Include(p => p.Kuchnia)
                .Include(p => p.GrupaPrzepisu)
                .Where(p => p.CzyAktywny);

            if (kuchniaId.HasValue)
                przepisyQuery = przepisyQuery.Where(p => p.KuchniaId == kuchniaId);

            if (grupaId.HasValue)
                przepisyQuery = przepisyQuery.Where(p => p.GrupaPrzepisuId == grupaId);

            int totalCount = await przepisyQuery.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SelectedKuchnia = kuchniaId;
            ViewBag.SelectedGrupa = grupaId;

            var przepisy = await przepisyQuery
                .OrderByDescending(p => p.IdPrzepisu)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(przepisy);
        }
    }
}
