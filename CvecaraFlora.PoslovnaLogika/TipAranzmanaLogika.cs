using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.Repozitorijum.Repozitorijumi;

namespace CvecaraFlora.PoslovnaLogika
{
    public class TipAranzmanaLogika
    {
        private readonly TipAranzmanaRepozitorijum tipAranzmanaRepozitorijum;

        public TipAranzmanaLogika()
        {
            tipAranzmanaRepozitorijum = new TipAranzmanaRepozitorijum();
        }

        public List<TipAranzmana> VratiSve()
        {
            return tipAranzmanaRepozitorijum.VratiSve();
        }

        public TipAranzmana VratiPoId(int id)
        {
            return tipAranzmanaRepozitorijum.VratiPoId(id);
        }

        public void Dodaj(string naziv)
        {
            tipAranzmanaRepozitorijum.Dodaj(naziv);
        }

        public void Izmeni(TipAranzmana tipAranzmana)
        {
            tipAranzmanaRepozitorijum.Izmeni(tipAranzmana);
        }

        public void Obrisi(int id)
        {
            tipAranzmanaRepozitorijum.Obrisi(id);
        }

        public List<TipAranzmana> VratiSvePrekoEntityFramework()
        {
            return tipAranzmanaRepozitorijum.VratiSvePrekoEntityFramework();
        }
    }
}