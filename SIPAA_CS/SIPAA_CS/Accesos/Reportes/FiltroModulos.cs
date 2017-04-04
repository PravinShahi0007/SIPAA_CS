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
    public partial class FiltroModulos : Form
    {
        public int estatus;
        public int cvmodpad;
        public int ambiente;
        public int modulo;
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        Utilerias utilerias = new Utilerias();
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroModulos()
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
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            Utilerias.AsignarBotonResize(btnImprimirDetalle, new Size(sysW, sysH), "Imprimir");
            estatus = cbEstatus.SelectedIndex;
            cvmodpad = cbModPad.SelectedIndex;
            ambiente = cbAmbiente.SelectedIndex;
            modulo = cbModulo.SelectedIndex;

            if (estatus < 0 && cvmodpad <= 0 && ambiente <= 0 && modulo <= 0)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Status");
                timer1.Start();
            }

            //if (estatus < 0)
            //{

            //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Status");
            //    timer1.Start();
            //}

            if (estatus == 0)
            {

                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                //timer1.Start();

                Modulo objModulo = new Modulo();
                DataTable dtReporte;
                dtReporte = objModulo.ReporteModulos("","","",0,"","","",0,"","",6);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporteModulos form = new ViewerReporteModulos();
                        ReporteModulos dtrpt = new ReporteModulos();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            if (estatus == 1)
            {

                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                //timer1.Start();

                Modulo objModulo = new Modulo();
                DataTable dtReporte;
                dtReporte = objModulo.ReporteModulos("", "", "", 0, "", "", "", 0, "", "", 7);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporteModulos form = new ViewerReporteModulos();
                        ReporteModulos dtrpt = new ReporteModulos();
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
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                //timer1.Start();

                Modulo objModulo = new Modulo();
                DataTable dtReporte;
                dtReporte = objModulo.ReporteModulos("", "", "", 0, "", "", "", 0, "", "", 8);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporteModulos form = new ViewerReporteModulos();
                        ReporteModulos dtrpt = new ReporteModulos();
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
        private void FiltroModulos_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            //utilerias.cargarcombo(cbModPad,);
            llenaComboModPad();
            llenaComboModulo();
            llenaComboAmbiente();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }




        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        public void llenaComboModPad()
        {
            Modulo objModulo = new Modulo();
            DataTable dtModulo = objModulo.ObtenerModulo("", "", "", 0, "", "", "", 0, "", "", 9);


            List<string> ltModulo = new List<string>();

            ltModulo.Insert(0, "Selecciona un CvModPad");
            foreach (DataRow row in dtModulo.Rows)
            {
                ltModulo.Add(row["cvmodpad"].ToString());
            }

            cbModPad.DataSource = ltModulo;
        }
        public void llenaComboModulo()
        {
            Modulo objModulo = new Modulo();
            DataTable dtModulo = objModulo.ObtenerModulo("", "", "", 0, "", "", "", 0, "", "", 10);


            List<string> ltModulo = new List<string>();

            ltModulo.Insert(0, "Selecciona una Módulo");
            foreach (DataRow row in dtModulo.Rows)
            {
                ltModulo.Add(row["modulo"].ToString());
            }

            cbModulo.DataSource = ltModulo;
        }

        public void llenaComboAmbiente()
        {
            Modulo objModulo = new Modulo();
            DataTable dtModulo = objModulo.ObtenerModulo("", "", "", 0, "", "", "", 0, "", "", 11);


            List<string> ltModulo = new List<string>();

            ltModulo.Insert(0, "Selecciona una Ambiente");
            foreach (DataRow row in dtModulo.Rows)
            {
                ltModulo.Add(row["ambiente"].ToString());
            }

            cbAmbiente.DataSource = ltModulo;
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------



    }
}
