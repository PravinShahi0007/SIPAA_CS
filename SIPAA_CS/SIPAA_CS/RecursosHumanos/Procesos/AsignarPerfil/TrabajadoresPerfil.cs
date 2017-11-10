using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
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
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class TrabajadoresPerfil : Form
    {
        #region
        int iins, iact, ielim;
        int iactbtn, istcheca;
        string sestperf, iidtrabmodif, istchec;
        #endregion

        Perfil DatPerfil = new Perfil();
        TrabajadorPerfil TrabPerf = new TrabajadorPerfil();
        Utilerias Util = new Utilerias();

       // int sysH = SystemInformation.PrimaryMonitorSize.Height;
        //int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public TrabajadoresPerfil()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara   modif: noe alvarez marquina  ****funcionalidad 
        //Fecha creación:7-04-2017     Última Modificacion: 28/09/2017
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvIncidencia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlsuid.Visible = false;
            DataGridViewRow row = this.dgvTrab.SelectedRows[0];
            sestperf = row.Cells["Perfil"].Value.ToString();

            if (sestperf == "Si")
            {
                if (iins == 1 && iact == 1 && ielim == 1)
                {
                    factgrid();
                    Util.ChangeButton(btninsertar, 2, false);
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
                    DialogResult result = MessageBox.Show("No tienes permisos de modificar el perfil", "SIPAA", MessageBoxButtons.OK);
                    //factgrid();
                    //Util.ChangeButton(btninsertar, 2, false);
                    //iactbtn = 2;
                }
                else if (iact == 1 && ielim == 1)
                {
                    factgrid();
                    Util.ChangeButton(btninsertar, 2, false);
                    iactbtn = 2;
                }
                else if (iins == 1)
                {
                    DialogResult result = MessageBox.Show("NNo tienes permisos de modificar el perfil", "SIPAA", MessageBoxButtons.OK);
                    //factgrid();
                    //Util.ChangeButton(btninsertar, 2, false);
                    //iactbtn = 2;
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
                    iactbtn = 3;
                }
                else
                {
                    DialogResult result = MessageBox.Show("No tienes permisos de modificar el perfil", "SIPAA", MessageBoxButtons.OK);
                }
            }
            else if (sestperf == "No")
            {
                if (iins == 1 && iact == 1 && ielim == 1)
                {
                    factgrid();
                    Util.ChangeButton(btninsertar, 1, false);
                    iactbtn = 1;
                }
                else if (iins == 1 && iact == 1)
                {
                    factgrid();
                    Util.ChangeButton(btninsertar, 1, false);
                    iactbtn = 1;
                }
                else if (iins == 1 && ielim == 1)
                {
                    factgrid();
                    Util.ChangeButton(btninsertar, 1, false);
                    iactbtn = 1;
                }
                else if (iact == 1 && ielim == 1)
                {
                    DialogResult result = MessageBox.Show("No tienes permisos para crear el perfil", "SIPAA", MessageBoxButtons.OK);
                    //factgrid();
                    //Util.ChangeButton(btninsertar, 2, false);
                    //iactbtn = 2;
                }
                else if (iins == 1)
                {
                    factgrid();
                    Util.ChangeButton(btninsertar, 2, false);
                    iactbtn = 2;
                }
                else if (iact == 1)
                {
                    DialogResult result = MessageBox.Show("No tienes permisos para crear el perfil", "SIPAA", MessageBoxButtons.OK);
                    //factgrid();
                    //Util.ChangeButton(btninsertar, 2, false);
                    //iactbtn = 2;
                }
                else if (ielim == 1)
                {
                    DialogResult result = MessageBox.Show("No tienes permisos para crear el perfil", "SIPAA", MessageBoxButtons.OK);
                    //factgrid();
                    //Util.ChangeButton(btninsertar, 3, false);
                    //iactbtn = 3;
                }
                else
                {
                    DialogResult result = MessageBox.Show("No tienes permisos para crear el perfil", "SIPAA", MessageBoxButtons.OK);
                }
            }
            else
            {
            }

            //for (int iContador = 0; iContador < dgvTrab.Rows.Count; iContador++)
            //{
            //    dgvTrab.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            //}


            //if (dgvTrab.SelectedRows.Count != 0)
            //{

            //    DataGridViewRow row = this.dgvTrab.SelectedRows[0];

            //    //CVPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
            //    //string Desc = row.Cells["DESCRIPCION"].Value.ToString();

            //    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            //    TrabajadorInfo.IdTrab = row.Cells["idtrab"].Value.ToString();

                //    DatosTrabajadorPerfil form = new DatosTrabajadorPerfil();
                //    form.Show();
                //    this.Close(); 
                //}
            }


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            pnlsuid.Visible = false;
            //lena grid
            fllenagridbusqueda(16, txtconceptobusq.Text.Trim());
        }

        private void btninsertar_Click(object sender, EventArgs e)
        {
            //valida campos
            Boolean bvalidacampos = fvalidacampos();

            //st checa
            if (ckbcheca.Checked == true)
            {
                istchec = "1";
            }
            else if (ckbcheca.Checked == false)
            {
                istchec = "0";
            }
            else
            {
                istchec = "0";
            }

            if (iactbtn == 1)
            {
                if (bvalidacampos == true)//valida campos
                {

                    TrabPerf.vuidtrabperf(iidtrabmodif, 19, istchec, cbosup.SelectedValue.ToString(), Int32.Parse(cbodir.SelectedValue.ToString()), LoginInfo.IdTrab, this.Name);
                    //lena grid
                    fllenagridbusqueda(16, iidtrabmodif);
                    pnlsuid.Visible = false;
                    pnlmenssuid.Visible = true;
                    pnlperf.Visible = false;
                    pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                    menssuid.Text = "Registro agregado correctamente";
                    timer1.Start();
                    iidtrabmodif = "0";
                }
            }
            else if (iactbtn == 2)
            {

                if (bvalidacampos == true)//valida campos
                {

                    TrabPerf.vuidtrabperf(iidtrabmodif, 18, istchec, cbosup.SelectedValue.ToString(), Int32.Parse(cbodir.SelectedValue.ToString()), LoginInfo.IdTrab, this.Name);
                    //lena grid
                    fllenagridbusqueda(16, iidtrabmodif);
                    pnlsuid.Visible = false;
                    pnlperf.Visible = false;
                    pnlmenssuid.Visible = true;
                    pnlmenssuid.BackColor = ColorTranslator.FromHtml("#0277bd");
                    menssuid.Text = "Registro modificado correctamente";
                    timer1.Start();
                    iidtrabmodif = "0";
                }

            }
            else if (iactbtn == 3)
            {
                DialogResult result = MessageBox.Show("Operación no valida", "SIPAA", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult result = MessageBox.Show("Operación no valida", "SIPAA", MessageBoxButtons.OK);
            }
        }

        private void btopcperfil_Click(object sender, EventArgs e)
        {
            TrabajadorInfo.IdTrab = iidtrabmodif;
            DatosTrabajadorPerfil form = new DatosTrabajadorPerfil();
            form.Show();
            this.Close();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void TrabajadoresPerfil_Load(object sender, EventArgs e)
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

            //resize 
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

            //iins = 0;
            //iact = 1;
            //ielim = 0;

            //lena grid
            fllenagridbusqueda(15,"");

            iidtrabmodif = "0";
        }

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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMin, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
        }

        private void txtconceptobusq_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtconceptobusq_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtconceptobusq_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnbuscar_Click(sender, e);
            }
        }

        protected void fllenagridbusqueda(int iopc, string stxtb)
        {

            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else if (iins == 1 && iact == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else if (iins == 1 && ielim == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else if (iact == 1 && ielim == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else if (iins == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else if (iact == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else if (ielim == 1)
            {
                fdgvsuid(iopc, stxtb);
                lblModif.Visible = true;
            }
            else
            {
                fdgvs(iopc);
                lblModif.Visible = false;
            }

        }

        //funcion formto grid con modificación busqueda con permisos
        protected void fdgvsuid(int iopc, string stextbus)
        {
            dgvTrab.DataSource = null;

            int inumcolumngrid = dgvTrab.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvTrab.Columns.RemoveAt(0);
            }

            DataTable dtdgvtrab = TrabPerf.dtdgvcb("", iopc, "", "", 0, LoginInfo.IdTrab, stextbus);
            dgvTrab.DataSource = dtdgvtrab;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvTrab.Columns.Insert(0, imgCheckUsuarios);
            dgvTrab.Columns[0].HeaderText = "Selección";

            dgvTrab.Columns[0].Width = 75;
            dgvTrab.Columns[1].Width = 90;
            dgvTrab.Columns[2].Width = 290;
            dgvTrab.Columns[3].Width = 80;
            dgvTrab.Columns[4].Width = 50;
            dgvTrab.Columns[5].Visible = false;
            dgvTrab.Columns[6].Visible = false;
            dgvTrab.Columns[7].Visible = false;
            dgvTrab.ClearSelection();
            lblModif.Visible = true;
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(int iopc)
        {
            DataTable dtdgvtc = TrabPerf.dtdgvcb("", iopc, "", "", 0, LoginInfo.IdTrab, txtconceptobusq.Text.Trim());
            dgvTrab.DataSource = dtdgvtc;

            dgvTrab.Columns[0].Width = 75;
            dgvTrab.Columns[1].Width = 370;
            dgvTrab.Columns[2].Width = 90;
            dgvTrab.Columns[3].Width = 50;
            dgvTrab.Columns[4].Visible = false;
            dgvTrab.Columns[5].Visible = false;
            dgvTrab.Columns[6].Visible = false;
            dgvTrab.ClearSelection();
        }

        private void factgrid()
        {
            for (int iContador = 0; iContador < dgvTrab.Rows.Count; iContador++)
            {
                dgvTrab.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvTrab.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvTrab.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                pnlsuid.Visible = true;

                if (sestperf == "Si")
                {
                    pnlperf.Visible = true;
                }

                txttrabjador.Text = row.Cells["empleado"].Value.ToString();
                txtst.Text = row.Cells["Estatus"].Value.ToString();
                istcheca = Convert.ToInt32(row.Cells["estcheca"].Value.ToString());

                if (istcheca == 0)
                {
                    ckbcheca.Checked = false;
                }
                else if (istcheca == 1)
                {
                    ckbcheca.Checked = true;
                }

                iidtrabmodif = row.Cells["Número Empleado"].Value.ToString();

                //cb sup
                cbosup.DataSource = null;
                DataTable dtsup = TrabPerf.dtdgvcb("", 17, "", "", 0, LoginInfo.IdTrab, this.Name);
                Utilerias.llenarComboxDataTable(cbosup, dtsup, "Idtrab", "empleado");
                cbosup.SelectedValue = Convert.ToInt32(row.Cells["idsup"].Value.ToString());
                
                //cb dir
                cbodir.DataSource = null;
                DataTable dtdir = TrabPerf.dtdgvcb("", 17, "", "", 0, LoginInfo.IdTrab, this.Name);
                Utilerias.llenarComboxDataTable(cbodir, dtdir, "Idtrab", "empleado");
                cbodir.SelectedValue = Convert.ToInt32(row.Cells["iddir"].Value.ToString());

                cbosup.Focus();
            }
        }

        //validacion de campos
        private Boolean fvalidacampos()
        {
            if (iidtrabmodif == "0")
            {
                DialogResult result = MessageBox.Show("Selecciona un empleado a modificar", "SIPAA", MessageBoxButtons.OK);
                txtconceptobusq.Focus();
                return false;
            }
            else if (cbosup.Text.Trim() == "" || cbosup.SelectedIndex == -1 )
            {
                DialogResult result = MessageBox.Show("Supervisor no valido", "SIPAA", MessageBoxButtons.OK);
                cbosup.Focus();
                return false;
            }
            else if (cbodir.Text.Trim() == "" || cbodir.SelectedIndex == -1 || cbodir.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleciona un director", "SIPAA", MessageBoxButtons.OK);
                cbodir.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------    
    }
}
