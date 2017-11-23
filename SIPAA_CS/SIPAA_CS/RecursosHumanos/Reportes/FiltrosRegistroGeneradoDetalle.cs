using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
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
    public partial class FiltrosRegistroGeneradoDetalle : Form
    {

        public DateTime dtFechaInicio = DateTime.Today;
        public DateTime dtFechaFin = DateTime.Today;
        public string sIdTrab;
        public string sCompania;
        public string sUbicacion;
        public int sysH = SystemInformation.PrimaryMonitorSize.Height;
        public int sysW = SystemInformation.PrimaryMonitorSize.Width;
        SonaTrabajador contenedorempleados = new SonaTrabajador();

        public FiltrosRegistroGeneradoDetalle()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:04-04-2017     Última Modificacion: 17-04-2017
        //Descripción: -------------------------------
        //***********************************************************************************************


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        private void LlenarCombos(DataTable dt, ComboBox cb)
        {

            List<string> lt = new List<string>();

            foreach (DataRow row in dt.Rows)
            {

                lt.Add(row["Descripción"].ToString());
            }

            lt.Insert(0, "Seleccionar");

            cb.DataSource = lt;

        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
         

            dtFechaInicio = dpFechaInicio.Value.AddDays(-1);
            dtFechaFin = dpFechaFin.Value.AddDays(-1);

            if (cbEmpleados.Text == String.Empty)
            {
                sIdTrab = "%";
            }
            else
            {
                sIdTrab = cbEmpleados.SelectedValue.ToString();
            }



            sCompania = AsignarVariableCombo(cbCia);
            sUbicacion = AsignarVariableCombo(cbUbicacion);

            Incidencia objIncidencia = new Incidencia();
            DataTable dtReporte;
            dtReporte = objIncidencia.ReporteRegistroGeneradoDetalle(sIdTrab, dtFechaInicio, dtFechaFin, sUbicacion, sCompania);

            switch (dtReporte.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    RegistroGeneradoDetalle dtrpt = new RegistroGeneradoDetalle();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, this.CompanyName, dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaTermino", dpFechaFin.Value);
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
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void FiltrosRegistroGeneradoDetalle_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FiltrosRegistroGeneradoDetalle.cs")
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
            LlenarCombos(dtCia, cbCia);

            DataTable dtUbicacion = objCia.ObtenerUbicacionPlantel(5, "%");
            LlenarCombos(dtUbicacion, cbUbicacion);
            //Combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();
            btnImprimirDetalle.Image = Properties.Resources.Imprimir;
        }



        private void dpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (dpFechaInicio.Value > dpFechaFin.Value)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La fecha de Inicio no puede ser MAYOR a la de Término");

                timer1.Start();

                dpFechaFin.Value = dpFechaInicio.Value;
                btnImprimirDetalle.Enabled = false;
                //btnImprimirResumen.Enabled = false;

            }
            else
            {

                btnImprimirDetalle.Enabled = true;
                //btnImprimirResumen.Enabled = true;

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------


        private string AsignarVariableCombo(ComboBox cb)
        {

            string sAsignacion = "";

            switch (cb.SelectedIndex)
            {
                case 0:
                    sAsignacion = "%";
                    break;

                default:
                    sAsignacion = cb.SelectedItem.ToString();
                    break;

            }

            return sAsignacion;

        }



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
