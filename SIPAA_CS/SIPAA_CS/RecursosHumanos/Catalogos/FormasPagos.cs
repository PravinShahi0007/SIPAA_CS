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
using SIPAA_CS.Conexiones;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using static SIPAA_CS.App_Code.Usuario;

//***********************************************************************************************
//Autor: Marco Dupont               modif: noe alvarez marquina(se agrega funcionalidad, estandar, imagen, usuario)
//Fecha creación: 28-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Consulta Catálogo de Formas de PAgo SonarH
//***********************************************************************************************


namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class FormasPagos : Form
    {

        SonaFormaPago CFormaPago = new SonaFormaPago();
        public FormasPagos()
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
        //BOTON BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //LLAMA METODO LLENAR GRID
            SLlenaGrid(4, 0, txtBusca.Text.Trim());
            txtBusca.Text = "";
            txtBusca.Focus();
        }
        //BOTON CERRAR
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
        //BOTON REGRESAR
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }
        //BOTON MINIMIZAR   
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void FoarmasPagos_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FormasPagos.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            //Configuracion de la pantalla
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            //LLAMA TOOL TIP
            sTooltip();

            //LLAMA METODO LLENAR GRID
            SLlenaGrid(4, 0, "");

        }
        //-----------------------------------------------------------------------------------------------
        //                                  S U B R U T I N A S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        //TOOL TIP PARA OBJETOS
        private void sTooltip()
        {

            //CREA TOOL TIP
            ToolTip toolTip1 = new ToolTip();

            //CONFIGURACION
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            // CONFIGURA EL TEXTO POR OBJETO
            toolTip1.SetToolTip(this.btnCerrar, "Cierra Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimiza Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresa");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }

        //LLENA GRID
        private void SLlenaGrid(int p_opcion, int p_idforma, string p_descripcion)
        {

            DataTable dtConsulta = CFormaPago.FormaPago_S(p_opcion, p_idforma, p_descripcion);
            dgvConsulta.DataSource = dtConsulta;

            dgvConsulta.Columns[0].Visible = false;
            dgvConsulta.Columns[1].Width = 210;
            txtBusca.Focus();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
