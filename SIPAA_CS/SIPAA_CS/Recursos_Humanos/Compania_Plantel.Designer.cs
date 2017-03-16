namespace SIPAA_CS.Recursos_Humanos
{
    partial class Compania_Plantel
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
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compania_Plantel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PanelPlantilla = new System.Windows.Forms.Panel();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.dgvPlantel = new System.Windows.Forms.DataGridView();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.cbCia = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtBuscarPerfil = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            this.PanelPlantilla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlantel)).BeginInit();
            this.pnlBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Enabled = false;
            label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label6.Location = new System.Drawing.Point(35, 73);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(56, 16);
            label6.TabIndex = 58;
            label6.Text = "Compañia";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label8.Location = new System.Drawing.Point(35, 21);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(48, 16);
            label8.TabIndex = 44;
            label8.Text = "Plantel";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.ForeColor = System.Drawing.Color.Gray;
            label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label9.Location = new System.Drawing.Point(5, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(123, 17);
            label9.TabIndex = 41;
            label9.Text = "     Buscar Plantel";
            // 
            // PanelPlantilla
            // 
            this.PanelPlantilla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.PanelPlantilla.BackgroundImage = global::SIPAA_CS.Properties.Resources.JSierra;
            this.PanelPlantilla.Controls.Add(this.btnRegresar);
            this.PanelPlantilla.Controls.Add(this.btnMinimizar);
            this.PanelPlantilla.Controls.Add(this.btnCerrar);
            this.PanelPlantilla.Controls.Add(this.dgvPlantel);
            this.PanelPlantilla.Controls.Add(this.pnlBusqueda);
            this.PanelPlantilla.Controls.Add(this.label3);
            this.PanelPlantilla.Location = new System.Drawing.Point(-2, -6);
            this.PanelPlantilla.Name = "PanelPlantilla";
            this.PanelPlantilla.Size = new System.Drawing.Size(1029, 775);
            this.PanelPlantilla.TabIndex = 51;
            this.PanelPlantilla.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPlantilla_Paint_1);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Image = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.Location = new System.Drawing.Point(913, 6);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(50, 24);
            this.btnRegresar.TabIndex = 106;
            this.btnRegresar.UseVisualStyleBackColor = false;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_remove_white_18dp;
            this.btnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.Location = new System.Drawing.Point(963, 7);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 105;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_clear_white_18dp;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCerrar.Location = new System.Drawing.Point(987, 7);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 104;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // dgvPlantel
            // 
            this.dgvPlantel.AllowUserToAddRows = false;
            this.dgvPlantel.AllowUserToDeleteRows = false;
            this.dgvPlantel.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.dgvPlantel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPlantel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlantel.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPlantel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dgvPlantel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlantel.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPlantel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPlantel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPlantel.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPlantel.Location = new System.Drawing.Point(589, 205);
            this.dgvPlantel.Name = "dgvPlantel";
            this.dgvPlantel.ReadOnly = true;
            this.dgvPlantel.RowHeadersVisible = false;
            this.dgvPlantel.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPlantel.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            this.dgvPlantel.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.dgvPlantel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlantel.Size = new System.Drawing.Size(361, 345);
            this.dgvPlantel.TabIndex = 61;
            this.dgvPlantel.Tag = "Editar";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(label6);
            this.pnlBusqueda.Controls.Add(this.cbCia);
            this.pnlBusqueda.Controls.Add(label8);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Controls.Add(this.panel7);
            this.pnlBusqueda.Controls.Add(this.txtBuscarPerfil);
            this.pnlBusqueda.Controls.Add(label9);
            this.pnlBusqueda.Location = new System.Drawing.Point(98, 205);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(437, 131);
            this.pnlBusqueda.TabIndex = 59;
            this.pnlBusqueda.TabStop = true;
            // 
            // cbCia
            // 
            this.cbCia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbCia.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCia.FormattingEnabled = true;
            this.cbCia.Location = new System.Drawing.Point(38, 92);
            this.cbCia.Name = "cbCia";
            this.cbCia.Size = new System.Drawing.Size(280, 25);
            this.cbCia.TabIndex = 57;
            this.cbCia.Text = "Seleccionar";
            this.cbCia.SelectedIndexChanged += new System.EventHandler(this.cbCia_SelectedIndexChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.Image = global::SIPAA_CS.Properties.Resources.btnSearch;
            this.btnBuscar.Location = new System.Drawing.Point(363, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(50, 50);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel7.Location = new System.Drawing.Point(32, 61);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(289, 3);
            this.panel7.TabIndex = 43;
            // 
            // txtBuscarPerfil
            // 
            this.txtBuscarPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtBuscarPerfil.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuscarPerfil.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarPerfil.Location = new System.Drawing.Point(32, 43);
            this.txtBuscarPerfil.Name = "txtBuscarPerfil";
            this.txtBuscarPerfil.Size = new System.Drawing.Size(286, 15);
            this.txtBuscarPerfil.TabIndex = 1;
            this.txtBuscarPerfil.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarPerfil_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::SIPAA_CS.Properties.Resources.ic_settings_white_18dp;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(493, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "       Áreas  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Compania_Plantel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.PanelPlantilla);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Compania_Plantel";
            this.Text = "Compania_Plantel";
            this.Load += new System.EventHandler(this.Compania_Plantel_Load);
            this.PanelPlantilla.ResumeLayout(false);
            this.PanelPlantilla.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlantel)).EndInit();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelPlantilla;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvPlantel;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.ComboBox cbCia;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtBuscarPerfil;
        private System.Windows.Forms.Label label3;
    }
}