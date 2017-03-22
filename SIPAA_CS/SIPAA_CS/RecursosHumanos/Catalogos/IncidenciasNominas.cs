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
using SIPAA_CS.Recursos_Humanos.App_Code;
using SIPAA_CS.Conexiones;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación: 15-mar-2017      Última Modificacion: dd-mm-aaaa
//Descripción: administra incidencias de nomina
//***********************************************************************************************

namespace SIPAA_CS.Recursos_Humanos
{
    public partial class IncidenciasNom : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        
        int p_rep;

        int p_cvincidencia;
        int p_cvrepresenta;
        int p_stdir;
        int p_cvtipohr;
        int p_stpremio;

        #endregion

        IncNomina IncNom = new IncNomina();
        Utilerias Util = new Utilerias();

        public IncidenciasNom()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        // combo incidencia busqueda
        private void cboincnombusq_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        //accion al tocar grid conforme a permisos del usuario
        private void dgvincnomia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pins == 1 && pact == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1 && pact == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pins == 1 && pelim == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pact == 1 && pelim == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pact == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pelim == 1)
            {
                Util.ChangeButton(btninsertar, 3, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 3;
            }
            else
            {

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            pnlincnom.Visible = true;
            lbluid.Text = "     Agregar Incidencia Nomina";
            Util.ChangeButton(btninsertar, 1, false);
            fcargarcbo();
            pactbtn = 1;
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void IncidenciasNom_Load(object sender, EventArgs e)
        {
            //habilita tool tip
            ftooltip();

            //variable para inserta nuevo registro
            pins = 1;
            pact = 0;
            pelim =0;
            pactbtn = 0;
            p_rep = 0;

            Util.cargarcombo(cboincnombusq, IncNom.cboInc(1));

            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }

            //llena grid con datos existente
            fgtphr(4);
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
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
        //llenado de grid conforme a permisos
        private void fgtphr(int p_opcion)
        {

            if (pins == 1 && pact == 1 && pelim == 1)
            {
                fformatgridsuid(p_opcion);
            }
            else if (pins == 1 && pact == 1)
            {
                fformatgridsuid(p_opcion);
            }
            else if (pins == 1 && pelim == 1)
            {
                fformatgridsuid(p_opcion);
            }
            else if (pact == 1 && pelim == 1)
            {
                fformatgridsuid(p_opcion);
            }
            else if (pins == 1)
            {
                fformatgridsi(p_opcion);
            }
            else if (pact == 1)
            {
                fformatgridsi(p_opcion);
            }
            else if (pelim == 1)
            {
                fformatgridsuid(p_opcion);
            }
            else
            {
                fformatgridsi(p_opcion);
            }
        }

        //funcion formto grid con modificación
        protected void fformatgridsuid(int p_opcion)
        {
            DataTable dttipohr = IncNom.obtincnomina(p_opcion);
            dgvincnomia.DataSource = dttipohr;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvincnomia.Columns.Insert(0, imgCheckUsuarios);
            dgvincnomia.Columns[0].HeaderText = "Selección";

            dgvincnomia.Columns[0].Width = 75;
            dgvincnomia.Columns[1].Visible = false;
            dgvincnomia.Columns[2].Visible = false;
            dgvincnomia.Columns[3].Visible = false;
            dgvincnomia.Columns[4].Visible = false;
            dgvincnomia.Columns[5].Visible = false;
            dgvincnomia.Columns[6].Width = 120;
            dgvincnomia.Columns[7].Width = 100;
            dgvincnomia.Columns[8].Width = 75;
            dgvincnomia.Columns[9].Width = 75;
            dgvincnomia.Columns[10].Width = 75;
            dgvincnomia.Columns[11].Width = 130;
            dgvincnomia.Columns[12].Width = 95;
            dgvincnomia.Columns[13].Width = 85;
            dgvincnomia.Columns[14].Width = 100;
            dgvincnomia.ClearSelection();
        }

        //funcio formto grid sin modificación
        protected void fformatgridsi(int p_opcion)
        {
            DataTable dttipohr = IncNom.obtincnomina(p_opcion);
            dgvincnomia.DataSource = dttipohr;

            dgvincnomia.Columns[0].Visible = false;
            dgvincnomia.Columns[1].Visible = false;
            dgvincnomia.Columns[2].Visible = false;
            dgvincnomia.Columns[3].Visible = false;
            dgvincnomia.Columns[4].Visible = false;
            dgvincnomia.Columns[5].Width = 120;
            dgvincnomia.Columns[6].Width = 100;
            dgvincnomia.Columns[7].Width = 75;
            dgvincnomia.Columns[8].Width = 75;
            dgvincnomia.Columns[9].Width = 75;
            dgvincnomia.Columns[10].Width = 130;
            dgvincnomia.Columns[11].Width = 95;
            dgvincnomia.Columns[12].Width = 85;
            dgvincnomia.Columns[13].Width = 100;
            dgvincnomia.ClearSelection();
        }

        //funcion formto grid con modificación busqueda
        protected void fformatgrididb(int p_opcion, int p_cvincidencia, string p_descripcion)
        {
            DataTable dttipohr = IncNom.obtincnominair(p_opcion, p_cvincidencia, p_descripcion);
            dgvincnomia.DataSource = dttipohr;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvincnomia.Columns.Insert(0, imgCheckUsuarios);
            dgvincnomia.Columns[0].HeaderText = "Selección";

            dgvincnomia.Columns[0].Width = 75;
            dgvincnomia.Columns[1].Visible = false;
            dgvincnomia.Columns[2].Visible = false;
            dgvincnomia.Columns[3].Visible = false;
            dgvincnomia.Columns[4].Visible = false;
            dgvincnomia.Columns[5].Visible = false;
            dgvincnomia.Columns[6].Width = 120;
            dgvincnomia.Columns[7].Width = 100;
            dgvincnomia.Columns[8].Width = 75;
            dgvincnomia.Columns[9].Width = 75;
            dgvincnomia.Columns[10].Width = 75;
            dgvincnomia.Columns[11].Width = 130;
            dgvincnomia.Columns[12].Width = 95;
            dgvincnomia.Columns[13].Width = 85;
            dgvincnomia.Columns[14].Width = 100;
            dgvincnomia.ClearSelection();
        }

        //funcion formto grid sin modificación busqueda
        protected void fformatgrididbsm(int p_opcion, int p_cvincidencia, string p_descripcion)
        {
            DataTable dttipohr = IncNom.obtincnominair(p_opcion, p_cvincidencia, p_descripcion);
            dgvincnomia.DataSource = dttipohr;

            dgvincnomia.Columns[0].Visible = false;
            dgvincnomia.Columns[1].Visible = false;
            dgvincnomia.Columns[2].Visible = false;
            dgvincnomia.Columns[3].Visible = false;
            dgvincnomia.Columns[4].Visible = false;
            dgvincnomia.Columns[5].Width = 120;
            dgvincnomia.Columns[6].Width = 100;
            dgvincnomia.Columns[7].Width = 75;
            dgvincnomia.Columns[8].Width = 75;
            dgvincnomia.Columns[9].Width = 75;
            dgvincnomia.Columns[10].Width = 130;
            dgvincnomia.Columns[11].Width = 95;
            dgvincnomia.Columns[12].Width = 85;
            dgvincnomia.Columns[13].Width = 100;
            dgvincnomia.ClearSelection();
        }

        private void fcargarcbo()
        {
            Util.cargarcombo(cboRepresenta, IncNom.cboInc(1));

            Util.cargarcombo(cbotipohr, IncNom.cboTipoHr(4));

            Util.cargarcombo(cbostdir, IncNom.cboEsNoPr(5));

            Util.cargarcombo(cbopremio, IncNom.cboEsNoPr(5));

            Util.cargarcombo(cbopasanom, IncNom.cboEsNoPr(5));
            cboRepresenta.Focus();
        }
        private void factgrid()
        {
            if (pins == 1 && pact == 0 && pelim == 0)
            {
            }
            else
            {
                for (int iContador = 0; iContador < dgvincnomia.Rows.Count; iContador++)
                {
                    dgvincnomia.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                if (dgvincnomia.SelectedRows.Count != 0)
                {

                    DataGridViewRow row = this.dgvincnomia.SelectedRows[0];

                    p_cvincidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());
                    p_cvrepresenta = Convert.ToInt32(row.Cells["cvrepresenta"].Value.ToString());
                    p_stdir = Convert.ToInt32(row.Cells["stdir"].Value.ToString());
                    p_cvtipohr = Convert.ToInt32(row.Cells["cvtipohr"].Value.ToString());
                    p_stpremio = Convert.ToInt32(row.Cells["stpremio"].Value.ToString());

                    lbluid.Text = "     Modifica Incidencia de Nomina";

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (cboincnombusq.SelectedIndex != 0)
            {
                if (pins == 1 && pact == 1 && pelim == 1)
                {
                    fformatgrididb(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else if (pins == 1 && pact == 1)
                {
                    fformatgrididb(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else if (pins == 1 && pelim == 1)
                {
                    fformatgrididb(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else if (pact == 1 && pelim == 1)
                {
                    fformatgrididb(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else if (pins == 1)
                {
                    fformatgrididbsm(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else if (pact == 1)
                {
                    fformatgrididbsm(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else if (pelim == 1)
                {
                    fformatgrididb(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }
                else
                {
                    fformatgrididbsm(7, cboincnombusq.SelectedIndex, txtincnomb.Text.Trim());
                }

            }
            else
            {
                //llena grid con datos existente
                fgtphr(4);
            }

            cboincnombusq.DataSource = null;
            Util.cargarcombo(cboincnombusq, IncNom.cboInc(1));
            cboincnombusq.Text = "";
            txtincnomb.Text = "";
            cboincnombusq.Focus();


        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
