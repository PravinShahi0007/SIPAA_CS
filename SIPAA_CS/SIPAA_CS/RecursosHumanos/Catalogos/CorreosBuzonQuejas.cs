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
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.Properties;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class CorreosBuzonQuejas : Form
    {
        int iins, iact, ielim, iimp, iactbtn, icvcorreomodif;
        Perfil DatPerfil = new Perfil();
        Utilerias Util = new Utilerias();
        CorreosBuzonQuejasClase CorreosBuzon = new CorreosBuzonQuejasClase();
        SonaTrabajador contenedorempleados = new SonaTrabajador();

        public CorreosBuzonQuejas()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnregresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvcorreos.DataSource = null;

            int inumcolumngrid = dgvcorreos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvcorreos.Columns.RemoveAt(0);
            }

            //llena grid 
            fllenagridbusqueda(4, 0, 0, 0, txtcorreobusq.Text.Trim(), 0, "", "");
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dgvcorreos.DataSource = null;

            int inumcolumngrid = dgvcorreos.ColumnCount;
            if (inumcolumngrid == 1)
            {
                dgvcorreos.Columns.RemoveAt(0);
            }

            //llena grid
            fllenagridbusqueda(4, 0, 0, 0, "", 0, "", "");

            pnlcrudcorreo.Visible = true;
            Util.ChangeButton(btninsertar, 1, false);

            //Combo Planteles
            cbplantel.Text = "";
            DataTable dtplanteles = CorreosBuzon.dtdgvcbcorreo(10, 0, 0, 0, "", 0, "", "");
            Utilerias.llenarComboxDataTable(cbplantel, dtplanteles, "clave", "descrip");
            cbplantel.Focus();

            //Combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();

            txtcorreo.Text = "";
            ckbeliminar.Visible = false;
            iactbtn = 1;
            cbplantel.Focus();
        }

        private void btninsertar_Click(object sender, EventArgs e) //al dar guardar
        {
            //guarda datos
            if (iactbtn == 1)
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)
                { //insertar 
                    int ivali = CorreosBuzon.crudcorreo(1, 0, Int32.Parse(cbplantel.SelectedValue.ToString()), Int32.Parse(cbEmpleados.SelectedValue.ToString()), txtcorreo.Text.Trim(), 1, LoginInfo.IdTrab, this.Name);
                    if (ivali == 1)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, 0, 0, "", 0, "", "");
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        menssuid.Text = "Registro agregado correctamente";
                        timer1.Start();
                        pnlcrudcorreo.Visible = false;
                        iactbtn = 1;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("No agrego su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
            }
            else if (iactbtn == 2)
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)
                { //hay que pasar valores
                    int ivalu = CorreosBuzon.crudcorreo(2, icvcorreomodif, Int32.Parse(cbplantel.SelectedValue.ToString()), Int32.Parse(cbEmpleados.SelectedValue.ToString()), txtcorreo.Text.Trim(), 1, LoginInfo.IdTrab, this.Name);

                    if (ivalu == 2)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, 0, 0, "", 0, "", "");
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#0277bd");
                        menssuid.Text = "Registro modificado correctamente";
                        timer1.Start();
                        iactbtn = 0;
                        pnlcrudcorreo.Visible = false;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("No se modifico su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
            }
            else if (iactbtn == 3)
            {
                //eliminar registro
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int ivald = CorreosBuzon.crudcorreo(3, icvcorreomodif, 0, Int32.Parse(cbEmpleados.SelectedValue.ToString()), txtcorreo.Text.Trim(), 1, LoginInfo.IdTrab, this.Name);

                    if (ivald == 3)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, 0, 0, "", 0, "", "");
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#f44336");
                        menssuid.Text = "Registro eliminado correctamente";
                        timer1.Start();
                        ckbeliminar.Checked = false;
                        Util.ChangeButton(btninsertar, 2, false);
                        iactbtn = 0;
                        pnlcrudcorreo.Visible = false;
                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("No se elimmino su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    ckbeliminar.Checked = false;
                    cbplantel.Focus();
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Seleccione una acción a realizar", "SIPAA", MessageBoxButtons.OK);
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvcorreos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (iins == 1 && iact == 1 && ielim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 2;
            }
            else if (iins == 1 && iact == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
            else if (iins == 1 && ielim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 2;
            }
            else if (iact == 1 && ielim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 2;
            }
            else if (iins == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
            else if (iact == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
            else if (ielim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 3, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 3;
            }
            else
            {

            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void CorreosBuzonQuejas_Load(object sender, EventArgs e)
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

            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //inicializa tool tip
            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //variables accesos
            DataTable Permisos = DatPerfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            ielim = Int32.Parse(Permisos.Rows[0][5].ToString());
            iimp = Int32.Parse(Permisos.Rows[0][6].ToString());

            if (iins == 1)
            {
                btnAgregar.Visible = true;
            }

            if (iimp == 1)
            {
                //label3.Visible = true;
                //btnImprimircat.Visible = true;
            }

            //llena grid
            fllenagridbusqueda(4, 0, 0, 0,"", 0, "", "");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlmenssuid.Visible = false;
            timer1.Stop();
        }

        private void ckbeliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbeliminar.Checked == true)
            {
                Util.ChangeButton(btninsertar, 3, false);
                iactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
                cbplantel.Visible = true;
                cbEmpleados.Visible = true;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //funcion para tool tip
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
            toolTip1.SetToolTip(this.btnbuscar, "Busca Registro");
            //toolTip1.SetToolTip(this.btnbuscar, "Imprimir Catálogo");
        }
                
        protected void fllenagridbusqueda(int iopcion, int icvcorreo, int icvplantel, int iidtrab, string scorreo, int istcorreo, string susuumod, string sprgumod)
        {
            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else if (iins == 1 && iact == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else if (iins == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else if (iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else if (iins == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else if (iact == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else if (ielim == 1)
            {
                fdgvsuid(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
            else
            {
                fdgvs(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            }
        }

        //funcion formto grid con modificación busqueda con permisos
        protected void fdgvsuid(int iopcion, int icvcorreo, int icvplantel, int iidtrab, string scorreo, int istcorreo, string susuumod, string sprgumod)
        {
            dgvcorreos.DataSource = null;

            int inumcolumngrid = dgvcorreos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvcorreos.Columns.RemoveAt(0);
            }
                        
            DataTable dtdgvcapn = CorreosBuzon.dtdgvcbcorreo(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            dgvcorreos.DataSource = dtdgvcapn;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvcorreos.Columns.Insert(0, imgCheckUsuarios);
            dgvcorreos.Columns[0].HeaderText = "Selección";

            dgvcorreos.Columns[1].Visible = false;
            dgvcorreos.Columns[2].Width = 70;
            dgvcorreos.Columns[3].Width = 60;
            dgvcorreos.Columns[4].Width = 190;
            dgvcorreos.Columns[5].Width = 200;
            dgvcorreos.Columns[6].Width = 60;
            dgvcorreos.ClearSelection();
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(int iopcion, int icvcorreo, int icvplantel, int iidtrab, string scorreo, int istcorreo, string susuumod, string sprgumod)
        {
            DataTable dtdgvji = CorreosBuzon.dtdgvcbcorreo(iopcion, icvcorreo, icvplantel, iidtrab, scorreo, istcorreo, susuumod, sprgumod);
            dgvcorreos.DataSource = dtdgvji;

            dgvcorreos.Columns[0].Visible = false;
            dgvcorreos.Columns[1].Width = 220;
            dgvcorreos.Columns[2].Width = 80;
            dgvcorreos.Columns[3].Width = 80;
            dgvcorreos.Columns[4].Width = 220;
            dgvcorreos.Columns[5].Visible = false;
            dgvcorreos.Columns[6].Visible = false;
            dgvcorreos.ClearSelection();
        }

        private Boolean fvalidacampos() //Valida los campos capturados
        {
            if (cbplantel.Text.Trim() == "" || cbplantel.SelectedIndex == -1 || cbplantel.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un Plantel", "SIPAA", MessageBoxButtons.OK);
                cbplantel.Focus();
                return false;
            }
            else if (cbEmpleados.Text.Trim() == "" || cbEmpleados.SelectedIndex == -1 || cbEmpleados.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un Empleado", "SIPAA", MessageBoxButtons.OK);
                cbEmpleados.Focus();
                return false;
            }
            else if (txtcorreo.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Capture un correo electrónico", "SIPAA", MessageBoxButtons.OK);
                txtcorreo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void factgrid()
        {
            if (iins == 1 && iact == 0 && ielim == 0)
            {
            }
            else
            {
                for (int iContador = 0; iContador < dgvcorreos.Rows.Count; iContador++)
                {
                    dgvcorreos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                if (dgvcorreos.SelectedRows.Count != 0)
                {
                    pnlcrudcorreo.Visible = true;
                    DataGridViewRow row = this.dgvcorreos.SelectedRows[0];
                    icvcorreomodif = Convert.ToInt32(row.Cells["cvcorreo"].Value.ToString());

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    //Combo Planteles
                    cbplantel.DataSource = null; ;
                    DataTable dtplanteles = CorreosBuzon.dtdgvcbcorreo(10, 0, 0, 0, "", 0, "", "");
                    Utilerias.llenarComboxDataTable(cbplantel, dtplanteles, "clave", "descrip");
                    cbplantel.SelectedValue = Convert.ToInt32(row.Cells["cvplantel"].Value.ToString());
                    //cbplantel.Focus();

                    //Combo Empleados
                    cbEmpleados.DataSource = null;
                    DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
                    Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
                    cbEmpleados.SelectedValue = Convert.ToInt32(row.Cells["idtrab"].Value.ToString());
                    //cbplantel.Focus();
                    txtcorreo.Text = row.Cells["Correo"].Value.ToString();
                }
            }
        }
    }
}
