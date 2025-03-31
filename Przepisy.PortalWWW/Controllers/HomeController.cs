using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data;
using Przepisy.PortalWWW.Models;

namespace Przepisy.PortalWWW.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //dodanie dostêpu do bazy danych:
    private readonly PrzepisyContext _context;

    public HomeController(ILogger<HomeController> logger, PrzepisyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task <IActionResult> Index(int? id)
    {
        ViewBag.ModelPrzepisy = await _context.Przepis.Where(t => t.CzyAktywny == true).OrderByDescending(p => p.IdPrzepisu).Take(3).ToListAsync();
        ViewBag.ModelStrony = (
            from strona in _context.Strona //dla ka¿dej strony z bazy danych kontektst stron
            orderby strona.Pozycja //posortowanej wzglêdem pozycji
            select strona //pobieramy strone
            ).ToList(); //zamieniamy na listê

        //przechwytujemy id, jeœli go nie ma to wtedy dajemy 1 jako id, nastêpnie szukamy itemu danym id i zwaracamy je jako view
        if (id == null)
        {
            id = 1;
        }

        var item = await _context.Strona.FindAsync(id);


        return View(item);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
