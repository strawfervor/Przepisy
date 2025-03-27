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
    public class RecenzjaController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public RecenzjaController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: Recenzja
        public async Task<IActionResult> Index()
        {
            var przepisyIntranetContext = _context.Recenzja.Include(r => r.Przepis).Include(r => r.Uzytkownik);
            return View(await przepisyIntranetContext.ToListAsync());
        }

        // GET: Recenzja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzja = await _context.Recenzja
                .Include(r => r.Przepis)
                .Include(r => r.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recenzja == null)
            {
                return NotFound();
            }

            return View(recenzja);
        }

        // GET: Recenzja/Create
        public IActionResult Create()
        {
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania");
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo");
            return View();
        }

        // POST: Recenzja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tresc,DataDodania,UzytkownikId,PrzepisId")] Recenzja recenzja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", recenzja.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", recenzja.UzytkownikId);
            return View(recenzja);
        }

        // GET: Recenzja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzja = await _context.Recenzja.FindAsync(id);
            if (recenzja == null)
            {
                return NotFound();
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", recenzja.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", recenzja.UzytkownikId);
            return View(recenzja);
        }

        // POST: Recenzja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tresc,DataDodania,UzytkownikId,PrzepisId")] Recenzja recenzja)
        {
            if (id != recenzja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzjaExists(recenzja.Id))
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
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", recenzja.PrzepisId);
            ViewData["UzytkownikId"] = new SelectList(_context.Set<Uzytkownik>(), "Id", "Haslo", recenzja.UzytkownikId);
            return View(recenzja);
        }

        // GET: Recenzja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzja = await _context.Recenzja
                .Include(r => r.Przepis)
                .Include(r => r.Uzytkownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recenzja == null)
            {
                return NotFound();
            }

            return View(recenzja);
        }

        // POST: Recenzja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzja = await _context.Recenzja.FindAsync(id);
            if (recenzja != null)
            {
                _context.Recenzja.Remove(recenzja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzjaExists(int id)
        {
            return _context.Recenzja.Any(e => e.Id == id);
        }
    }
}
