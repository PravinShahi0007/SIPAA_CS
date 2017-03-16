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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void IncidenciasNom_Load(object sender, EventArgs e)
        {

            Util.cargarcombo(cboRepresenta, IncNom.cboInc(1));
            cboRepresenta.Text = "";
            cboRepresenta.Focus();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
