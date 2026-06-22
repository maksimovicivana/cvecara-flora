using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.PoslovnaLogika;
using Microsoft.AspNetCore.Mvc;

namespace CvecaraFlora.WebServis.Kontroleri
{
    [Controller]
    [ApiController]
    [Route("api/tipovi-aranzmana")]
    public class TipoviAranzmanaApiKontroler : ControllerBase
    {
        private readonly TipAranzmanaLogika tipAranzmanaLogika;

        public TipoviAranzmanaApiKontroler()
        {
            tipAranzmanaLogika = new TipAranzmanaLogika();
        }

        [HttpGet]
        public ActionResult<List<TipAranzmana>> VratiSve()
        {
            return tipAranzmanaLogika.VratiSve();
        }

        [HttpGet("{id}")]
        public ActionResult<TipAranzmana> VratiPoId(int id)
        {
            var tip = tipAranzmanaLogika.VratiPoId(id);

            if (tip == null)
            {
                return NotFound();
            }

            return tip;
        }

        [HttpPost]
        public IActionResult Dodaj(TipAranzmana tipAranzmana)
        {
            if (tipAranzmana == null || string.IsNullOrWhiteSpace(tipAranzmana.Naziv))
            {
                return BadRequest("Naziv tipa aranžmana je obavezan.");
            }

            tipAranzmanaLogika.Dodaj(tipAranzmana.Naziv);

            return Ok();
        }

        [HttpPut]
        public IActionResult Izmeni(TipAranzmana tipAranzmana)
        {
            if (tipAranzmana == null || string.IsNullOrWhiteSpace(tipAranzmana.Naziv))
            {
                return BadRequest("Naziv tipa aranžmana je obavezan.");
            }

            tipAranzmanaLogika.Izmeni(tipAranzmana);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Obrisi(int id)
        {
            tipAranzmanaLogika.Obrisi(id);

            return Ok();
        }

        [HttpGet("entity-framework")]
        public ActionResult<List<TipAranzmana>> VratiSvePrekoEntityFramework()
        {
            return tipAranzmanaLogika.VratiSvePrekoEntityFramework();
        }
    }
}