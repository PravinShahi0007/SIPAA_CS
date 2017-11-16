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
//Fecha creación:13-sep-2017       Última Modificacion: dd-mm-aaaa
//Descripción: resetea password de usuario
//***********************************************************************************************

namespace SIPAA_CS.Accesos.Asignaciones
{
    public partial class ResetPassw : Form
    {
        string cvuser;
        string susu;
        int istusu;

        Usuario Usu = new Usuario();
        Utilerias utilerias = new Utilerias();
        Usuario usu = new Usuario();

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
            //variables accesos
            DataTable dtusuario = Usu.irstpwd(20,txtcvusuarios.Text.Trim(),"");

            if (dtusuario.Rows.Count >= 1)
            {
                istusu = Int32.Parse(dtusuario.Rows[0][3].ToString());
            }
            else
            {
                istusu = 0;
            }
            

            if (dtusuario.Rows.Count <= 0)
            {
                DialogResult result = MessageBox.Show("El usuario que busca no existe, verificar", "SIPAA", MessageBoxButtons.OK);
                txtusuario.Text = "";
                txtestatus.Text = "";
                txtcvusuarios.Focus();
                btguardar.Enabled = false;
            }
            else if (istusu == 0)
            {
                DialogResult result = MessageBox.Show("El usuario esta inactivo, no se le puede restaurar la contraseña, verificar", "SIPAA", MessageBoxButtons.OK);
                txtusuario.Text = dtusuario.Rows[0][2].ToString();
                txtestatus.Text = dtusuario.Rows[0][4].ToString();

            }
            else
            {
                cvuser = dtusuario.Rows[0][0].ToString();
                txtusuario.Text = dtusuario.Rows[0][2].ToString();
                susu = dtusuario.Rows[0][2].ToString();
                txtestatus.Text = dtusuario.Rows[0][4].ToString();
                btguardar.Enabled = true;
                btguardar.Focus();

            }
            
        }

        private void btguardar_Click(object sender, EventArgs e)
        {

            //restaura contraseña
            DialogResult result = MessageBox.Show( LoginInfo.Nombre +": esta acción restaura la contraseña del usuario;" + "\r\n" + "\r\n" + susu + "\r\n" + "\r\n" + "¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int ivalida = usu.iactpw(18, cvuser, utilerias.cifradoMd5(cvuser),LoginInfo.IdTrab, this.Name);

                if (ivalida == 2)
                {
                    txtcvusuarios.Text = "";
                    txtusuario.Text = "";
                    txtestatus.Text = "";
                    btguardar.Enabled = false;
                    DialogResult result1 = MessageBox.Show("Contraseña Modificada con exito!", "SIPAA", MessageBoxButtons.OK);
                    txtcvusuarios.Focus();
                }
            }
            else
            {
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            //toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
    }
}
