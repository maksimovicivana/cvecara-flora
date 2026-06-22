using CvecaraFlora.Modeli.Modeli;
using Microsoft.Data.SqlClient;

namespace CvecaraFlora.Repozitorijum.Repozitorijumi
{
    public class KorisnikRepozitorijum
    {
        public Korisnik PrijaviKorisnika(string korisnickoIme, string lozinka)
        {
            Korisnik korisnik = null;

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"SELECT KorisnikID, KorisnickoIme, Lozinka, Uloga
                                FROM Korisnik
                                WHERE KorisnickoIme = @KorisnickoIme AND Lozinka = @Lozinka";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@KorisnickoIme", korisnickoIme);
                    komanda.Parameters.AddWithValue("@Lozinka", lozinka);

                    using (SqlDataReader citac = komanda.ExecuteReader())
                    {
                        if (citac.Read())
                        {
                            korisnik = new Korisnik
                            {
                                KorisnikID = Convert.ToInt32(citac["KorisnikID"]),
                                KorisnickoIme = citac["KorisnickoIme"].ToString(),
                                Lozinka = citac["Lozinka"].ToString(),
                                Uloga = citac["Uloga"].ToString()
                            };
                        }
                    }
                }
            }

            return korisnik;
        }
    }
}