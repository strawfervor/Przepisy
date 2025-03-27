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
    public class SkladnikController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public SkladnikController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: Skladnik
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skladnik.ToListAsync());
        }

        // GET: Skladnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skladnik = await _context.Skladnik
                .FirstOrDefaultAsync(m => m.IdSkladnika == id);
            if (skladnik == null)
            {
                return NotFound();
            }

            return View(skladnik);
        }

        // GET: Skladnik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skladnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSkladnika,Nazwa,KalorycznoscNa100g,PrzelicznikNaSztuke,CzyAktywny,UrlZdjecia")] Skladnik skladnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skladnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skladnik);
        }

        // GET: Skladnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skladnik = await _context.Skladnik.FindAsync(id);
            if (skladnik == null)
            {
                return NotFound();
            }
            return View(skladnik);
        }

        // POST: Skladnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSkladnika,Nazwa,KalorycznoscNa100g,PrzelicznikNaSztuke,CzyAktywny,UrlZdjecia")] Skladnik skladnik)
        {
            if (id != skladnik.IdSkladnika)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skladnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkladnikExists(skladnik.IdSkladnika))
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
            return View(skladnik);
        }

        // GET: Skladnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skladnik = await _context.Skladnik
                .FirstOrDefaultAsync(m => m.IdSkladnika == id);
            if (skladnik == null)
            {
                return NotFound();
            }

            return View(skladnik);
        }

        // POST: Skladnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skladnik = await _context.Skladnik.FindAsync(id);
            if (skladnik != null)
            {
                _context.Skladnik.Remove(skladnik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkladnikExists(int id)
        {
            return _context.Skladnik.Any(e => e.IdSkladnika == id);
        }
    }
}
