using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Przepisy.Data.Data;
using Przepisy.Data.Data.Uzytkownicy;
using Przepisy.PortalWWW.Models;

namespace Przepisy.PortalWWW.Controllers
{
    public class ProfilController : Controller
    {
        private readonly PrzepisyContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfilController(PrzepisyContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 1; //testowe id usera, bez logowania

            var user = await _context.Uzytkownik
                .Include(u => u.Rola)
                .FirstOrDefaultAsync(u => u.Id == userId);

            ViewBag.ModelStrony = await _context.Strona
                .Where(s => s.Pozycja > 0)
                .OrderBy(s => s.Pozycja)
                .ToListAsync();

            if (user == null)
                return NotFound();

            return View(user);
        }
 
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            int userId = 1;
            var user = await _context.Uzytkownik.FindAsync(userId);
            if (user == null) return NotFound();

            var model = new ProfilEditViewModel
            {
                Id = user.Id,
                NazwaUzytkownika = user.NazwaUzytkownika,
                Opis = user.Opis,
                UrlAwataru = user.UrlAwataru
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfilEditViewModel model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var user = await _context.Uzytkownik.FindAsync(id);
            if (user == null) return NotFound();

            user.Opis = model.Opis;

            //awatar
            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "avatary");
                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.Avatar.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(stream);
                }

                user.UrlAwataru = "/images/avatary/" + fileName;
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



    }
}
