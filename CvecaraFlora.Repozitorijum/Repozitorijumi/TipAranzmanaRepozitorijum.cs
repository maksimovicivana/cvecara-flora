using CvecaraFlora.Modeli.Modeli;
using Microsoft.Data.SqlClient;
using CvecaraFlora.Repozitorijum;
using System.Data;

namespace CvecaraFlora.Repozitorijum.Repozitorijumi
{
    public class TipAranzmanaRepozitorijum : Tabela
    {
        public List<TipAranzmana> VratiSve()
        {
            List<TipAranzmana> lista = new List<TipAranzmana>();

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = "SELECT TipAranzmanaID, Naziv FROM TipAranzmana";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                using (SqlDataReader citac = komanda.ExecuteReader())
                {
                    while (citac.Read())
                    {
                        lista.Add(new TipAranzmana
                        {
                            TipAranzmanaID = Convert.ToInt32(citac["TipAranzmanaID"]),
                            Naziv = citac["Naziv"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public List<TipAranzmana> VratiSvePrekoTabele()
        {
            DataTable tabela = IzvrsiUpit("SELECT TipAranzmanaID, Naziv FROM TipAranzmana");

            List<TipAranzmana> lista = new List<TipAranzmana>();

            foreach (DataRow red in tabela.Rows)
            {
                lista.Add(new TipAranzmana
                {
                    TipAranzmanaID = Convert.ToInt32(red["TipAranzmanaID"]),
                    Naziv = red["Naziv"].ToString()
                });
            }

            return lista;
        }

        public TipAranzmana VratiPoId(int id)
        {
            TipAranzmana tip = null;

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = "SELECT TipAranzmanaID, Naziv FROM TipAranzmana WHERE TipAranzmanaID = @Id";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader citac = komanda.ExecuteReader())
                    {
                        if (citac.Read())
                        {
                            tip = new TipAranzmana
                            {
                                TipAranzmanaID = Convert.ToInt32(citac["TipAranzmanaID"]),
                                Naziv = citac["Naziv"].ToString()
                            };
                        }
                    }
                }
            }

            return tip;
        }

        public void Dodaj(string naziv)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                using (SqlCommand komanda = new SqlCommand("DodajTipAranzmana", konekcija))
                {
                    komanda.CommandType = System.Data.CommandType.StoredProcedure;
                    komanda.Parameters.AddWithValue("@Naziv", naziv);

                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void Izmeni(TipAranzmana tip)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = "UPDATE TipAranzmana SET Naziv = @Naziv WHERE TipAranzmanaID = @Id";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Naziv", tip.Naziv);
                    komanda.Parameters.AddWithValue("@Id", tip.TipAranzmanaID);
                    komanda.ExecuteNonQuery();
                }
            }
        }

        public void Obrisi(int id)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                string upit = "DELETE FROM TipAranzmana WHERE TipAranzmanaID = @Id";

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                {
                    komanda.Parameters.AddWithValue("@Id", id);
                    komanda.ExecuteNonQuery();
                }
            }
        }

        public List<TipAranzmana> VratiSvePrekoEntityFramework()
        {
            using (CvecaraFloraDbContext context = new CvecaraFloraDbContext())
            {
                return context.TipoviAranzmana.ToList();
            }
        }
    }
}