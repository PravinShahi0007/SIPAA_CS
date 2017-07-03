namespace SIPAA_CS.RelojChecadorTrabajador
{
    partial class AdministracionUsuariosReloj
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministracionUsuariosReloj));
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblusuario = new System.Windows.Forms.Label();
            this.pnlimgusuario = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.dgvReloj = new System.Windows.Forms.DataGridView();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.cbEstatus = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtidtrab = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panelAccion = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTeclado = new System.Windows.Forms.Button();
            this.btnHuellas = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnDescarga = new System.Windows.Forms.Button();
            this.panelTag = new System.Windows.Forms.Panel();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReloj)).BeginInit();
            this.pnlBusqueda.SuspendLayout();
            this.panelAccion.SuspendLayout();
            this.panelTag.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label2.Location = new System.Drawing.Point(373, 108);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(115, 16);
            label2.TabIndex = 176;
            label2.Text = "Relojes Asignados";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label4.Location = new System.Drawing.Point(39, 89);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(53, 16);
            label4.TabIndex = 53;
            label4.Text = "Nombre";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label5.Location = new System.Drawing.Point(39, 37);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(88, 16);
            label5.TabIndex = 50;
            label5.Text = "No Trabajador";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = System.Drawing.Color.Gray;
            label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label6.Location = new System.Drawing.Point(17, 16);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(133, 17);
            label6.TabIndex = 177;
            label6.Text = "     Filtro Trabajador";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.ForeColor = System.Drawing.Color.Gray;
            label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label7.Location = new System.Drawing.Point(17, 148);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(135, 17);
            label7.TabIndex = 178;
            label7.Text = "     Estatus en Reloj";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.ForeColor = System.Drawing.Color.Gray;
            label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
            label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label10.Location = new System.Drawing.Point(-7, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(87, 17);
            label10.TabIndex = 180;
            label10.Text = "     Acciones";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label11.ForeColor = System.Drawing.Color.Gray;
            label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label11.Location = new System.Drawing.Point(134, 244);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(121, 20);
            label11.TabIndex = 184;
            label11.Text = "Borrar Asignacion";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label12.ForeColor = System.Drawing.Color.Gray;
            label12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label12.Location = new System.Drawing.Point(134, 109);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(133, 20);
            label12.TabIndex = 185;
            label12.Text = "Descargar Registros";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.Gray;
            label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label3.Location = new System.Drawing.Point(146, 307);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(115, 20);
            label3.TabIndex = 187;
            label3.Text = "Desactivar Huella";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.ForeColor = System.Drawing.Color.Gray;
            label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label8.Location = new System.Drawing.Point(157, 38);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(109, 20);
            label8.TabIndex = 189;
            label8.Text = "Asignar Teclado";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.ForeColor = System.Drawing.Color.Gray;
            label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label9.Location = new System.Drawing.Point(105, 174);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(156, 20);
            label9.TabIndex = 191;
            label9.Text = "Sincronizar Información";
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.Transparent;
            this.lblusuario.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblusuario.Location = new System.Drawing.Point(5, 73);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(64, 23);
            this.lblusuario.TabIndex = 133;
            this.lblusuario.Text = "Usuario";
            this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlimgusuario
            // 
            this.pnlimgusuario.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlimgusuario.BackgroundImage")));
            this.pnlimgusuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlimgusuario.Location = new System.Drawing.Point(9, 29);
            this.pnlimgusuario.Name = "pnlimgusuario";
            this.pnlimgusuario.Size = new System.Drawing.Size(37, 41);
            this.pnlimgusuario.TabIndex = 132;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::SIPAA_CS.Properties.Resources.ic_settings_white_18dp;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(402, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 23);
            this.label1.TabIndex = 131;
            this.label1.Text = "      Administración Usuarios Reloj";
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Location = new System.Drawing.Point(912, 0);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(30, 24);
            this.btnRegresar.TabIndex = 130;
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
            this.btnMinimizar.Location = new System.Drawing.Point(971, 1);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 129;
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
            this.btnCerrar.Location = new System.Drawing.Point(995, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 128;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // dgvReloj
            // 
            this.dgvReloj.AllowUserToAddRows = false;
            this.dgvReloj.AllowUserToDeleteRows = false;
            this.dgvReloj.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.dgvReloj.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvReloj.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReloj.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReloj.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dgvReloj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReloj.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReloj.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReloj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvReloj.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvReloj.Location = new System.Drawing.Point(376, 127);
            this.dgvReloj.Name = "dgvReloj";
            this.dgvReloj.ReadOnly = true;
            this.dgvReloj.RowHeadersVisible = false;
            this.dgvReloj.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvReloj.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            this.dgvReloj.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.dgvReloj.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReloj.Size = new System.Drawing.Size(643, 567);
            this.dgvReloj.TabIndex = 173;
            this.dgvReloj.Tag = "Editar";
            this.dgvReloj.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReloj_CellContentClick);
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.pnlBusqueda.Controls.Add(this.cbEstatus);
            this.pnlBusqueda.Controls.Add(label7);
            this.pnlBusqueda.Controls.Add(label6);
            this.pnlBusqueda.Controls.Add(label4);
            this.pnlBusqueda.Controls.Add(label5);
            this.pnlBusqueda.Controls.Add(this.panel2);
            this.pnlBusqueda.Controls.Add(this.txtNombre);
            this.pnlBusqueda.Controls.Add(this.panel3);
            this.pnlBusqueda.Controls.Add(this.txtidtrab);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Location = new System.Drawing.Point(7, 116);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(363, 219);
            this.pnlBusqueda.TabIndex = 175;
            this.pnlBusqueda.TabStop = true;
            // 
            // cbEstatus
            // 
            this.cbEstatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEstatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEstatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbEstatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEstatus.FormattingEnabled = true;
            this.cbEstatus.Items.AddRange(new object[] {
            "Seleccionar",
            "Activo",
            "Desactivado"});
            this.cbEstatus.Location = new System.Drawing.Point(36, 172);
            this.cbEstatus.Name = "cbEstatus";
            this.cbEstatus.Size = new System.Drawing.Size(180, 24);
            this.cbEstatus.TabIndex = 179;
            this.cbEstatus.Text = "Seleccionar";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel2.Location = new System.Drawing.Point(36, 131);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 1);
            this.panel2.TabIndex = 52;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombre.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(36, 111);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(204, 15);
            this.txtNombre.TabIndex = 51;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel3.Location = new System.Drawing.Point(36, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(204, 1);
            this.panel3.TabIndex = 49;
            // 
            // txtidtrab
            // 
            this.txtidtrab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtidtrab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtidtrab.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtidtrab.Location = new System.Drawing.Point(36, 59);
            this.txtidtrab.Name = "txtidtrab";
            this.txtidtrab.Size = new System.Drawing.Size(204, 15);
            this.txtidtrab.TabIndex = 48;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.Image = global::SIPAA_CS.Properties.Resources.Buscar;
            this.btnBuscar.Location = new System.Drawing.Point(301, 59);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(50, 50);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Tag = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // panelAccion
            // 
            this.panelAccion.Controls.Add(this.button1);
            this.panelAccion.Controls.Add(label9);
            this.panelAccion.Controls.Add(this.btnTeclado);
            this.panelAccion.Controls.Add(label8);
            this.panelAccion.Controls.Add(this.btnHuellas);
            this.panelAccion.Controls.Add(label3);
            this.panelAccion.Controls.Add(this.btnBorrar);
            this.panelAccion.Controls.Add(this.btnDescarga);
            this.panelAccion.Controls.Add(label10);
            this.panelAccion.Controls.Add(label12);
            this.panelAccion.Controls.Add(label11);
            this.panelAccion.Location = new System.Drawing.Point(7, 348);
            this.panelAccion.Name = "panelAccion";
            this.panelAccion.Size = new System.Drawing.Size(363, 404);
            this.panelAccion.TabIndex = 180;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.button1.Image = global::SIPAA_CS.Properties.Resources.Sync;
            this.button1.Location = new System.Drawing.Point(267, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 190;
            this.button1.Tag = "Sync";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTeclado
            // 
            this.btnTeclado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnTeclado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeclado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnTeclado.Image = global::SIPAA_CS.Properties.Resources.Pass;
            this.btnTeclado.Location = new System.Drawing.Point(267, 24);
            this.btnTeclado.Name = "btnTeclado";
            this.btnTeclado.Size = new System.Drawing.Size(50, 50);
            this.btnTeclado.TabIndex = 188;
            this.btnTeclado.Tag = "Pass";
            this.btnTeclado.UseVisualStyleBackColor = false;
            this.btnTeclado.Click += new System.EventHandler(this.btnTeclado_Click);
            // 
            // btnHuellas
            // 
            this.btnHuellas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnHuellas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuellas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnHuellas.Image = global::SIPAA_CS.Properties.Resources.Huellalock;
            this.btnHuellas.Location = new System.Drawing.Point(267, 293);
            this.btnHuellas.Name = "btnHuellas";
            this.btnHuellas.Size = new System.Drawing.Size(50, 50);
            this.btnHuellas.TabIndex = 186;
            this.btnHuellas.Tag = "Huella";
            this.btnHuellas.UseVisualStyleBackColor = false;
            this.btnHuellas.Click += new System.EventHandler(this.btnHuellas_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBorrar.Image = global::SIPAA_CS.Properties.Resources.Borrar;
            this.btnBorrar.Location = new System.Drawing.Point(267, 229);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(50, 50);
            this.btnBorrar.TabIndex = 183;
            this.btnBorrar.Tag = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnDescarga
            // 
            this.btnDescarga.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnDescarga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescarga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnDescarga.Image = global::SIPAA_CS.Properties.Resources.Descargar;
            this.btnDescarga.Location = new System.Drawing.Point(267, 94);
            this.btnDescarga.Name = "btnDescarga";
            this.btnDescarga.Size = new System.Drawing.Size(50, 50);
            this.btnDescarga.TabIndex = 182;
            this.btnDescarga.Tag = "Descargar";
            this.btnDescarga.UseVisualStyleBackColor = false;
            this.btnDescarga.Click += new System.EventHandler(this.btnDescarga_Click);
            // 
            // panelTag
            // 
            this.panelTag.Controls.Add(this.lbMensaje);
            this.panelTag.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTag.Location = new System.Drawing.Point(376, 725);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(648, 27);
            this.panelTag.TabIndex = 198;
            this.panelTag.Visible = false;
            // 
            // lbMensaje
            // 
            this.lbMensaje.AutoSize = true;
            this.lbMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lbMensaje.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensaje.ForeColor = System.Drawing.Color.White;
            this.lbMensaje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbMensaje.Location = new System.Drawing.Point(9, 3);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(60, 20);
            this.lbMensaje.TabIndex = 26;
            this.lbMensaje.Tag = "frmAsignar_Perfil";
            this.lbMensaje.Text = "Mensaje";
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AdministracionUsuariosReloj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.f8;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panelTag);
            this.Controls.Add(this.panelAccion);
            this.Controls.Add(label2);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.dgvReloj);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.pnlimgusuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdministracionUsuariosReloj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdministracionUsuariosReloj";
            this.Load += new System.EventHandler(this.AdministracionUsuariosReloj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReloj)).EndInit();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.panelAccion.ResumeLayout(false);
            this.panelAccion.PerformLayout();
            this.panelTag.ResumeLayout(false);
            this.panelTag.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Panel pnlimgusuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvReloj;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtidtrab;
        private System.Windows.Forms.ComboBox cbEstatus;
        private System.Windows.Forms.Panel panelAccion;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnDescarga;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnTeclado;
        private System.Windows.Forms.Button btnHuellas;
        private System.Windows.Forms.Button button1;
    }
}