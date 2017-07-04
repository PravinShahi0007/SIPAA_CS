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

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltrosRegistroDetalle : Form
    {
        public string sIdTrab;
        public string sCompania;
        public string sUbicacion;
        public DateTime dtFechaInicio = DateTime.Today;
        public DateTime dtFechaFin = DateTime.Today;
        //public int sysH = SystemInformation.PrimaryMonitorSize.Height;
        //public int sysW = SystemInformation.PrimaryMonitorSize.Width;

        //////instanciamos los objetos (segun san lucas)
        SonaTrabajador oTrabajador = new SonaTrabajador();
        SonaCompania2 oCompañia = new SonaCompania2();
        SonaUbicacion oUbicacion = new SonaUbicacion();
        Utilerias util = new Utilerias();

        public FiltrosRegistroDetalle()
        {
            InitializeComponent();
        }

        bool bprimeravez = true;

        //***********************************************************************************************
        //Autor: José Luis Alvarez Delgado
        //Fecha creación:26-04-2017     Última Modificacion: 
        //Descripción: -------------------------------
        //***********************************************************************************************

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
            dtFechaInicio = dpFechaInicio.Value;
            dtFechaFin = dpFechaFin.Value;

            if (txtIdTrab.Text==string.Empty)
            {
                sIdTrab = "%";
            }
            else
            {
                sIdTrab = txtIdTrab.Text;
            }

            if (cbCompania.Text==string.Empty | cbCompania.Text=="Seleccionar Compañia...")
            {
                sCompania = "%";
            }
            else
            {
                sCompania = cbCompania.SelectedValue.ToString();
            }

            if (cbUbicacion.Text==string.Empty | cbUbicacion.Text=="Seleccionar")
            {
                sUbicacion = "%";
            }
            else
            {
                sUbicacion = cbUbicacion.SelectedValue.ToString();
            }

            DataTable dtReporteRegistroDetalle = oTrabajador.ObtenerRegistroDetalle(sIdTrab, dtFechaInicio
                     ,dtFechaFin, sCompania, sUbicacion);

            switch (dtReporteRegistroDetalle.Rows.Count)
            {
                case 0:
                    DialogResult result = MessageBox.Show("No se encontro información.", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    RegistroDetalle dtrpt = new RegistroDetalle();
                    //metodo del vic para ejecutar un reporte (segun yo)
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporteRegistroDetalle, "RecursosHumanos", dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaFin", dpFechaFin.Value);
                    form.RptDoc = ReportDoc;
                    form.Show();
                    break;
            }
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
            DialogResult result = MessageBox.Show("¿Esta seguro que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

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
        private void FiltrosRegistroDetalle_Load(object sender, EventArgs e)
        {
            //Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            if (bprimeravez == true)
            {
                //llenado de combo compañias
                util.cargarcombo(cbCompania, oCompañia.obtCompania2(5, ""));
                //DataTable dtCompañia = oCompañia.obtCompania2(5, "");
                //cbCompania.DataSource = dtCompañia;
                //cbCompania.DisplayMember = "Descripción";
                //cbCompania.ValueMember = "Clave";
                cbCompania.Text = "Seleccionar Compañia...";

                //llenado de combo ubicaciones
                Utilerias.llenarComboxDataTable(cbUbicacion, oUbicacion.obtenerSonaUbicacion("", 6), "Clave", "Descripción");
                bprimeravez = false;
            }
        }

        private void dpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (dpFechaInicio.Value > dpFechaFin.Value)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La fecha de Inicio no puede ser MAYOR a la de Término");
                MessageBox.Show("La fecha de Inicio no puede ser MAYOR a la Final", "SIPPA");
                dpFechaFin.Value = dpFechaInicio.Value;                
                btnImprimirDetalle.Enabled = false;
                dpFechaInicio.Focus();
            }
            else
            {
                btnImprimirDetalle.Enabled = true;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
