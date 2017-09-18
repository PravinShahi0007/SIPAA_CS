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
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Properties;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class VacIncPermHrEsp2 : Form
    {
        public VacIncPermHrEsp2()
        {
            InitializeComponent();
        }
        string NoTrabajador;

        SonaTrabajador contenedorempleados = new SonaTrabajador();
        Utilerias util = new Utilerias();

        //***********************************************************************************************
        //Autor: José Luis Alvarez Delgado
        //Fecha creación:14-09-2017     Última Modificacion: 
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvEmpleados.Rows.Count; iContador++)
            {
                dgvEmpleados.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvEmpleados.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvEmpleados.SelectedRows[0];

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                NoTrabajador = row.Cells["NoEmpleado"].Value.ToString();
                llenarGridDiasEsp(NoTrabajador);

                //DatosTrabajadorPerfil form = new DatosTrabajadorPerfil();
                //form.Show();
                //this.Close();
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar empleados
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //llena grid
            if (txtEmpleado.Text == String.Empty)
            {
                fgridEmpleados(3, txtEmpleado.Text.Trim()); //todos los activos x Num
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
            }
        }

        //boton minimizar        
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

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
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void VacIncPermHrEsp2_Load(object sender, EventArgs e)
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

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //llama el tooltip
            ftooltip();

            //pone el foco en el campo de busqueda
            txtEmpleado.Focus();

            //llena grid
            //fgridEmpleados(1,"");
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        /////////funciones. FUNCION tooltip
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

        //FUNCION que Llena el Grid de Empleados
        private void fgridEmpleados(int popc, string pbusq)
        {
            DataTable dtempleados = contenedorempleados.obtenerempleados(popc, pbusq);
            dgvEmpleados.DataSource = dtempleados;

            Utilerias.AgregarCheck(dgvEmpleados, 0);
            dgvEmpleados.ClearSelection();
        }

        private void llenarGridDiasEsp(string NoTrabajador)
        {
            DiasEspeciales objDia = new DiasEspeciales();

            objDia.sIdTrab = NoTrabajador;
            DataTable dtdias = objDia.ObtenerDiasEspecialesxTrabajador(objDia, 4);

            dgvInc.DataSource = dtdias;
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


    }
}
