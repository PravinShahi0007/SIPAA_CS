namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    partial class Trabajadores
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trabajadores));
            System.Windows.Forms.Label label3;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioBtnAmbos = new System.Windows.Forms.RadioButton();
            this.radioBtnInactivo = new System.Windows.Forms.RadioButton();
            this.radioBtnActivo = new System.Windows.Forms.RadioButton();
            this.radioBtnapaterno = new System.Windows.Forms.RadioButton();
            this.radioBtnnempleado = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtEmpleado = new System.Windows.Forms.TextBox();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.lblusuario = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lbltitulo = new System.Windows.Forms.Label();
            this.ptbimgusuario = new System.Windows.Forms.PictureBox();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.pnlBusqueda.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbimgusuario)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label2.Location = new System.Drawing.Point(29, 218);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 16);
            label2.TabIndex = 133;
            label2.Text = "Empleados";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label4.Location = new System.Drawing.Point(25, 54);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(76, 16);
            label4.TabIndex = 44;
            label4.Text = "Descripción";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.Gray;
            label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label1.Location = new System.Drawing.Point(4, 4);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(182, 17);
            label1.TabIndex = 41;
            label1.Text = "     Buscar Empleados por:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.Gray;
            label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label3.Location = new System.Drawing.Point(2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(74, 17);
            label3.TabIndex = 142;
            label3.Text = "     Estado";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.pnlBusqueda.Controls.Add(this.panel1);
            this.pnlBusqueda.Controls.Add(this.radioBtnapaterno);
            this.pnlBusqueda.Controls.Add(this.radioBtnnempleado);
            this.pnlBusqueda.Controls.Add(label4);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Controls.Add(this.panel5);
            this.pnlBusqueda.Controls.Add(this.txtEmpleado);
            this.pnlBusqueda.Controls.Add(label1);
            this.pnlBusqueda.Location = new System.Drawing.Point(35, 109);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(461, 104);
            this.pnlBusqueda.TabIndex = 125;
            this.pnlBusqueda.TabStop = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(label3);
            this.panel1.Controls.Add(this.radioBtnAmbos);
            this.panel1.Controls.Add(this.radioBtnInactivo);
            this.panel1.Controls.Add(this.radioBtnActivo);
            this.panel1.Location = new System.Drawing.Point(285, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 92);
            this.panel1.TabIndex = 134;
            // 
            // radioBtnAmbos
            // 
            this.radioBtnAmbos.AutoSize = true;
            this.radioBtnAmbos.Checked = true;
            this.radioBtnAmbos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnAmbos.Location = new System.Drawing.Point(32, 66);
            this.radioBtnAmbos.Name = "radioBtnAmbos";
            this.radioBtnAmbos.Size = new System.Drawing.Size(64, 19);
            this.radioBtnAmbos.TabIndex = 141;
            this.radioBtnAmbos.TabStop = true;
            this.radioBtnAmbos.Text = "Ambos";
            this.radioBtnAmbos.UseVisualStyleBackColor = true;
            // 
            // radioBtnInactivo
            // 
            this.radioBtnInactivo.AutoSize = true;
            this.radioBtnInactivo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnInactivo.Location = new System.Drawing.Point(32, 43);
            this.radioBtnInactivo.Name = "radioBtnInactivo";
            this.radioBtnInactivo.Size = new System.Drawing.Size(66, 19);
            this.radioBtnInactivo.TabIndex = 140;
            this.radioBtnInactivo.Text = "InActivo";
            this.radioBtnInactivo.UseVisualStyleBackColor = true;
            // 
            // radioBtnActivo
            // 
            this.radioBtnActivo.AutoSize = true;
            this.radioBtnActivo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnActivo.Location = new System.Drawing.Point(32, 20);
            this.radioBtnActivo.Name = "radioBtnActivo";
            this.radioBtnActivo.Size = new System.Drawing.Size(56, 19);
            this.radioBtnActivo.TabIndex = 139;
            this.radioBtnActivo.Text = "Activo";
            this.radioBtnActivo.UseVisualStyleBackColor = true;
            // 
            // radioBtnapaterno
            // 
            this.radioBtnapaterno.AutoSize = true;
            this.radioBtnapaterno.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnapaterno.Location = new System.Drawing.Point(140, 30);
            this.radioBtnapaterno.Name = "radioBtnapaterno";
            this.radioBtnapaterno.Size = new System.Drawing.Size(115, 19);
            this.radioBtnapaterno.TabIndex = 135;
            this.radioBtnapaterno.Text = "Apellido Paterno";
            this.radioBtnapaterno.UseVisualStyleBackColor = true;
            // 
            // radioBtnnempleado
            // 
            this.radioBtnnempleado.AutoSize = true;
            this.radioBtnnempleado.Checked = true;
            this.radioBtnnempleado.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnnempleado.Location = new System.Drawing.Point(28, 30);
            this.radioBtnnempleado.Name = "radioBtnnempleado";
            this.radioBtnnempleado.Size = new System.Drawing.Size(104, 19);
            this.radioBtnnempleado.TabIndex = 134;
            this.radioBtnnempleado.TabStop = true;
            this.radioBtnnempleado.Text = "No. Empleado";
            this.radioBtnnempleado.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.Image = global::SIPAA_CS.Properties.Resources.Buscar;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBuscar.Location = new System.Drawing.Point(402, 39);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(55, 55);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Tag = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel5.Location = new System.Drawing.Point(29, 92);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(250, 1);
            this.panel5.TabIndex = 43;
            // 
            // txtEmpleado
            // 
            this.txtEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmpleado.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpleado.Location = new System.Drawing.Point(30, 75);
            this.txtEmpleado.Name = "txtEmpleado";
            this.txtEmpleado.Size = new System.Drawing.Size(250, 15);
            this.txtEmpleado.TabIndex = 1;
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.dgvEmpleados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmpleados.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvEmpleados.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dgvEmpleados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmpleados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmpleados.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvEmpleados.Location = new System.Drawing.Point(31, 237);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvEmpleados.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            this.dgvEmpleados.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.Size = new System.Drawing.Size(966, 501);
            this.dgvEmpleados.TabIndex = 132;
            this.dgvEmpleados.TabStop = false;
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.Transparent;
            this.lblusuario.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblusuario.Location = new System.Drawing.Point(9, 73);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(52, 20);
            this.lblusuario.TabIndex = 130;
            this.lblusuario.Text = "usuario";
            this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Image = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.Location = new System.Drawing.Point(911, 1);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(30, 24);
            this.btnRegresar.TabIndex = 129;
            this.btnRegresar.TabStop = false;
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
            this.btnMinimizar.Location = new System.Drawing.Point(973, 1);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 128;
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
            this.btnCerrar.Location = new System.Drawing.Point(999, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 127;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lbltitulo
            // 
            this.lbltitulo.AutoSize = true;
            this.lbltitulo.BackColor = System.Drawing.Color.Transparent;
            this.lbltitulo.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitulo.ForeColor = System.Drawing.Color.White;
            this.lbltitulo.Image = global::SIPAA_CS.Properties.Resources.ic_view_carousel_white_24dp;
            this.lbltitulo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltitulo.Location = new System.Drawing.Point(403, 1);
            this.lbltitulo.Name = "lbltitulo";
            this.lbltitulo.Size = new System.Drawing.Size(201, 23);
            this.lbltitulo.TabIndex = 126;
            this.lbltitulo.Text = "      Cátalogo de Empleados";
            this.lbltitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ptbimgusuario
            // 
            this.ptbimgusuario.Image = ((System.Drawing.Image)(resources.GetObject("ptbimgusuario.Image")));
            this.ptbimgusuario.InitialImage = ((System.Drawing.Image)(resources.GetObject("ptbimgusuario.InitialImage")));
            this.ptbimgusuario.Location = new System.Drawing.Point(10, 29);
            this.ptbimgusuario.Name = "ptbimgusuario";
            this.ptbimgusuario.Size = new System.Drawing.Size(43, 41);
            this.ptbimgusuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbimgusuario.TabIndex = 150;
            this.ptbimgusuario.TabStop = false;
            this.ptbimgusuario.Visible = false;
            // 
            // Trabajadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.f8;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.ptbimgusuario);
            this.Controls.Add(label2);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lbltitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Trabajadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Empleados";
            this.Load += new System.EventHandler(this.Empleados_Load);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbimgusuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lbltitulo;
        private System.Windows.Forms.RadioButton radioBtnapaterno;
        private System.Windows.Forms.RadioButton radioBtnnempleado;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioBtnAmbos;
        private System.Windows.Forms.RadioButton radioBtnInactivo;
        private System.Windows.Forms.RadioButton radioBtnActivo;
        private System.Windows.Forms.PictureBox ptbimgusuario;
    }
}