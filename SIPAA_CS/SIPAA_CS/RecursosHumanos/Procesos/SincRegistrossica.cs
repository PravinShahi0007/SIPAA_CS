using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using static SIPAA_CS.App_Code.Usuario;


//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
//Descripción: sincroniza sica provicional
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class SincRegistrossica : Form
    {

        SincRegistrosica SincReg = new SincRegistrosica();
        SonaTrabajador InsReg = new SonaTrabajador();
        Utilerias Util = new Utilerias();
        ProcesaIncidencia ProcesaInc = new ProcesaIncidencia();

        int inum;

        public SincRegistrossica()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        private void cbtiponomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Util.p_inicbo == 1)
            {
                Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Espere por favor, buscando registros...");
                Cursor.Current = Cursors.WaitCursor;
                
                DataTable dtemplsinc = SincReg.cbsincsica(16, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                string sdatos = dtemplsinc.Rows[0][0].ToString();

                string icaracttotal = (sdatos.Length.ToString());
                int ivalor = Int32.Parse(icaracttotal) - 1;

                string sdatosbusq = sdatos.Substring(0,ivalor);


                DataTable dtfechas = ProcesaInc.dttiponomina(14, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                string sfecini = dtfechas.Rows[0][2].ToString();
                string sfecfin = dtfechas.Rows[0][3].ToString();

                DataTable dtregsica = SincReg.sincregsica(sfecini, sfecfin, sdatosbusq);
                dgvregistros.DataSource = dtregsica;

                dgvregistros.Columns[0].Width = 400;//empleado
                dgvregistros.Columns[1].Width = 140;//fecha
                dgvregistros.Columns[2].Width = 130;//reloj

                pnlmenssuid.Visible = false;
                Cursor.Current = Cursors.Default;

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

        private void btnsinc_Click(object sender, EventArgs e)
        {
            try
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)
                {

                    //re-procesa incidencias periodo
                    DialogResult resultp = MessageBox.Show("Esta acción sincroniza SICA -- SIPAA ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                    if (resultp == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        btnsinc.Enabled = false;

                        Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Espere por favor, descargando registros...");

                        DataTable dtempleados = SincReg.cbsincsica(17, Int32.Parse(cbtiponomina.SelectedValue.ToString()));

                        DataTable dtfechas = ProcesaInc.dttiponomina(14, Int32.Parse(cbtiponomina.SelectedValue.ToString()));
                        string sfecini = dtfechas.Rows[0][2].ToString();
                        string sfecfin = dtfechas.Rows[0][3].ToString();

                        int contador = 1;
                        int inumeroempleado = dtempleados.Rows.Count + 1;

                        foreach (DataRow row in dtempleados.Rows)
                        {
                            int iporcentajeavance = (contador * 100) / inumeroempleado;

                            string iidempleado = row["idtrab"].ToString();
                            string sempleado = row["empleado"].ToString();

                            Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 2, "Sincronizando registros, Empleado: " + sempleado + ", " + contador + "  de  " + inumeroempleado + " Avance: " + iporcentajeavance + " %");


                            DataTable dtsipaasicasinc = SincReg.dtsincsicasipa(sfecini, sfecfin, iidempleado);

                            foreach (DataRow row1 in dtsipaasicasinc.Rows)
                            {
                                string sidtrab = row1["idtrab"].ToString();
                                string sferegistro = row1["fecha"].ToString();
                                string shrregistro = row1["registro"].ToString();

                                InsReg.Relojchecador(sidtrab, 5, DateTime.Parse(sferegistro), 0, TimeSpan.Parse(shrregistro), 6, 0, LoginInfo.IdTrab, this.Name);
                            }
                            contador = contador + 1;
                        }

                        //llena combo tipo de nomina
                        Util.p_inicbo = 0;
                        DataTable dtcbtipnom = SincReg.cbsincsica(15, 0);
                        Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "cvperiodo", "descripcion");
                        Util.p_inicbo = 1;

                        dgvregistros.DataSource = null;

                        btnsinc.Enabled = true;
                        Cursor.Current = Cursors.Default;

                        Utilerias.ControlNotificaciones(pnlmenssuid, menssuid, 1, "Registros importados con exito");
                        DialogResult result = MessageBox.Show("Registros importados con exito", "SIPAA", MessageBoxButtons.OK);

                        pnlmenssuid.Visible = false;
                        cbtiponomina.Focus();
                        
                    }
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
        private void SincRegistrossica_Load(object sender, EventArgs e)
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
                //tool tip
                ftooltip();

                //usuario
                lblusuario.Text = LoginInfo.Nombre;
                Utilerias.cargaimagen(ptbimgusuario);

                //llena combo tipo de nomina
                Util.p_inicbo = 0;
                DataTable dtcbtipnom = SincReg.cbsincsica(15, 0);
                Utilerias.llenarComboxDataTable(cbtiponomina, dtcbtipnom, "cvperiodo", "descripcion");
                Util.p_inicbo = 1;

                cbtiponomina.Focus();
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

        private Boolean fvalidacampos()
        {
            if (cbtiponomina.Text.Trim() == "" || cbtiponomina.SelectedIndex == -1 || cbtiponomina.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona una forma de pago", "SIPAA", MessageBoxButtons.OK);
                cbtiponomina.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
