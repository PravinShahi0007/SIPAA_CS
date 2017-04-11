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

        public FiltrosRegistroGeneradoDetalle()
        {
            InitializeComponent();
        }

        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {


           
             dtFechaInicio = dpFechaInicio.Value.AddDays(-1);
             dtFechaFin = dpFechaFin.Value.AddDays(-1);

            if (txtIdTrab.Text == String.Empty)
            {
                sIdTrab = "%";
            }
            else {
                sIdTrab = txtIdTrab.Text;
            }

           

            sCompania = AsignarVariableCombo(cbCia);
            sUbicacion = AsignarVariableCombo(cbUbicacion);

            Incidencia objIncidencia = new Incidencia();
            DataTable dtReporte;
            dtReporte = objIncidencia.ReporteRegistroGeneradoDetalle(sIdTrab,dtFechaInicio,dtFechaFin,sUbicacion,sCompania);

            switch (dtReporte.Rows.Count) {

                case 0: DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    RegistroGeneradoDetalle dtrpt = new RegistroGeneradoDetalle();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "RecursosHumanos", dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaTermino", dpFechaFin.Value);
                    form.RptDoc = ReportDoc;
                    form.Show();
                    break;

            }

           


        }

        private void FiltrosRegistroGeneradoDetalle_Load(object sender, EventArgs e)
        {
           
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(5, "");
            LlenarCombos(dtCia,cbCia);

            DataTable dtUbicacion = objCia.ObtenerUbicacionPlantel(5,"%");
            LlenarCombos(dtUbicacion,cbUbicacion);
        }

        private void LlenarCombos(DataTable dt,ComboBox cb) {

            List<string> lt = new List<string>();

            foreach (DataRow row in dt.Rows)
            {

                lt.Add(row["Descripción"].ToString());
            }

            lt.Insert(0, "Seleccionar");

            cb.DataSource = lt;

        }

        private string AsignarVariableCombo(ComboBox cb) {

            string sAsignacion = "";

            switch (cb.SelectedIndex)
            {
                case 0: sAsignacion = "%";
                    break;

                default: sAsignacion = cb.SelectedItem.ToString();
                    break;

            }

            return sAsignacion;

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
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
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
            else {

                btnImprimirDetalle.Enabled = true;
                //btnImprimirResumen.Enabled = true;

            }
           
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
    }
}
