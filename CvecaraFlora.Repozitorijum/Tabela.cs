using Microsoft.Data.SqlClient;
using System.Data;

namespace CvecaraFlora.Repozitorijum
{
    public class Tabela
    {
        protected DataTable IzvrsiUpit(string upit)
        {
            DataTable tabela = new DataTable();

            using (SqlConnection konekcija = new SqlConnection(Konekcija.VratiKonekcioniString()))
            {
                konekcija.Open();

                using (SqlCommand komanda = new SqlCommand(upit, konekcija))
                using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                {
                    adapter.Fill(tabela);
                }
            }

            return tabela;
        }
    }
}