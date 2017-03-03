namespace SIPAA_CS
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.PanelMetro = new System.Windows.Forms.Panel();
            this.btnAlmacen = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.barraSuperior = new System.Windows.Forms.Panel();
            this.btnAccesos = new System.Windows.Forms.Button();
            this.btnContabilidad = new System.Windows.Forms.Button();
            this.btnDeportivo = new System.Windows.Forms.Button();
            this.btnEscolar = new System.Windows.Forms.Button();
            this.btnSistemas = new System.Windows.Forms.Button();
            this.btnCompras = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnIngresos = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRecursosh = new System.Windows.Forms.Button();
            this.PanelMetro.SuspendLayout();
            this.barraSuperior.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMetro
            // 
            this.PanelMetro.AutoScroll = true;
            this.PanelMetro.Controls.Add(this.btnCompras);
            this.PanelMetro.Controls.Add(this.btnSistemas);
            this.PanelMetro.Controls.Add(this.btnEscolar);
            this.PanelMetro.Controls.Add(this.btnDeportivo);
            this.PanelMetro.Controls.Add(this.btnContabilidad);
            this.PanelMetro.Controls.Add(this.btnAccesos);
            this.PanelMetro.Controls.Add(this.btnIngresos);
            this.PanelMetro.Controls.Add(this.btnAlmacen);
            this.PanelMetro.Controls.Add(this.btnRecursosh);
            this.PanelMetro.Location = new System.Drawing.Point(61, 197);
            this.PanelMetro.Name = "PanelMetro";
            this.PanelMetro.Size = new System.Drawing.Size(901, 472);
            this.PanelMetro.TabIndex = 45;
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(174)))), ((int)(((byte)(65)))));
            this.btnAlmacen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlmacen.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlmacen.ForeColor = System.Drawing.Color.White;
            this.btnAlmacen.Image = ((System.Drawing.Image)(resources.GetObject("btnAlmacen.Image")));
            this.btnAlmacen.Location = new System.Drawing.Point(316, 24);
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Size = new System.Drawing.Size(270, 132);
            this.btnAlmacen.TabIndex = 45;
            this.btnAlmacen.Text = "Almacén";
            this.btnAlmacen.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnAlmacen.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_clear_white_18dp;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(1003, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(15, 15);
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
            this.btnMinimizar.Location = new System.Drawing.Point(982, 5);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(15, 15);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // barraSuperior
            // 
            this.barraSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.barraSuperior.Controls.Add(this.btnMinimizar);
            this.barraSuperior.Controls.Add(this.btnCerrar);
            this.barraSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraSuperior.Location = new System.Drawing.Point(0, 0);
            this.barraSuperior.Name = "barraSuperior";
            this.barraSuperior.Size = new System.Drawing.Size(1024, 26);
            this.barraSuperior.TabIndex = 1;
            this.barraSuperior.MouseDown += new System.Windows.Forms.MouseEventHandler(this.barraSuperior_MouseDown);
            this.barraSuperior.MouseMove += new System.Windows.Forms.MouseEventHandler(this.barraSuperior_MouseMove);
            this.barraSuperior.MouseUp += new System.Windows.Forms.MouseEventHandler(this.barraSuperior_MouseUp);
            // 
            // btnAccesos
            // 
            this.btnAccesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(139)))), ((int)(((byte)(32)))));
            this.btnAccesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccesos.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccesos.ForeColor = System.Drawing.Color.White;
            this.btnAccesos.Image = ((System.Drawing.Image)(resources.GetObject("btnAccesos.Image")));
            this.btnAccesos.Location = new System.Drawing.Point(31, 170);
            this.btnAccesos.Name = "btnAccesos";
            this.btnAccesos.Size = new System.Drawing.Size(270, 132);
            this.btnAccesos.TabIndex = 47;
            this.btnAccesos.Text = "Acessos";
            this.btnAccesos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnAccesos.UseVisualStyleBackColor = false;
            // 
            // btnContabilidad
            // 
            this.btnContabilidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(62)))), ((int)(((byte)(8)))));
            this.btnContabilidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContabilidad.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContabilidad.ForeColor = System.Drawing.Color.White;
            this.btnContabilidad.Image = global::SIPAA_CS.Properties.Resources.ic_insert_chart_white_48dp;
            this.btnContabilidad.Location = new System.Drawing.Point(316, 170);
            this.btnContabilidad.Name = "btnContabilidad";
            this.btnContabilidad.Size = new System.Drawing.Size(270, 132);
            this.btnContabilidad.TabIndex = 48;
            this.btnContabilidad.Text = "Contabilidad";
            this.btnContabilidad.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnContabilidad.UseVisualStyleBackColor = false;
            // 
            // btnDeportivo
            // 
            this.btnDeportivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(89)))), ((int)(((byte)(173)))));
            this.btnDeportivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeportivo.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeportivo.ForeColor = System.Drawing.Color.White;
            this.btnDeportivo.Image = ((System.Drawing.Image)(resources.GetObject("btnDeportivo.Image")));
            this.btnDeportivo.Location = new System.Drawing.Point(600, 170);
            this.btnDeportivo.Name = "btnDeportivo";
            this.btnDeportivo.Size = new System.Drawing.Size(270, 132);
            this.btnDeportivo.TabIndex = 49;
            this.btnDeportivo.Text = "Deportivo";
            this.btnDeportivo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnDeportivo.UseVisualStyleBackColor = false;
            // 
            // btnEscolar
            // 
            this.btnEscolar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(104)))), ((int)(((byte)(174)))));
            this.btnEscolar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolar.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscolar.ForeColor = System.Drawing.Color.White;
            this.btnEscolar.Image = global::SIPAA_CS.Properties.Resources.ic_school_white_48dp;
            this.btnEscolar.Location = new System.Drawing.Point(31, 316);
            this.btnEscolar.Name = "btnEscolar";
            this.btnEscolar.Size = new System.Drawing.Size(270, 132);
            this.btnEscolar.TabIndex = 50;
            this.btnEscolar.Text = "Control Escolar";
            this.btnEscolar.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnEscolar.UseVisualStyleBackColor = false;
            // 
            // btnSistemas
            // 
            this.btnSistemas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(0)))), ((int)(((byte)(62)))));
            this.btnSistemas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSistemas.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSistemas.ForeColor = System.Drawing.Color.White;
            this.btnSistemas.Image = global::SIPAA_CS.Properties.Resources.ic_devices_white_48dp;
            this.btnSistemas.Location = new System.Drawing.Point(316, 316);
            this.btnSistemas.Name = "btnSistemas";
            this.btnSistemas.Size = new System.Drawing.Size(270, 132);
            this.btnSistemas.TabIndex = 51;
            this.btnSistemas.Text = "Sistemas";
            this.btnSistemas.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnSistemas.UseVisualStyleBackColor = false;
            // 
            // btnCompras
            // 
            this.btnCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(47)))), ((int)(((byte)(66)))));
            this.btnCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompras.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompras.ForeColor = System.Drawing.Color.White;
            this.btnCompras.Image = global::SIPAA_CS.Properties.Resources.ic_local_shipping_white_48dp;
            this.btnCompras.Location = new System.Drawing.Point(600, 316);
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Size = new System.Drawing.Size(270, 132);
            this.btnCompras.TabIndex = 52;
            this.btnCompras.Text = "Compras";
            this.btnCompras.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnCompras.UseVisualStyleBackColor = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnSalir.Image = global::SIPAA_CS.Properties.Resources.ic_power_settings_new_white_18dp;
            this.btnSalir.Location = new System.Drawing.Point(18, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(50, 50);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panel3
            // 
            this.panel3.CausesValidation = false;
            this.panel3.Controls.Add(this.btnSalir);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.panel3.Location = new System.Drawing.Point(93, 693);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(839, 63);
            this.panel3.TabIndex = 44;
            // 
            // btnIngresos
            // 
            this.btnIngresos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(75)))), ((int)(((byte)(43)))));
            this.btnIngresos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresos.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresos.ForeColor = System.Drawing.Color.White;
            this.btnIngresos.Image = ((System.Drawing.Image)(resources.GetObject("btnIngresos.Image")));
            this.btnIngresos.Location = new System.Drawing.Point(600, 24);
            this.btnIngresos.Name = "btnIngresos";
            this.btnIngresos.Size = new System.Drawing.Size(270, 132);
            this.btnIngresos.TabIndex = 46;
            this.btnIngresos.Text = "Ingresos";
            this.btnIngresos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnIngresos.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::SIPAA_CS.Properties.Resources.logo;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel5.Location = new System.Drawing.Point(814, 15);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.panel7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.panel7.Location = new System.Drawing.Point(153, 137);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(900, 3);
            this.panel7.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::SIPAA_CS.Properties.Resources.ic_home_white_18dp;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(0, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "       Inicio                     ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 140);
            this.panel2.TabIndex = 2;
            // 
            // btnRecursosh
            // 
            this.btnRecursosh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(95)))), ((int)(((byte)(143)))));
            this.btnRecursosh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRecursosh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecursosh.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecursosh.ForeColor = System.Drawing.Color.White;
            this.btnRecursosh.Image = ((System.Drawing.Image)(resources.GetObject("btnRecursosh.Image")));
            this.btnRecursosh.Location = new System.Drawing.Point(31, 24);
            this.btnRecursosh.Name = "btnRecursosh";
            this.btnRecursosh.Size = new System.Drawing.Size(270, 132);
            this.btnRecursosh.TabIndex = 44;
            this.btnRecursosh.Text = "Recursos Humanos";
            this.btnRecursosh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnRecursosh.UseVisualStyleBackColor = false;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.barraSuperior);
            this.Controls.Add(this.PanelMetro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.PanelMetro.ResumeLayout(false);
            this.barraSuperior.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMetro;
        private System.Windows.Forms.Button btnCompras;
        private System.Windows.Forms.Button btnSistemas;
        private System.Windows.Forms.Button btnEscolar;
        private System.Windows.Forms.Button btnDeportivo;
        private System.Windows.Forms.Button btnContabilidad;
        private System.Windows.Forms.Button btnAccesos;
        private System.Windows.Forms.Button btnIngresos;
        private System.Windows.Forms.Button btnAlmacen;
        private System.Windows.Forms.Button btnRecursosh;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Panel barraSuperior;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
    }
}