using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.RecursosHumanos.Reportes;
using CrystalDecisions.CrystalReports.Engine;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación:28-abr-2017       Última Modificacion: dd-mm-aaaa
//Descripción: proceso de creación de incidencias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class ProcesaIncidencias : Form
    {
        //variables
        int ivarproceso;
        public ProcesaIncidencias()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        //                         V A R I A B L E S    L O C A L E S
        //-----------------------------------------------------------------------------------------------

        #region

        int iins, iact, ielim;// variables permisos de insertar, actualizar, eliminar
        int iactbtn, istprocesaper;// actión de realizar botón

        int iresp;// variable de respuesta-acción realizada

        #endregion

        Utilerias Util = new Utilerias();
        ProcesaIncidencia ProcesaInc = new ProcesaIncidencia();
        Perfil DatPerfil = new Perfil();
        //ProcesoIncidenciasLogs clprocesoincidenciaslogs = new ProcesoIncidenciasLogs();

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //busca registros
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnbuscar.Enabled = false;
                //valida se seleccione un periodo
                if (Int32.Parse(cbtiponomina.SelectedValue.ToString()) == 0)
                {
                    DialogResult result = MessageBox.Show("Seleccione un tipo de nomina", "SIPAA");
                    cbtiponomina.Focus();
                }
                else
                {
                    Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Espere por favor, buscando registros...");

                    //llena grid
                    fgregistros(4, Int32.Parse(cbtiponomina.SelectedValue.ToString()),0);


                }
                btnbuscar.Enabled = true;
                Cursor.Current = Cursors.Default;

                pnlmenssuid.Visible = false;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                //valida se seleccione un periodo
                if (Int32.Parse(cbtiponomina.SelectedValue.ToString()) == 0)
                {
                    DialogResult result = MessageBox.Show("Seleccione un tipo de nomina", "SIPAA");
                    cbtiponomina.Focus();
                }
                else
                {
                    DataTable dtvarproinc = clprocesoincidenciaslogs.dtdatos(1, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "", "", "", "", "");
                    ivarproceso = Int32.Parse(dtvarproinc.Rows[0][0].ToString());

                    //ejecuta conforme a las variables
                    if (ivarproceso == 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        btnguardar.Enabled = false;

                        DialogResult resultms = MessageBox.Show("Esta acción genera las incidencias del periodo: " + "\r\n" + "\r\n" + cbtiponomina.Text + "\r\n" + "\r\n" + " ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                        if (resultms == DialogResult.Yes)
                        {
                            Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Espere por favor, generando incidencias...");

                            int ival = ProcesaInc.vuidprocesainc(5, 0, 0, txtfecini.Text, txtfecfin.Text, Int32.Parse(cbtiponomina.SelectedValue.ToString()), 0, 0, LoginInfo.IdTrab, this.Name);
                            clprocesoincidenciaslogs.cruddatos(2, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "Genera incidencias", "", LoginInfo.IdTrab, this.Name, Util.scontrol());

                            ProcesaInc.vuidprocesainc(8, 0, 0, "", "", 0, 0, 0, LoginInfo.IdTrab, this.Name);
                            clprocesoincidenciaslogs.cruddatos(2, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "Manda correos a supervisores y/directores cuando aplique", "", LoginInfo.IdTrab, this.Name, Util.scontrol());

                            txtfecini.Text = "";
                            txtfecfin.Text = "";
                            dgvregistros.DataSource = null;
                            btnguardar.Enabled = false;

                            //llena combo tipo de nomina
                            Util.p_inicbo = 0;
                            DataTable dtcbtipnom = ProcesaInc.cbtiponomina(8);
                            Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "Clave", "Descripción");
                            Util.p_inicbo = 1;

                            Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 1, "Incidencias generadas con exito");

                            DialogResult result = MessageBox.Show("Incidencias generadas con exito", "SIPAA", MessageBoxButtons.OK);

                            pnlmenssuid.Visible = false;
                            cbtiponomina.Focus();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                    else if (ivarproceso == 1)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        btnguardar.Enabled = false;

                        DialogResult resultms = MessageBox.Show("Esta acción genera las incidencias del periodo: " + "\r\n" + "\r\n" + cbtiponomina.Text + "\r\n" + "\r\n" + " ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                        if (resultms == DialogResult.Yes)
                        {
                            Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Espere por favor, generando incidencias...");

                            clprocesoincidenciaslogs.cruddatos(3, 0, 0, "", "", LoginInfo.IdTrab, this.Name, Util.scontrol());
                            clprocesoincidenciaslogs.cruddatos(2, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "Crea LOG antes de procesar incidencias", "", LoginInfo.IdTrab, this.Name, Util.scontrol());

                            int ival = ProcesaInc.vuidprocesainc(5, 0, 0, txtfecini.Text, txtfecfin.Text, Int32.Parse(cbtiponomina.SelectedValue.ToString()), 0, 0, LoginInfo.IdTrab, this.Name);
                            clprocesoincidenciaslogs.cruddatos(2, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "Genera incidencias", "", LoginInfo.IdTrab, this.Name, Util.scontrol());

                            ProcesaInc.vuidprocesainc(8, 0, 0, "", "", 0, 0, 0, LoginInfo.IdTrab, this.Name);
                            clprocesoincidenciaslogs.cruddatos(2, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "Manda correos a supervisores y/directores cuando aplique", "", LoginInfo.IdTrab, this.Name, Util.scontrol());

                            clprocesoincidenciaslogs.cruddatos(4, 0, 0, "", "", LoginInfo.IdTrab, this.Name, Util.scontrol());
                            clprocesoincidenciaslogs.cruddatos(2, 0, Int32.Parse(cbtiponomina.SelectedValue.ToString()), "Crea LOG después de procesar incidencias", "", LoginInfo.IdTrab, this.Name, Util.scontrol());

                            txtfecini.Text = "";
                            txtfecfin.Text = "";
                            dgvregistros.DataSource = null;
                            btnguardar.Enabled = false;

                            //llena combo tipo de nomina
                            Util.p_inicbo = 0;
                            DataTable dtcbtipnom = ProcesaInc.cbtiponomina(8);
                            Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "Clave", "Descripción");
                            Util.p_inicbo = 1;

                            Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 1, "Incidencias generadas con exito");

                            DialogResult result = MessageBox.Show("Incidencias generadas con exito", "SIPAA", MessageBoxButtons.OK);

                            pnlmenssuid.Visible = false;
                            cbtiponomina.Focus();
                        }
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("No se realizo ninguna accrion, verificar con el área de sistemas", "SIPAA", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        //boton minimizar
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        //botón regresar
        private void btnregresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void ProcesaIncidencias_Load(object sender, EventArgs e)
        {
            try
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

                //Rezise de la Forma
                Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

                //llena etiqueta de usuario
                lblusuario.Text = LoginInfo.Nombre;
                Utilerias.cargaimagen(ptbimgusuario);

                //función para tool tip
                ftooltip();

                //variables accesos
                DataTable Permisos = DatPerfil.accpantalla(LoginInfo.IdTrab, this.Name);
                iins = Int32.Parse(Permisos.Rows[0][3].ToString());

                //llena combo tipo de nomina
                Util.p_inicbo = 0;
                DataTable dtcbtipnom = ProcesaInc.cbtiponomina(8);
                Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "Clave", "Descripción");
                Util.p_inicbo = 1;

                if (iins == 1)
                {
                    pnlprocesoinc.Visible = true;
                }
                else
                {
                    pnlprocesoinc.Visible = false;
                }

            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void cbtiponomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Util.p_inicbo == 1)
                {
                    //valida se seleccione un periodo
                    if (Int32.Parse(cbtiponomina.SelectedValue.ToString())==0)
                    {
                        txtfecini.Text = "";
                        txtfecfin.Text = "";
                    }
                    else
                    {
                        //dt fechas
                        DataTable dtfechas = ProcesaInc.dttiponomina(9, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                        txtfecini.Text = dtfechas.Rows[0][2].ToString();
                        txtfecfin.Text = dtfechas.Rows[0][3].ToString();
                        istprocesaper = Int32.Parse(dtfechas.Rows[0][4].ToString());
                        if (istprocesaper == 0)
                        {
                            btnguardar.Enabled = true;
                        }
                        else
                        {
                            btnImprimir.Visible = true;
                            btnguardar.Enabled = false;
                            DialogResult result = MessageBox.Show("Este periodo ya fue procesado, solo puede consultar los registros de checadas", "SIPAA", MessageBoxButtons.OK);
                        }
                    }
                    dgvregistros.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnImprimir.Enabled = false;
                //valida se seleccione un periodo
                if (Int32.Parse(cbtiponomina.SelectedValue.ToString()) == 0)
                {
                    DialogResult result = MessageBox.Show("Seleccione un tipo de nomina", "SIPAA");
                    cbtiponomina.Focus();
                }
                else
                {
                    Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Espere por favor, buscando registros...");
                    //imprime reporte
                    DataTable dtreporte = new DataTable();
                    dtreporte = ProcesaInc.dgvregistros(7, 0, 0, "", "", Int32.Parse(cbtiponomina.SelectedValue.ToString()), 0, 0, LoginInfo.IdTrab, this.Name);

                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    rep_glinc dtrpt = new rep_glinc();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtreporte, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

                    form.RptDoc = ReportDoc;
                    form.Show();




                }
                btnImprimir.Enabled = true;
                Cursor.Current = Cursors.Default;

                pnlmenssuid.Visible = false;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //funcion para tool tip
        //funcion para tool tip
        private void ftooltip()
        {
                //crea tool tip
                ToolTip toolTip1 = new ToolTip();

                //configuraciòn
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 500;
                toolTip1.ShowAlways = true;

                //configura texto del objeto
                toolTip1.SetToolTip(this.btncerrar, "Cerrar Sistema");
                toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
                toolTip1.SetToolTip(this.btnregresar, "Regresar");
                toolTip1.SetToolTip(this.btnbuscar, "Busca Registro");
        }

        private void fgregistros(int iopc, int icveperiodo, int iidtrab)
        {
            DataTable dtcompania = ProcesaInc.dgvregistros(iopc, 0, 0, "", "", icveperiodo, 0, 0, LoginInfo.IdTrab, this.Name);
            dgvregistros.DataSource = dtcompania;

            dgvregistros.Columns[0].Width = 400;//empleado
            dgvregistros.Columns[1].Width = 140;//forma de pago
            dgvregistros.Columns[2].Width = 95;//horario
            dgvregistros.Columns[3].Width = 130;//reloj
            dgvregistros.Columns[4].Width = 100;//fecha
            dgvregistros.Columns[5].Width = 90;//registro

            dgvregistros.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
