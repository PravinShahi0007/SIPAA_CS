using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//***********************************************************************************************
//Autor: Jose Luis Alvarez Delgado
//Fecha creación: 24-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Catalogo Plantillas
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class Plantillas : Form
    {
        #region

        int ipins;
        int ipact;
        int ipelim;
        int ipactbtn;
        int ip_rep;

        #endregion

        Utilerias Util = new Utilerias();
        DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();

        public Plantillas()
        {
            InitializeComponent();
        }

        //se "instancia" la clase para usar todos los metodos que contenga
        Plantilla oPlantillas = new Plantilla();

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvPlantillas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ipins == 1 && ipact == 1 && ipelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                ipactbtn = 2;
            }
            else if (ipins == 1 && ipact == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ipactbtn = 2;
            }
            else if (ipins == 1 && ipelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                ipactbtn = 2;
            }
            else if (ipact == 1 && ipelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                ipactbtn = 2;
            }

            else if (ipins == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ipactbtn = 2;
            }
            else if (ipact == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ipactbtn = 2;
            }
            else if (ipelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 3, false);
                ckbEliminar.Visible = true;
                ipactbtn = 3;
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
            pnlmensajes.Visible = false;
            gridPlantillas(4, 0, txtMensaje.Text, "","");
            txtMensaje.Text = "";
            txtMensaje.Focus();
        }

        //boton Insertar
        private void btnInsertar_Click(object sender, EventArgs e)
        {          
            if (txtmensajeiu.Text.Trim() == "" && ipactbtn == 1)
            {
                lblavisos.Text = "Capture un dato a guardar";
            }
            else if (ipactbtn == 1)//insertar
            {
                //inserta registro nuevo 
                fuidPlantillas(1, 0, txtmensajeiu.Text.Trim(), "JLA", "InsPlantillas");
                dgvPlantillas.DataSource = null;
                dgvPlantillas.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtmensajeiu.Text = "";
                txtmensajeiu.Focus();
                //llena grid con datos existente
                gridPlantillas(4, 0,"","","");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlmensajes.Visible = false;
            }
            else if (ipactbtn == 2)//actualizar
            {
                //Actualizar
                fuidPlantillas(2, Convert.ToInt32(txtcvplantilla.Text.Trim()), txtmensajeiu.Text.Trim(), "JLA", "InsPlantillas");
                dgvPlantillas.DataSource = null;
                dgvPlantillas.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtmensajeiu.Text = "";
                txtmensajeiu.Focus();
                //llena grid con datos existente
                gridPlantillas(4, 0, "", "", "");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlmensajes.Visible = false;
            }
            else if (ipactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //Eliminar
                    fuidPlantillas(3, Convert.ToInt32(txtcvplantilla.Text.Trim()), txtmensajeiu.Text.Trim(), "JLA", "InsPlantillas");
                    dgvPlantillas.DataSource = null;
                    dgvPlantillas.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txtmensajeiu.Text = "";
                    txtmensajeiu.Focus();
                    //llena grid con datos existente
                    gridPlantillas(4, 0, "","","");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlmensajes.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }
        }

        //boton Agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            btnInsertar.Image = Resources.Guardar;
            ckbEliminar.Visible = false;
            pnlmensajes.Visible = true;
            lbluid.Text = "     Agregar Plantilla";
            Util.ChangeButton(btnAgregar, 1, false);
            ipactbtn = 1;
            txtmensajeiu.Text = "";
            txtmensajeiu.Focus();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

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
        private void Plantillas_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            ipact = 1;
            ipelim = 1;
            ipins = 1;

            ftooltip();

            if (ipins==1)
            {
                btnAgregar.Visible = true;
            }

            //hay que llenar el grid de plantillas
            gridPlantillas(4, 0, "", "", "");
            dgvPlantillas.Columns[1].Visible = false;
            dgvPlantillas.Columns[2].Width = 450;
        }

        //chekBox Eliminar
        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lbluid.Text = "     Elimina Plantilla";
                ipactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lbluid.Text = "     Modifica Plantilla";
                ipactbtn = 2;
            }
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

        private void gridPlantillas(int ipopcion, int ipcvplantilla, string spdescripcion, string spusuumod, string spprgumod)
        {
            if (ipins==1 && ipact==1 && ipelim==1) 
            {
                DataTable dtPlantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtPlantillas;
                
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPlantillas.Columns.Count>2)
                {
                    dgvPlantillas.Columns.RemoveAt(0);
                }
                dgvPlantillas.Columns.Insert(0, imgCheckUsuarios);
                dgvPlantillas.Columns[0].HeaderText = "   Selección";

                dgvPlantillas.ClearSelection();
            }
            else if (ipins == 1 && ipact == 1)
            {
                DataTable dtPlantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtPlantillas;

                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPlantillas.Columns.Count > 2)
                {
                    dgvPlantillas.Columns.RemoveAt(0);
                }
                dgvPlantillas.Columns.Insert(0, imgCheckUsuarios);
                dgvPlantillas.Columns[0].HeaderText = "   Selección";

                dgvPlantillas.ClearSelection();
            }
            else if (ipins == 1 && ipelim == 1)
            {
                DataTable dtPlantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtPlantillas;

                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPlantillas.Columns.Count > 2)
                {
                    dgvPlantillas.Columns.RemoveAt(0);
                }
                dgvPlantillas.Columns.Insert(0, imgCheckUsuarios);
                dgvPlantillas.Columns[0].HeaderText = "   Selección";

                dgvPlantillas.ClearSelection();
            }
            else if (ipact==1 && ipelim==1)
            {
                DataTable dtplantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtplantillas;

                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPlantillas.Columns.Count > 2)
                {
                    dgvPlantillas.Columns.RemoveAt(0);
                }
                dgvPlantillas.Columns.Insert(0, imgCheckUsuarios);
                dgvPlantillas.Columns[0].HeaderText = "   Selección";

                dgvPlantillas.ClearSelection();
            }
            else if (ipins==1)
            {
                DataTable dtplantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtplantillas;

                dgvPlantillas.Columns[0].Visible = false;
                dgvPlantillas.ClearSelection(); 
            }
            else if (ipact==1)
            {
                DataTable dtplantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtplantillas;

                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPlantillas.Columns.Count > 2)
                {
                    dgvPlantillas.Columns.RemoveAt(0);
                }
                dgvPlantillas.Columns.Insert(0, imgCheckUsuarios);
                dgvPlantillas.Columns[0].HeaderText = "   Selección";

                dgvPlantillas.ClearSelection();
            }
            else if (ipelim==1)
            {
                DataTable dtplantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtplantillas;

                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPlantillas.Columns.Count > 2)
                {
                    dgvPlantillas.Columns.RemoveAt(0);
                }
                dgvPlantillas.Columns.Insert(0, imgCheckUsuarios);
                dgvPlantillas.Columns[0].HeaderText = "   Selección";

                dgvPlantillas.ClearSelection();
            }
            else
            {
                DataTable dtplantillas = oPlantillas.ObtenerPlantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                dgvPlantillas.DataSource = dtplantillas;

                dgvPlantillas.Columns[0].Visible = false;
                dgvPlantillas.ClearSelection();
            }
            dgvPlantillas.Columns[1].Visible = false;
            dgvPlantillas.Columns[2].Width = 450;
        }

        private void fuidPlantillas(int ipopcion, int ipcvplantilla, string spdescripcion, string spusuumod, string spprgumod)
        {
            //agrega registro
            if (ipactbtn == 1)
            {
                ip_rep =oPlantillas.fuid_sp_plantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                txtmensajeiu.Text = "";
                txtMensaje.Focus();
            }
            //actualiza registro
            else if (ipactbtn == 2)
            {
                ip_rep =oPlantillas.fuid_sp_plantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                txtmensajeiu.Text = "";
                txtMensaje.Focus();
            }
            //elimina registro
            else if (ipactbtn == 3)
            {
                ip_rep = oPlantillas.fuid_sp_plantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                txtmensajeiu.Text = "";
                txtMensaje.Focus();
            } // 

            switch (ip_rep.ToString())
            {
                case "1":
                    lblavisos.Text = "Registro agregado correctamente";
                    break;
                case "2":
                    lblavisos.Text = "Registro modificado correctamente";
                    break;
                case "3":
                    lblavisos.Text = "Registro eliminado correctamente";
                    break;
                case "99":
                    lblavisos.Text = "Registro ya existe";
                    break;
                default:
                    lblavisos.Text = "";
                    break;
            } 
        } //    

        private void factgrid()
        {
            for (int iContador = 0; iContador < dgvPlantillas.Rows.Count; iContador++)
            {
                dgvPlantillas.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvPlantillas.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvPlantillas.SelectedRows[0];

                string valor1 = row.Cells["CvPlantilla"].Value.ToString();
                string valor2 = row.Cells["Descripcion"].Value.ToString();

                pnlmensajes.Visible = true;
                lbluid.Text = "     Modifica Descripción";
                txtcvplantilla.Text = valor1;
                txtmensajeiu.Text = valor2;

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }
    }
}
