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
        public int idmodulo;
        public int cvindmodulo;
        public string cvmodulo;
        public int cvtipomodulo;
        public string descripcion;
        public string cvmodpad;
        public int orden;
        public string ambiente;
        public string modulo;
        public string usuumod;
        public string prgmod;
        public string stmodulo;
        public int response;
        public string ruta;
        public int padre;
        public string padresel;
        public string cvtipo;

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
                idmodulo =  Convert.ToInt32(row.Cells["idmodulo"].Value.ToString());
                cvmodulo = row.Cells["cvmodulo"].Value.ToString();
                ambiente = row.Cells["ambiente"].Value.ToString();
                string cvtipomodulo = row.Cells["cvindmodulo"].Value.ToString();

                descripcion = row.Cells["descripcion"].Value.ToString();
                
                string cvtipo = row.Cells["cvtipomodulo1"].Value.ToString();
                try
                {
                    orden = Convert.ToInt32(row.Cells["orden"].Value.ToString());
                }
                catch (Exception)
                {

                    orden = 0;
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
                    cbPadre.Text= cvtipomodulo;
                    cbAmbiente.Text = ambiente;
                    cbTipoModulo.Text = cvtipo;
                //AsignarPlantel();

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
                    PanelEditar.Visible = true;
                    lblAccion.Text = "      Editar Módulo";
                    txtCvModulo.Enabled = false;

                    txtCvModulo.Text = cvmodulo;
                    txtDescripcion.Text = descripcion;
                    //txtModPad.Text = cvmodpad;
                    txtRuta.Text = ruta;
                    txtOrden.Text = Convert.ToString(orden);
                    cbAmbiente.SelectedItem = ambiente;
                    //cbModulo.SelectedItem = modulo;

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
                    cbAmbiente.SelectedItem = ambiente;

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
            txtCvModulo.Text = "";
            txtDescripcion.Text = "";
            //txtModPad.Text = "";
            txtOrden.Text = "";
            //cbModulo.Text = "Selecciona un Módulo";
            cbAmbiente.Text = "Selecciona un Ambiente";
            PanelEditar.Visible = true;
            ckbEliminar.Visible = false;
            Utilerias.AsignarBotonResize(btnGuardar,Utilerias.PantallaSistema(),"Guardar");
            txtCvModulo.Focus();
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
                
                if (cbPadre.SelectedIndex != 0 && cbAmbiente.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "")
                {
                    if (utilerias.IsNumber(txtOrden.Text))
                    {
                        if (txtOrden.Text != "" )
                        {
                            padre = Convert.ToInt32(cbPadre.SelectedValue.ToString());
                            ambiente = cbAmbiente.SelectedItem.ToString();
                            cvtipomodulo = Convert.ToInt32(cbTipoModulo.SelectedValue.ToString());
                            cvmodulo = txtCvModulo.Text;
                            descripcion = txtDescripcion.Text;
                            orden = Convert.ToInt32(txtOrden.Text);
                            ruta = txtRuta.Text;

                            //usuumod = LoginInfo.IdTrab;
                            usuumod = "ADMIN";
                            prgmod = this.Name;

                            //Convert.ToInt32(padre);

                            if (padresel != "999999999")
                            {
                                response = objModulo.CrearModulo(0, padre, cvmodulo.Trim(), descripcion.Trim(), orden, ambiente, cvtipomodulo, ruta, 1, usuumod, prgmod, 1);
                            }
                            else
                            {
                                response = objModulo.CrearModulo(0, 0, cvmodulo.Trim(), descripcion.Trim(), orden, ambiente, cvtipomodulo, ruta, 1, usuumod, prgmod, 1);
                            }


                            txtCvModulo.Text = "";
                            txtDescripcion.Text = "";
                            txtOrden.Text = "";
                            txtRuta.Text = "";
                            cbPadre.Text = "Selecciona un Padre";
                            cbAmbiente.Text = "Selecciona un Ambiente";
                            cbTipoModulo.Text = "Selecciona un Tipo Módulo";
                            //txtCvModulo.Focus();
                            PanelEditar.Visible = false;
                            Modulos_Load(sender, e);
                            if (response == 1)
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se creo correctamente");
                                timer1.Start();
                            }
                            if (response == 0)
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El registro ya existe");
                                timer1.Start();
                            }
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
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

                if (cbPadre.SelectedIndex != 0 && cbAmbiente.SelectedIndex != -1 && txtCvModulo.Text != "" && txtDescripcion.Text != "")
                {

                    if (utilerias.IsNumber(txtOrden.Text))
                    {
                        if (txtOrden.Text != "")
                        {
                            padre = Convert.ToInt32(cbPadre.SelectedValue.ToString());
                            ambiente = cbAmbiente.SelectedItem.ToString();
                            cvtipomodulo = Convert.ToInt32(cbTipoModulo.SelectedValue.ToString());
                            cvmodulo = txtCvModulo.Text;
                            descripcion = txtDescripcion.Text;
                            orden = Convert.ToInt32(txtOrden.Text);
                            ruta = txtRuta.Text;

                            //usuumod = LoginInfo.IdTrab;
                            usuumod = "ADMIN";
                            prgmod = this.Name;



                            if (padresel != "999999999")
                            {
                                response = objModulo.CrearModulo(idmodulo,padre, cvmodulo, descripcion, orden, ambiente,cvtipomodulo,ruta,0,usuumod,prgmod,2);
                            }
                            else
                            {
                                response = objModulo.CrearModulo(idmodulo, 0, cvmodulo, descripcion, orden, ambiente, cvtipomodulo, ruta, 0, usuumod, prgmod, 2);
                            }

                            Modulos_Load(sender, e);

                            txtCvModulo.Text = "";
                            txtDescripcion.Text = "";
                            txtOrden.Text = "";
                            txtRuta.Text = "";
                            cbPadre.Text = "Selecciona un Padre";
                            cbAmbiente.Text = "Selecciona un Ambiente";
                            cbTipoModulo.Text = "Selecciona un Tipo Módulo";
                            
                            PanelEditar.Visible = false;
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
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un orden");
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
                    usuumod = "ADMIN";
                    response = objModulo.CrearModulo(idmodulo,0,"","",0,"",0,"",0,usuumod,"",3);
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

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Modulos_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            //DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            //Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            LlenarGridModulos("", "", "", 0, "", "", "", 0, "", "", 4,dgvModulos);

           
            DataTable dtModulo = objModulo.ObtenerModulo(0, 0, "", "", 0, "", 0, "", 0, "", "", 6);
            llenaComboPadre(cbPadre, dtModulo, "idmodulo", "descripcion");

            DataTable dtModulo1 = objModulo.ObtenerModulo(0, 0, "", "", 0, "", 0, "", 0, "", "", 7);
            llenaCombo(cbTipoModulo, dtModulo1, "cvtipomodulo", "descripcion");

            //if (Permisos.dcPermisos["Crear"] == 0)
            //{
            //    btnAgregar.Visible = false;
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();

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
            DataTable dtModulo = objModulo.ObtenerModulo(idmodulo,cvindmodulo, cvmodulo,descripcion, orden, ambiente, cvtipomodulo, rutaaaceso, stmodulo, usumod, prgumod, opcion);

            dgvModulo.DataSource = dtModulo;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvModulo.Columns.Insert(0, imgCheckProcesos);
            dgvModulo.Columns[3].HeaderText = "Descripción";
            dgvModulo.Columns[4].HeaderText = "Ambiente";
            dgvModulos.Columns[1].Visible = false;
            dgvModulos.Columns[2].Visible = false;
            dgvModulos.Columns[3].Visible = true;
            dgvModulos.Columns[4].Visible = true;
            dgvModulos.Columns[5].Visible = false;
            dgvModulos.Columns[6].Visible = false;
            dgvModulos.Columns[7].Visible = false;
            dgvModulos.Columns[8].Visible = false;
            dgvModulos.Columns[9].Visible = true;
            dgvModulos.Columns[10].Visible = false;
            dgvModulos.Columns[11].Visible = false;


            dgvModulo.ClearSelection();
        }
        //public void llenaComboAbuelo()
        //{
        //    Modulo objModulo = new Modulo();
        //    DataTable dtModulo = objModulo.ObtenerModulo(0,0,"","",0,"",0,"",0,"","",6);


        //    List<string> ltModuloAbuelo = new List<string>();

        //    ltModuloAbuelo.Insert(0, "Selecciona un Abuelo");
        //    foreach (DataRow row in dtModulo.Rows)
        //    {
        //        ltModuloAbuelo.Add(row["descripcion"].ToString());
        //    }

        //    cbAbuelo.DataSource = ltModuloAbuelo;
        //}

        public static void llenaComboPadre(ComboBox cb, DataTable dt, string sClave, string sDescripcion)
        {
            DataRow row = dt.NewRow();
            row[sClave] = "0";
            row[sDescripcion] = "Selecciona un Módulo Padre";
            dt.Rows.InsertAt(row, 0);
            row = dt.NewRow();
            row[sClave] = "999999999";
            row[sDescripcion] = "Agregar nuevo";
            int index = dt.Rows.Count + 1;
            dt.Rows.InsertAt(row, index);
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

        private void cbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            padresel = cbPadre.SelectedValue.ToString();
            if (padresel == "999999999")
            {
                pnlTipoModulo.Visible = false;
            }
            else
            {
                pnlTipoModulo.Visible = true;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
