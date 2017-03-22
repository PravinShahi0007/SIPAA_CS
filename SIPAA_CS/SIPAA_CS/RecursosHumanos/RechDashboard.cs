using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos
{
    public partial class RechDashboard : Form
    {
        public RechDashboard()
        {
            InitializeComponent();
        }

        private void RechDashboard_Load(object sender, EventArgs e)
        {
            Dashboard form = new Dashboard();
            form.Enabled = false;

            Usuario objUsuario = new Usuario();
            string IdTrab = LoginInfo.IdTrab;
            List<string> ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(IdTrab);

            Utilerias.MenuDinamico(MenuAccesos, ltModulosxUsuario);
        }


        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard form = new Dashboard();
            form.Enabled = true;
            form.Show();
            this.Close();
        }

        private void tsmiCompanias_Click(object sender, EventArgs e)
        {
            Companias form = new Companias();
            form.Show();
           
            
        }

        private void diasFestivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiasFestivos form = new DiasFestivos();
            form.Show();
        }

        private void tsmiDepartamentos_Click(object sender, EventArgs e)
        {
            Departamentos form = new Departamentos();
            form.Show();
        }

        private void tsmiUbicacion_Click(object sender, EventArgs e)
        {
            Ubicaciones form = new Ubicaciones();
            form.Show();
        }

        private void tsmiPuestos_Click(object sender, EventArgs e)
        {
            Puestos form = new Puestos();
            form.Show();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trabajadores form = new Trabajadores();
            form.Show();
        }

        private void formasDeRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormasRegistros form = new FormasRegistros();
            form.Show();
        }

        private void incapacidadRepresentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncidenciasRepresentan form = new IncidenciasRepresentan();
            form.Show();
        }

        private void mensajesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mensajes form = new Mensajes();
            form.Show();
        }

        private void areasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Areas form = new Areas();
            form.Show();
        }
    }
}
