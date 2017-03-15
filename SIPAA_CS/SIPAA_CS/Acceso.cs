using SIPAA_CS.Recursos_Humanos.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.Recursos_Humanos.App_Code.Usuario;

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
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {

            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            user = txtUsuario.Text;
            pwd = txtPwd.Text;
            int us = Convert.ToInt32(txtUsuario.Text);

            string  password = utilerias.cifradoMd5(pwd);

            Console.Write(password);
                

            try
            {
                //obtiene la lista 
                usuario = usuario.ObtenerListaTrabajadorUsuario(us);

                //valida si lo encontro
                if (usuario.enc == 1)
                {
                    //valida si esta activo en sonarh
                    if (usuario.st == 1)
                    {
                        //MessageBox.Show("El usuario " + usuario.Nombre + " esta activo en sonarh");
                        int u = usuario.AsignarAccesoUsuario("",us,"","",0,"","",4);

                        //valida si esta activo en sipaa
                        if (u == 1)
                        {
                            //MessageBox.Show("El usuario  esta activo en sipaa");
                            int respuesta = usuario.AsignarAccesoUsuario(user.Trim(), us, "", password.Trim(), 0, "", "", 5);
                            if (respuesta == 1)
                            {
                                Dashboard ds = new Dashboard();
                                LoginInfo.IdTrab = txtUsuario.Text;
                                //ds.RecibirIdTrab(txtUsuario.Text);
                                ds.Show();
                                this.Close();
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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Acceso_Load(object sender, EventArgs e)
        {

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
