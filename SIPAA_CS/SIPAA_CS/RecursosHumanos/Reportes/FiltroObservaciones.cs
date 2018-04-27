using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltroObservaciones : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        SonaTrabajador contenedorempleados = new SonaTrabajador();
        public FiltroObservaciones()
        {
            InitializeComponent();

        }
        


        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:09-04-2017     Última Modificacion: 17-04-2017
        //Descripción: -------------------------------
        //***********************************************************************************************


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        private void cbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCia.SelectedIndex != 0)
            {

                cbTipoNomina.Enabled = true;
                SonaTipoNomina objTnom = new SonaTipoNomina();
                DataTable dtTnom = objTnom.obtTipoNomina(5, cbCia.SelectedIndex, 0, "");
                // llenarCombo(cbTipoNomina, dtTnom, "Descripción"); // quite el acento
                Utilerias.llenarComboxDataTable(cbTipoNomina, dtTnom, "Clave", "Descripción");

                cbArea.Enabled = true;
                SonaCompania objCia = new SonaCompania();
                DataTable dtPlanta = objCia.ObtenerPlantel(4, 0, cbCia.SelectedItem.ToString(), "");
                llenarCombo(cbArea, dtPlanta, "Descripción"); // quite el acento
            }
            else
            {
                cbTipoNomina.Enabled = false;
                cbArea.Enabled = false;
            }


            /*if (cbCia.SelectedIndex != 0)
            {

                cbTipoNomina.Enabled = true;
                SonaTipoNomina objTnom = new SonaTipoNomina();
                DataTable dtTnom = objTnom.obtTipoNomina(5, cbCia.SelectedIndex, 0, "");
                llenarCombo(cbTipoNomina, dtTnom, "Descripción");

                cbArea.Enabled = true;
                SonaCompania objCia = new SonaCompania();
                DataTable dtPlanta = objCia.ObtenerPlantel(4, 0, cbCia.SelectedItem.ToString(), "");
                llenarCombo(cbArea, dtPlanta, "Descripción");
            }
            else
            {
                cbTipoNomina.Enabled = false;
                cbArea.Enabled = false;
            }*/




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }


        public void llenarCombo(ComboBox cb, DataTable dt, string sValor)
        {

            List<string> ltvalores = new List<string>();
            foreach (DataRow row in dt.Rows)
            {

                ltvalores.Add(row[1].ToString());
            }

            ltvalores.Insert(0, "Seleccionar");

            cb.DataSource = ltvalores;
        }

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
        }

        private void btnImprimirObs_Click(object sender, EventArgs e)
        {
            DateTime dtFechaInicio = dpFechaInicio.Value.AddDays(-1);
            DateTime dtFechaFin = dpFechaFin.Value.AddDays(-1);
            string sCia = AsignarVariableCombo(cbCia);
            string sArea = AsignarVariableCombo(cbArea);
            string sUbicacion = AsignarVariableCombo(cbUbicacion);
            string sTipoNom = AsignarVariableCombo(cbTipoNomina);
            string sDepto = AsignarVariableCombo(cbDepartamento);
            string sIncidencia = AsignarVariableCombo(cbIncidencia);
            string sIdtrab = "";
            string sStatus = string.Empty;

            //if (cbEmpleados.Text == String.Empty )
            //    sIdtrab = "%";
            //else
            //   sIdtrab =cbEmpleados.SelectedValue.ToString();


            if (cbEmpleados.SelectedIndex == 0)
                sIdtrab = "%";
            else
                sIdtrab = cbEmpleados.SelectedValue.ToString(); 



            if (cbStatus.SelectedIndex == 0)
                sStatus = "%";
            else if (cbStatus.SelectedIndex == 2)
                sStatus = "0";
            else 
                sStatus = cbStatus.SelectedIndex.ToString();

           // MessageBox.Show(sStatus);


               


            Incidencia objInc = new Incidencia();
            DataTable dtRpt = objInc.ReporteObservaciones(sIdtrab, dtFechaInicio, dtFechaFin, sDepto, sCia, sTipoNom, sUbicacion, sArea, sIncidencia, sStatus);

            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    Observaciones dtrpt = new Observaciones();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, this.CompanyName, dtrpt.ResourceName);
                    ReportDoc.SetParameterValue("TotalRegistros", dtRpt.Rows.Count.ToString());
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaFin", dpFechaFin.Value);
                    /*ReportDoc.SetParameterValue("Comp", sCia);
                    ReportDoc.SetParameterValue("Ubicacion", sUbicacion);
                    ReportDoc.SetParameterValue("Area", sArea);
                    ReportDoc.SetParameterValue("TipoNomina", sTipoNom);*/
                    form.RptDoc = ReportDoc;
                    form.Show();



                    // crear CSV
                    DialogResult Resultado = MessageBox.Show("¿Desea crear el archivo en formato .csv para abrirlo con excel?", "SIPAA", MessageBoxButtons.YesNo);
                    if (Resultado == DialogResult.Yes)
                        creacsv(dtRpt);


                    break;

            }
        }



 
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void FiltroObservaciones_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FiltroObservaciones.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(5, "");
            llenarCombo(cbCia, dtCia, "Descripción");

            DataTable dtUbicaciones = objCia.ObtenerUbicacionPlantel(5, "%");
            llenarCombo(cbUbicacion, dtUbicaciones, "Descripción");

            SonaDepartamento objDepto = new SonaDepartamento();
            DataTable dtDepto = objDepto.obtdepto(5, "");
            llenarCombo(cbDepartamento, dtDepto, "Descripción");

            ConcepInc objInc = new ConcepInc();
            DataTable dtInc = objInc.ConcepInc_S(4, 0, "", 0, 0, 0, 0, "", "");
            llenarCombo(cbIncidencia, dtInc, "Descripción");
            cbTipoNomina.Enabled = false;
            cbArea.Enabled = false;
            //Combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();


            List<string> ltvalores = new List<string>();
            ltvalores.Insert(0, "Seleccionar");
            ltvalores.Insert(1, "Activo");
            ltvalores.Insert(2, "Inactivo");
            cbStatus.DataSource = ltvalores;


        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        


        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        public string AsignarVariableCombo(ComboBox cb)
        {

            string sAsignacion;

            if (cb.SelectedIndex == 0)
            {
                sAsignacion = "%";
            }
            else
            {
                sAsignacion = cb.SelectedItem.ToString();

            }

            return sAsignacion;

        }

        private void ValidarFechaDataPicker(object sender, EventArgs e)
        {
            if (dpFechaInicio.Value > dpFechaFin.Value)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La fecha de Inicio no puede ser MAYOR a la de Término");

                timer1.Start();

                //  dpFechaFin.Value = dpFechaInicio.Value;
                btnImprimirObs.Enabled = false;

            }
            else
            {
                btnImprimirObs.Enabled = true;

            }

        }

        private void button3_Click(object sender, EventArgs e)
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

        private void cbStatus_DropDown(object sender, EventArgs e)
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
                    cadenaReg = "Reporte Observaciones ";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);
                    cadenaReg = "Idtrab, Nombre, Fecha, Incidencia, Justificacion, Tipo, Id_Aut, Observaciones ";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    foreach (DataRow row in dtRpt.Rows)
                    {
                        cadenaReg = row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," + row[3].ToString() + "," + row[4].ToString() + "," + row[5].ToString() + "," + row[6].ToString() + "," + row[7].ToString();
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
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
