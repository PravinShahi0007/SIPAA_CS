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

namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    public partial class AccesosDashboard : Form
    {
        public AccesosDashboard()
        {
            InitializeComponent();
        }

        private void asginaciònDeProcesosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AccesosDashboard_Load(object sender, EventArgs e)
        {
            Usuario objUsuario = new Usuario();
            List<string> ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario("140414");

            Utilerias.MenuDinamico(MenuAccesos, ltModulosxUsuario);
        }
    }
}
