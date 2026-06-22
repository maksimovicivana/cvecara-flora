using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.PoslovnaLogika;
using Microsoft.AspNetCore.Mvc;

namespace CvecaraFlora.WebServis.Kontroleri
{
    [Controller]
    [ApiController]
    [Route("api/stavke-narudzbine")]
    public class StavkeNarudzbineApiKontroler : ControllerBase
    {
        private readonly StavkaNarudzbineLogika stavkaNarudzbineLogika;

        public StavkeNarudzbineApiKontroler()
        {
            stavkaNarudzbineLogika = new StavkaNarudzbineLogika();
        }

        [HttpGet("narudzbina/{narudzbinaId}")]
        public ActionResult<List<StavkaNarudzbine>> VratiPoNarudzbiniId(int narudzbinaId)
        {
            return stavkaNarudzbineLogika.VratiPoNarudzbiniId(narudzbinaId);
        }

        [HttpPost]
        public IActionResult Dodaj(StavkaNarudzbine stavka)
        {
            stavkaNarudzbineLogika.Dodaj(stavka);

            return Ok();
        }

        [HttpPut]
        public IActionResult Izmeni(StavkaNarudzbine stavka)
        {
            stavkaNarudzbineLogika.Izmeni(stavka);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Obrisi(int id)
        {
            stavkaNarudzbineLogika.Obrisi(id);

            return Ok();
        }
    }
}