using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data.Przepisy;
using Przepisy.Data.Data.Uzytkownicy;
using Przepisy.Intranet.Data;

namespace Przepisy.Intranet.Controllers.Przepisy
{
    public class PrzepisController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public PrzepisController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: Przepis
        public async Task<IActionResult> Index()
        {
            var przepisyIntranetContext = _context.Przepis.Include(p => p.Autor).Include(p => p.GrupaPrzepisu).Include(p => p.Kuchnia);
            return View(await przepisyIntranetContext.ToListAsync());
        }

        // GET: Przepis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepis
                .Include(p => p.Autor)
                .Include(p => p.GrupaPrzepisu)
                .Include(p => p.Kuchnia)
                .FirstOrDefaultAsync(m => m.IdPrzepisu == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // GET: Przepis/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo");
            ViewData["GrupaPrzepisuId"] = new SelectList(_context.GrupaPrzepisu, "IdGrupyPrzepisu", "Nazwa");
            ViewData["KuchniaId"] = new SelectList(_context.Kuchnia, "IdKuchni", "Nazwa");
            return View();
        }

        // POST: Przepis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrzepisu,Tytul,KrotkiOpis,OpisWykonania,Trudnosc,UrlZdjecia,CzyAktywny,SredniaOcena,AutorId,KuchniaId,GrupaPrzepisuId")] Przepis przepis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przepis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", przepis.AutorId);
            ViewData["GrupaPrzepisuId"] = new SelectList(_context.GrupaPrzepisu, "IdGrupyPrzepisu", "Nazwa", przepis.GrupaPrzepisuId);
            ViewData["KuchniaId"] = new SelectList(_context.Kuchnia, "IdKuchni", "Nazwa", przepis.KuchniaId);
            return View(przepis);
        }

        // GET: Przepis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepis.FindAsync(id);
            if (przepis == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", przepis.AutorId);
            ViewData["GrupaPrzepisuId"] = new SelectList(_context.GrupaPrzepisu, "IdGrupyPrzepisu", "Nazwa", przepis.GrupaPrzepisuId);
            ViewData["KuchniaId"] = new SelectList(_context.Kuchnia, "IdKuchni", "Nazwa", przepis.KuchniaId);
            return View(przepis);
        }

        // POST: Przepis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrzepisu,Tytul,KrotkiOpis,OpisWykonania,Trudnosc,UrlZdjecia,CzyAktywny,SredniaOcena,AutorId,KuchniaId,GrupaPrzepisuId")] Przepis przepis)
        {
            if (id != przepis.IdPrzepisu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przepis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzepisExists(przepis.IdPrzepisu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", przepis.AutorId);
            ViewData["GrupaPrzepisuId"] = new SelectList(_context.GrupaPrzepisu, "IdGrupyPrzepisu", "Nazwa", przepis.GrupaPrzepisuId);
            ViewData["KuchniaId"] = new SelectList(_context.Kuchnia, "IdKuchni", "Nazwa", przepis.KuchniaId);
            return View(przepis);
        }

        // GET: Przepis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepis = await _context.Przepis
                .Include(p => p.Autor)
                .Include(p => p.GrupaPrzepisu)
                .Include(p => p.Kuchnia)
                .FirstOrDefaultAsync(m => m.IdPrzepisu == id);
            if (przepis == null)
            {
                return NotFound();
            }

            return View(przepis);
        }

        // POST: Przepis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przepis = await _context.Przepis.FindAsync(id);
            if (przepis != null)
            {
                _context.Przepis.Remove(przepis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzepisExists(int id)
        {
            return _context.Przepis.Any(e => e.IdPrzepisu == id);
        }
    }
}
