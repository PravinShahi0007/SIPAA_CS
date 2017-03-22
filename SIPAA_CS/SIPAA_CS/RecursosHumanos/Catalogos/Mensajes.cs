using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.Properties;
using SIPAA_CS.App_Code;

namespace SIPAA_CS.RecursosHumanos
{

    #region variables


    #endregion

    public partial class Mensajes : Form
    {
        public Mensajes()
        {
            InitializeComponent();
        }

        //se "instancia" la clase para usar todos los metodos que contenga
        MensajesSonarh pantallaMensajes = new MensajesSonarh();
                
        private void Mensajes_Load(object sender, EventArgs e)
        {
            ftooltip();
            txtMensaje.Focus();
            //gridMensajes(1, "",0,0,"","","","");
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //gridMensajes(1, txtMensaje.Text.Trim());
            txtMensaje.Text = "";
            txtMensaje.Focus();            
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }

        private void gridMensajes(int popc, string pbusqueda, int pidtrab, int pcvmensaje, DateTime pfeinicio, DateTime pfefin, string pusuumod, string pprgumod)
        {
            DataTable dtmensajes = pantallaMensajes.ObtenerMensajes(popc, pbusqueda, pidtrab, pcvmensaje, pfeinicio, pfefin, pusuumod, pprgumod);
            dgvMensajes.DataSource = dtmensajes;
            dgvMensajes.ClearSelection();
              
        }
        
    }
}
