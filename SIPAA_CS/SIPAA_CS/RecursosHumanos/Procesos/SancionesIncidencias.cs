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
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;

using CrystalDecisions.CrystalReports.Engine;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación: 07-mar-2018       Última Modificacion: dd-mm-aaaa
//Descripción: catalogo de sanciones de incidencias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class SancionesIncidencias : Form
    {
        #region
        int iins, iact, ielim, iimp;
        int iactbtn, icvpolmodif;
        #endregion

        Perfil DatPerfil = new Perfil();
        Utilerias Util = new Utilerias();
        SancionesIncidencia clsancionesincidencias = new SancionesIncidencia();
        

        public SancionesIncidencias()
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

        private void cbotipeval_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            dgvdatos.DataSource = null;
            int inumcolumngrid = dgvdatos.ColumnCount;
            if (inumcolumngrid == 1){dgvdatos.Columns.RemoveAt(0);}

            //llena grid
            fllenagridbusqueda(4, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);

            pnlcrud.Visible = true;
            Util.ChangeButton(btninsertar, 1, false);
            ckbeliminar.Visible = false;
            iactbtn = 1;

            //limpia y carga objetos
            flimponj();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //boton buscar
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvdatos.DataSource = null;
            int inumcolumngrid = dgvdatos.ColumnCount;
            if (inumcolumngrid == 1){dgvdatos.Columns.RemoveAt(0);}

            //llena grid
            fllenagridbusqueda(4, 0, txtbusqueda.Text.Trim(), 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
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
                    int ivali = clsancionesincidencias.cruddatos(1, 0, txtdesc.Text.Trim(), Int32.Parse(cbonivel.SelectedValue.ToString()), Int32.Parse(cbotiponomina.SelectedValue.ToString()), Int32.Parse(txtnoeventos.Text),
                                                                 Int32.Parse(cboincidencia.SelectedValue.ToString()), Int32.Parse(cbotipeval.SelectedValue.ToString()), Int32.Parse(cbosancion.SelectedValue.ToString()), 0, 1,
                                                                 LoginInfo.IdTrab, this.Name);

                    if (ivali == 1)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
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
                    int ivalu = clsancionesincidencias.cruddatos(2, icvpolmodif, txtdesc.Text.Trim(), Int32.Parse(cbonivel.SelectedValue.ToString()), Int32.Parse(cbotiponomina.SelectedValue.ToString()), Int32.Parse(txtnoeventos.Text),
                                                                 Int32.Parse(cboincidencia.SelectedValue.ToString()), Int32.Parse(cbotipeval.SelectedValue.ToString()), Int32.Parse(cbosancion.SelectedValue.ToString()), 0, 1,
                                                                 LoginInfo.IdTrab, this.Name);

                    if (ivalu == 2)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#0277bd");
                        menssuid.Text = "Registro modificado correctamente";
                        timer1.Start();
                        flimponj();
                        iactbtn = 0;
                        icvpolmodif = 0;
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
                    int ivald = clsancionesincidencias.cruddatos(3, icvpolmodif, txtdesc.Text.Trim(), Int32.Parse(cbonivel.SelectedValue.ToString()), Int32.Parse(cbotiponomina.SelectedValue.ToString()), Int32.Parse(txtnoeventos.Text),
                                                                 Int32.Parse(cboincidencia.SelectedValue.ToString()), Int32.Parse(cbotipeval.SelectedValue.ToString()), Int32.Parse(cbosancion.SelectedValue.ToString()), 0, 1,
                                                                 LoginInfo.IdTrab, this.Name);

                    if (ivald == 3)
                    {
                        //llena grid
                        fllenagridbusqueda(4, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
                        pnlmenssuid.Visible = true;
                        pnlmenssuid.BackColor = ColorTranslator.FromHtml("#f44336");
                        menssuid.Text = "Registro eliminado correctamente";
                        timer1.Start();
                        flimponj();
                        iactbtn = 0;
                        icvpolmodif = 0;
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

        //load
        private void SancionesIncidencias_Load(object sender, EventArgs e)
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

            //agregar
            if (iins == 1){btnagregar.Visible = true;}
            //imprimir
            if (iimp == 1){label3.Visible = true; btnImprimir.Visible = true;}

            //llena grid
            fllenagridbusqueda(4, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
        }

        //timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlmenssuid.Visible = false;
            timer1.Stop();
        }

        //eliminar
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
            toolTip1.SetToolTip(this.btncerrar, "Cerrar");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
            toolTip1.SetToolTip(this.btnbuscar, "Buscar Registro");
            toolTip1.SetToolTip(this.btnImprimir, "Imprimir");
        }

        //funcion busqueda conforme a permisos
        protected void fllenagridbusqueda(int iopcion, int icvpolitica, string sdescpolitica, int icvnivel, int icvtiponomina,
                                         int inumeventos, int icvincrepresenta, int icvevaluacion, int isancgenera, int iordenejec,
                                         int istpolitica, string susuumod, string sprgumod)
        {

            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else if (iins == 1 && iact == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else if (iins == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else if (iact == 1 && ielim == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else if (iins == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else if (iact == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else if (ielim == 1)
            {
                fdgvsuid(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }
            else
            {
                fdgvs(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            }

        }

        //funcion formato grid con modificación busqueda con permisos
        protected void fdgvsuid(int iopcion, int icvpolitica, string sdescpolitica, int icvnivel, int icvtiponomina,
                         int inumeventos, int icvincrepresenta, int icvevaluacion, int isancgenera, int iordenejec,
                         int istpolitica, string susuumod, string sprgumod)
        {
            dgvdatos.DataSource = null;

            int inumcolumngrid = dgvdatos.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvdatos.Columns.RemoveAt(0);
            }

            DataTable dtdatos = clsancionesincidencias.dtdatos(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            dgvdatos.DataSource = dtdatos;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvdatos.Columns.Insert(0, imgCheckUsuarios);
            dgvdatos.Columns[0].HeaderText = "Selección";

            dgvdatos.Columns[0].Width = 75;
            dgvdatos.Columns[1].Width = 200;
            dgvdatos.Columns[2].Width = 110;
            dgvdatos.Columns[3].Width = 90;
            dgvdatos.Columns[4].Width = 80;
            dgvdatos.Columns[5].Width = 90;
            dgvdatos.Columns[6].Width = 90;
            dgvdatos.Columns[7].Width = 105;
            dgvdatos.Columns[8].Visible = false;
            dgvdatos.Columns[9].Visible = false;
            dgvdatos.Columns[10].Visible = false;
            dgvdatos.Columns[11].Visible = false;
            dgvdatos.Columns[12].Visible = false;
            dgvdatos.Columns[13].Visible = false;
            dgvdatos.ClearSelection();
            lblModif.Visible = true;
        }

        private void pnlcrud_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable dtReporteSanc = new DataTable();

            dtReporteSanc = clsancionesincidencias.dtdatos(4, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);

            //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
            ViewerReporte form = new ViewerReporte();
            RepSancionesInc dtrpt = new RepSancionesInc();
            ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporteSanc, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

            //ReportDoc.SetParameterValue("Titulo1", "SIPAA - Recursos Humanos");
            //ReportDoc.SetParameterValue("Titulo2", "Catálogo de Conceptos de Nómina");
            //ReportDoc.SetParameterValue("Titulo3", "");

            form.RptDoc = ReportDoc;
            form.Show();
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(int iopcion, int icvpolitica, string sdescpolitica, int icvnivel, int icvtiponomina,
                         int inumeventos, int icvincrepresenta, int icvevaluacion, int isancgenera, int iordenejec,
                         int istpolitica, string susuumod, string sprgumod)
        {
            DataTable dtdgvji = clsancionesincidencias.dtdatos(iopcion, icvpolitica, sdescpolitica, icvnivel, icvtiponomina, inumeventos, icvincrepresenta, icvevaluacion, isancgenera, iordenejec, istpolitica, susuumod, sprgumod);
            dgvdatos.DataSource = dtdgvji;

            dgvdatos.Columns[0].Width = 200;
            dgvdatos.Columns[1].Width = 110;
            dgvdatos.Columns[2].Width = 90;
            dgvdatos.Columns[3].Width = 80;
            dgvdatos.Columns[4].Width = 90;
            dgvdatos.Columns[5].Width = 90;
            dgvdatos.Columns[6].Width = 105;
            dgvdatos.Columns[7].Visible = false;
            dgvdatos.Columns[8].Visible = false;
            dgvdatos.Columns[9].Visible = false;
            dgvdatos.Columns[10].Visible = false;
            dgvdatos.Columns[11].Visible = false;
            dgvdatos.Columns[12].Visible = false;
            lblModif.Visible = false;
        }

        //limpia carga objetos
        private void flimponj()
        {
            txtdesc.Text = "";
            //cb nivel
            cbonivel.DataSource = null;
            DataTable dtnivel = clsancionesincidencias.dtdatos(5, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbonivel, dtnivel, "cve", "desc");
            //cbotiponomina
            cbotiponomina.DataSource = null;
            DataTable dtcbotiponomina = clsancionesincidencias.dtdatos(6, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotiponomina, dtcbotiponomina, "cve", "desc");
            //cboincidencia
            cboincidencia.DataSource = null;
            DataTable dtcboincidencia = clsancionesincidencias.dtdatos(7, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cboincidencia, dtcboincidencia, "cve", "desc");

            txtnoeventos.Text = "";

            //cbotipeval
            cbotipeval.DataSource = null;
            DataTable dtcbotipeval = clsancionesincidencias.dtdatos(8, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipeval, dtcbotipeval, "cve", "desc");
            //cbosancion
            cbosancion.DataSource = null;
            DataTable dtcbosancion = clsancionesincidencias.dtdatos(9, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbosancion, dtcbosancion, "cve", "desc");

            txtdesc.Focus();
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
                    flimponj();
                    DataGridViewRow row = this.dgvdatos.SelectedRows[0];
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    icvpolmodif = Convert.ToInt32(row.Cells["cvpolitica"].Value.ToString());
                    txtdesc.Text = row.Cells["Politica"].Value.ToString();

                    cbonivel.Text = row.Cells["Nivel"].Value.ToString();
                    cbotiponomina.Text = row.Cells["Forma de Pago"].Value.ToString();
                    cboincidencia.Text = row.Cells["Incidecnia"].Value.ToString();
                    txtnoeventos.Text = row.Cells["Num Eventos"].Value.ToString();
                    cbotipeval.Text = row.Cells["Evaluación"].Value.ToString();
                    cbosancion.Text = row.Cells["Sanción"].Value.ToString();

                    txtdesc.Focus();
                }
            }
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
            else if (cbonivel.Text.Trim() == "" || cbonivel.SelectedIndex == -1 || cbonivel.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un nivel", "SIPAA", MessageBoxButtons.OK);
                cbonivel.Focus();
                return false;
            }
            else if (cbotiponomina.Text.Trim() == "" || cbotiponomina.SelectedIndex == -1 || cbotiponomina.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona una forma de pago", "SIPAA", MessageBoxButtons.OK);
                cbotiponomina.Focus();
                return false;
            }
            else if (cboincidencia.Text.Trim() == "" || cboincidencia.SelectedIndex == -1 || cboincidencia.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un tipo de incidencia", "SIPAA", MessageBoxButtons.OK);
                cboincidencia.Focus();
                return false;
            }
            else if (cbotipeval.Text.Trim() == "" || cbotipeval.SelectedIndex == -1 || cbotipeval.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un método de evaluación", "SIPAA", MessageBoxButtons.OK);
                cbotipeval.Focus();
                return false;
            }
            else if (cbosancion.Text.Trim() == "" || cbosancion.SelectedIndex == -1 || cbosancion.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un tipo de sanción", "SIPAA", MessageBoxButtons.OK);
                cbosancion.Focus();
                return false;
            }
            else if (!Util.IsNumber(txtnoeventos.Text.Trim()) || txtnoeventos.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("El número de eventos no es numerico", "SIPAA", MessageBoxButtons.OK);
                txtnoeventos.Focus();
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
