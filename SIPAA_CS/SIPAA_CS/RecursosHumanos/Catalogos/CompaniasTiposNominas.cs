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


//***********************************************************************************************
//Autor: Benjamin Huizar Barajas
//Fecha creación: 23-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Tipos de Nómina
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class CompaniasTiposNominas : Form
    {
        #region Variables

        int iIns;
        int iAct;
        int iElim;

        bool bPuedeBuscar = false;
        bool bClickPrimeraVezCompania = true;

        #endregion

        SonaCompania2 oCompania = new SonaCompania2();
        SonaTipoNomina oSonaTipoNomina = new SonaTipoNomina();

        public CompaniasTiposNominas()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Benjamin Huizar Barajas
        //Fecha creación: 13-Mar-2017       Última Modificacion: dd-mm-aaaa
        //Descripción: Administra Días Festivos
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        //
        // Llena Combox Box de Compañias con la tabla de SONARH
        private void cbCompania_Click(object sender, EventArgs e)
        {
            if (bClickPrimeraVezCompania)
            {
                DataTable dtCompania = oCompania.obtCompania2(5, "");
                cbCompania.DataSource = dtCompania;
                cbCompania.DisplayMember = "Descripción";
                cbCompania.ValueMember = "Clave";
                bClickPrimeraVezCompania = false;
            } // if (bClickPrimeraVezCompania)
        } // private void cbCompania_Click(object sender, EventArgs e)

        private void cbCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIdCompaniaBuscar;
            if (!bClickPrimeraVezCompania)
            {
                if (!bPuedeBuscar)
                {
                    bPuedeBuscar = true;
                    btnBuscar.Enabled = true;
                    txtBuscarTipoNomina.Enabled = true;
                } // if (!bPuedeBuscar)
                txtBuscarTipoNomina.Text = "";
                iIdCompaniaBuscar = Convert.ToInt32(cbCompania.SelectedValue.ToString());
                //
                // Llama a la función que procesa el DataGridView de Tipo de Nómina
                fgTiposNominas(5, iIdCompaniaBuscar, "");
            } // if (!bClickPrimeraVezCompania)
              //            }
        } // private void cbCompania_SelectedIndexChanged(object sender, EventArgs e)

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //Botón Buscar    
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int iIdCompaniaBuscar;
            if (bPuedeBuscar)
            {
                iIdCompaniaBuscar = Convert.ToInt32(cbCompania.SelectedValue.ToString());
                fgTiposNominas(4, iIdCompaniaBuscar, txtBuscarTipoNomina.Text);
            }
        } // private void btnBuscar_Click(object sender, EventArgs e)

        //Boton Minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        } // private void btnMinimizar_Click(object sender, EventArgs e)

        //boton cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                // No hace nada, se queda en la pantalla
            }
        } // private void btnCerrar_Click(object sender, EventArgs e)

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void CompaniasTiposNominas_Load(object sender, EventArgs e)
        {
            // Entorno de permisos
            iIns = 0;
            iAct = 0;
            iElim = 0;
        } // private void CompaniasTiposNominas_Load(object sender, EventArgs e)

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        //TOOL TIP PARA OBJETOS
        private void fTooltip()
        {

            //CREA TOOL TIP
            ToolTip toolTip1 = new ToolTip();

            //CONFIGURACION
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            // CONFIGURA EL TEXTO POR OBJETO
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            //toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            //toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
            //toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            //toolTip1.SetToolTip(this.btnInsertar, "Insertar Registro");

        } // private void fTooltip()

        //
        // Grid para mostrar Tipos de Nómina
        private void fgTiposNominas(int iOpcion, int iIdCompania, string sDescripcionTipoNomina)
        {
            DataTable dtTipoNomina = oSonaTipoNomina.obtTipoNomina(iOpcion, iIdCompania, 0, sDescripcionTipoNomina);
            dgvTiposNomina.DataSource = dtTipoNomina;
            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            //
            if (dgvTiposNomina.Columns.Count > 3)
            {
                dgvTiposNomina.Columns.RemoveAt(0);
            }
            //
            dgvTiposNomina.Columns.Insert(0, imgCheckUsuarios);
            dgvTiposNomina.Columns[0].HeaderText = "Selección";

            dgvTiposNomina.Columns[0].Width = 75;
            dgvTiposNomina.Columns[1].Visible = false;
            dgvTiposNomina.Columns[2].Width = 80;
            dgvTiposNomina.Columns[3].Width = 380;
            dgvTiposNomina.ClearSelection();
        } // private void fgTiposNominas()


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    } // public partial class CompaniasTiposNominas : Form
} // namespace SIPAA_CS.RecursosHumanos.Catalogos
