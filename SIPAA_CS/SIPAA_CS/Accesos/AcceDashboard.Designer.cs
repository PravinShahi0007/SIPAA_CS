namespace SIPAA_CS.Accesos
{
    partial class AcceDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcceDashboard));
            this.label3 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.MenuAccesos = new System.Windows.Forms.MenuStrip();
            this.msCatalogo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPerfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModulos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProcesos = new System.Windows.Forms.ToolStripMenuItem();
            this.msAsignacionPerfil = new System.Windows.Forms.ToolStripMenuItem();
            this.asignaciónDePerfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignaciónDeMódulosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asignaciónDeProcesosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.lblusuario = new System.Windows.Forms.Label();
            this.pnlimgusuario = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.MenuAccesos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::SIPAA_CS.Properties.Resources.ic_fingerprint_white_18dp;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(415, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "       Accesos         ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_clear_white_18dp;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(997, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_remove_white_18dp;
            this.btnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.Location = new System.Drawing.Point(972, 2);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
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
            // MenuAccesos
            // 
            this.MenuAccesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.MenuAccesos.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuAccesos.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuAccesos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msCatalogo,
            this.msAsignacionPerfil});
            this.MenuAccesos.Location = new System.Drawing.Point(27, 3);
            this.MenuAccesos.Name = "MenuAccesos";
            this.MenuAccesos.Size = new System.Drawing.Size(339, 26);
            this.MenuAccesos.TabIndex = 5;
            this.MenuAccesos.TabStop = true;
            // 
            // msCatalogo
            // 
            this.msCatalogo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUsuarios,
            this.tsmiPerfiles,
            this.tsmiModulos,
            this.tsmiProcesos});
            this.msCatalogo.ForeColor = System.Drawing.Color.White;
            this.msCatalogo.Image = global::SIPAA_CS.Properties.Resources.ic_view_carousel_white_24dp;
            this.msCatalogo.Name = "msCatalogo";
            this.msCatalogo.Size = new System.Drawing.Size(108, 22);
            this.msCatalogo.Tag = "frmCatalogos";
            this.msCatalogo.Text = "Catálogos";
            // 
            // tsmiUsuarios
            // 
            this.tsmiUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.tsmiUsuarios.ForeColor = System.Drawing.Color.White;
            this.tsmiUsuarios.Image = global::SIPAA_CS.Properties.Resources.ic_view_carousel_white_24dp;
            this.tsmiUsuarios.Name = "tsmiUsuarios";
            this.tsmiUsuarios.Size = new System.Drawing.Size(152, 22);
            this.tsmiUsuarios.Tag = "Usuarios";
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
            this.tsmiPerfiles.Tag = "Perfiles";
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
            this.tsmiModulos.Tag = "Modulos";
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
            this.tsmiProcesos.Tag = "Procesos";
            this.tsmiProcesos.Text = "Procesos";
            this.tsmiProcesos.Click += new System.EventHandler(this.tsmiProcesos_Click);
            // 
            // msAsignacionPerfil
            // 
            this.msAsignacionPerfil.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asignaciónDePerfilToolStripMenuItem,
            this.asignaciónDeMódulosToolStripMenuItem,
            this.asignaciónDeProcesosToolStripMenuItem});
            this.msAsignacionPerfil.ForeColor = System.Drawing.Color.White;
            this.msAsignacionPerfil.Image = global::SIPAA_CS.Properties.Resources.ic_compare_arrows_white_24dp;
            this.msAsignacionPerfil.Name = "msAsignacionPerfil";
            this.msAsignacionPerfil.Size = new System.Drawing.Size(131, 22);
            this.msAsignacionPerfil.Tag = "frmAsignaciones";
            this.msAsignacionPerfil.Text = "Asignaciones";
            // 
            // asignaciónDePerfilToolStripMenuItem
            // 
            this.asignaciónDePerfilToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.asignaciónDePerfilToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.asignaciónDePerfilToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("asignaciónDePerfilToolStripMenuItem.Image")));
            this.asignaciónDePerfilToolStripMenuItem.Name = "asignaciónDePerfilToolStripMenuItem";
            this.asignaciónDePerfilToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.asignaciónDePerfilToolStripMenuItem.Tag = "UsuarioPerfil";
            this.asignaciónDePerfilToolStripMenuItem.Text = "Asignación de Perfil";
            this.asignaciónDePerfilToolStripMenuItem.Click += new System.EventHandler(this.msAsignacionPerfil_Click);
            // 
            // asignaciónDeMódulosToolStripMenuItem
            // 
            this.asignaciónDeMódulosToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.asignaciónDeMódulosToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.asignaciónDeMódulosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("asignaciónDeMódulosToolStripMenuItem.Image")));
            this.asignaciónDeMódulosToolStripMenuItem.Name = "asignaciónDeMódulosToolStripMenuItem";
            this.asignaciónDeMódulosToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.asignaciónDeMódulosToolStripMenuItem.Tag = "PerfilModulo";
            this.asignaciónDeMódulosToolStripMenuItem.Text = "Asignación de Módulos";
            this.asignaciónDeMódulosToolStripMenuItem.Click += new System.EventHandler(this.msAsignacionModulo_Click);
            // 
            // asignaciónDeProcesosToolStripMenuItem
            // 
            this.asignaciónDeProcesosToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.asignaciónDeProcesosToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.asignaciónDeProcesosToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("asignaciónDeProcesosToolStripMenuItem.Image")));
            this.asignaciónDeProcesosToolStripMenuItem.Name = "asignaciónDeProcesosToolStripMenuItem";
            this.asignaciónDeProcesosToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.asignaciónDeProcesosToolStripMenuItem.Tag = "frmAsignar_Proceso";
            this.asignaciónDeProcesosToolStripMenuItem.Text = "Asignación de Procesos";
            this.asignaciónDeProcesosToolStripMenuItem.Click += new System.EventHandler(this.msAsignacionProceso_Click);
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
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Location = new System.Drawing.Point(914, 1);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(30, 24);
            this.btnRegresar.TabIndex = 115;
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.Transparent;
            this.lblusuario.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblusuario.Location = new System.Drawing.Point(8, 72);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(172, 23);
            this.lblusuario.TabIndex = 117;
            this.lblusuario.Text = "Noe Alvarez Marquina  ";
            this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlimgusuario
            // 
            this.pnlimgusuario.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlimgusuario.BackgroundImage")));
            this.pnlimgusuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlimgusuario.Location = new System.Drawing.Point(12, 28);
            this.pnlimgusuario.Name = "pnlimgusuario";
            this.pnlimgusuario.Size = new System.Drawing.Size(37, 41);
            this.pnlimgusuario.TabIndex = 116;
            // 
            // AcceDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.f8;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.pnlimgusuario);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AcceDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AccesosDashboard";
            this.Load += new System.EventHandler(this.AccesosDashboard_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.MenuAccesos.ResumeLayout(false);
            this.MenuAccesos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.MenuStrip MenuAccesos;
        private System.Windows.Forms.ToolStripMenuItem msCatalogo;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsuarios;
        private System.Windows.Forms.ToolStripMenuItem tsmiPerfiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiModulos;
        private System.Windows.Forms.ToolStripMenuItem tsmiProcesos;
        private System.Windows.Forms.ToolStripMenuItem msAsignacionPerfil;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.ToolStripMenuItem asignaciónDePerfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignaciónDeMódulosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asignaciónDeProcesosToolStripMenuItem;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Panel pnlimgusuario;
    }
}