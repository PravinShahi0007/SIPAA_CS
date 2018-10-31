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
//Fecha creación:10-jul-aaaa       Última Modificacion: dd-mm-aaaa
//Descripción: -------------------------------
//***********************************************************************************************


namespace SIPAA_CS.Accesos
{
    public partial class RecuperaContrasena : Form
    {

        Usuarioap cusuarioap = new Usuarioap();
        Utilerias cutilerias = new Utilerias();

        string smail, sdominio;

        public RecuperaContrasena()
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

        //bton reresar
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Acceso facceso = new Acceso();
            facceso.Show();
            this.Close();
        }

        //boton cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

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

        //boton recuperar contraseña
        private void btnreccontrasena_Click(object sender, EventArgs e)
        {
            //valida formato de correo electronico
            string mail = txtUsuario.Text.Trim();
            bool verificar = mail.Contains("@");

            //valida que esista el correo
            DataTable dtaccesos = cusuarioap.dtdatos(14, "", 0, "", txtUsuario.Text.Trim(), 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            int existe = Int32.Parse(dtaccesos.Rows[0][0].ToString());

            //valida informacion correcta
            if (verificar == false)
            {
                MessageBox.Show("Ingresa un correo electrónico valido", "SIPAA", MessageBoxButtons.OK);
                txtUsuario.Focus();
            }
            else if (existe == 0)
            {
                MessageBox.Show("Correo electrónico inexistente", "SIPAA", MessageBoxButtons.OK);
                txtUsuario.Focus();
            }
            else
            {
                int ivalida = cusuarioap.cruddatos(12, "", 0, "", txtUsuario.Text.Trim(),
                                                   0, "", 0, 1, "",
                                                   "", "", "", "", "",
                                                   0, 4, txtUsuario.Text.Trim(), "", this.Name,
                                                   cutilerias.scontrol());

                if (ivalida == 2)
                {

                    txtUsuario.Text = "";

                    DialogResult result1 = MessageBox.Show("En breve recibira un correo electrónico con su contraseña temporal.", "SIPAA", MessageBoxButtons.OK);

                    Acceso frm = new Acceso();
                    this.Hide();
                    frm.Show();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void RecuperaContrasena_Load(object sender, EventArgs e)
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
        }

        //timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblalert.Visible = false;
            ckbaut.Checked = false;
            timer1.Stop();
            txtUsuario.Focus();
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
            toolTip1.ReshowDelay  = 500;
            toolTip1.ShowAlways   = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
        }

        private void ckbaut_CheckedChanged(object sender, EventArgs e)
        {
            fobtdatos();
        }

        private void fobtdatos()
        {
            if (ckbaut.Checked == true)
            {
                //variables datos del usuario
                DataTable datosusuario = cusuarioap.dtdatos(4, txtUsuario.Text.Trim(), 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");

                if (datosusuario.Rows.Count >= 1)
                {
                    string scorreo, sdominio;
                    scorreo = datosusuario.Rows[0][3].ToString();
                    sdominio = datosusuario.Rows[0][9].ToString();

                    if (scorreo == "" || sdominio == "")
                    {
                        timer1.Start();

                        lblalert.Visible = true;
                        lblalert.Text = "No se cuenta con los suficientes datos para recuperar tu contraseña";
                    }
                    else
                    {

                        lblalert.Visible = false;
                        lblcorreo.Visible = true;
                        txtcorreo.Visible = true;
                        txtcorreo.Enabled = false;
                        pnlcorreo.Visible = true;
                        lblarrb.Visible = true;
                        txtdominio.Visible = true;
                        txtdominio.Enabled = false;
                        pnldominio.Visible = true;
                        btnreccontrasena.Visible = true;
                        btnreccontrasena.Enabled = true;

                        txtcorreo.Text = datosusuario.Rows[0][3].ToString();
                        txtdominio.Text = datosusuario.Rows[0][9].ToString();
                    }
                }
                else
                {
                    timer1.Start();
                    lblalert.Visible = true;
                    lblalert.Text = "No se cuenta con los suficientes datos para recuperar tu contraseña";
                }

            }
            else
            {
                lblalert.Visible = false;
                txtcorreo.Text = "";
                txtdominio.Text = "";
                lblcorreo.Visible = false;
                txtcorreo.Visible = false;
                pnlcorreo.Visible = false;
                lblarrb.Visible = false;
                txtdominio.Visible = false;
                pnldominio.Visible = false;
                btnreccontrasena.Visible = false;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
