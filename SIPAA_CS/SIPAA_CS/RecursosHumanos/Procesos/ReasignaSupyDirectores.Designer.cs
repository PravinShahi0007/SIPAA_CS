namespace SIPAA_CS.RecursosHumanos.Procesos
{
    partial class ReasignaSupyDirectores
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
            System.Windows.Forms.Label lblFormaPago;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReasignaSupyDirectores));
            this.cbFormaPago = new System.Windows.Forms.ComboBox();
            this.panelTag = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lbltitulo = new System.Windows.Forms.Label();
            this.lblusuario = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pnlimgusuario = new System.Windows.Forms.Panel();
            this.TxtFeIni = new System.Windows.Forms.TextBox();
            this.TxtFeFin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            lblFormaPago = new System.Windows.Forms.Label();
            this.panelTag.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFormaPago
            // 
            lblFormaPago.AutoSize = true;
            lblFormaPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            lblFormaPago.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblFormaPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            lblFormaPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblFormaPago.Location = new System.Drawing.Point(33, 134);
            lblFormaPago.Name = "lblFormaPago";
            lblFormaPago.Size = new System.Drawing.Size(97, 16);
            lblFormaPago.TabIndex = 152;
            lblFormaPago.Text = "Forma de Pago";
            // 
            // cbFormaPago
            // 
            this.cbFormaPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.cbFormaPago.FormattingEnabled = true;
            this.cbFormaPago.Location = new System.Drawing.Point(36, 153);
            this.cbFormaPago.Name = "cbFormaPago";
            this.cbFormaPago.Size = new System.Drawing.Size(114, 21);
            this.cbFormaPago.TabIndex = 151;
            this.cbFormaPago.Text = "Seleccionar...";
            this.cbFormaPago.SelectedIndexChanged += new System.EventHandler(this.cbFormaPago_SelectedIndexChanged);
            this.cbFormaPago.ValueMemberChanged += new System.EventHandler(this.cbFormaPago_SelectedIndexChanged);
            this.cbFormaPago.Click += new System.EventHandler(this.cbFormaPago_Click);
            // 
            // panelTag
            // 
            this.panelTag.Controls.Add(this.lblMensaje);
            this.panelTag.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTag.Location = new System.Drawing.Point(448, 705);
            this.panelTag.Name = "panelTag";
            this.panelTag.Size = new System.Drawing.Size(531, 25);
            this.panelTag.TabIndex = 150;
            this.panelTag.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblMensaje.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.Navy;
            this.lblMensaje.Location = new System.Drawing.Point(10, 3);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(122, 20);
            this.lblMensaje.TabIndex = 26;
            this.lblMensaje.Text = "Barra de mensajes";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbltitulo
            // 
            this.lbltitulo.AutoSize = true;
            this.lbltitulo.BackColor = System.Drawing.Color.Transparent;
            this.lbltitulo.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitulo.ForeColor = System.Drawing.Color.White;
            this.lbltitulo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltitulo.Location = new System.Drawing.Point(345, 6);
            this.lbltitulo.Name = "lbltitulo";
            this.lbltitulo.Size = new System.Drawing.Size(274, 23);
            this.lbltitulo.TabIndex = 146;
            this.lbltitulo.Text = "Reasignación de Supervisor y Director";
            this.lbltitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.Transparent;
            this.lblusuario.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblusuario.Location = new System.Drawing.Point(6, 74);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(124, 23);
            this.lblusuario.TabIndex = 145;
            this.lblusuario.Text = "Nombre Usuario";
            this.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnRegresar.Image = global::SIPAA_CS.Properties.Resources.ic_reply_white_18dp;
            this.btnRegresar.Location = new System.Drawing.Point(877, 7);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(50, 24);
            this.btnRegresar.TabIndex = 149;
            this.btnRegresar.TabStop = false;
            this.btnRegresar.UseVisualStyleBackColor = false;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.BackgroundImage = global::SIPAA_CS.Properties.Resources.ic_remove_white_18dp;
            this.btnMinimizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.btnMinimizar.Location = new System.Drawing.Point(954, 7);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(24, 24);
            this.btnMinimizar.TabIndex = 148;
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
            this.btnCerrar.Location = new System.Drawing.Point(983, 8);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(24, 24);
            this.btnCerrar.TabIndex = 147;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // pnlimgusuario
            // 
            this.pnlimgusuario.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlimgusuario.BackgroundImage")));
            this.pnlimgusuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlimgusuario.Location = new System.Drawing.Point(10, 25);
            this.pnlimgusuario.Name = "pnlimgusuario";
            this.pnlimgusuario.Size = new System.Drawing.Size(37, 41);
            this.pnlimgusuario.TabIndex = 144;
            // 
            // TxtFeIni
            // 
            this.TxtFeIni.Location = new System.Drawing.Point(254, 154);
            this.TxtFeIni.Name = "TxtFeIni";
            this.TxtFeIni.Size = new System.Drawing.Size(110, 20);
            this.TxtFeIni.TabIndex = 153;
            // 
            // TxtFeFin
            // 
            this.TxtFeFin.Location = new System.Drawing.Point(401, 155);
            this.TxtFeFin.Name = "TxtFeFin";
            this.TxtFeFin.Size = new System.Drawing.Size(105, 20);
            this.TxtFeFin.TabIndex = 154;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label1.Location = new System.Drawing.Point(262, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 155;
            this.label1.Text = "Fecha Inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(211)))), ((int)(((byte)(213)))));
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.label2.Location = new System.Drawing.Point(405, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 156;
            this.label2.Text = "Fecha Final";
            // 
            // ReasignaSupyDirectores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SIPAA_CS.Properties.Resources.JSierra;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtFeFin);
            this.Controls.Add(this.TxtFeIni);
            this.Controls.Add(lblFormaPago);
            this.Controls.Add(this.cbFormaPago);
            this.Controls.Add(this.panelTag);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnMinimizar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lbltitulo);
            this.Controls.Add(this.lblusuario);
            this.Controls.Add(this.pnlimgusuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReasignaSupyDirectores";
            this.Text = "ReasignaSupyDirectores";
            this.panelTag.ResumeLayout(false);
            this.panelTag.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFormaPago;
        private System.Windows.Forms.Panel panelTag;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lbltitulo;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Panel pnlimgusuario;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox TxtFeIni;
        private System.Windows.Forms.TextBox TxtFeFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}