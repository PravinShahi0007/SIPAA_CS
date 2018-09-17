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
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.RecursosHumanos.Reportes;
using SIPAA_CS.Properties;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;

using CrystalDecisions.CrystalReports.Engine;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:11-sep-2018       Última Modificacion: dd-mm-aaaa
//Descripción: administra sanciones creadas automaticamente
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class SancionesProcesos : Form
    {

        //clases
        #region
        int iins, iact, ielim, iimp;
        #endregion

        Perfil cperfil = new Perfil();
        Utilerias cutilerias = new Utilerias();
        SancionesProceso scancionesproceso = new SancionesProceso();

        public SancionesProcesos()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        private void cbempleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cutilerias.p_inicbo == 1)
            {
                //llena sanciones
                cutilerias.p_inicbo = 0;
                DataTable dtcbsanciones = scancionesproceso.dtdatos(5, Int32.Parse(cbempleado.SelectedValue.ToString()), "", 0, 0, 0, 0, 0, "", "", "", "", "", "", 0, "");
                Utilerias.llenarComboxDataTable(cbsancion, dtcbsanciones, "cv", "desc");
                cutilerias.p_inicbo = 1;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

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

        private void btninsertar_Click(object sender, EventArgs e)
        {
            if (opcaplicar.Checked == false && opccancelar.Checked == false)
            {
                DialogResult result = MessageBox.Show("Seleccione una acción APLICAR o CANCELAR", "SIPAA", MessageBoxButtons.OK);
            }
        }

        //botón buscar
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            DataTable dtsanciones = scancionesproceso.dtdatos(6, Int32.Parse(cbempleado.SelectedValue.ToString()), "", 0, 0, Int32.Parse(cbsancion.SelectedValue.ToString()), 0, 0, "", "", "", "", "", "", 0, "");
            dgvdatos.DataSource = dtsanciones;

            dgvdatos.Columns[0].HeaderText = "Fecha";//fecha
            dgvdatos.Columns[0].Width = 100;
            dgvdatos.Columns[1].HeaderText = "Incidencia";//incidencia
            dgvdatos.Columns[1].Width = 240;
            dgvdatos.Columns[2].HeaderText = "Tiempo empleado";//tiempo empleado
            dgvdatos.Columns[2].Width = 110;
            dgvdatos.Columns[3].HeaderText = "Tiempo profesor";//tiempo profesor
            dgvdatos.Columns[3].Width = 110;
            dgvdatos.ClearSelection();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        // cargar
        private void SancionesProcesos_Load(object sender, EventArgs e)
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

            //inicializa tool tip
            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //variables accesos
            DataTable Permisos = cperfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            ielim = Int32.Parse(Permisos.Rows[0][5].ToString());

            //llena empleados
            cutilerias.p_inicbo = 0;
            DataTable dtcbempleado = scancionesproceso.dtdatos(4,0, "", 0, 0, 0, 0, 0, "", "", "", "", "", "", 0, "");
            Utilerias.llenarComboxDataTable(cbempleado, dtcbempleado, "cv", "empleado");
            cutilerias.p_inicbo = 1;
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
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    }
}
