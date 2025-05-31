using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TraductorXinkaEspanol
{
    class SugerenciasManager
    {
        private string connectionString;

        public SugerenciasManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> ObtenerPalabraEspanol()
        {
            List<string> palabras = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT Espanol FROM Traducciones ORDER BY Espanol ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        palabras.Add(reader["Espanol"].ToString());
                    }
                }
            }

            return palabras;
        }
    }
}
