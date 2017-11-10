using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;

using SIPAA_CS.RecursosHumanos;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Accesos;


//***********************************************************************************************
//Autor: Jaime Avendaño Vargas        noe alvarez marquina              Jose Luis Alvarez D
//Fecha creación: 04-Abril-2017       Última Modificacion: 20/09/2017   30/10/2017
//Descripción: Reasignación de Supervisor y Director
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class ReasignaSupyDirectores : Form
    {

        ReasignaSupyDirector Resupdir = new ReasignaSupyDirector();

        SonaFormaPago oSonaFormaPago = new SonaFormaPago();
        Utilerias Util = new Utilerias();
        Perfil DatPerfil = new Perfil();

        public ReasignaSupyDirectores()
        {
            InitializeComponent();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Util.p_inicbo == 0)
            {
                // obtiene fechas de periodo
                DataTable dtfechas = Resupdir.dtfecperiodo(4, Int32.Parse(cbFormaPago.SelectedValue.ToString()), LoginInfo.IdTrab, this.Name);
                TxtFeIni.Text = dtfechas.Rows[0][2].ToString();
                TxtFeFin.Text = dtfechas.Rows[0][3].ToString();

                if (cbFormaPago.SelectedValue.ToString() == "0" || cbFormaPago.Text == "" || TxtFeIni.Text.Trim() == "" || TxtFeFin.Text.Trim() == "")
                {
                    DialogResult result = MessageBox.Show("Selecciona un periodo", "SIPAA", MessageBoxButtons.OK);
                }
                else
                {
                    cbtrab();
                }
            }
        }

        private void cbempleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Util.p_inicbo == 0)
            {
                cbsupervisor.DataSource = null;
                DataTable dtsup = Resupdir.dttrabsupdir(6, TxtFeIni.Text.Trim(), TxtFeFin.Text.Trim(), Int32.Parse(cbempleado.SelectedValue.ToString()), LoginInfo.IdTrab, this.Name,0);
                Util.cargarcombo(cbsupervisor, dtsup);

                cbdirector.DataSource = null;
                DataTable dtdir = Resupdir.dttrabsupdir(7, TxtFeIni.Text.Trim(), TxtFeFin.Text.Trim(), Int32.Parse(cbempleado.SelectedValue.ToString()), LoginInfo.IdTrab, this.Name,0);
                Util.cargarcombo(cbdirector, dtdir);

                //llena cb nuevo supervisor y director
                cbsupervnuevo.DataSource = null;
                DataTable dtsupnew = Resupdir.dttrabsupdir(8,"","",0, LoginInfo.IdTrab, this.Name,0);
                Utilerias.llenarComboxDataTable(cbsupervnuevo, dtsupnew, "IdTrab", "supdir");

                cbdirecnuevo.DataSource = null;
                DataTable dtdirnew = Resupdir.dttrabsupdir(8, "", "", 0, LoginInfo.IdTrab, this.Name,0);
                Utilerias.llenarComboxDataTable(cbdirecnuevo, dtdirnew, "IdTrab", "supdir");

                fllenagrid(10, TxtFeIni.Text.Trim(), TxtFeFin.Text.Trim(), Int32.Parse(cbempleado.SelectedValue.ToString()));
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //Boton minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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
                // No hace nada, se queda en la pantalla
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            //valida campos
            Boolean bvalidacampos = fvalidacampos();

            if (bvalidacampos == true)
            {
                //variable supervisor nuevo
                int isupnew;
                if (cbsupervnuevo.Text.Trim() == "" || cbsupervnuevo.SelectedIndex == -1 || cbsupervnuevo.SelectedIndex == 0)
                {
                    isupnew = 0;
                }
                else
                {
                    isupnew = Int32.Parse(cbsupervnuevo.SelectedValue.ToString());
                }

                //actualiza datos
                int iresp = Resupdir.actsupdir(2, Int32.Parse(cbempleado.SelectedValue.ToString()), TxtFeIni.Text.Trim(), TxtFeFin.Text.Trim(), isupnew, Int32.Parse(cbdirecnuevo.SelectedValue.ToString()), LoginInfo.IdTrab, this.Name, 0);

                if (iresp == 2)
                {
                    Util.p_inicbo = 1;
                    dgvdatos.DataSource = null;
                    cbdirecnuevo.DataSource = null;
                    cbsupervnuevo.DataSource = null;
                    cbdirector.DataSource = null;
                    cbsupervisor.DataSource = null;
                    cbempleado.DataSource = null;
                    TxtFeFin.Text = "";
                    TxtFeIni.Text = "";
                    cbFormaPago.DataSource = null;
                    //llena combo con formas de pago
                    fllenacbformpago();
                    cbFormaPago.Focus();
                    panelTag.Visible = true;
                    panelTag.BackColor = ColorTranslator.FromHtml("#0277bd");
                    lblMensaje.Text = "Registro Actualizado Correctamente";
                    timer1.Start();
                    Util.p_inicbo = 0;
                    frecargar();
                }
                else
                {
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

        private void ReasignaSupyDirectores_Load(object sender, EventArgs e)
        {
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }

            //variables accesos
            DataTable Permisos = DatPerfil.accpantalla(LoginInfo.IdTrab, this.Name);
            int iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            int iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            int ielim = Int32.Parse(Permisos.Rows[0][5].ToString());

            //tool tip
            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            if (iins == 1 || iact == 1 || ielim == 1)
            {
                btnInsertar.Enabled = true;
            }
            else
            {
                btnInsertar.Enabled = false;
            }

            int icalperact = Resupdir.actsupdir(12,0, "", "", 0,0, LoginInfo.IdTrab, this.Name, 0);

            if (icalperact <= 0)
            {
                DialogResult result = MessageBox.Show("No existe ningún periodo activo", "SIPAA", MessageBoxButtons.OK);
                cbFormaPago.Enabled = false;
            }
            else
            {
                //llena combo con formas de pago
                fllenacbformpago();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
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
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnInsertar, "Insertar Registros");
        }

        // Llena Combo Box de Forma Pago de la tabla de SONARH
        private void fllenacbformpago()
        {
            Util.p_inicbo = 1;
            DataTable dtFormaPago = oSonaFormaPago.FormaPago_S(4, 0, "");
            Utilerias.llenarComboxDataTable(cbFormaPago, dtFormaPago, "Clave", "Descripción");
            Util.p_inicbo = 0;
        }

        public void cbtrab()
        {
            Util.p_inicbo = 1;
            DataTable dttrab = Resupdir.dttrabsupdir(5, TxtFeIni.Text, TxtFeFin.Text, 0, LoginInfo.IdTrab, this.Name, Int32.Parse(cbFormaPago.SelectedValue.ToString()));
            Utilerias.llenarComboxDataTable(cbempleado, dttrab, "idtrab", "trab");
            Util.p_inicbo = 0;
        }

        public void fllenagrid(int iopc, string sfecini, string sfecfin, int iintrab)
        {
            DataTable dttrab = Resupdir.dttrabsupdir(10, sfecini, sfecfin, iintrab, LoginInfo.IdTrab, this.Name, 0);
            dgvdatos.DataSource = dttrab;

            dgvdatos.Columns[0].Visible = false;//empleado
            dgvdatos.Columns[1].Width = 130;//fecha registro
            dgvdatos.Columns[2].Width = 110;//hr registro
            dgvdatos.Columns[3].Width = 110;//hr salida
            dgvdatos.Columns[4].Width = 100;//dif tiempo
            dgvdatos.ClearSelection();
        }

        //validacion de campos
        private Boolean fvalidacampos()
        {
            if (cbFormaPago.Text.Trim() == "" || cbFormaPago.SelectedIndex == -1 || cbFormaPago.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un periodo", "SIPAA", MessageBoxButtons.OK);
                cbFormaPago.Focus();
                return false;
            }
            else if (cbempleado.Text.Trim() == "" || cbempleado.SelectedIndex == -1 || cbempleado.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un empleado", "SIPAA", MessageBoxButtons.OK);
                cbempleado.Focus();
                return false;
            }
            else if (cbdirecnuevo.Text.Trim() == "" || cbdirecnuevo.SelectedIndex == -1 || cbdirecnuevo.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecione Director nuevo", "SIPAA", MessageBoxButtons.OK);
                cbdirecnuevo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void frecargar()
        {
            ReasignaSupyDirectores recargar = new ReasignaSupyDirectores();
            recargar.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
