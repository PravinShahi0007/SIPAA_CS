namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    partial class AccesosDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccesosDashboard));
            this.panel3 = new System.Windows.Forms.Panel();
            this.MenuAccesos = new System.Windows.Forms.MenuStrip();
            this.msCatalogo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPerfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModulos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProcesos = new System.Windows.Forms.ToolStripMenuItem();
            this.msAsignacionPerfil = new System.Windows.Forms.ToolStripMenuItem();
            this.msAsignacionModulo = new System.Windows.Forms.ToolStripMenuItem();
            this.msAsignacionProceso = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.MenuAccesos.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.panel3.Location = new System.Drawing.Point(1, 129);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1024, 1);
            this.panel3.TabIndex = 25;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // MenuAccesos
            // 
            this.MenuAccesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.MenuAccesos.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuAccesos.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuAccesos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msCatalogo,
            this.msAsignacionPerfil,
            this.msAsignacionModulo,
            this.msAsignacionProceso});
            this.MenuAccesos.Location = new System.Drawing.Point(27, 3);
            this.MenuAccesos.Name = "MenuAccesos";
            this.MenuAccesos.Size = new System.Drawing.Size(698, 26);
            this.MenuAccesos.TabIndex = 5;
            this.MenuAccesos.TabStop = true;
            // 
            // msCatalogo
            // 
            this.msCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.msCatalogo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUsuarios,
            this.tsmiPerfiles,
            this.tsmiModulos,
            this.tsmiProcesos});
            this.msCatalogo.ForeColor = System.Drawing.Color.White;
            this.msCatalogo.Image = global::SIPAA_CS.Properties.Resources.ic_view_list_white_18dp;
            this.msCatalogo.Name = "msCatalogo";
            this.msCatalogo.Size = new System.Drawing.Size(108, 22);
            this.msCatalogo.Tag = "frmCatalogos";
            this.msCatalogo.Text = "Catalogos";
            // 
            // tsmiUsuarios
            // 
            this.tsmiUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.tsmiUsuarios.ForeColor = System.Drawing.Color.White;
            this.tsmiUsuarios.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUsuarios.Image")));
            this.tsmiUsuarios.Name = "tsmiUsuarios";
            this.tsmiUsuarios.Size = new System.Drawing.Size(152, 22);
            this.tsmiUsuarios.Tag = "frmCrear_Usuario";
            this.tsmiUsuarios.Text = "Usuarios";
            this.tsmiUsuarios.Click += new System.EventHandler(this.tsmiUsuarios_Click);
            // 
            // tsmiPerfiles
            // 
            this.tsmiPerfiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.tsmiPerfiles.ForeColor = System.Drawing.Color.White;
            this.tsmiPerfiles.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPerfiles.Image")));
            this.tsmiPerfiles.Name = "tsmiPerfiles";
            this.tsmiPerfiles.Size = new System.Drawing.Size(152, 22);
            this.tsmiPerfiles.Tag = "frmCrear_Perfil";
            this.tsmiPerfiles.Text = "Perfiles";
            this.tsmiPerfiles.Click += new System.EventHandler(this.tsmiPerfiles_Click);
            // 
            // tsmiModulos
            // 
            this.tsmiModulos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.tsmiModulos.ForeColor = System.Drawing.Color.White;
            this.tsmiModulos.Image = ((System.Drawing.Image)(resources.GetObject("tsmiModulos.Image")));
            this.tsmiModulos.Name = "tsmiModulos";
            this.tsmiModulos.Size = new System.Drawing.Size(152, 22);
            this.tsmiModulos.Tag = "frmCrear_Modulo";
            this.tsmiModulos.Text = "Módulos";
            this.tsmiModulos.Click += new System.EventHandler(this.tsmiModulos_Click);
            // 
            // tsmiProcesos
            // 
            this.tsmiProcesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.tsmiProcesos.ForeColor = System.Drawing.Color.White;
            this.tsmiProcesos.Image = ((System.Drawing.Image)(resources.GetObject("tsmiProcesos.Image")));
            this.tsmiProcesos.Name = "tsmiProcesos";
            this.tsmiProcesos.Size = new System.Drawing.Size(152, 22);
            this.tsmiProcesos.Tag = "frmCrear_Proceso";
            this.tsmiProcesos.Text = "Procesos";
            this.tsmiProcesos.Click += new System.EventHandler(this.tsmiProcesos_Click);
            // 
            // msAsignacionPerfil
            // 
            this.msAsignacionPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.msAsignacionPerfil.ForeColor = System.Drawing.Color.White;
            this.msAsignacionPerfil.Image = global::SIPAA_CS.Properties.Resources.ic_compare_arrows_white_24dp;
            this.msAsignacionPerfil.Name = "msAsignacionPerfil";
            this.msAsignacionPerfil.Size = new System.Drawing.Size(176, 22);
            this.msAsignacionPerfil.Tag = "frmAsignar_Perfil";
            this.msAsignacionPerfil.Text = "Asignación de Perfil";
            this.msAsignacionPerfil.Click += new System.EventHandler(this.msAsignacionPerfil_Click);
            // 
            // msAsignacionModulo
            // 
            this.msAsignacionModulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.msAsignacionModulo.ForeColor = System.Drawing.Color.White;
            this.msAsignacionModulo.Image = global::SIPAA_CS.Properties.Resources.ic_compare_arrows_white_24dp;
            this.msAsignacionModulo.Name = "msAsignacionModulo";
            this.msAsignacionModulo.Size = new System.Drawing.Size(199, 22);
            this.msAsignacionModulo.Tag = "frmAsignar_Modulo";
            this.msAsignacionModulo.Text = "Asignación de Modulos";
            this.msAsignacionModulo.Click += new System.EventHandler(this.msAsignacionModulo_Click);
            // 
            // msAsignacionProceso
            // 
            this.msAsignacionProceso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.msAsignacionProceso.ForeColor = System.Drawing.Color.White;
            this.msAsignacionProceso.Image = global::SIPAA_CS.Properties.Resources.ic_compare_arrows_white_24dp;
            this.msAsignacionProceso.Name = "msAsignacionProceso";
            this.msAsignacionProceso.Size = new System.Drawing.Size(207, 22);
            this.msAsignacionProceso.Tag = "frmAsignar_Proceso";
            this.msAsignacionProceso.Text = "Asignación de Procesos";
            this.msAsignacionProceso.Click += new System.EventHandler(this.msAsignacionProceso_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.panelMenu.Controls.Add(this.MenuAccesos);
            this.panelMenu.Location = new System.Drawing.Point(0, 97);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1024, 32);
            this.panelMenu.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::SIPAA_CS.Properties.Resources.ic_fingerprint_white_18dp;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(417, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "       Accesos                    ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AccesosDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.f8;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AccesosDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AccesosDashboard";
            this.Load += new System.EventHandler(this.AccesosDashboard_Load);
            this.MenuAccesos.ResumeLayout(false);
            this.MenuAccesos.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MenuStrip MenuAccesos;
        private System.Windows.Forms.ToolStripMenuItem msCatalogo;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsuarios;
        private System.Windows.Forms.ToolStripMenuItem tsmiPerfiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiModulos;
        private System.Windows.Forms.ToolStripMenuItem tsmiProcesos;
        private System.Windows.Forms.ToolStripMenuItem msAsignacionPerfil;
        private System.Windows.Forms.ToolStripMenuItem msAsignacionModulo;
        private System.Windows.Forms.ToolStripMenuItem msAsignacionProceso;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label label3;
    }
}