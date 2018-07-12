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
using SIPAA_CS.App_Code.Accesos.Catalogos;

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
        Usuarioap cusuarioap = new Usuarioap();
        Utilerias utilerias = new Utilerias();

        string smail, scvdominio, sultacceso;

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

                    int ivalida = cusuarioap.cruddatos(7, LoginInfo.cvusuario, 0, "", txtcorreo.Text.Trim(), 
                                                       Int32.Parse(cbodominios.SelectedValue.ToString()), utilerias.cifradoMd5(txtcontrasena.Text), 0, 0, "", 
                                                       "", "", "", "", "", 
                                                       0, 1, LoginInfo.cvusuario, "", this.Name, 
                                                       utilerias.scontrol());

                    if (ivalida == 2)
                    {
                        txtcontrasena.Text = "";
                        txtconfirmcontrasena.Text = "";
                        txtcorreo.Text = "";
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
            //actualiza acceso
            cusuarioap.cruddatos(6, LoginInfo.cvusuario, 0, "", "", 0, "", 0, 0, "", "", "", "", utilerias.scontrol(), "SIPAA CS", 0, 0, "", "", "", "");

            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //variables datos del usuario
            DataTable datosusuario = cusuarioap.dtdatos(4, LoginInfo.cvusuario, 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            smail = datosusuario.Rows[0][3].ToString();
            scvdominio = datosusuario.Rows[0][4].ToString();
            sultacceso = datosusuario.Rows[0][5].ToString();

            lblacceso.Text = sultacceso;

            //cb dominios
            utilerias.p_inicbo = 0;
            cbodominios.DataSource = null;
            DataTable dtdatos = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            Utilerias.llenarComboxDataTable(cbodominios, dtdatos, "cv", "desc");

            if (scvdominio != "0"){ cbodominios.SelectedValue = scvdominio; }
            utilerias.p_inicbo = 1;

            txtcorreo.Text = smail;

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
            else if (txtcorreo.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Captura un correo electrónico para recuperar contraseña, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcorreo.Focus();
                return false;
            }
            else if (cbodominios.Text.Trim() == "" || cbodominios.SelectedIndex == -1 || cbodominios.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un dominio", "SIPAA", MessageBoxButtons.OK);
                cbodominios.Focus();
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
