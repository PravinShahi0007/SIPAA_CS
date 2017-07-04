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
using SIPAA_CS.App_Code.Generales;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;

//***********************************************************************************************
//Autor: Benjamin Huizar Barajas
//Fecha creación: 28-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Períodos Proceso Incidencias
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class PeriodosProcesoIncidencia : Form
    {
        #region Variables

        int iIns;
        int iAct;
        int iElim;
        int iActbtn;
        int iIdFormaPago;
        string sFechaInicioPeriodoIncidencia;
        string sFechaFinPeriodoIncidencia;
        string sDescripcion;
        int iStPeriodoIncidencia;
        int iResp; // iResp -> iResp

        string sCadenaStatusPeriodo;
        bool bClickPrimeraVezFormaPago = true;

        #endregion

        statue oStatus = new statue();
        SonaFormaPago oSonaFormaPago = new SonaFormaPago();
        PeriodoProcesoIncidencia oPeriodoProcesoIncidencia = new PeriodoProcesoIncidencia();
        Utilerias Util = new Utilerias();


        public PeriodosProcesoIncidencia()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        //
        // Llena Combo Box de Forma Pago de la tabla de SONARH
        private void cbFormaPago_Click(object sender, EventArgs e)
        {
            if (bClickPrimeraVezFormaPago)
            {
                DataTable dtFormaPago = oSonaFormaPago.FormaPago_S(4, 0, "");
                cbFormaPago.DataSource = dtFormaPago;
                cbFormaPago.DisplayMember = "Descripción";
                cbFormaPago.ValueMember = "Clave";
                bClickPrimeraVezFormaPago = false;
            } // if (bClickPrimeraVezFormaPago)
        } // private void cbFormaPago_Click(object sender, EventArgs e)

        private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIdFormaPagoBuscar = 0;
            if (!bClickPrimeraVezFormaPago)
            {
                //
                // 
                pnlActPeriodoIncidencia.Visible = false;
                iIdFormaPagoBuscar = Convert.ToInt32(cbFormaPago.SelectedValue.ToString());
                iIdFormaPago = iIdFormaPagoBuscar;
                fgPeriodosProcesoIncidencia(6, iIdFormaPagoBuscar, "", "", "", iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
            } // if (!bClickPrimeraVezFormaPago)
        } // private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)

        private void cbStatusPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
//            string sCadena = "";
//            sCadena = cbStatusPeriodo.SelectedValue.ToString();
        } // private void cbStatusPeriodo_SelectedIndexChanged(object sender EventArgs e)

        private void cbStatusPeriodo_SelectedValueChanged(object sender, EventArgs e)
        {
            sCadenaStatusPeriodo = cbStatusPeriodo.SelectedValue.ToString();

            if (sCadenaStatusPeriodo != "System.Data.DataRowView")
            {
                iStPeriodoIncidencia = Convert.ToInt32(sCadenaStatusPeriodo);
            }

            /*            sCadenaStatusPeriodo = cbStatusPeriodo.Text;
                        switch (sCadenaStatusPeriodo)
                        {
                            case "Procesar":
                                iStPeriodoIncidencia = 2;
                                break;
                            case "Abierto":
                                iStPeriodoIncidencia = 1;
                                break;
                            case "Cerrado":
                                iStPeriodoIncidencia = 0;
                                break;
                        } // switch (sCadenaStatusPeriodo)*/
        } // private void cbStatusPeriodo_SelectedIndexChanged(object sender EventArgs e)

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvPeriodosProcesoIncidencias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (iIns == 1 && iAct == 1 && iElim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                iActbtn = 2;
            }
            else if (iIns == 1 && iAct == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                iActbtn = 2;
            }
            else if (iIns == 1 && iElim == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                iActbtn = 2;
            }
            else if (iAct == 1 && iElim == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                iActbtn = 2;
            }
            else if (iIns == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                iActbtn = 2;
            }
            else if (iAct == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                iActbtn = 2;
            }
            else if (iElim == 1)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                factgrid();
                ckbEliminar.Visible = true;
                iActbtn = 3;
            }
            else
            {

            }

        } // private void dgvPeriodosProcesoIncidencias_CellContentClick(object sender, DataGridViewCellEventArgs e)


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvPeriodosProcesoIncidencias.DataSource = null;
            //llena grid con datos existente
            fgPeriodosProcesoIncidencia(4, iIdFormaPago, "", "", "", iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
            txtDescripcionPeriodoIncidencia.Text = "";
            txtDescripcionPeriodoIncidencia.Focus();
            if (dgvPeriodosProcesoIncidencias.Columns.Count > 3)
            {
                dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
            }
        }
        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //
            // Valida que haya seleccionado un valor del Combo de Forma Pago
            if(cbFormaPago.SelectedIndex!=-1) // Se ha seleccionado un elemento del Combo Box
            {
                ckbEliminar.Visible = false;
                pnlActPeriodoIncidencia.Visible = true;
                lblActPeriodoIncidencia.Text = "     Agregar Período Proceso Incidencia";
                Util.ChangeButton(btnAgregar, 1, false);
                txtDescripcionPeriodoIncidencia.Text = cbFormaPago.Text;
                iActbtn = 1;

                //cbStatusPeriodo.SelectedItem = "Procesar";
                //cbStatusPeriodo.SelectedValue = 2;
                fStatusPeriodoIncidencia(4, "rechtperiodopro", 0, "", 0, "bhb", "PeriodosProcesoIncidencia");
//                cbStatusPeriodo.SelectedText = "Procesar";
                cbStatusPeriodo.Text = "Procesar";

                dtpFechaInicioPeriodoIncidencia.Text = "";
                dtpFechaFinPeriodoIncidencia.Text = "";
            }
            else
            {
                lblMensaje.Text = "Debes seleccionar una Forma de Pago";
                panelTag.Visible = true;
                timer1.Start();
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtDescripcionPeriodoIncidencia.Text.Trim() == "" && iActbtn == 1)
            {
                lblMensaje.Text = "Capture un dato a guardar";
            }
            else if (iActbtn == 1)//insertar
            {
                //inserta registro nuevo
                fuidPeriodosProcesoIncidencia(1, iIdFormaPago, dtpFechaInicioPeriodoIncidencia.Text, dtpFechaFinPeriodoIncidencia.Text, txtDescripcionPeriodoIncidencia.Text.Trim(), iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
                dgvPeriodosProcesoIncidencias.DataSource = null;
                dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtDescripcionPeriodoIncidencia.Text = "";
                txtDescripcionPeriodoIncidencia.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgPeriodosProcesoIncidencia(6, iIdFormaPago, "", "", "", iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActPeriodoIncidencia.Visible = false;
            }
            else if (iActbtn == 2)//actualizar
            {
                //inserta registro nuevo
                fuidPeriodosProcesoIncidencia(2, iIdFormaPago, dtpFechaInicioPeriodoIncidencia.Text, dtpFechaFinPeriodoIncidencia.Text, txtDescripcionPeriodoIncidencia.Text.Trim(), iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
                dgvPeriodosProcesoIncidencias.DataSource = null;
                dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtDescripcionPeriodoIncidencia.Text = "";
                txtDescripcionPeriodoIncidencia.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgPeriodosProcesoIncidencia(6, iIdFormaPago, "", "", "", iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActPeriodoIncidencia.Visible = false;
            }
            else if (iActbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //inserta registro nuevo
                    fuidPeriodosProcesoIncidencia(3, iIdFormaPago, dtpFechaInicioPeriodoIncidencia.Text, dtpFechaFinPeriodoIncidencia.Text, txtDescripcionPeriodoIncidencia.Text.Trim(), iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
                    dgvPeriodosProcesoIncidencias.DataSource = null;
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txtDescripcionPeriodoIncidencia.Text = "";
                    txtDescripcionPeriodoIncidencia.Focus();
                    timer1.Start();
                    //llena grid con datos existente
                    fgPeriodosProcesoIncidencia(6, iIdFormaPago, "", "", "", iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlActPeriodoIncidencia.Visible = false;
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
        private void PeriodosProcesoIncidencia_Load(object sender, EventArgs e)
        {
            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //LLAMA TOOL TIP
            fTooltip();

            //
            // Los valores deben venir de los permisos con los que cuente el PERFIL de USUARIO
            iIns = 1; // Permite Insertar
            iAct = 1; // Permite Actualizar
            iElim = 1; // Permite Eliminar

            //HABILITA BOTON AGREGAR
            if (iIns == 1)
            {
                btnAgregar.Visible = true;
            }

        } // private void PeriodosProcesoIncidencia_Load(object sender, EventArgs e)

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lblActPeriodoIncidencia.Text = "     Elimina Período Proceso Incidencia";
                iActbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lblActPeriodoIncidencia.Text = "     Modifica Período Proceso Incidencia";
                iActbtn = 2;
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
        private void fTooltip()
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

        } // private void fTooltip()

        private void fgPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioPeriodoIncidencia, string sFechaFinPeriodoIncidencia, string sDescripcion, int iStPeriodoProceso, string sUsuumod, string sPrgumod)
        {

            if (iIns == 1 && iAct == 1 && iElim == 1)
            {

                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if(dgvPeriodosProcesoIncidencias.Columns.Count > 6)
                {
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                }
                dgvPeriodosProcesoIncidencias.Columns.Insert(0, imgCheckUsuarios);
                dgvPeriodosProcesoIncidencias.Columns[0].HeaderText = "Selección";
                dgvPeriodosProcesoIncidencias.Columns[0].Width = 75;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
            else if (iIns == 1 && iAct == 1)
            {
                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPeriodosProcesoIncidencias.Columns.Count > 6)
                {
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                }
                dgvPeriodosProcesoIncidencias.Columns.Insert(0, imgCheckUsuarios);
                dgvPeriodosProcesoIncidencias.Columns[0].HeaderText = "Selección";
                dgvPeriodosProcesoIncidencias.Columns[0].Width = 75;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();

            }
            else if (iIns == 1 && iElim == 1)
            {
                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPeriodosProcesoIncidencias.Columns.Count > 6)
                {
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                }
                dgvPeriodosProcesoIncidencias.Columns.Insert(0, imgCheckUsuarios);
                dgvPeriodosProcesoIncidencias.Columns[0].HeaderText = "Selección";
                dgvPeriodosProcesoIncidencias.Columns[0].Width = 75;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
            else if (iAct == 1 && iElim == 1)
            {
                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPeriodosProcesoIncidencias.Columns.Count > 6)
                {
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                }
                dgvPeriodosProcesoIncidencias.Columns.Insert(0, imgCheckUsuarios);
                dgvPeriodosProcesoIncidencias.Columns[0].HeaderText = "Selección";
                dgvPeriodosProcesoIncidencias.Columns[0].Width = 75;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
            else if (iIns == 1)
            {
                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
            else if (iAct == 1)
            {
                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPeriodosProcesoIncidencias.Columns.Count > 6)
                {
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                }
                dgvPeriodosProcesoIncidencias.Columns.Insert(0, imgCheckUsuarios);
                dgvPeriodosProcesoIncidencias.Columns[0].HeaderText = "Selección";
                dgvPeriodosProcesoIncidencias.Columns[0].Width = 75;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
            else if (iElim == 1)
            {
                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                if (dgvPeriodosProcesoIncidencias.Columns.Count > 6)
                {
                    dgvPeriodosProcesoIncidencias.Columns.RemoveAt(0);
                }
                dgvPeriodosProcesoIncidencias.Columns.Insert(0, imgCheckUsuarios);
                dgvPeriodosProcesoIncidencias.Columns[0].HeaderText = "Selección";
                dgvPeriodosProcesoIncidencias.Columns[0].Width = 75;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
            else
            {

                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                dgvPeriodosProcesoIncidencias.DataSource = dtPeriodosProcesoIncidencias;

                dgvPeriodosProcesoIncidencias.Columns[1].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[2].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[3].Width = 100;
                dgvPeriodosProcesoIncidencias.Columns[4].Width = 130;
                dgvPeriodosProcesoIncidencias.Columns[5].Width = 60;
                dgvPeriodosProcesoIncidencias.Columns[5].Visible = false;
                dgvPeriodosProcesoIncidencias.Columns[6].Width = 80;
                dgvPeriodosProcesoIncidencias.ClearSelection();
            }
        }

        private void fStatusPeriodoIncidencia(int iOpcion, string sTablaStatus, int iStValor, string sDescripcion, int iStStatus, string sUsuumod, string sPrgumod)
        {
            DataTable dtStatusPeriodoIncidencia = oStatus.statue_S(iOpcion, sTablaStatus, iStValor, sDescripcion, iStStatus, sUsuumod, sPrgumod);
            cbStatusPeriodo.DataSource = dtStatusPeriodoIncidencia;
            cbStatusPeriodo.DisplayMember = "Descripción";
            cbStatusPeriodo.ValueMember = "Val Status";
        }
        private void fuidPeriodosProcesoIncidencia(int iOpcion, int iIdFormaPago, string sFechaInicioPeriodoIncidencia, string sFechaFinPeriodoIncidencia, string sDescripcion, int iStPeriodoIncidencia, string sUsuumod, string sPrgumod)
        {
            //agrega registro
            if (iActbtn == 1)
            {
                iResp = oPeriodoProcesoIncidencia.udiPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                //lbMensaje.Text = iResp.ToString();
                txtDescripcionPeriodoIncidencia.Text = "";
            }
            //actualiza registro
            else if (iActbtn == 2)
            {
                iResp = oPeriodoProcesoIncidencia.udiPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                //lbMensaje.Text = iResp.ToString();
                txtDescripcionPeriodoIncidencia.Text = "";
            }
            //elimina registro
            else if (iActbtn == 3)
            {
                iResp = oPeriodoProcesoIncidencia.udiPeriodosProcesoIncidencia(iOpcion, iIdFormaPago, sFechaInicioPeriodoIncidencia, sFechaFinPeriodoIncidencia, sDescripcion, iStPeriodoIncidencia, sUsuumod, sPrgumod);
                //lbMensaje.Text = iResp.ToString();
                txtDescripcionPeriodoIncidencia.Text = "";
            } // 

            switch (iResp.ToString())
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
            } // switch (iResp.ToString())
        } // 


        private void factgrid()
        {
            string sStPeriodoIncidencia = "";
            for (int iContador = 0; iContador < dgvPeriodosProcesoIncidencias.Rows.Count; iContador++)
            {
                dgvPeriodosProcesoIncidencias.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvPeriodosProcesoIncidencias.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvPeriodosProcesoIncidencias.SelectedRows[0];

                sFechaInicioPeriodoIncidencia = row.Cells["FechaInicio"].Value.ToString();
                sFechaFinPeriodoIncidencia = row.Cells["FechaFin"].Value.ToString();
                sDescripcion = row.Cells["Descripción"].Value.ToString();
                //cbStatusPeriodo.Items.Clear();
                //cbStatusPeriodo.SelectedText = "";

                fStatusPeriodoIncidencia(4, "rechtperiodopro", 0, "", 0, "bhb", "PeriodosProcesoIncidencia");

                switch (row.Cells["Estatus"].Value.ToString())
                {
                    case "2":
                        sStPeriodoIncidencia = "Procesar";
                        break;
                    case "1":
                        sStPeriodoIncidencia = "Abierto";
                        break;
                    case "0":
                        sStPeriodoIncidencia = "Cerrado";
                        break;
                } // switch (row.Cells["Estatus"].Value.ToString())

                pnlActPeriodoIncidencia.Visible = true;
                lblActPeriodoIncidencia.Text = "     Modifica Período Proceso Incidencia";
                dtpFechaInicioPeriodoIncidencia.Text = sFechaInicioPeriodoIncidencia;
                dtpFechaFinPeriodoIncidencia.Text = sFechaFinPeriodoIncidencia;
                dtpFechaInicioPeriodoIncidencia.Focus();
                txtDescripcionPeriodoIncidencia.Text = sDescripcion;
//                cbStatusPeriodo.SelectedText = sStPeriodoIncidencia;
                cbStatusPeriodo.Text = sStPeriodoIncidencia;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    } // public partial class PeriodosProcesoIncidencia : Form
}
