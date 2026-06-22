using CvecaraFlora.ApiKlijenti;
using CvecaraFlora.Modeli.Modeli;
using Microsoft.AspNetCore.Mvc;

namespace CvecaraFlora.Controllers
{
    [Controller]
    public class TipAranzmanaKontroler : Controller
    {
        private readonly TipoviAranzmanaApiKlijent tipoviAranzmanaApiKlijent;

        public TipAranzmanaKontroler()
        {
            tipoviAranzmanaApiKlijent = new TipoviAranzmanaApiKlijent();
        }

        public async Task<IActionResult> Index()
        {
            var tipovi = await tipoviAranzmanaApiKlijent.VratiSve();
            return View(tipovi);
        }

        [HttpGet]
        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj(string naziv)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                ViewBag.Greska = "Naziv tipa aranžmana je obavezan.";
                ViewBag.Naziv = naziv;
                return View();
            }

            var tipovi = await tipoviAranzmanaApiKlijent.VratiSve();

            var postoji = tipovi
                .Any(t => t.Naziv.ToLower() == naziv.ToLower());

            if (postoji)
            {
                ViewBag.Greska = "Tip aranžmana već postoji.";
                ViewBag.Naziv = naziv;
                return View();
            }

            await tipoviAranzmanaApiKlijent.Dodaj(naziv);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Izmeni(int id)
        {
            var tip = await tipoviAranzmanaApiKlijent.VratiPoId(id);
            return View(tip);
        }

        [HttpPost]
        public async Task<IActionResult> Izmeni(TipAranzmana tip)
        {
            if (string.IsNullOrWhiteSpace(tip.Naziv))
            {
                ViewBag.Greska = "Naziv tipa aranžmana je obavezan.";
                return View(tip);
            }

            var tipovi = await tipoviAranzmanaApiKlijent.VratiSve();

            var postoji = tipovi
                .Any(t => t.Naziv.ToLower() == tip.Naziv.ToLower()
                       && t.TipAranzmanaID != tip.TipAranzmanaID);

            if (postoji)
            {
                ViewBag.Greska = "Tip aranžmana već postoji.";
                return View(tip);
            }

            await tipoviAranzmanaApiKlijent.Izmeni(tip);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Obrisi(int id)
        {
            await tipoviAranzmanaApiKlijent.Obrisi(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EntityFrameworkPrikaz()
        {
            var tipovi = await tipoviAranzmanaApiKlijent.VratiSvePrekoEntityFramework();
            return View(tipovi);
        }
    }
}