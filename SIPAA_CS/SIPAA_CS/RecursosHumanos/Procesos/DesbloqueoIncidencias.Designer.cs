namespace SIPAA_CS.RecursosHumanos.Procesos
{
    partial class DesbloqueoIncidencias
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
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.cbFormaPago = new System.Windows.Forms.ComboBox();
            this.TxtFeIni = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtFeFin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtIdEmp = new System.Windows.Forms.TextBox();
            this.TxtNombreEmpleado = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtIdSupOri = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtIdDirOri = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvrechtinc = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnDesbloqueo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrechtinc)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.AutoSize = true;
            this.lblFormaPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.lblFormaPago.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblFormaPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblFormaPago.Location = new System.Drawing.Point(34, 118);
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.Size = new System.Drawing.Size(97, 16);
            this.lblFormaPago.TabIndex = 0;
            this.lblFormaPago.Text = "Forma de Pago";
            // 
            // cbFormaPago
            // 
            this.cbFormaPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbFormaPago.FormattingEnabled = true;
            this.cbFormaPago.Location = new System.Drawing.Point(37, 137);
            this.cbFormaPago.Name = "cbFormaPago";
            this.cbFormaPago.Size = new System.Drawing.Size(144, 21);
            this.cbFormaPago.TabIndex = 151;
            this.cbFormaPago.Text = "Seleccionar ...";
            this.cbFormaPago.SelectedIndexChanged += new System.EventHandler(this.cbFormaPago_SelectedIndexChanged_1);
            this.cbFormaPago.ValueMemberChanged += new System.EventHandler(this.cbFormaPago_SelectedIndexChanged);
            this.cbFormaPago.Click += new System.EventHandler(this.cbFormaPago_Click);
            // 
            // TxtFeIni
            // 
            this.TxtFeIni.Location = new System.Drawing.Point(349, 137);
            this.TxtFeIni.Name = "TxtFeIni";
            this.TxtFeIni.Size = new System.Drawing.Size(107, 20);
            this.TxtFeIni.TabIndex = 152;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(346, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 153;
            this.label1.Text = "Fecha Inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(459, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 154;
            this.label2.Text = "Fecha Final";
            // 
            // TxtFeFin
            // 
            this.TxtFeFin.Location = new System.Drawing.Point(462, 138);
            this.TxtFeFin.Name = "TxtFeFin";
            this.TxtFeFin.Size = new System.Drawing.Size(107, 20);
            this.TxtFeFin.TabIndex = 155;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label3.Location = new System.Drawing.Point(253, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 156;
            this.label3.Text = "( Periodo abierto)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(34, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 157;
            this.label4.Text = "Id Trabajador";
            // 
            // TxtIdEmp
            // 
            this.TxtIdEmp.Location = new System.Drawing.Point(37, 202);
            this.TxtIdEmp.Name = "TxtIdEmp";
            this.TxtIdEmp.Size = new System.Drawing.Size(73, 20);
            this.TxtIdEmp.TabIndex = 158;
            this.TxtIdEmp.Leave += new System.EventHandler(this.ObtieneEmpleado);
            // 
            // TxtNombreEmpleado
            // 
            this.TxtNombreEmpleado.Location = new System.Drawing.Point(218, 202);
            this.TxtNombreEmpleado.Name = "TxtNombreEmpleado";
            this.TxtNombreEmpleado.Size = new System.Drawing.Size(351, 20);
            this.TxtNombreEmpleado.TabIndex = 159;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label5.Location = new System.Drawing.Point(215, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 16);
            this.label5.TabIndex = 160;
            this.label5.Text = "Nombre del Trabajador";
            // 
            // TxtIdSupOri
            // 
            this.TxtIdSupOri.Location = new System.Drawing.Point(612, 202);
            this.TxtIdSupOri.Name = "TxtIdSupOri";
            this.TxtIdSupOri.Size = new System.Drawing.Size(96, 20);
            this.TxtIdSupOri.TabIndex = 161;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label6.Location = new System.Drawing.Point(609, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 162;
            this.label6.Text = "Supervisor";
            // 
            // TxtIdDirOri
            // 
            this.TxtIdDirOri.Location = new System.Drawing.Point(723, 202);
            this.TxtIdDirOri.Name = "TxtIdDirOri";
            this.TxtIdDirOri.Size = new System.Drawing.Size(96, 20);
            this.TxtIdDirOri.TabIndex = 163;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label7.Location = new System.Drawing.Point(720, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 16);
            this.label7.TabIndex = 164;
            this.label7.Text = "Director";
            // 
            // dgvrechtinc
            // 
            this.dgvrechtinc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvrechtinc.Location = new System.Drawing.Point(37, 257);
            this.dgvrechtinc.Name = "dgvrechtinc";
            this.dgvrechtinc.Size = new System.Drawing.Size(532, 277);
            this.dgvrechtinc.TabIndex = 165;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.Image = global::SIPAA_CS.Properties.Resources.Buscar;
            this.btnBuscar.Location = new System.Drawing.Point(845, 183);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(50, 50);
            this.btnBuscar.TabIndex = 166;
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // btnDesbloqueo
            // 
            this.btnDesbloqueo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesbloqueo.Location = new System.Drawing.Point(612, 257);
            this.btnDesbloqueo.Name = "btnDesbloqueo";
            this.btnDesbloqueo.Size = new System.Drawing.Size(121, 40);
            this.btnDesbloqueo.TabIndex = 178;
            this.btnDesbloqueo.Text = "Desbloquear Incidencias";
            this.btnDesbloqueo.UseVisualStyleBackColor = true;
            // 
            // DesbloqueoIncidencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.JSierra;
            this.ClientSize = new System.Drawing.Size(1025, 691);
            this.Controls.Add(this.btnDesbloqueo);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvrechtinc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TxtIdDirOri);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TxtIdSupOri);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtNombreEmpleado);
            this.Controls.Add(this.TxtIdEmp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtFeFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtFeIni);
            this.Controls.Add(this.cbFormaPago);
            this.Controls.Add(this.lblFormaPago);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.Name = "DesbloqueoIncidencias";
            this.Text = "DesbloqueoIncidencias";
            this.Load += new System.EventHandler(this.DesbloqueoIncidencias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvrechtinc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.ComboBox cbFormaPago;
        private System.Windows.Forms.TextBox TxtFeIni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtFeFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtIdEmp;
        private System.Windows.Forms.TextBox TxtNombreEmpleado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtIdSupOri;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtIdDirOri;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvrechtinc;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnDesbloqueo;
    }
}