namespace CvecaraFlora.Modeli.Modeli
{
    public class Narudzbina
    {
        public int NarudzbinaID { get; set; }

        public string? ImePrezimeKupca { get; set; }

        public string? BrojTelefona { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public DateTime DatumPreuzimanja { get; set; }

        public string? Status { get; set; }

        public decimal UkupnaCena { get; set; }

        public int TipAranzmanaID { get; set; }

        public string? NazivTipaAranzmana { get; set; }

        public List<TipAranzmana>? TipoviAranzmana { get; set; }

        public string? VrstaCveca { get; set; }

        public int Kolicina { get; set; }

        public string? DodatnaDekoracija { get; set; }

        public decimal CenaStavke { get; set; }
    }
}