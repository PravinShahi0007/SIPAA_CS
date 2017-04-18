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
//Fecha creación: 17-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Administra Formas de Registro Empleado
//***********************************************************************************************
namespace SIPAA_CS.RecursosHumanos
{
    public partial class FormasRegistros : Form
    {
        #region

        int vValida;
        int iAgr;
        int iAct;
        int iEli;
        int iCvFR;
        int iSt;
        string sDescAnt;

        #endregion

        FormaReg FormasRegistro = new FormaReg();

        public FormasRegistros()
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
            SLlenaGrid(4,0, txtFormReg.Text.Trim(), 0, "", "");
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
            btnActiva.Visible = false;
            iSt = 1;
            txtCapFR.Text = "";
            txtCapFR.Focus();
        }

        //BOTON GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //VALIDA ESCRITURA DE ALGUN TEXTO
                if (txtCapFR.Text.Trim() == "")
                {
                    DialogResult result = MessageBox.Show("Captura el dato a guardar", "SIPAA", MessageBoxButtons.OK);
                }
                else
                {
                    sValIns(5, 0, txtCapFR.Text.Trim(), 0, "", "");

                    if (vValida <= 0)
                    {
                        sGuardaMod(1, 0, txtCapFR.Text.Trim(), iSt, "150076", "frmFormReg");
                        txtCapFR.Text = "";

                        panelTag.Visible = true;
                        timer1.Start();

                        SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "", "");
                        txtCapFR.Focus();
                        pnlAct.Visible = false;
                    }
                    else
                    {
                        SLlenaGrid(4, 0, txtCapFR.Text.Trim(), 0, "", "");
                        txtCapFR.Text = "";
                        txtCapFR.Focus();
                        pnlAct.Visible = false;
                        DialogResult result = MessageBox.Show("Registro existente", "SIPAA", MessageBoxButtons.OK);
                        txtFormReg.Focus();
                    }
                }
           }

        //BOTON EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {

            //VALIDA ESCRITURA DE ALGUN TEXTO
            if (txtCapFR.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("La Descripcion no puede estar en blanco", "SIPAA", MessageBoxButtons.OK);
                txtCapFR.Focus();
            }
            else
            {
                if (sDescAnt != txtCapFR.Text.Trim())
                {
                    sValIns(5, 0, txtCapFR.Text.Trim(), 0, "", "");

                    if (vValida <= 0)
                    {
                        sGuardaMod(2, iCvFR, txtCapFR.Text.Trim(), 0, "150076", "frmFormReg");
                        txtCapFR.Text = "";
                        panelTag.Visible = true;
                        timer1.Start();
                        SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "", "");
                        iCvFR = 0;
                        pnlAct.Visible = false;
                    }
                    else
                    {
                        SLlenaGrid(4, 0, txtCapFR.Text.Trim(), 0, "", "");
                        txtCapFR.Text = "";
                        txtCapFR.Focus();
                        pnlAct.Visible = false;
                        DialogResult result = MessageBox.Show("Registro existente", "SIPAA", MessageBoxButtons.OK);
                        txtFormReg.Focus();
                    }
                }
                else
                {
                    sGuardaMod(2, iCvFR, txtCapFR.Text.Trim(), 0, "150076", "frmFormReg");
                    txtCapFR.Text = "";
                    panelTag.Visible = true;
                    timer1.Start();
                    SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "", "");
                    iCvFR = 0;
                    pnlAct.Visible = false;
                }
            }
        }

        //BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta acción de de baja el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                iSt = 0;
                sGuardaMod(3, iCvFR, txtCapFR.Text.Trim(), iSt, "150076", "frmFormReg");
                txtCapFR.Text = "";
                panelTag.Visible = true;
                timer1.Start();
                SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "", "");
                iCvFR = 0;
                pnlAct.Visible = false;
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
                    iSt = 1;
                    sGuardaMod(3, iCvFR, txtCapFR.Text.Trim(), iSt, "150076", "frmFormReg");
                    txtCapFR.Text = "";
                    panelTag.Visible = true;
                    timer1.Start();
                    SLlenaGrid(4, 0, txtFormReg.Text.Trim(), 0, "", "");
                    iCvFR = 0;
                    pnlAct.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
        }
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
        private void frmFormReg_Load(object sender, EventArgs e)
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

            //LLAMA METODO LLENAR GRID
            SLlenaGrid(4, 0,"",0,"","");
            
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
            toolTip1.SetToolTip(this.btnActiva, "Activa Registro");
        }

        //LLENA GRID
        private void SLlenaGrid(int p_opcion, int p_cvforma, string p_descripcion, int p_stforma, string p_usuumod, string p_prgumodr)
        {

            DataTable dtFormasRegistro = FormasRegistro.FormaReg_S(p_opcion, p_cvforma, p_descripcion, p_stforma, p_usuumod, p_prgumodr);
            dgvForReg.DataSource = dtFormasRegistro;
            
            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            if (dgvForReg.Columns[0].HeaderText != "Selección")
            {
                imgCheckUsuarios.Name = "img";
                dgvForReg.Columns.Insert(0, imgCheckUsuarios);
                dgvForReg.Columns[0].HeaderText = "Selección";
            }
            dgvForReg.Columns[0].Width = 55;
            dgvForReg.Columns[1].Visible = false;
            dgvForReg.Columns[2].Width = 155;
            dgvForReg.Columns[3].Width = 45;
            dgvForReg.Columns[4].Visible = false;

            dgvForReg.ClearSelection();
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
            else if(iAct == 1)
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
                if(iSt == 1)
                {
                    btnGuardar.Visible = false;
                    btnEditar.Visible = false;
                    btnEliminar.Visible = true;
                    btnActiva.Visible = false;
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnEditar.Visible = false;
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

        private void dgvForReg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            for (int iContador = 0; iContador < dgvForReg.Rows.Count; iContador++)
            {
                dgvForReg.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }
            
            sHabilitaPermisos();

            if (dgvForReg.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvForReg.SelectedRows[0];

                iCvFR = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                string ValorRow = row.Cells["Descripción"].Value.ToString();
                iSt = Convert.ToInt32(row.Cells["ST"].Value.ToString());
                
                txtCapFR.Text = ValorRow;
                sDescAnt = txtCapFR.Text;
                txtCapFR.Focus();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                if (iSt == 0)
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
        private void sGuardaMod(int iOpc, int sCve, string sDesc, int iStT, string sUsu, string sProg)
        {
            FormasRegistro.formaReg_UID(iOpc, sCve, sDesc, iStT, sUsu, sProg);
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
                if (iSt == 0)
                {
                    lbMensaje.Text = "Registro dado de baja";
                }
                else
                {
                    lbMensaje.Text = "Registro dado de alta";
                }
            }
        }
        private void sValIns(int iOpc, int sCve, string sDesc, int iStT, string sUsu, string sProg)
        {
            vValida = FormasRegistro.FormaReg_V(iOpc, sCve, sDesc, iStT, sUsu, sProg);
            
        }



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
