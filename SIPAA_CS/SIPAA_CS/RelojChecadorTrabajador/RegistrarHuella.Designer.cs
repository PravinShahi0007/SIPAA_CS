namespace SIPAA_CS.RelojChecadorTrabajador
{
    partial class RegistrarHuella
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarHuella));
            this.pbHuella = new System.Windows.Forms.PictureBox();
            this.panelTag = new System.Windows.Forms.Panel();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbHoraActual = new System.Windows.Forms.Label();
            this.lbFecha = new System.Windows.Forms.Label();
            this.tmHora = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lbDia = new System.Windows.Forms.Label();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.timeReload = new System.Windows.Forms.Timer(this.components);
            this.lb1 = new System.Windows.Forms.Label();
            this.lbIdtrab = new System.Windows.Forms.Label();
            this.lbNombre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pInfoTrab = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuella)).BeginInit();
            this.panelTag.SuspendLayout();
            this.pInfoTrab.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbHuella
            // 
            this.pbHuella.Location = new System.Drawing.Point(578, 116);
            this.pbHuella.Name = "pbHuella";
            this.pbHuella.Size = new System.Drawing.Size(195, 225);
            this.pbHuella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHuella.TabIndex = 0;
            this.pbHuella.TabStop = false;
            // 
            // panelTag
            // 
            this.panelTag.BackColor = System.Drawing.Color.Transparent;
            this.panelTag.Controls.Add(this.lbMensaje);
            this.panelTag.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTag.Location = new System.Drawing.Point(18, 353);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(494, 42);
            this.panelTag.TabIndex = 114;
            this.panelTag.Visible = false;
            // 
            // lbMensaje
            // 
            this.lbMensaje.AutoSize = true;
            this.lbMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lbMensaje.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensaje.ForeColor = System.Drawing.Color.White;
            this.lbMensaje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMensaje.Location = new System.Drawing.Point(10, 12);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(72, 19);
            this.lbMensaje.TabIndex = 26;
            this.lbMensaje.Text = "Mensaje";
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbHoraActual
            // 
            this.lbHoraActual.AutoSize = true;
            this.lbHoraActual.BackColor = System.Drawing.Color.Transparent;
            this.lbHoraActual.Font = new System.Drawing.Font("Arial Rounded MT Bold", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoraActual.Location = new System.Drawing.Point(95, 177);
            this.lbHoraActual.Name = "lbHoraActual";
            this.lbHoraActual.Size = new System.Drawing.Size(300, 75);
            this.lbHoraActual.TabIndex = 115;
            this.lbHoraActual.Text = "00:00:00";
            // 
            // lbFecha
            // 
            this.lbFecha.AutoSize = true;
            this.lbFecha.BackColor = System.Drawing.Color.Transparent;
            this.lbFecha.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.Location = new System.Drawing.Point(12, 274);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(167, 32);
            this.lbFecha.TabIndex = 118;
            this.lbFecha.Text = "dd/MM/yyyy";
            // 
            // tmHora
            // 
            this.tmHora.Tick += new System.EventHandler(this.tmHora_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label2.Location = new System.Drawing.Point(148, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 41);
            this.label2.TabIndex = 119;
            this.label2.Text = "Bienvenido.";
            // 
            // lbDia
            // 
            this.lbDia.AutoSize = true;
            this.lbDia.BackColor = System.Drawing.Color.Transparent;
            this.lbDia.Font = new System.Drawing.Font("Arial Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDia.Location = new System.Drawing.Point(349, 270);
            this.lbDia.Name = "lbDia";
            this.lbDia.Size = new System.Drawing.Size(161, 38);
            this.lbDia.TabIndex = 120;
            this.lbDia.Text = "Miércoles";
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(578, 341);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(195, 2);
            this.prgBar.TabIndex = 122;
            this.prgBar.Visible = false;
            // 
            // timeReload
            // 
            this.timeReload.Enabled = true;
            this.timeReload.Interval = 3000;
            this.timeReload.Tick += new System.EventHandler(this.timeReload_Tick);
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lb1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb1.ForeColor = System.Drawing.Color.White;
            this.lb1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lb1.Location = new System.Drawing.Point(9, 40);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(97, 19);
            this.lb1.TabIndex = 27;
            this.lb1.Text = "Trabajador:";
            this.lb1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbIdtrab
            // 
            this.lbIdtrab.AutoSize = true;
            this.lbIdtrab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lbIdtrab.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIdtrab.ForeColor = System.Drawing.Color.White;
            this.lbIdtrab.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbIdtrab.Location = new System.Drawing.Point(133, 10);
            this.lbIdtrab.Name = "lbIdtrab";
            this.lbIdtrab.Size = new System.Drawing.Size(144, 19);
            this.lbIdtrab.TabIndex = 28;
            this.lbIdtrab.Text = "_______________";
            this.lbIdtrab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNombre
            // 
            this.lbNombre.AutoSize = true;
            this.lbNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lbNombre.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombre.ForeColor = System.Drawing.Color.White;
            this.lbNombre.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbNombre.Location = new System.Drawing.Point(112, 40);
            this.lbNombre.Name = "lbNombre";
            this.lbNombre.Size = new System.Drawing.Size(405, 19);
            this.lbNombre.TabIndex = 26;
            this.lbNombre.Text = "____________________________________________";
            this.lbNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 19);
            this.label1.TabIndex = 29;
            this.label1.Text = "No Empleado:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pInfoTrab
            // 
            this.pInfoTrab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.pInfoTrab.Controls.Add(this.label1);
            this.pInfoTrab.Controls.Add(this.lbNombre);
            this.pInfoTrab.Controls.Add(this.lb1);
            this.pInfoTrab.Controls.Add(this.lbIdtrab);
            this.pInfoTrab.Location = new System.Drawing.Point(18, 417);
            this.pInfoTrab.Name = "pInfoTrab";
            this.pInfoTrab.Size = new System.Drawing.Size(797, 66);
            this.pInfoTrab.TabIndex = 123;
            // 
            // RegistrarHuella
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.log2;
            this.ClientSize = new System.Drawing.Size(840, 495);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.lbDia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbFecha);
            this.Controls.Add(this.lbHoraActual);
            this.Controls.Add(this.panelTag);
            this.Controls.Add(this.pbHuella);
            this.Controls.Add(this.pInfoTrab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistrarHuella";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrarHuella";
            this.Load += new System.EventHandler(this.RegistrarHuella_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbHuella)).EndInit();
            this.panelTag.ResumeLayout(false);
            this.panelTag.PerformLayout();
            this.pInfoTrab.ResumeLayout(false);
            this.pInfoTrab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHuella;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbHoraActual;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.Timer tmHora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbDia;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Timer timeReload;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label lbIdtrab;
        private System.Windows.Forms.Label lbNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pInfoTrab;
    }
}