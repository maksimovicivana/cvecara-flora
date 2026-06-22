using CvecaraFlora.ApiKlijenti;
using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.ViewModeli;
using Microsoft.AspNetCore.Mvc;

namespace CvecaraFlora.Controllers
{
    [Controller]
    public class NarudzbinaKontroler : Controller
    {
        private readonly NarudzbineApiKlijent narudzbineApiKlijent;
        private readonly TipoviAranzmanaApiKlijent tipoviAranzmanaApiKlijent;

        public NarudzbinaKontroler()
        {
            narudzbineApiKlijent = new NarudzbineApiKlijent();
            tipoviAranzmanaApiKlijent = new TipoviAranzmanaApiKlijent();
        }

        public async Task<IActionResult> Index(string pretraga)
        {
            var narudzbine = await narudzbineApiKlijent.VratiSve();

            if (!string.IsNullOrWhiteSpace(pretraga))
            {
                narudzbine = narudzbine
                    .Where(n =>
                        n.ImePrezimeKupca.Equals(pretraga, StringComparison.OrdinalIgnoreCase)
                        ||
                        n.ImePrezimeKupca.Split(' ')
                            .Any(deo => deo.Equals(pretraga, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }

            ViewBag.Pretraga = pretraga;

            return View(narudzbine);
        }

        public async Task<IActionResult> Detalji(int id)
        {
            var narudzbina = await narudzbineApiKlijent.VratiDetalje(id);

            if (narudzbina == null)
            {
                return RedirectToAction("Index");
            }

            return View(narudzbina);
        }

        public async Task<IActionResult> StampajSve()
        {
            var narudzbine = await narudzbineApiKlijent.VratiSve();
            return View(narudzbine);
        }

        public async Task<IActionResult> StampajFiltrirano(string pretraga)
        {
            var narudzbine = await narudzbineApiKlijent.VratiSve();

            if (!string.IsNullOrWhiteSpace(pretraga))
            {
                narudzbine = narudzbine
                    .Where(n =>
                        n.ImePrezimeKupca.Equals(pretraga, StringComparison.OrdinalIgnoreCase)
                        ||
                        n.ImePrezimeKupca.Split(' ')
                            .Any(deo => deo.Equals(pretraga, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }

            ViewBag.Pretraga = pretraga;

            return View(narudzbine);
        }

        public async Task<IActionResult> Potvrda(int id)
        {
            var narudzbina = await narudzbineApiKlijent.VratiDetalje(id);

            if (narudzbina == null)
            {
                return RedirectToAction("Index");
            }

            return View(narudzbina);
        }

        [HttpGet]
        public async Task<IActionResult> Dodaj()
        {
            NarudzbinaViewModel model = new NarudzbinaViewModel
            {
                Narudzbina = new Narudzbina
                {
                    DatumKreiranja = DateTime.Today,
                    DatumPreuzimanja = DateTime.Today
                },
                TipoviAranzmana = await tipoviAranzmanaApiKlijent.VratiSve()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj(NarudzbinaViewModel model)
        {
            var narudzbina = model.Narudzbina;

            if (narudzbina.TipAranzmanaID == 0)
            {
                ViewBag.Greska = "Morate izabrati tip aranžmana.";
                model.TipoviAranzmana = await tipoviAranzmanaApiKlijent.VratiSve();
                return View(model);
            }

            if (narudzbina.DatumPreuzimanja.Date < DateTime.Today)
            {
                ViewBag.Greska = "Datum preuzimanja ne može biti u prošlosti.";
                model.TipoviAranzmana = await tipoviAranzmanaApiKlijent.VratiSve();
                return View(model);
            }

            await narudzbineApiKlijent.Dodaj(narudzbina);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Izmeni(int id)
        {
            var narudzbina = await narudzbineApiKlijent.VratiPoId(id);

            if (narudzbina == null)
            {
                return RedirectToAction("Index");
            }

            narudzbina.TipoviAranzmana = await tipoviAranzmanaApiKlijent.VratiSve();

            return View(narudzbina);
        }

        [HttpPost]
        public async Task<IActionResult> Izmeni(Narudzbina narudzbina)
        {
            if (narudzbina.DatumPreuzimanja.Date < DateTime.Today)
            {
                ViewBag.Greska = "Datum preuzimanja ne može biti u prošlosti.";
                narudzbina.TipoviAranzmana = await tipoviAranzmanaApiKlijent.VratiSve();
                return View(narudzbina);
            }

            await narudzbineApiKlijent.Izmeni(narudzbina);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Obrisi(int id)
        {
            await narudzbineApiKlijent.Obrisi(id);
            return RedirectToAction("Index");
        }
    }
}