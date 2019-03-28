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
using System.IO;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltroMasDe3Faltas : Form
    {

        private static string TITULO_REPORTE = "SIPAA - Recursos Humanos";
        private static string NOMBRE_REPORTE = "3 o más faltas en un periodo de 30 dias";
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        //Definición de Variables
        public string sIdTrab;
        public string sCompania;
        public string sUbicacion;
        public DateTime dtFechaBase = DateTime.Today;

        //Instanciamos las clases
        SonaTrabajador oTrabajador = new SonaTrabajador();
        SonaCompania oCompania = new SonaCompania();
        SonaUbicacion oUbicacion = new SonaUbicacion();
        Utilerias util = new Utilerias();

        public FiltroMasDe3Faltas()
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
            /////////////////////////////

            string sIdtrab = "";
            if (cboEmpleados.Text == String.Empty)
                sIdtrab = "%";
            else
                sIdtrab = cboEmpleados.SelectedValue.ToString();
            if (sIdtrab == "0")
                sIdtrab = "%";
            ///////////////////////

           /* if (cboEmpleados.SelectedIndex.ToString() == "0")
                sIdTrab = "%";
            else
                sIdTrab = cboEmpleados.SelectedIndex.ToString();
                */
                
            if (cbCompania.SelectedIndex.ToString() == "0")
                sCompania = "%";
            else
                sCompania = cbCompania.SelectedIndex.ToString();
            if (cbUbicacion.SelectedIndex.ToString() == "0")
                sUbicacion = "%";
            else
                sUbicacion = cbUbicacion.SelectedIndex.ToString(); 

            dtFechaBase = new DateTime(dpFechaBase.Value.Year, dpFechaBase.Value.Month, dpFechaBase.Value.Day);

            //sIdTrab = cboEmpleados.SelectedIndex.ToString() != "0" ? cboEmpleados.SelectedValue.ToString() : '%';
            //sCompania = cbCompania.SelectedIndex > 0 ? cbCompania.SelectedValue.ToString() : 0;
            //sUbicacion = cbUbicacion.SelectedIndex > 0 ? cbUbicacion.SelectedValue.ToString() : 0;
            //dtFechaBase = new DateTime(dpFechaBase.Value.Year, dpFechaBase.Value.Month, dpFechaBase.Value.Day);
           

            DataTable dtReporteRegistro = oTrabajador.MasDe3Faltas(5, sIdtrab, sCompania, sUbicacion, dtFechaBase);

            switch (dtReporteRegistro.Rows.Count)
            {
                case 0:
                    DialogResult result = MessageBox.Show("Sin Resultados", "SIPAA");
                    break;

                default:
                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    FaltasPeriodo dtrpt = new FaltasPeriodo();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporteRegistro, this.CompanyName, dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("titulo1", TITULO_REPORTE);
                    ReportDoc.SetParameterValue("descripcion1", NOMBRE_REPORTE);
                    ReportDoc.SetParameterValue("descripcion2", "Fecha base: " + dpFechaBase.Value.ToShortDateString());

                    form.RptDoc = ReportDoc;
                    form.Show();


                    // crear CSV
                    DialogResult Resultado = MessageBox.Show("¿Desea crear el archivo en formato .csv para abrirlo con excel?", "SIPAA", MessageBoxButtons.YesNo);
                    if (Resultado == DialogResult.Yes)
                        creacsv(dtReporteRegistro);


                   


                    break;
            }
        }

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
            DialogResult result = MessageBox.Show("¿Esta seguro que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FiltroMasDe3Faltas_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FiltroMasDe3Faltas.cs")
                {
                    f.Hide();
                }
            }
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);


            //LLENA COMBOS
            DataTable dtCompania = oCompania.obtcomp(5, "");
            Utilerias.llenarComboxDataTable(cbCompania, dtCompania, "Clave", "Descripción");

            DataTable dtUbicacion = oCompania.ObtenerUbicacionPlantel(5, "");
            Utilerias.llenarComboxDataTable(cbUbicacion, dtUbicacion, "IdUbicacion", "Descripción");

            DataTable dtempleados = oTrabajador.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cboEmpleados, dtempleados, "NoEmpleado", "Nombre");
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
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

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void creacsv(DataTable dtRpt)
        {



            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                bool bandera = false;

                try
                {

                    StreamWriter Texto = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);

                    string cadenaReg = "";
                    cadenaReg = "Reporte de 3 o mas Faltas ";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);
                    cadenaReg = "IdTrab, Trabajador, Fereget, Incidencia, Representa, IdTrabDir, Fecha Calificación Director, Falta, Calificada, Falta Efectiva ";
                    
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    foreach (DataRow row in dtRpt.Rows)
                    {
                        cadenaReg = row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," + row[3].ToString() + "," + row[4].ToString() + ","+
                            "" + row[5].ToString() + "," + row[6].ToString() + "," + row[7].ToString() + "," + row[8].ToString();
                        Texto.WriteLine(cadenaReg);
                    }

                    Texto.Close();
                }
                catch
                {

                    bandera = true;
                }
                if (!bandera)
                    MessageBox.Show("El archivo " + saveFileDialog.FileName + "ha sido creado Correctamente, ahora puede abrirlo con excel");
                else
                    MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");


            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
    } // public partial class FiltroMasDe3Faltas : Form
} // namespace SIPAA_CS.RecursosHumanos.Reportes
