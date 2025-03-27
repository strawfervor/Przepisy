using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przepisy.Data.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aktualnosc",
                columns: table => new
                {
                    IdAktualnosci = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTytul = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tytul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Pozycja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktualnosc", x => x.IdAktualnosci);
                });

            migrationBuilder.CreateTable(
                name: "GrupaPrzepisu",
                columns: table => new
                {
                    IdGrupyPrzepisu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupaPrzepisu", x => x.IdGrupyPrzepisu);
                });

            migrationBuilder.CreateTable(
                name: "Kuchnia",
                columns: table => new
                {
                    IdKuchni = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuchnia", x => x.IdKuchni);
                });

            migrationBuilder.CreateTable(
                name: "Rola",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CenaAbonamentu = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rola", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skladnik",
                columns: table => new
                {
                    IdSkladnika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    KalorycznoscNa100g = table.Column<double>(type: "float", nullable: false),
                    PrzelicznikNaSztuke = table.Column<double>(type: "float", nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    UrlZdjecia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skladnik", x => x.IdSkladnika);
                });

            migrationBuilder.CreateTable(
                name: "Strona",
                columns: table => new
                {
                    IdStrony = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTytul = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tytul = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Pozycja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strona", x => x.IdStrony);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaUzytkownika = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UrlAwataru = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uzytkownik_Rola_RolaId",
                        column: x => x.RolaId,
                        principalTable: "Rola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Przepis",
                columns: table => new
                {
                    IdPrzepisu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KrotkiOpis = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OpisWykonania = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Trudnosc = table.Column<int>(type: "int", nullable: false),
                    UrlZdjecia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CzyAktywny = table.Column<bool>(type: "bit", nullable: false),
                    SredniaOcena = table.Column<double>(type: "float", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    KuchniaId = table.Column<int>(type: "int", nullable: false),
                    GrupaPrzepisuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przepis", x => x.IdPrzepisu);
                    table.ForeignKey(
                        name: "FK_Przepis_GrupaPrzepisu_GrupaPrzepisuId",
                        column: x => x.GrupaPrzepisuId,
                        principalTable: "GrupaPrzepisu",
                        principalColumn: "IdGrupyPrzepisu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przepis_Kuchnia_KuchniaId",
                        column: x => x.KuchniaId,
                        principalTable: "Kuchnia",
                        principalColumn: "IdKuchni",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przepis_Uzytkownik_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Uzytkownik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wartosc = table.Column<int>(type: "int", nullable: false),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    PrzepisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocena_Przepis_PrzepisId",
                        column: x => x.PrzepisId,
                        principalTable: "Przepis",
                        principalColumn: "IdPrzepisu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocena_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrzepisSkladnik",
                columns: table => new
                {
                    IdPrzepisSkladnik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrzepisId = table.Column<int>(type: "int", nullable: false),
                    SkladnikId = table.Column<int>(type: "int", nullable: false),
                    IloscGram = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzepisSkladnik", x => x.IdPrzepisSkladnik);
                    table.ForeignKey(
                        name: "FK_PrzepisSkladnik_Przepis_PrzepisId",
                        column: x => x.PrzepisId,
                        principalTable: "Przepis",
                        principalColumn: "IdPrzepisu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrzepisSkladnik_Skladnik_SkladnikId",
                        column: x => x.SkladnikId,
                        principalTable: "Skladnik",
                        principalColumn: "IdSkladnika",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tresc = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    PrzepisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzja_Przepis_PrzepisId",
                        column: x => x.PrzepisId,
                        principalTable: "Przepis",
                        principalColumn: "IdPrzepisu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzja_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UlubionyPrzepis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzytkownikId = table.Column<int>(type: "int", nullable: false),
                    PrzepisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlubionyPrzepis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UlubionyPrzepis_Przepis_PrzepisId",
                        column: x => x.PrzepisId,
                        principalTable: "Przepis",
                        principalColumn: "IdPrzepisu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UlubionyPrzepis_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_PrzepisId",
                table: "Ocena",
                column: "PrzepisId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_UzytkownikId",
                table: "Ocena",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Przepis_AutorId",
                table: "Przepis",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Przepis_GrupaPrzepisuId",
                table: "Przepis",
                column: "GrupaPrzepisuId");

            migrationBuilder.CreateIndex(
                name: "IX_Przepis_KuchniaId",
                table: "Przepis",
                column: "KuchniaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzepisSkladnik_PrzepisId",
                table: "PrzepisSkladnik",
                column: "PrzepisId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzepisSkladnik_SkladnikId",
                table: "PrzepisSkladnik",
                column: "SkladnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzja_PrzepisId",
                table: "Recenzja",
                column: "PrzepisId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzja_UzytkownikId",
                table: "Recenzja",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_UlubionyPrzepis_PrzepisId",
                table: "UlubionyPrzepis",
                column: "PrzepisId");

            migrationBuilder.CreateIndex(
                name: "IX_UlubionyPrzepis_UzytkownikId",
                table: "UlubionyPrzepis",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownik_RolaId",
                table: "Uzytkownik",
                column: "RolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aktualnosc");

            migrationBuilder.DropTable(
                name: "Ocena");

            migrationBuilder.DropTable(
                name: "PrzepisSkladnik");

            migrationBuilder.DropTable(
                name: "Recenzja");

            migrationBuilder.DropTable(
                name: "Strona");

            migrationBuilder.DropTable(
                name: "UlubionyPrzepis");

            migrationBuilder.DropTable(
                name: "Skladnik");

            migrationBuilder.DropTable(
                name: "Przepis");

            migrationBuilder.DropTable(
                name: "GrupaPrzepisu");

            migrationBuilder.DropTable(
                name: "Kuchnia");

            migrationBuilder.DropTable(
                name: "Uzytkownik");

            migrationBuilder.DropTable(
                name: "Rola");
        }
    }
}
