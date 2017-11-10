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
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;

using SIPAA_CS.RecursosHumanos;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Accesos;


//***********************************************************************************************
//Autor: Jaime Avendaño Vargas      modif: noe alvarez marquina (se agrega standar, tooltip, funcionalidad, picture box fotografia, label usuario, tamaño)
//Fecha creación: 22-Mar-2017       Última Modificacion: 21/07/2017
//Descripción: Días Festivos
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class DiasSemana : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        int pcvdia;
        int p_rep;

        #endregion

        DiaSemana oDSemana = new DiaSemana();
        Utilerias Util = new Utilerias();

        public DiasSemana()
        {
            InitializeComponent();
        }

        private void DiasSemana_Load(object sender, EventArgs e)
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

            //LLAMA TOOL TIP
            sTooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            /* JAV
            Usuario objUsuario = new Usuario();
            string idtrab = LoginInfo.IdTrab;
            ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(idtrab, 4);
            Utilerias.DashboardDinamico(PanelMetro, ltModulosxUsuario);
            //LoginInfo.Nombre = lblusuario.Text;
            string NomUsu = LoginInfo.Nombre;
            lblusuario.Text = NomUsu;
            JAV */
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvDSem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pins == 1 && pact == 1 && pelim == 1)
            {
                
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1 && pact == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pins == 1 && pelim == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pact == 1 && pelim == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pact == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pelim == 1)
            {
                Util.ChangeButton(btnInsertar, 3, false);
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

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvDSem.DataSource = null;
            //llena grid con datos existente
            fgDSem(4, 0, txtBuscarDS.Text.Trim(), "JAV", "DiaSemana");
            txtDescripcionDS.Text = "";
            txtDescripcionDS.Focus();
            /*
            if (dgvDSem.Columns.Count > 0)
            {
                dgvDSem.Columns.RemoveAt(0);
            }
            */
        }
        //boton agregar
        /*
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            pnlActDSem.Visible = true;
            lblActDSem.Text = "     Agregar Día de la Semana";
            Util.ChangeButton(btnAgregar, 1, false);
            pactbtn = 1;
            dtpFechaDSem.Text = "";
            txtDescripcionDS.Text = "";
            txtDescripcionDS.Focus();
        }
        */

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtDescripcionDS.Text.Trim() == "" && pactbtn == 1)
            {
                lblMensaje.Text = "Capture un dato a guardar";
            }
            else if (pactbtn == 1)//insertar
            {
                //inserta registro nuevo
                fgDSem(1, 0, txtDescripcionDS.Text.Trim(), "JAV", "DiaSemana");
                dgvDSem.DataSource = null;
                dgvDSem.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtDescripcionDS.Text = "";
                txtDescripcionDS.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgDSem(4, 0, "", "JAV", "DiaSemana");
                dgvDSem.Columns.RemoveAt(0);
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActDSem.Visible = false;
            }
            else if (pactbtn == 2)//actualizar
            {
                //inserta registro nuevo
                fgDSem(2, pcvdia, txtDescripcionDS.Text.Trim(), "JAV", "DiaSemana");
                dgvDSem.DataSource = null;
                dgvDSem.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtDescripcionDS.Text = "";
                txtDescripcionDS.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgDSem(4, 0, "", "JAV", "DiaSemana");
                dgvDSem.Columns.RemoveAt(0);
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActDSem.Visible = false;
            }
            else if (pactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //inserta registro nuevo
                    fuidDSem(3, pcvdia, txtDescripcionDS.Text.Trim(), "JAV", "DiaSemana");
                    dgvDSem.DataSource = null;
                    dgvDSem.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txtDescripcionDS.Text = "";
                    txtDescripcionDS.Focus();
                    timer1.Start();
                    //llena grid con datos existente
                    fgDSem(4, 0, "", "JAV", "DiaSemana");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlActDSem.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }

        }

        //Boton minimizar
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
                // No hace nada, se queda en la pantalla
            }
        }
        private void btnRegresar_Click(object sender, EventArgs e)
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

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lblActDSem.Text = "     Elimina Día Semana";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lblActDSem.Text = "     Modifica Día Semana";
                pactbtn = 2;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //TOOL TIP PARA OBJETOS
        private void sTooltip()
        {

            //CREA TOOL TIP
            ToolTip toolTip1 = new ToolTip();

            //CONFIGURACION
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            // CONFIGURA EL TEXTO POR OBJETO
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            toolTip1.SetToolTip(this.btnInsertar, "Insertar Registro");

        } // private void sTooltip()

        private void fgDSem(int p_opcion, int p_cvdia, string p_descripcion, string p_usuumod, string p_prgumodr)
        {

            if (pins == 1 && pact == 1 && pelim == 1)
            {

                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDSem.Columns.Insert(0, imgCheckUsuarios);
                dgvDSem.Columns[0].HeaderText = "Selección";

                dgvDSem.Columns[0].Width = 75;
                dgvDSem.Columns.RemoveAt(0);
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDSem.Columns[0].Width = 55;
                dgvDSem.Columns.RemoveAt(0);
                dgvDSem.Columns[0].Width = 120;
                //                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDSem.ClearSelection();
            }
            else if (pins == 1 && pact == 1)
            {
                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDSem.Columns.Insert(0, imgCheckUsuarios);
                dgvDSem.Columns[0].HeaderText = "Selección";

                dgvDSem.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDSem.Columns[1].Width = 55;
                dgvDSem.Columns[2].Width = 120;
                //                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDSem.ClearSelection();

            }
            else if (pins == 1 && pelim == 1)
            {
                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDSem.Columns.Insert(0, imgCheckUsuarios);
                dgvDSem.Columns[0].HeaderText = "Selección";

                dgvDSem.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDSem.Columns[1].Width = 55;
                dgvDSem.Columns[2].Width = 120;
                //                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDSem.ClearSelection();
            }
            else if (pact == 1 && pelim == 1)
            {
                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDSem.Columns.Insert(0, imgCheckUsuarios);
                dgvDSem.Columns[0].HeaderText = "Selección";

                dgvDSem.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDSem.Columns[1].Width = 55;
                dgvDSem.Columns[2].Width = 120;
                //                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDSem.ClearSelection();
            }
            else if (pins == 1)
            {
                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                dgvDSem.Columns[0].Visible = false;
                //                dgvDiasFestivos.Columns[2].Visible = false;
                dgvDSem.Columns[1].Width = 55;
                dgvDSem.Columns[2].Width = 120;

                dgvDSem.ClearSelection();
            }
            else if (pact == 1)
            {
                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDSem.Columns.Insert(0, imgCheckUsuarios);
                dgvDSem.Columns[0].HeaderText = "Selección";

                dgvDSem.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDSem.Columns[1].Width = 55;
                dgvDSem.Columns[2].Width = 120;
                //                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDSem.ClearSelection();
            }
            else if (pelim == 1)
            {
                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDSem.Columns.Insert(0, imgCheckUsuarios);
                dgvDSem.Columns[0].HeaderText = "Selección";

                dgvDSem.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDSem.Columns[1].Width = 55;
                dgvDSem.Columns[2].Width = 120;
                //                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDSem.ClearSelection();
            }
            else
            {

                DataTable dtDSem = oDSemana.obtdsem(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                dgvDSem.DataSource = dtDSem;

                dgvDSem.Columns[0].Visible = false;
                //                dgvDiasFestivos.Columns[2].Visible = false;
                dgvDSem.Columns[1].Width = 250;
                //dgvDSem.Columns[2].Width = 120; noe alvarez marquina el datatable solo devielve 2 columnas
                dgvDSem.ClearSelection();
            }
        }

        private void fuidDSem(int p_opcion, int p_cvdia, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            //agrega registro
            if (pactbtn == 1)
            {
                p_rep = oDSemana.udidsemana(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionDS.Text = "";
            }
            //actualiza registro
            else if (pactbtn == 2)
            {
                p_rep = oDSemana.udidsemana(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionDS.Text = "";
            }
            //elimina registro
            else if (pactbtn == 3)
            {
                p_rep = oDSemana.udidsemana(p_opcion, p_cvdia, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionDS.Text = "";
            } // 

            switch (p_rep.ToString())
            {
                case "1":
                    lblMensaje.Text = "Registro agregado correctamente";
                    break;
                case "2":
                    lblMensaje.Text = "Registro modificado correctamente";
                    break;
                case "3":
                    lblMensaje.Text = "Registro eliminado correctamente";
                    break;
                case "99":
                    lblMensaje.Text = "Registro ya existe";
                    break;
                default:
                    lblMensaje.Text = "";
                    break;
            } // switch (p_rep.ToString())
        } // 


        private void factgrid()
        {
            for (int iContador = 0; iContador < dgvDSem.Rows.Count; iContador++)
            {
                dgvDSem.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            
            if (dgvDSem.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvDSem.SelectedRows[0];

                //pcvdia = int.Parse(row.Cells["Dia"].Value.ToString());
                string ValorRow = row.Cells["Descripción"].Value.ToString();

                pnlActDSem.Visible = true;
                lblActDSem.Text = "     Modifica Dia Festivo";
                dtpFechaDSem.Text = pcvdia.ToString();
                //dtpFechaDSem.Focus();
                txtDescripcionDS.Text = ValorRow;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
            
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    } 
} 
