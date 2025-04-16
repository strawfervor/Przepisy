using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Przepisy.Data.Data;
using Przepisy.Data.Data.Przepisy;
using Przepisy.Data.Data.Uzytkownicy;
using Przepisy.PortalWWW.Models;

namespace Przepisy.PortalWWW.Controllers
{
    public class PrzepisyController : Controller
    {
        private readonly PrzepisyContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PrzepisyController(PrzepisyContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(int? kuchniaId, int? grupaId, int page = 1)
        {
            int pageSize = 12;

            ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
            ViewBag.ModelRodzaje = await _context.Kuchnia.ToListAsync();
            ViewBag.ModelGrupy = await _context.GrupaPrzepisu.ToListAsync();
            ViewBag.ModelStrony = await _context.Strona
                .Where(s => s.Pozycja > 0)
                .OrderBy(s => s.Pozycja)
                .ToListAsync();

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

            int userId = 1; // tymczasowo (docelowo z autoryzacji)

            ViewBag.Ulubione = await _context.UlubionyPrzepis
                .Where(up => up.UzytkownikId == userId)
                .Select(up => up.PrzepisId)
                .ToListAsync();

            return View(przepisy);
        }

        //
        //Create poniżej
        //

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Kuchnie = new SelectList(await _context.Kuchnia.ToListAsync(), "IdKuchni", "Nazwa");
            ViewBag.Grupy = new SelectList(await _context.GrupaPrzepisu.ToListAsync(), "IdGrupyPrzepisu", "Nazwa");
            ViewBag.Trudnosci = Enum.GetNames(typeof(Trudnosc));

            ViewBag.ModelStrony = await _context.Strona
                .Where(s => s.Pozycja > 0)
                .OrderBy(s => s.Pozycja)
                .ToListAsync();

            ViewBag.Skladniki = await _context.Skladnik
                .Where(s => s.CzyAktywny)
                .OrderBy(s => s.Nazwa)
                .ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrzepisCreateViewModel model, IFormFile zdjecie)
        {
            // uzupełnianie viewbagów jeszcze raz, żeby przeszło walidacje
            async Task UzupelnijViewBagi()
            {
                ViewBag.Kuchnie = new SelectList(await _context.Kuchnia.ToListAsync(), "IdKuchni", "Nazwa");
                ViewBag.Grupy = new SelectList(await _context.GrupaPrzepisu.ToListAsync(), "IdGrupyPrzepisu", "Nazwa");
                ViewBag.Trudnosci = Enum.GetNames(typeof(Trudnosc));

                ViewBag.ModelStrony = await _context.Strona
                    .Where(s => s.Pozycja > 0)
                    .OrderBy(s => s.Pozycja)
                    .ToListAsync();

                ViewBag.Skladniki = await _context.Skladnik
                    .Where(s => s.CzyAktywny)
                    .OrderBy(s => s.Nazwa)
                    .ToListAsync();
            }

            ModelState.Remove("zdjecie");// żeby nie wywałało błędu walidacji, gdy nie ma obrazku

            if (!ModelState.IsValid)
            {
                await UzupelnijViewBagi();
                return View(model);
            }

            // upload i obsługa zjęć, jesli nie upload zdjecia to wtedu użuwa domyślne
            string urlZdjecia = "/images/przepisy/default.jpeg";//zamiast elsa po tym ifie, od razu domyślny obrazek, a potem ew. podmianka stringa
            if (zdjecie != null && zdjecie.Length > 0)
            {
                var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "przepisy");
                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(zdjecie.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await zdjecie.CopyToAsync(stream);
                }

                urlZdjecia = "/images/przepisy/" + fileName;
            }
            //else
            //{
            //    // defautowy obrazek
            //    urlZdjecia = "/images/przepisy/default.jpeg";
            //}

            // dodawnaie przpeisy do "kolekcji"
            var przepis = new Przepis
            {
                Tytul = model.Tytul,
                KrotkiOpis = model.KrotkiOpis,
                OpisWykonania = model.OpisWykonania,
                UrlZdjecia = urlZdjecia,
                KuchniaId = model.KuchniaId,
                GrupaPrzepisuId = model.GrupaPrzepisuId,
                Trudnosc = Enum.Parse<Trudnosc>(model.Trudnosc),
                CzyAktywny = false,
                AutorId = 1 // nie ma logownia, więc będzie domyslnie autor 1
            };

            _context.Przepis.Add(przepis);
            Console.WriteLine($"Dodawanie: {przepis.Tytul}, składników: {model.Skladniki.Count}");
            await _context.SaveChangesAsync(); // potrzebne, żeby mieć Id przepisu

            // dodawanie skłądników do przpeisu
            foreach (var s in model.Skladniki)
            {
                _context.PrzepisSkladnik.Add(new PrzepisSkladnik
                {
                    PrzepisId = przepis.IdPrzepisu,
                    SkladnikId = s.SkladnikId,
                    IloscGram = s.IloscGram
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //
        //Ditejlsy poniżej
        //
        public async Task<IActionResult> Details(int id)
        {
            int userId = 1; // tymczasowo (docelowo z autoryzacji)
            var przepis = await _context.Przepis
                .Include(p => p.Kuchnia)
                .Include(p => p.GrupaPrzepisu)
                .FirstOrDefaultAsync(p => p.IdPrzepisu == id);

            if (przepis == null)
                return NotFound();

            var skladniki = await _context.PrzepisSkladnik
                .Include(ps => ps.Skladnik)
                .Where(ps => ps.PrzepisId == id)
                .ToListAsync();

            var recenzje = await _context.Recenzja
                .Include(r => r.Uzytkownik)
                .Where(r => r.PrzepisId == id)
                .OrderByDescending(r => r.DataDodania)
                .ToListAsync();

            ViewBag.Ulubione = await _context.UlubionyPrzepis
                .Where(up => up.UzytkownikId == userId)
                .Select(up => up.PrzepisId)
                .ToListAsync();

            bool juzOcenione = await _context.Ocena.AnyAsync(o => o.PrzepisId == id && o.UzytkownikId == userId);

            var model = new PrzepisDetailsViewModel
            {
                Przepis = przepis,
                Skladniki = skladniki,
                Recenzje = recenzje,
                JuzOcenione = juzOcenione
            };

            return View(model);
        }

        //
        //Dodawanie ocen
        //

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajOcene(int PrzepisId, int Wartosc, string Tresc)
        {
            int userId = 1; // id usera na razie statuczne

            if (await _context.Ocena.AnyAsync(o => o.PrzepisId == PrzepisId && o.UzytkownikId == userId))
                return RedirectToAction("Details", new { id = PrzepisId });

            var ocena = new Ocena
            {
                PrzepisId = PrzepisId,
                UzytkownikId = userId,
                Wartosc = Wartosc
            };

            var recenzja = new Recenzja
            {
                PrzepisId = PrzepisId,
                UzytkownikId = userId,
                Tresc = Tresc,
                DataDodania = DateTime.Now
            };

            _context.Add(ocena);
            _context.Add(recenzja);
            await _context.SaveChangesAsync();

            // Przelicz średnią
            var oceny = await _context.Ocena.Where(o => o.PrzepisId == PrzepisId).ToListAsync();
            var przepis = await _context.Przepis.FindAsync(PrzepisId);
            przepis.SredniaOcena = oceny.Average(o => o.Wartosc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = PrzepisId });
        }
    }
}
