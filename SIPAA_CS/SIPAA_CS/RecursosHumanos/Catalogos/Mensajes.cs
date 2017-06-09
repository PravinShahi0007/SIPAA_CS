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
using SIPAA_CS.App_Code;

//***********************************************************************************************
//Autor: Jose Luis Alvarez Delgado
//Fecha creación: 20-Mar-2017       Última Modificacion: 30-Mar-2017 
//Descripción: Catalogo Mensajes
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{

    #region variables


    #endregion

    public partial class Mensajes : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        int p_rep;

        #endregion

        Utilerias Util = new Utilerias();

        public Mensajes()
        {
            InitializeComponent();
        }

        //se "instancia" la clase para usar todos los metodos que contenga
        Mensaje pantallaMensajes = new Mensaje();
        SonaTrabajador oTrabajador = new SonaTrabajador();

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvMensajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                pactbtn = 2;
            }
            else if (pins == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pact == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                pactbtn = 2;
            }
            else if (pact == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                pactbtn = 2;
            }
            else if (pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 3, false);
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
            txtidtrab.Text = "";
            cbTrabajador.Text = "Seleccionar Empleado...";

            pnlmensajes.Visible = false;
            gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
            txtMensaje.Text = "";
            txtMensaje.Focus();
            dgvMensajes.Columns.RemoveAt(0);
            dgvMensajes.Columns[0].Width = 65;
            dgvMensajes.Columns[1].Width = 80;
            dgvMensajes.Columns[2].Width = 80;
            dgvMensajes.Columns[3].Width = 80;
            dgvMensajes.Columns[4].Width = 300;
        }

        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnldatos.Visible = true;
            btnInsertar.Image = Resources.Guardar;
            ckbEliminar.Visible = false;
            pnlmensajes.Visible = true;
            lbluid.Text = "     Agregar Mensaje";
            Util.ChangeButton(btnAgregar, 1, false);
            pactbtn = 1;
            txtidtrab.Text = "";
            txtmensajeiu.Text = "";
            dtpfechainicial.Text = "";
            dtpfechafin.Text = "";
            //cbTrabajador.Text = "Seleccionar Empleado...";
            //cbTrabajador.Focus();
        }

        //boton
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtmensajeiu.Text.Trim() == "" && pactbtn == 1)
            {
                lblMensaje.Text = "Capture un dato a guardar";
            }
            else if (pactbtn == 1)//insertar
            {
                //inserta registro nuevo 
                fuidMensajes(1, Convert.ToInt32(txtidtrab.Text.Trim()), 0, txtmensajeiu.Text.Trim(), dtpfechainicial.Text.Trim(), dtpfechafin.Text.Trim(), "JLA", "InsMensajes");
                dgvMensajes.DataSource = null;
                dgvMensajes.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtidtrab.Text = "";
                txtmensajeiu.Text = "";
                txtidtrab.Focus();
                //llena grid con datos existente
                gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlmensajes.Visible = false;
            }
            else if (pactbtn == 2)//actualizar
            {
                //Actualizar
                fuidMensajes(2, Convert.ToInt32(txtidtrab.Text.Trim()), 0, txtmensajeiu.Text.Trim(), dtpfechainicial.Text.Trim(), dtpfechafin.Text.Trim(), "JLA", "InsMensajes");
                dgvMensajes.DataSource = null;
                dgvMensajes.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtmensajeiu.Text = "";
                txtidtrab.Text = "";
                txtmensajeiu.Focus();
                //llena grid con datos existente
                gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlmensajes.Visible = false;
            }
            else if (pactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //Eliminar
                    fuidMensajes(3, Convert.ToInt32(txtidtrab.Text.Trim()), 0, txtmensajeiu.Text.Trim(), dtpfechainicial.Text.Trim(), dtpfechafin.Text.Trim(), "JLA", "InsMensajes");
                    dgvMensajes.DataSource = null;
                    dgvMensajes.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txtmensajeiu.Text = "";
                    txtidtrab.Text = "";
                    txtmensajeiu.Focus();
                    //llena grid con datos existente
                    gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlmensajes.Visible = false;
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
            DialogResult result = MessageBox.Show("¿Está seguro, que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Mensajes_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            ftooltip();
            pnldatos.Visible = false;

            pins = 1;
            pact = 1;
            pelim = 1;

            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }

            gridMensajes(4,0,0,"","","","","");

            //genero el listado
            DataTable dtempleados = oTrabajador.obtenerempleados(7, "");
            //lo vacio en el combo
            //cbTrabajador.DataSource = dtempleados;
            //cbTrabajador.DisplayMember = "Nombre";
            //cbTrabajador.ValueMember = "Clave";
            //cbTrabajador.Text = "Seleccionar Empleado...";
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lbluid.Text = "     Elimina Mensaje";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lbluid.Text = "     Modifica Mensaje";
                pactbtn = 2;
            }
        }

        //Selección del combo
        private void cbTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtidtrab.Text = cbTrabajador.SelectedValue.ToString();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

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
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            toolTip1.SetToolTip(this.btnInsertar, "Insertar Registro");
        }

        private void gridMensajes(int popc, int pidtrab, int pcvmensaje, string pdescripcion, string pfeinicio, string pfefin, string pusuumod, string pprgumod)
        {
            if (pins == 1 && pact == 1 && pelim == 1)
            {
                DataTable dtmensajes = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtmensajes;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pins == 1 && pact == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pins == 1 && pelim == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pact == 1 && pelim == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pins == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                dgvMensajes.Columns[0].Visible = false;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;

                dgvMensajes.ClearSelection();
            }
            else if (pact == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pelim == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                dgvMensajes.Columns[0].Visible = false;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
        }

        private void fuidMensajes(int popc, int pidtrab, int pcvmensaje, string pdescripcion, string pfeinicio, string pfefin, string pusuumod, string pprgumod)
        {
            //agrega registro
            if (pactbtn == 1)
            {                
                p_rep = pantallaMensajes.fudimensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                txtmensajeiu.Text = "";
            }
            //actualiza registro
            else if (pactbtn == 2)
            {
                p_rep = pantallaMensajes.fudimensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                txtmensajeiu.Text = "";
            }
            //elimina registro
            else if (pactbtn == 3)
            {
                p_rep = pantallaMensajes.fudimensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                txtmensajeiu.Text = "";
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
            } 
        } //


        private void factgrid()
        {
            for (int iContador = 0; iContador < dgvMensajes.Rows.Count; iContador++)
            {
                dgvMensajes.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvMensajes.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvMensajes.SelectedRows[0];

                string valor1 = row.Cells["FechaInicial"].Value.ToString();
                string valor2 = row.Cells["FechaFin"].Value.ToString();
                string valor3 = row.Cells["NoEmpleado"].Value.ToString();
                string ValorRow = row.Cells["Mensaje"].Value.ToString();

                pnlmensajes.Visible = true;
                lbluid.Text = "     Modifica Mensaje";
                
                dtpfechainicial.Text= valor1;
                dtpfechafin.Text = valor2;
                txtidtrab.Text = valor3;
                txtmensajeiu.Text = ValorRow;
                //dejo solo el mensaje

                pnldatos.Visible = false;

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }
    }
}
