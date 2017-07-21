using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIPAA_CS.RecursosHumanos;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Accesos;
using System.Data;

//***********************************************************************************************
//Autor: ------------------       modifico: noe alvarez marquina (se agrega estandar)
//Fecha creación:dd-mm-aaaa       Última Modificacion: 17/07/2017
//Descripción: -------------------------------
//***********************************************************************************************

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
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

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
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNoCancel);

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
            //inicia tool tip
            ftooltip();

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
            lblusuario.Text = LoginInfo.Nombre;
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
            this.Close();
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

        private void btnPower_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Esta seguro de cerrar sesión?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                LoginInfo.IdTrab = String.Empty;
                Acceso frm = new Acceso();
                this.Hide();
                frm.Show();
            }


        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btnCerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnPower, "Cerara Sesión");
        }
    }
}
