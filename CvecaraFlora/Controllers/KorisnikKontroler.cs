using CvecaraFlora.ApiKlijenti;
using Microsoft.AspNetCore.Mvc;

namespace CvecaraFlora.Controllers
{
    public class KorisnikKontroler : Controller
    {
        private readonly KorisniciApiKlijent korisniciApiKlijent;

        public KorisnikKontroler()
        {
            korisniciApiKlijent = new KorisniciApiKlijent();
        }

        [HttpGet]
        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Prijava(string korisnickoIme, string lozinka)
        {
            var korisnik = await korisniciApiKlijent.PrijaviKorisnika(korisnickoIme, lozinka);

            if (korisnik == null)
            {
                ViewBag.Greska = "Pogrešno korisničko ime ili lozinka.";
                return View();
            }

            HttpContext.Session.SetString("KorisnickoIme", korisnik.KorisnickoIme);
            HttpContext.Session.SetString("Uloga", korisnik.Uloga);

            return RedirectToAction("Index", "NarudzbinaKontroler");
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Prijava");
        }
    }
}