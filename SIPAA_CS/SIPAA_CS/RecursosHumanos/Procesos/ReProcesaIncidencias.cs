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
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;


//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación: 13-nov-2017       Última Modificacion: dd-mm-aaaa
//Descripción: re-procesa incidencias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class ReProcesaIncidencias : Form
    {
        public ReProcesaIncidencias()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                         V A R I A B L E S    L O C A L E S
        //-----------------------------------------------------------------------------------------------

        #region

        int iins;// variables permisos de insertar, actualizar, eliminar

        #endregion

        Perfil DatPerfil = new Perfil();
        Utilerias Util = new Utilerias();
        ProcesaIncidencia ProcesaInc = new ProcesaIncidencia();

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        private void cbtiponomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (opcperiodo.Checked == true)
                {
                    if (Util.p_inicbo == 1)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        fgregistros(6, Int32.Parse(cbtiponomina.SelectedValue.ToString()), 0);
                        Cursor.Current = Cursors.Default;
                    }
                }
                else if (opcempleado.Checked == true)
                {
                    if (Util.p_inicbo == 1)
                    {
                        //valida se seleccione un periodo
                        if (Int32.Parse(cbtiponomina.SelectedValue.ToString()) == 0)
                        {
                            cbotrab.DataSource = null;
                        }
                        else
                        {
                            //llena empleados
                            Util.p_inicbo = 0;
                            DataTable dttrab = ProcesaInc.cbincrp(13, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                            Utilerias.llenarComboxDataTable(cbotrab, dttrab, "clave", "descr");
                            Util.p_inicbo = 1;
                        }
                        dgvregistros.DataSource = null;
                    }
                }
                else
                {
                }


            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void cbotrab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Util.p_inicbo == 1 && opcempleado.Checked == true)
                {
                    //valida se seleccione un periodo
                    if (Int32.Parse(cbotrab.SelectedValue.ToString()) == 0)
                    {
                        dgvregistros.DataSource = null;
                    }
                    else
                    {
                        //llena grid
                        fgregistros(6, Int32.Parse(cbtiponomina.SelectedValue.ToString()), Int32.Parse(cbotrab.SelectedValue.ToString()));
                    }

                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnregresar_Click(object sender, EventArgs e)
        {
            try
            {
                RechDashboard rechdb = new RechDashboard();
                rechdb.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (opcperiodo.Checked == true)
                {
                    //re-procesa incidencias periodo
                    DialogResult resultp = MessageBox.Show("Esta acción re-proceasa las incidencias del periodo:" + "\r\n" + "\r\n" + cbtiponomina.Text + "\r\n" + "\r\n" + "¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                    if (resultp == DialogResult.Yes)
                    {

                        Cursor.Current = Cursors.WaitCursor;
                        btnguardar.Enabled = false;

                        //dt fechas
                        DataTable dtfechas = ProcesaInc.dttiponomina(14, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                        string sfecini = dtfechas.Rows[0][2].ToString();
                        string sfecfin = dtfechas.Rows[0][3].ToString();
                        int iformapago = Int32.Parse(dtfechas.Rows[0][0].ToString());

                        int ival = ProcesaInc.vuidreprocesainc(5, iformapago, 0, sfecini, sfecfin,
                                                               Int32.Parse(cbtiponomina.SelectedValue.ToString()),0, LoginInfo.IdTrab, this.Name);

                        DialogResult result = MessageBox.Show("Incidencias re-procesadas con exito", "SIPAA", MessageBoxButtons.OK);

                        //llena combo tipo de nomina
                        Util.p_inicbo = 0;
                        DataTable dtcbtipnom = ProcesaInc.cbincrp(12, 0);
                        Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "cvperiodo", "descripcion");
                        Util.p_inicbo = 1;

                        dgvregistros.DataSource = null;
                        cbtiponomina.Focus();
                        btnguardar.Enabled = true;
                        Cursor.Current = Cursors.Default;

                    }
                    else
                    {
                        cbtiponomina.Focus();
                    }

                }
                else if (opcempleado.Checked == true)
                {

                    //re-procesa incidencias empleado
                    DialogResult result = MessageBox.Show("Esta acción re-proceasa las incidencias del empleado:" + "\r\n" + "\r\n" + cbotrab.Text + "\r\n" + "\r\n" + "¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {

                        Cursor.Current = Cursors.WaitCursor;
                        btnguardar.Enabled = false;

                        //dt fechas
                        DataTable dtfechas = ProcesaInc.dttiponomina(14, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                        string sfecini = dtfechas.Rows[0][2].ToString();
                        string sfecfin = dtfechas.Rows[0][3].ToString();
                        int iformapago = Int32.Parse(dtfechas.Rows[0][0].ToString());

                        int ival = ProcesaInc.vuidreprocesainc(5, iformapago, Int32.Parse(cbotrab.SelectedValue.ToString()), sfecini, sfecfin,
                                                               Int32.Parse(cbtiponomina.SelectedValue.ToString()), 0, LoginInfo.IdTrab, this.Name);

                        DialogResult result2 = MessageBox.Show("Incidencias re-procesadas con exito", "SIPAA", MessageBoxButtons.OK);

                        //llena combo tipo de nomina
                        Util.p_inicbo = 0;
                        DataTable dtcbtipnom = ProcesaInc.cbincrp(12, 0);
                        Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "cvperiodo", "descripcion");
                        Util.p_inicbo = 1;
                        
                        dgvregistros.DataSource = null;
                        cbtiponomina.Focus();
                        btnguardar.Enabled = true;
                        Cursor.Current = Cursors.Default;

                    }
                    else
                    {
                        cbotrab.Focus();
                    }

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void ReProcesaIncidencias_Load(object sender, EventArgs e)
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
            //tool tip
            ftooltip();

            //usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //variables accesos
            DataTable Permisos = DatPerfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
        }

        private void opcperiodo_CheckedChanged(object sender, EventArgs e)
        {
            pnlformapago.Visible = true;
            pnlempleado.Visible = false;
            dgvregistros.DataSource = null;
            btnguardar.Visible = true;

            //llena combo tipo de nomina
            Util.p_inicbo = 0;
            DataTable dtcbtipnom = ProcesaInc.cbincrp(12, 0);
            Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "cvperiodo", "descripcion");
            Util.p_inicbo = 1;

            cbtiponomina.Focus();
        }

        private void opcempleado_CheckedChanged(object sender, EventArgs e)
        {
            pnlformapago.Visible = true;
            pnlempleado.Visible = true;
            dgvregistros.DataSource = null;
            btnguardar.Visible = true;

            //llena combo tipo de nomina
            Util.p_inicbo = 0;
            DataTable dtcbtipnom = ProcesaInc.cbincrp(12, 0);
            Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "cvperiodo", "descripcion");
            Util.p_inicbo = 1;

            cbotrab.DataSource = null;
            cbtiponomina.Focus();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //funcion para tool tip
        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btncerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
        }

        private void fgregistros(int iopc, int iformapago, int iidtrab)
        {
            DataTable dtcompania = ProcesaInc.dgvregistros(iopc, iformapago, iidtrab);
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
