# ProyectoFinal2-TraductorXinka

Códigos
  Form1:
```csharp
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace TraductorXinkaEspanol
{
    public partial class Form1: Form
    {
        string connectionString = "Server=ALEJANDROHPVICT\\SQLEXPRESS;Database=TraductorXinkadb;Trusted_Connection=True";
        SugerenciasManager sugerenciasManager;


        public Form1()
        {
            InitializeComponent();
            //Constructor del ListBox de sugerencias
            sugerenciasManager = new SugerenciasManager(connectionString);
            lstSugerencias.DataSource = sugerenciasManager.ObtenerPalabraEspanol();

            //Constructor del boton speech
            SpeechSynthesizer sintetizador = new SpeechSynthesizer();
            sintetizador.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult); // Selecciona una voz neutral y adulta
            sintetizador.Speak(txtSalida.Text);

            btnEscuchar2.Click += btnEscuchar2_Click; // Asignar evento al botón de escuchar entrada
        }

        //Método para buscar traducción en la base de datos
        string BuscarTraduccion(string palabra, bool esEspanol) //metodo implementado para buscar traducción en la base de datos
        {
            string resultado = null;
            string columnaOrigen = esEspanol ? "Espanol" : "Xinka";
            string columnaDestino = esEspanol ? "Xinka" : "Espanol";

            string palabraNormalizada = palabra.Trim().ToLower();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $@"Select { columnaDestino} From Traducciones Where Lower({ columnaOrigen}) = @palabra";



                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@palabra", palabraNormalizada);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        resultado = reader[columnaDestino].ToString();
                    }
                }
            }

            return resultado;
        }

        async Task<string> ConsultarOpenAI(string palabra, bool esEspanol)
        {
            try
            {
                string prompt = esEspanol
                ? $"(Eres un traductor de Español a Xinka) ¿Cómo se dice '{palabra}' en Xinka."
                : $"(Eres un traductor de Xinka a Español) ¿Cómo se dice '{palabra}' en Español (desde Xinka)";

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Colocar la API key acá, de OpenAi");

                var data = new
                {
                    model = "gpt-4-turbo",
                    messages = new[] {new { role = "user", content = prompt }}
                };

                var json = JsonConvert.SerializeObject(data);
                var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                var responseString = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result.choices[0].message.content.ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de la Api, no se pudo encontrar la traducción por medio de AI. " + ex.Message);
                return null;
            }
        } //metodo para consultar a OpenAI y obtener la traducción

        void GuardarTraduccion(string espanol, string xinka)
        {
            string espanolBase = espanol.Trim().Split(' ')[0].ToLower();
            string xinkaBase = xinka.Trim().Split(' ')[0].ToLower();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Insertar en tabla Traducciones si no existe
                string verificarQuery = "SELECT COUNT(*) FROM Traducciones WHERE LOWER(Espanol) = @espanol AND LOWER(Xinka) = @xinka";
                using (SqlCommand verificarCmd = new SqlCommand(verificarQuery, conn))
                {
                    verificarCmd.Parameters.AddWithValue("@espanol", espanolBase);
                    verificarCmd.Parameters.AddWithValue("@xinka", xinkaBase);

                    int count = (int)verificarCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertarQuery = "INSERT INTO Traducciones (Espanol, Xinka) VALUES (@espanol, @xinka)";
                        using (SqlCommand insertarCmd = new SqlCommand(insertarQuery, conn))
                        {
                            insertarCmd.Parameters.AddWithValue("@espanol", espanolBase);
                            insertarCmd.Parameters.AddWithValue("@xinka", xinkaBase);
                            insertarCmd.ExecuteNonQuery();
                        }
                    }
                }

                // Siempre guardar en el historial completo (sin normalizar)
                try
                {
                    string insertarHistorialQuery = "INSERT INTO HistorialTraducciones (Palabra, Traduccion) VALUES (@palabra, @traduccion)";
                    using (SqlCommand historialCmd = new SqlCommand(insertarHistorialQuery, conn))
                    {
                        historialCmd.Parameters.AddWithValue("@palabra", espanol.Trim());
                        historialCmd.Parameters.AddWithValue("@traduccion", xinka.Trim());
                        historialCmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Guardado en historial: " + espanol + " → " + xinka);
                    MessageBox.Show("Traducción guardada en historial.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar en el historial: " + ex.Message);
                }
            }
        }
         

        // Botones o eventos de la interfaz gráfica (traductor)
        private async void btnTraducir_Click(object sender, EventArgs e)
        {            
            string entrada = txtEntrada.Text.Trim();

            if (string.IsNullOrEmpty(entrada))
            {
                MessageBox.Show("Por favor escribe una palabra.");
                return;
            }

            bool esEspanol;
            if (cmbIdioma.SelectedIndex == 0)
                esEspanol = true; //para el Español a Xinka
            else
                esEspanol = false; //para el Xinka a Español

            string resultado = BuscarTraduccion(entrada, esEspanol);

            if (string.IsNullOrEmpty(resultado))
            {
                // No está en la base de datos, preguntar a la IA
                resultado = await ConsultarOpenAI(entrada, esEspanol);

                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    // Mostrar y guardar
                    txtSalida.Text = resultado;
                    GuardarTraduccion(esEspanol ? entrada : resultado, esEspanol ? resultado : entrada);
                    lstSugerencias.DataSource = null; // Limpiar el DataSource para actualizarlo
                    lstSugerencias.DataSource = sugerenciasManager.ObtenerPalabraEspanol(); // Actualizar el ListBox de sugerencias
                }
                else
                {
                    txtSalida.Text = "No se encontró traducción.";
                }
            }
            else
            {
                txtSalida.Text = resultado;
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtEntrada.Clear();
            txtSalida.Clear();
            txtEntrada.Focus();
        }

        private void lstSugerencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSugerencias.SelectedItem != null)
            {
                txtEntrada.Text = lstSugerencias.SelectedItem.ToString();
                txtEntrada.Focus();
                txtEntrada.SelectionStart = txtEntrada.Text.Length; // Mueve el cursor al final del texto
            }
        }

        private void btnEscuchar_Click(object sender, EventArgs e) //Boton para escuchar la traducción de salida
        {
            string texto = txtSalida.Text;
            if (!string.IsNullOrWhiteSpace(texto))
            {
                using (SpeechSynthesizer sintetizador = new SpeechSynthesizer())
                {
                    sintetizador.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult);
                    sintetizador.Speak(texto);
                }
            }
            else
            {
                MessageBox.Show("No hay texto para pronunciar.");
            }
        }

        private void btnEscuchar2_Click(object sender, EventArgs e)
        {
            string texto = txtEntrada.Text.Trim();

            if (!string.IsNullOrEmpty(texto))
            {
                SpeechSynthesizer sintetizador = new SpeechSynthesizer();
                sintetizador.SpeakAsync(texto);
            }
            else
            {
                MessageBox.Show("No hay texto para leer.");
            }
        }

        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Documento PDF|*.pdf";
                saveFile.Title = "Guardar vocabulario Español → Xinka";
                saveFile.FileName = "Vocabulario Español → Xinka";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportadorPDF exportador = new ExportadorPDF(connectionString);
                        exportador.ExportarVocabulario(saveFile.FileName);
                        MessageBox.Show("PDF generado con éxito.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar el PDF: " + ex.Message);
                    }
                }
            }
        }
    }
}
```

Clase 1 SugerenciasManager:
```csharp
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
```

Clase 2 ExportadorPDF:
```csharp
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

```
