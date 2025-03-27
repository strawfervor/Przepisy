using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data;
using Przepisy.Data.Data.Przepisy;

namespace Przepisy.Intranet.Controllers.Przepisy
{
    public class KuchniaController : Controller
    {
        private readonly PrzepisyContext _context;

        public KuchniaController(PrzepisyContext context)
        {
            _context = context;
        }

        // GET: Kuchnia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kuchnia.ToListAsync());
        }

        // GET: Kuchnia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kuchnia = await _context.Kuchnia
                .FirstOrDefaultAsync(m => m.IdKuchni == id);
            if (kuchnia == null)
            {
                return NotFound();
            }

            return View(kuchnia);
        }

        // GET: Kuchnia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kuchnia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKuchni,Nazwa")] Kuchnia kuchnia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kuchnia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kuchnia);
        }

        // GET: Kuchnia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kuchnia = await _context.Kuchnia.FindAsync(id);
            if (kuchnia == null)
            {
                return NotFound();
            }
            return View(kuchnia);
        }

        // POST: Kuchnia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKuchni,Nazwa")] Kuchnia kuchnia)
        {
            if (id != kuchnia.IdKuchni)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kuchnia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KuchniaExists(kuchnia.IdKuchni))
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
            return View(kuchnia);
        }

        // GET: Kuchnia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kuchnia = await _context.Kuchnia
                .FirstOrDefaultAsync(m => m.IdKuchni == id);
            if (kuchnia == null)
            {
                return NotFound();
            }

            return View(kuchnia);
        }

        // POST: Kuchnia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kuchnia = await _context.Kuchnia.FindAsync(id);
            if (kuchnia != null)
            {
                _context.Kuchnia.Remove(kuchnia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KuchniaExists(int id)
        {
            return _context.Kuchnia.Any(e => e.IdKuchni == id);
        }
    }
}
