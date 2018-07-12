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
//Fecha creación:13-sep-2017       Última Modificacion: dd-mm-aaaa
//Descripción: resetea password de usuario
//***********************************************************************************************

namespace SIPAA_CS.Accesos.Asignaciones
{
    public partial class ResetPassw : Form
    {
        #region
        string smail, scvdominio, spasswant, snombreusueario, susuarioreset;
        int istusu;
        #endregion

        Utilerias cutilerias = new Utilerias();
        Usuarioap cusuarioap = new Usuarioap();

        public ResetPassw()
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

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            AcceDashboard accdasb = new AcceDashboard();
            accdasb.Show();
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //variables datos del usuario
            DataTable datosusuario = cusuarioap.dtdatos(4, txtcvusuarios.Text, 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            //smail = datosusuario.Rows[0][3].ToString();
            //scvdominio = datosusuario.Rows[0][4].ToString();
            //spasswant = datosusuario.Rows[0][6].ToString();
            //istusu = Int32.Parse(datosusuario.Rows[0][7].ToString());

            if (datosusuario.Rows.Count >= 1)
            {
                istusu = Int32.Parse(datosusuario.Rows[0][7].ToString());
            }
            else
            {
                istusu = 0;
            }
            

            if (datosusuario.Rows.Count <= 0)
            {
                txtusuario.Text = "";
                txtestatus.Text = "";
                txtcorreo.Text = "";

                //cb dominios
                cutilerias.p_inicbo = 0;
                cbodominios.DataSource = null;
                DataTable dtdatos = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
                Utilerias.llenarComboxDataTable(cbodominios, dtdatos, "cv", "desc");
                cutilerias.p_inicbo = 1;

                DialogResult result = MessageBox.Show("El usuario que busca no existe, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcvusuarios.Focus();
                btguardar.Enabled = false;
            }
            else if (istusu == 0)
            {
                DialogResult result = MessageBox.Show("El usuario esta inactivo, no se le puede restaurar la contraseña, verificar", "SIPAA", MessageBoxButtons.OK);
                txtusuario.Text = datosusuario.Rows[0][1].ToString();
                txtestatus.Text = datosusuario.Rows[0][8].ToString();
                txtcorreo.Text = datosusuario.Rows[0][3].ToString();

            }
            else
            {
                txtusuario.Text = datosusuario.Rows[0][1].ToString();
                txtestatus.Text = datosusuario.Rows[0][8].ToString();
                txtcorreo.Text = datosusuario.Rows[0][3].ToString();
                scvdominio = datosusuario.Rows[0][4].ToString();
                snombreusueario = datosusuario.Rows[0][1].ToString();
                susuarioreset = datosusuario.Rows[0][0].ToString();

                //cb dominios
                cutilerias.p_inicbo = 0;
                cbodominios.DataSource = null;
                DataTable dtdatos = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
                Utilerias.llenarComboxDataTable(cbodominios, dtdatos, "cv", "desc");

                if (scvdominio != "0") { cbodominios.SelectedValue = scvdominio; }
                cutilerias.p_inicbo = 1;

                btguardar.Enabled = true;
                btguardar.Focus();

            }
            
        }

        private void btguardar_Click(object sender, EventArgs e)
        {
            if (txtcorreo.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Captura un correo electrónico para recuperar contraseña, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcorreo.Focus();
            }
            else if (cbodominios.Text.Trim() == "" || cbodominios.SelectedIndex == -1 || cbodominios.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona un dominio", "SIPAA", MessageBoxButtons.OK);
                cbodominios.Focus();

            }
            else if (susuarioreset == "")
            {
                DialogResult result = MessageBox.Show("Captura un usuario, verificar", "SIPAA", MessageBoxButtons.OK);
                txtcvusuarios.Focus();
            }
            else
            {
                //restaura contraseña
                DialogResult result = MessageBox.Show(LoginInfo.Nombre + ": esta acción restaura la contraseña del usuario;" + "\r\n" + "\r\n" + snombreusueario + "\r\n" + "\r\n" + "¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int ivalida = cusuarioap.cruddatos(8, susuarioreset, 0, "", txtcorreo.Text.Trim(),
                                   Int32.Parse(cbodominios.SelectedValue.ToString()), "", 0, 1, "",
                                   "", "", "", "", "",
                                   0, 3, LoginInfo.cvusuario, "", this.Name,
                                   cutilerias.scontrol());

                    if (ivalida == 2)
                    {
                        txtcvusuarios.Text = "";
                        txtusuario.Text = "";
                        txtestatus.Text = "";
                        txtcorreo.Text = "";

                        //cb dominios
                        cutilerias.p_inicbo = 0;
                        cbodominios.DataSource = null;
                        DataTable dtdatos = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
                        Utilerias.llenarComboxDataTable(cbodominios, dtdatos, "cv", "desc");
                        cutilerias.p_inicbo = 1;

                        btguardar.Enabled = false;
                        susuarioreset = "";
                        DialogResult result1 = MessageBox.Show("Contraseña Modificada con exito!  En breve le llegara un correo electrónico al usuario con su contraseña temporal.", "SIPAA", MessageBoxButtons.OK);
                        txtcvusuarios.Focus();
                    }
                }
                else
                {
                }
            }

            
        }


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void ResetPassw_Load(object sender, EventArgs e)
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

            //tool tip
            ftooltip();

            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

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
            toolTip1.SetToolTip(this.btnCerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            //toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
    }
}
