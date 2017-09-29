using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltroResumen : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        SonaTrabajador contenedorempleados = new SonaTrabajador();
        public FiltroResumen()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:04-04-2017     Última Modificacion: 22-09-2017 Rafael Lemus
        //Descripción: -------------------------------
        //***********************************************************************************************



        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        public void llenarCombo(ComboBox cb, DataTable dt, string sValor)
        {

            List<string> ltvalores = new List<string>();
            foreach (DataRow row in dt.Rows)
            {

                ltvalores.Add(row[sValor].ToString());
            }

            ltvalores.Insert(0, "Seleccionar");

            cb.DataSource = ltvalores;
        }

        private void cbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCia.SelectedIndex != 0)
            {

                cbTipoNomina.Enabled = true;
                SonaTipoNomina objTnom = new SonaTipoNomina();
                DataTable dtTnom = objTnom.obtTipoNomina(5, cbCia.SelectedIndex, 0, "");
                llenarCombo(cbTipoNomina, dtTnom, "Descripción"); // quite el acento

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


        }


        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

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
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {
            DateTime dtFechaInicio = dpFechaInicio.Value.AddDays(-1);
            DateTime dtFechaFin = dpFechaFin.Value.AddDays(-1);
            string sCia = AsignarVariableCombo(cbCia);
            string sArea = AsignarVariableCombo(cbArea);
            string sUbicacion = AsignarVariableCombo(cbUbicacion);
            string sTipoNom = AsignarVariableCombo(cbTipoNomina);
            string sDepto = AsignarVariableCombo(cbDepartamento);

            string sIdtrab = "";
            if (cbEmpleados.Text == String.Empty)
               sIdtrab = "%";
            else
              sIdtrab = cbEmpleados.SelectedValue.ToString();
            
            Incidencia objInc = new Incidencia();

            DataTable dtRpt = objInc.ReporteResumen(sIdtrab, dtFechaInicio, dtFechaFin, sDepto, sCia, sTipoNom, sUbicacion, sArea);

            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    Resumen dtrpt = new Resumen();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, "RecursosHumanos", dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("TotalRegistros", dtRpt.Rows.Count.ToString());
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaTermino", dpFechaFin.Value);
                    ReportDoc.SetParameterValue("Comp", sCia);
                    ReportDoc.SetParameterValue("Ubicacion", sUbicacion);
                    ReportDoc.SetParameterValue("Area", sArea);
                    ReportDoc.SetParameterValue("TipoNomina", sTipoNom);
                    form.RptDoc = ReportDoc;
                    form.Show();
                    break;

            }

        }



     

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------


        private void FiltroResumen_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FiltroResumen.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;


            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(5, "%");
            llenarCombo(cbCia, dtCia, "Descripción");

            DataTable dtUbicaciones = objCia.ObtenerUbicacionPlantel(5, "%");
            llenarCombo(cbUbicacion, dtUbicaciones, "Descripción");

            SonaDepartamento objDepto = new SonaDepartamento();
            DataTable dtDepto = objDepto.obtdepto(5, "%");
            llenarCombo(cbDepartamento, dtDepto, "Descripción");

            cbTipoNomina.Enabled = false;
            cbArea.Enabled = false;
            //Combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void ValidarFechaDataPicker(object sender, EventArgs e)
        {
            if (dpFechaInicio.Value > dpFechaFin.Value)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La fecha de Inicio no puede ser MAYOR a la de Término");

                timer1.Start();

                //  dpFechaFin.Value = dpFechaInicio.Value;
                btnImprimirResumen.Enabled = false;

            }
            else
            {
                btnImprimirResumen.Enabled = true;

            }


        }


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

      



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------






    }
}
