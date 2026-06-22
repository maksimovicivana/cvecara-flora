using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.PoslovnaLogika;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CvecaraFlora.WebServis.Kontroleri
{
    [Controller]
    [ApiController]
    [Route("api/narudzbine")]
    public class NarudzbineApiKontroler : ControllerBase
    {
        private readonly NarudzbinaLogika narudzbinaLogika;

        public NarudzbineApiKontroler()
        {
            narudzbinaLogika = new NarudzbinaLogika();
        }

        [HttpGet]
        public ActionResult<List<Narudzbina>> VratiSve()
        {
            return narudzbinaLogika.VratiSve();
        }

        [HttpGet("{id}")]
        public ActionResult<Narudzbina> VratiPoId(int id)
        {
            var narudzbina = narudzbinaLogika.VratiPoId(id);

            if (narudzbina == null)
            {
                return NotFound();
            }

            return narudzbina;
        }

        [HttpGet("detalji/{id}")]
        public ActionResult<Narudzbina> VratiDetalje(int id)
        {
            var narudzbina = narudzbinaLogika.VratiDetalje(id);

            if (narudzbina == null)
            {
                return NotFound();
            }

            return narudzbina;
        }

        [HttpPost]
        public IActionResult Dodaj(Narudzbina narudzbina)
        {
            KriterijumNarudzbine kriterijum = VratiKriterijumIzJsonDokumenta();

            narudzbinaLogika.Dodaj(narudzbina, kriterijum);

            return Ok();
        }

        [HttpPut]
        public IActionResult Izmeni(Narudzbina narudzbina)
        {
            KriterijumNarudzbine kriterijum = VratiKriterijumIzJsonDokumenta();

            narudzbinaLogika.Izmeni(narudzbina, kriterijum);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Obrisi(int id)
        {
            narudzbinaLogika.Obrisi(id);

            return Ok();
        }

        private KriterijumNarudzbine VratiKriterijumIzJsonDokumenta()
        {
            string putanja = Path.Combine(Directory.GetCurrentDirectory(), "parametri.json");

            if (!System.IO.File.Exists(putanja))
            {
                return new KriterijumNarudzbine
                {
                    BrojDanaZaHitno = 0
                };
            }

            string json = System.IO.File.ReadAllText(putanja);

            JsonDocument dokument = JsonDocument.Parse(json);

            int brojDana = dokument
                .RootElement
                .GetProperty("KriterijumNarudzbine")
                .GetProperty("BrojDanaZaHitno")
                .GetInt32();

            return new KriterijumNarudzbine
            {
                BrojDanaZaHitno = brojDana
            };
        }
    }
}