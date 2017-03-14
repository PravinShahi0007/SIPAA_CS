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
using SIPAA_CS.Recursos_Humanos.App_Code;

namespace SIPAA_CS.Recursos_Humanos.Administracion
{

    #region variables


    #endregion

    public partial class frmCompanias : Form
    {
        public frmCompanias()
        {
            InitializeComponent();
        }
        CompaniasSonarh companias = new CompaniasSonarh();

        //***********************************************************************************************
        //Autor: Noe Alvarez Marquina
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: Lee la tabla de compañias de SONARH
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar compañia 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //llena grid
            fgcomp(1,txtComp.Text.Trim());


            txtComp.Text = "";
            txtComp.Focus();
        }

        //boton minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
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
        private void frmCompanias_Load(object sender, EventArgs e)
        {
            //inicializa tool tip
            ftooltip();

            txtComp.Focus();

            //llena grid
            fgcomp(1,"");
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
        //llena grid compañias
        private void fgcomp(int popc, string pbusq)
        {

            DataTable dtcompania = companias.obtcomp(popc, pbusq);
            dgvComp.DataSource = dtcompania;

            dgvComp.Columns[0].Visible = false;
            dgvComp.Columns[1].Width = 355;
            dgvComp.Columns[2].Width = 125;
            dgvComp.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
