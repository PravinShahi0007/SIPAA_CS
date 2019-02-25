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
using SIPAA_CS.RecursosHumanos.Reportes;
using SIPAA_CS.Properties;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;

using CrystalDecisions.CrystalReports.Engine;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:11-sep-2018       Última Modificacion: dd-mm-aaaa
//Descripción: administra sanciones creadas automaticamente
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class SancionesProcesos : Form
    {

        //clases
        #region
        int iins, iact, ielim, iimp;
        int icompaniab, iplantab, iubicacionb;
        #endregion

        Perfil cperfil = new Perfil();
        Utilerias cutilerias = new Utilerias();
        SancionesProceso scancionesproceso = new SancionesProceso();

        public SancionesProcesos()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        private void cbempleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cutilerias.p_inicbo == 1)
            {
                //refresca pantalla
                flimpiapendrh();

                //llena sanciones
                cutilerias.p_inicbo = 0;
                DataTable dtcbsanciones = scancionesproceso.dtdatos(6, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                Utilerias.llenarComboxDataTable(cbsancion, dtcbsanciones, "cv", "desc");
                cutilerias.p_inicbo = 1;
            }
        }

        private void cboreporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cutilerias.p_inicbo == 1)
            {
                if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 1)//reporte global
                {
                    lblrepmes.Visible = false;
                    cbomes.Visible = false;
                    lblcampania.Visible = false;
                    cbocompania.Visible = false;
                    lblPlanta.Visible = false;
                    cboplanta.Visible = false;
                    lblubicacion.Visible = false;
                    cboubicacion.Visible = false;
                    //obtiene ciclo activo
                    DataTable dtcicloactivo = scancionesproceso.dtdatos(13, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    string scicloactivo = dtcicloactivo.Rows[0][1].ToString();

                    cutilerias.p_inicbo = 0;
                    DataTable dtcbcatciclosesc = scancionesproceso.dtdatos(12, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    Utilerias.llenarComboxDataTable(cbociclo, dtcbcatciclosesc, "cv", "desc");
                    cutilerias.p_inicbo = 1;

                    cbociclo.Text = scicloactivo;

                    dgvrepsanciones.DataSource = null;
                }
                else if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 2)
                {
                    cutilerias.p_inicbo = 0;
                    lblrepmes.Visible = true;
                    cbomes.Visible = true;
                    cbomes.DataSource = null;
                    lblcampania.Visible = true;
                    cbocompania.Visible = true;
                    cbocompania.DataSource = null;
                    lblPlanta.Visible = true;
                    cboplanta.Visible = true;
                    cboplanta.DataSource = null;
                    lblubicacion.Visible = true;
                    cboubicacion.Visible = true;
                    cboubicacion.DataSource = null;

                    //obtiene ciclo activo
                    DataTable dtcicloactivo = scancionesproceso.dtdatos(13, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    string scicloactivo = dtcicloactivo.Rows[0][1].ToString();

                    
                    DataTable dtcbcatciclosesc = scancionesproceso.dtdatos(12, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    Utilerias.llenarComboxDataTable(cbociclo, dtcbcatciclosesc, "cv", "desc");
                    cutilerias.p_inicbo = 1;

                    //cbociclo.Text = scicloactivo;

                }
                else if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 3)//reporte general extrañamiento y suspensiones
                {
                    lblrepmes.Visible = false;
                    cbomes.Visible = false;
                    lblcampania.Visible = false;
                    cbocompania.Visible = false;
                    lblPlanta.Visible = false;
                    cboplanta.Visible = false;
                    lblubicacion.Visible = false;
                    cboubicacion.Visible = false;
                    //obtiene ciclo activo
                    DataTable dtcicloactivo = scancionesproceso.dtdatos(13, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    string scicloactivo = dtcicloactivo.Rows[0][1].ToString();

                    cutilerias.p_inicbo = 0;
                    DataTable dtcbcatciclosesc = scancionesproceso.dtdatos(12, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    Utilerias.llenarComboxDataTable(cbociclo, dtcbcatciclosesc, "cv", "desc");
                    cutilerias.p_inicbo = 1;

                    cbociclo.Text = scicloactivo;

                    dgvrepsanciones.DataSource = null;
                }
                else
                {
                }


            }
        }

        //cbo ciclo reporte mensual
        private void cbociclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvrepsanciones.DataSource = null;

            if (Int32.Parse(cboreporte.SelectedValue.ToString())==2)
            {

                //cutilerias.p_inicbo = 0;
                //cbocompania.DataSource = null;
                //cboplanta.DataSource = null;
                //cboubicacion.DataSource = null;
                //cutilerias.p_inicbo = 1;

                if (cutilerias.p_inicbo == 1)
                {
                    //llena sanciones
                    cutilerias.p_inicbo = 0;
                    DataTable dtmes = scancionesproceso.dtdatos(16, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    Utilerias.llenarComboxDataTable(cbomes, dtmes, "cv", "desc");

                    cbocompania.DataSource = null;
                    cboplanta.DataSource = null;
                    cboubicacion.DataSource = null;

                    cutilerias.p_inicbo = 1;
                }
            }
        }

        //cbo mes
        private void cbomes_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvrepsanciones.DataSource = null;

            if (cutilerias.p_inicbo == 1)
            {
                //llena sanciones
                cutilerias.p_inicbo = 0;
                DataTable dtcompania = scancionesproceso.dtdatos(17, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, Int32.Parse(cbomes.SelectedValue.ToString()), 0, 0, 0, "", 0, "", "", "", "", "");
                Utilerias.llenarComboxDataTable(cbocompania, dtcompania, "cv", "desc");

                cboplanta.DataSource = null;
                cboubicacion.DataSource = null;

                cutilerias.p_inicbo = 1;
            }
        }

        //cbo compañia
        private void cbocompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvrepsanciones.DataSource = null;

            if (cutilerias.p_inicbo == 1)
            {
                cboubicacion.DataSource = null;
                //llena sanciones
                cutilerias.p_inicbo = 0;
                DataTable dtplanta = scancionesproceso.dtdatos(18, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, Int32.Parse(cbomes.SelectedValue.ToString()), Int32.Parse(cbocompania.SelectedValue.ToString()), 0, 0, "", 0, "", "", "", "", "");
                Utilerias.llenarComboxDataTable(cboplanta, dtplanta, "cv", "desc");

                cboubicacion.DataSource = null;

                cutilerias.p_inicbo = 1;
            }
        }

        //cbo planta
        private void cboplanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvrepsanciones.DataSource = null;

            if (cutilerias.p_inicbo == 1)
            {
                //llena sanciones
                cutilerias.p_inicbo = 0;
                DataTable dtubica = scancionesproceso.dtdatos(19, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, Int32.Parse(cbomes.SelectedValue.ToString()), Int32.Parse(cbocompania.SelectedValue.ToString()), 0, Int32.Parse(cboplanta.SelectedValue.ToString()), "", 0, "", "", "", "", "");
                Utilerias.llenarComboxDataTable(cboubicacion, dtubica, "cv", "desc");
                cutilerias.p_inicbo = 1;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton minimizar
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btncerrar_Click(object sender, EventArgs e)
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

        //botón regresar
        private void btnregresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void btninsertar_Click(object sender, EventArgs e)
        {
            if (cbempleado.Text.Trim() == "" || cbempleado.SelectedIndex == -1 || cbempleado.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona una empleado", "SIPAA", MessageBoxButtons.OK);
                cbempleado.Focus();
            }
            else if (cbsancion.Text.Trim() == "" || cbsancion.SelectedIndex == -1 || cbsancion.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Selecciona una sanción", "SIPAA", MessageBoxButtons.OK);
                cbsancion.Focus();
            }
            else if (opcaplicar.Checked == false && opccancelar.Checked == false)
            {
                DialogResult result = MessageBox.Show("Seleccione una acción APLICAR o CANCELAR", "SIPAA", MessageBoxButtons.OK);
            }
            else
            {
                if (Int32.Parse(cbsancion.SelectedValue.ToString()) == 19 && opcaplicar.Checked == true)//autorizar
                {
                    string sobsaut;
                    if (txtobs.Text == "")
                    {
                        sobsaut = "0";
                    }
                    else
                    {
                        sobsaut = txtobs.Text.Trim();
                    }

                    int ivalcancel = scancionesproceso.cruddatos(10, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "null", 8, sobsaut, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

                    pnlmenssuid.Visible = true;
                    pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                    menssuid.Text = "Sanción autorizada correctamente";
                    timer1.Start();

                    //llena empleados
                    cutilerias.p_inicbo = 0;
                    DataTable dtcbempleado = scancionesproceso.dtdatos(5, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    Utilerias.llenarComboxDataTable(cbempleado, dtcbempleado, "cv", "empleado");
                    cutilerias.p_inicbo = 1;
                    flimpiapendrh();
                    fest();
                    cbempleado.Focus();


                }
                else if (opcaplicar.Checked == true)
                {
                    string sobsaut;
                    if (txtobs.Text == "")
                    {
                        sobsaut = "0";
                    }
                    else
                    {
                        sobsaut = txtobs.Text.Trim();
                    }

                    int ivalcancel = scancionesproceso.cruddatos(10, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "null", 3, sobsaut, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

                    pnlmenssuid.Visible = true;
                    pnlmenssuid.BackColor = ColorTranslator.FromHtml("#2e7d32");
                    menssuid.Text = "Sanción autorizada correctamente";
                    timer1.Start();

                    //llena empleados
                    cutilerias.p_inicbo = 0;
                    DataTable dtcbempleado = scancionesproceso.dtdatos(5, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                    Utilerias.llenarComboxDataTable(cbempleado, dtcbempleado, "cv", "empleado");
                    cutilerias.p_inicbo = 1;
                    flimpiapendrh();
                    fest();
                    cbempleado.Focus();
                }
                else if (opccancelar.Checked == true)//cancelar
                {
                    //valida campos
                    Boolean bvalidacampos = fvalidacamposcancela();

                    if (bvalidacampos == true)
                    {
                        int ivalcancel = scancionesproceso.cruddatos(10, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "null", 2, txtobs.Text, LoginInfo.cvusuario, "", this.Name, cutilerias.scontrol());

                        if (ivalcancel == 2)
                        {
                            pnlmenssuid.Visible = true;
                            pnlmenssuid.BackColor = ColorTranslator.FromHtml("#f44336");
                            menssuid.Text = "Sanción cancelada correctamente";
                            timer1.Start();

                            //llena empleados
                            cutilerias.p_inicbo = 0;
                            DataTable dtcbempleado = scancionesproceso.dtdatos(5, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
                            Utilerias.llenarComboxDataTable(cbempleado, dtcbempleado, "cv", "empleado");
                            cutilerias.p_inicbo = 1;
                            flimpiapendrh();
                            fest();
                            cbempleado.Focus();
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("No se agrego su registro", "SIPAA", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        //botón buscar
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (cbempleado.Text.Trim() == "" || cbempleado.SelectedIndex == -1 || cbempleado.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un empleado", "SIPAA", MessageBoxButtons.OK);
                cbempleado.Focus();
            }
            else if (cbsancion.Text.Trim() == "" || cbsancion.SelectedIndex == -1 || cbsancion.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione una sancion", "SIPAA", MessageBoxButtons.OK);
                cbsancion.Focus();
            }
            else
            {

                //dt registros sancion
                DataTable dtsanciones = scancionesproceso.dtdatos(7, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 00, 0, 0, "", 0, "", "", "", "", "");
                dgvdatos.DataSource = dtsanciones;

                //variables politica/inf trabajador
                DataTable dtdatemplsancion = scancionesproceso.dtdatos(8, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 00, 0, 0, "", 0, "", "", "", "", "");
                string sdatemple = dtdatemplsancion.Rows[0][0].ToString();
                string sdatsanc = dtdatemplsancion.Rows[0][1].ToString();

                txtdatempleado.Text = sdatemple;
                txtdatsancion.Text = sdatsanc;

                dgvdatos.Columns[0].HeaderText = "Num";//numero
                dgvdatos.Columns[0].Width = 40;
                dgvdatos.Columns[1].HeaderText = "Fecha";//fecha
                dgvdatos.Columns[1].Width = 75;
                dgvdatos.Columns[2].HeaderText = "Incidencia";//incidencia
                dgvdatos.Columns[2].Width = 150;
                dgvdatos.Columns[3].HeaderText = "Tiempo empleado";//tiempo empleado
                dgvdatos.Columns[3].Width = 80;
                dgvdatos.Columns[4].HeaderText = "Tiempo profesor";//tiempo profesor
                dgvdatos.Columns[4].Width = 80;
                dgvdatos.ClearSelection();

                //dt obs sancion
                DataTable dtobssanciones = scancionesproceso.dtdatos(9, Int32.Parse(cbempleado.SelectedValue.ToString()), 0, 0, 0, 00, 0, 0, "", 0, "", "", "", "", "");
                dgvsegsanciones.DataSource = dtobssanciones;

                dgvsegsanciones.Columns[0].HeaderText = "Fecha";//fecha
                dgvsegsanciones.Columns[0].Width = 140;
                dgvsegsanciones.Columns[1].HeaderText = "Usuario";//usuario
                dgvsegsanciones.Columns[01].Width = 130;
                dgvsegsanciones.Columns[2].HeaderText = "Estatus";//estatus
                dgvsegsanciones.Columns[2].Width = 100;
                dgvsegsanciones.Columns[3].HeaderText = "Tiempo empleado";//obs
                dgvsegsanciones.Columns[3].Width = 170;
                dgvsegsanciones.ClearSelection();

                int inumreg = dgvdatos.RowCount;

                if (inumreg >= 1)
                {
                    btninsertar.Enabled = true;
                }
                else
                {
                    btninsertar.Enabled = false;
                }

            }
        }

        //boton buscar reportes
        private void btnrepbusqueda_Click(object sender, EventArgs e)
        {
            //valida campos
            Boolean bvalidacampos = fvalidacamposrep();

            if (bvalidacampos == true)
            {
                if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 1)//reporte global
                {
                    //valida campos
                    Boolean bvalcamposrepglobal = fvalidacamposrepglobal();
                    if (bvalcamposrepglobal == true)
                    {
                        fllenagrid(14, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                    }

                }
                else if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 2)//reporte mensual
                {
                    //valida campos
                    Boolean bvalcamposrepglobal = fvalidacamposrepmensual();
                    if (bvalcamposrepglobal == true)
                    {
                        //var compañia
                        if (cbocompania.Text.Trim() == "" || cbocompania.SelectedIndex == -1 || cbocompania.SelectedIndex == 0)
                        {
                            icompaniab = 0;
                        }
                        else
                        {
                            icompaniab = Int32.Parse(cbocompania.SelectedValue.ToString());
                        }
                        //var planta
                        if (cboplanta.Text.Trim() == "" || cboplanta.SelectedIndex == -1 || cboplanta.SelectedIndex == 0)
                        {
                            iplantab = 0;
                        }
                        else
                        {
                            iplantab = Int32.Parse(cboplanta.SelectedValue.ToString());
                        }
                        //var ubicacion
                        if (cboubicacion.Text.Trim() == "" || cboubicacion.SelectedIndex == -1 || cboubicacion.SelectedIndex == 0)
                        {
                            iubicacionb = 0;
                        }
                        else
                        {
                            iubicacionb = Int32.Parse(cboubicacion.SelectedValue.ToString());
                        }

                        fllenagrid(20, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, Int32.Parse(cbomes.SelectedValue.ToString()), icompaniab, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                    }
                }
                else if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 3)//Reporte general extrañamiento y suspensiones
                {
                    //valida campos
                    Boolean bvalcamposrepglobal = fvalidacamposrepglobal();
                    if (bvalcamposrepglobal == true)
                    {
                        fllenagrid(15, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                    }
                }
                else
                {
                }
            }
        }

        //boton imprimir reporte
        private void btnImprimirrep_Click(object sender, EventArgs e)
        {
            //valida campos
            Boolean bvalidacampos = fvalidacamposrep();

            if (bvalidacampos == true)
            {
                if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 1)//reporte global
                {
                    //valida campos
                    Boolean bvalcamposrepglobal = fvalidacamposrepglobal();
                    if (bvalcamposrepglobal == true)
                    {
                        fllenagrid(14, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                        freportes(14, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                    }

                }
                else if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 2)//
                {
                    //var compañia
                    if (cbocompania.Text.Trim() == "" || cbocompania.SelectedIndex == -1 || cbocompania.SelectedIndex == 0)
                    {
                        icompaniab = 0;
                    }
                    else
                    {
                        icompaniab = Int32.Parse(cbocompania.SelectedValue.ToString());
                    }
                    //var planta
                    if (cboplanta.Text.Trim() == "" || cboplanta.SelectedIndex == -1 || cboplanta.SelectedIndex == 0)
                    {
                        iplantab = 0;
                    }
                    else
                    {
                        iplantab = Int32.Parse(cboplanta.SelectedValue.ToString());
                    }
                    //var ubicacion
                    if (cboubicacion.Text.Trim() == "" || cboubicacion.SelectedIndex == -1 || cboubicacion.SelectedIndex == 0)
                    {
                        iubicacionb = 0;
                    }
                    else
                    {
                        iubicacionb = Int32.Parse(cboubicacion.SelectedValue.ToString());
                    }

                    fllenagrid(20, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, Int32.Parse(cbomes.SelectedValue.ToString()), icompaniab, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                    freportes(20, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, Int32.Parse(cbomes.SelectedValue.ToString()), icompaniab, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));

                }
                else if (Int32.Parse(cboreporte.SelectedValue.ToString()) == 3)//reporte general extrañamiento y suspensiones
                {
                    //valida campos
                    Boolean bvalcamposrepglobal = fvalidacamposrepglobal();
                    if (bvalcamposrepglobal == true)
                    {
                        fllenagrid(15, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
                        freportes(15, Int32.Parse(cbociclo.SelectedValue.ToString()), 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "", Int32.Parse(cboreporte.SelectedValue.ToString()));
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

        // cargar
        private void SancionesProcesos_Load(object sender, EventArgs e)
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

            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());


            //inicializa tool tip
            ftooltip();

            txtdatempleado.Text = "";

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //variables accesos
            DataTable Permisos = cperfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            ielim = Int32.Parse(Permisos.Rows[0][5].ToString());

            opcaplicar.Checked = false;
            pnlpendrh.Visible = false;

            fest();
        }

        private void opcpendrh_CheckedChanged(object sender, EventArgs e)
        {
            pnlpendrh.Visible = true;
            pnlconsulta.Visible = false;
            pnlrep.Visible = false;

            lblrepmes.Visible = false;
            cbomes.Visible = false;
            lblcampania.Visible = false;
            cbocompania.Visible = false;
            lblPlanta.Visible = false;
            cboplanta.Visible = false;
            lblubicacion.Visible = false;
            cboubicacion.Visible = false;

            //llena empleados
            cutilerias.p_inicbo = 0;
            DataTable dtcbempleado = scancionesproceso.dtdatos(5, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
            Utilerias.llenarComboxDataTable(cbempleado, dtcbempleado, "cv", "empleado");
            cutilerias.p_inicbo = 1;

            //refresca
            flimpiapendrh();
            fest();
        }

        private void opcconsulta_CheckedChanged(object sender, EventArgs e)
        {
            pnlpendrh.Visible = false;
            pnlconsulta.Visible = true;
            pnlrep.Visible = false;
            lblrepmes.Visible = false;
            cbomes.Visible = false;
            fest();
        }

        //opcion reportes
        private void opcreportes_CheckedChanged(object sender, EventArgs e)
        {
            pnlpendrh.Visible = false;
            pnlconsulta.Visible = false;
            pnlrep.Visible = true;

            lblrepmes.Visible = false;
            cbomes.Visible = false;
            lblrepmes.Visible = false;
            cbomes.Visible = false;
            lblcampania.Visible = false;
            cbocompania.Visible = false;
            lblPlanta.Visible = false;
            cboplanta.Visible = false;
            lblubicacion.Visible = false;
            cboubicacion.Visible = false;

            //llama funciones
            fcbocatreportes();

            cbociclo.DataSource = null;
        }

        //timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlmenssuid.Visible = false;
            timer1.Stop();
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
            toolTip1.SetToolTip(this.btncerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");

        }

        //validacion de campos cancelar
        private Boolean fvalidacamposcancela()
        {
            if (txtobs.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Captura el motivo de la cancelación", "SIPAA", MessageBoxButtons.OK);
                txtobs.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //limpia pendientes RH
        private void flimpiapendrh()
        {
            cbsancion.DataSource = null;
            txtdatempleado.Text = "";
            txtdatsancion.Text = "";
            opcaplicar.Checked = false;
            opccancelar.Checked = false;
            txtobs.Text = "";
            dgvdatos.DataSource = null;
            dgvsegsanciones.DataSource = null;
            btninsertar.Enabled = false;
        }

        //limpia pantalla despues de guardar sancion RH
        private void fest()
        {
            //variables politica/inf trabajador
            DataTable dtestad = scancionesproceso.dtdatos(4, 0, 0, 0, 0, 00, 0, 0, "", 0, "", "", "", "", "");
            string totalsanc = dtestad.Rows[0][0].ToString();
            string pendrh = dtestad.Rows[0][1].ToString();
            string cancelrh = dtestad.Rows[0][2].ToString();
            string penddir = dtestad.Rows[0][3].ToString();

            lblesttot.Text = totalsanc;
            lblestpendrh.Text = pendrh;
            lblestcancelrh.Text = cancelrh;
            lblestpenddir.Text = penddir;
        }

        //carga combo reportes
        private void fcbocatreportes()
        {
            cutilerias.p_inicbo = 0;
            DataTable dtcbcatreportes = scancionesproceso.dtdatos(11, 0, 0, 0, 0, 0, 0, 0, "", 0, "", "", "", "", "");
            Utilerias.llenarComboxDataTable(cboreporte, dtcbcatreportes, "cv", "desc");
            cutilerias.p_inicbo = 1;
        }

        //validacion de campos reportes
        private Boolean fvalidacamposrep()
        {
            if (cboreporte.Text.Trim() == "" || cboreporte.SelectedIndex == -1 || cboreporte.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un reporte", "SIPAA", MessageBoxButtons.OK);
                cboreporte.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //validacion reporte global
        private Boolean fvalidacamposrepglobal()
        {
            if (cbociclo.Text.Trim() == "" || cbociclo.SelectedIndex == -1 || cbociclo.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un ciclo", "SIPAA", MessageBoxButtons.OK);
                cbociclo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //valida campos reporte mensual
        private Boolean fvalidacamposrepmensual()
        {
            if (cbociclo.Text.Trim() == "" || cbociclo.SelectedIndex == -1 || cbociclo.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un ciclo", "SIPAA", MessageBoxButtons.OK);
                cbociclo.Focus();
                return false;
            }
            else if (cbomes.Text.Trim() == "" || cbomes.SelectedIndex == -1 || cbomes.SelectedIndex == 0)
            {
                DialogResult result = MessageBox.Show("Seleccione un mes", "SIPAA", MessageBoxButtons.OK);
                cbomes.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        //llena grid conforme a reporte seleccionado
        private void fllenagrid(int iopcion, int icvsancion, int iidtrab, int iidtrabdir, int icvincidenciagen,
                                int icvtipogen, int icvperiodo, int icvpolitica, string sfsuspension, int istsanción,
                                string sobssanción, string susumodifst, string sfhumodst, string sprgumodst, string sequmodst,
                                int ireporte)
        {
            if (ireporte == 1)
            {
                //dt registros sancion
                DataTable dtrepglobal = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                int inumregrepglobal = dtrepglobal.Rows.Count;

                if (inumregrepglobal >= 1)
                {
                    dgvrepsanciones.DataSource = dtrepglobal;

                    dgvrepsanciones.Columns[0].Visible = false;
                    dgvrepsanciones.Columns[1].Visible = false;
                    dgvrepsanciones.Columns[2].HeaderText = "Estatus";//incidencia
                    dgvrepsanciones.Columns[2].Width = 400;
                    dgvrepsanciones.Columns[3].HeaderText = "Sanciones";//tiempo empleado
                    dgvrepsanciones.Columns[3].Width = 100;
                    dgvrepsanciones.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.ClearSelection();
                }
                else
                {
                    DialogResult result = MessageBox.Show("No existen registros para el ciclo seleccionado", "SIPAA", MessageBoxButtons.OK);
                }
            }
            else if (ireporte == 2)
            {
                //dt registros reporte mensual
                DataTable dtrepglobal = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                int inumregrepmensual = dtrepglobal.Rows.Count;

                if (inumregrepmensual >= 1)
                {
                    dgvrepsanciones.DataSource = dtrepglobal;

                    dgvrepsanciones.Columns[0].HeaderText = "No Emp";//numero empleado
                    dgvrepsanciones.Columns[0].Width = 70;
                    dgvrepsanciones.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[1].HeaderText = "Nombre";//nombre
                    dgvrepsanciones.Columns[1].Width = 240;
                    dgvrepsanciones.Columns[2].HeaderText = "Puesto";//puesto
                    dgvrepsanciones.Columns[2].Width = 160;
                    dgvrepsanciones.Columns[3].HeaderText = "Director";//nombre
                    dgvrepsanciones.Columns[3].Width = 240;
                    dgvrepsanciones.Columns[4].HeaderText = "No";//acumulado
                    dgvrepsanciones.Columns[4].Width = 75;
                    dgvrepsanciones.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[5].HeaderText = "Sanción";//sancion
                    dgvrepsanciones.Columns[5].Width = 130;
                    dgvrepsanciones.Columns[6].HeaderText = "Periodo";//quincena
                    dgvrepsanciones.Columns[6].Width = 195;
                    dgvrepsanciones.Columns[7].HeaderText = "Retardos";//retardos
                    dgvrepsanciones.Columns[7].Width = 70;
                    dgvrepsanciones.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[8].HeaderText = "Minutos";//minutos
                    dgvrepsanciones.Columns[8].Width = 70;
                    dgvrepsanciones.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[9].HeaderText = "Fecha Susp";//fecha de suspension
                    dgvrepsanciones.Columns[9].Width = 75;
                    dgvrepsanciones.Columns[10].HeaderText = "Estatus";//estado
                    dgvrepsanciones.Columns[10].Width = 240;
                    dgvrepsanciones.Columns[11].HeaderText = "Observaciones";//obs
                    dgvrepsanciones.Columns[11].Width = 240;
                    dgvrepsanciones.Columns[12].Visible = false;
                    dgvrepsanciones.Columns[13].Visible = false;
                    dgvrepsanciones.Columns[14].Visible = false;
                    dgvrepsanciones.Columns[15].Visible = false;
                    dgvrepsanciones.Columns[16].Visible = false;
                    dgvrepsanciones.Columns[17].Visible = false;
                    dgvrepsanciones.Columns[18].Visible = false;
                    dgvrepsanciones.Columns[19].Visible = false;
                    dgvrepsanciones.Columns[20].Visible = false;
                    dgvrepsanciones.Columns[21].Visible = false;
                    dgvrepsanciones.Columns[22].Visible = false;
                    dgvrepsanciones.ClearSelection();
                }
                else
                {
                    DialogResult result = MessageBox.Show("No existen registros para el ciclo seleccionado", "SIPAA", MessageBoxButtons.OK);
                }
            }
            else if (ireporte == 3)
            {
                //dt registros sancion
                DataTable dtgralextsusp = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                int inumreggralextsusp = dtgralextsusp.Rows.Count;

                if (inumreggralextsusp >= 1)
                {
                    dgvrepsanciones.DataSource = dtgralextsusp;

                    dgvrepsanciones.Columns[0].Visible = false;
                    dgvrepsanciones.Columns[1].HeaderText = "Área";//area
                    dgvrepsanciones.Columns[1].Width = 145;
                    dgvrepsanciones.Columns[2].HeaderText = "Periodo";//periodo
                    dgvrepsanciones.Columns[2].Width = 75;
                    dgvrepsanciones.Columns[3].HeaderText = "Quincena";//quincena
                    dgvrepsanciones.Columns[3].Width = 170;
                    dgvrepsanciones.Columns[4].HeaderText = "No Emp";//numero empleado
                    dgvrepsanciones.Columns[4].Width = 70;
                    dgvrepsanciones.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[5].HeaderText = "Nombre";//nombre
                    dgvrepsanciones.Columns[5].Width = 240;
                    dgvrepsanciones.Columns[6].HeaderText = "Puesto";//puesto
                    dgvrepsanciones.Columns[6].Width = 160;
                    dgvrepsanciones.Columns[7].HeaderText = "Director";//dir
                    dgvrepsanciones.Columns[7].Width = 160;
                    dgvrepsanciones.Columns[8].HeaderText = "Ext";//extrañamiento
                    dgvrepsanciones.Columns[8].Width = 35;
                    dgvrepsanciones.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[9].HeaderText = "Susp";//suspension
                    dgvrepsanciones.Columns[9].Width = 35;
                    dgvrepsanciones.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[10].HeaderText = "Fecha Susp";//fecha de suspension
                    dgvrepsanciones.Columns[10].Width = 75;
                    dgvrepsanciones.Columns[11].HeaderText = "Retardos";//retardos
                    dgvrepsanciones.Columns[11].Width = 70;
                    dgvrepsanciones.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[12].HeaderText = "Minutos";//minutos
                    dgvrepsanciones.Columns[12].Width = 70;
                    dgvrepsanciones.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[13].HeaderText = "Acum Ext, Susp";//acumulado
                    dgvrepsanciones.Columns[13].Width = 75;
                    dgvrepsanciones.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvrepsanciones.Columns[14].HeaderText = "Estatus";//estado
                    dgvrepsanciones.Columns[14].Width = 240;
                    dgvrepsanciones.Columns[15].HeaderText = "Observaciones";//obs
                    dgvrepsanciones.Columns[15].Width = 240;
                    dgvrepsanciones.Columns[16].Visible = false;
                    dgvrepsanciones.Columns[17].Visible = false;
                    dgvrepsanciones.Columns[18].Visible = false;
                    dgvrepsanciones.Columns[19].Visible = false;
                    dgvrepsanciones.Columns[20].Visible = false;
                    dgvrepsanciones.ClearSelection();
                }
                else
                {
                    DialogResult result = MessageBox.Show("No existen registros para el ciclo seleccionado", "SIPAA", MessageBoxButtons.OK);
                }
            }
            else
            {
            }
        }

        //muestra reportes
        private void freportes(int iopcion, int icvsancion, int iidtrab, int iidtrabdir, int icvincidenciagen,
                                int icvtipogen, int icvperiodo, int icvpolitica, string sfsuspension, int istsanción,
                                string sobssanción, string susumodifst, string sfhumodst, string sprgumodst, string sequmodst,
                                int ireporte)
        {
            if (ireporte == 1)
            {
                //dt registros sancion
                DataTable dtrepglobal = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                int inumregrepglobal = dtrepglobal.Rows.Count;

                if (inumregrepglobal >= 1)
                {
                    DataTable repsancionesglobal = new DataTable();

                    repsancionesglobal = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    repsancionesglobal dtrpt = new repsancionesglobal();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(repsancionesglobal, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

                    form.RptDoc = ReportDoc;
                    form.Show();

                }
            }
            else if (ireporte == 2)
            {
                //dt registros sancion
                DataTable dtrepmensual = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                int inumregrepmensual = dtrepmensual.Rows.Count;

                if (inumregrepmensual >= 1)
                {
                    DataTable repmensual_se = new DataTable();

                    repmensual_se = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    rep_mensual_se dtrpt = new rep_mensual_se();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(repmensual_se, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

                    form.RptDoc = ReportDoc;
                    form.Show();
                }
            }
            else if (ireporte == 3)
            {
                //dt registros sancion
                DataTable dtrepgeralse = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                int inumregrepgeneralse = dtrepgeralse.Rows.Count;

                if (inumregrepgeneralse >= 1)
                {
                    DataTable repsancionesggeneral = new DataTable();

                    repsancionesggeneral = scancionesproceso.dtdatos(iopcion, icvsancion, iidtrab, iidtrabdir, icvincidenciagen, icvtipogen, icvperiodo, icvpolitica, sfsuspension, istsanción, sobssanción, susumodifst, sfhumodst, sprgumodst, sequmodst);

                    //Preparación de los objetos para mandar a imprimir el reporte de Crystal Reports
                    ViewerReporte form = new ViewerReporte();
                    repgeneralse dtrpt = new repgeneralse();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(repsancionesggeneral, "SIPAA_CS.RecursosHumanos.Reportes", dtrpt.ResourceName);

                    form.RptDoc = ReportDoc;
                    form.Show();

                }
            }
            else
            {
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    }
}
