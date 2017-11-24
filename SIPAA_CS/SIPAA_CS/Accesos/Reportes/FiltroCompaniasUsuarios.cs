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
    public partial class FiltroCompaniasUsuarios : Form
    {
        public int compania;
        public int usuario;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public FiltroCompaniasUsuarios()
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
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            AcceDashboard accedb = new AcceDashboard();
            accedb.Show();
            this.Close();
        }
        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            usuario = cbUsuario.SelectedIndex;
            compania = cbCompania.SelectedIndex;

            string usu = cbUsuario.SelectedValue.ToString();
            string comp = cbCompania.SelectedValue.ToString();

            //FILTRA POR TODOS
            if (usuario == 0 && compania == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                CompaniaUsuario objCompaniaUsuario = new CompaniaUsuario();
                DataTable dtReporte;
                dtReporte = objCompaniaUsuario.ReporteCompaniasUsuarios("%", "%", "", "", 6);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteCompaniasUsuarios dtrpt = new ReporteCompaniasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, this.CompanyName, dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR CVUSUARIO Y IDCOMPANIA
            else if (usuario > 0 && compania > 0 )
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                CompaniaUsuario objCompaniaUsuario = new CompaniaUsuario();
                DataTable dtReporte;
                dtReporte = objCompaniaUsuario.ReporteCompaniasUsuarios(usu, comp, "", "", 6);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteCompaniasUsuarios dtrpt = new ReporteCompaniasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, this.CompanyName, dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR CVUSUARIO
            else if (usuario > 0 && compania == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                CompaniaUsuario objCompaniaUsuario = new CompaniaUsuario();
                DataTable dtReporte;
                dtReporte = objCompaniaUsuario.ReporteCompaniasUsuarios(usu, "%", "", "", 6);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteCompaniasUsuarios dtrpt = new ReporteCompaniasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, this.CompanyName, dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }

            //FILTRA POR IDCOMPANIA
            else if (usuario == 0 && compania > 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                CompaniaUsuario objCompaniaUsuario = new CompaniaUsuario();
                DataTable dtReporte;
                dtReporte = objCompaniaUsuario.ReporteCompaniasUsuarios("%", comp, "", "", 6);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReporteCompaniasUsuarios dtrpt = new ReporteCompaniasUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, this.CompanyName, dtrpt.ResourceName);

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
        private void FiltroCompaniasUsuarios_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //// Diccionario Permisos x Pantalla
            //DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            //Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            ////////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            Usuario objUsuario = new Usuario();
            SonaCompania objSonaComp = new SonaCompania();

            DataTable dtUsuario = objUsuario.ObtenerListaUsuarios("%", 0, "", "", 0, "", "", 11);
            DataTable dtCompania = objSonaComp.obtcomp(7,"");
            
            llenaCombo(cbUsuario, dtUsuario, "cvusuario", "nombre");
            llenaCombo(cbCompania, dtCompania, "idcompania", "descripcion");
            btnImprimirDetalle.Image = global::SIPAA_CS.Properties.Resources.Imprimir;
            btnImprimirResumen.Image = Properties.Resources.Imprimir;

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

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {

        }



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------



    }
}
