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

namespace SIPAA_CS.Accesos.Reportes
{
    public partial class FiltroProcesos : Form
    {
        public int estatus;
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroProcesos()
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
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            estatus = cbEstatus.SelectedIndex;
            if (estatus <= 0 )
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Estatus");
                timer1.Start();
            }
            if (estatus == 1)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "1");
                //timer1.Start();

                cbEstatus.Text = "Seleccionar un Proceso";

                Proceso objProceso = new Proceso();
                DataTable dtReporte;
                dtReporte = objProceso.ReporteProcesos(0,"","1","","",7);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteProcesos dtrpt = new ReporteProcesos();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }

            }
            if (estatus == 2)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "2");
                //timer1.Start();

                cbEstatus.Text = "Seleccionar un Proceso";

                Proceso objProceso = new Proceso();
                DataTable dtReporte;
                dtReporte = objProceso.ReporteProcesos(0, "", "0", "", "", 7);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteProcesos dtrpt = new ReporteProcesos();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }

            }
            if (estatus == 3)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "3");
                //timer1.Start();

                cbEstatus.Text = "Seleccionar un Proceso";

                Proceso objProceso = new Proceso();
                DataTable dtReporte;
                dtReporte = objProceso.ReporteProcesos(0, "", "%", "", "", 7);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteProcesos dtrpt = new ReporteProcesos();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void FiltroProcesos_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        



        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------




    }
}
