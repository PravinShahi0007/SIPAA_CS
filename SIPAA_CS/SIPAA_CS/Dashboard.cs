using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIPAA_CS.RecursosHumanos;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Accesos;
using System.Data;

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
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "SIPAA", MessageBoxButtons.YesNoCancel);

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
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, "RECH");
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            

            Usuario objUsuario = new Usuario();
            string idtrab = LoginInfo.IdTrab;
            ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(idtrab,6);
            Utilerias.DashboardDinamico(PanelMetro, ltModulosxUsuario);
            //LoginInfo.Nombre = lblusuario.Text;
            string NomUsu = LoginInfo.Nombre;
            lblusuario.Text = NomUsu;
        }

        private void PanelMetro_Paint(object sender, PaintEventArgs e)
        {

        }

      

        private void btnRecursosh_Click(object sender, EventArgs e)
        {
            
            RechDashboard form = new RechDashboard();
            form.Show();
        }

        private void btnAccesos_Click(object sender, EventArgs e)
        {
            AcceDashboard Ads = new AcceDashboard();
            Ads.Show();
            //this.Hide();   
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {

        }
    }
}
