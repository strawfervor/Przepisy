using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data.CMS;
using Przepisy.Data.Data.Przepisy;
using Przepisy.Data.Data.Uzytkownicy;

namespace Przepisy.Intranet.Data
{
    public class PrzepisyIntranetContext : DbContext
    {
        public PrzepisyIntranetContext (DbContextOptions<PrzepisyIntranetContext> options)
            : base(options)
        {
        }

        public DbSet<Przepisy.Data.Data.CMS.Strona> Strona { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.CMS.Aktualnosc> Aktualnosc { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Przepisy.GrupaPrzepisu> GrupaPrzepisu { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Przepisy.Kuchnia> Kuchnia { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Przepisy.Przepis> Przepis { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Przepisy.PrzepisSkladnik> PrzepisSkladnik { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Przepisy.Skladnik> Skladnik { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Uzytkownicy.Ocena> Ocena { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Uzytkownicy.Recenzja> Recenzja { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Uzytkownicy.Rola> Rola { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Uzytkownicy.UlubionyPrzepis> UlubionyPrzepis { get; set; } = default!;
        public DbSet<Przepisy.Data.Data.Uzytkownicy.Uzytkownik> Uzytkownik { get; set; } = default!;
    }
}
