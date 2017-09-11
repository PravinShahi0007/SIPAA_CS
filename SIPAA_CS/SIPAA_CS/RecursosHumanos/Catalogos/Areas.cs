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

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara  Modif:noe alvarez marquina 17/07/2017 (se agrega estandar y se ordena tabulador)
    //Fecha creación:23-03-2017       Última Modificacion: 23-03-2017
    //Descripción: Consulta a Sonar de Áreas
    //***********************************************************************************************
    public partial class Areas : Form
    {
        public Areas()
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
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbCia.SelectedValue.ToString() == "0")
            {
                DialogResult result = MessageBox.Show("Debé seleccionar una compañia", "SIPAA");
                cbCia.Focus();
            }
            else
            {
                LlenarGridPlanteles(cbCia.SelectedValue.ToString(), txtBuscarPerfil.Text, dgvPlantel, 6);
                txtBuscarPerfil.Text = "";
            }
            
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Compania_Plantel_Load(object sender, EventArgs e)
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

            lblusuario.Text = LoginInfo.Nombre;
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////


            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(4, "");

            Utilerias.llenarComboxDataTable(cbCia,dtCia,"Clave","Descripción");


            //LlenarGridPlanteles("", "", dgvPlantel,8);

            txtBuscarPerfil.Focus();

        }

        private void PanelPlantilla_Paint_1(object sender, PaintEventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //agrega noe alvarez marquina 17/07/2017
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
            toolTip1.SetToolTip(this.btnCerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Buscar Registros");
        }
        public void LlenarGridPlanteles(string idcompania, string busqueda, DataGridView dgvPlantel, int iopc)
        {


            SonaCompania objCia = new SonaCompania();
            DataTable dtPlantel = objCia.ObtenerPlantelxCompania(iopc, idcompania, busqueda, "");

            dgvPlantel.DataSource = dtPlantel;

            dgvPlantel.Columns[0].Visible = false;
            dgvPlantel.Columns[1].Visible = false;
            dgvPlantel.Columns[2].Visible = false;
            dgvPlantel.Columns[3].Width = 280;
            dgvPlantel.Columns[4].Width = 240;
            dgvPlantel.ClearSelection();


        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard inirh = new RechDashboard();
            inirh.Show();
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pnlBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------









    }
}
