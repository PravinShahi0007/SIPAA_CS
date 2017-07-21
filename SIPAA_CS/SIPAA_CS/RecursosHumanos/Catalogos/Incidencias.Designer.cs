namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    partial class Incidencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Incidencias));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.pnlAct = new System.Windows.Forms.Panel();
            this.lblRep = new System.Windows.Forms.Label();
            this.cboRep = new System.Windows.Forms.ComboBox();
            this.lblGen = new System.Windows.Forms.Label();
            this.cboGen = new System.Windows.Forms.ComboBox();
            this.btnActiva = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCapOrd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbEliminar = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCapInc = new System.Windows.Forms.TextBox();
            this.lblAct = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvIncidencia = new System.Windows.Forms.DataGridView();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtFormReg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTag = new System.Windows.Forms.Panel();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.lblModifElim = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.ptbimgusuario = new System.Windows.Forms.PictureBox();
            this.lblusuario = new System.Windows.Forms.Label();
            this.pnlAct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidencia)).BeginInit();
            this.pnlBusqueda.SuspendLayout();
            this.panelTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbimgusuario)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnAgregar.Image = global::SIPAA_CS.Properties.Resources.Agregar;
            this.btnAgregar.Location = new System.Drawing.Point(926, 156);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(55, 55);
            this.btnAgregar.TabIndex = 151;
            this.btnAgregar.Tag = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Visible = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // pnlAct
            // 
            this.pnlAct.Controls.Add(this.lblRep);
            this.pnlAct.Controls.Add(this.cboRep);
            this.pnlAct.Controls.Add(this.lblGen);
            this.pnlAct.Controls.Add(this.cboGen);
            this.pnlAct.Controls.Add(this.btnActiva);
            this.pnlAct.Controls.Add(this.panel1);
            this.pnlAct.Controls.Add(this.txtCapOrd);
            this.pnlAct.Controls.Add(this.label6);
            this.pnlAct.Controls.Add(this.ckbEliminar);
            this.pnlAct.Controls.Add(this.label2);
            this.pnlAct.Controls.Add(this.panel3);
            this.pnlAct.Controls.Add(this.txtCapInc);
            this.pnlAct.Controls.Add(this.lblAct);
            this.pnlAct.Controls.Add(this.btnEliminar);
            this.pnlAct.Controls.Add(this.btnEditar);
            this.pnlAct.Controls.Add(this.btnGuardar);
            this.pnlAct.Location = new System.Drawing.Point(36, 335);
            this.pnlAct.Name = "pnlAct";
            this.pnlAct.Size = new System.Drawing.Size(437, 248);
            this.pnlAct.TabIndex = 147;
            this.pnlAct.TabStop = true;
            this.pnlAct.Visible = false;
            // 
            // lblRep
            // 
            this.lblRep.AutoSize = true;
            this.lblRep.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblRep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRep.Location = new System.Drawing.Point(23, 221);
            this.lblRep.Name = "lblRep";
            this.lblRep.Size = new System.Drawing.Size(103, 16);
            this.lblRep.TabIndex = 60;
            this.lblRep.Text = "Representa una:";
            // 
            // cboRep
            // 
            this.cboRep.FormattingEnabled = true;
            this.cboRep.Location = new System.Drawing.Point(132, 221);
            this.cboRep.Name = "cboRep";
            this.cboRep.Size = new System.Drawing.Size(137, 21);
            this.cboRep.TabIndex = 7;
            // 
            // lblGen
            // 
            this.lblGen.AutoSize = true;
            this.lblGen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblGen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGen.Location = new System.Drawing.Point(22, 165);
            this.lblGen.Name = "lblGen";
            this.lblGen.Size = new System.Drawing.Size(90, 16);
            this.lblGen.TabIndex = 59;
            this.lblGen.Text = "Generada por:";
            // 
            // cboGen
            // 
            this.cboGen.FormattingEnabled = true;
            this.cboGen.Location = new System.Drawing.Point(131, 165);
            this.cboGen.Name = "cboGen";
            this.cboGen.Size = new System.Drawing.Size(137, 21);
            this.cboGen.TabIndex = 6;
            // 
            // btnActiva
            // 
            this.btnActiva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnActiva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActiva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnActiva.Image = global::SIPAA_CS.Properties.Resources.Alta;
            this.btnActiva.Location = new System.Drawing.Point(362, 19);
            this.btnActiva.Name = "btnActiva";
            this.btnActiva.Size = new System.Drawing.Size(55, 55);
            this.btnActiva.TabIndex = 8;
            this.btnActiva.Tag = "Alta";
            this.btnActiva.UseVisualStyleBackColor = false;
            this.btnActiva.Visible = false;
            this.btnActiva.Click += new System.EventHandler(this.btnActiva_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel1.Location = new System.Drawing.Point(24, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 1);
            this.panel1.TabIndex = 44;
            // 
            // txtCapOrd
            // 
            this.txtCapOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtCapOrd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCapOrd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCapOrd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapOrd.Location = new System.Drawing.Point(26, 109);
            this.txtCapOrd.Name = "txtCapOrd";
            this.txtCapOrd.Size = new System.Drawing.Size(250, 15);
            this.txtCapOrd.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(21, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 16);
            this.label6.TabIndex = 57;
            this.label6.Text = "Orden";
            // 
            // ckbEliminar
            // 
            this.ckbEliminar.AutoSize = true;
            this.ckbEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ckbEliminar.Location = new System.Drawing.Point(282, 49);
            this.ckbEliminar.Name = "ckbEliminar";
            this.ckbEliminar.Size = new System.Drawing.Size(47, 17);
            this.ckbEliminar.TabIndex = 55;
            this.ckbEliminar.Text = "Baja";
            this.ckbEliminar.UseVisualStyleBackColor = true;
            this.ckbEliminar.Visible = false;
            this.ckbEliminar.CheckedChanged += new System.EventHandler(this.ckbEliminar_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(22, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "Descripción";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel3.Location = new System.Drawing.Point(24, 67);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 1);
            this.panel3.TabIndex = 43;
            // 
            // txtCapInc
            // 
            this.txtCapInc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtCapInc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCapInc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCapInc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapInc.Location = new System.Drawing.Point(26, 50);
            this.txtCapInc.Name = "txtCapInc";
            this.txtCapInc.Size = new System.Drawing.Size(250, 15);
            this.txtCapInc.TabIndex = 4;
            // 
            // lblAct
            // 
            this.lblAct.AutoSize = true;
            this.lblAct.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAct.ForeColor = System.Drawing.Color.Gray;
            this.lblAct.Image = ((System.Drawing.Image)(resources.GetObject("lblAct.Image")));
            this.lblAct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAct.Location = new System.Drawing.Point(4, 4);
            this.lblAct.Name = "lblAct";
            this.lblAct.Size = new System.Drawing.Size(147, 17);
            this.lblAct.TabIndex = 41;
            this.lblAct.Text = "     Agregar Incidencia";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnEliminar.Image = global::SIPAA_CS.Properties.Resources.Baja;
            this.btnEliminar.Location = new System.Drawing.Point(362, 19);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(50, 50);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Tag = "Baja";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Visible = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnEditar.Image = global::SIPAA_CS.Properties.Resources.Editar;
            this.btnEditar.Location = new System.Drawing.Point(362, 19);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(50, 50);
            this.btnEditar.TabIndex = 8;
            this.btnEditar.Tag = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Visible = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnGuardar.Image = global::SIPAA_CS.Properties.Resources.Guardar;
            this.btnGuardar.Location = new System.Drawing.Point(362, 19);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(50, 50);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Tag = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // dgvIncidencia
            // 
            this.dgvIncidencia.AllowUserToAddRows = false;
            this.dgvIncidencia.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.dgvIncidencia.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIncidencia.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvIncidencia.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dgvIncidencia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIncidencia.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncidencia.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvIncidencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncidencia.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvIncidencia.Location = new System.Drawing.Point(488, 212);
            this.dgvIncidencia.Name = "dgvIncidencia";
            this.dgvIncidencia.ReadOnly = true;
            this.dgvIncidencia.RowHeadersVisible = false;
            this.dgvIncidencia.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvIncidencia.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            this.dgvIncidencia.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.dgvIncidencia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncidencia.Size = new System.Drawing.Size(488, 345);
            this.dgvIncidencia.TabIndex = 148;
            this.dgvIncidencia.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIncidencia_CellContentClick_1);
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.label4);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            this.pnlBusqueda.Controls.Add(this.panel5);
            this.pnlBusqueda.Controls.Add(this.txtFormReg);
            this.pnlBusqueda.Controls.Add(this.label1);
            this.pnlBusqueda.Location = new System.Drawing.Point(36, 212);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(437, 87);
            this.pnlBusqueda.TabIndex = 146;
            this.pnlBusqueda.TabStop = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(24, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 44;
            this.label4.Text = "Descripción";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.btnBuscar.Image = global::SIPAA_CS.Properties.Resources.Buscar;
            this.btnBuscar.Location = new System.Drawing.Point(362, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(55, 55);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Tag = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click_1);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.panel5.Location = new System.Drawing.Point(27, 71);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(250, 1);
            this.panel5.TabIndex = 43;
            // 
            // txtFormReg
            // 
            this.txtFormReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.txtFormReg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFormReg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormReg.Location = new System.Drawing.Point(29, 54);
            this.txtFormReg.Name = "txtFormReg";
            this.txtFormReg.Size = new System.Drawing.Size(250, 15);
            this.txtFormReg.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 41;
            this.label1.Text = "     Buscar Incidencia";
            // 
            // panelTag
            // 
            this.panelTag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panelTag.Controls.Add(this.lbMensaje);
            this.panelTag.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTag.ForeColor = System.Drawing.Color.White;
            this.panelTag.Location = new System.Drawing.Point(488, 560);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(438, 25);
            this.panelTag.TabIndex = 153;
            this.panelTag.Visible = false;
            // 
            // lbMensaje
            // 
            this.lbMensaje.AutoSize = true;
            this.lbMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lbMensaje.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMensaje.ForeColor = System.Drawing.Color.Navy;
            this.lbMensaje.Location = new System.Drawing.Point(10, 3);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(181, 20);
            this.lbMensaje.TabIndex = 26;
            this.lbMensaje.Text = "Administración de Perfiles    ";
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModifElim
            // 
            this.lblModifElim.AutoSize = true;
            this.lblModifElim.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModifElim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblModifElim.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblModifElim.Location = new System.Drawing.Point(485, 560);
            this.lblModifElim.Name = "lblModifElim";
            this.lblModifElim.Size = new System.Drawing.Size(93, 16);
            this.lblModifElim.TabIndex = 152;
            this.lblModifElim.Text = "Seleccion Grid";
            this.lblModifElim.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Image = global::SIPAA_CS.Properties.Resources.ic_view_carousel_white_24dp;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(447, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 23);
            this.label3.TabIndex = 157;
            this.label3.Text = "      Cátalogo de Incidencias";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Location = new System.Drawing.Point(908, 1);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(30, 24);
            this.btnRegresar.TabIndex = 156;
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
            this.btnMinimizar.Location = new System.Drawing.Point(975, 1);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 155;
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
            this.btnCerrar.Location = new System.Drawing.Point(1000, 1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 154;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // ptbimgusuario
            // 
            this.ptbimgusuario.Image = ((System.Drawing.Image)(resources.GetObject("ptbimgusuario.Image")));
            this.ptbimgusuario.InitialImage = ((System.Drawing.Image)(resources.GetObject("ptbimgusuario.InitialImage")));
            this.ptbimgusuario.Location = new System.Drawing.Point(10, 29);
            this.ptbimgusuario.Name = "ptbimgusuario";
            this.ptbimgusuario.Size = new System.Drawing.Size(43, 41);
            this.ptbimgusuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbimgusuario.TabIndex = 159;
            this.ptbimgusuario.TabStop = false;
            this.ptbimgusuario.Visible = false;
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
            this.lblusuario.Size = new System.Drawing.Size(56, 20);
            this.lblusuario.TabIndex = 158;
            this.lblusuario.Text = "usuario ";
            this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Incidencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.f8;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.ptbimgusuario);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.panelTag);
            this.Controls.Add(this.lblModifElim);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.pnlAct);
            this.Controls.Add(this.dgvIncidencia);
            this.Controls.Add(this.pnlBusqueda);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Incidencias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Incidencias";
            this.Load += new System.EventHandler(this.Incidencias_Load);
            this.pnlAct.ResumeLayout(false);
            this.pnlAct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidencia)).EndInit();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.panelTag.ResumeLayout(false);
            this.panelTag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbimgusuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Panel pnlAct;
        private System.Windows.Forms.CheckBox ckbEliminar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCapInc;
        private System.Windows.Forms.Label lblAct;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.DataGridView dgvIncidencia;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtFormReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Label lblModifElim;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtCapOrd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnActiva;
        private System.Windows.Forms.ComboBox cboGen;
        private System.Windows.Forms.Label lblRep;
        private System.Windows.Forms.ComboBox cboRep;
        private System.Windows.Forms.Label lblGen;
        private System.Windows.Forms.PictureBox ptbimgusuario;
        private System.Windows.Forms.Label lblusuario;
    }
}