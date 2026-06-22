using CvecaraFlora.Modeli.Modeli;
using System.Net.Http.Json;

namespace CvecaraFlora.ApiKlijenti
{
    public class TipoviAranzmanaApiKlijent
    {
        private readonly HttpClient httpClient;

        public TipoviAranzmanaApiKlijent()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7258/")
            };
        }

        public async Task<List<TipAranzmana>> VratiSve()
        {
            var tipovi = await httpClient.GetFromJsonAsync<List<TipAranzmana>>("api/tipovi-aranzmana");

            return tipovi ?? new List<TipAranzmana>();
        }

        public async Task<TipAranzmana> VratiPoId(int id)
        {
            var tip = await httpClient.GetFromJsonAsync<TipAranzmana>($"api/tipovi-aranzmana/{id}");

            return tip;
        }

        public async Task Dodaj(string naziv)
        {
            TipAranzmana tip = new TipAranzmana
            {
                Naziv = naziv
            };

            await httpClient.PostAsJsonAsync("api/tipovi-aranzmana", tip);
        }

        public async Task Izmeni(TipAranzmana tip)
        {
            await httpClient.PutAsJsonAsync("api/tipovi-aranzmana", tip);
        }

        public async Task Obrisi(int id)
        {
            await httpClient.DeleteAsync($"api/tipovi-aranzmana/{id}");
        }

        public async Task<List<TipAranzmana>> VratiSvePrekoEntityFramework()
        {
            var tipovi = await httpClient.GetFromJsonAsync<List<TipAranzmana>>("api/tipovi-aranzmana/entity-framework");

            return tipovi ?? new List<TipAranzmana>();
        }
    }
}