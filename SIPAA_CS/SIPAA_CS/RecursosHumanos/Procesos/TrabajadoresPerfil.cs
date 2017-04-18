using SIPAA_CS.App_Code;
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
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class TrabajadoresPerfil : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public TrabajadoresPerfil()
        {
            InitializeComponent();
        }

        private void TrabajadoresPerfil_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dtTrab = objTrab.ObtenerPerfilTrabajador("%",6, "%", "%",0, LoginInfo.IdTrab, this.Name);
            //   llenarListView(dtTrab, ltvTrabajador);
            llenarGrid(dtTrab, dgvTrab);
        }

        private void dgvIncidencia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvTrab.Rows.Count; iContador++)
            {
                dgvTrab.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvTrab.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvTrab.SelectedRows[0];

                //CVPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                //string Desc = row.Cells["DESCRIPCION"].Value.ToString();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                TrabajadorInfo.IdTrab = row.Cells["idtrab"].Value.ToString();
                DatosTrabajadorPerfil form = new DatosTrabajadorPerfil();
                form.Show();
            }
        }


        public void llenarListView(DataTable dt,ListView lt) {

            string sIdActual= "";
            foreach (DataRow row in dt.Rows)
            {
                if (row["idtrab"].ToString() != sIdActual) {

                    sIdActual = row["idtrab"].ToString();
                    string[] row1 = { row["idtrab"].ToString(), row["Nombre"].ToString(), row["Compañia"].ToString(), row["Ubicación"].ToString()
                                      ,row["Área"].ToString(),row["Estatus"].ToString()};

                    ListViewItem ltitems = new ListViewItem(row1);
                    // lt.Items.Add(ltitems);


                    string[] row2 = { row["Depto"].ToString(), row["Puesto"].ToString(), row["Tipo_Nomina"].ToString(), row["Fecha_Ingreso"].ToString() };

                    ListViewItem listitems2 = new ListViewItem(row2);
                    lt.Items.Add(listitems2);
                }
               


               


            }

        }
        public void llenarGrid(DataTable dt,DataGridView dgv) {

            if (dgv.Columns.Count > 0) {
                dgv.Columns.RemoveAt(0);
            }
                
                

            dgv.DataSource = dt;

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "Seleccionar";
            //imgCheckPerfiles.HeaderText = "";
            dgv.Columns.Insert(0, imgCheckPerfiles);


            
           

        }

        private void pnlBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string sIdbusqueda= "";
            if (txtdtrab.Text != String.Empty)
            {
                sIdbusqueda = txtdtrab.Text;
            }
            else {
                sIdbusqueda = "%";
            }

            string sCheca= "";
            if (cbCheca.SelectedIndex == 0)
            {
                sCheca = "%";
            }
            else if(cbCheca.SelectedIndex == 2) {

                sCheca = "0";
            }
            else {
                sCheca = "1";
            }
            string sEstatus = "";
            if (cbEstatus.SelectedIndex == 0)
            {
                sEstatus = "%";
            }
            else if (cbEstatus.SelectedIndex == 2)
            {

                sEstatus = "0";
            }
            else
            {
                sEstatus = "1";
            }

            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(sIdbusqueda, 6, sCheca, sEstatus, 0,LoginInfo.IdTrab, this.Name);
            llenarGrid(dtTrab, dgvTrab);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
