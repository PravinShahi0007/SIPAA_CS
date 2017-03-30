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
    public partial class FiltroUsuarios : Form
    {
        public int estatus;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroUsuarios()
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
        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            estatus = cbEstatus.SelectedIndex;

            if (estatus <= 0)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Status");
                timer1.Start();
            }
            if (estatus == 1)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                timer1.Start();

                Usuario objUsuario = new Usuario();
                DataTable dtReporte;
                dtReporte = objUsuario.ReporteUsuarios("", 0, "", "", 0, "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        //ViewerReporte form = new ViewerReporte();
                        //RegistroGeneradoDetalle dtrpt = new RegistroGeneradoDetalle();
                        //ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "RecursosHumanos", dtrpt.ResourceName);

                        //ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                        //ReportDoc.SetParameterValue("FechaTermino", dpFechaFin.Value);
                        //form.RptDoc = ReportDoc;
                        //form.Show();
                        break;

                }
            }
            if (estatus == 2)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                timer1.Start();
            }
            if (estatus == 3)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "" + estatus);
                timer1.Start();
            }
        }


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
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
