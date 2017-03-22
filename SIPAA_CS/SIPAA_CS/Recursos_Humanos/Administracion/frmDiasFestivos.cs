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
//Autor: Benjamin Huizar Barajas
//Fecha creación: 15-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Días Festivos
//***********************************************************************************************

namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    public partial class frmDiasFestivos : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        string pfecha;
        int p_rep;

        #endregion

        DiasFestivos oDiasFestivos = new DiasFestivos();
        Utilerias Util = new Utilerias();

        public frmDiasFestivos()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Benjamin Huizar Barajas
        //Fecha creación: 13-Mar-2017       Última Modificacion: dd-mm-aaaa
        //Descripción: Administra Días Festivos
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvDiasFestivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        } // private void dgvDiasFestivos_CellContentClick(object sender, DataGridViewCellEventArgs e)

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvDiasFestivos.DataSource = null;
            //llena grid con datos existente
            fgDiasFestivos(4, "0", txtBuscarDF.Text.Trim(), "bhb", "DiasFestivos");
            txtDescripcionDF.Text = "";
            txtDescripcionDF.Focus();
            if(dgvDiasFestivos.Columns.Count>3)
            {
                dgvDiasFestivos.Columns.RemoveAt(0);
            }
        }
        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            pnlActDiasFestivos.Visible = true;
            lblActDiasFestivos.Text = "     Agregar Tipo de Horario";
            Util.ChangeButton(btnAgregar, 1, false);
            pactbtn = 1;
            dtpFechaDiaFestivo.Text = "";
            txtDescripcionDF.Text = "";
            txtDescripcionDF.Focus();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtDescripcionDF.Text.Trim() == "" && pactbtn == 1)
            {
                lblMensaje.Text = "Capture un dato a guardar";
            }
            else if (pactbtn == 1)//insertar
            {
                //inserta registro nuevo
                fuidDiasFestivos(1, dtpFechaDiaFestivo.Text, txtDescripcionDF.Text.Trim(), "bhb", "DiasFestivos");
                dgvDiasFestivos.DataSource = null;
                dgvDiasFestivos.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtDescripcionDF.Text = "";
                txtDescripcionDF.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgDiasFestivos(4, "0", "", "bhb", "DiasFestivos");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActDiasFestivos.Visible = false;
            }
            else if (pactbtn == 2)//actualizar
            {
                //inserta registro nuevo
                fuidDiasFestivos(2, pfecha, txtDescripcionDF.Text.Trim(), "bhb", "DiasFestivos");
                dgvDiasFestivos.DataSource = null;
                dgvDiasFestivos.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtDescripcionDF.Text = "";
                txtDescripcionDF.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgDiasFestivos(4, "0", "", "bhb", "DiasFestivos");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActDiasFestivos.Visible = false;
            }
            else if (pactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //inserta registro nuevo
                    fuidDiasFestivos(3, pfecha, txtDescripcionDF.Text.Trim(), "bhb", "DiasFestivos");
                    dgvDiasFestivos.DataSource = null;
                    dgvDiasFestivos.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txtDescripcionDF.Text = "";
                    txtDescripcionDF.Focus();
                    timer1.Start();
                    //llena grid con datos existente
                    fgDiasFestivos(4, "0", "", "bhb", "DiasFestivos");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlActDiasFestivos.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }

        } // private void btnInsertar_Click(object sender, EventArgs e)

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

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void frmDiasFestivos_Load(object sender, EventArgs e)
        {
            //LLAMA TOOL TIP
            sTooltip();

            //LLAMA METODO LLENAR GRID
//            SLlenaGrid(1, "");

            pins = 1;
            pact = 1;
            pelim = 1;

            //HABILITA BOTON AGREGAR
            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }
            //
            fgDiasFestivos(4, "0", "", "bhb", "DiasFestivos");
//            txtBuscarDF.Focus();
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lblActDiasFestivos.Text = "     Elimina Día Festivo";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lblActDiasFestivos.Text = "     Modifica Día Festivo";
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
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
//            toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
//            toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            toolTip1.SetToolTip(this.btnInsertar, "Insertar Registro");

        } // private void sTooltip()

        private void fgDiasFestivos(int p_opcion, string p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)
        {

            if (pins == 1 && pact == 1 && pelim == 1)
            {

                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDiasFestivos.Columns.Insert(0, imgCheckUsuarios);
                dgvDiasFestivos.Columns[0].HeaderText = "Selección";

                dgvDiasFestivos.Columns[0].Width = 75;
//                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
//                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDiasFestivos.ClearSelection();
            }
            else if (pins == 1 && pact == 1)
            {
                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDiasFestivos.Columns.Insert(0, imgCheckUsuarios);
                dgvDiasFestivos.Columns[0].HeaderText = "Selección";

                dgvDiasFestivos.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
//                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDiasFestivos.ClearSelection();

            }
            else if (pins == 1 && pelim == 1)
            {
                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDiasFestivos.Columns.Insert(0, imgCheckUsuarios);
                dgvDiasFestivos.Columns[0].HeaderText = "Selección";

                dgvDiasFestivos.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
//                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDiasFestivos.ClearSelection();
            }
            else if (pact == 1 && pelim == 1)
            {
                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDiasFestivos.Columns.Insert(0, imgCheckUsuarios);
                dgvDiasFestivos.Columns[0].HeaderText = "Selección";

                dgvDiasFestivos.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
//                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDiasFestivos.ClearSelection();
            }
            else if (pins == 1)
            {
                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                dgvDiasFestivos.Columns[0].Visible = false;
                //                dgvDiasFestivos.Columns[2].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;

                dgvDiasFestivos.ClearSelection();
            }
            else if (pact == 1)
            {
                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDiasFestivos.Columns.Insert(0, imgCheckUsuarios);
                dgvDiasFestivos.Columns[0].HeaderText = "Selección";

                dgvDiasFestivos.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
//                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDiasFestivos.ClearSelection();
            }
            else if (pelim == 1)
            {
                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvDiasFestivos.Columns.Insert(0, imgCheckUsuarios);
                dgvDiasFestivos.Columns[0].HeaderText = "Selección";

                dgvDiasFestivos.Columns[0].Width = 75;
                //                dgvDiasFestivos.Columns[1].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
//                dgvDiasFestivos.Columns[3].Visible = false;
                dgvDiasFestivos.ClearSelection();
            }
            else
            {

                DataTable dtDiaFestivo = oDiasFestivos.obtdiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                dgvDiasFestivos.DataSource = dtDiaFestivo;

                dgvDiasFestivos.Columns[0].Visible = false;
//                dgvDiasFestivos.Columns[2].Visible = false;
                dgvDiasFestivos.Columns[1].Width = 90;
                dgvDiasFestivos.Columns[2].Width = 300;
                dgvDiasFestivos.ClearSelection();
            }
        }

        private void fuidDiasFestivos(int p_opcion, string p_fecha, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            //agrega registro
            if (pactbtn == 1)
            {
                p_rep = oDiasFestivos.udidiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionDF.Text = "";
            }
            //actualiza registro
            else if (pactbtn == 2)
            {
                p_rep = oDiasFestivos.udidiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionDF.Text = "";
            }
            //elimina registro
            else if (pactbtn == 3)
            {
                p_rep = oDiasFestivos.udidiasfestivos(p_opcion, p_fecha, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionDF.Text = "";
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
            for (int iContador = 0; iContador < dgvDiasFestivos.Rows.Count; iContador++)
            {
                dgvDiasFestivos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvDiasFestivos.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvDiasFestivos.SelectedRows[0];

                pfecha = row.Cells["Fecha"].Value.ToString();
                string ValorRow = row.Cells["Descripción"].Value.ToString();

                pnlActDiasFestivos.Visible = true;
                lblActDiasFestivos.Text = "     Modifica Dia Festivo";
                dtpFechaDiaFestivo.Text = pfecha;
                dtpFechaDiaFestivo.Focus();
                txtDescripcionDF.Text = ValorRow;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    } // public partial class frmDiasFestivos : Form
} // namespace SIPAA_CS.Recursos_Humanos.Administracion
