using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.Repozitorijum.Repozitorijumi;

namespace CvecaraFlora.PoslovnaLogika
{
    public class StavkaNarudzbineLogika
    {
        private readonly StavkaNarudzbineRepozitorijum stavkaNarudzbineRepozitorijum;

        public StavkaNarudzbineLogika()
        {
            stavkaNarudzbineRepozitorijum = new StavkaNarudzbineRepozitorijum();
        }

        public List<StavkaNarudzbine> VratiPoNarudzbiniId(int narudzbinaId)
        {
            return stavkaNarudzbineRepozitorijum.VratiPoNarudzbiniId(narudzbinaId);
        }

        public void Dodaj(StavkaNarudzbine stavka)
        {
            stavkaNarudzbineRepozitorijum.Dodaj(stavka);
        }

        public void Izmeni(StavkaNarudzbine stavka)
        {
            stavkaNarudzbineRepozitorijum.Izmeni(stavka);
        }

        public void Obrisi(int id)
        {
            stavkaNarudzbineRepozitorijum.Obrisi(id);
        }
    }
}