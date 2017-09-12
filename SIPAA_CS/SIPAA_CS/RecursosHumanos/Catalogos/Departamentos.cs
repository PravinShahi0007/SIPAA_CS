using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.Properties;
using SIPAA_CS.App_Code;

using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Catalogos
{

    #region variables


    #endregion

    public partial class Departamentos : Form
    {
        //public int sysH = SystemInformation.PrimaryMonitorSize.Height;
        //public int sysW = SystemInformation.PrimaryMonitorSize.Width;
        Utilerias util = new Utilerias();
        public Departamentos()
        {
            InitializeComponent();
        }
        SonaDepartamento departamentos = new SonaDepartamento();

        //***********************************************************************************************
        //Autor:                          modif: noe alvarez marquina (se arreglan llamados a sp conforme al standar)
        //Fecha creación:dd-mm-aaaa       Última Modificacion: 20/07/2017
        //Descripción: Lee la tabla de compañias de SONARH
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

        //boton buscar compañia 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //llena grid
            fgdeptos(5, txtComp.Text.Trim());
            txtComp.Text = "";
            txtComp.Focus();
        }

        //boton minimizar        
        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btnCerrar_Click_1(object sender, EventArgs e)
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
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Departamentos_Load(object sender, EventArgs e)
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

            //Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            //inicializa tool tip
            ftooltip();

            txtComp.Focus();

            //llena grid
            fgdeptos(4, "");

        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Buscar Registros");
        }

        //Llena el Grid de Departamentos
        private void fgdeptos(int popc, string pbusq)
        {

            DataTable dtdepartamentos = departamentos.obtdepto(popc, pbusq);
            dgvComp.DataSource = dtdepartamentos;

            dgvComp.Columns[0].Visible = false;
            dgvComp.Columns[1].Width = 355;
            //dgvComp.Columns[2].Width = 125;
            dgvComp.ClearSelection();
        }
    }
}
