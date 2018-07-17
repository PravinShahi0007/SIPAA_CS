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
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.Accesos.Catalogos;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:27/10/2017     Última Modificacion: dd-mm-aaaa
//Descripción: menu sistemas
//***********************************************************************************************

namespace SIPAA_CS.Sistemas
{
    public partial class SistDashboard : Form
    {
        public SistDashboard()
        {
            InitializeComponent();
        }

        Usuarioap cusuarioap = new Usuarioap();

        string sultacceso;

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnregresar_Click(object sender, EventArgs e)
        {
            Dashboard dasb = new Dashboard();
            dasb.Show();
            this.Close();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void SistDashboard_Load(object sender, EventArgs e)
        {
            //variables datos del usuario
            DataTable datosusuario = cusuarioap.dtdatos(4, LoginInfo.cvusuario, 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            sultacceso = datosusuario.Rows[0][5].ToString();
            lblacceso.Text = sultacceso;

            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //tool tip
            ftooltip();
            //usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //menu
            CrearMenu();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //funcion para tool tip
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
            toolTip1.SetToolTip(this.btncerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
        }

        public void CrearMenu()
        {
            Perfil objPer = new Perfil();
            DataTable dt = objPer.ReportePerfilesModulos("SIST", "%", LoginInfo.IdTrab, "CS", 0, 0, 0, 0, 0, 14);
            DataTable dtEncabezados = Utilerias.CrearEncabezados(dt);
            Utilerias.ProcesoMenu(dtEncabezados, LoginInfo.IdTrab, "SIST", null, MsMenu, paneltitulo.BackColor);
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
