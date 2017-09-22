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
            pnlsuid.Visible = true;
            cboincidencias.Focus();

            //cb incidencias
            cboincidencias.DataSource = null;
            DataTable dtinc = JustInc.dtdgvcb(7, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cboincidencias, dtinc, "cvincidencia", "descrip");

            //cb tipociclo
            cbociclo.DataSource = null;
            DataTable dttipciclo = JustInc.dtdgvcb(8, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbociclo, dttipciclo, "cvciclo", "descrip");

            //cb tipo evento
            cbotipevento.DataSource = null;
            DataTable dttipevent = JustInc.dtdgvcb(5, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipevento, dttipevent, "stvalor", "descrip");

            //cb tipo evaliacion
            cbotipeval.DataSource = null;
            DataTable dttipeval = JustInc.dtdgvcb(6, 0, 0, txtconceptobusq.Text.Trim(), 0, 0, 0, 0, 0, LoginInfo.IdTrab, this.Name);
            Utilerias.llenarComboxDataTable(cbotipeval, dttipeval, "stvalor", "descrp");
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
            dgvjustinc.Columns[5].Width = 75;
            dgvjustinc.Columns[6].Width = 180;
            dgvjustinc.Columns[7].Width = 90;
            dgvjustinc.Columns[8].Width = 90;
            dgvjustinc.Columns[9].Visible = false;
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
            dgvjustinc.ClearSelection();
            lblModif.Visible = false;
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
