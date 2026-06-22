using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.PoslovnaLogika;
using Microsoft.AspNetCore.Mvc;

namespace CvecaraFlora.WebServis.Kontroleri
{
    public class PrijavaZahtev
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
    }

    [Controller]
    [ApiController]
    [Route("api/korisnici")]
    public class KorisniciApiKontroler : ControllerBase
    {
        private readonly KorisnikLogika korisnikLogika;

        public KorisniciApiKontroler()
        {
            korisnikLogika = new KorisnikLogika();
        }

        [HttpPost("prijava")]
        public ActionResult<Korisnik> PrijaviKorisnika(PrijavaZahtev prijavaZahtev)
        {
            var korisnik = korisnikLogika.PrijaviKorisnika(
                prijavaZahtev.KorisnickoIme,
                prijavaZahtev.Lozinka
            );

            if (korisnik == null)
            {
                return Unauthorized("Pogrešno korisničko ime ili lozinka.");
            }

            return korisnik;
        }
    }
}