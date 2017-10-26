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

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación:22-09-2017       Última Modificacion: dd-mm-aaaa
//Descripción: catalogo de comentarios para justificar incidecnias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class JustificaIncidencias : Form
    {
        #region
        int iins, iact, ielim;

        int iactbtn;

        int icvjustinc, icvincidencia;
        #endregion

        Perfil DatPerfil = new Perfil();
        JustificaIncidencia JustInc = new JustificaIncidencia();
        Utilerias Util = new Utilerias();

        public JustificaIncidencias()
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
            dgvjustinc.DataSource = null;

            int inumcolumngrid = dgvjustinc.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvjustinc.Columns.RemoveAt(0);
            }

            //llena grid
            fdgvsuid(4);

            pnlsuid.Visible = true;
            Util.ChangeButton(btninsertar, 1, false);
            cboincidencias.Focus();

            //cb incidencias
            cboincidencias.DataSource = null;
            DataTable dtinc = JustInc.dtdgvcb(7, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cboincidencias, dtinc, "cvincidencia", "descrip");

            txtdesc.Text = "";

            //cb tipociclo
            cbociclo.DataSource = null;
            DataTable dttipciclo = JustInc.dtdgvcb(8, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbociclo, dttipciclo, "cvciclo", "descrip");

            txtnoeventos.Text = "";

            //cb tipo evento
            cbotipevento.DataSource = null;
            DataTable dttipevent = JustInc.dtdgvcb(5, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipevento, dttipevent, "stvalor", "descrip");

            //cb tipo evaliacion
            cbotipeval.DataSource = null;
            DataTable dttipeval = JustInc.dtdgvcb(6, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipeval, dttipeval, "stvalor", "descrp");

            ckbeliminar.Visible = false;


            iactbtn = 1;
        }

        private void btninsertar_Click(object sender, EventArgs e)
        {
            //guarda datos
            if (iactbtn == 1)
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)
                {
                    int ivali = JustInc.vuidjustinc(1, 0, Int32.Parse(cboincidencias.SelectedValue.ToString()), txtdesc.Text.Trim(), Int32.Parse(cbociclo.SelectedValue.ToString()),
                        Int32.Parse(txtnoeventos.Text.Trim()), Int32.Parse(cbotipevento.SelectedValue.ToString()), Int32.Parse(cbotipeval.SelectedValue.ToString()), 1,
                        LoginInfo.IdTrab, this.Name);

                    if (ivali == 1)
                    {
                        //llena grid
                        fdgvsuid(4);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        menssuid.Text = "Registro agregado correctamente";
                        timer1.Start();
                        flimpiaobj();
                        iactbtn = 1;
                        //pnlsuid.Visible = false;
                        cboincidencias.Focus();
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
                {
                    int ivalu = JustInc.vuidjustinc(2, icvjustinc, icvincidencia, txtdesc.Text.Trim(), Int32.Parse(cbociclo.SelectedValue.ToString()),
                        Int32.Parse(txtnoeventos.Text.Trim()), Int32.Parse(cbotipevento.SelectedValue.ToString()), Int32.Parse(cbotipeval.SelectedValue.ToString()), 1,
                        LoginInfo.IdTrab, this.Name);

                    if (ivalu == 2)
                    {
                        //llena grid
                        fdgvsuid(4);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#0277bd");
                        menssuid.Text = "Registro modificado correctamente";
                        timer1.Start();
                        flimpiaobj();
                        iactbtn = 0;
                        pnlsuid.Visible = false;
                        cboincidencias.Focus();
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
                    int ivald = JustInc.vuidjustinc(3, icvjustinc, icvincidencia, txtdesc.Text.Trim(), Int32.Parse(cbociclo.SelectedValue.ToString()),
                        Int32.Parse(txtnoeventos.Text.Trim()), Int32.Parse(cbotipevento.SelectedValue.ToString()), Int32.Parse(cbotipeval.SelectedValue.ToString()), 1,
                        LoginInfo.IdTrab, this.Name);

                    if (ivald == 3)
                    {
                        //llena grid
                        fdgvsuid(4);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#f44336");
                        menssuid.Text = "Registro eliminado correctamente";
                        timer1.Start();
                        flimpiaobj();
                        ckbeliminar.Checked = false;
                        Util.ChangeButton(btninsertar, 2, false);
                        iactbtn = 0;
                        pnlsuid.Visible = false;
                        cboincidencias.Focus();
                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("No se elimmino su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    ckbeliminar.Checked = false;
                    cboincidencias.Focus();
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Seleccione una acción a realizar", "SIPAA", MessageBoxButtons.OK);
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvjustinc.DataSource = null;

            int inumcolumngrid = dgvjustinc.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvjustinc.Columns.RemoveAt(0);
            }

            //llena grid
            fdgvsuid(4);
        }

        private void ckbeliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbeliminar.Checked == true)
            {
                Util.ChangeButton(btninsertar, 3, false);
                //lbluid.Text = "     Elimina Incidencia de Nomina";
                iactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btninsertar, 2, false);
                //lbluid.Text = "     Modifica Incidencia de Nomina";
                iactbtn = 2;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void JustificaIncidencias_Load(object sender, EventArgs e)
        {
            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }

            //inicializa tool tip
            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            //variables accesos
            DataTable Permisos = DatPerfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            ielim = Int32.Parse(Permisos.Rows[0][5].ToString());

            if (iins == 1)
            {
                btnAgregar.Visible = true;
            }

            //llena grid
            fdgvsuid(4);
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
            toolTip1.SetToolTip(this.btncerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
            toolTip1.SetToolTip(this.btnbuscar, "Buscar Registros");
        }

        //funcion para llenar grid
        protected void fllenagridbusqueda()
        {

            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fdgvsuid(4);
            }
            else if (iins == 1 && iact == 1)
            {
                fdgvsuid(4);
            }
            else if (iins == 1 && ielim == 1)
            {
                fdgvsuid(4);
            }
            else if (iact == 1 && ielim == 1)
            {
                fdgvsuid(4);
            }
            else if (iins == 1)
            {
                fdgvsuid(4);
            }
            else if (iact == 1)
            {
                fdgvsuid(4);
            }
            else if (ielim == 1)
            {
                fdgvsuid(4);
            }
            else
            {
                fdgvs(4);
            }

        }

        //funcion formto grid con modificación busqueda con permisos
        protected void fdgvsuid(int iopcion)
        {
            dgvjustinc.DataSource = null;

            int inumcolumngrid = dgvjustinc.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvjustinc.Columns.RemoveAt(0);
            }

            DataTable dtdgvji = JustInc.dtdgvcb(iopcion, 0,0,txtconceptobusq.Text.Trim(),0,0,0,0,0,"","");
            dgvjustinc.DataSource = dtdgvji;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvjustinc.Columns.Insert(0, imgCheckUsuarios);
            dgvjustinc.Columns[0].HeaderText = "Selección";

            dgvjustinc.Columns[0].Width = 75;
            dgvjustinc.Columns[1].Visible = false;
            dgvjustinc.Columns[2].Visible = false;
            dgvjustinc.Columns[3].Width = 150;
            dgvjustinc.Columns[4].Width = 180;
            dgvjustinc.Columns[5].Width = 150;
            dgvjustinc.Columns[6].Width = 80;
            dgvjustinc.Columns[7].Width = 80;
            dgvjustinc.Columns[8].Width = 90;
            dgvjustinc.Columns[9].Visible = false;
            dgvjustinc.Columns[10].Visible = false;
            dgvjustinc.Columns[11].Visible = false;
            dgvjustinc.Columns[12].Visible = false;
            dgvjustinc.ClearSelection();
            lblModif.Visible = true;
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(int iopcion)
        {
            DataTable dtdgvji = JustInc.dtdgvcb(iopcion, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, "", "");
            dgvjustinc.DataSource = dtdgvji;

            dgvjustinc.Columns[0].Visible = false;
            dgvjustinc.Columns[1].Visible = false;
            dgvjustinc.Columns[2].Width = 150;
            dgvjustinc.Columns[3].Width = 180;
            dgvjustinc.Columns[4].Width = 75;
            dgvjustinc.Columns[5].Width = 180;
            dgvjustinc.Columns[6].Width = 90;
            dgvjustinc.Columns[7].Width = 90;
            dgvjustinc.Columns[8].Visible = false;
            dgvjustinc.Columns[9].Visible = false;
            dgvjustinc.Columns[10].Visible = false;
            dgvjustinc.ClearSelection();
            lblModif.Visible = false;
        }

        private void factgrid()
        {
            if (iins == 1 && iact == 0 && ielim == 0)
            {
            }
            else
            {
                for (int iContador = 0; iContador < dgvjustinc.Rows.Count; iContador++)
                {
                    dgvjustinc.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                if (dgvjustinc.SelectedRows.Count != 0)
                {
                    pnlsuid.Visible = true;
                    DataGridViewRow row = this.dgvjustinc.SelectedRows[0];
                    icvjustinc = Convert.ToInt32(row.Cells["cvjustinc"].Value.ToString());
                    icvincidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());

                    //lbluid.Text = "     Modifica Incidencia de Nomina";

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    //cb incidencias
                    cboincidencias.DataSource = null;
                    DataTable dtinc = JustInc.dtdgvcb(7, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
                    Utilerias.llenarComboxDataTable(cboincidencias, dtinc, "cvincidencia", "descrip");
                    cboincidencias.SelectedValue = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());

                    txtdesc.Text = row.Cells["Justifica Incidencia"].Value.ToString();

                    //cb tipociclo
                    cbociclo.DataSource = null;
                    DataTable dttipciclo = JustInc.dtdgvcb(8, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
                    Utilerias.llenarComboxDataTable(cbociclo, dttipciclo, "cvciclo", "descrip");
                    cbociclo.SelectedValue = Convert.ToInt32(row.Cells["cvtipociclo"].Value.ToString());

                    txtnoeventos.Text = row.Cells["No de Enventos"].Value.ToString();

                    //cb tipo evento
                    cbotipevento.DataSource = null;
                    DataTable dttipevent = JustInc.dtdgvcb(5, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
                    Utilerias.llenarComboxDataTable(cbotipevento, dttipevent, "stvalor", "descrip");
                    cbotipevento.SelectedValue = Convert.ToInt32(row.Cells["cvtipoevento"].Value.ToString());

                    //cb tipo evaliacion
                    cbotipeval.DataSource = null;
                    DataTable dttipeval = JustInc.dtdgvcb(6, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
                    Utilerias.llenarComboxDataTable(cbotipeval, dttipeval, "stvalor", "descrp");
                    cbotipeval.SelectedValue = Convert.ToInt32(row.Cells["cvtipoeval"].Value.ToString());

                    cboincidencias.Focus();
                }
            }
        }

        private void flimpiaobj()
        {
            //cb incidencias
            cboincidencias.DataSource = null;
            DataTable dtinc = JustInc.dtdgvcb(7, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cboincidencias, dtinc, "cvincidencia", "descrip");

            txtdesc.Text = "";

            //cb tipociclo
            cbociclo.DataSource = null;
            DataTable dttipciclo = JustInc.dtdgvcb(8, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbociclo, dttipciclo, "cvciclo", "descrip");

            txtnoeventos.Text = "";

            //cb tipo evento
            cbotipevento.DataSource = null;
            DataTable dttipevent = JustInc.dtdgvcb(5, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipevento, dttipevent, "stvalor", "descrip");

            //cb tipo evaliacion
            cbotipeval.DataSource = null;
            DataTable dttipeval = JustInc.dtdgvcb(6, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipeval, dttipeval, "stvalor", "descrp");
        }

        //validacion de campos
        private Boolean fvalidacampos()
        {
            if (cboincidencias.Text.Trim() == "" || cboincidencias.SelectedIndex == -1 || cboincidencias.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona una incidencias", "SIPAA", MessageBoxButtons.OK);
                cboincidencias.Focus();
                return false;
            }
            else if (txtdesc.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Captura una descripción", "SIPAA", MessageBoxButtons.OK);
                cboincidencias.Focus();
                return false;
            }
            else if (cbociclo.Text.Trim() == "" || cbociclo.SelectedIndex == -1 || cbociclo.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleciona un ciclo", "SIPAA", MessageBoxButtons.OK);
                cbociclo.Focus();
                return false;
            }
            else if (txtnoeventos.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Captura el número de eventos", "SIPAA", MessageBoxButtons.OK);
                txtnoeventos.Focus();
                return false;
            }
            else if (cbotipevento.Text.Trim() == "" || cbotipevento.SelectedIndex == -1 || cbotipevento.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleciona tipo de evento", "SIPAA", MessageBoxButtons.OK);
                cbotipevento.Focus();
                return false;
            }
            else if (cbotipeval.Text.Trim() == "" || cbotipeval.SelectedIndex == -1 || cbotipeval.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleciona un método de evaluación", "SIPAA", MessageBoxButtons.OK);
                cbotipeval.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
