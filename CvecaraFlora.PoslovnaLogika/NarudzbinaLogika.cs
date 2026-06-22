using CvecaraFlora.Modeli.Modeli;
using CvecaraFlora.Repozitorijum.Repozitorijumi;

namespace CvecaraFlora.PoslovnaLogika
{
    public class NarudzbinaLogika
    {
        private readonly NarudzbinaRepozitorijum narudzbinaRepozitorijum;
        private readonly ObradaNarudzbine obradaNarudzbine;

        public NarudzbinaLogika()
        {
            narudzbinaRepozitorijum = new NarudzbinaRepozitorijum();
            obradaNarudzbine = new ObradaNarudzbine();
        }

        public List<Narudzbina> VratiSve()
        {
            return narudzbinaRepozitorijum.VratiSve();
        }

        public Narudzbina VratiPoId(int id)
        {
            return narudzbinaRepozitorijum.VratiPoId(id);
        }

        public Narudzbina VratiDetalje(int id)
        {
            return narudzbinaRepozitorijum.VratiDetalje(id);
        }

        public void Dodaj(Narudzbina narudzbina, KriterijumNarudzbine kriterijum)
        {
            narudzbina.DatumKreiranja = DateTime.Today;

            obradaNarudzbine.OdrediStatus(narudzbina, kriterijum);

            narudzbinaRepozitorijum.Dodaj(narudzbina);
        }

        public void Izmeni(Narudzbina narudzbina, KriterijumNarudzbine kriterijum)
        {
            narudzbina.DatumKreiranja = DateTime.Today;

            obradaNarudzbine.OdrediStatus(narudzbina, kriterijum);

            narudzbinaRepozitorijum.Izmeni(narudzbina);
        }

        public void Obrisi(int id)
        {
            narudzbinaRepozitorijum.Obrisi(id);
        }
    }
}