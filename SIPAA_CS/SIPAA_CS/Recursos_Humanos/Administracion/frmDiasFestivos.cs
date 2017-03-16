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


namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    public partial class frmDiasFestivos : Form
    {
        #region

        int iAgr;
        int iAct;
        int iEli;
        int iCvFR;

        #endregion

        DiasFestivos oDiasFestivos = new DiasFestivos();

        public frmDiasFestivos()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Benjamin Huizar Barajas
        //Fecha creación: 13-Mar-2017       Última Modificacion: dd-mm-aaaa
        //Descripción: Administra Días Festivos
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

        //BOTON BUSCAR

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void frmDiasFestivos_Load(object sender, EventArgs e)
        {
            //LLAMA TOOL TIP
            sTooltip();

            //LLAMA METODO LLENAR GRID
            //SLlenaGrid(1, "");

            iAgr = 1;
            iAct = 1;
            iEli = 1;

            //HABILITA BOTON AGREGAR
            if (iAgr == 1)
            {
                btnAgregar.Visible = true;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                  S U B R U T I N A S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //TOOL TIP PARA OBJETOS
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
            toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            toolTip1.SetToolTip(this.btnEliminar, "Alimina Registro");

        } // private void sTooltip()
    } // public partial class frmDiasFestivos : Form
} // namespace SIPAA_CS.Recursos_Humanos.Administracion
