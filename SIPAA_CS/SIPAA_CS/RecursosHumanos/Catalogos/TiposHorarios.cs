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
using SIPAA_CS.Conexiones;
using SIPAA_CS.App_Code;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación: 14-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Tipos de Horario
//***********************************************************************************************

namespace SIPAA_CS.Recursos_Humanos.Administracion
{

    public partial class TipoHorario : Form
    {

        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        int pcvtipohr;
        int p_rep;

        #endregion

        TipoHr TipHr = new TipoHr();
        Utilerias Util = new Utilerias();

        public TipoHorario()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvtiphr_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvtiphr.DataSource = null;
            dgvtiphr.Columns.RemoveAt(0);
            //llena grid con datos existente
            fgtphr(4, 0, txttipohr.Text.Trim(), "nam", "TipoHorario");
            txttipohr.Text = "";
            txttipohr.Focus();
        }
        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            pnltiphr.Visible = true;
            lbluid.Text = "     Agregar Tipo de Horario";
            Util.ChangeButton(btninsertar, 1, false);
            pactbtn = 1;
            txttipohriu.Text = "";
            txttipohriu.Focus();
        }
        private void btninsertar_Click(object sender, EventArgs e)
        {
            if (txttipohriu.Text.Trim() == "" && pactbtn ==1)
            {
                lbMensaje.Text = "Capture un dato a guardar";
            }
            else if(pactbtn == 1)//insertar
            {
                //inserta registro nuevo
                fuidtiphr(1,0,txttipohriu.Text.Trim(), "nam", "TipoHorario");
                dgvtiphr.DataSource = null;
                if (pins == 1 && pact == 0 && pelim == 0)
                {
                    
                }
                else
                {
                    dgvtiphr.Columns.RemoveAt(0);
                }
                panelTag.Visible = true;
                txttipohriu.Text = "";
                txttipohriu.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgtphr(4, 0, "", "nam", "TipoHorario");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnltiphr.Visible = false;
            }
            else if (pactbtn == 2)//actualizar
            {
                //inserta registro nuevo
                fuidtiphr(2, pcvtipohr, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                dgvtiphr.DataSource = null;
                dgvtiphr.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txttipohriu.Text = "";
                txttipohriu.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgtphr(4, 0, "", "nam", "TipoHorario");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnltiphr.Visible = false;
            }
            else if (pactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //inserta registro nuevo
                    fuidtiphr(3, pcvtipohr, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                    dgvtiphr.DataSource = null;
                    dgvtiphr.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txttipohriu.Text = "";
                    txttipohriu.Focus();
                    timer1.Start();
                    //llena grid con datos existente
                    fgtphr(4, 0, "", "nam", "TipoHorario");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnltiphr.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }
        }
        //boton minimizar
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

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void TipoHorario_Load(object sender, EventArgs e)
        {
            //habilita tool tip
            ftooltip();

            //variable para inserta nuevo registro
            pins = 1;
            pact = 1;
            pelim = 1;
            pcvtipohr = 0;
            pactbtn = 0;
            p_rep = 0;

            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }

            //llena grid con datos existente
            fgtphr(4,0,"","nam","TipoHorario");
        }
        //evento tick de timer de mensajes
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btninsertar, 3, false);
                lbluid.Text = "     Elimina Tipo de Horario";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btninsertar, 2, false);
                lbluid.Text = "     Modifica Tipo de Horario";
                pactbtn = 2;
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
        private void fgtphr(int p_opcion, int p_cvtipohr, string p_descripcion, string p_usuumod, string p_prgumodr)
        {

            if (pins == 1 && pact == 1 && pelim == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvtiphr.Columns.Insert(0, imgCheckUsuarios);
                dgvtiphr.Columns[0].HeaderText = "Selección";

                dgvtiphr.Columns[0].Width = 75;
                dgvtiphr.Columns[1].Visible = false;
                dgvtiphr.Columns[2].Width = 400;
                dgvtiphr.Columns[3].Visible = false;
                dgvtiphr.ClearSelection();
            }
            else if (pins == 1 && pact == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvtiphr.Columns.Insert(0, imgCheckUsuarios);
                dgvtiphr.Columns[0].HeaderText = "Selección";

                dgvtiphr.Columns[0].Width = 75;
                dgvtiphr.Columns[1].Visible = false;
                dgvtiphr.Columns[2].Width = 400;
                dgvtiphr.Columns[3].Visible = false;
                dgvtiphr.ClearSelection();

            }
            else if (pins == 1 && pelim == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvtiphr.Columns.Insert(0, imgCheckUsuarios);
                dgvtiphr.Columns[0].HeaderText = "Selección";

                dgvtiphr.Columns[0].Width = 75;
                dgvtiphr.Columns[1].Visible = false;
                dgvtiphr.Columns[2].Width = 400;
                dgvtiphr.Columns[3].Visible = false;
                dgvtiphr.ClearSelection();
            }
            else if (pact == 1 && pelim == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvtiphr.Columns.Insert(0, imgCheckUsuarios);
                dgvtiphr.Columns[0].HeaderText = "Selección";

                dgvtiphr.Columns[0].Width = 75;
                dgvtiphr.Columns[1].Visible = false;
                dgvtiphr.Columns[2].Width = 400;
                dgvtiphr.Columns[3].Visible = false;
                dgvtiphr.ClearSelection();
            }
            else if (pins == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                dgvtiphr.Columns[0].Visible = false;
                dgvtiphr.Columns[1].Width = 465;
                dgvtiphr.Columns[2].Visible = false;

                dgvtiphr.ClearSelection();
            }
            else if (pact == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvtiphr.Columns.Insert(0, imgCheckUsuarios);
                dgvtiphr.Columns[0].HeaderText = "Selección";

                dgvtiphr.Columns[0].Width = 75;
                dgvtiphr.Columns[1].Visible = false;
                dgvtiphr.Columns[2].Width = 400;
                dgvtiphr.Columns[3].Visible = false;
                dgvtiphr.ClearSelection();
            }
            else if (pelim == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvtiphr.Columns.Insert(0, imgCheckUsuarios);
                dgvtiphr.Columns[0].HeaderText = "Selección";

                dgvtiphr.Columns[0].Width = 75;
                dgvtiphr.Columns[1].Visible = false;
                dgvtiphr.Columns[2].Width = 400;
                dgvtiphr.Columns[3].Visible = false;
                dgvtiphr.ClearSelection();
            }
            else
            {

                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                dgvtiphr.Columns[0].Visible = false;
                dgvtiphr.Columns[1].Width = 465;
                dgvtiphr.Columns[2].Visible = false;

                dgvtiphr.ClearSelection();
            }
        }

        private void fuidtiphr(int p_opcion, int p_cvtipohr, string p_descripcion, string p_usuumod, string p_prgumodr)
        {
            //agrega registro
            if (pactbtn == 1)
            {

                p_rep = TipHr.uditipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txttipohriu.Text = "";
            }
            //actualiza registro
            else if (pactbtn == 2)
            {

                p_rep = TipHr.uditipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txttipohriu.Text = "";
            }
            //elimina registro
            else if (pactbtn == 3)
            {

                p_rep = TipHr.uditipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txttipohriu.Text = "";
            }

            switch (p_rep.ToString())
            {
                case "1":
                    lbMensaje.Text = "Registro agregado correctamente";
                    break;
                case "2":
                    lbMensaje.Text = "Registro modificado correctamente";
                    break;
                case "3":
                    lbMensaje.Text = "Registro eliminado correctamente";
                    break;
                default:
                    lbMensaje.Text = "";
                    break;
            }

        }
        private void factgrid()
        {
            if (pins == 1 && pact == 0 && pelim == 0)
            {
            }
            else
            {
                for (int iContador = 0; iContador < dgvtiphr.Rows.Count; iContador++)
                {
                    dgvtiphr.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                if (dgvtiphr.SelectedRows.Count != 0)
                {

                    DataGridViewRow row = this.dgvtiphr.SelectedRows[0];

                    pcvtipohr = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    string ValorRow = row.Cells["Descripción"].Value.ToString();

                    pnltiphr.Visible = true;
                    lbluid.Text = "     Modifica Tipo de Horario";
                    txttipohriu.Text = ValorRow;
                    txttipohriu.Focus();

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
