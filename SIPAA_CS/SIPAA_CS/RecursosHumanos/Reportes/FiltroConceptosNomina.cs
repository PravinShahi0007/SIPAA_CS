using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltroConceptosNomina : Form
    {
        //Definición de Variables
        public int iIdAfecta;
        public string sDescripcion;

        SonaConcAfec oConcAfec = new SonaConcAfec();
        Utilerias util = new Utilerias();

        public FiltroConceptosNomina()
        {
            InitializeComponent();
        }
        bool bprimeravez = true;

        //***********************************************************************************************
        //Autor: Benjamin Huizar Barajas
        //Fecha creación:12-05-2017     Última Modificacion: 
        //Descripción: Forma que llama al Reporte -> Mas de 3 Faltas en un período de 30 días
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txtIdAfecta.Text == string.Empty)
            {
                iIdAfecta = 0;
            }
            else
            {
                iIdAfecta = int.Parse(txtIdAfecta.Text);
            }

            DataTable dtReporteRegistro = oConcAfec.obtConcAfec(4, iIdAfecta, txtDescripcion.Text);

            switch (dtReporteRegistro.Rows.Count)
            {
                case 0:
                    DialogResult result = MessageBox.Show("No se encontro información.", "SIPAA");
                    break;

                default:
                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    ConceptosNomina dtrpt = new ConceptosNomina();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporteRegistro, this.CompanyName, dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("Titulo1", "SIPAA - Recursos Humanos");
                    ReportDoc.SetParameterValue("Titulo2", "Catálogo de Conceptos de Nómina");
                    ReportDoc.SetParameterValue("Titulo3", "");

                    form.RptDoc = ReportDoc;
                    form.Show();
                    break;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FiltroConceptosNomina_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FiltroConceptosNomina.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
    } // public class FiltroConceptoNomina : Form
} // namespace SIPAA_CS.RecursosHumanos.Reportes

