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
using SIPAA_CS.RecursosHumanos.Reportes;
using SIPAA_CS.Properties;
using static SIPAA_CS.App_Code.Usuario;

using CrystalDecisions.CrystalReports.Engine;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación: 07-dic-2017       Última Modificacion: dd-mm-aaaa
//Descripción: catalogo de incidencia extras
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class Justificaincidenciasd : Form
    {

        #region
        int iins, iact, ielim, iimp;

        int iactbtn, icvedatomoficar;
        #endregion

        Perfil DatPerfil = new Perfil();
        Utilerias Util = new Utilerias();
        JustificaIncidenciad justincd = new JustificaIncidenciad();

        public Justificaincidenciasd()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvdatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvdatos.DataSource = null;

            int inumcolumngrid = dgvdatos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvdatos.Columns.RemoveAt(0);
            }

            //llena grid
            fllenagridbusqueda(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dgvdatos.DataSource = null;

            int inumcolumngrid = dgvdatos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvdatos.Columns.RemoveAt(0);
            }

            //llena grid
            fllenagridbusqueda(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);

            pnlcrud.Visible = true;

            txtdescripcion.Text = "";

            Util.ChangeButton(btninsertar, 1, false);

            ckbeliminar.Visible = false;

            iactbtn = 1;

            txtdescripcion.Focus();
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
                    int ivali = justincd.cruddatos(1, 20, 0, 0, txtdescripcion.Text.Trim(), 1, LoginInfo.IdTrab, this.Name);

                    if (ivali == 1)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        menssuid.Text = "Registro agregado correctamente";
                        timer1.Start();
                        flimpiaobj();
                        iactbtn = 1;
                        txtdescripcion.Focus();
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
                    int ivalu = justincd.cruddatos(2, 20, 0, icvedatomoficar, txtdescripcion.Text.Trim(), 1, LoginInfo.IdTrab, this.Name);

                    if (ivalu == 2)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#0277bd");
                        menssuid.Text = "Registro modificado correctamente";
                        timer1.Start();
                        flimpiaobj();
                        iactbtn = 0;
                        pnlcrud.Visible = false;
                        txtdescripcion.Focus();
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
                    int ivald = justincd.cruddatos(3, 20, 0, icvedatomoficar, txtdescripcion.Text.Trim(), 0, LoginInfo.IdTrab, this.Name);

                    if (ivald == 3)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#f44336");
                        menssuid.Text = "Registro eliminado correctamente";
                        timer1.Start();
                        flimpiaobj();
                        ckbeliminar.Checked = false;
                        Util.ChangeButton(btninsertar, 2, false);
                        iactbtn = 0;
                        pnlcrud.Visible = false;
                        txtdescripcion.Focus();
                    }
                    else
                    {
                        DialogResult result1 = MessageBox.Show("No se elimmino su registro", "SIPAA", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    ckbeliminar.Checked = false;
                    txtdescripcion.Focus();
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Seleccione una acción a realizar", "SIPAA", MessageBoxButtons.OK);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable dtreporte = new DataTable();
            dtreporte = justincd.dtdatos(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);

            //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
            ViewerReporte form = new ViewerReporte();
            RepCatalogoJustIncExt dtrpt = new RepCatalogoJustIncExt();
            ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtreporte, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

            form.RptDoc = ReportDoc;
            form.Show();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void Justificaincidenciasd_Load(object sender, EventArgs e)
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
                label3.Visible = true;
                btnImprimir.Visible = true;
            }

            //llena grid
            fllenagridbusqueda(4, 0, 0, 0, "", 0, LoginInfo.IdTrab, this.Name);
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
            toolTip1.SetToolTip(this.btnbuscar, "Buscar Registros");
            toolTip1.SetToolTip(this.btnAgregar, "Agregar Registro");
            toolTip1.SetToolTip(this.btnImprimir, "Imprimir Catalogo");
        }

        protected void fllenagridbusqueda(int iopcion, int icvjustinc, int icvincidencia, int icvjustab, string sdescjustab, int istjustab, string susuumod, string sprgumod)
        {

            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else if (iins == 1 && iact == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else if (iins == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else if (iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else if (iins == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else if (iact == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else if (ielim == 1)
            {
                fdgvsuid(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }
            else
            {
                fdgvs(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            }

        }

        //funcion formto grid con modificación busqueda con permisos
        protected void fdgvsuid(int iopcion, int icvjustinc, int icvincidencia, int icvjustab, string sdescjustab, int istjustab, string susuumod, string sprgumod)
        {
            dgvdatos.DataSource = null;

            int inumcolumngrid = dgvdatos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvdatos.Columns.RemoveAt(0);
            }

            DataTable dtdatos = justincd.dtdatos(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            dgvdatos.DataSource = dtdatos;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvdatos.Columns.Insert(0, imgCheckUsuarios);
            dgvdatos.Columns[0].HeaderText = "Selección";

            dgvdatos.Columns[0].Width = 75;
            dgvdatos.Columns[1].Visible = false;
            dgvdatos.Columns[2].Visible = false;
            dgvdatos.Columns[3].Visible = false;
            dgvdatos.Columns[4].Width = 500;
            dgvdatos.ClearSelection();
            lblModif.Visible = true;
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(int iopcion, int icvjustinc, int icvincidencia, int icvjustab, string sdescjustab, int istjustab, string susuumod, string sprgumod)
        {
            DataTable dtdgvji = justincd.dtdatos(iopcion, icvjustinc, icvincidencia, icvjustab, sdescjustab, istjustab, susuumod, sprgumod);
            dgvdatos.DataSource = dtdgvji;

            dgvdatos.Columns[0].Visible = false;
            dgvdatos.Columns[1].Visible = false;
            dgvdatos.Columns[2].Visible = false;
            dgvdatos.Columns[3].Width = 600;
            dgvdatos.ClearSelection();
            lblModif.Visible = false;
        }

        //validacion de campos
        private Boolean fvalidacampos()
        {
            if (txtdescripcion.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Capture una descripción", "SIPAA", MessageBoxButtons.OK);
                txtdescripcion.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void flimpiaobj()
        {
            txtdescripcion.Text = "";
        }

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
                    icvedatomoficar = Convert.ToInt32(row.Cells["cvjustab"].Value.ToString());

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    txtdescripcion.Text = row.Cells["Descripción"].Value.ToString();

                    txtdescripcion.Focus();
                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
