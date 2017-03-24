namespace SIPAA_CS.RecursosHumanos.Reportes
{
    partial class Reportes
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
            this.ReporteCrystal = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // ReporteCrystal
            // 
            this.ReporteCrystal.ActiveViewIndex = -1;
            this.ReporteCrystal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReporteCrystal.Cursor = System.Windows.Forms.Cursors.Default;
            this.ReporteCrystal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReporteCrystal.Location = new System.Drawing.Point(0, 0);
            this.ReporteCrystal.Name = "ReporteCrystal";
            this.ReporteCrystal.Size = new System.Drawing.Size(1024, 768);
            this.ReporteCrystal.TabIndex = 0;
            this.ReporteCrystal.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.ReporteCrystal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer ReporteCrystal;
    }
}