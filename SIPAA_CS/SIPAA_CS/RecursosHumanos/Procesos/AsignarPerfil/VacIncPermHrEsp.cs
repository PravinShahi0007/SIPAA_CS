using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class VacIncPermHrEsp : Form
    {
        public VacIncPermHrEsp()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:17-05-04      Última Modificacion: 17-05-04    
        //Descripción:
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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void VacIncPermHrEsp_Load(object sender, EventArgs e)
        {

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ////////////////////////////////////////////////////
            llenarGrid();

            lbIdTrab.Text = TrabajadorInfo.IdTrab;
            lbNombre.Text = TrabajadorInfo.Nombre;
            lblusuario.Text = LoginInfo.Nombre;

        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void llenarGrid()
        {

            DiasEspeciales objDia = new DiasEspeciales();

            objDia.sIdTrab = TrabajadorInfo.IdTrab;
            DataTable dtdias = objDia.ObtenerDiasEspecialesxTrabajador(objDia, 4);

            dgvInc.DataSource = dtdias;
        }




        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
