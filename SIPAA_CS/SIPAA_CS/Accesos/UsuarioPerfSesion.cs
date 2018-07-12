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
//Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
//Descripción: -------------------------------
//***********************************************************************************************

namespace SIPAA_CS.Accesos
{
    public partial class UsuarioPerfSesion : Form
    {
        #region
        string smail, scvdominio, spasswant;
        #endregion

        Perfil DatPerfil = new Perfil();
        Utilerias cutilerias = new Utilerias();
        Usuarioap cusuarioap = new Usuarioap();


        public UsuarioPerfSesion()
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

        //boton cerrar
        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //boton minimizar
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton regresar
        private void btnregresar_Click(object sender, EventArgs e)
        {
            Dashboard dashb = new Dashboard();
            dashb.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void UsuarioPerfSesion_Load(object sender, EventArgs e)
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

            //inicializa tool tip
            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //variables datos del usuario
            DataTable datosusuario = cusuarioap.dtdatos(4, LoginInfo.cvusuario, 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            smail = datosusuario.Rows[0][3].ToString();
            scvdominio = datosusuario.Rows[0][4].ToString();
            spasswant = datosusuario.Rows[0][6].ToString();

            //cb dominios
            cutilerias.p_inicbo = 0;
            cbodominios.DataSource = null;
            DataTable dtdatos = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            Utilerias.llenarComboxDataTable(cbodominios, dtdatos, "cv", "desc");

            if (scvdominio != "0") { cbodominios.SelectedValue = scvdominio; }
            cutilerias.p_inicbo = 1;

            txtcorreo.Text = smail;

        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //funcion para tool tip
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
        }

        private void btguardar_Click(object sender, EventArgs e)
        {
            try
            {
                //valida campos
                Boolean bvalidacampos = fvalidacampos();

                if (bvalidacampos == true)//datos correctos
                {

                    int ivalida = cusuarioap.cruddatos(7, LoginInfo.cvusuario, 0, "", txtcorreo.Text.Trim(),
                                                       Int32.Parse(cbodominios.SelectedValue.ToString()), cutilerias.cifradoMd5(txtconnueva.Text), 0, 0, "",
                                                       "", "", "", "", "",
                                                       0, 2, LoginInfo.cvusuario, "", this.Name,
                                                       cutilerias.scontrol());

                    if (ivalida == 2)
                    {
                        txtcontrasenaact.Text = "";
                        txtconnueva.Text = "";
                        txtconnuevaconf.Text = "";
                        txtcorreo.Text = "";

                        //cb dominios
                        cutilerias.p_inicbo = 0;
                        cbodominios.DataSource = null;
                        DataTable dtdatos = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
                        Utilerias.llenarComboxDataTable(cbodominios, dtdatos, "cv", "desc");

                        //if (scvdominio != "0") { cbodominios.SelectedValue = scvdominio; }
                        cutilerias.p_inicbo = 1;

                        DialogResult result = MessageBox.Show("Perfil modificado con exito!, vuelva a ingresar al sistema", "SIPAA", MessageBoxButtons.OK);
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
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
        //validacion de campos
        private Boolean fvalidacampos()
        {
            string spassactcap = cutilerias.cifradoMd5(txtcontrasenaact.Text.Trim());

            if ((txtcontrasenaact.Text.Trim().Length) <= 3)
            {
                DialogResult result = MessageBox.Show("La contraseña actual es invalida, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcontrasenaact.Focus();
                return false;
            }
            else if ((txtconnueva.Text.Trim().Length) <= 3)
            {
                DialogResult result = MessageBox.Show("La contraseña que ingreso contiene menos de 4 caracteres, verificar", "SIPAA", MessageBoxButtons.OK);
                txtconnueva.Focus();
                return false;
            }
            else if (txtconnueva.Text.Trim() != txtconnuevaconf.Text.Trim())
            {
                DialogResult result = MessageBox.Show("La contraseña nueva no coincide, verificar", "SIPAA", MessageBoxButtons.OK);
                txtconnueva.Focus();
                return false;
            }
            else if (spassactcap != spasswant)
            {
                DialogResult result = MessageBox.Show("La contraseña actual no coincide, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcontrasenaact.Focus();
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
            else if (LoginInfo.cvusuario == txtconnueva.Text.Trim())
            {
                DialogResult result = MessageBox.Show("La contraseña no puede ser igual a su usuario, verificar", "SIPAA", MessageBoxButtons.OK);
                txtconnueva.Focus();
                return false;
            }
            else if (spasswant == cutilerias.cifradoMd5(txtconnueva.Text))
            {
                DialogResult result = MessageBox.Show("La contraseña actual no puede ser igual a la contraseña anterior, verificar", "SIPAA", MessageBoxButtons.OK);
                txtconnueva.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
