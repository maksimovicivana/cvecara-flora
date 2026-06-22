using CvecaraFlora.Modeli.Modeli;
using Microsoft.Data.SqlClient;

namespace CvecaraFlora.Repozitorijum.Repozitorijumi
{
    public class StavkaNarudzbineRepozitorijum
    {
        public List<StavkaNarudzbine> VratiPoNarudzbiniId(int narudzbinaId)
        {
            List<StavkaNarudzbine> stavke = new List<StavkaNarudzbine>();

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"SELECT StavkaNarudzbineID, NarudzbinaID, VrstaCveca, 
                                Kolicina, DodatnaDekoracija, Cena
                                FROM StavkaNarudzbine
                                WHERE NarudzbinaID = @NarudzbinaID";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@NarudzbinaID", narudzbinaId);

                    using (SqlDataReader citac = komanda.ExecuteReader())
                    {
                        while (citac.Read())
                        {
                            stavke.Add(new StavkaNarudzbine
                            {
                                StavkaNarudzbineID = Convert.ToInt32(citac["StavkaNarudzbineID"]),
                                NarudzbinaID = Convert.ToInt32(citac["NarudzbinaID"]),
                                VrstaCveca = citac["VrstaCveca"].ToString(),
                                Kolicina = Convert.ToInt32(citac["Kolicina"]),
                                DodatnaDekoracija = citac["DodatnaDekoracija"].ToString(),
                                Cena = Convert.ToDecimal(citac["Cena"])
                            });
                        }
                    }
                }
            }

            return stavke;
        }

        public void Dodaj(StavkaNarudzbine stavka)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"INSERT INTO StavkaNarudzbine
                                (NarudzbinaID, VrstaCveca, Kolicina, DodatnaDekoracija, Cena)
                                VALUES
                                (@NarudzbinaID, @VrstaCveca, @Kolicina, @DodatnaDekoracija, @Cena)";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@NarudzbinaID", stavka.NarudzbinaID);
                    komanda.Parameters.AddWithValue("@VrstaCveca", stavka.VrstaCveca);
                    komanda.Parameters.AddWithValue("@Kolicina", stavka.Kolicina);
                    komanda.Parameters.AddWithValue("@DodatnaDekoracija", stavka.DodatnaDekoracija);
                    komanda.Parameters.AddWithValue("@Cena", stavka.Cena);

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void Izmeni(StavkaNarudzbine stavka)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"UPDATE StavkaNarudzbine
                                SET VrstaCveca = @VrstaCveca,
                                    Kolicina = @Kolicina,
                                    DodatnaDekoracija = @DodatnaDekoracija,
                                    Cena = @Cena
                                WHERE StavkaNarudzbineID = @StavkaNarudzbineID";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@StavkaNarudzbineID", stavka.StavkaNarudzbineID);
                    komanda.Parameters.AddWithValue("@VrstaCveca", stavka.VrstaCveca);
                    komanda.Parameters.AddWithValue("@Kolicina", stavka.Kolicina);
                    komanda.Parameters.AddWithValue("@DodatnaDekoracija", stavka.DodatnaDekoracija);
                    komanda.Parameters.AddWithValue("@Cena", stavka.Cena);

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void Obrisi(int id)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = "DELETE FROM StavkaNarudzbine WHERE StavkaNarudzbineID = @Id";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Id", id);
                    komanda.ExecuteNonQuery();
                }
            }
        }
    }
}