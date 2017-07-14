namespace SIPAA_CS.RelojChecadorTrabajador
{
    partial class CapturaTrabajador
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
            System.Windows.Forms.Label label6;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapturaTrabajador));
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.panelTag = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.txtPass = new System.Windows.Forms.MaskedTextBox();
            this.txtConfPass = new System.Windows.Forms.MaskedTextBox();
            this.prgb1 = new System.Windows.Forms.ProgressBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelMensaje = new System.Windows.Forms.Panel();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelTag.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelMensaje.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = System.Drawing.Color.Gray;
            label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label6.Location = new System.Drawing.Point(30, 62);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(296, 17);
            label6.TabIndex = 182;
            label6.Text = "     Capture la Contraseña para el Trabajador";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label5.Location = new System.Drawing.Point(58, 94);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(74, 16);
            label5.TabIndex = 181;
            label5.Text = "Contraseña";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.Gray;
            label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label1.Location = new System.Drawing.Point(360, 152);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 20);
            label1.TabIndex = 184;
            label1.Text = "Guardar";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label2.Location = new System.Drawing.Point(58, 161);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(133, 16);
            label2.TabIndex = 202;
            label2.Text = "Confirmar Contraseña";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.White;
            label3.Image = global::SIPAA_CS.Properties.Resources.ic_settings_white_24dp;
            label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label3.Location = new System.Drawing.Point(7, 6);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(205, 19);
            label3.TabIndex = 222;
            label3.Text = "      Contraseña Trabajador";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel3.Location = new System.Drawing.Point(55, 141);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(204, 1);
            this.panel3.TabIndex = 180;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnGuardar.Image = global::SIPAA_CS.Properties.Resources.Guardar;
            this.btnGuardar.Location = new System.Drawing.Point(364, 99);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(50, 50);
            this.btnGuardar.TabIndex = 178;
            this.btnGuardar.Tag = "Buscar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.panel1.Controls.Add(label3);
            this.panel1.Controls.Add(this.btnRegresar);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 30);
            this.panel1.TabIndex = 183;
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Location = new System.Drawing.Point(348, 2);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(30, 24);
            this.btnRegresar.TabIndex = 186;
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click_1);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_remove_white_18dp;
            this.btnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.Location = new System.Drawing.Point(407, 3);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 185;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_clear_white_18dp;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(431, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 184;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lbMensaje
            // 
            this.lbMensaje.AutoSize = true;
            this.lbMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lbMensaje.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensaje.ForeColor = System.Drawing.Color.White;
            this.lbMensaje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMensaje.Location = new System.Drawing.Point(13, 9);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(83, 25);
            this.lbMensaje.TabIndex = 26;
            this.lbMensaje.Tag = "frmAsignar_Perfil";
            this.lbMensaje.Text = "Mensaje";
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTag
            // 
            this.panelTag.Controls.Add(this.lbMensaje);
            this.panelTag.Controls.Add(this.prgb1);
            this.panelTag.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTag.Location = new System.Drawing.Point(0, 12);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(457, 44);
            this.panelTag.TabIndex = 199;
            this.panelTag.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel2.Location = new System.Drawing.Point(55, 210);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 1);
            this.panel2.TabIndex = 201;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(55, 114);
            this.txtPass.Mask = "99999";
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(204, 19);
            this.txtPass.TabIndex = 203;
            this.txtPass.ValidatingType = typeof(int);
            // 
            // txtConfPass
            // 
            this.txtConfPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtConfPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfPass.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfPass.Location = new System.Drawing.Point(55, 184);
            this.txtConfPass.Mask = "99999";
            this.txtConfPass.Name = "txtConfPass";
            this.txtConfPass.Size = new System.Drawing.Size(204, 19);
            this.txtConfPass.TabIndex = 204;
            this.txtConfPass.ValidatingType = typeof(int);
            // 
            // prgb1
            // 
            this.prgb1.Location = new System.Drawing.Point(0, 39);
            this.prgb1.Name = "prgb1";
            this.prgb1.Size = new System.Drawing.Size(457, 2);
            this.prgb1.Step = 1;
            this.prgb1.TabIndex = 205;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Enabled = false;
            this.panel4.Location = new System.Drawing.Point(0, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(456, 10);
            this.panel4.TabIndex = 221;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Red;
            this.panel5.Enabled = false;
            this.panel5.Location = new System.Drawing.Point(1, 8);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(456, 10);
            this.panel5.TabIndex = 221;
            // 
            // panelMensaje
            // 
            this.panelMensaje.Controls.Add(this.panelTag);
            this.panelMensaje.Location = new System.Drawing.Point(0, 259);
            this.panelMensaje.Name = "panelMensaje";
            this.panelMensaje.Size = new System.Drawing.Size(456, 71);
            this.panelMensaje.TabIndex = 222;
            // 
            // CapturaTrabajador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(456, 315);
            this.Controls.Add(this.panelMensaje);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.txtConfPass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(label1);
            this.Controls.Add(label6);
            this.Controls.Add(label5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CapturaTrabajador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capturar Contraseña Trabajador";
            this.Load += new System.EventHandler(this.Captura_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTag.ResumeLayout(false);
            this.panelTag.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panelMensaje.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.MaskedTextBox txtPass;
        private System.Windows.Forms.MaskedTextBox txtConfPass;
        private System.Windows.Forms.ProgressBar prgb1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panelMensaje;
    }
}