namespace SIPAA_CS.RelojChecadorTrabajador
{
    partial class CapturaHora
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
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapturaHora));
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label7;
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cbHora = new System.Windows.Forms.ComboBox();
            this.cbMinutos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMes = new System.Windows.Forms.ComboBox();
            this.cbdia = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbAnio = new System.Windows.Forms.ComboBox();
            this.prgb1 = new System.Windows.Forms.ProgressBar();
            this.panelTag = new System.Windows.Forms.Panel();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.panelMensaje = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panelAccion = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            label6 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelTag.SuspendLayout();
            this.panelMensaje.SuspendLayout();
            this.panelAccion.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = System.Drawing.Color.White;
            label6.Image = global::SIPAA_CS.Properties.Resources.ic_settings_white_24dp;
            label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label6.Location = new System.Drawing.Point(10, 5);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(109, 19);
            label6.TabIndex = 187;
            label6.Text = "     Hora Reloj";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.Gray;
            label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label2.Location = new System.Drawing.Point(3, 19);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(122, 17);
            label2.TabIndex = 207;
            label2.Text = "     Hora Asignada";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.Gray;
            label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label3.Location = new System.Drawing.Point(2, 101);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(132, 17);
            label3.TabIndex = 211;
            label3.Text = "     Fecha Asignada";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.ForeColor = System.Drawing.Color.DimGray;
            label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label7.Location = new System.Drawing.Point(363, 90);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 20);
            label7.TabIndex = 214;
            label7.Text = "Guardar";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.panel1.Controls.Add(label6);
            this.panel1.Controls.Add(this.btnRegresar);
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Location = new System.Drawing.Point(-2, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 30);
            this.panel1.TabIndex = 201;
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
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
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
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
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
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnGuardar.Image = global::SIPAA_CS.Properties.Resources.Guardar;
            this.btnGuardar.Location = new System.Drawing.Point(368, 43);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(50, 50);
            this.btnGuardar.TabIndex = 203;
            this.btnGuardar.Tag = "Buscar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cbHora
            // 
            this.cbHora.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbHora.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbHora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbHora.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHora.FormattingEnabled = true;
            this.cbHora.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbHora.Location = new System.Drawing.Point(39, 47);
            this.cbHora.Name = "cbHora";
            this.cbHora.Size = new System.Drawing.Size(48, 24);
            this.cbHora.TabIndex = 204;
            this.cbHora.Text = "0";
            // 
            // cbMinutos
            // 
            this.cbMinutos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMinutos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMinutos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbMinutos.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMinutos.FormattingEnabled = true;
            this.cbMinutos.Items.AddRange(new object[] {
            "Seleccionar ",
            "Activo",
            "Inactivo"});
            this.cbMinutos.Location = new System.Drawing.Point(108, 47);
            this.cbMinutos.Name = "cbMinutos";
            this.cbMinutos.Size = new System.Drawing.Size(48, 24);
            this.cbMinutos.TabIndex = 205;
            this.cbMinutos.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 20);
            this.label1.TabIndex = 206;
            this.label1.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(107, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 33);
            this.label4.TabIndex = 210;
            this.label4.Text = "/";
            // 
            // cbMes
            // 
            this.cbMes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbMes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbMes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbMes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMes.FormattingEnabled = true;
            this.cbMes.Items.AddRange(new object[] {
            "Seleccionar ",
            "Activo",
            "Inactivo"});
            this.cbMes.Location = new System.Drawing.Point(130, 134);
            this.cbMes.Name = "cbMes";
            this.cbMes.Size = new System.Drawing.Size(93, 24);
            this.cbMes.TabIndex = 209;
            this.cbMes.Text = "Enero";
            this.cbMes.SelectedIndexChanged += new System.EventHandler(this.cbMes_SelectedIndexChanged);
            // 
            // cbdia
            // 
            this.cbdia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbdia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbdia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbdia.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbdia.FormattingEnabled = true;
            this.cbdia.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cbdia.Location = new System.Drawing.Point(40, 134);
            this.cbdia.Name = "cbdia";
            this.cbdia.Size = new System.Drawing.Size(66, 24);
            this.cbdia.TabIndex = 208;
            this.cbdia.Text = "31";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(229, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 33);
            this.label5.TabIndex = 213;
            this.label5.Text = "/";
            // 
            // cbAnio
            // 
            this.cbAnio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbAnio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbAnio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbAnio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAnio.FormattingEnabled = true;
            this.cbAnio.Items.AddRange(new object[] {
            "Seleccionar ",
            "Activo",
            "Inactivo"});
            this.cbAnio.Location = new System.Drawing.Point(252, 134);
            this.cbAnio.Name = "cbAnio";
            this.cbAnio.Size = new System.Drawing.Size(71, 24);
            this.cbAnio.TabIndex = 212;
            this.cbAnio.Text = "1999";
            // 
            // prgb1
            // 
            this.prgb1.Location = new System.Drawing.Point(3, 50);
            this.prgb1.Name = "prgb1";
            this.prgb1.Size = new System.Drawing.Size(457, 2);
            this.prgb1.Step = 1;
            this.prgb1.TabIndex = 216;
            // 
            // panelTag
            // 
            this.panelTag.Controls.Add(this.lbMensaje);
            this.panelTag.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTag.Location = new System.Drawing.Point(3, 3);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(443, 46);
            this.panelTag.TabIndex = 215;
            this.panelTag.Visible = false;
            // 
            // lbMensaje
            // 
            this.lbMensaje.AutoSize = true;
            this.lbMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lbMensaje.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensaje.ForeColor = System.Drawing.Color.White;
            this.lbMensaje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMensaje.Location = new System.Drawing.Point(10, 11);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(73, 23);
            this.lbMensaje.TabIndex = 26;
            this.lbMensaje.Tag = "frmAsignar_Perfil";
            this.lbMensaje.Text = "Mensaje";
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMensaje
            // 
            this.panelMensaje.Controls.Add(this.panelTag);
            this.panelMensaje.Controls.Add(this.prgb1);
            this.panelMensaje.Location = new System.Drawing.Point(-2, 314);
            this.panelMensaje.Name = "panelMensaje";
            this.panelMensaje.Size = new System.Drawing.Size(455, 72);
            this.panelMensaje.TabIndex = 217;
            // 
            // timer1
            // 
            this.timer1.Interval = 3500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox1.Location = new System.Drawing.Point(23, 43);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 18);
            this.checkBox1.TabIndex = 218;
            this.checkBox1.Text = "Hora Automática";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panelAccion
            // 
            this.panelAccion.Controls.Add(label2);
            this.panelAccion.Controls.Add(this.cbHora);
            this.panelAccion.Controls.Add(this.cbMinutos);
            this.panelAccion.Controls.Add(this.label1);
            this.panelAccion.Controls.Add(this.cbdia);
            this.panelAccion.Controls.Add(this.label5);
            this.panelAccion.Controls.Add(this.cbMes);
            this.panelAccion.Controls.Add(this.cbAnio);
            this.panelAccion.Controls.Add(this.label4);
            this.panelAccion.Controls.Add(label3);
            this.panelAccion.Enabled = false;
            this.panelAccion.Location = new System.Drawing.Point(12, 122);
            this.panelAccion.Name = "panelAccion";
            this.panelAccion.Size = new System.Drawing.Size(432, 180);
            this.panelAccion.TabIndex = 219;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(26, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 23);
            this.label8.TabIndex = 27;
            this.label8.Tag = "frmAsignar_Perfil";
            this.label8.Text = "Mensaje";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(26, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 23);
            this.label9.TabIndex = 220;
            this.label9.Tag = "frmAsignar_Perfil";
            this.label9.Text = "Mensaje";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(456, 10);
            this.panel2.TabIndex = 220;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Red;
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(1, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(456, 10);
            this.panel3.TabIndex = 221;
            // 
            // CapturaHora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.ClientSize = new System.Drawing.Size(456, 373);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panelAccion);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panelMensaje);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(label7);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CapturaHora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CapturaHora";
            this.Load += new System.EventHandler(this.CapturaHora_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTag.ResumeLayout(false);
            this.panelTag.PerformLayout();
            this.panelMensaje.ResumeLayout(false);
            this.panelAccion.ResumeLayout(false);
            this.panelAccion.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cbHora;
        private System.Windows.Forms.ComboBox cbMinutos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbMes;
        private System.Windows.Forms.ComboBox cbdia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbAnio;
        private System.Windows.Forms.ProgressBar prgb1;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Panel panelMensaje;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panelAccion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}