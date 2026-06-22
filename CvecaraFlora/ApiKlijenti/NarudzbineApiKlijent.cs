using CvecaraFlora.Modeli.Modeli;
using System.Net.Http.Json;

namespace CvecaraFlora.ApiKlijenti
{
    public class NarudzbineApiKlijent
    {
        private readonly HttpClient httpClient;

        public NarudzbineApiKlijent()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7258/")
            };
        }

        public async Task<List<Narudzbina>> VratiSve()
        {
            var narudzbine = await httpClient.GetFromJsonAsync<List<Narudzbina>>("api/narudzbine");

            return narudzbine ?? new List<Narudzbina>();
        }

        public async Task<Narudzbina?> VratiPoId(int id)
        {
            return await httpClient.GetFromJsonAsync<Narudzbina>($"api/narudzbine/{id}");
        }

        public async Task<Narudzbina?> VratiDetalje(int id)
        {
            return await httpClient.GetFromJsonAsync<Narudzbina>($"api/narudzbine/detalji/{id}");
        }

        public async Task Dodaj(Narudzbina narudzbina)
        {
            await httpClient.PostAsJsonAsync("api/narudzbine", narudzbina);
        }

        public async Task Izmeni(Narudzbina narudzbina)
        {
            await httpClient.PutAsJsonAsync("api/narudzbine", narudzbina);
        }

        public async Task Obrisi(int id)
        {
            await httpClient.DeleteAsync($"api/narudzbine/{id}");
        }
    }
}