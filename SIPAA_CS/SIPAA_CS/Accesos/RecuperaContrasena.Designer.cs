namespace SIPAA_CS.Accesos
{
    partial class RecuperaContrasena
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
            System.Windows.Forms.Label label9;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecuperaContrasena));
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.lblalert = new System.Windows.Forms.Label();
            this.lblarrb = new System.Windows.Forms.Label();
            this.txtdominio = new System.Windows.Forms.TextBox();
            this.pnldominio = new System.Windows.Forms.Panel();
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.pnlcorreo = new System.Windows.Forms.Panel();
            this.lblcorreo = new System.Windows.Forms.Label();
            this.ckbaut = new System.Windows.Forms.CheckBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnreccontrasena = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            this.pnlBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            label9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.ForeColor = System.Drawing.Color.Gray;
            label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label9.Location = new System.Drawing.Point(39, 103);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(769, 17);
            label9.TabIndex = 182;
            label9.Text = "     ¿Olvidaste tu contraseña? Escribe tu usuario y te enviaremos una contraseña " +
    "temporal al correo que diste de alta.";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_clear_white_18dp;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(816, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 26;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Location = new System.Drawing.Point(744, 2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(30, 24);
            this.btnRegresar.TabIndex = 116;
            this.btnRegresar.TabStop = false;
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.pnlBusqueda.Controls.Add(this.lblalert);
            this.pnlBusqueda.Controls.Add(this.lblarrb);
            this.pnlBusqueda.Controls.Add(this.txtdominio);
            this.pnlBusqueda.Controls.Add(this.pnldominio);
            this.pnlBusqueda.Controls.Add(this.txtcorreo);
            this.pnlBusqueda.Controls.Add(this.pnlcorreo);
            this.pnlBusqueda.Controls.Add(this.lblcorreo);
            this.pnlBusqueda.Controls.Add(this.ckbaut);
            this.pnlBusqueda.Controls.Add(this.txtUsuario);
            this.pnlBusqueda.Controls.Add(this.panel4);
            this.pnlBusqueda.Controls.Add(this.label1);
            this.pnlBusqueda.Controls.Add(this.btnreccontrasena);
            this.pnlBusqueda.Location = new System.Drawing.Point(129, 140);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(616, 343);
            this.pnlBusqueda.TabIndex = 181;
            this.pnlBusqueda.TabStop = true;
            // 
            // lblalert
            // 
            this.lblalert.AutoSize = true;
            this.lblalert.Enabled = false;
            this.lblalert.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblalert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblalert.Location = new System.Drawing.Point(37, 315);
            this.lblalert.Name = "lblalert";
            this.lblalert.Size = new System.Drawing.Size(54, 19);
            this.lblalert.TabIndex = 217;
            this.lblalert.Text = "label2";
            this.lblalert.Visible = false;
            // 
            // lblarrb
            // 
            this.lblarrb.AutoSize = true;
            this.lblarrb.BackColor = System.Drawing.Color.Transparent;
            this.lblarrb.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblarrb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(62)))), ((int)(((byte)(120)))));
            this.lblarrb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblarrb.Location = new System.Drawing.Point(297, 194);
            this.lblarrb.Name = "lblarrb";
            this.lblarrb.Size = new System.Drawing.Size(26, 27);
            this.lblarrb.TabIndex = 215;
            this.lblarrb.Text = "@";
            this.lblarrb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblarrb.Visible = false;
            // 
            // txtdominio
            // 
            this.txtdominio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtdominio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdominio.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdominio.Location = new System.Drawing.Point(332, 202);
            this.txtdominio.MaxLength = 20;
            this.txtdominio.Name = "txtdominio";
            this.txtdominio.Size = new System.Drawing.Size(250, 19);
            this.txtdominio.TabIndex = 128;
            this.txtdominio.Visible = false;
            // 
            // pnldominio
            // 
            this.pnldominio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.pnldominio.Location = new System.Drawing.Point(331, 224);
            this.pnldominio.Name = "pnldominio";
            this.pnldominio.Size = new System.Drawing.Size(250, 1);
            this.pnldominio.TabIndex = 129;
            this.pnldominio.Visible = false;
            // 
            // txtcorreo
            // 
            this.txtcorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtcorreo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcorreo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcorreo.Location = new System.Drawing.Point(40, 202);
            this.txtcorreo.MaxLength = 20;
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(250, 19);
            this.txtcorreo.TabIndex = 125;
            this.txtcorreo.Visible = false;
            // 
            // pnlcorreo
            // 
            this.pnlcorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.pnlcorreo.Location = new System.Drawing.Point(39, 224);
            this.pnlcorreo.Name = "pnlcorreo";
            this.pnlcorreo.Size = new System.Drawing.Size(250, 1);
            this.pnlcorreo.TabIndex = 127;
            this.pnlcorreo.Visible = false;
            // 
            // lblcorreo
            // 
            this.lblcorreo.AutoSize = true;
            this.lblcorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.lblcorreo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcorreo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblcorreo.Location = new System.Drawing.Point(36, 173);
            this.lblcorreo.Name = "lblcorreo";
            this.lblcorreo.Size = new System.Drawing.Size(137, 18);
            this.lblcorreo.TabIndex = 126;
            this.lblcorreo.Text = "Correo electrónico";
            this.lblcorreo.Visible = false;
            // 
            // ckbaut
            // 
            this.ckbaut.AutoSize = true;
            this.ckbaut.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbaut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ckbaut.Location = new System.Drawing.Point(40, 108);
            this.ckbaut.Name = "ckbaut";
            this.ckbaut.Size = new System.Drawing.Size(277, 20);
            this.ckbaut.TabIndex = 124;
            this.ckbaut.Text = "Autorizo proceso para restaurar contraseña";
            this.ckbaut.UseVisualStyleBackColor = true;
            this.ckbaut.CheckedChanged += new System.EventHandler(this.ckbaut_CheckedChanged);
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(38, 65);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(300, 19);
            this.txtUsuario.TabIndex = 120;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel4.Location = new System.Drawing.Point(37, 87);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 1);
            this.panel4.TabIndex = 122;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label1.Location = new System.Drawing.Point(34, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 121;
            this.label1.Text = "Usuario";
            // 
            // btnreccontrasena
            // 
            this.btnreccontrasena.BackColor = System.Drawing.Color.Red;
            this.btnreccontrasena.Enabled = false;
            this.btnreccontrasena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreccontrasena.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreccontrasena.ForeColor = System.Drawing.Color.White;
            this.btnreccontrasena.Location = new System.Drawing.Point(371, 253);
            this.btnreccontrasena.Name = "btnreccontrasena";
            this.btnreccontrasena.Size = new System.Drawing.Size(210, 50);
            this.btnreccontrasena.TabIndex = 4;
            this.btnreccontrasena.Text = "ENVIAR";
            this.btnreccontrasena.UseVisualStyleBackColor = false;
            this.btnreccontrasena.Visible = false;
            this.btnreccontrasena.Click += new System.EventHandler(this.btnreccontrasena_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 7000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Image = global::SIPAA_CS.Properties.Resources.ic_settings_white_24dp;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(334, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(189, 23);
            this.label7.TabIndex = 209;
            this.label7.Text = "       Recupera contraseña";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecuperaContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.log2;
            this.ClientSize = new System.Drawing.Size(840, 495);
            this.Controls.Add(this.label7);
            this.Controls.Add(label9);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecuperaContrasena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recupera Contraseña";
            this.Load += new System.EventHandler(this.RecuperaContrasena_Load);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnreccontrasena;
        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.Panel pnlcorreo;
        private System.Windows.Forms.Label lblcorreo;
        private System.Windows.Forms.CheckBox ckbaut;
        private System.Windows.Forms.TextBox txtdominio;
        private System.Windows.Forms.Panel pnldominio;
        private System.Windows.Forms.Label lblarrb;
        private System.Windows.Forms.Label lblalert;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label7;
    }
}