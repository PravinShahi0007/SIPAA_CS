namespace SIPAA_CS.RelojChecadorTrabajador
{
    partial class SplashHuella
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
            this.PrgbCarga = new System.Windows.Forms.ProgressBar();
            this.lbOperacion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PrgbCarga
            // 
            this.PrgbCarga.Location = new System.Drawing.Point(53, 113);
            this.PrgbCarga.Name = "PrgbCarga";
            this.PrgbCarga.Size = new System.Drawing.Size(179, 23);
            this.PrgbCarga.TabIndex = 0;
            // 
            // lbOperacion
            // 
            this.lbOperacion.AutoSize = true;
            this.lbOperacion.Location = new System.Drawing.Point(50, 51);
            this.lbOperacion.Name = "lbOperacion";
            this.lbOperacion.Size = new System.Drawing.Size(35, 13);
            this.lbOperacion.TabIndex = 1;
            this.lbOperacion.Text = "label1";
            // 
            // SplashHuella
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbOperacion);
            this.Controls.Add(this.PrgbCarga);
            this.Name = "SplashHuella";
            this.Text = "SplashHuella";
            this.Load += new System.EventHandler(this.SplashHuella_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar PrgbCarga;
        private System.Windows.Forms.Label lbOperacion;
    }
}