using CvecaraFlora.Modeli.Modeli;
using System.Net.Http.Json;

namespace CvecaraFlora.ApiKlijenti
{
    public class KorisniciApiKlijent
    {
        private readonly HttpClient httpClient;

        public KorisniciApiKlijent()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7258/")
            };
        }

        public async Task<Korisnik> PrijaviKorisnika(string korisnickoIme, string lozinka)
        {
            Korisnik korisnikZaPrijavu = new Korisnik
            {
                KorisnickoIme = korisnickoIme,
                Lozinka = lozinka
            };

            var odgovor = await httpClient.PostAsJsonAsync("api/korisnici/prijava", korisnikZaPrijavu);

            if (!odgovor.IsSuccessStatusCode)
            {
                return null;
            }

            var korisnik = await odgovor.Content.ReadFromJsonAsync<Korisnik>();

            return korisnik;
        }
    }
}