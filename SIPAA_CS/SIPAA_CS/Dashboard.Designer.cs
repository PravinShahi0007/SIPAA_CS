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
            this.btnCompras = new System.Windows.Forms.Button();
            this.btnSistemas = new System.Windows.Forms.Button();
            this.btnEscolar = new System.Windows.Forms.Button();
            this.btnDeportivo = new System.Windows.Forms.Button();
            this.btnContabilidad = new System.Windows.Forms.Button();
            this.btnAccesos = new System.Windows.Forms.Button();
            this.btnIngresos = new System.Windows.Forms.Button();
            this.btnAlmacen = new System.Windows.Forms.Button();
            this.btnRecursosh = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblusuario = new System.Windows.Forms.Label();
            this.ptbimgusuario = new System.Windows.Forms.PictureBox();
            this.btnPower = new System.Windows.Forms.Button();
            this.lblconexion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnperfil = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblacceso = new System.Windows.Forms.Label();
            this.PanelMetro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbimgusuario)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelMetro
            // 
            this.PanelMetro.AutoScroll = true;
            this.PanelMetro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
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
            this.PanelMetro.TabIndex = 0;
            this.PanelMetro.TabStop = true;
            this.PanelMetro.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelMetro_Paint);
            // 
            // btnCompras
            // 
            this.btnCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompras.ForeColor = System.Drawing.Color.White;
            this.btnCompras.Image = global::SIPAA_CS.Properties.Resources.ic_local_shipping_white_48dp;
            this.btnCompras.Location = new System.Drawing.Point(600, 316);
            this.btnCompras.Name = "btnCompras";
            this.btnCompras.Size = new System.Drawing.Size(270, 132);
            this.btnCompras.TabIndex = 52;
            this.btnCompras.Tag = "COMPR";
            this.btnCompras.Text = "Compras";
            this.btnCompras.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnCompras.UseVisualStyleBackColor = false;
            // 
            // btnSistemas
            // 
            this.btnSistemas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSistemas.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_devices_white_48dp;
            this.btnSistemas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSistemas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSistemas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSistemas.ForeColor = System.Drawing.Color.White;
            this.btnSistemas.Location = new System.Drawing.Point(316, 316);
            this.btnSistemas.Name = "btnSistemas";
            this.btnSistemas.Size = new System.Drawing.Size(270, 132);
            this.btnSistemas.TabIndex = 51;
            this.btnSistemas.Tag = "SIST";
            this.btnSistemas.Text = "Sistemas";
            this.btnSistemas.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnSistemas.UseVisualStyleBackColor = false;
            this.btnSistemas.Click += new System.EventHandler(this.btnSistemas_Click);
            // 
            // btnEscolar
            // 
            this.btnEscolar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnEscolar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscolar.ForeColor = System.Drawing.Color.White;
            this.btnEscolar.Image = global::SIPAA_CS.Properties.Resources.ic_school_white_48dp;
            this.btnEscolar.Location = new System.Drawing.Point(31, 316);
            this.btnEscolar.Name = "btnEscolar";
            this.btnEscolar.Size = new System.Drawing.Size(270, 132);
            this.btnEscolar.TabIndex = 50;
            this.btnEscolar.Tag = "CTRESC";
            this.btnEscolar.Text = "Control Escolar";
            this.btnEscolar.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnEscolar.UseVisualStyleBackColor = false;
            // 
            // btnDeportivo
            // 
            this.btnDeportivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(85)))), ((int)(((byte)(72)))));
            this.btnDeportivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeportivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeportivo.ForeColor = System.Drawing.Color.White;
            this.btnDeportivo.Image = ((System.Drawing.Image)(resources.GetObject("btnDeportivo.Image")));
            this.btnDeportivo.Location = new System.Drawing.Point(600, 170);
            this.btnDeportivo.Name = "btnDeportivo";
            this.btnDeportivo.Size = new System.Drawing.Size(270, 132);
            this.btnDeportivo.TabIndex = 49;
            this.btnDeportivo.Tag = "DEPOR";
            this.btnDeportivo.Text = "Deportivo";
            this.btnDeportivo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnDeportivo.UseVisualStyleBackColor = false;
            // 
            // btnContabilidad
            // 
            this.btnContabilidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btnContabilidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContabilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContabilidad.ForeColor = System.Drawing.Color.White;
            this.btnContabilidad.Image = global::SIPAA_CS.Properties.Resources.ic_insert_chart_white_48dp;
            this.btnContabilidad.Location = new System.Drawing.Point(316, 170);
            this.btnContabilidad.Name = "btnContabilidad";
            this.btnContabilidad.Size = new System.Drawing.Size(270, 132);
            this.btnContabilidad.TabIndex = 48;
            this.btnContabilidad.Tag = "CONTA";
            this.btnContabilidad.Text = "Contabilidad";
            this.btnContabilidad.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnContabilidad.UseVisualStyleBackColor = false;
            // 
            // btnAccesos
            // 
            this.btnAccesos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnAccesos.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_fingerprint_white_48dp;
            this.btnAccesos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAccesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccesos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccesos.ForeColor = System.Drawing.Color.White;
            this.btnAccesos.Location = new System.Drawing.Point(31, 170);
            this.btnAccesos.Name = "btnAccesos";
            this.btnAccesos.Size = new System.Drawing.Size(270, 132);
            this.btnAccesos.TabIndex = 47;
            this.btnAccesos.Tag = "ACCE";
            this.btnAccesos.Text = "Accesos";
            this.btnAccesos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnAccesos.UseVisualStyleBackColor = false;
            this.btnAccesos.Click += new System.EventHandler(this.btnAccesos_Click);
            // 
            // btnIngresos
            // 
            this.btnIngresos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnIngresos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresos.ForeColor = System.Drawing.Color.White;
            this.btnIngresos.Image = ((System.Drawing.Image)(resources.GetObject("btnIngresos.Image")));
            this.btnIngresos.Location = new System.Drawing.Point(600, 24);
            this.btnIngresos.Name = "btnIngresos";
            this.btnIngresos.Size = new System.Drawing.Size(270, 132);
            this.btnIngresos.TabIndex = 46;
            this.btnIngresos.Tag = "INGR";
            this.btnIngresos.Text = "Ingresos";
            this.btnIngresos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnIngresos.UseVisualStyleBackColor = false;
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAlmacen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlmacen.ForeColor = System.Drawing.Color.White;
            this.btnAlmacen.Image = ((System.Drawing.Image)(resources.GetObject("btnAlmacen.Image")));
            this.btnAlmacen.Location = new System.Drawing.Point(316, 24);
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Size = new System.Drawing.Size(270, 132);
            this.btnAlmacen.TabIndex = 45;
            this.btnAlmacen.Tag = "ALMC";
            this.btnAlmacen.Text = "Almacén";
            this.btnAlmacen.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnAlmacen.UseVisualStyleBackColor = false;
            this.btnAlmacen.Click += new System.EventHandler(this.btnAlmacen_Click);
            // 
            // btnRecursosh
            // 
            this.btnRecursosh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnRecursosh.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_group_white_48dp;
            this.btnRecursosh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRecursosh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecursosh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecursosh.ForeColor = System.Drawing.Color.White;
            this.btnRecursosh.Location = new System.Drawing.Point(31, 24);
            this.btnRecursosh.Name = "btnRecursosh";
            this.btnRecursosh.Size = new System.Drawing.Size(270, 132);
            this.btnRecursosh.TabIndex = 44;
            this.btnRecursosh.Tag = "RECH";
            this.btnRecursosh.Text = "Recursos Humanos";
            this.btnRecursosh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnRecursosh.UseVisualStyleBackColor = false;
            this.btnRecursosh.Click += new System.EventHandler(this.btnRecursosh_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_remove_white_18dp;
            this.btnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.Location = new System.Drawing.Point(974, 1);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_clear_white_18dp;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(999, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(439, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 23);
            this.label3.TabIndex = 46;
            this.label3.Text = "         Inicio  -";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(531, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 22);
            this.label4.TabIndex = 47;
            this.label4.Text = "SIPAA";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.Transparent;
            this.lblusuario.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblusuario.Location = new System.Drawing.Point(8, 75);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(62, 20);
            this.lblusuario.TabIndex = 115;
            this.lblusuario.Text = "Usuario  ";
            this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ptbimgusuario
            // 
            this.ptbimgusuario.Image = ((System.Drawing.Image)(resources.GetObject("ptbimgusuario.Image")));
            this.ptbimgusuario.InitialImage = ((System.Drawing.Image)(resources.GetObject("ptbimgusuario.InitialImage")));
            this.ptbimgusuario.Location = new System.Drawing.Point(11, 31);
            this.ptbimgusuario.Name = "ptbimgusuario";
            this.ptbimgusuario.Size = new System.Drawing.Size(43, 41);
            this.ptbimgusuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbimgusuario.TabIndex = 150;
            this.ptbimgusuario.TabStop = false;
            // 
            // btnPower
            // 
            this.btnPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnPower.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPower.BackgroundImage")));
            this.btnPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPower.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPower.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnPower.Location = new System.Drawing.Point(918, 1);
            this.btnPower.Name = "btnPower";
            this.btnPower.Size = new System.Drawing.Size(26, 24);
            this.btnPower.TabIndex = 151;
            this.btnPower.TabStop = false;
            this.btnPower.UseVisualStyleBackColor = false;
            this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
            // 
            // lblconexion
            // 
            this.lblconexion.AutoSize = true;
            this.lblconexion.BackColor = System.Drawing.Color.Red;
            this.lblconexion.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblconexion.ForeColor = System.Drawing.Color.White;
            this.lblconexion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblconexion.Location = new System.Drawing.Point(12, 4);
            this.lblconexion.Name = "lblconexion";
            this.lblconexion.Size = new System.Drawing.Size(198, 19);
            this.lblconexion.TabIndex = 152;
            this.lblconexion.Text = "Conexion BD Desarrollo";
            this.lblconexion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(935, 745);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 153;
            this.label1.Text = "Ver. 1.0.0.66";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnperfil
            // 
            this.btnperfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnperfil.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_group_white_24dp;
            this.btnperfil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnperfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnperfil.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnperfil.Location = new System.Drawing.Point(825, 1);
            this.btnperfil.Name = "btnperfil";
            this.btnperfil.Size = new System.Drawing.Size(26, 24);
            this.btnperfil.TabIndex = 154;
            this.btnperfil.TabStop = false;
            this.btnperfil.UseVisualStyleBackColor = false;
            this.btnperfil.Click += new System.EventHandler(this.btnperfil_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(854, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 155;
            this.label2.Text = "Perfíl";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblacceso
            // 
            this.lblacceso.AutoSize = true;
            this.lblacceso.BackColor = System.Drawing.Color.Transparent;
            this.lblacceso.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblacceso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.lblacceso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblacceso.Location = new System.Drawing.Point(13, 103);
            this.lblacceso.Name = "lblacceso";
            this.lblacceso.Size = new System.Drawing.Size(52, 16);
            this.lblacceso.TabIndex = 214;
            this.lblacceso.Text = "Acceso";
            this.lblacceso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.f8;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lblacceso);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnperfil);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblconexion);
            this.Controls.Add(this.btnPower);
            this.Controls.Add(this.ptbimgusuario);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.PanelMetro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.PanelMetro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbimgusuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.PictureBox ptbimgusuario;
        private System.Windows.Forms.Button btnPower;
        private System.Windows.Forms.Label lblconexion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnperfil;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblacceso;
    }
}