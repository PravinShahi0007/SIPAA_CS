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

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación:13-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Muestra y busca puestos sonarh
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos
{
    public partial class Puestos : Form
    {
        public Puestos()
        {
            InitializeComponent();
        }

        SonaPuesto puestos = new SonaPuesto();

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //llena grid
            fgptos(1, txtPuestos.Text.Trim());


            txtPuestos.Text = "";
            txtPuestos.Focus();
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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Puestos_Load(object sender, EventArgs e)
        {
            
            //inicializa tool tip
            ftooltip();

            txtPuestos.Focus();

            //llena grid
            fgptos(1, "");

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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
        private void fgptos(int popc, string pbusq)
        {

            DataTable dtpuestos = puestos.obtptos(popc, pbusq);
            dgvPuestos.DataSource = dtpuestos;

            dgvPuestos.Columns[0].Visible = false;
            dgvPuestos.Columns[1].Width = 475;

            dgvPuestos.ClearSelection();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
