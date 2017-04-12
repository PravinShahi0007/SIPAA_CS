using CrystalDecisions.CrystalReports.Engine;
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

namespace SIPAA_CS.Accesos.Reportes
{
    public partial class ViewerReporteModulosPerfiles : Form
    {
        public ReportDocument RptDoc;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public ViewerReporteModulosPerfiles()
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
            DialogResult result = MessageBox.Show("¿Seguro que deseas salir?", "SIPAA", MessageBoxButtons.YesNo);

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
        private void ViewerReporteModulosPerfiles_Load(object sender, EventArgs e)
        {
            //// Se crea lista de permisos por pantalla
            //LoginInfo.dtPermisosTrabajador = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab);
            //DataRow[] row = LoginInfo.dtPermisosTrabajador.Select("CVModulo = '" + this.Tag + "'");
            //LoginInfo.ltPermisosPantalla = Utilerias.CrearListaPermisoxPantalla(row, LoginInfo.ltPermisosPantalla);
            ////////////////////////////////////////////////////////
            //// resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //// variables de permisos
            //Permisos.Crear = Utilerias.ControlPermiso("Crear", LoginInfo.ltPermisosPantalla);
            //Permisos.Actualizar = Utilerias.ControlPermiso("Actualizar", LoginInfo.ltPermisosPantalla);
            //Permisos.Eliminar = Utilerias.ControlPermiso("Eliminar", LoginInfo.ltPermisosPantalla);
            //Permisos.Imprimir = Utilerias.ControlPermiso("Imprimir", LoginInfo.ltPermisosPantalla);
            //////////////////////////////////////////////////////////////////////////////////////////
            ReporteView.ReportSource = RptDoc;
        }
        
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


    }
}
