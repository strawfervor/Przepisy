using Microsoft.EntityFrameworkCore;
using Przepisy.Data.Data.CMS;
using Przepisy.Data.Data.Przepisy;
using Przepisy.Data.Data.Uzytkownicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy.Data.Data
{
    public class PrzepisyContext : DbContext
    {
        public PrzepisyContext(DbContextOptions<PrzepisyContext> options)
            : base(options)
        {
        }

        public DbSet<Strona> Strona { get; set; } = default!;
        public DbSet<Aktualnosc> Aktualnosc { get; set; } = default!;
        public DbSet<GrupaPrzepisu> GrupaPrzepisu { get; set; } = default!;
        public DbSet<Kuchnia> Kuchnia { get; set; } = default!;
        public DbSet<Przepis> Przepis { get; set; } = default!;
        public DbSet<PrzepisSkladnik> PrzepisSkladnik { get; set; } = default!;
        public DbSet<Skladnik> Skladnik { get; set; } = default!;
        public DbSet<Ocena> Ocena { get; set; } = default!;
        public DbSet<Recenzja> Recenzja { get; set; } = default!;
        public DbSet<Rola> Rola { get; set; } = default!;
        public DbSet<UlubionyPrzepis> UlubionyPrzepis { get; set; } = default!;
        public DbSet<Uzytkownik> Uzytkownik { get; set; } = default!;
    }
}
