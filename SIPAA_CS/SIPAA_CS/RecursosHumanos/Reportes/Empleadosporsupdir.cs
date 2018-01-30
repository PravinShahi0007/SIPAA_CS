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
//Autor: Noe Alvarez Marquina
//Fecha creación: 26-ene-2018      Última Modificacion: dd-mm-aaaa
//Descripción: reporete de empleador pos supervisor o director
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class Empleadosporsupdir : Form
    {

        #region
        int iins, iact, ielim, iimp;
        int iopc;
        #endregion

        Perfil DatPerfil = new Perfil();
        Utilerias Util = new Utilerias();
        Empleadoporsupdir Empsupdir = new Empleadoporsupdir();

        public Empleadosporsupdir()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        private void cborol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Util.p_inicbo == 1)
            {
                int irol = Int32.Parse(cborol.SelectedValue.ToString());
                if (irol == 1)
                {
                    iopc = 10;
                }
                else if (irol == 2)
                {
                    iopc = 9;
                }
                else
                {
                    cbosupdir.DataSource = null;
                    dgvdatos.DataSource = null;
                }

                if (irol == 1 || irol == 2)
                {
                    cbosupdir.DataSource = null;
                    DataTable dtsupdir = Empsupdir.dtdatos("0", iopc, "", "", 0, LoginInfo.IdTrab, this.Name, "", "");
                    Utilerias.llenarComboxDataTable(cbosupdir, dtsupdir, "clave", "dato");
                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            //valida campos
            Boolean bvalidacampos = fvalidacampos();

            int irol = Int32.Parse(cborol.SelectedValue.ToString());
            if (irol == 1)
            {
                iopc = 11;
            }
            else if (irol == 2)
            {
                iopc = 12;
            }
            //llena grid
            fdgvs(cbosupdir.SelectedValue.ToString(), iopc, "", "", 0, LoginInfo.IdTrab, this.Name, "", "");
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable dtperscardo = new DataTable();

            //valida campos
            Boolean bvalidacampos = fvalidacampos();

            if (bvalidacampos == true)
            {

                int irol = Int32.Parse(cborol.SelectedValue.ToString());
                if (irol == 1)
                {
                    iopc = 11;
                }
                else if (irol == 2)
                {
                    iopc = 12;
                }
                else
                {

                }
                if (irol == 1 || irol == 2)
                {
                    dtperscardo = Empsupdir.dtdatos(cbosupdir.SelectedValue.ToString(), iopc, "", "", 0, cbosupdir.Text.ToString(), cborol.Text.ToString(), "", "");

                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    RepEmpxsupdir dtrpt = new RepEmpxsupdir();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtperscardo, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

                    form.RptDoc = ReportDoc;
                    form.Show();
                }

            }
        }
        private void btnregresar_Click(object sender, EventArgs e)
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
        private void Empleadosporsupdir_Load(object sender, EventArgs e)
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

            if (iimp == 1)
            {
                label3.Visible = true;
                btnImprimir.Visible = true;
            }

            //cb incidencias
            Util.p_inicbo = 0;
            cborol.DataSource = null;
            DataTable dtinc = Empsupdir.dtdatos("0", 8, "", "", 0, LoginInfo.IdTrab, this.Name, "", "");
            Utilerias.llenarComboxDataTable(cborol, dtinc, "clave", "descrip");
            Util.p_inicbo = 1;
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
            toolTip1.SetToolTip(this.btnbuscar, "Buscar Registros");
            toolTip1.SetToolTip(this.btnImprimir, "Imprimir Registros");
        }

        //funcion formto grid sin modificación busqueda
        protected void fdgvs(string sidtrab, int iopcion, string scheca, string sactivo, int icvtipohr,
                                string susuumod, string sprgumod, string sfhinireg, string sfhfinreg)
        {
            DataTable dtdgvjdat = Empsupdir.dtdatos(sidtrab, iopcion, "", "", 0, LoginInfo.IdTrab, this.Name, "", "");
            dgvdatos.DataSource = dtdgvjdat;

            dgvdatos.Columns[0].Width = 30;
            dgvdatos.Columns[1].Width = 60;
            dgvdatos.Columns[2].Width = 300;
            dgvdatos.Columns[3].Width = 250;
            dgvdatos.Columns[4].Width = 120;
            dgvdatos.Columns[5].Width = 120;
            dgvdatos.Columns[6].Width = 150;
            dgvdatos.Columns[7].Width = 140;
            dgvdatos.Columns[8].Width = 90;
            dgvdatos.Columns[9].Width = 120;
            dgvdatos.Columns[10].Width = 140;
            dgvdatos.Columns[11].Visible = false;
            dgvdatos.Columns[12].Visible = false;
            dgvdatos.ClearSelection();
        }

        //validacion de campos
        private Boolean fvalidacampos()
        {
            if (cborol.Text.Trim() == "" || cborol.SelectedIndex == -1 || cborol.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un rol", "SIPAA", MessageBoxButtons.OK);
                cborol.Focus();
                return false;
            }
            else if (cbosupdir.Text.Trim() == "" || cbosupdir.SelectedIndex == -1 || cbosupdir.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleciona un director y/o supervisor", "SIPAA", MessageBoxButtons.OK);
                cbosupdir.Focus();
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
