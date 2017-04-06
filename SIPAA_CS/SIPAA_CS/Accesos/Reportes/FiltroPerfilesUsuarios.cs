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
    public partial class FiltroPerfilesUsuarios : Form
    {
        public int perfil;
        public int usuario;
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroPerfilesUsuarios()
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que deseas salir?", "SIPAA", MessageBoxButtons.YesNo);

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

        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            perfil = cbPerfil.SelectedIndex;
            usuario = cbPerfil.SelectedIndex;
            //VALIDA SI ESTA SELECCIONADO
            if (perfil < 0 && usuario < 0)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un filtro");
                timer1.Start();
            }
            // VALIDA SI SELECCIONO OPCION TODOS EN AMBOS
            if (perfil == 0)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                timer1.Start();

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfilesUsuarios("",0,5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReportePerfilesUsuarios form = new ViewerReportePerfilesUsuarios();
                        ReportePerfilesUsuarios dtrpt = new ReportePerfilesUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        //ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }
            // VALIDA SELECCION DE PERFIL
            if (perfil > 0 )
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado perfil");
                timer1.Start();
            }
            // VALIDA SELECCION DE USUARIO
            if (usuario > 0)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado usuario");
                timer1.Start();
            }

        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void FiltroPerfilesUsuarios_Load(object sender, EventArgs e)
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
