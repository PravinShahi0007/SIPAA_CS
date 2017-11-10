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
            AcceDashboard accedb = new AcceDashboard();
            accedb.Show();
            this.Close();
        }

        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            perfil = cbPerfil.SelectedIndex;
            usuario = cbUsuario.SelectedIndex;

            string cvusuario = cbUsuario.SelectedValue.ToString();
            string cvperfil = cbPerfil.SelectedValue.ToString();

           
            // VALIDA SI SELECCIONO OPCION TODOS EN AMBOS
            if (usuario == 0 && perfil == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado todos");
                //timer1.Start();

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfilesUsuarios("%","%","","", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfilesUsuarios dtrpt = new ReportePerfilesUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }
            // VALIDA SELECCION DE USUARIO x Y PERFIL x
            else if (!(usuario == 0) && !(perfil == 0 ))
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado perfil y usuario");
                //timer1.Start();

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfilesUsuarios(cvusuario, cvperfil,"","", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfilesUsuarios dtrpt = new ReportePerfilesUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }
            // VALIDA SELECCION DE USUARIO x Y PERFIL TODOS
            else if (usuario > 0 && perfil == 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado usuario");
                //timer1.Start();

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfilesUsuarios(cvusuario, "%", "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfilesUsuarios dtrpt = new ReportePerfilesUsuarios();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "Accesos", dtrpt.ResourceName);

                        ReportDoc.SetParameterValue("TotalRegistros", dtReporte.Rows.Count.ToString());
                        //ReportDoc.SetParameterValue("Filtro", cbEstatus.SelectedItem.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;

                }
            }
            // VALIDA SELECCION DE USUARIO TODOS Y PERFIL x
            else if (usuario == 0 && perfil > 0)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Has seleccionado usuario");
                //timer1.Start();

                Perfil objPerfil = new Perfil();
                DataTable dtReporte;
                dtReporte = objPerfil.ReportePerfilesUsuarios("%", cvperfil, "", "", 5);

                switch (dtReporte.Rows.Count)
                {

                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportePerfilesUsuarios dtrpt = new ReportePerfilesUsuarios();
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
        private void FiltroPerfilesUsuarios_Load(object sender, EventArgs e)
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
            Perfil objPerfil = new Perfil();
            Usuario objUsuario = new Usuario();
            DataTable dtPerfil = objPerfil.ObtenerPerfiles("", "", "", 10);
            DataTable dtUsuario = objUsuario.ObtenerListaUsuarios("%",0,"","",0,"","",11);

            llenaCombo(cbPerfil,dtPerfil,"cvperfil","Descripcion");
            llenaCombo(cbUsuario, dtUsuario, "cvusuario", "nombre");
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
