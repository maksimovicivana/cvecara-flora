using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.Repozitorijum.Repozitorijumi;

namespace CvecaraFlora.PoslovnaLogika
{
    public class KorisnikLogika
    {
        private readonly KorisnikRepozitorijum korisnikRepozitorijum;

        public KorisnikLogika()
        {
            korisnikRepozitorijum = new KorisnikRepozitorijum();
        }

        public Korisnik PrijaviKorisnika(string korisnickoIme, string lozinka)
        {
            return korisnikRepozitorijum.PrijaviKorisnika(korisnickoIme, lozinka);
        }
    }
}