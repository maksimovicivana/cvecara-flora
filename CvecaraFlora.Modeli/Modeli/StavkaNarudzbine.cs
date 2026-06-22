namespace CvecaraFlora.Modeli.Modeli
{
    public class StavkaNarudzbine
    {
        public int StavkaNarudzbineID { get; set; }

        public int NarudzbinaID { get; set; }

        public string VrstaCveca { get; set; }

        public int Kolicina { get; set; }

        public string DodatnaDekoracija { get; set; }

        public decimal Cena { get; set; }
    }
}