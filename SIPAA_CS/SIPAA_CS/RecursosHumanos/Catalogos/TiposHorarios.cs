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

namespace SIPAA_CS.RecursosHumanos.Catalogos
{

    public partial class TiposHorario : Form
    {
        #region
        //variables utilizadas
        int iins, iact, ielim, ivalresp;
        int iactbtn;
        int icvtipohr;
        int iresp;
        #endregion

        TipoHr TipHr = new TipoHr();
        Utilerias Util = new Utilerias();

        public TiposHorario()
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
            if (iins == 1 && iact == 1 && ielim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                cbxEliminar.Visible = true;
                iactbtn = 2;
            }
            else if (iins == 1 && iact == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                factgrid();
                iactbtn = 2;
            }
            else if (iins == 1 && ielim == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                factgrid();
                cbxEliminar.Visible = true;
                iactbtn = 2;
            }
            else if (iact == 1 && ielim == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                factgrid();
                cbxEliminar.Visible = true;
                iactbtn = 2;
            }
            else if (iins == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                factgrid();
                iactbtn = 2;
            }
            else if (iact == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                factgrid();
                iactbtn = 2;
            }
            else if (ielim == 1)
            {
                Util.ChangeButton(btninsertar, 3, false);
                btnagregar.Image = Resources.Baja;
                factgrid();
                cbxEliminar.Visible = true;
                iactbtn = 3;
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
            cbxEliminar.Visible = false;
            pnltiphr.Visible = true;
            lbluid.Text = "     Agregar Tipo de Horario";
            Util.ChangeButton(btninsertar, 1, false);
            btnagregar.Image = Resources.Guardar;
            iactbtn = 1;
            txttipohriu.Text = "";
            txttipohriu.Focus();
        }
        private void btninsertar_Click(object sender, EventArgs e)
        {
            if (txttipohriu.Text.Trim() == "" && iactbtn ==1)
            {
                lbMensaje.Text = "Capture un dato a guardar";
            }
            else if(iactbtn == 1)//insertar
            {
                //valida registros identicos
                ivalresp = TipHr.valtipohr(5,0, txttipohriu.Text.Trim());

                if (ivalresp > 0)
                {
                    DialogResult result = MessageBox.Show("El registro que intenta insertar ya existe, Favor de verificar", "SIPAA", MessageBoxButtons.OK);
                    dgvtiphr.DataSource = null;
                    dgvtiphr.Columns.RemoveAt(0);
                    //llena grid con datos existente
                    fgtphr(4, 0, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                }
                else
                {
                    //valida registro similares
                    ivalresp = TipHr.valtipohr(6, 0, txttipohriu.Text.Trim());

                    if (ivalresp > 0)
                    {
                        dgvtiphr.DataSource = null;
                        dgvtiphr.Columns.RemoveAt(0);
                        //llena grid con datos existente
                        fgtphr(4, 0, txttipohriu.Text.Trim(), "nam", "TipoHorario");

                        DialogResult result = MessageBox.Show("Exiten " + ivalresp + " registros similares, desea agregar el registro", "SIPAA", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            //inserta registro nuevo
                            fuidtiphr(1, 0, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                            dgvtiphr.DataSource = null;
                            if (iins == 1 && iact == 0 && ielim == 0)
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
                            cbxEliminar.Checked = false;
                            cbxEliminar.Visible = false;
                            pnltiphr.Visible = false;
                        }
                        else if (result == DialogResult.No)
                        {

                        }

                    }
                    else
                    {
                        //inserta registro nuevo
                        fuidtiphr(1, 0, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                        dgvtiphr.DataSource = null;
                        if (iins == 1 && iact == 0 && ielim == 0)
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
                        cbxEliminar.Checked = false;
                        cbxEliminar.Visible = false;
                        pnltiphr.Visible = false;
                    }
                }

            }
            else if (iactbtn == 2)//actualizar
            {
                //inserta registro nuevo
                fuidtiphr(2, icvtipohr, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                dgvtiphr.DataSource = null;
                dgvtiphr.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txttipohriu.Text = "";
                txttipohriu.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgtphr(4, 0, "", "nam", "TipoHorario");
                cbxEliminar.Checked = false;
                cbxEliminar.Visible = false;
                pnltiphr.Visible = false;
            }
            else if (iactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //inserta registro nuevo
                    fuidtiphr(3, icvtipohr, txttipohriu.Text.Trim(), "nam", "TipoHorario");
                    dgvtiphr.DataSource = null;
                    dgvtiphr.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txttipohriu.Text = "";
                    txttipohriu.Focus();
                    timer1.Start();
                    //llena grid con datos existente
                    fgtphr(4, 0, "", "nam", "TipoHorario");
                    cbxEliminar.Checked = false;
                    cbxEliminar.Visible = false;
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
            iins = 1;
            iact = 1;
            ielim = 1;
            icvtipohr = 0;
            iactbtn = 0;
            iresp = 0;

            if (iins == 1)
            {
                btnagregar.Visible = true;
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
            if (cbxEliminar.Checked == true)
            {
                Util.ChangeButton(btninsertar, 3, false);
                btnagregar.Image = Resources.Baja;
                lbluid.Text = "     Elimina Tipo de Horario";
                iactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btninsertar, 2, false);
                btnagregar.Image = Resources.Editar;
                lbluid.Text = "     Modifica Tipo de Horario";
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
            toolTip1.SetToolTip(this.btnbuscar, "Busca Registro");
        }
        private void fgtphr(int p_opcion, int p_cvtipohr, string p_descripcion, string p_usuumod, string p_prgumodr)
        {

            if (iins == 1 && iact == 1 && ielim == 1)
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
            else if (iins == 1 && iact == 1)
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
            else if (iins == 1 && ielim == 1)
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
            else if (iact == 1 && ielim == 1)
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
            else if (iins == 1)
            {
                DataTable dttipohr = TipHr.obttipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                dgvtiphr.DataSource = dttipohr;

                dgvtiphr.Columns[0].Visible = false;
                dgvtiphr.Columns[1].Width = 465;
                dgvtiphr.Columns[2].Visible = false;

                dgvtiphr.ClearSelection();
            }
            else if (iact == 1)
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
            else if (ielim == 1)
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
            if (iactbtn == 1)
            {

                iresp = TipHr.uditipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txttipohriu.Text = "";
            }
            //actualiza registro
            else if (iactbtn == 2)
            {

                iresp = TipHr.uditipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txttipohriu.Text = "";
            }
            //elimina registro
            else if (iactbtn == 3)
            {

                iresp = TipHr.uditipohr(p_opcion, p_cvtipohr, p_descripcion, p_usuumod, p_prgumodr);
                //lbMensaje.Text = p_rep.ToString();
                txttipohriu.Text = "";
            }

            switch (iresp.ToString())
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
            if (iins == 1 && iact == 0 && ielim == 0)
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

                    icvtipohr = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    string ValorRow = row.Cells["Descripción"].Value.ToString();

                    pnltiphr.Visible = true;
                    lbluid.Text = "     Modifica Tipo de Horario";
                    txttipohriu.Text = ValorRow;
                    txttipohriu.Focus();

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
