using CvecaraFlora.Modeli.Modeli;

namespace CvecaraFlora.PoslovnaLogika
{
    public class ObradaNarudzbine
    {
        public void OdrediStatus(Narudzbina narudzbina, KriterijumNarudzbine kriterijum)
        {
            DateTime granicaZaHitno = narudzbina.DatumKreiranja.Date.AddDays(kriterijum.BrojDanaZaHitno);

            if (narudzbina.DatumPreuzimanja.Date <= granicaZaHitno)
            {
                narudzbina.Status = "Hitno";
            }
            else
            {
                narudzbina.Status = "Standardno";
            }
        }
    }
}