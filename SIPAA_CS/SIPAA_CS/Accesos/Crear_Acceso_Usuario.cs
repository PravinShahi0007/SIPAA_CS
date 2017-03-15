using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using SIPAA_CS.Recursos_Humanos.App_Code;
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

namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    public partial class Crear_Acceso_Usuario : Form
    {
        public Point formPosition;
        public Boolean mouseAction;

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


        public int variable = 0;

        Utilerias utilerias = new Utilerias();
        Usuario usuario = new Usuario();


        public Crear_Acceso_Usuario()
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


                DataGridViewRow row = this.dgvAccesoUsuario.SelectedRows[0];

                cvusuario = row.Cells["CVUSUARIO"].Value.ToString();
                idtrab = Convert.ToInt32(row.Cells["IDTRAB"].Value.ToString());
                nombre = row.Cells["NOMBRE"].Value.ToString();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                //cajas de texto panel actualizar
                txtCvUsuario.Text = cvusuario;
                txtNombre.Text = nombre;
                txtPassword.Text = Convert.ToString(idtrab);

                utilerias.DisableBotones(btnElimina, 3, false);

            }
        }
        
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que deseas salir?", "SIPAA", MessageBoxButtons.YesNo);

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


            prgmod = "Crear_Acceso_Usuario";

            pass = utilerias.cifradoMd5(passw);


            //AGREGAR
            if (variable == 1)
            {
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

                    //response = usuario.AsignarAccesoUsuario(cvusuario, idtrab, nombre, pass, 0, usumod, prgmod, 1);
                    if (response == 0)
                    {
                        txtBuscar.Text = "";
                        txtCvUsuario.Text = "";
                        txtNombre.Text = "";
                        txtPassword.Text = "";
                        MessageBox.Show("El usuario " + nombre + " ya existe");
                        utilerias.DisableBotones(btnGuardar, 1, true);
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

            //ACTUALIZAR
            if (variable == 3)
            {

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

                    utilerias.DisableBotones(btnGuardar, 1, true);


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
                                txtCvUsuario.Text = buscar;
                                txtNombre.Text = usuario.Nombre;
                                txtPassword.Text = buscar;


                            }
                            else
                            {
                                MessageBox.Show("El usuario " + usuario.Nombre + " esta inactivo");
                                utilerias.DisableBotones(btnGuardar, 1, true);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No se encontró usuario en SONARH");
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("No se encontró usuario en SONARH"+ ex);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Idtrab de usuario");
                    txtBuscar.Focus();
                }
            }
            else
            {
                MessageBox.Show("Debes escribir solo números");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            variable = 1;

            cvusuario = txtCvUsuario.Text;
            nombre = txtNombre.Text;
            passw = txtPassword.Text;

            passw = txtPassword.Text;

            if (cvusuario != String.Empty && nombre != String.Empty && passw != String.Empty)
            {

                utilerias.DisableBotones(btnGuardar, 1, false);

            }
            else
            {

                MessageBox.Show("Asigna primero una busqueda");
                txtBuscar.Focus();
            }
        }


        private void btnElimina_Click(object sender, EventArgs e)
        {
            variable = 2;
            utilerias.DisableBotones(btnGuardar, 3, false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            variable = 3;
            utilerias.DisableBotones(btnGuardar, 2, false);



        }

        private void btnBuscarSipaa_Click(object sender, EventArgs e)
        {
            utilerias.DisableBotones(btnGuardar, 0, true);
            utilerias.DisableBotones(btnElimina, 0, true);
            utilerias.DisableBotones(btnEditar, 0, true);
            txtBuscar.Text = "";

            txtCvUsuario.Text = "";
            txtNombre.Text = "";
            txtPassword.Text = "";

            CVusuario = txtBuscarSipaa.Text;

            buscar = txtBuscarSipaa.Text;

            if (buscar == "ADMIN")
            {
                MessageBox.Show("sI ES ADMON");

                txtCvUsuario.Text = "ADMIN";
                txtNombre.Text = "ADMIN";
                txtPassword.Text = "ADMIN";

                DataTable tabla = usuario.ObtenerAccesosUsuario(CVusuario, 0, "", "", 0, "", "", 2);

                dgvAccesoUsuario.DataSource = tabla;

                DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
                imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckPerfiles.Name = "SELECCIONAR";
                dgvAccesoUsuario.Columns.Insert(0, imgCheckPerfiles);
                ImageList imglt = new ImageList();
                dgvAccesoUsuario.Columns[1].Visible = false;
            }
            else
            {
                MessageBox.Show("No es Admin");


                if (CVusuario != String.Empty)
                {

                    DataTable tabla = usuario.ObtenerAccesosUsuario(CVusuario, 0, "", "", 0, "", "", 2);

                    dgvAccesoUsuario.DataSource = tabla;

                    DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
                    imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
                    imgCheckPerfiles.Name = "SELECCIONAR";
                    dgvAccesoUsuario.Columns.Insert(0, imgCheckPerfiles);
                    ImageList imglt = new ImageList();
                    dgvAccesoUsuario.Columns[1].Visible = false;


                }
                else
                {
                    MessageBox.Show("Asigna una primero un CvUsuario");
                    txtBuscarSipaa.Focus();
                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Crear_Acceso_Usuario_Load(object sender, EventArgs e)
        {


            txtCvUsuario.Enabled = false;
            txtNombre.Enabled = false;

            utilerias.DisableBotones(btnGuardar, 1, true);
            utilerias.DisableBotones(btnElimina, 1, true);
            utilerias.DisableBotones(btnEditar, 1, true);
            txtBuscarSipaa.Focus();


        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
            }
        }
        
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
        
    }
}
