using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data;
using Przepisy.Data.Data.Uzytkownicy;

namespace Przepisy.Intranet.Controllers.Uzytkownicy
{
    public class RolaController : Controller
    {
        private readonly PrzepisyContext _context;

        public RolaController(PrzepisyContext context)
        {
            _context = context;
        }

        // GET: Rola
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rola.ToListAsync());
        }

        // GET: Rola/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rola = await _context.Rola
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rola == null)
            {
                return NotFound();
            }

            return View(rola);
        }

        // GET: Rola/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rola/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,CenaAbonamentu")] Rola rola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rola);
        }

        // GET: Rola/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rola = await _context.Rola.FindAsync(id);
            if (rola == null)
            {
                return NotFound();
            }
            return View(rola);
        }

        // POST: Rola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,CenaAbonamentu")] Rola rola)
        {
            if (id != rola.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolaExists(rola.Id))
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
            return View(rola);
        }

        // GET: Rola/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rola = await _context.Rola
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rola == null)
            {
                return NotFound();
            }

            return View(rola);
        }

        // POST: Rola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rola = await _context.Rola.FindAsync(id);
            if (rola != null)
            {
                _context.Rola.Remove(rola);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolaExists(int id)
        {
            return _context.Rola.Any(e => e.Id == id);
        }
    }
}
