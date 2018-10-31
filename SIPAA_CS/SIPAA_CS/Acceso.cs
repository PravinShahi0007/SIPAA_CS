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
using SIPAA_CS.Accesos;
using SIPAA_CS.Conexiones;
using SIPAA_CS.App_Code.Accesos.Catalogos;

//***********************************************************************************************
//Autor: Gamaliel Lobato Solis    Modif: noe alvarez marquina
//Fecha creación:dd-mm-aaaa       Última Modificacion: 30-cot-2018
//Descripción: se cambia a loggin a correo electronico
//***********************************************************************************************

namespace SIPAA_CS
{
    public partial class Acceso : Form
    {
        public Point formPosition;
        public Boolean mouseAction;

        Usuario usuario = new Usuario();
        Utilerias utilerias = new Utilerias();
        Conexion conex = new Conexion();
        Usuarioap cusuarioap = new Usuarioap();
        cAcceso clacceso = new cAcceso();

        public string user;
        public string pwd;
        public List<string> ltModulosxUsuario = new List<string>();
        public Acceso()
        {
            InitializeComponent();
        }
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
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

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
            //valida formato de correo electronico
            string mail = txtUsuario.Text.Trim();
            bool verificar = mail.Contains("@");

            //valida informacion correcta
            if (verificar == false)
            {
                MessageBox.Show("Ingresa un correo electrónico valido", "SIPAA", MessageBoxButtons.OK);
                txtUsuario.Focus();
            }
            else if (txtUsuario.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa tu correo electrónico", "SIPAA", MessageBoxButtons.OK);
                txtUsuario.Focus();
            }
            else if (txtPwd.Text.Trim() == "")
            {
                MessageBox.Show("Ingresa tu contraseña", "SIPAA", MessageBoxButtons.OK);
                txtUsuario.Focus();
            }
            else
            {
                //funcion validar usuario
                fvalidausuario(utilerias.cifradoMd5(txtUsuario.Text.Trim()), utilerias.cifradoMd5(txtPwd.Text.Trim()));
            }            
        }

        //recupera contraseña
        private void btnreccontrasena_Click(object sender, EventArgs e)
        {
            //ventana recuperar contraseña
            RecuperaContrasena reccon = new RecuperaContrasena();
            reccon.Show();
            this.Close();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Acceso_Load(object sender, EventArgs e)
        {
            //inicia tool tip
            ftooltip();

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
        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btnCerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
        }

        //valida usuario por email
        private void fvalidausuario(string susuario, string spassw)
        {
            //obtiene datos del usuario
            DataTable dtaccesos = clacceso.dtdatos(1, susuario, spassw);

            int inumregusu = dtaccesos.Rows.Count;
            if (inumregusu == 0)
            {
                DialogResult result = MessageBox.Show("El usuario no existe, verifique usuario y contraseña", "SIPAA", MessageBoxButtons.OK);
                txtUsuario.Focus();
            }
            else if (inumregusu == 1)
            {
                string stusuario = dtaccesos.Rows[0][3].ToString();
                string stpassw   = dtaccesos.Rows[0][2].ToString();

                LoginInfo.Nombre    = dtaccesos.Rows[0][1].ToString();
                LoginInfo.cvusuario = dtaccesos.Rows[0][0].ToString();
                LoginInfo.IdTrab    = dtaccesos.Rows[0][0].ToString();

                if (stusuario == "0")
                {
                    DialogResult result = MessageBox.Show("Su usuario esta dado de baja, comuniquese al área de sistemas para verificar su situación", "SIPAA", MessageBoxButtons.OK);
                }
                else if (stpassw == "1")
                {
                    CambioContrasena camcon = new CambioContrasena();
                    camcon.Show();
                    this.Close();
                }
                else
                {
                    ltModulosxUsuario = usuario.ObtenerListaModulosxUsuario(LoginInfo.cvusuario, 6);

                    //actualiza acceso
                    cusuarioap.cruddatos(6, LoginInfo.cvusuario, 0, "", "", 0, "", 0, 0, "", "", "", "", utilerias.scontrol(), "SIPAA CS", 0, 0, "", "", "", "");
                    
                    //abre dashboard
                    Dashboard ds = new Dashboard();
                    ds.Show();
                    this.Close();
                }

            }

        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


    }
}
