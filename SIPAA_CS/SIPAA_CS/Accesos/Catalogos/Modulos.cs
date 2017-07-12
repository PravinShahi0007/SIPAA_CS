using SIPAA_CS.App_Code;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;
using static SIPAA_CS.App_Code.Utilerias;

namespace SIPAA_CS.Accesos.Catalogos
{
    public partial class Modulos : Form
    {
        public int variable;
        public string idmodulo;
        public string cvindmodulo;
        public string cvmodulo;
        public string cvtipomodulo;
        public string descripcion;
        public string cvmodpad;
        public string orden;
        public string ambiente;
        public string modulo;
        public string usuumod;
        public string prgmod;
        public string stmodulo;
        public int response;
        public string ruta;
        public string padre;
        public string padresel;
        public string cvtipo;
        public string cvmodulos;

        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
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
        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                variable = 3;
                if (stmodulo == "Inactivo")
                {
                    lblAccion.Text = "      Alta Módulo";
                    Utilerias.AsignarBotonResize(btnGuardar,Utilerias.PantallaSistema(),Botones.Alta);
                }
                else if (stmodulo == "Activo")
                {
                    lblAccion.Text = "      Baja Módulo";
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(),Botones.Baja);
                }
            }
            else
            {
                variable = 0;
                lblAccion.Text = "      Editar Módulo";
                Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Editar);
            }
        }
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
                DataGridViewRow row = this.dgvModulos.SelectedRows[0];

                idmodulo =  row.Cells["idmodulo"].Value.ToString();
                cvmodulo = row.Cells["cvmodulo"].Value.ToString();
                descripcion = row.Cells["descripcion"].Value.ToString();
                cvindmodulo = row.Cells["cvtipomodulo1"].Value.ToString();
                ambiente = row.Cells["ambiente"].Value.ToString();
                cvmodulos = row.Cells["cvindmodulo"].Value.ToString();
                if (cvmodulos != "")
                {
                    cbTipoMod.Text = "Submódulo";
                    pnlModulo.Visible = true;
                    
                }
                else
                {
                    cbTipoMod.Text = "Módulo";
                    pnlModulo.Visible = false;
                }
                try
                {
                    orden = row.Cells["orden"].Value.ToString();
                }
                catch (Exception)
                {
                    orden = "0";
                }
                
                ruta = row.Cells["rutaaaceso"].Value.ToString();
                stmodulo = row.Cells["Estatus"].Value.ToString();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                if (Permisos.dcPermisos["Eliminar"] == 1 && Permisos.dcPermisos["Actualizar"] == 1)
                {
                    variable = 2;
                    lblAccion.Text = "      Editar Módulo";
                    PanelEditar.Visible = true;
                    ckbEliminar.Visible = true;
                   
                    txtCvModulo.Text = cvmodulo;
                    txtDescripcion.Text = descripcion;
                    txtRuta.Text = ruta;
                    txtOrden.Text = Convert.ToString(orden);
                    cbAmbiente.Text = ambiente;
                    cbModuloTipo.Text = cvindmodulo;
                    cbModulos.Text = cvmodulos;

                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Editar");

                    ckbEliminar.Checked = false;

                    if (stmodulo == "Inactivo")
                    {
                        ckbEliminar.Text = "Alta";
                    }
                    else if (stmodulo == "Activo")
                    {
                        ckbEliminar.Text = "Baja";
                    }
                }
                else if (Permisos.dcPermisos["Actualizar"] == 1)
                {
                    variable = 2;
                    lblAccion.Text = "      Editar Módulo";
                    PanelEditar.Visible = true;
                    txtCvModulo.Enabled = false;

                    txtCvModulo.Text = cvmodulo;
                    txtDescripcion.Text = descripcion;
                    txtRuta.Text = ruta;
                    txtOrden.Text = Convert.ToString(orden);
                    cbAmbiente.Text = ambiente;
                    cbModuloTipo.Text = cvindmodulo;
                    cbModulos.Text = cvmodulos;

                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Editar");
                }
                else if (Permisos.dcPermisos["Eliminar"] == 1)
                {
                    variable = 3;
                    PanelEditar.Visible = true;

                    txtCvModulo.Text = cvmodulo;
                    txtDescripcion.Text = descripcion;
                    txtRuta.Text = ruta;
                    txtOrden.Text = Convert.ToString(orden);
                    cbAmbiente.Text = ambiente;
                    cbModuloTipo.Text = cvindmodulo;
                    cbModulos.Text = cvmodulos;

                    txtCvModulo.Enabled = true;

                    if (stmodulo == "Inactivo")
                    {
                        lblAccion.Text = "      Alta Módulo";
                        Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Alta);
                    }
                    else if (stmodulo == "Activo")
                    {
                        lblAccion.Text = "      Baja Módulo";
                        Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Baja);
                    }

                }

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnRegresa_Click(object sender, EventArgs e)
        {
            this.Close();
        }
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
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            variable = 1;
            lblAccion.Text = "      Agregar Módulo";
            txtCvModulo.Enabled = true;

            cbTipoMod.Text = "Selecciona un Tipo de Módulo";
            cbAmbiente.Text = "Selecciona un Ambiente";
            cbModuloTipo.Text = "Selecciona una Categoría";
            cbModulos.Text = "Selecciona un Módulo";
            txtCvModulo.Text = "";
            txtDescripcion.Text = "";
            txtOrden.Text = "";
            txtRuta.Text = "";

            pnlModulo.Visible = false;
            PanelEditar.Visible = true;
            ckbEliminar.Visible = false;
            Utilerias.AsignarBotonResize(btnGuardar,Utilerias.PantallaSistema(),"Guardar");
            txtCvModulo.Focus();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            modulo = txtBuscarModulo.Text;
            LlenarGridModulos("", "", "", modulo.Trim(), "", "", "", "", "", "", "", 5, dgvModulos);
            txtBuscarModulo.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //agregar
            if (variable == 1)
            {
                

                if (cbTipoMod.SelectedItem.ToString() == "Módulo")
                {
                    if (cbTipoMod.SelectedIndex != -1 && cbAmbiente.SelectedIndex != -1 && cbModuloTipo.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "")
                    {
                        if (utilerias.IsNumber(txtOrden.Text))
                        {
                            //if (txtOrden.Text != "")
                            //{
                                //if (cbTipoMod.SelectedItem.ToString() == "Módulo")
                                //{
                                    //MessageBox.Show("modulo seleccionado");

                                    ambiente = cbAmbiente.SelectedItem.ToString();
                                    cvtipomodulo = cbModuloTipo.SelectedValue.ToString();
                                    cvmodulo = txtCvModulo.Text;
                                    descripcion = txtDescripcion.Text;
                                    orden = txtOrden.Text;
                                    ruta = txtRuta.Text;

                                    usuumod = LoginInfo.IdTrab;
                                    prgmod = this.Name;
                                    
                                    response = objModulo.CrearModulo("", "", cvmodulo.Trim(), descripcion.Trim(), orden, ambiente, cvtipomodulo, ruta, "1", usuumod, prgmod, 1);
                                  
                                    txtCvModulo.Text = "";
                                    txtDescripcion.Text = "";
                                    txtOrden.Text = "";
                                    txtRuta.Text = "";
                                    cbTipoMod.Text = "Selecciona un Tipo de Módulo";
                                    cbAmbiente.Text = "Selecciona un Ambiente";
                                    cbModuloTipo.Text = "Selecciona una Categoría";
                                    PanelEditar.Visible = false;
                                    Modulos_Load(sender, e);

                                    if (response == 1)
                                    {
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se creo correctamente");
                                        timer1.Start();
                                    }
                                    else if (response == 0)
                                    {
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El registro ya existe");
                                        timer1.Start();
                                    }

                                //}
                                //else if (cbTipoMod.SelectedItem.ToString() == "Submódulo")
                                //{
                                //    MessageBox.Show("submodulo seleccionado");
                                //}

                            //}
                            //else
                            //{
                            //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
                            //    timer1.Start();
                            //}
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
                else if (cbTipoMod.SelectedItem.ToString() == "Submódulo")
                {

                    if (cbTipoMod.SelectedIndex != -1 && cbAmbiente.SelectedIndex != -1 && cbModuloTipo.SelectedIndex != -1 && cbModulos.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "" )
                    {
                        //if (utilerias.IsNumber(txtOrden.Text))
                        //{
                        //    if (txtOrden.Text != "")
                        //    {
                                
                                    //MessageBox.Show("modulo seleccionado");

                                    ambiente = cbAmbiente.SelectedItem.ToString();
                                    cvtipomodulo = cbModuloTipo.SelectedValue.ToString();
                                    cvmodulos = cbModulos.SelectedValue.ToString();
                                    cvmodulo = txtCvModulo.Text;
                                    descripcion = txtDescripcion.Text;
                                    orden = txtOrden.Text;
                                    ruta = txtRuta.Text;

                                    usuumod = LoginInfo.IdTrab;
                                    prgmod = this.Name;
                                
                                    response = objModulo.CrearModulo("", cvmodulos.Trim(), cvmodulo.Trim(), descripcion.Trim(), orden, ambiente, cvtipomodulo, ruta, "1", usuumod, prgmod, 1);
                                
                                    txtCvModulo.Text = "";
                                    txtDescripcion.Text = "";
                                    txtOrden.Text = "";
                                    txtRuta.Text = "";
                                    cbTipoMod.Text = "Selecciona un Tipo de Módulo";
                                    cbAmbiente.Text = "Selecciona un Ambiente";
                                    cbModuloTipo.Text = "Selecciona una Categoría";
                                    cbModulos.Text = "Selecciona un Módulo";
                                    PanelEditar.Visible = false;
                                    Modulos_Load(sender, e);

                                    if (response == 1)
                                    {
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se creo correctamente");
                                        timer1.Start();
                                    }
                                    else if (response == 0)
                                    {
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El registro ya existe");
                                        timer1.Start();
                                    }
                                    
                        //    }
                        //    else
                        //    {
                        //        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
                        //        timer1.Start();
                        //    }
                        //}
                        //else
                        //{
                        //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El campo Orden debe ser un número");
                        //    timer1.Start();
                        //    txtOrden.Focus();
                        //}
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa valores");
                        timer1.Start();
                        txtCvModulo.Focus();
                    }

                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Tipo de Módulo");
                    timer1.Start();
                }
                
            }
            //update
            if (variable == 2)
            {

                if (cbTipoMod.SelectedItem.ToString() == "Módulo")
                {
                    if (cbTipoMod.SelectedIndex != -1 && cbAmbiente.SelectedIndex != -1 && cbModuloTipo.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "")
                    {
                        if (utilerias.IsNumber(txtOrden.Text))
                        {
                            //if (txtOrden.Text != "")
                            //{
                            //if (cbTipoMod.SelectedItem.ToString() == "Módulo")
                            //{
                            //MessageBox.Show("modulo seleccionado");

                            ambiente = cbAmbiente.SelectedItem.ToString();
                            cvtipomodulo = cbModuloTipo.SelectedValue.ToString();
                            cvmodulo = txtCvModulo.Text;
                            descripcion = txtDescripcion.Text;
                            orden = txtOrden.Text;
                            ruta = txtRuta.Text;

                            usuumod = LoginInfo.IdTrab;
                            prgmod = this.Name;

                            response = objModulo.CrearModulo(idmodulo, "", cvmodulo.Trim(), descripcion.Trim(), orden, ambiente, cvtipomodulo, ruta, "1", usuumod, prgmod, 2);

                            txtCvModulo.Text = "";
                            txtDescripcion.Text = "";
                            txtOrden.Text = "";
                            txtRuta.Text = "";
                            cbTipoMod.Text = "Selecciona un Tipo de Módulo";
                            cbAmbiente.Text = "Selecciona un Ambiente";
                            cbModuloTipo.Text = "Selecciona una Categoría";
                            PanelEditar.Visible = false;
                            Modulos_Load(sender, e);

                            if (response == 1)
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se creo correctamente");
                                timer1.Start();
                            }
                            else if (response == 0)
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El registro ya existe");
                                timer1.Start();
                            }

                            //}
                            //else if (cbTipoMod.SelectedItem.ToString() == "Submódulo")
                            //{
                            //    MessageBox.Show("submodulo seleccionado");
                            //}

                            //}
                            //else
                            //{
                            //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
                            //    timer1.Start();
                            //}
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
                //if (cbAmbiente.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "")
                //{

                //    if (utilerias.IsNumber(txtOrden.Text))
                //    {
                //        if (txtOrden.Text != "")
                //        {
                //            //padre = Convert.ToInt32(cbPadre.SelectedValue.ToString());
                //            ambiente = cbAmbiente.SelectedItem.ToString();
                //            //cvtipomodulo = Convert.ToInt32(cbTipoModulo.SelectedValue.ToString());
                //            cvmodulo = txtCvModulo.Text;
                //            descripcion = txtDescripcion.Text;
                //            orden = txtOrden.Text;
                //            ruta = txtRuta.Text;

                //            usuumod = LoginInfo.IdTrab;
                //            //usuumod = "ADMIN";
                //            prgmod = this.Name;



                //            if (padresel != "999999999")
                //            {
                //                response = objModulo.CrearModulo(idmodulo,padre, cvmodulo, descripcion, orden, ambiente,cvtipomodulo,ruta,"",usuumod,prgmod,2);
                //            }
                //            else
                //            {
                //                response = objModulo.CrearModulo(idmodulo, "", cvmodulo, descripcion, orden, ambiente, cvtipomodulo, ruta, "", usuumod, prgmod, 2);
                //            }

                //            Modulos_Load(sender, e);

                //            txtCvModulo.Text = "";
                //            txtDescripcion.Text = "";
                //            txtOrden.Text = "";
                //            txtRuta.Text = "";
                //            //cbPadre.Text = "Selecciona un Padre";
                //            cbAmbiente.Text = "Selecciona un Ambiente";
                //            //cbTipoModulo.Text = "Selecciona un Tipo Módulo";

                //            PanelEditar.Visible = false;
                //            if (response == 1)
                //            {
                //                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se actualizo correctamente");
                //                timer1.Start();
                //            }
                //            else
                //            {
                //                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Intentalo mas tarde");
                //                timer1.Start();
                //            }
                //        }
                //        else
                //        {
                //            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
                //            timer1.Start();
                //        }

                //    }
                //    else
                //    {
                //        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El campo orden debe ser un número");
                //        timer1.Start();
                //        txtOrden.Focus();
                //    }

                //}
                else if (cbTipoMod.SelectedItem.ToString() == "Submódulo")
                {

                    if (cbTipoMod.SelectedIndex != -1 && cbAmbiente.SelectedIndex != -1 && cbModuloTipo.SelectedIndex != -1 && cbModulos.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "")
                    {
                        //if (utilerias.IsNumber(txtOrden.Text))
                        //{
                        //    if (txtOrden.Text != "")
                        //    {

                        //MessageBox.Show("modulo seleccionado");

                        ambiente = cbAmbiente.SelectedItem.ToString();
                        cvtipomodulo = cbModuloTipo.SelectedValue.ToString();
                        cvmodulos = cbModulos.SelectedValue.ToString();
                        cvmodulo = txtCvModulo.Text;
                        descripcion = txtDescripcion.Text;
                        orden = txtOrden.Text;
                        ruta = txtRuta.Text;

                        usuumod = LoginInfo.IdTrab;
                        prgmod = this.Name;

                        response = objModulo.CrearModulo(idmodulo, cvmodulos.Trim(), cvmodulo.Trim(), descripcion.Trim(), orden, ambiente, cvtipomodulo, ruta, "1", usuumod, prgmod, 2);

                        txtCvModulo.Text = "";
                        txtDescripcion.Text = "";
                        txtOrden.Text = "";
                        txtRuta.Text = "";
                        cbTipoMod.Text = "Selecciona un Tipo de Módulo";
                        cbAmbiente.Text = "Selecciona un Ambiente";
                        cbModuloTipo.Text = "Selecciona una Categoría";
                        cbModulos.Text = "Selecciona un Módulo";
                        PanelEditar.Visible = false;
                        Modulos_Load(sender, e);

                        if (response == 1)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se creo correctamente");
                            timer1.Start();
                        }
                        else if (response == 0)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El registro ya existe");
                            timer1.Start();
                        }

                        //    }
                        //    else
                        //    {
                        //        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
                        //        timer1.Start();
                        //    }
                        //}
                        //else
                        //{
                        //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El campo Orden debe ser un número");
                        //    timer1.Start();
                        //    txtOrden.Focus();
                        //}
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa valores");
                        timer1.Start();
                        txtCvModulo.Focus();
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
                    usuumod = LoginInfo.IdTrab;
                    response = objModulo.CrearModulo(idmodulo,"","","","","","","","",usuumod,"",3);
                    PanelEditar.Visible = false;

                    if (response == 1)
                    {
                        Modulos_Load(sender, e);
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "El Módulo esta Activado");
                        timer1.Start();
                    }
                    else if (response == 0)
                    {
                        Modulos_Load(sender, e);
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "El Módulo esta Inactivo");
                        timer1.Start();
                    }

                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un modulo");
                    timer1.Start();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        private void cbTipoMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cbtipomodulo = cbTipoMod.SelectedIndex;

            if (cbtipomodulo == 1)
            {
                pnlModulo.Visible = true;
            }
            else if (cbtipomodulo == 0)
            {
                pnlModulo.Visible = false;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Modulos_Load(object sender, EventArgs e)
        {
            //Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            pnlModulo.Visible = false;
            LlenarGridModulos("%","", "", "", "", "", "", "", "", "", "", 4,dgvModulos);
            
            DataTable dtModulo1 = objModulo.ObtenerModulo("1", "", "", "", "", "", "", "", "", "", "", 7);
            llenaCombo(cbModuloTipo, dtModulo1, "cvtipomodulo", "descripcion");

            DataTable dtModulo = objModulo.ObtenerModulo("1", "", "", "", "", "", "", "", "", "", "", 4);
            llenaComboModulo(cbModulos, dtModulo, "idmodulo", "descripcion");

            if (Permisos.dcPermisos["Crear"] == 0)
            {
                btnAgregar.Visible = false;
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
        public void LlenarGridModulos(string idmodulo, string cvindmodulo, string cvmodulo, string descripcion, string orden, string ambiente, string cvtipomodulo, string rutaaaceso, string stmodulo, string usumod, string prgumod, int opcion, DataGridView dgvModulo)
        {

            if (dgvModulo.Columns.Count > 1)
            {
                dgvModulo.Columns.RemoveAt(0);
            }
            Modulo objModulo = new Modulo();
            DataTable dtModulo = objModulo.ObtenerModulo(idmodulo, cvindmodulo, cvmodulo,descripcion, orden, ambiente, cvtipomodulo, rutaaaceso, stmodulo, usumod, prgumod, opcion);

            dgvModulo.DataSource = dtModulo;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvModulo.Columns.Insert(0, imgCheckProcesos);
            dgvModulo.Columns[3].HeaderText = "Descripción";
            dgvModulo.Columns[4].HeaderText = "Ambiente";
            dgvModulo.Columns[5].HeaderText = "Tipo Módulo";
            dgvModulos.Columns[1].Visible = false;
            dgvModulos.Columns[2].Visible = false;
            dgvModulos.Columns[5].Visible = false;
            dgvModulos.Columns[6].Visible = false;
            dgvModulos.Columns[7].Visible = false;
            dgvModulos.Columns[8].Visible = false;





            dgvModulo.ClearSelection();
        }
       
        public static void llenaComboModulo(ComboBox cb, DataTable dt, string sClave, string sDescripcion)
        {
            DataRow row = dt.NewRow();
            row[sClave] = "0";
            row[sDescripcion] = "Selecciona un Módulo";
            dt.Rows.InsertAt(row, 0);
            cb.DataSource = dt;
            cb.DisplayMember = sDescripcion;
            cb.ValueMember = sClave;
        }

        public static void llenaCombo(ComboBox cb, DataTable dt, string sClave, string sDescripcion)
        {
            DataRow row = dt.NewRow();
            row[sClave] = "0";
            row[sDescripcion] = "Selecciona una Opción";
            dt.Rows.InsertAt(row, 0);
            cb.DataSource = dt;
            cb.DisplayMember = sDescripcion;
            cb.ValueMember = sClave;
        }
        
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
