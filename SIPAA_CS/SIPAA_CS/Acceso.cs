using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS
{
    public partial class Acceso : Form
    {
        public Point formPosition;
        public Boolean mouseAction;

        Usuario usuario = new Usuario();
        Utilerias utilerias = new Utilerias();

        public string user;
        public string pwd;
        public List<string> ltModulosxUsuario = new List<string>();
        public Acceso()
        {
            InitializeComponent();
        }


        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
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
        private void btnCerrar_Click_1(object sender, EventArgs e)
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

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //valida si estan vacios los textbox
            if (txtUsuario.Text != String.Empty && txtPwd.Text != String.Empty)
            {
                //MessageBox.Show("esta lleno");

                //valida si es numero
                if (utilerias.IsNumber(txtUsuario.Text))
                {
                    user = txtUsuario.Text;
                    pwd = txtPwd.Text;
                    int us = Convert.ToInt32(user);

                    string password = utilerias.cifradoMd5(pwd);

                    Console.Write(password);
                    //MessageBox.Show("es numero");
                    try
                    {
                        //obtiene la lista 
                        usuario = usuario.ObtenerListaTrabajadorUsuario(5,us);

                        //valida si lo encontro
                        if (usuario.enc == 1)
                        {
                            //valida si esta activo en sonarh
                            if (usuario.st == 1)
                            {
                                //MessageBox.Show("El usuario " + usuario.Nombre + " esta activo en sonarh");
                                int u = usuario.AsignarAccesoUsuario("", us, "", "", 0, "", "", 6);

                                //valida si esta activo en sipaa
                                if (u == 1)
                                {
                                    //MessageBox.Show("El usuario  esta activo en sipaa");
                                    int respuesta = usuario.AsignarAccesoUsuario(user.Trim(), us, "", password.Trim(), 0, "", "", 10);
                                    if (respuesta == 1)
                                    {
                                        ltModulosxUsuario = usuario.ObtenerListaModulosxUsuario(txtUsuario.Text, 4);

                                        ltModulosxUsuario = usuario.ObtenerListaModulosxUsuario(txtUsuario.Text, 4);

                                        if (ltModulosxUsuario.Count != 0)
                                        {
                                            //MessageBox.Show("si tienes padres");
                                            Dashboard ds = new Dashboard();
                                            LoginInfo.IdTrab = txtUsuario.Text;

                                            usuario = usuario.ObtenerDatosUsuario(txtUsuario.Text, 0, "", "", "", "", "", 7);

                                            string NomUsu = usuario.Nombre;
                                            LoginInfo.Nombre = NomUsu;
                                            //ds.RecibirIdTrab(txtUsuario.Text);
                                            ds.Show();
                                            this.Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("No tienes Módulos Asignados");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Usuario y contraseña no coincide");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("El usuario  esta inaactivo en sipaa");
                                }

                            }
                            else
                            {
                                MessageBox.Show("El usuario " + usuario.Nombre + " esta inactivo");
                                //utilerias.DisableBotones(btnGuardar, 1, true);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No se encontró usuario en SONARH");
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("No se encontró usuario en SONARH");
                    }
                }


                //valida si es texto
                if (!utilerias.IsNumber(txtUsuario.Text))
                {
                    user = txtUsuario.Text;
                    pwd = txtPwd.Text;
                    //int us = Convert.ToInt32(user);

                    string password = utilerias.cifradoMd5(pwd);

                    Console.Write(password);
                    //MessageBox.Show("no es numero");

                    try
                    {
                       
                        int u = usuario.AsignarAccesoUsuario(user, 0, "", "", 0, "", "", 9);

                        //valida si esta activo en sipaa
                        if (u == 1)
                        {
                            //MessageBox.Show("El usuario  esta activo en sipaa");
                            int respuesta = usuario.AsignarAccesoUsuario(user.Trim(), 0, "", password.Trim(), 0, "", "", 10);
                            if (respuesta == 1)
                            {

                                ltModulosxUsuario = usuario.ObtenerListaModulosxUsuario(txtUsuario.Text, 4);

                                if (ltModulosxUsuario.Count != 0)
                                {
                                    //MessageBox.Show("si tienes padres");
                                    Dashboard ds = new Dashboard();
                                    LoginInfo.IdTrab = txtUsuario.Text;

                                    usuario = usuario.ObtenerDatosUsuario(txtUsuario.Text, 0, "", "", "", "", "", 7);

                                    string NomUsu = usuario.Nombre;
                                    LoginInfo.Nombre = NomUsu;
                                    //ds.RecibirIdTrab(txtUsuario.Text);
                                    ds.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No tienes Módulos Asignados");
                                }
                               
                            }
                            else
                            {
                                MessageBox.Show("Usuario y Contraseña no coincide");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El usuario esta Inaactivo en SIPAA");
                        }
                        
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("No se encontró usuario en SONARH");
                    }
                }
            }
            else
            {
                MessageBox.Show("Asigna Usuario y Contraseña");
            }
            
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Acceso_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
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
