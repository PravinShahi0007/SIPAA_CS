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
using SIPAA_CS.App_Code;

//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación:12/09/2017       Última Modificacion: dd-mm-aaaa
//Descripción: cambia contraseña al crear usuario
//***********************************************************************************************


namespace SIPAA_CS.Accesos
{
    public partial class CambioContrasena : Form
    {

        Usuario usu = new Usuario();
        Utilerias utilerias = new Utilerias();

        public CambioContrasena()
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

        private void btguardar_Click(object sender, EventArgs e)
        {

            try
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)//datos correctos
                {

                    int ivalida = usu.iactpw(18, LoginInfo.IdTrab, utilerias.cifradoMd5(txtconfirmcontrasena.Text.Trim()), LoginInfo.IdTrab, this.Name);

                    if (ivalida == 2)
                    {
                        txtcontrasena.Text = "";
                        txtconfirmcontrasena.Text = "";
                        DialogResult result = MessageBox.Show("Contraseña Modificada con exito!, vuelva a ingresar al sistema", "SIPAA", MessageBoxButtons.OK);
                        LoginInfo.IdTrab = String.Empty;
                        Acceso frm = new Acceso();
                        this.Hide();
                        frm.Show();
                    }

                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }

        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void CambioContrasena_Load(object sender, EventArgs e)
        {
            lblusuario.Text = LoginInfo.Nombre;
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //validacion de campos
        private Boolean fvalidacampos()
        {
            string sid = utilerias.cifradoMd5(LoginInfo.IdTrab);
            string spass = utilerias.cifradoMd5(txtcontrasena.Text.Trim());


            if ((txtcontrasena.Text.Trim().Length) <= 3)
            {
                DialogResult result = MessageBox.Show("La contraseña que ingreso contiene menos de 4 caracteres, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcontrasena.Focus();
                return false;
            }
            //else if ((txtconfirmcontrasena.Text.Trim().Length) <= 3)
            //{
            //    DialogResult result = MessageBox.Show("La confirmación de contraseña que ingreso contiene menos de 4 caracteres, verificar", "SIPAA", MessageBoxButtons.OK);
            //    txtconfirmcontrasena.Focus();
            //    return false;
            //}
            else if (txtcontrasena.Text.Trim() != txtconfirmcontrasena.Text.Trim())
            {
                DialogResult result = MessageBox.Show("La contraseña no coincide, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcontrasena.Focus();
                return false;
            }
            else if (sid == spass)
            {
                DialogResult result = MessageBox.Show("La contraseña no puede ser igual a su usuario, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcontrasena.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
