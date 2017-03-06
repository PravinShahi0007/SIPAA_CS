using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SIPAA_CS.Recursos_Humanos
{
    public partial class Crear_Modulo : Form
    {
        public Point formPosition;
        public Boolean mouseAction;

        Conexion c = new Conexion();
        public Crear_Modulo()
        {
            InitializeComponent();
        }

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
            btnEliminar.BackColor = Color.FromArgb(97,97,97);
            btnEliminar.ForeColor = Color.FromArgb(97, 97, 97);

            btnEditar.Enabled = false;
            btnEditar.BackColor = Color.FromArgb(97, 97, 97);
            btnEditar.ForeColor = Color.FromArgb(97, 97, 97);


        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
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
                txtCvModulo.Text = modulo;
                txtPrgUmod.Text = prgumod;
                
            }

        }
    }
}
