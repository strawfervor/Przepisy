using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data.Przepisy;
using Przepisy.Intranet.Data;

namespace Przepisy.Intranet.Controllers.Przepisy
{
    public class PrzepisSkladnikController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public PrzepisSkladnikController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: PrzepisSkladnik
        public async Task<IActionResult> Index()
        {
            var przepisyIntranetContext = _context.PrzepisSkladnik.Include(p => p.Przepis).Include(p => p.Skladnik);
            return View(await przepisyIntranetContext.ToListAsync());
        }

        // GET: PrzepisSkladnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepisSkladnik = await _context.PrzepisSkladnik
                .Include(p => p.Przepis)
                .Include(p => p.Skladnik)
                .FirstOrDefaultAsync(m => m.IdPrzepisSkladnik == id);
            if (przepisSkladnik == null)
            {
                return NotFound();
            }

            return View(przepisSkladnik);
        }

        // GET: PrzepisSkladnik/Create
        public IActionResult Create()
        {
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania");
            ViewData["SkladnikId"] = new SelectList(_context.Set<Skladnik>(), "IdSkladnika", "Nazwa");
            return View();
        }

        // POST: PrzepisSkladnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrzepisSkladnik,PrzepisId,SkladnikId,IloscGram")] PrzepisSkladnik przepisSkladnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przepisSkladnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", przepisSkladnik.PrzepisId);
            ViewData["SkladnikId"] = new SelectList(_context.Set<Skladnik>(), "IdSkladnika", "Nazwa", przepisSkladnik.SkladnikId);
            return View(przepisSkladnik);
        }

        // GET: PrzepisSkladnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepisSkladnik = await _context.PrzepisSkladnik.FindAsync(id);
            if (przepisSkladnik == null)
            {
                return NotFound();
            }
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", przepisSkladnik.PrzepisId);
            ViewData["SkladnikId"] = new SelectList(_context.Set<Skladnik>(), "IdSkladnika", "Nazwa", przepisSkladnik.SkladnikId);
            return View(przepisSkladnik);
        }

        // POST: PrzepisSkladnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPrzepisSkladnik,PrzepisId,SkladnikId,IloscGram")] PrzepisSkladnik przepisSkladnik)
        {
            if (id != przepisSkladnik.IdPrzepisSkladnik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przepisSkladnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzepisSkladnikExists(przepisSkladnik.IdPrzepisSkladnik))
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
            ViewData["PrzepisId"] = new SelectList(_context.Przepis, "IdPrzepisu", "OpisWykonania", przepisSkladnik.PrzepisId);
            ViewData["SkladnikId"] = new SelectList(_context.Set<Skladnik>(), "IdSkladnika", "Nazwa", przepisSkladnik.SkladnikId);
            return View(przepisSkladnik);
        }

        // GET: PrzepisSkladnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przepisSkladnik = await _context.PrzepisSkladnik
                .Include(p => p.Przepis)
                .Include(p => p.Skladnik)
                .FirstOrDefaultAsync(m => m.IdPrzepisSkladnik == id);
            if (przepisSkladnik == null)
            {
                return NotFound();
            }

            return View(przepisSkladnik);
        }

        // POST: PrzepisSkladnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przepisSkladnik = await _context.PrzepisSkladnik.FindAsync(id);
            if (przepisSkladnik != null)
            {
                _context.PrzepisSkladnik.Remove(przepisSkladnik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzepisSkladnikExists(int id)
        {
            return _context.PrzepisSkladnik.Any(e => e.IdPrzepisSkladnik == id);
        }
    }
}
