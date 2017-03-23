using SIPAA_CS.App_Code;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace SIPAA_CS.Accesos
{
    public partial class Modulos : Form
    {
        public Point formPosition;
        public Boolean mouseAction;

        public int variable= 0;

        Utilerias u = new Utilerias();

        Conexion c = new Conexion();
        public Modulos()
        {
            InitializeComponent();
        }



        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: Formulario de crear Modulo
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvModulo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cvmodulo, descripcion, cvmodpad, ambiente, modulo, usuumod, prgumod;
            int orden;

            for (int iContador = 0; iContador < dgvModulo.Rows.Count; iContador++)
            {
                dgvModulo.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvModulo.SelectedRows.Count != 0)
            {


                DataGridViewRow row = this.dgvModulo.SelectedRows[0];

                cvmodulo = row.Cells["CVMODULO"].Value.ToString();
                descripcion = row.Cells["DESCRIPCION"].Value.ToString();
                cvmodpad = row.Cells["CVMODPAD"].Value.ToString();
                orden = Convert.ToInt32(row.Cells["ORDEN"].Value.ToString());
                ambiente = row.Cells["AMBIENTE"].Value.ToString();
                modulo = row.Cells["MODULO"].Value.ToString();
                usuumod = row.Cells["USUUMOD"].Value.ToString();
                prgumod = row.Cells["PRGUMOD"].Value.ToString();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                //cajas de texto panel actualizar
                txtCvModulo.Text = cvmodulo;
                txtDescripcion.Text = descripcion;
                txtCvModPad.Text = cvmodpad;
                txtOrden.Text = Convert.ToString(orden);
                cbAmbiente.Text = ambiente;
                cbModulo.Text = modulo;
                txtPrgUmod.Text = prgumod;

                btnEliminar.Enabled = true;
                btnEliminar.BackColor = Color.FromArgb(244, 67, 54);
                btnEliminar.ForeColor = Color.FromArgb(244, 67, 54);

                btnEditar.Enabled = true;
                btnEditar.BackColor = Color.FromArgb(2, 119, 189);
                btnEditar.ForeColor = Color.FromArgb(2, 119, 189);


                //txtCvModulo.Enabled = true;
                //txtDescripcion.Enabled = true;
                //txtCvModPad.Enabled = true;
                //txtOrden.Enabled = true;
                //cbAmbiente.Enabled = true;
                //cbModulo.Enabled = true;
                //txtPrgUmod.Enabled = true;

                txtCvModulo.Focus();

                //btnGuardar.Enabled = true;
                //btnGuardar.BackColor = Color.FromArgb(2, 119, 189);
                //btnGuardar.ForeColor = Color.FromArgb(2, 119, 189);

                //btnAgregar.Enabled = false;
                //btnAgregar.BackColor = Color.FromArgb(97, 97, 97);
                //btnAgregar.ForeColor = Color.FromArgb(97, 97, 97);

                btnGuardar.Enabled = false;
                btnGuardar.BackColor = Color.FromArgb(97, 97, 97);
                btnGuardar.ForeColor = Color.FromArgb(97, 97, 97);
                
            }

        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnAgregar_Click(object sender, EventArgs e)
        {

            variable = 1;


            txtCvModulo.Text = "";
            txtDescripcion.Text = "";
            txtCvModPad.Text = "";
            txtOrden.Text = "";
            cbAmbiente.Text = "SELECCIONA UNA AMBIENTE";
            cbModulo.Text = "SELECCIONA UN MÓDULO";
            txtPrgUmod.Text = "";


            //Activa campos 
            txtCvModulo.Enabled = true;
            txtDescripcion.Enabled = true;
            txtCvModPad.Enabled = true;
            txtOrden.Enabled = true;
            txtPrgUmod.Enabled = true;

            //Activa combobox
            cbAmbiente.Enabled = true;
            cbModulo.Enabled = true;

            //Habilita boton guardar
            btnGuardar.Enabled = true;
            btnGuardar.BackColor = Color.FromArgb(46, 125, 50);


            txtCvModulo.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cvmodulo;

            cvmodulo = txtBuscar.Text;

            dgvModulo.Visible = true;

            dgvModulo.DataSource = c.buscarModulo(cvmodulo);

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "SELECCIONAR";
            dgvModulo.Columns.Insert(0, imgCheckPerfiles);
            ImageList imglt = new ImageList();
            dgvModulo.Columns[1].Visible = false;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            variable = 2;

            txtCvModulo.Enabled = true;
            txtDescripcion.Enabled = true;
            txtCvModPad.Enabled = true;
            txtOrden.Enabled = true;
            cbAmbiente.Enabled = true;
            cbModulo.Enabled = true;
            txtPrgUmod.Enabled = true;

            btnGuardar.Enabled = true;
            btnGuardar.BackColor = Color.FromArgb(2, 119, 189);
            btnGuardar.ForeColor = Color.FromArgb(2, 119, 189);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cvmodulo;

            btnGuardar.Enabled = false;
            btnGuardar.BackColor = Color.FromArgb(97, 97, 97);
            btnGuardar.ForeColor = Color.FromArgb(97, 97, 97);



            cvmodulo = txtCvModulo.Text;

            c.eliminarCatalogo(cvmodulo);

            //CrearModulo_Load(sender, e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (variable == 1)
            {
                //Se instancia la clase conexion

                string cvmodulo, descripcion, cvmodpad, ambiente, modulo, usuumod, prgumod, fhumod, fecha, hora, fecha_hora;
                DateTime fhumod1, fh;
                int orden;

                // Se asginan valores de los componentes
                cvmodulo = txtCvModulo.Text;
                descripcion = txtDescripcion.Text;
                cvmodpad = txtCvModPad.Text;
                ambiente = cbAmbiente.Text;
                modulo = cbModulo.Text;
                prgumod = this.Name;
                usuumod = "140014";

                orden = int.Parse(txtOrden.Text);

                //se arma la fecha 
                fecha = DateTime.Now.ToShortDateString();
                hora = DateTime.Now.ToLongTimeString();

                fecha_hora = fecha + " " + hora;

                fh = DateTime.Parse(fecha_hora);
                //MessageBox.Show(fecha_hora);

                // pasamos parametros a la funcion
                c.crearModulo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, usuumod, fh, prgumod);

                DataTable tabla = c.mostrarModulo(cvmodulo);

                dgvModulo.DataSource = tabla;

                dgvModulo.Visible = true;

                txtCvModulo.Text = "";
                txtDescripcion.Text = "";
                txtCvModPad.Text = "";
                txtOrden.Text = "";
                cbAmbiente.Text = "";
                cbModulo.Text = "";
                cbAmbiente.Text = "SELECCIONA UNA AMBIENTE";
                cbModulo.Text = "SELECCIONA UN MÓDULO";
                txtPrgUmod.Text = "";


            }
            if (variable == 2)
            {
                string cvmodulo, descripcion, cvmodpad, ambiente, modulo, usuumod, prgumod, fecha, hora, fecha_hora;
                int orden;
                DateTime fh;

                cvmodulo = txtCvModulo.Text;
                descripcion = txtDescripcion.Text;
                cvmodpad = txtCvModPad.Text;
                orden = int.Parse(txtOrden.Text);
                ambiente = cbAmbiente.Text;
                modulo = cbModulo.Text;
                usuumod = "140014";
                prgumod = txtPrgUmod.Text;

                fecha = DateTime.Now.ToShortDateString();
                hora = DateTime.Now.ToLongTimeString();

                fecha_hora = fecha + " " + hora;

                fh = DateTime.Parse(fecha_hora);

                c.actualizarCatalogo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, usuumod, fh, prgumod);

                DataTable tabla = c.mostrarModulo(cvmodulo);

                dgvModulo.DataSource = tabla;

                dgvModulo.Visible = true;
            }

        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Crear_Modulo_Load(object sender, EventArgs e)
        {
            //btnAgregar.Enabled = false;
            txtCvModulo.Enabled = false;
            txtDescripcion.Enabled = false;
            txtCvModPad.Enabled = false;
            txtOrden.Enabled = false;
            txtPrgUmod.Enabled = false;

            cbAmbiente.Enabled = false;
            cbModulo.Enabled = false;

            //btnGuardar.Hide();

            btnGuardar.Enabled = false;
            btnGuardar.BackColor = Color.FromArgb(97, 97, 97);

            btnEliminar.Enabled = false;
            btnEliminar.BackColor = Color.FromArgb(97, 97, 97);
            btnEliminar.ForeColor = Color.FromArgb(97, 97, 97);

            btnEditar.Enabled = false;
            btnEditar.BackColor = Color.FromArgb(97, 97, 97);
            btnEditar.ForeColor = Color.FromArgb(97, 97, 97);


        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnRegresa_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
