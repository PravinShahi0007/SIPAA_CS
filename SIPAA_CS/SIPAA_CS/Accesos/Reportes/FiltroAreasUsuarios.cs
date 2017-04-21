using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.Accesos.Asignaciones;
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
    public partial class FiltroAreasUsuarios : Form
    {
        public int usuario;
        public int compania;
        public int planta;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroAreasUsuarios()
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
        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            usuario = cbUsuario.SelectedIndex;
            compania = cbCompania.SelectedIndex;
            planta = cbPlantel.SelectedIndex;

            string usu = cbUsuario.SelectedValue.ToString();
            string comp = cbCompania.SelectedValue.ToString();
            string pla = cbPlantel.SelectedValue.ToString();

            //FILTRA POR TODOS
            if (usuario == 0 && compania == 0 && planta == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario("%", "%", "%", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR CVUSUARIO, IDCOMPANIA, IDPLANTA
            else if (usuario > 0 && compania > 0 && planta > 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario(usu, comp, pla, "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR CVUSUARIO, IDCOMPANIA
            else if (usuario > 0 && compania > 0 && planta == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario(usu, comp, "%", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR CVUSUARIO, IDPLANTA
            else if (usuario > 0 && compania == 0 && planta > 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario(usu, "%", pla, "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR IDCOMPANIA, IDPLANTA
            else if (usuario == 0 && compania > 0 && planta > 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario("%", comp, pla, "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR CVUSUARIO
            else if (usuario > 0 && compania == 0 && planta == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario(usu, "%", "%", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR IDCOMPANIA
            else if (usuario == 0 && compania > 0 && planta == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario("%", comp, "%", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR IDPLANTA
            else if (usuario == 0 && compania == 0 && planta > 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                AreaUsuario objAreaUsuario = new AreaUsuario();
                DataTable dtReporte;
                dtReporte = objAreaUsuario.ReporteAreaUsuario("%", "%", pla, "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteAreasUsuarios dtrpt = new ReporteAreasUsuarios();
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
        private void FiltroAreasUsuarios_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            Usuario objUsuario = new Usuario();
            SonaCompania objSonaComp = new SonaCompania();

            DataTable dtUsuario = objUsuario.ObtenerListaUsuarios("", 0, "", "", 0, "", "", 11);
            DataTable dtCompania = objSonaComp.obtcomp(7, "");
            DataTable dtPlantel = objSonaComp.ObtenerPlantel(5, 0,"","");

            llenaCombo(cbUsuario, dtUsuario, "cvusuario", "nombre");
            llenaCombo(cbCompania, dtCompania, "idcompania", "descripcion");
            llenaCombo(cbPlantel, dtPlantel, "Clave", "Descripción");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        public static void llenaCombo(ComboBox cb, DataTable dt, string sClave, string sDescripcion)
        {
            DataRow row = dt.NewRow();
            row[sClave] = "0";
            row[sDescripcion] = "Todos";
            dt.Rows.InsertAt(row, 0);
            cb.DataSource = dt;
            cb.DisplayMember = sDescripcion;
            cb.ValueMember = sClave;
        }

        

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
