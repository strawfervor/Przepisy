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
    public class UlubionyPrzepisController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public UlubionyPrzepisController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: UlubionyPrzepis
        public async Task<IActionResult> Index()
        {
            var przepisyIntranetContext = _context.UlubionyPrzepis.Include(u => u.Przepis).Include(u => u.Uzytkownik);
            return View(await przepisyIntranetContext.ToListAsync());
        }

        // GET: UlubionyPrzepis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ulubionyPrzepis = await _context.UlubionyPrzepis
                .Include(u => u.Przepis)
                .Include(u => u.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ulubionyPrzepis == null)
            {
                return NotFound();
            }

            return View(ulubionyPrzepis);
        }

        // GET: UlubionyPrzepis/Create
        public IActionResult Create()
        {
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania");
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo");
            return View();
        }

        // POST: UlubionyPrzepis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UzytkownikId,PrzepisId")] UlubionyPrzepis ulubionyPrzepis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ulubionyPrzepis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", ulubionyPrzepis.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", ulubionyPrzepis.UzytkownikId);
            return View(ulubionyPrzepis);
        }

        // GET: UlubionyPrzepis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ulubionyPrzepis = await _context.UlubionyPrzepis.FindAsync(id);
            if (ulubionyPrzepis == null)
            {
                return NotFound();
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", ulubionyPrzepis.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", ulubionyPrzepis.UzytkownikId);
            return View(ulubionyPrzepis);
        }

        // POST: UlubionyPrzepis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UzytkownikId,PrzepisId")] UlubionyPrzepis ulubionyPrzepis)
        {
            if (id != ulubionyPrzepis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ulubionyPrzepis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UlubionyPrzepisExists(ulubionyPrzepis.Id))
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
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", ulubionyPrzepis.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", ulubionyPrzepis.UzytkownikId);
            return View(ulubionyPrzepis);
        }

        // GET: UlubionyPrzepis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ulubionyPrzepis = await _context.UlubionyPrzepis
                .Include(u => u.Przepis)
                .Include(u => u.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ulubionyPrzepis == null)
            {
                return NotFound();
            }

            return View(ulubionyPrzepis);
        }

        // POST: UlubionyPrzepis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ulubionyPrzepis = await _context.UlubionyPrzepis.FindAsync(id);
            if (ulubionyPrzepis != null)
            {
                _context.UlubionyPrzepis.Remove(ulubionyPrzepis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UlubionyPrzepisExists(int id)
        {
            return _context.UlubionyPrzepis.Any(e => e.Id == id);
        }
    }
}
