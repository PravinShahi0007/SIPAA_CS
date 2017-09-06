using System;
using System.Data;
using System.Windows.Forms;
using SIPAA_CS.App_Code;

using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using static SIPAA_CS.App_Code.Usuario;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación: 13-mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Muestra y busca puestos sonarh
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class Puestos : Form
    {
        public Puestos()
        {
            InitializeComponent();
        }

        SonaPuesto puestos = new SonaPuesto();

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
            //llena grid
            fgptos(4, txtpuestos.Text.Trim());


            txtpuestos.Text = "";
            txtpuestos.Focus();
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnregresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
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
        private void Puestos_Load(object sender, EventArgs e)
        {
            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Companias.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            //inicializa tool tip
            ftooltip();

            txtpuestos.Focus();

            //llena grid
            fgptos(4, "");
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
            toolTip1.SetToolTip(this.btncerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
            toolTip1.SetToolTip(this.btnbuscar, "Buscar Registros");
        }
        private void fgptos(int popc, string pbusq)
        {
            DataTable dtpuestos = puestos.obtptos(popc, pbusq);
            dgvpuestos.DataSource = dtpuestos;

            dgvpuestos.Columns[0].Visible = false;
            dgvpuestos.Columns[1].Width = 475;

            dgvpuestos.ClearSelection();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
