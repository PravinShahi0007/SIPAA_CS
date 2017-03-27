using SIPAA_CS.App_Code;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace SIPAA_CS.Accesos
{
    public partial class Modulos : Form
    {
        public int variable;
        public string cvmodulo;
        public string descripcion;
        public string cvmodpad;
        public int orden;
        public string ambiente;
        public string modulo;
        public string usuumod;
        public string prgmod;
        public int stmodulo;
        public int response;

        Utilerias utilerias = new Utilerias();
        Modulo objModulo = new Modulo();
        
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: Formulario de crear Modulo
        //***********************************************************************************************
        public Modulos()
        {
            InitializeComponent();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvModulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvModulos.Rows.Count; iContador++)
            {
                dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvModulos.SelectedRows.Count != 0)
            {
                variable = 2;
                txtCvModulo.Enabled = false;
                ckbEliminar.Visible = true;
                PanelEditar.Visible = true;
                DataGridViewRow row = this.dgvModulos.SelectedRows[0];
               
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                cvmodulo = row.Cells["cvmodulo"].Value.ToString();
                descripcion = row.Cells["descripcion"].Value.ToString();
                cvmodpad = row.Cells["cvmodpad"].Value.ToString();
                orden = Convert.ToInt32(row.Cells["orden"].Value.ToString());
                ambiente = row.Cells["ambiente"].Value.ToString();
                modulo = row.Cells["modulo"].Value.ToString();
                stmodulo = Convert.ToInt32(row.Cells["stmodulo"].Value.ToString());

                txtCvModulo.Text = cvmodulo;
                txtDescripcion.Text = descripcion;
                txtModPad.Text = cvmodpad;
                txtOrden.Text = Convert.ToString(orden);
                cbAmbiente.SelectedItem = ambiente;
                cbModulo.SelectedItem = modulo;
                //AsignarPlantel();

                utilerias.ChangeButton(btnGuardar, 2, false);

                ckbEliminar.Checked = false;

                if (stmodulo == 0)
                {
                    ckbEliminar.Text = "Alta";

                }
                else if (stmodulo == 1)
                {
                    ckbEliminar.Text = "Baja";

                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            variable = 1;
            txtCvModulo.Enabled = true;
            txtCvModulo.Text = "";
            txtDescripcion.Text = "";
            txtModPad.Text = "";
            txtOrden.Text = "";
            cbModulo.Text = "Selecciona un Módulo";
            cbAmbiente.Text = "Selecciona un Ambiente";
            PanelEditar.Visible = true;
            utilerias.ChangeButton(btnGuardar,1,false);
            txtCvModulo.Focus();
        }


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Modulos_Load(object sender, EventArgs e)
        {
            LlenarGridModulos("", "", "", 0, "", "", "", 0, "", "", 4,dgvModulos);
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        public void LlenarGridModulos(string cvmodulo, string descripcion, string cvmodpad, int orden, string ambiente, string modulo, string rutaaaceso, int stmodulo, string usumod, string prgumod, int opcion, DataGridView dgvModulo)
        {

            if (dgvModulo.Columns.Count > 1)
            {
                dgvModulo.Columns.RemoveAt(0);
            }
            Modulo objModulo = new Modulo();
            DataTable dtModulo = objModulo.ObtenerModulo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, rutaaaceso, stmodulo, usumod, prgumod, opcion);

            dgvModulo.DataSource = dtModulo;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvModulo.Columns.Insert(0, imgCheckProcesos);
            dgvModulo.Columns[0].HeaderText = "Seleccionar";
            dgvModulos.Columns[1].Visible = false;
            //dgvModulos.Columns[2].Visible = false;
            dgvModulos.Columns[3].Visible = false;
            dgvModulos.Columns[4].Visible = false;
            dgvModulos.Columns[5].Visible = false;
            dgvModulos.Columns[6].Visible = false;
            dgvModulos.Columns[7].Visible = false;
            //dgvModulos.Columns[8].Visible = false;
            dgvModulos.Columns[9].Visible = false;
            dgvModulos.Columns[10].Visible = false;

            dgvModulo.ClearSelection();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            modulo = txtBuscarModulo.Text;

            LlenarGridModulos("", modulo.Trim(), "", 0, "", "", "", 0, "", "", 5, dgvModulos);

            txtBuscarModulo.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //agregar
            if (variable == 1)
            {
                
                if (txtCvModulo.Text != "" && txtDescripcion.Text != "" && txtModPad.Text != "" && txtOrden.Text !="" && cbAmbiente.SelectedIndex == 0 && cbModulo.SelectedIndex == 0)
                {
                   
                    if (utilerias.IsNumber(txtOrden.Text))
                    {
                        cvmodulo = txtCvModulo.Text;
                        descripcion = txtDescripcion.Text;
                        cvmodpad = txtModPad.Text;
                        orden = Convert.ToInt32(txtOrden.Text);
                        ambiente = cbAmbiente.SelectedItem.ToString();
                        modulo = cbModulo.SelectedItem.ToString();
                        usuumod = "140414";
                        prgmod = this.Name;


                        response = objModulo.CrearModulo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, "", 1, usuumod, prgmod,2);

                        Modulos_Load(sender, e);
                        if (response == 1)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se creo correctamente");
                            timer1.Start();
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Intentalo mas tarde");
                            timer1.Start();
                        }
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El campo Orden debe ser un número");
                        timer1.Start();
                        txtOrden.Focus();
                    }
                    
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa valores");
                    timer1.Start();
                    txtCvModulo.Focus();
                }
            }
            //update
            if (variable == 2)
            {
                
                if (txtCvModulo.Text != "" && txtDescripcion.Text != "" && txtModPad.Text != "" && txtOrden.Text != "" )
                {

                    if (utilerias.IsNumber(txtOrden.Text))
                    {
                        cvmodulo = txtCvModulo.Text;
                        descripcion = txtDescripcion.Text;
                        cvmodpad = txtModPad.Text;
                        orden = Convert.ToInt32(txtOrden.Text);
                        ambiente = cbAmbiente.SelectedItem.ToString();
                        modulo = cbModulo.SelectedItem.ToString();
                        usuumod = "140414";
                        prgmod = this.Name;


                        response = objModulo.CrearModulo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, "", 0, usuumod, prgmod, 2);
                        Modulos_Load(sender, e);
                        if (response == 1)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se actualizo correctamente");
                            timer1.Start();
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Intentalo mas tarde");
                            timer1.Start();
                        }
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El campo orden debe ser un número");
                        timer1.Start();
                        txtOrden.Focus();
                    }

                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa valores");
                    timer1.Start();
                    txtDescripcion.Focus();
                }
            }

            //cambio de status
            if (variable == 3)
            {
                
                if (dgvModulos.SelectedRows.Count != 0)
                {
                    ckbEliminar.Checked = false;
                    response = objModulo.CrearModulo(cvmodulo, "", "", 0, "", "", "", 0, "", "", 3);
                    Modulos_Load(sender, e);
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un modulo");
                    timer1.Start();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                //btnGuardar.Image = Resources.Alta;
                variable = 3;
                if (stmodulo == 0)
                {

                    btnGuardar.Image = Resources.Alta;
                    //Utilerias.AsignarBotonResize(btnGuardar,);
                }
                else if (stmodulo == 1)
                {

                    btnGuardar.Image = Resources.Borrar;
                }
                
            }
            else
            {
                variable = 0;
                btnGuardar.Image = Resources.Editar;


            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
