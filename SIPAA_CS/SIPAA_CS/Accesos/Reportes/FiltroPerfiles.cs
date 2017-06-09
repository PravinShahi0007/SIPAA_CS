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
    public partial class FiltroPerfiles : Form
    {
        public int estatus;
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroPerfiles()
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
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            Utilerias.AsignarBotonResize(btnImprimirDetalle, new Size(sysW, sysH), "Imprimir");
            estatus = cbEstatus.SelectedIndex;

            if (estatus < 0)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Estatus");
                timer1.Start();
            }
            if (estatus == 0)
            {

                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                //timer1.Start();

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfiles(0, "","1", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfiles dtrpt = new ReportePerfiles();
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

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfiles(0, "", "0", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfiles dtrpt = new ReportePerfiles();
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

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfiles(0, "", "%", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfiles dtrpt = new ReportePerfiles();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
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
        private void FiltroPerfiles_Load(object sender, EventArgs e)
        {
            //// Diccionario Permisos x Pantalla
            //DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            //Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            ////////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////
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
