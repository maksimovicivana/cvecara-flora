using CvecaraFlora.Modeli.Modeli;
using Microsoft.Data.SqlClient;

namespace CvecaraFlora.Repozitorijum.Repozitorijumi
{
    public class NarudzbinaRepozitorijum
    {
        public List<Narudzbina> VratiSve()
        {
            List<Narudzbina> lista = new List<Narudzbina>();

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"SELECT n.NarudzbinaID, n.ImePrezimeKupca, n.BrojTelefona,
                                n.DatumKreiranja, n.DatumPreuzimanja, n.Status,
                                n.UkupnaCena, n.TipAranzmanaID, t.Naziv AS NazivTipaAranzmana
                                FROM Narudzbina n
                                INNER JOIN TipAranzmana t ON n.TipAranzmanaID = t.TipAranzmanaID";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        lista.Add(new Narudzbina
                        {
                            NarudzbinaID = Convert.ToInt32(citac["NarudzbinaID"]),
                            ImePrezimeKupca = citac["ImePrezimeKupca"].ToString(),
                            BrojTelefona = citac["BrojTelefona"].ToString(),
                            DatumKreiranja = Convert.ToDateTime(citac["DatumKreiranja"]),
                            DatumPreuzimanja = Convert.ToDateTime(citac["DatumPreuzimanja"]),
                            Status = citac["Status"].ToString(),
                            UkupnaCena = Convert.ToDecimal(citac["UkupnaCena"]),
                            TipAranzmanaID = Convert.ToInt32(citac["TipAranzmanaID"]),
                            NazivTipaAranzmana = citac["NazivTipaAranzmana"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public Narudzbina VratiPoId(int id)
        {
            Narudzbina narudzbina = null;

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = "SELECT * FROM Narudzbina WHERE NarudzbinaID = @Id";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader citac = komanda.ExecuteReader())
                    {
                        if (citac.Read())
                        {
                            narudzbina = new Narudzbina
                            {
                                NarudzbinaID = Convert.ToInt32(citac["NarudzbinaID"]),
                                ImePrezimeKupca = citac["ImePrezimeKupca"].ToString(),
                                BrojTelefona = citac["BrojTelefona"].ToString(),
                                DatumKreiranja = Convert.ToDateTime(citac["DatumKreiranja"]),
                                DatumPreuzimanja = Convert.ToDateTime(citac["DatumPreuzimanja"]),
                                Status = citac["Status"].ToString(),
                                UkupnaCena = Convert.ToDecimal(citac["UkupnaCena"]),
                                TipAranzmanaID = Convert.ToInt32(citac["TipAranzmanaID"])
                            };
                        }
                    }
                }
            }

            return narudzbina;
        }

        public Narudzbina VratiDetalje(int id)
        {
            Narudzbina narudzbina = null;

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"SELECT n.NarudzbinaID, n.ImePrezimeKupca, n.BrojTelefona,
                                n.DatumKreiranja, n.DatumPreuzimanja, n.Status,
                                n.UkupnaCena, n.TipAranzmanaID,
                                t.Naziv AS NazivTipaAranzmana,
                                s.VrstaCveca, s.Kolicina, s.DodatnaDekoracija, s.Cena AS CenaStavke
                                FROM Narudzbina n
                                INNER JOIN TipAranzmana t ON n.TipAranzmanaID = t.TipAranzmanaID
                                INNER JOIN StavkaNarudzbine s ON n.NarudzbinaID = s.NarudzbinaID
                                WHERE n.NarudzbinaID = @Id";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader citac = komanda.ExecuteReader())
                    {
                        if (citac.Read())
                        {
                            narudzbina = new Narudzbina
                            {
                                NarudzbinaID = Convert.ToInt32(citac["NarudzbinaID"]),
                                ImePrezimeKupca = citac["ImePrezimeKupca"].ToString(),
                                BrojTelefona = citac["BrojTelefona"].ToString(),
                                DatumKreiranja = Convert.ToDateTime(citac["DatumKreiranja"]),
                                DatumPreuzimanja = Convert.ToDateTime(citac["DatumPreuzimanja"]),
                                Status = citac["Status"].ToString(),
                                UkupnaCena = Convert.ToDecimal(citac["UkupnaCena"]),
                                TipAranzmanaID = Convert.ToInt32(citac["TipAranzmanaID"]),
                                NazivTipaAranzmana = citac["NazivTipaAranzmana"].ToString(),
                                VrstaCveca = citac["VrstaCveca"].ToString(),
                                Kolicina = Convert.ToInt32(citac["Kolicina"]),
                                DodatnaDekoracija = citac["DodatnaDekoracija"].ToString(),
                                CenaStavke = Convert.ToDecimal(citac["CenaStavke"])
                            };
                        }
                    }
                }
            }

            return narudzbina;
        }

        public void Dodaj(Narudzbina narudzbina)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                using (SqlTransaction transakcija = konekcija.BeginTransaction())
                {
                    try
                    {
                        string upitNarudzbina = @"INSERT INTO Narudzbina
                                (ImePrezimeKupca, BrojTelefona, DatumKreiranja, DatumPreuzimanja, Status, UkupnaCena, TipAranzmanaID)
                                OUTPUT INSERTED.NarudzbinaID
                                VALUES
                                (@ImePrezimeKupca, @BrojTelefona, @DatumKreiranja, @DatumPreuzimanja, @Status, @UkupnaCena, @TipAranzmanaID)";

                        int noviId;

                        using (SqlCommand komanda = new SqlCommand(upitNarudzbina, konekcija, transakcija))
                        {
                            komanda.Parameters.AddWithValue("@ImePrezimeKupca", narudzbina.ImePrezimeKupca);
                            komanda.Parameters.AddWithValue("@BrojTelefona", narudzbina.BrojTelefona);
                            komanda.Parameters.AddWithValue("@DatumKreiranja", narudzbina.DatumKreiranja);
                            komanda.Parameters.AddWithValue("@DatumPreuzimanja", narudzbina.DatumPreuzimanja);
                            komanda.Parameters.AddWithValue("@Status", narudzbina.Status);
                            komanda.Parameters.AddWithValue("@UkupnaCena", narudzbina.UkupnaCena);
                            komanda.Parameters.AddWithValue("@TipAranzmanaID", narudzbina.TipAranzmanaID);

                            noviId = Convert.ToInt32(komanda.ExecuteScalar());
                        }

                        string upitStavka = @"INSERT INTO StavkaNarudzbine
                                (NarudzbinaID, VrstaCveca, Kolicina, DodatnaDekoracija, Cena)
                                VALUES
                                (@NarudzbinaID, @VrstaCveca, @Kolicina, @DodatnaDekoracija, @Cena)";

                        using (SqlCommand komandaStavka = new SqlCommand(upitStavka, konekcija, transakcija))
                        {
                            komandaStavka.Parameters.AddWithValue("@NarudzbinaID", noviId);
                            komandaStavka.Parameters.AddWithValue("@VrstaCveca", narudzbina.VrstaCveca);
                            komandaStavka.Parameters.AddWithValue("@Kolicina", narudzbina.Kolicina);
                            komandaStavka.Parameters.AddWithValue("@DodatnaDekoracija", narudzbina.DodatnaDekoracija ?? "");
                            komandaStavka.Parameters.AddWithValue("@Cena", narudzbina.UkupnaCena);

                            komandaStavka.ExecuteNonQuery();
                        }

                        transakcija.Commit();
                    }
                    catch
                    {
                        transakcija.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Izmeni(Narudzbina narudzbina)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = @"UPDATE Narudzbina
                                SET ImePrezimeKupca = @ImePrezimeKupca,
                                    BrojTelefona = @BrojTelefona,
                                    DatumPreuzimanja = @DatumPreuzimanja,
                                    Status = @Status,
                                    UkupnaCena = @UkupnaCena,
                                    TipAranzmanaID = @TipAranzmanaID
                                WHERE NarudzbinaID = @NarudzbinaID";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@NarudzbinaID", narudzbina.NarudzbinaID);
                    komanda.Parameters.AddWithValue("@ImePrezimeKupca", narudzbina.ImePrezimeKupca);
                    komanda.Parameters.AddWithValue("@BrojTelefona", narudzbina.BrojTelefona);
                    komanda.Parameters.AddWithValue("@DatumPreuzimanja", narudzbina.DatumPreuzimanja);
                    komanda.Parameters.AddWithValue("@Status", narudzbina.Status);
                    komanda.Parameters.AddWithValue("@UkupnaCena", narudzbina.UkupnaCena);
                    komanda.Parameters.AddWithValue("@TipAranzmanaID", narudzbina.TipAranzmanaID);

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void Obrisi(int id)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string brisiStavke = "DELETE FROM StavkaNarudzbine WHERE NarudzbinaID = @Id";

                using (SqlCommand komandaStavke = new SqlCommand(brisiStavke, konekcija))
                {
                    komandaStavke.Parameters.AddWithValue("@Id", id);
                    komandaStavke.ExecuteNonQuery();
                }

                string brisiNarudzbinu = "DELETE FROM Narudzbina WHERE NarudzbinaID = @Id";

                using (SqlCommand komanda = new SqlCommand(brisiNarudzbinu, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Id", id);
                    komanda.ExecuteNonQuery();
                }
            }
        }
    }
}