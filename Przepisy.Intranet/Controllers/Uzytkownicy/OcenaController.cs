using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data.Uzytkownicy;
using Przepisy.Intranet.Data;

namespace Przepisy.Intranet.Controllers.Uzytkownicy
{
    public class OcenaController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public OcenaController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: Ocena
        public async Task<IActionResult> Index()
        {
            var przepisyIntranetContext = _context.Ocena.Include(o => o.Przepis).Include(o => o.Uzytkownik);
            return View(await przepisyIntranetContext.ToListAsync());
        }

        // GET: Ocena/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocena
                .Include(o => o.Przepis)
                .Include(o => o.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // GET: Ocena/Create
        public IActionResult Create()
        {
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania");
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo");
            return View();
        }

        // POST: Ocena/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Wartosc,UzytkownikId,PrzepisId")] Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", ocena.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", ocena.UzytkownikId);
            return View(ocena);
        }

        // GET: Ocena/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocena.FindAsync(id);
            if (ocena == null)
            {
                return NotFound();
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", ocena.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", ocena.UzytkownikId);
            return View(ocena);
        }

        // POST: Ocena/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Wartosc,UzytkownikId,PrzepisId")] Ocena ocena)
        {
            if (id != ocena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcenaExists(ocena.Id))
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
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", ocena.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", ocena.UzytkownikId);
            return View(ocena);
        }

        // GET: Ocena/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocena
                .Include(o => o.Przepis)
                .Include(o => o.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // POST: Ocena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocena = await _context.Ocena.FindAsync(id);
            if (ocena != null)
            {
                _context.Ocena.Remove(ocena);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcenaExists(int id)
        {
            return _context.Ocena.Any(e => e.Id == id);
        }
    }
}
