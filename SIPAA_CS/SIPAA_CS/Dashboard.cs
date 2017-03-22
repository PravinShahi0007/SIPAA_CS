using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIPAA_CS.RecursosHumanos;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Accesos;

namespace SIPAA_CS
{
    public partial class Dashboard : Form
    {
        public Point formPosition;
        public Boolean mouseAction;
        public List<string> ltModulosxUsuario = new List<string>();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void barraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void barraSuperior_MouseDown(object sender, MouseEventArgs e)
        {

            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void barraSuperior_MouseMove(object sender, MouseEventArgs e)
        {

            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {

            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {

            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Usuario objUsuario = new Usuario();
            string idtrab = LoginInfo.IdTrab;
            ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(idtrab);
            Utilerias.DashboardDinamico(PanelMetro, ltModulosxUsuario);
        }

        private void PanelMetro_Paint(object sender, PaintEventArgs e)
        {

        }

        internal void  RecibirIdTrab(string idTab) {

            Usuario objUsuario = new Usuario();

            ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(idTab);

           
        }

        private void btnRecursosh_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            RechDashboard form = new RechDashboard();
            form.Show();
        }

        private void btnAccesos_Click(object sender, EventArgs e)
        {
            AccesosDashboard Ads = new AccesosDashboard();
            Ads.Show();
            //this.Hide();   
        }
    }
}
