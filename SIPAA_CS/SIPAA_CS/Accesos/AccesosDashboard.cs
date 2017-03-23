using SIPAA_CS.Accesos;
using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.Accesos
{

    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017       Última Modificacion: 13-03-2017 
    //Descripción: Pantalla de Inicio de SIPAA
    //***********************************************************************************************
    public partial class AccesosDashboard : Form
    {

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void tsmiUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios cau = new Usuarios();
            cau.Show();
        }

        private void tsmiPerfiles_Click(object sender, EventArgs e)
        {
            Perfiles cp = new Perfiles();
            cp.Show();
        }

        private void tsmiModulos_Click(object sender, EventArgs e)
        {
            Modulos cm = new Modulos();
            cm.Show();
        }

        private void tsmiProcesos_Click(object sender, EventArgs e)
        {
            Procesos cp = new Procesos();
            cp.Show();
        }

        private void msAsignacionPerfil_Click(object sender, EventArgs e)
        {
            Perfiles ap = new Perfiles();
            ap.Show();
        }

        private void msAsignacionModulo_Click(object sender, EventArgs e)
        {
            PerfilModulo am = new PerfilModulo();
            am.Show();
        }

        private void msAsignacionProceso_Click(object sender, EventArgs e)
        {
            UsuarioProceso aproc = new UsuarioProceso();
            aproc.Show();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public AccesosDashboard()
        {
            InitializeComponent();
        }


        private void AccesosDashboard_Load(object sender, EventArgs e)
        {
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            Usuario objUsuario = new Usuario();
            string IdTrab = LoginInfo.IdTrab;
            List<string> ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(IdTrab);

            Utilerias.MenuDinamico(MenuAccesos, ltModulosxUsuario);
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que deseas salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //Dashboard d = new Dashboard();
            //d.Show();
            this.Close();
        }
    }
}
