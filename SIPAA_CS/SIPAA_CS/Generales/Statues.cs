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
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.Generales;

//***********************************************************************************************
//Autor: Marco Dupont
//Fecha creación: 29-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Valores de los Status
//***********************************************************************************************

namespace SIPAA_CS.Generales
{
    public partial class Statues : Form
    {
        #region

        int iAgr;
        int iAct;
        int iEli;

        int iVld_c;
        string sCv_c;
        string sDescAnt;
        int ivst_c;
        int iSt_c;
        

        #endregion

        statue Cstatus = new statue();
        Utilerias Util = new Utilerias();
        public Statues()
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
            SLlenaGrid(4, "", 0, "", 0, "", "");
            //txtBusca.Text = "";
            dgvConsulta.Focus();
            pnlAct.Visible = false;

        }
        private void cbobusq_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LLAMA METODO LLENAR GRID
            pnlAct.Visible = false;
            SLlenaGrid(4, cbobusq.SelectedValue.ToString(), 0, "", 0, "", "");
            //txtBusca.Text = "";
            cbobusq.Focus();
            pnlAct.Visible = false;

        }

        //BOTOS AGREGAR REGISTRO NUEVO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlAct.Visible = true;
            lblAct.Text = "     Agregar Status";
            btnEliminar.Visible = false;
            btnEditar.Visible = false;
            ckbEliminar.Visible = false;
            btnGuardar.Visible = true;
            txtCapcv.Text = "";
            txtCapcv.Visible = false;
            panel3.Visible = false;
            cboCrea.Visible = true;
            txtCapSt.Text = "";
            txtCapSt.ReadOnly = false;
            txtCapDes.Text = "";
            iSt_c = 1;
            cboCrea.Focus();
        }
        //BOTON BAJA
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta acción da de baja el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                iSt_c = 0;
                sGuardaMod(3, txtCapcv.Text.Trim(), ivst_c, "", iSt_c, "150076", "Statues");
                SLlenaGrid(4, txtCapcv.Text.Trim(), 0, "", 0, "", "");
                txtCapcv.Text = "";
                txtCapSt.Text = "";
                txtCapDes.Text = "";
                panelTag.Visible = true;
                timer1.Start();
                sCv_c = "";
                ivst_c = 0;
                pnlAct.Visible = false;
                cbobusq.Focus();
            }
            else if (result == DialogResult.No)
            {

            }
        }
        //BOTON ALTA
        private void btnActiva_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta acción habilita el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                iSt_c = 1;
                sGuardaMod(3, txtCapcv.Text.Trim(), ivst_c, "", iSt_c, "150076", "Statues");
                SLlenaGrid(4, txtCapcv.Text.Trim(), 0, "", 0, "", "");
                txtCapcv.Text = "";
                txtCapSt.Text = "";
                txtCapDes.Text = "";
                panelTag.Visible = true;
                timer1.Start();
                sCv_c = "";
                ivst_c = 0;
                pnlAct.Visible = false;
                cbobusq.Focus();
            }
            else if (result == DialogResult.No)
            {

            }
        }
        //BOTON EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //VALIDA ESCRITURA DE ALGUN TEXTO
            if (txtCapDes.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Seleccione un dato en el grid a modificar", "SIPAA", MessageBoxButtons.OK);
            }
            else
            {
                if (sDescAnt != txtCapDes.Text)
                {
                    sValIns(6, txtCapcv.Text, 0, txtCapDes.Text, 0, "", "");

                    if (iVld_c <= 0)
                    {
                        sGuardaMod(2, txtCapcv.Text, ivst_c, txtCapDes.Text, iSt_c, "150076", "Statues");
                        SLlenaGrid(4, txtCapcv.Text, 0, "", 0, "", "");
                        txtCapcv.Text = "";
                        txtCapSt.Text = "";
                        txtCapDes.Text = "";
                        panelTag.Visible = true;
                        timer1.Start();
                        sCv_c = "";
                        ivst_c = 0;
                        pnlAct.Visible = false;
                        cbobusq.Focus();
                    }
                    else
                    {
                        SLlenaGrid(4, txtCapcv.Text, 0, "", 0, "", "");
                        txtCapcv.Text = "";
                        txtCapSt.Text = "";
                        txtCapDes.Text = "";
                        pnlAct.Visible = false;
                        DialogResult result = MessageBox.Show("La Descripción ya existente", "SIPAA", MessageBoxButtons.OK);
                        cbobusq.Focus();
                    }

                }
                else
                {
                    sGuardaMod(2, txtCapcv.Text, ivst_c, txtCapDes.Text, iSt_c, "150076", "Statues");
                    SLlenaGrid(4, txtCapcv.Text, 0, "", 0, "", "");
                    txtCapcv.Text = "";
                    txtCapSt.Text = "";
                    txtCapDes.Text = "";
                    panelTag.Visible = true;
                    timer1.Start();
                    sCv_c = "";
                    ivst_c = 0;
                    pnlAct.Visible = false;
                    cbobusq.Focus();
                }
            }
        }
        //BOTON GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //VALIDA ESCRITURA DE ALGUN TEXTO

            if (cboCrea.SelectedValue.ToString() == "")
            {
                DialogResult result = MessageBox.Show("Captura el dato a guardar", "SIPAA", MessageBoxButtons.OK);
                cboCrea.Focus();
            }
            else
            {
                if (txtCapSt.Text.Trim() == "")
                {
                    DialogResult result = MessageBox.Show("Captura el dato a guardar", "SIPAA", MessageBoxButtons.OK);
                    txtCapSt.Focus();
                }
                else
                {
                    ivst_c = int.Parse(txtCapSt.Text);
                    sValIns(5, cboCrea.SelectedValue.ToString(), ivst_c, "", 0, "", "");

                    if (iVld_c <= 0)
                    {
                        if (txtCapDes.Text.Trim() == "")
                        {
                            DialogResult result = MessageBox.Show("Captura el dato a guardar", "SIPAA", MessageBoxButtons.OK);
                            txtCapDes.Focus();
                        }
                        else
                        {
                            ivst_c = int.Parse(txtCapSt.Text);
                            sGuardaMod(1, cboCrea.SelectedValue.ToString(), ivst_c, txtCapDes.Text, 1, "150076", "Statues");
                            txtCapcv.Text = "";
                            txtCapSt.Text = "";
                            txtCapDes.Text = "";
                            panelTag.Visible = true;
                            timer1.Start();

                            SLlenaGrid(4, cboCrea.SelectedValue.ToString(), 0, "", 0, "", "");
                            pnlAct.Visible = false;
                            cbobusq.Focus();
                        }
                    }
                    else
                    {
                        SLlenaGrid(4, cboCrea.SelectedValue.ToString(), 0, "", 0, "", "");
                        txtCapcv.Text = "";
                        txtCapSt.Text = "";
                        txtCapDes.Text = "";
                        pnlAct.Visible = false;
                        DialogResult result = MessageBox.Show("Registro ya existente", "SIPAA", MessageBoxButtons.OK);
                        cbobusq.Focus();
                    }
                }
            }
        }
        //BOTON REGRESAR
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Statues_Load(object sender, EventArgs e)
        {
            //Configuracion de la pantalla
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            //LLAMA TOOL TIP
            sTooltip();

            iAgr = 1;
            iAct = 1;
            iEli = 1;

            Util.cargarcombo(cbobusq, Cstatus.cboTab(9));
            Util.cargarcombo(cboCrea, Cstatus.cboTab(9));
            
            //LLAMA METODO LLENAR GRID
            SLlenaGrid(4, "", 0, "", 0, "", "");
            cbobusq.Focus();

            //HABILITA BOTON AGREGAR
            if (iAgr == 1)
            {
                btnAgregar.Visible = true;
                pnlAct.Visible = false;
            }
        }
        //evento tick de timer de mensajes
        private void timer1_Tick_1(object sender, EventArgs e)
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
            toolTip1.SetToolTip(this.btnActiva, "Activa Registro");
        }

        //LLENA GRID
        private void SLlenaGrid(int p_opcion, string p_cvtabla, int p_stvalor, string p_descripcion, int p_ststatus, string p_usuumod, string p_prgumodr)
        {

            DataTable dtConsulta = Cstatus.statue_S(p_opcion, p_cvtabla, p_stvalor, p_descripcion, p_ststatus, p_usuumod, p_prgumodr);
            dgvConsulta.DataSource = dtConsulta;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            if (dgvConsulta.Columns[0].HeaderText != "Selección")
            {
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvConsulta.Columns.Insert(0, imgCheckUsuarios);
                dgvConsulta.Columns[0].HeaderText = "Selección";
            }

            dgvConsulta.Columns[0].Width = 55;
            dgvConsulta.Columns[1].Width = 90;
            dgvConsulta.Columns[2].Width = 35;
            dgvConsulta.Columns[3].Width = 130;
            dgvConsulta.Columns[4].Width = 45;
            dgvConsulta.Columns[5].Visible = false;

            dgvConsulta.ClearSelection();
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

        private void ckbEliminar_CheckedChanged_1(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                if (iSt_c == 1)
                {
                    btnGuardar.Visible = false;
                    btnEditar.Visible = false;
                    btnEliminar.Visible = true;
                    btnActiva.Visible = false;
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnEditar.Visible = true;
                    btnEliminar.Visible = false;
                    btnActiva.Visible = true;
                }
            }
            else
            {
                btnGuardar.Visible = false;
                btnEditar.Visible = true;
                btnEliminar.Visible = false;
                btnActiva.Visible = false;
            }
        }

        private void dgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvConsulta.Rows.Count; iContador++)
            {
                dgvConsulta.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            sHabilitaPermisos();

            if (dgvConsulta.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvConsulta.SelectedRows[0];

                txtCapcv.Text = row.Cells["Clave"].Value.ToString();
                txtCapSt.Text = row.Cells["Val Status"].Value.ToString();
                txtCapDes.Text = row.Cells["Descripción"].Value.ToString();
                iSt_c = Convert.ToInt32(row.Cells["ST"].Value.ToString());

                sCv_c = txtCapcv.Text;
                ivst_c = int.Parse(txtCapSt.Text);
                txtCapcv.ReadOnly = true;
                txtCapSt.ReadOnly = true;
                txtCapcv.Visible = true;
                panel3.Visible = true;
                cboCrea.Visible = false;
                sDescAnt = txtCapDes.Text;

                txtCapDes.Focus();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                if (iSt_c == 0)
                {
                    ckbEliminar.Text = "Alta";
                }
                else
                {
                    ckbEliminar.Text = "Baja";
                }

            }
        }

        //GUARDA MODIFICA BAJA
        private void sGuardaMod(int iOpc, string sCve, int ivst, string sDesc, int iStT, string sUsu, string sProg)
        {
            Cstatus.statue_UID(iOpc, sCve, ivst, sDesc, iStT, sUsu, sProg);
            if (iOpc == 1)
            {
                lbMensaje.Text = "Registro almacenado";
            }
            else if (iOpc == 2)
            {
                lbMensaje.Text = "Registro actualizado";
            }
            else if (iOpc == 3)
            {
                if(iSt_c == 0)
                {
                    lbMensaje.Text = "Registro dado de baja";
                }
                else
                {
                    lbMensaje.Text = "Registro dado de alta";
                }
            }
        }
        private void sValIns(int iOpc, string sCve, int ivst, string sDesc, int iStT, string sUsu, string sProg)
        {
            iVld_c = Cstatus.statue_V(iOpc, sCve, ivst, sDesc, iStT, sUsu, sProg);
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
