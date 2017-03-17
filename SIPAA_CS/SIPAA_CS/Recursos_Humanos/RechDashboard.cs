using SIPAA_CS.Recursos_Humanos.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.Recursos_Humanos.App_Code.Usuario;

namespace SIPAA_CS.Recursos_Humanos
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
            this.Enabled = false;
            
        }

        private void diasFestivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
