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

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
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
            LlenarGridPlanteles(cbCia.SelectedItem.ToString(), txtBuscarPerfil.Text, dgvPlantel);
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Compania_Plantel_Load(object sender, EventArgs e)
        {

            lblusuario.Text = LoginInfo.Nombre;
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////


            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(1, "");
            List<string> ltCia = new List<string>();
            foreach (DataRow row in dtCia.Rows)
            {
                ltCia.Add(row["Descripción"].ToString());
            }
            cbCia.DataSource = ltCia;
            LlenarGridPlanteles("", "", dgvPlantel);
        }
        private void PanelPlantilla_Paint(object sender, PaintEventArgs e)
        {
            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(1, "");
            List<string> ltCia = new List<string>();
            foreach (DataRow row in dtCia.Rows)
            {
                ltCia.Add(row["Descripción"].ToString());
            }
            cbCia.DataSource = ltCia;
            LlenarGridPlanteles("", "", dgvPlantel);
        }

        private void cbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarGridPlanteles(cbCia.SelectedItem.ToString(), txtBuscarPerfil.Text, dgvPlantel);
        }
        private void txtBuscarPerfil_KeyUp(object sender, KeyEventArgs e)
        {
            LlenarGridPlanteles(cbCia.SelectedItem.ToString(), txtBuscarPerfil.Text, dgvPlantel);
        }

        private void PanelPlantilla_Paint_1(object sender, PaintEventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        public void LlenarGridPlanteles(string idcompania, string busqueda, DataGridView dgvPlantel)
        {


            SonaCompania objCia = new SonaCompania();
            DataTable dtPlantel = objCia.ObtenerPlantelxCompania(6, idcompania, busqueda, "");

            dgvPlantel.DataSource = dtPlantel;

            dgvPlantel.Columns[0].Visible = false;
            dgvPlantel.Columns[1].Visible = false;
            dgvPlantel.Columns[2].Visible = false;
            dgvPlantel.ClearSelection();


        }

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
