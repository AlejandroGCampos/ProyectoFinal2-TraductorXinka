using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TraductorXinkaEspanol
{
    public class ExportadorPDF
    {
        private string connectionString;

        public ExportadorPDF(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ExportarVocabulario(string rutaArchivo)
        {
            var traducciones = new List<Tuple<string, string>>();
            string query = "SELECT Espanol, Xinka FROM Traducciones ORDER BY Espanol ASC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string espanol = reader["Espanol"].ToString();
                        string xinka = reader["Xinka"].ToString();
                        traducciones.Add(Tuple.Create(espanol, xinka));
                    }
                }
            }

            if (traducciones.Count == 0)
                throw new Exception("No hay vocabulario para exportar.");

            GenerarPDF(traducciones, rutaArchivo);
        }

        private void GenerarPDF(List<Tuple<string, string>> datos, string rutaArchivo)
        {
            Document doc = new Document(PageSize.A4);
            PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
            var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 14);

            doc.Add(new Paragraph("Vocabulario Español → Xinka", titleFont));
            doc.Add(new Paragraph(" ")); // Espacio

            foreach (var item in datos)
            {
                doc.Add(new Paragraph($"{item.Item1} → {item.Item2}", bodyFont));
            }

            doc.Close();
        }
    }
}
