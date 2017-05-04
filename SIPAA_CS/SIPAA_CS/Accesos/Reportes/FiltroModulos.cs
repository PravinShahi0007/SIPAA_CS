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

namespace SIPAA_CS.Accesos.Reportes
{
    public partial class FiltroModulos : Form
    {
        public int estatus;
        public int cvmodpad;
        public int ambiente;
        public int modulo;
        public int st;
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

            if (!(estatus <= 0 && cvmodpad <= 0 && ambiente <= 0 && modulo <= 0))
            {
                //FILTRO ESTATUS, CVMOD, AMBIENTE, MODULO
                if (estatus > 0 && cvmodpad > 0 && ambiente > 0 && modulo > 0)
                {
                    Modulo objModulo = new Modulo();
                    DataTable dtReporte;
                    if (estatus == 1)
                    {
                        dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "1", "", "", 15);
                    }
                    else if (estatus == 2)
                    {
                        dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "0", "", "", 15);
                    }
                        dtReporte = objModulo.ReporteModulos("", "", "%", 0, "%", "%", "", "%", "", "", 15);

                    switch (dtReporte.Rows.Count)
                    {

                        case 0:
                            DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                            cbEstatus.Text = "Seleccionar un Estatus";
                            llenaComboModPad();
                            llenaComboModulo();
                            llenaComboAmbiente();
                            break;

                        default:
                            cbEstatus.Text = "Seleccionar un Estatus";
                            llenaComboModPad();
                            llenaComboModulo();
                            llenaComboAmbiente();
                            ViewerReporte form = new ViewerReporte();
                            ReporteModulos dtrpt = new ReporteModulos();
                            ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                            ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                            form.RptDoc = ReportDoc;
                            form.Show();
                            break;

                    }
                }
                else
                {
                    //////////////////////////////////////////////FILTRO ESTATUS////////////////////////////////////////////////
                    if (estatus > 0)
                    {
                        //ESTATUS ACTIVO
                        if (estatus == 1)
                        {
                            //FILTRO SOLO ESTATUS
                            if (estatus == 1 && cvmodpad <= 0 && ambiente <= 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "", 0, "", "", "", "%", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATUS(ACTIVO) Y CVMODPAD
                            if (estatus == 1 && cvmodpad > 0 && ambiente <= 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", "%", "", "1", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(ACTIVO) Y AMBIENTE
                            if (estatus == 1 && cvmodpad <= 0 && ambiente > 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), "%", "", "1", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(ACTIVO) Y MODULO
                            if (estatus == 1 && cvmodpad <= 0 && ambiente <= 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, "%", cbModulo.SelectedItem.ToString(), "", "1", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(ACTIVO) Y CVMODPAD Y AMBIENTE
                            if (estatus == 1 && cvmodpad > 0 && ambiente > 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, cbAmbiente.SelectedItem.ToString(), "%", "", "1", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(ACTIVO) Y CVMODPAD Y MODULO
                            if (estatus == 1 && cvmodpad > 0 && ambiente <= 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", cbModulo.SelectedItem.ToString(), "", "1", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(ACTIVO) Y AMBIENTE Y MODULO
                            if (estatus == 1 && cvmodpad <= 0 && ambiente > 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "1", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
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


                        //ESTATUS INACTIVO
                        if (estatus == 2)
                        {
                            //FILTRO SOLO ESTATUS
                            if (estatus == 2 && cvmodpad <= 0 && ambiente <= 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, "%", "%", "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATUS(INACTIVO) Y CVMODPAD
                            if (estatus == 2 && cvmodpad > 0 && ambiente <= 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", "%", "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(INACTIVO) Y AMBIENTE
                            if (estatus == 2 && cvmodpad <= 0 && ambiente > 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), "%", "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(INACTIVO) Y MODULO
                            if (estatus == 2 && cvmodpad <= 0 && ambiente <= 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, "%", cbModulo.SelectedItem.ToString(), "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(INACTIVO) Y CVMODPAD Y AMBIENTE
                            if (estatus == 2 && cvmodpad > 0 && ambiente > 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, cbAmbiente.SelectedItem.ToString(), "%", "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(INACTIVO) Y CVMODPAD Y MODULO
                            if (estatus == 2 && cvmodpad > 0 && ambiente <= 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", cbModulo.SelectedItem.ToString(), "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(INACTIVO) Y AMBIENTE Y MODULO
                            if (estatus == 2 && cvmodpad <= 0 && ambiente > 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "0", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
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

                        //ESTATUS TODOS
                        if (estatus == 3)
                        {
                            //FILTRO SOLO ESTATUS
                            if (estatus == 3 && cvmodpad <= 0 && ambiente <= 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("%", "%", "%", 0, "%", "%", "%", "%", "%", "%", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATUS(TODOS) Y CVMODPAD
                            if (estatus == 3 && cvmodpad > 0 && ambiente <= 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", "%", "", "%", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(TODOS) Y AMBIENTE
                            if (estatus == 3 && cvmodpad <= 0 && ambiente > 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), "%", "", "%", "", "",15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(TODOS) Y MODULO
                            if (estatus == 3 && cvmodpad <= 0 && ambiente <= 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, "%", cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(TODOS) Y CVMODPAD Y AMBIENTE
                            if (estatus == 3 && cvmodpad > 0 && ambiente > 0 && modulo <= 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, cbAmbiente.SelectedItem.ToString(), "%", "", "%", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(TODOS) Y CVMODPAD Y MODULO
                            if (estatus == 3 && cvmodpad > 0 && ambiente <= 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
                                        ReporteModulos dtrpt = new ReporteModulos();
                                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                        form.RptDoc = ReportDoc;
                                        form.Show();
                                        break;

                                }
                            }
                            //FILTRO ESTATU(TODOS) Y AMBIENTE Y MODULO
                            if (estatus == 3 && cvmodpad <= 0 && ambiente > 0 && modulo > 0)
                            {

                                Modulo objModulo = new Modulo();
                                DataTable dtReporte;
                                dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                                switch (dtReporte.Rows.Count)
                                {

                                    case 0:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                        break;

                                    default:
                                        cbEstatus.Text = "Seleccionar un Estatus";
                                        llenaComboModPad();
                                        llenaComboModulo();
                                        llenaComboAmbiente();
                                        ViewerReporte form = new ViewerReporte();
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
                        
                    }
                    ////////////////////////////////////////////// FIN FILTRO ESTATUS ////////////////////////////////////////////////



                    //////////////////////////////////////////////FILTRO CVMODPAD////////////////////////////////////////////////////
                    if (cvmodpad > 0)
                    {   
                        //FILTRO CVMODPAD
                        if (estatus <= 0 && ambiente <= 0 && modulo <= 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", "%", "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    ViewerReporte form = new ViewerReporte();
                                    ReporteModulos dtrpt = new ReporteModulos();
                                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                    ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                    //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                    form.RptDoc = ReportDoc;
                                    form.Show();
                                    break;

                            }

                        }

                        //FILTRO CVMODPAD,ESTATUS
                        //if (estatus > 0 && ambiente <= 0 && modulo <= 0)
                        //{
                        //    Modulo objModulo = new Modulo();
                        //    DataTable dtReporte;
                        //    if (estatus == 1)
                        //    {
                                
                        //        dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "", "", "", "1", "", "", 16);
                        //    }
                        //    else if (estatus == 2)
                        //    {
                        //        st = 0;
                        //        dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "", "", "", "0", "", "", 16);
                        //    }
                        //    else
                        //    {
                        //        dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "", "", "", "0", "", "", 22);
                        //    }

                        //    switch (dtReporte.Rows.Count)
                        //    {

                        //        case 0:
                        //            cbEstatus.Text = "Seleccionar un Estatus";
                        //            llenaComboModPad();
                        //            llenaComboModulo();
                        //            llenaComboAmbiente();
                        //            DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        //            break;

                        //        default:
                        //            cbEstatus.Text = "Seleccionar un Estatus";
                        //            llenaComboModPad();
                        //            llenaComboModulo();
                        //            llenaComboAmbiente();
                        //            ViewerReporte form = new ViewerReporte();
                        //            ReporteModulos dtrpt = new ReporteModulos();
                        //            ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        //            ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //            //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        //            form.RptDoc = ReportDoc;
                        //            form.Show();
                        //            break;

                        //    }

                        //}

                        //FILTRO CVMODPAD,AMBIENTE
                        if (estatus <= 0 && ambiente > 0 && modulo <= 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, cbAmbiente.SelectedItem.ToString(), "%", "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    ViewerReporte form = new ViewerReporte();
                                    ReporteModulos dtrpt = new ReporteModulos();
                                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                    ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                    //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                    form.RptDoc = ReportDoc;
                                    form.Show();
                                    break;

                            }

                        }

                        //FILTRO CVMODPAD,MODULO
                        if (estatus <= 0 && ambiente <= 0 && modulo > 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", cbModPad.SelectedItem.ToString(), 0, "%", cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    ViewerReporte form = new ViewerReporte();
                                    ReporteModulos dtrpt = new ReporteModulos();
                                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                    ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                    //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                    form.RptDoc = ReportDoc;
                                    form.Show();
                                    break;

                            }

                        }

                        //FILTRO CVMODPAD,AMBIENTE,MODULO
                        if (estatus <= 0 && ambiente > 0 && modulo > 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    ViewerReporte form = new ViewerReporte();
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
                    //////////////////////////////////////////////FIN FILTRO CVMODPAD////////////////////////////////////////////////



                    //////////////////////////////////////////////FILTRO AMBIENTE////////////////////////////////////////////////////
                    if (ambiente > 0)
                    {
                        //FILTRO AMBIENTE
                        if (estatus <= 0 && cvmodpad <= 0 && modulo <= 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), "%", "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    ViewerReporte form = new ViewerReporte();
                                    ReporteModulos dtrpt = new ReporteModulos();
                                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                                    ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                                    //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                                    form.RptDoc = ReportDoc;
                                    form.Show();
                                    break;

                            }

                        }

                        //FILTRO AMBIENTE, MODULO
                        if (estatus <= 0 && cvmodpad <= 0 && modulo > 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", "%", 0, cbAmbiente.SelectedItem.ToString(), cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    ViewerReporte form = new ViewerReporte();
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
                    //////////////////////////////////////////////FIN FILTRO AMBIENTE////////////////////////////////////////////////////


                    //////////////////////////////////////////////   FILTRO MODULO   ////////////////////////////////////////////////////
                    if (modulo > 0)
                    {
                        //FILTRO MODULO
                        if (estatus <= 0 && cvmodpad <= 0 && ambiente <= 0)
                        {
                            Modulo objModulo = new Modulo();
                            DataTable dtReporte;
                            dtReporte = objModulo.ReporteModulos("", "", "%", 0, "%", cbModulo.SelectedItem.ToString(), "", "%", "", "", 15);

                            switch (dtReporte.Rows.Count)
                            {

                                case 0:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                                    break;

                                default:
                                    cbEstatus.Text = "Seleccionar un Estatus";
                                    llenaComboModPad();
                                    llenaComboModulo();
                                    llenaComboAmbiente();
                                    ViewerReporte form = new ViewerReporte();
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
                    ////////////////////////////////////////////// FIN FILTRO MODULO ////////////////////////////////////////////////////

                }

            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Filtro");
                timer1.Start();
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
            //// Diccionario Permisos x Pantalla
            //DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            //Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            ////////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------



    }
}
