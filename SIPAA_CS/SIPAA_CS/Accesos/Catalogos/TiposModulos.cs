using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.Accesos;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.App_Code.Accesos.Catalogos;
using SIPAA_CS.Properties;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:12-jul-2018       Última Modificacion: dd-mm-aaaa
//Descripción: catalogo de tipos de modulos
//***********************************************************************************************

namespace SIPAA_CS.Accesos.Catalogos
{
    public partial class TiposModulos : Form
    {
        //clases
        Utilerias cutilerias = new Utilerias();
        Perfil cperfil = new Perfil();
        TipoModulo ctipomodulo = new TipoModulo();

        #region
        int iins, iact, ielim;
        int iactbtn, icvtipomodulomodif;
        #endregion

        public TiposModulos()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        //accion al tocar grid conforme a permisos del usuario
        private void dgvjustinc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (iins == 1 && iact == 1 && ielim == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 2, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 2;
            }
            else if (iins == 1 && iact == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
            else if (iins == 1 && ielim == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 2, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 2;
            }
            else if (iact == 1 && ielim == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 2, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 2;
            }
            else if (iins == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
            else if (iact == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
            else if (ielim == 1)
            {
                factgrid();
                cutilerias.ChangeButton(btninsertar, 3, false);
                ckbeliminar.Visible = true;
                ckbeliminar.Checked = false;
                iactbtn = 3;
            }
            else
            {

            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton regresar
        private void btnregresar_Click(object sender, EventArgs e)
        {
            AcceDashboard faccedashboard = new AcceDashboard();
            faccedashboard.Show();
            this.Close();
        }

        //boton minimizar
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //boton agregar
        private void btnagregar_Click(object sender, EventArgs e)
        {
            dgvdatos.DataSource = null;
            int inumcolumngrid = dgvdatos.ColumnCount;
            if (inumcolumngrid == 1) { dgvdatos.Columns.RemoveAt(0); }

            //llena grid
            fllenagridbusqueda(4, 0, "", 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

            pnlcrud.Visible = true;
            cutilerias.ChangeButton(btninsertar, 1, false);
            ckbeliminar.Visible = false;
            iactbtn = 1;

            //limpia y carga objetos
            flimponj();
        }

        //boton buscar
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvdatos.DataSource = null;
            int inumcolumngrid = dgvdatos.ColumnCount;
            if (inumcolumngrid == 1) { dgvdatos.Columns.RemoveAt(0); }

            //llena grid
            fllenagridbusqueda(4, 0, txtbusqueda.Text.Trim(), 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());
        }

        //boton guardar
        private void btninsertar_Click(object sender, EventArgs e)
        {
            //guarda datos
            if (iactbtn == 1)
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)
                {
                    int ivali = ctipomodulo.cruddatos(1, 0, txtdesc.Text.Trim(), 1, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

                    if (ivali == 1)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, "", 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        menssuid.Text = "Registro agregado correctamente";
                        timer1.Start();
                        flimponj();
                        iactbtn = 1;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("No se agrego su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
            }
            else if (iactbtn == 2)
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)
                {
                    int ivalu = ctipomodulo.cruddatos(2, icvtipomodulomodif, txtdesc.Text.Trim(), 1, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

                    if (ivalu == 2)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, "", 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#0277bd");
                        menssuid.Text = "Registro modificado correctamente";
                        timer1.Start();
                        flimponj();
                        iactbtn = 0;
                        icvtipomodulomodif = 0;
                        pnlcrud.Visible = false;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("No se modifico su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
            }
            else if (iactbtn == 3)
            {
                ////eliminar registro
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int ivald = ctipomodulo.cruddatos(3, icvtipomodulomodif, txtdesc.Text.Trim(), 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

                    if (ivald == 3)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, "", 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#f44336");
                        menssuid.Text = "Registro eliminado correctamente";
                        timer1.Start();
                        flimponj();
                        iactbtn = 0;
                        icvtipomodulomodif = 0;
                        pnlcrud.Visible = false;
                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("No se elimmino su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    ckbeliminar.Checked = false;
                    txtdesc.Focus();
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Seleccione una acción a realizar", "SIPAA", MessageBoxButtons.OK);
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        //al cargar
        private void TiposModulos_Load(object sender, EventArgs e)
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
            DataTable Permisos = cperfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            ielim = Int32.Parse(Permisos.Rows[0][5].ToString());

            //agregar
            if (iins == 1) { btnagregar.Visible = true; }

            //llena grid
            fllenagridbusqueda(4, 0, "", 0, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());
        }

        //timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlmenssuid.Visible = false;
            timer1.Stop();
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
            toolTip1.SetToolTip(this.btncerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
            toolTip1.SetToolTip(this.btnbuscar, "Busca Registro");
        }

        //funcion busqueda conforme a permisos
        protected void fllenagridbusqueda(int iopcion, int icvtipomodulo, string sdescripcion, int istmodulo, string susuumod,
                                          string sfhumod, string sprgumod, string sequmod)
        {

            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else if (iins == 1 && iact == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else if (iins == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else if (iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else if (iins == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else if (iact == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else if (ielim == 1)
            {
                fdgvsuid(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }
            else
            {
                fdgvs(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            }

        }

        //funcion formato grid con modificación busqueda con permisos
        protected void fdgvsuid(int iopcion, int icvtipomodulo, string sdescripcion, int istmodulo, string susuumod,
                                string sfhumod, string sprgumod, string sequmod)
        {
            dgvdatos.DataSource = null;

            int inumcolumngrid = dgvdatos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvdatos.Columns.RemoveAt(0);
            }

            DataTable dtdatos = ctipomodulo.dtdatos(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            dgvdatos.DataSource = dtdatos;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvdatos.Columns.Insert(0, imgCheckUsuarios);
            dgvdatos.Columns[0].HeaderText = "Selección";

            dgvdatos.Columns[0].Width = 75;
            dgvdatos.Columns[1].Visible = false;
            dgvdatos.Columns[2].Width = 500;
            dgvdatos.Columns[3].Visible = false;
            dgvdatos.ClearSelection();
            lblModif.Visible = true;
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(int iopcion, int icvtipomodulo, string sdescripcion, int istmodulo, string susuumod,
                             string sfhumod, string sprgumod, string sequmod)
        {
            DataTable dtdgvji = ctipomodulo.dtdatos(iopcion, icvtipomodulo, sdescripcion, istmodulo, susuumod, sfhumod, sprgumod, sequmod);
            dgvdatos.DataSource = dtdgvji;

            dgvdatos.Columns[0].Width = 500;
            dgvdatos.Columns[1].Visible = false;
            lblModif.Visible = false;
        }

        //funcion grid
        private void factgrid()
        {
            if (iins == 1 && iact == 0 && ielim == 0)
            {
            }
            else
            {
                for (int iContador = 0; iContador < dgvdatos.Rows.Count; iContador++)
                {
                    dgvdatos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                if (dgvdatos.SelectedRows.Count != 0)
                {
                    pnlcrud.Visible = true;

                    DataGridViewRow row = this.dgvdatos.SelectedRows[0];
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    icvtipomodulomodif = Convert.ToInt32(row.Cells["cve"].Value.ToString());
                    txtdesc.Text = row.Cells["Tipo de Módulo"].Value.ToString();
                    txtdesc.Focus();
                }
            }
        }

        //limpia carga objetos
        private void flimponj()
        {
            txtdesc.Text = "";
            txtdesc.Focus();
        }

        //validacion de campos
        private Boolean fvalidacampos()
        {
            if (txtdesc.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Captura una descripción", "SIPAA", MessageBoxButtons.OK);
                txtdesc.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //eliminar
        private void ckbeliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbeliminar.Checked == true)
            {
                cutilerias.ChangeButton(btninsertar, 3, false);
                iactbtn = 3;
            }
            else
            {
                cutilerias.ChangeButton(btninsertar, 2, false);
                iactbtn = 2;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    }
}