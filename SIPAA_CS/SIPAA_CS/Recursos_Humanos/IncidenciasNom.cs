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
using SIPAA_CS.Conexiones;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación: 15-mar-2017      Última Modificacion: dd-mm-aaaa
//Descripción: administra incidencias de nomina
//***********************************************************************************************

namespace SIPAA_CS.Recursos_Humanos
{
    public partial class IncidenciasNom : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        int pcvtipohr;
        int p_rep;

        #endregion

        IncNomina IncNom = new IncNomina();
        Utilerias Util = new Utilerias();

        public IncidenciasNom()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
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
        private void IncidenciasNom_Load(object sender, EventArgs e)
        {
            //habilita tool tip
            ftooltip();

            Util.cargarcombo(cboRepresenta, IncNom.cboInc(1));
            
            Util.cargarcombo(cbotipohr, IncNom.cboTipoHr(4));

            Util.cargarcombo(cbostdir, IncNom.cboEsNoPr(5));

            Util.cargarcombo(cbopremio, IncNom.cboEsNoPr(5));

            Util.cargarcombo(cbopasanom, IncNom.cboEsNoPr(5));
            cboRepresenta.Focus();
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
