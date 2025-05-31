namespace TraductorXinkaEspanol
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnTraducir = new System.Windows.Forms.Button();
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.txtSalida = new System.Windows.Forms.TextBox();
            this.cmbIdioma = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lstSugerencias = new System.Windows.Forms.ListBox();
            this.btnEscuchar = new System.Windows.Forms.Button();
            this.btnEscuchar2 = new System.Windows.Forms.Button();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTraducir
            // 
            this.btnTraducir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnTraducir.Font = new System.Drawing.Font("Castellar", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTraducir.Location = new System.Drawing.Point(588, 459);
            this.btnTraducir.Name = "btnTraducir";
            this.btnTraducir.Size = new System.Drawing.Size(247, 31);
            this.btnTraducir.TabIndex = 0;
            this.btnTraducir.Text = "Traducir";
            this.btnTraducir.UseVisualStyleBackColor = false;
            this.btnTraducir.Click += new System.EventHandler(this.btnTraducir_Click);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntrada.Location = new System.Drawing.Point(588, 197);
            this.txtEntrada.Multiline = true;
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEntrada.Size = new System.Drawing.Size(247, 256);
            this.txtEntrada.TabIndex = 1;
            // 
            // txtSalida
            // 
            this.txtSalida.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalida.Location = new System.Drawing.Point(871, 197);
            this.txtSalida.Multiline = true;
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.ReadOnly = true;
            this.txtSalida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSalida.Size = new System.Drawing.Size(247, 256);
            this.txtSalida.TabIndex = 2;
            // 
            // cmbIdioma
            // 
            this.cmbIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdioma.FormattingEnabled = true;
            this.cmbIdioma.ItemHeight = 16;
            this.cmbIdioma.Items.AddRange(new object[] {
            "Español -> Xinka",
            "Xinka -> Español"});
            this.cmbIdioma.Location = new System.Drawing.Point(30, 167);
            this.cmbIdioma.Name = "cmbIdioma";
            this.cmbIdioma.Size = new System.Drawing.Size(317, 24);
            this.cmbIdioma.TabIndex = 3;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnLimpiar.Font = new System.Drawing.Font("Castellar", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(871, 459);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(247, 31);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lstSugerencias
            // 
            this.lstSugerencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSugerencias.FormattingEnabled = true;
            this.lstSugerencias.ItemHeight = 18;
            this.lstSugerencias.Location = new System.Drawing.Point(30, 197);
            this.lstSugerencias.Name = "lstSugerencias";
            this.lstSugerencias.Size = new System.Drawing.Size(317, 436);
            this.lstSugerencias.TabIndex = 5;
            this.lstSugerencias.SelectedIndexChanged += new System.EventHandler(this.lstSugerencias_SelectedIndexChanged);
            // 
            // btnEscuchar
            // 
            this.btnEscuchar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEscuchar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnEscuchar.Location = new System.Drawing.Point(1124, 197);
            this.btnEscuchar.Name = "btnEscuchar";
            this.btnEscuchar.Size = new System.Drawing.Size(25, 25);
            this.btnEscuchar.TabIndex = 6;
            this.btnEscuchar.Text = "🔊";
            this.btnEscuchar.UseVisualStyleBackColor = false;
            this.btnEscuchar.Click += new System.EventHandler(this.btnEscuchar_Click);
            // 
            // btnEscuchar2
            // 
            this.btnEscuchar2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnEscuchar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnEscuchar2.Location = new System.Drawing.Point(841, 197);
            this.btnEscuchar2.Name = "btnEscuchar2";
            this.btnEscuchar2.Size = new System.Drawing.Size(24, 25);
            this.btnEscuchar2.TabIndex = 7;
            this.btnEscuchar2.Text = "🔊";
            this.btnEscuchar2.UseVisualStyleBackColor = false;
            this.btnEscuchar2.Click += new System.EventHandler(this.btnEscuchar2_Click);
            // 
            // btnExportarPdf
            // 
            this.btnExportarPdf.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnExportarPdf.Font = new System.Drawing.Font("Microsoft Uighur", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarPdf.Location = new System.Drawing.Point(974, 12);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(175, 28);
            this.btnExportarPdf.TabIndex = 8;
            this.btnExportarPdf.Text = "Descargar Vocabulario (Español-Xinka)";
            this.btnExportarPdf.UseVisualStyleBackColor = false;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Castellar", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(384, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(404, 73);
            this.label1.TabIndex = 9;
            this.label1.Text = "Xinka-Go";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1176, 657);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.btnEscuchar2);
            this.Controls.Add(this.btnEscuchar);
            this.Controls.Add(this.lstSugerencias);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.cmbIdioma);
            this.Controls.Add(this.txtSalida);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.btnTraducir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "XinkaGo | Traductor Español - Xinka";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTraducir;
        private System.Windows.Forms.TextBox txtEntrada;
        private System.Windows.Forms.TextBox txtSalida;
        private System.Windows.Forms.ComboBox cmbIdioma;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.ListBox lstSugerencias;
        private System.Windows.Forms.Button btnEscuchar;
        private System.Windows.Forms.Button btnEscuchar2;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Label label1;
    }
}

