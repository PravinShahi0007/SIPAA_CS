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

namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    public partial class frmFormReg : Form
    {
        #region

        int iAgr;
        int iAct;
        int iEli;
        int iCvFR;

        #endregion

        FormReg FormasRegistro = new FormReg();
  

        public frmFormReg()
        {
            InitializeComponent();
        }
        //***********************************************************************************************
        //Autor: Noe Alvarez Marquina
        //Fecha creación: 08-Mar-2017       Última Modificacion: dd-mm-aaaa
        //Descripción: Administra formas de registro
        //***********************************************************************************************

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
            //VALIDA ESCRITURA DE ALGUN TEXTO
            //if (txtFormReg.Text.Trim() == "")
            //{

               // DialogResult result = MessageBox.Show("Captura Texto deseado", "SIPAA", MessageBoxButtons.OK);

           // }
           // else
           // {

                //LLAMA METODO LLENAR GRID
                pnlAct.Visible = false;
                SLlenaGrid(1, txtFormReg.Text.Trim());
                txtFormReg.Text = "";

            //}

            txtFormReg.Focus();

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

                sGuardaMod(1,0,txtCapFR.Text.Trim(),"noe", "frmFormReg");
                txtCapFR.Text = "";
                SLlenaGrid(1, txtFormReg.Text.Trim());
                DialogResult result = MessageBox.Show("Dato Guardado con Exito!!!!!", "SIPAA", MessageBoxButtons.OK);
                txtCapFR.Focus();

            }

        }

        //BOTON EDITAR
        private void btnEditar_Click(object sender, EventArgs e)
        {

            //VALIDA ESCRITURA DE ALGUN TEXTO
            if (txtCapFR.Text.Trim() == "")
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

                    sGuardaMod(2, iCvFR, txtCapFR.Text.Trim(), "noe", "frmFormReg");
                    txtCapFR.Text = "";
                    SLlenaGrid(1, txtFormReg.Text.Trim());
                    iCvFR = 0;
                    DialogResult result = MessageBox.Show("Dato Modificado con Exito!!!!!", "SIPAA", MessageBoxButtons.OK);
                    pnlAct.Visible = false;
                    //txtCapFR.Focus();

                }


            }

        }

        //BOTON ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                sGuardaMod(3, iCvFR, txtCapFR.Text.Trim(),"noe", "frmFormReg");
                txtCapFR.Text = "";
                SLlenaGrid(1, txtFormReg.Text.Trim());
                iCvFR = 0;
                DialogResult result1 = MessageBox.Show("Dato Eliminado con Exito!!!!!", "SIPAA", MessageBoxButtons.OK);
                pnlAct.Visible = false;
                //txtCapFR.Focus();
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

            //LLAMA METODO LLENAR GRID
            SLlenaGrid(1, "");
            
            iAgr = 1;
            iAct = 1;
            iEli = 1;

            //HABILITA BOTON AGREGAR
            if (iAgr == 1)
            {

                btnAgregar.Visible = true;
            }

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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
            toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            toolTip1.SetToolTip(this.btnEliminar, "Alimina Registro");

        }

        //LLENA GRID
        private void SLlenaGrid(int Opc, string Busq)
        {

            DataTable dtFormasRegistro = FormasRegistro.cObtFormRegBusqueda(Opc, Busq);
            dgvForReg.DataSource = dtFormasRegistro;
            
            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvForReg.Columns.Insert(0, imgCheckUsuarios);
            dgvForReg.Columns[0].HeaderText = "";

            dgvForReg.Columns[1].Visible = false;
            dgvForReg.Columns[0].Width = 55;
            dgvForReg.Columns[2].Width  = 302;

            dgvForReg.ClearSelection();
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

                btnGuardar.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = true;

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

                iCvFR = Convert.ToInt32(row.Cells["CLAVE"].Value.ToString());
                string ValorRow = row.Cells["DESCRIPCION"].Value.ToString();

                txtCapFR.Text = ValorRow;
                txtCapFR.Focus();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }



        }

        //GUARDA MODIFICA BAJA
        private void sGuardaMod(int iOpc, int sCve, string sDesc, string sUsu, string sProg)
        {

            FormasRegistro.sGuardaModifReg(iOpc, sCve, sDesc, sUsu, sProg);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
