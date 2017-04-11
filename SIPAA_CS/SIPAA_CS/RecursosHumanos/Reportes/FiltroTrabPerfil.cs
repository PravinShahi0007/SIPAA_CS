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
using SIPAA_CS.Conexiones;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;

//***********************************************************************************************
//Autor: Marco Dupont
//Fecha creación: 17-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Formas de Registro Empleado
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltroTrabPerfil : Form
    {
        Utilerias Util = new Utilerias();
        SonaCompania CComUbi = new SonaCompania();

        public FiltroTrabPerfil()
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
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void FiltroTrabPerfil_Load(object sender, EventArgs e)
        {
            //Configuracion de la pantalla
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            //LLAMA TOOL TIP
            sTooltip();

            //LLENA COMBOS
            Util.cargarcombo(cboCia, CComUbi.obtcomp(5, ""));
            Util.cargarcombo(cboUbicacion, CComUbi.ObtenerUbicacionPlantel(""));

            txtIdTrab.Focus();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                  S U B R U T I N A S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        private void sTooltip()
        {

            //CREA TOOL TIP
            ToolTip toolTip1 = new ToolTip();

            //CONFIGURACION
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            // CONFIGURA EL TEXTO POR OBJETO
            toolTip1.SetToolTip(this.btnCerrar, "Cierra Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimiza Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresa");
            //toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            //toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            //toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
            //toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            //toolTip1.SetToolTip(this.btnEliminar, "Elimina Registro");
            //toolTip1.SetToolTip(this.btnActiva, "Activa Registro");
        }

        //-----------------------------------------------------------------------------------------------
        //                                  S U B R U T I N A S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

    }
}
