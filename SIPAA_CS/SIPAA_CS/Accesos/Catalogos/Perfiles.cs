using SIPAA_CS.App_Code;
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


    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara       modif: noe alvarez marquina   -------su quitan variables fijas, mensajes
    //Fecha creación:13-03-2017       Última Modificacion: 25-09-2017
    //Descripción: Pantalla que permite la gestión de Perfiles de usuario
    //***********************************************************************************************

    public partial class Perfiles : Form
    {
        public Point formPosition;
        public Boolean mouseAction;
        int iOpcionAdmin;
        public int IdPerfil;
        string strEstatus;
        public List<string> ltPermisos = new List<string>();
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void LlenarGridPerfiles(DataTable dtPerfiles)
        {
            if (Permisos.dcPermisos["Eliminar"] == 1 || Permisos.dcPermisos["Actualizar"] == 1)
            {
                if (dgvPerfiles.Columns.Count == 0)
                {

                    Utilerias.AgregarCheck(dgvPerfiles, 0);
                   
                }
            }
            else {
              label2.Text  = "Perfiles Registrados";
                dgvPerfiles.Enabled = false;
            }

            dgvPerfiles.DataSource = dtPerfiles;

            dgvPerfiles.Columns[0].Width = 90;
            dgvPerfiles.Columns[2].Width = 400;
            dgvPerfiles.Columns[6].Width = 80;
            

            dgvPerfiles.Columns["CVPERFIL"].Visible = false;
            dgvPerfiles.Columns["USUUMOD"].Visible = false;
            dgvPerfiles.Columns["FHUMOD"].Visible = false;
            dgvPerfiles.Columns["PRGUMOD"].Visible = false;
           
            dgvPerfiles.Visible = true;
            
            dgvPerfiles.ClearSelection();

        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Checked = false;
            ckbEliminar.Visible = false;
            lblAccion.Text = "       Perfil Seleccionado";
          
          
            PanelEditar.Visible = false;
            txtPerfil.Text = "Sin Selección";

            Perfil objPerfil = new Perfil();

            string strPerfil = "%";

            if (txtBuscarPerfil.Text != String.Empty)
            {
                strPerfil = txtBuscarPerfil.Text.Trim();
            }

            string strEstatus = "%";

            if (cbEstatus.SelectedIndex > 0)
            {
                if (cbEstatus.SelectedIndex == 1)
                {
                    strEstatus = "1";
                }
                else if (cbEstatus.SelectedIndex == 2)
                {
                    strEstatus = "0";
                }
            }
            else
            {
                strEstatus = "%";
            }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, strEstatus);
            LlenarGridPerfiles(dtPerfiles);

        }



        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            ckbEliminar.Checked = false;
           
            txtPerfil.Text = "";
            PanelEditar.Visible = true;
            lblAccion.Text = "       Nuevo Perfil";

            iOpcionAdmin = 1;
            //btnEditar.Visible = false;
            btnGuardar.Image = Resources.Guardar;
            //Utilerias.CambioBoton(btnGuardar,btnEliminar ,btnEditar, btnGuardar);
            Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Guardar");

            txtPerfil.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            lblAccion.Text = "       Perfil Seleccionado";

            Perfil objPerfil = new Perfil();
            objPerfil.CVPerfil = IdPerfil;
            objPerfil.Descripcion = txtPerfil.Text.Trim();
            objPerfil.PrguMod = this.Name;
            objPerfil.UsuuMod = LoginInfo.IdTrab;
            string strMensaje = "";

            if (txtPerfil.Text.Trim() == String.Empty)
            {
                DialogResult result = MessageBox.Show("Captura una perfil", "SIPAA", MessageBoxButtons.OK);
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Capture un perfil");
                txtPerfil.Focus();
            }
            else
            {

                if (iOpcionAdmin == 1)
                {
                    strMensaje = "Perfil Guardado Correctamente";
                }
                else if (iOpcionAdmin == 2)
                {
                    strMensaje = "Perfil Actualizado Correctamente";
                }
                else if (iOpcionAdmin == 3)
                {
                    strMensaje = "Cambio de estatus realizado correctamente";
                }

                int iResponse = GestionarPefilesxOpcion(txtPerfil, objPerfil, strMensaje, iOpcionAdmin, sender, e);


                ckbEliminar.Checked = false;
                if (iResponse != 0)
                {
                    btnBuscar_Click(sender, e);
                }

            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public Perfiles()
        {
            InitializeComponent();
        }

        private void Crear_Perfil_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }

            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            Modulo objModulo = new Modulo();
            
            if (Permisos.dcPermisos["Crear"] == 0 ) {

                btnAgregar.Visible = false;
            }

            lblAccion.Text = "       Perfil Seleccionado";
            txtPerfil.Text = "Sin Selección";

            Perfil objPerfil = new Perfil();

            string strPerfil = "%";

            if (txtBuscarPerfil.Text != String.Empty)
            {
                strPerfil = txtBuscarPerfil.Text.Trim();
            }

            string strEstatus = "%";

            if (cbEstatus.SelectedIndex > 0)
            {
                if (cbEstatus.SelectedIndex == 1)
                {
                    strEstatus = "1";
                }
                else if (cbEstatus.SelectedIndex == 2)
                {
                    strEstatus = "0";
                }
            }
            else
            {
                strEstatus = "%";
            }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, strEstatus);
            LlenarGridPerfiles(dtPerfiles);

        }
       
        private void dgvPerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void PanelEditar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbEliminar.Checked == true)
            {

                if (strEstatus == "Inactivo")
                {

                    Utilerias.AsignarBotonResize(btnGuardar, PantallaSistema(), Botones.Alta);
                }
                else if (strEstatus == "Activo")
                {

                    Utilerias.AsignarBotonResize(btnGuardar, PantallaSistema(), Botones.Baja);
                }

                iOpcionAdmin = 3;
                //Utilerias.CambioBoton(btnGuardar, btnEditar, btnGuardar, btnEliminar);
              //  Utilerias.AsignarBotonResize(btnGuardar, PantallaSistema(), Botones.Alta);
            }
            else
            {
                iOpcionAdmin = 2;
                btnGuardar.Image = Resources.btnEdit;
                //Utilerias.CambioBoton(btnGuardar, btnEliminar, btnGuardar, btnEditar);
                Utilerias.AsignarBotonResize(btnGuardar, PantallaSistema(), Botones.Editar);

            }


        }

        private void PanelPlantilla_Paint(object sender, PaintEventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private int GestionarPefilesxOpcion(TextBox txt, Perfil objPerfil,
                     string strMensaje, int iOpcion, object sender, EventArgs e)
        {
            if (txt.Text.Trim() != String.Empty)
            {
                try
                {
                    int iResponse = 0;
                    string Mensaje = "";

                    if (strEstatus == "Inactivo")
                    {
                        Mensaje = "¿Seguro que desea dar de ALTA el perfil?";
                    }
                    else
                    {
                        Mensaje = "¿Seguro que desea dar de BAJA el perfil?";
                    }

                    
                    switch (iOpcionAdmin)
                    {

                        case 3:
                            DialogResult result = MessageBox.Show(Mensaje, "SIPAA", MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                            {
                                 iResponse = objPerfil.GestionarPerfiles(objPerfil, iOpcion);
                                
                            }
                            break;

                        default:
                                 iResponse = objPerfil.GestionarPerfiles(objPerfil, iOpcion);


                            break;
                    }


                    if (iResponse != 0)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, strMensaje);
                        timer1.Start();
                        return iResponse;
                    }
                    else if (iResponse == 0)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El Perfil Ingresado ya existe, verificar");
                        timer1.Start();
                        return iResponse;
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde" + ex);
                    DialogResult result = MessageBox.Show(ex.ToString());
                    timer1.Start();
                    return 0;
                }
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Capture un perfil");
                timer1.Start();
                return 0;
            }
            return 0;
        }

        private void txtBuscarPerfil_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            AcceDashboard accedb = new AcceDashboard();
            accedb.Show();
            this.Close();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
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

        private void dgvPerfiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {





            //    Utilerias.ApagarControlxPermiso(ckbEliminar, "Eliminar", ltPermisos);
            for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
            {
                dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvPerfiles.SelectedRows.Count != 0)
            {
                lblAccion.Text = "       Perfil Seleccionado";
                DataGridViewRow row = this.dgvPerfiles.SelectedRows[0];

                IdPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                string ValorRow = row.Cells["DESCRIPCION"].Value.ToString();
                strEstatus = row.Cells["Estatus"].Value.ToString();
                txtPerfil.Text = ValorRow;
                PanelEditar.Visible = true;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;


                //Permisos
                if (Permisos.dcPermisos["Eliminar"] == 1 && Permisos.dcPermisos["Actualizar"] == 1)
                {

                    Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Editar");
                    iOpcionAdmin = 2;

                    ckbEliminar.Visible = true;
                    ckbEliminar.Checked = false;

                    if (strEstatus == "Inactivo")
                    {
                        ckbEliminar.Text = "Alta";

                    }
                    else if (strEstatus == "Activo")
                    {
                        ckbEliminar.Text = "Baja";

                    }

                }
                else if (Permisos.dcPermisos["Eliminar"] == 1)
                {
                    iOpcionAdmin = 3;
                    if (strEstatus == "0")
                    {
                        Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Alta");

                    }
                    else if (strEstatus == "1")
                    {
                        Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Baja");
                    }
                }
                else if (Permisos.dcPermisos["Actualizar"] == 1)
                {
                    Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Editar");
                    iOpcionAdmin = 2;
                }






                //  Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);



            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}



//private void barraSuperior_MouseUp(object sender, MouseEventArgs e)
//{
//    mouseAction = false;
//}

//private void barraSuperior_MouseDown(object sender, MouseEventArgs e)
//{

//    formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
//    mouseAction = true;
//}

//private void barraSuperior_MouseMove(object sender, MouseEventArgs e)
//{

//    if (mouseAction == true)
//    {
//        Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
//    }
//}