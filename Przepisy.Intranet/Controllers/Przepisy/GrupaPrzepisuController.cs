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
    public class GrupaPrzepisuController : Controller
    {
        private readonly PrzepisyIntranetContext _context;

        public GrupaPrzepisuController(PrzepisyIntranetContext context)
        {
            _context = context;
        }

        // GET: GrupaPrzepisu
        public async Task<IActionResult> Index()
        {
            return View(await _context.GrupaPrzepisu.ToListAsync());
        }

        // GET: GrupaPrzepisu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupaPrzepisu = await _context.GrupaPrzepisu
                .FirstOrDefaultAsync(m => m.IdGrupyPrzepisu == id);
            if (grupaPrzepisu == null)
            {
                return NotFound();
            }

            return View(grupaPrzepisu);
        }

        // GET: GrupaPrzepisu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrupaPrzepisu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGrupyPrzepisu,Nazwa")] GrupaPrzepisu grupaPrzepisu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupaPrzepisu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupaPrzepisu);
        }

        // GET: GrupaPrzepisu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupaPrzepisu = await _context.GrupaPrzepisu.FindAsync(id);
            if (grupaPrzepisu == null)
            {
                return NotFound();
            }
            return View(grupaPrzepisu);
        }

        // POST: GrupaPrzepisu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGrupyPrzepisu,Nazwa")] GrupaPrzepisu grupaPrzepisu)
        {
            if (id != grupaPrzepisu.IdGrupyPrzepisu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupaPrzepisu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupaPrzepisuExists(grupaPrzepisu.IdGrupyPrzepisu))
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
            return View(grupaPrzepisu);
        }

        // GET: GrupaPrzepisu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupaPrzepisu = await _context.GrupaPrzepisu
                .FirstOrDefaultAsync(m => m.IdGrupyPrzepisu == id);
            if (grupaPrzepisu == null)
            {
                return NotFound();
            }

            return View(grupaPrzepisu);
        }

        // POST: GrupaPrzepisu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupaPrzepisu = await _context.GrupaPrzepisu.FindAsync(id);
            if (grupaPrzepisu != null)
            {
                _context.GrupaPrzepisu.Remove(grupaPrzepisu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupaPrzepisuExists(int id)
        {
            return _context.GrupaPrzepisu.Any(e => e.IdGrupyPrzepisu == id);
        }
    }
}
