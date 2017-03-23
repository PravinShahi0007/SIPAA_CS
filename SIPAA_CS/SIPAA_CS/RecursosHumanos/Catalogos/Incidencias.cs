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
//Autor: Marco Dupont
//Fecha creación: 22-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Catálogo de Incidencias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class Incidencias : Form
    {
        #region

        int vValida;
        int iAgr;
        int iAct;
        int iEli;
        int iCvFR;

        #endregion

        Incidencia CIncidencias = new Incidencia();
        public Incidencias()
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
        //BOTON BUSCAR
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //LLAMA METODO LLENAR GRID
            pnlAct.Visible = false;
            SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "150076", "frmFormReg");
            txtFormReg.Text = "";
            txtFormReg.Focus();
            pnlAct.Visible = false;
        }

        //BOTOS AGREGAR REGISTRO NUEVO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlAct.Visible = true;
            lblAct.Text = "     Agregar forma de registro";
            btnEliminar.Visible = false;
            btnEditar.Visible = false;
            ckbEliminar.Visible = false;
            btnGuardar.Visible = true;
            txtCapInc.Text = "";
            txtCapInc.Focus();
        }

        //BOTON GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            //VALIDA ESCRITURA DE ALGUN TEXTO
            if (txtCapInc.Text.Trim() == "")
            {

                DialogResult result = MessageBox.Show("Captura el dato a guardar", "SIPAA", MessageBoxButtons.OK);

            }
            else
            {
                sValIns(5, 0, txtCapInc.Text.Trim(), 0, "150076", "frmFormReg");

                if (vValida <= 0)
                {
                    sGuardaMod(1, 0, txtCapInc.Text.Trim(), 1, "150076", "frmFormReg");
                    txtCapInc.Text = "";

                    panelTag.Visible = true;
                    timer1.Start();

                    SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "150076", "frmFormReg");
                    txtCapInc.Focus();
                    pnlAct.Visible = false;
                }
                else
                {
                    SLlenaGrid(4, 0, txtCapInc.Text.Trim(), 0, "150076", "frmFormReg");
                    txtCapInc.Text = "";
                    txtCapInc.Focus();
                    pnlAct.Visible = false;
                    DialogResult result = MessageBox.Show("El Registro ya existe", "SIPAA", MessageBoxButtons.OK);
                }
            }

        }

        //BOTON EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {

            //VALIDA ESCRITURA DE ALGUN TEXTO
            if (txtCapInc.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Seleccione un dato en el grid a modificar", "SIPAA", MessageBoxButtons.OK);
            }
            else
            {
                if (ckbEliminar.Checked == true)
                {

                }
                else
                {
                    sGuardaMod(2, iCvFR, txtCapInc.Text.Trim(), 0, "150076", "frmFormReg");
                    txtCapInc.Text = "";
                    panelTag.Visible = true;
                    timer1.Start();
                    SLlenaGrid(4, iCvFR, txtFormReg.Text.Trim(), 0, "150076", "frmFormReg");
                    iCvFR = 0;
                    pnlAct.Visible = false;
                }
            }
        }

        //BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                sGuardaMod(3, iCvFR, txtCapInc.Text.Trim(), 0, "150076", "frmFormReg");
                txtCapInc.Text = "";
                panelTag.Visible = true;
                timer1.Start();
                SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "150076", "frmFormReg");
                iCvFR = 0;
                pnlAct.Visible = false;
            }
            else if (result == DialogResult.No)
            {

            }
        }

        //BOTON MINIMIZAR
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //BOTON CERRAR
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
        private void frmFormReg_Load(object sender, EventArgs e)
        {
            //LLAMA TOOL TIP
            sTooltip();

            iAgr = 1;
            iAct = 1;
            iEli = 1;

            //LLAMA METODO LLENAR GRID
            SLlenaGrid(4, 0, "", 0, "", "");

            //HABILITA BOTON AGREGAR
            if (iAgr == 1)
            {
                btnAgregar.Visible = true;
                pnlAct.Visible = false;
            }
        }
        //evento tick de timer de mensajes
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
        //-----------------------------------------------------------------------------------------------
        //                                  S U B R U T I N A S
        //-----------------------------------------------------------------------------------------------
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierra Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimiza Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresa");
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
            toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            toolTip1.SetToolTip(this.btnEliminar, "Elimina Registro");
        }

        //LLENA GRID
        private void SLlenaGrid(int p_opcion, int p_cvIncidencia, string p_descripcion, int p_orden, string p_usuumod, string p_prgumodr)
        {

            DataTable dtIncidencia = CIncidencias.Incidencia_S(p_opcion, p_cvIncidencia, p_descripcion, p_orden, p_usuumod, p_prgumodr);
            dgvIncidencia.DataSource = dtIncidencia;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvIncidencia.Columns.Insert(0, imgCheckUsuarios);
            dgvIncidencia.Columns[0].HeaderText = "Selección";

            dgvIncidencia.Columns[1].Visible = false;
            dgvIncidencia.Columns[0].Width = 75;
            dgvIncidencia.Columns[2].Width = 255;
            dgvIncidencia.Columns[3].Width = 90;

            dgvIncidencia.ClearSelection();
            sHabilitaPermisos();
        }

        //HABILITA PERMISOS
        private void sHabilitaPermisos()
        {

            //HABILITA MODITICAR ELIMINAR
            if (iAct == 1 && iEli == 1)
            {

                lblModifElim.Visible = true;

                pnlAct.Visible = true;
                btnEliminar.Visible = false;
                btnGuardar.Visible = false;
                ckbEliminar.Visible = true;

                ckbEliminar.Checked = false;

                lblModifElim.Visible = true;
                lblModifElim.Text = "Si desea modificar o eliminar un registro seleccione en el grid";

                btnEditar.Visible = true;

            }
            else if (iAct == 1)
            {

                pnlAct.Visible = true;
                lblModifElim.Visible = true;
                lblModifElim.Text = "Si desea modificar un registro seleccione en el grid";

            }
            else if (iEli == 1)
            {

                pnlAct.Visible = true;
                lblModifElim.Visible = true;
                lblModifElim.Text = "Si desea eliminar un registro seleccione en el grid";

            }
            else
            {
                //pnlAct.Visible = false;
                lblModifElim.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {

                btnGuardar.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = true;

            }
        }

        private void dgvIncidencia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            for (int iContador = 0; iContador < dgvIncidencia.Rows.Count; iContador++)
            {
                dgvIncidencia.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            sHabilitaPermisos();

            if (dgvIncidencia.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvIncidencia.SelectedRows[0];

                iCvInc = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                string ValorRow = row.Cells["Descripción"].Value.ToString();

                txtCapInc.Text = ValorRow;
                txtCapInc.Focus();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }

        //GUARDA MODIFICA BAJA
        private void sGuardaMod(int iOpc, int sCve, string sDesc, int sOrd, string sUsu, string sProg)
        {
            CIncidencias.Incidencia_UID(iOpc, sCve, sDesc, sOrd, sUsu, sProg);
            if (iOpc == 1)
            {
                lbMensaje.Text = "Registro agregado correctamente";
            }
            else if (iOpc == 2)
            {
                lbMensaje.Text = "Registro modificado correctamente";
            }
            else if (iOpc == 3)
            {
                lbMensaje.Text = "Registro eliminado correctamente";
            }
        }
        private void sValIns(int iOpc, int sCve, string sDesc, int sOrd, string sUsu, string sProg)
        {
            vValida = CIncidencias.Incidencia_V(iOpc, sCve, sDesc, sOrd, sUsu, sProg);

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
