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

    public partial class Trabajadores : Form
    {
        public Trabajadores()
        {
            InitializeComponent();
        }
        SonaTrabajador contenedorempleados = new SonaTrabajador();
        Utilerias util = new Utilerias();

        //boton buscar empleados
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //llena grid
            if (txtEmpleado.Text == String.Empty)
            {
                fgridEmpleados(1, txtEmpleado.Text.Trim());
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
            }
            else
            {
                if (radioBtnnempleado.Checked== true & radioBtnapaterno.Checked == false & radioBtnAmbos.Checked==true)
                {
                    fgridEmpleados(1, txtEmpleado.Text.Trim());
                    txtEmpleado.Text = "";
                    txtEmpleado.Focus();
                }
                else
                {
                    if (radioBtnnempleado.Checked == false & radioBtnapaterno.Checked == true & radioBtnAmbos.Checked == true)
                    {
                        fgridEmpleados(2, txtEmpleado.Text.Trim());
                        txtEmpleado.Text = "";
                        txtEmpleado.Focus();
                    }
                }
            }
            //Conbinacion de consultas sencillas
            if (radioBtnnempleado.Checked== true & radioBtnActivo.Checked==true)
            {
                fgridEmpleados(3, txtEmpleado.Text.Trim());
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
            }

            if (radioBtnnempleado.Checked == true & radioBtnInactivo.Checked == true)
            {
                fgridEmpleados(4, txtEmpleado.Text.Trim());
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
            }

            if (radioBtnapaterno.Checked == true & radioBtnActivo.Checked == true)
            {
                fgridEmpleados(5, txtEmpleado.Text.Trim());
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
            }

            if (radioBtnapaterno.Checked == true & radioBtnInactivo.Checked == true)
            {
                fgridEmpleados(6, txtEmpleado.Text.Trim());
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
            }
            //fgridEmpleados(1, txtEmpleado.Text.Trim());
            //txtEmpleado.Text = "";
            //txtEmpleado.Focus();


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

        private void Empleados_Load(object sender, EventArgs e)
        {
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

            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //llama el tooltip
            ftooltip();

            //pone el foco en el campo de busqueda
            txtEmpleado.Focus();

            //llena grid
            fgridEmpleados(1,"");
        }

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

            //dgvEmpleados.Columns[0].Visible = false;
            //dgvEmpleados.Columns[1].Width = 355;
            //dgvComp.Columns[2].Width = 125;
            dgvEmpleados.ClearSelection();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }
    }
}
