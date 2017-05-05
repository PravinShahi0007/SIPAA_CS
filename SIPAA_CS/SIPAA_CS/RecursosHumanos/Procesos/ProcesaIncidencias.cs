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


//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación:28-abr-2017       Última Modificacion: dd-mm-aaaa
//Descripción: proceso de creación de incidencias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class ProcesaIncidencias : Form
    {
        public ProcesaIncidencias()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        //                         V A R I A B L E S    L O C A L E S
        //-----------------------------------------------------------------------------------------------

        #region

        int iins, iact, ielim;// variables permisos de insertar, actualizar, eliminar
        int iactbtn;// actión de realizar botón

        int iresp;// variable de respuesta-acción realizada

        #endregion

        Utilerias Util = new Utilerias();
        ProcesaIncidencia ProcesaInc = new ProcesaIncidencia();

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
                }
                else
                {
                    //llena grid
                    fgregistros(4, Int32.Parse(cbtiponomina.SelectedValue.ToString()),0);
                }
                btnbuscar.Enabled = true;
                Cursor.Current = Cursors.Default;
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
                //función para tool tip
                ftooltip();

                //actualiza accesos
                iins = 1;

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
                    }
                    dgvregistros.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }

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

        private void fgregistros(int iopc, int iformapago, int iidtrab)
        {

            DataTable dtcompania = ProcesaInc.dgvregistros(iopc, iformapago, iidtrab);
            dgvregistros.DataSource = dtcompania;

            dgvregistros.Columns[0].Width = 300;//empleado
            dgvregistros.Columns[1].Width = 90;//forma de pago
            dgvregistros.Columns[2].Width = 90;//horario
            dgvregistros.Columns[3].Width = 80;//reloj
            dgvregistros.Columns[4].Width = 90;//fecha
            dgvregistros.Columns[5].Width = 130;//registro

            dgvregistros.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
