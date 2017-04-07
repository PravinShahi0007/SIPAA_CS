﻿using SIPAA_CS.App_Code;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;
using static SIPAA_CS.App_Code.Utilerias;

namespace SIPAA_CS.Accesos
{
    public partial class Usuarios : Form
    {

        public string cvusuario;
        public string CVusuario;
        public string nombre;
        public string passw;
        public string pass;
        public int idtrab;
        public int status;
        public string usumod;
        public string prgmod;
        public int response;
        public string buscar;
        public string stusuario;

        public int variable = 0;

        Utilerias utilerias = new Utilerias();
        Usuario usuario = new Usuario();


        public Usuarios()
        {
            InitializeComponent();
        }

        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: Formulario de Acceso Usuario
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvAccesoUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            for (int iContador = 0; iContador < dgvAccesoUsuario.Rows.Count; iContador++)
            {
                dgvAccesoUsuario.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvAccesoUsuario.SelectedRows.Count != 0)
            {
                label13.Text = "      Editar Usuario SIPAA";
                panel1.Visible = true;
                ckbElimina.Visible = true;
                ckbElimina.Checked = false;
                txtPassword.Enabled = false;
                Utilerias.AsignarBotonResize(btnSipaa,Utilerias.PantallaSistema(),"Editar");
                DataGridViewRow row = this.dgvAccesoUsuario.SelectedRows[0];

                cvusuario = row.Cells["cvusuario"].Value.ToString();
                nombre = row.Cells["nombre"].Value.ToString();
                stusuario = row.Cells["stusuario"].Value.ToString();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                txtNombreSipaa.Text = nombre;
                variable = 5;

                if (stusuario == "0")
                {
                    ckbElimina.Text = "Alta";

                }
                else if (stusuario == "1")
                {
                    ckbElimina.Text = "Baja";

                }

            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //int response;
            cvusuario = txtCvUsuario.Text;
            nombre = txtNombre.Text;
            passw = txtPassword.Text;
            usumod = "140014";


            if (cvusuario == "ADMIN")
            {
                string idtrab = txtCvUsuario.Text;
            }
            else
            {
                int idtrab = Convert.ToInt32(txtCvUsuario.Text);
            }


            prgmod = this.Name;

            pass = utilerias.cifradoMd5(passw);


            //AGREGAR
            if (variable == 1)
            {

               // MessageBox.Show("pasa var 1");
               
                if (cvusuario != String.Empty && nombre != String.Empty && passw != String.Empty)
                {
                    //MessageBox.Show("llenos");
                    if (cvusuario == "ADMIN")
                    {

                        response = usuario.AsignarAccesoUsuario(cvusuario, idtrab, nombre, pass, 0, usumod, prgmod, 1);
                    }
                    else
                    {

                        response = usuario.AsignarAccesoUsuario(cvusuario, idtrab, nombre, pass, 0, usumod, prgmod, 1);
                    }
                    
                    if (response == 0)
                    {
                        txtBuscar.Text = "";
                        txtCvUsuario.Text = "";
                        txtNombre.Text = "";
                        txtPassword.Text = "";
                        MessageBox.Show("El usuario " + nombre + " ya existe");
                        txtBuscar.Focus();
                    }

                    if (response == 1)
                    {
                        txtBuscar.Text = "";
                        txtCvUsuario.Text = "";
                        txtNombre.Text = "";
                        txtPassword.Text = "";
                        MessageBox.Show("El usuario " + nombre + " se agrego correctamente");
                    }

                    txtBuscar.Text = "";
                    txtCvUsuario.Text = "";
                    txtNombre.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("Asigna primero una busqueda");
                }
            }

            //ELIMINAR
            if (variable == 2)
            {
                if (cvusuario != String.Empty && nombre != String.Empty && passw != String.Empty)
                {
                    cvusuario = txtCvUsuario.Text;
                    response = usuario.EliminarAccesoUsuario(cvusuario, 0, "", "", 0, "", "", 3);

                    if (response == 1)
                    {

                        MessageBox.Show("El usuario esta Activado");

                        DataTable tabla = usuario.ObtenerAccesosUsuario(cvusuario, 0, "", "", 0, "", "", 2);

                        dgvAccesoUsuario.DataSource = tabla;

                        DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
                        imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
                        imgCheckPerfiles.Name = "SELECCIONAR";
                        dgvAccesoUsuario.Columns.Insert(0, imgCheckPerfiles);
                        ImageList imglt = new ImageList();
                        dgvAccesoUsuario.Columns[1].Visible = false;

                    }
                    else if (response == 0)
                    {
                        MessageBox.Show("El usuario esta inactivo");
                        DataTable tabla = usuario.ObtenerAccesosUsuario(cvusuario, 0, "", "", 0, "", "", 2);

                        dgvAccesoUsuario.DataSource = tabla;

                        DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
                        imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
                        imgCheckPerfiles.Name = "SELECCIONAR";
                        dgvAccesoUsuario.Columns.Insert(0, imgCheckPerfiles);
                        ImageList imglt = new ImageList();
                        dgvAccesoUsuario.Columns[1].Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Asigna primero una busqueda para Eliminar un Usuario");
                }
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtBuscarSipaa.Text = "";
            string buscar = txtBuscar.Text;

            if (utilerias.IsNumber(txtBuscar.Text))
            {

                //valida si esta vacio
                if (txtBuscar.Text != String.Empty)
                {
                    // toma elvalor de la busqueda 
                    idtrab = Convert.ToInt32(txtBuscar.Text);

                    //utilerias.DisableBotones(btnGuardar, 1, true);


                    try
                    {
                        //obtiene la lista 
                        usuario = usuario.ObtenerListaTrabajadorUsuario(idtrab);

                        //valida si lo encontro
                        if (usuario.enc == 1)
                        {
                            //valida si esta activo
                            if (usuario.st == 1)
                            {
                                ///asigna valores de sp
                                ///
                                txtCvUsuario.Enabled = false;
                                txtNombre.Enabled = false;
                                txtPassword.Enabled = false;
                                txtCvUsuario.Text = buscar;
                                txtNombre.Text = usuario.Nombre;
                                txtPassword.Text = buscar;
                                btnGuardar.Enabled = true;
                                variable = 1;


                            }
                            else
                            {
                                //MessageBox.Show("El usuario " + usuario.Nombre + " esta inactivo");
                                Utilerias.AsignarBotonResize(btnSipaa, Utilerias.PantallaSistema(), "Guardar");
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El usuario " + usuario.Nombre + " esta Inactivo");
                                timer1.Start();
                            }

                        }
                        else
                        {
                            //MessageBox.Show("No se encontró usuario en SONARH");
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se encontró usuario en SONARH");
                            timer1.Start();
                        }
                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("No se encontró usuario en SONARH"+ ex);
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se encontró usuario en SONARH");
                        timer1.Start();
                    }
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingrese Idtrab de usuario");
                    timer1.Start();
                    txtBuscar.Focus();

                }
            }
            else
            {
                //MessageBox.Show("Debes escribir solo números");
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Debes escribir solo números");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            variable = 1;
            cvusuario = txtCvUsuario.Text;
            nombre = txtNombre.Text;
            passw = txtPassword.Text;

            if (cvusuario != String.Empty && nombre != String.Empty && passw != String.Empty)
            {
                utilerias.DisableBotones(btnGuardar, 1, false);
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Asigna primero una busqueda");
                txtBuscar.Focus();
            }
        }
       
        private void btnBuscarSipaa_Click(object sender, EventArgs e)
        {
            //variable = 3;
            txtNombreSipaa.Text = "";
            ckbElimina.Visible = false;
            buscar = txtBuscarSipaa.Text;
            dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
            LlenaGridUsuarios(buscar.Trim(), 0, "", "", 0, "", "", 8);
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            variable = 3;
            label13.Text = "      Crear Usuario SIPAA";
            txtNombreSipaa.Text = "";
            panel1.Visible = true;
            ckbElimina.Visible = false;
            Utilerias.AsignarBotonResize(btnSipaa, Utilerias.PantallaSistema(), "Guardar");
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Crear_Acceso_Usuario_Load(object sender, EventArgs e)
        {
            //// Se crea lista de permisos por pantalla
            //LoginInfo.dtPermisosTrabajador = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab);
            //DataRow[] row = LoginInfo.dtPermisosTrabajador.Select("CVModulo = '" + this.Tag + "'");
            //LoginInfo.ltPermisosPantalla = Utilerias.CrearListaPermisoxPantalla(row, LoginInfo.ltPermisosPantalla);
            ////////////////////////////////////////////////////////
            //// resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            //// variables de permisos
            //Permisos.Crear = Utilerias.ControlPermiso("Crear", LoginInfo.ltPermisosPantalla);
            //Permisos.Actualizar = Utilerias.ControlPermiso("Actualizar", LoginInfo.ltPermisosPantalla);
            //Permisos.Eliminar = Utilerias.ControlPermiso("Eliminar", LoginInfo.ltPermisosPantalla);
            //Permisos.Imprimir = Utilerias.ControlPermiso("Imprimir", LoginInfo.ltPermisosPantalla);
            //////////////////////////////////////////////////////////////////////////////////////////

            panel1.Visible = false;        
            ckbElimina.Visible = false;
            btnGuardar.Enabled = false;
            txtPassword.Enabled = false;
            LlenaGridUsuarios("", 0, "", "", 0, "", "", 4);
            //txtBuscarSipaa.Focus();
            if (Permisos.Crear )
            {
                btnAgregar.Visible = false;
            }
            else
            {

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
        private void LlenaGridUsuarios(string cvusuario, int idtrab, string nombre, string pass, int stusuario, string usuumod, string prgmod, int opcion)
        {
            DataTable dtFormasRegistro = usuario.ObtenerListaUsuarios(cvusuario, idtrab, nombre, pass, stusuario, usuumod, prgmod, opcion);
            dgvAccesoUsuario.DataSource = dtFormasRegistro;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvAccesoUsuario.Columns.Insert(0, imgCheckProcesos);
            dgvAccesoUsuario.Columns[0].HeaderText = "Seleccionar";
            dgvAccesoUsuario.ClearSelection();
        }

        private void btnSipaa_Click(object sender, EventArgs e)
        {
            if (txtNombreSipaa.Text != String.Empty)
            {
                if (variable == 3)
                {
                    //MessageBox.Show("variable 3");
                    cvusuario = txtNombreSipaa.Text;
                    string pass = utilerias.cifradoMd5(cvusuario);
                    usumod = "140114";
                    prgmod = this.Name;
                    response = usuario.AsignarAccesoUsuario(cvusuario.Trim(), 0, cvusuario.Trim(), pass, 1, usumod, prgmod, 1);
                    dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                    LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);

                    if (response == 0)
                    {
                        txtBuscar.Text = "";
                        txtCvUsuario.Text = "";
                        txtNombre.Text = "";
                        txtPassword.Text = "";
                        txtNombreSipaa.Text = "";
                        //panel1.Visible = false;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El usuario " + nombre + " ya existe");
                        timer1.Start();
                    }

                    if (response == 1)
                    {
                        txtBuscar.Text = "";
                        txtCvUsuario.Text = "";
                        txtNombre.Text = "";
                        txtPassword.Text = "";
                        txtNombreSipaa.Text = "";
                        //panel1.Visible = false;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "El usuario " + nombre + " se agrego correctamente");
                        timer1.Start();
                        
                    }
                }
                //bloquea usuario sipaa
                if (variable == 4)
                {
                    if (cvusuario != String.Empty)
                    {   
                        response = usuario.EliminarAccesoUsuario(cvusuario, 0, "", "", 0, "", "", 3);

                        if (response == 1)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "El usuario esta Activado");
                            timer1.Start();
                            dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                            LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);

                        }
                        else if (response == 0)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El usuario esta inactivo");
                            timer1.Start();
                            dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                            LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);
                        }
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Asigna primero una busqueda dar de baja un Usuario");
                        timer1.Start();
                    }
                }
                //edita nombre usuario
                if (variable == 5)
                {
                    if (cvusuario != String.Empty)
                    {
                        if (ckbElimina.Checked == true)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Deselecciona el checkbox para editar");
                            timer1.Start();
                        }

                        nombre = txtNombreSipaa.Text;
                        response = usuario.EliminarAccesoUsuario(cvusuario.Trim(), 0, nombre.Trim(), "", 0, "", "", 2);

                        if (response == 1)
                        {
                            txtBuscar.Text = "";
                            txtCvUsuario.Text = "";
                            txtNombre.Text = "";
                            txtPassword.Text = "";
                            txtNombreSipaa.Text = "";
                            //panel1.Visible = false;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Usuario actualizado");
                            timer1.Start();
                            dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                            LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);

                        }
                        else if (response == 0)
                        {
                            txtBuscar.Text = "";
                            txtCvUsuario.Text = "";
                            txtNombre.Text = "";
                            txtPassword.Text = "";
                            txtNombreSipaa.Text = "";
                            //panel1.Visible = false;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Usuario no actualizado");
                            timer1.Start();
                            dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                            LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);
                        }
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Asigna primero una busqueda para Eliminar un Usuario");
                        timer1.Start();
                    }

                }

                //baja y alta de  usuario
                if (variable == 6)
                {
                    if (ckbElimina.Checked == true)
                    {
                        //MessageBox.Show("chekado ");
                        if (cvusuario != String.Empty)
                        {
                            response = usuario.EliminarAccesoUsuario(cvusuario, 0, "", "", 0, "", "", 3);

                            if (response == 1)
                            {
                                txtBuscar.Text = "";
                                txtCvUsuario.Text = "";
                                txtNombre.Text = "";
                                txtPassword.Text = "";
                                txtNombreSipaa.Text = "";
                                //panel1.Visible = false;
                                ckbElimina.Checked = false;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "El usuario esta Activado");
                                timer1.Start();
                                dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                                LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);

                            }
                            else if (response == 0)
                            {
                                txtBuscar.Text = "";
                                txtCvUsuario.Text = "";
                                txtNombre.Text = "";
                                txtPassword.Text = "";
                                txtNombreSipaa.Text = "";
                                //panel1.Visible = false;
                                ckbElimina.Checked = false;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El usuario esta Inactivo");
                                timer1.Start();
                                dgvAccesoUsuario.Columns.Remove(columnName: "Seleccionar");
                                LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);
                            }
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Asigna primero una busqueda para Eliminar un Usuario");
                            timer1.Start();
                        }
                    }
                    else if(ckbElimina.Checked == false)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Da click al checkbox");
                        timer1.Start();
                    }
                }
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Asigna un nombre");
                timer1.Start();
            }
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
        
        private void ckbElimina_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbElimina.Checked == true)
            {
                
                variable = 6;
                if (stusuario == "0")
                {
                    label13.Text = "      Alta Usuario SIPAA";
                    Utilerias.AsignarBotonResize(btnSipaa, Utilerias.PantallaSistema(), Botones.Alta);
                }
                else if (stusuario == "1")
                {
                    label13.Text = "      Baja Usuario SIPAA";
                    Utilerias.AsignarBotonResize(btnSipaa,Utilerias.PantallaSistema(),Botones.Baja);
                }
                
            }
            else
            {
                variable = 5;
                label13.Text = "      Editar Usuario SIPAA";
                Utilerias.AsignarBotonResize(btnSipaa, Utilerias.PantallaSistema(), Botones.Editar);

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
