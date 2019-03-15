using SIPAA_CS.App_Code;
using SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.App_Code.Accesos.Catalogos;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class DatosTrabajadorPerfil : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        Usuarioap cusuarioap = new Usuarioap();
        Utilerias cutilerias = new Utilerias();
        bool verificacorreo = false;
        string mail;
        bool verificar;

        public DatosTrabajadorPerfil()
        {
            InitializeComponent();
        }

        int iprespuesta = 0;
        DiasEspeciales DiasEspeciales = new DiasEspeciales();

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:7-04-2017     Última Modificacion: 17-04-2017
        //Última Modificacion: 20-02-2019 JLA se agrega lo de dias especiales por  ingreso defasado
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


        private void btnRegresar_Click(object sender, EventArgs e)
        {
            TrabajadoresPerfil trabperf = new TrabajadoresPerfil();
            trabperf.Show();
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

        private void btnAsignaciones_Click(object sender, EventArgs e)
        {
            AsignacionTrabajadorPerfil form = new AsignacionTrabajadorPerfil();
            form.Show();
            this.Close(); 
        }

        private void btnTipoHr_Click(object sender, EventArgs e)
        {
            AsignacionTipoHorarioTrabajador form = new AsignacionTipoHorarioTrabajador();
            form.Show();
            this.Close(); 
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void DatosTrabajadorPerfil_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);
            
            string sIdtrab = TrabajadorInfo.IdTrab;
            List<string> ltTnom = new List<string>();
            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(sIdtrab, 20, "%", "%", 0, LoginInfo.IdTrab, this.Name);
           
            foreach (DataRow row in dtTrab.Rows)
            {
                lbIdTrab.Text = row["idtrab"].ToString();
                lbNombre.Text = row["Nombre"].ToString();
                TrabajadorInfo.Nombre = row["Nombre"].ToString();
                lbCia.Text = row["Compañia"].ToString();
                lbUbicacion.Text = row["Ubicación"].ToString();
                if (row["Estatus"].ToString() == "1") { lbEstatus.Text = "Activo"; } else { lbEstatus.Text = "Inactivo"; }
                lbArea.Text = row["Área"].ToString();
                lbPuesto.Text = row["Puesto"].ToString();
                TrabajadorInfo.IdTrabSupervisor = row["IdTrabSupervisor"].ToString();
                TrabajadorInfo.NombreSupervisor = row["Supervisor"].ToString();
                lbFechaIngreso.Text = row["Fecha_Ingreso"].ToString();
                lbltiponom.Text = (row["Tipo_Nomina"].ToString());
                lbHorario.Text = row["Tipo_Horario"].ToString();

                switch (Convert.ToInt32(row["Checa"].ToString()))
                {
                    case 0:
                        panelAsignacionTrabajador.Visible = false;
                        lbCheca.Text = "No";
                        lbCheca.ForeColor = ColorTranslator.FromHtml("#f44336");
                        chkBoxdias.Visible = false;
                        break;
                    case 1:
                        lbCheca.Text = "Si";
                        lbCheca.ForeColor = ColorTranslator.FromHtml("#2e7d32");
                        panelAsignacionTrabajador.Visible = true;
                        lbSupervisor.Text = row["Supervisor"].ToString();
                        lbDirector.Text = row["IdTrabDirector"].ToString();
                        panelDiasEsp.Enabled = false;
                        break;
                }
                lbDepto.Text = row["Depto"].ToString();
                Correos(); 

            }
            try
            {                
                pictureBox1.Image = Image.FromFile(@"\\172.165.1.10\FotosJS\FotosEmpleados\" + lbIdTrab.Text + ".jpg");
            }
            catch {}
           
            ltTnom.Clear();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void Correos()
        {
            

            DataTable datosusuario = cusuarioap.dtdatos(4, lbIdTrab.Text, 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");

            string scvdominio = string.Empty;

            if (datosusuario.Rows.Count <= 0) 
            {

                txtcorreo.Text = "";
                cutilerias.p_inicbo = 0;
                cbodominios.DataSource = null;
                DataTable dtdatos2 = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
                Utilerias.llenarComboxDataTable(cbodominios, dtdatos2, "cv", "desc");
                cutilerias.p_inicbo = 1;
                verificacorreo = false;
                button10.Visible = true;


            }

            else 
            {

                txtcorreo.Text = datosusuario.Rows[0][3].ToString();
                scvdominio = datosusuario.Rows[0][4].ToString();
                cutilerias.p_inicbo = 0;
                cbodominios.DataSource = null;
                DataTable dtdatos3 = cusuarioap.dtdatos(5, "", 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
                Utilerias.llenarComboxDataTable(cbodominios, dtdatos3, "cv", "desc");
                if (scvdominio != "0") { cbodominios.SelectedValue = scvdominio; }
                cutilerias.p_inicbo = 1;
                if (datosusuario.Rows[0][3].ToString() == "")
                {
                    verificacorreo = true;
                    button10.Visible = true;
                }
                else if (datosusuario.Rows[0][3].ToString() != "")
                    button10.Visible = false;


            }



        }













        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AsignacionIncidenciasTrabajador form = new AsignacionIncidenciasTrabajador();
            form.Show();
            this.Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VacIncPermHrEsp nvform = new VacIncPermHrEsp();
            nvform.Show();
            this.Close();  
        }

        private void txtDias_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtDias.Text) > 1)
            {
                DateTime resultado = Convert.ToDateTime(dtpFechaInical.Text);
                dtpFechaFinal.Text = Convert.ToString(resultado.AddDays(Convert.ToInt32(txtDias.Text) - 1));
                //dtpFechaFinal.Focus();
                dtpFechaFinal.Enabled = false;
                txtReferencia.Focus();
            }
            else if (Convert.ToInt32(txtDias.Text) == 1)
            {
                dtpFechaFinal.Text = dtpFechaInical.Text;
                //dtpFechaFinal.Focus();
                dtpFechaFinal.Enabled = false;
                txtReferencia.Focus();
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {            
            //panelTag.Visible = true;
            //timer1.Start();
            string usuumod = LoginInfo.IdTrab;
            string prgumod = this.Name;
            int idtrab = int.Parse(lbIdTrab.Text);
            
            iprespuesta = DiasEspeciales.InsertarDiasEspecialesxTrabajador(idtrab, 1, 3, 9, dtpFechaInical.Text.Trim(),
            dtpFechaFinal.Text.Trim(), Convert.ToInt32(txtDias.Text), "00:00", "00:00", txtReferencia.Text, 4,
            0, 0, usuumod.ToString(), prgumod.ToString(), 0, 0);

            switch (iprespuesta.ToString())
            {
                case "1":
                    MessageBox.Show("La Asignación de dias especiales se llevo a cabo correctamente", "SIPAA");
                    panelDiasEsp.Enabled = false;
                    chkBoxdias.Visible = false;
                    break;                
                case "":
                    MessageBox.Show("Problemas al insertar los dias especiales, verifique.", "SIPAA");
                    chkBoxdias.Visible = false;
                    break;
            }
        }

        private void chkBoxdias_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxdias.Checked)
            {
                panelDiasEsp.Enabled = true;
            }
            else
            {
                panelDiasEsp.Enabled = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mail = txtcorreo.Text.Trim();
            verificar = mail.Contains("@");

            if (string.IsNullOrEmpty(mail))
            {
                DialogResult result = MessageBox.Show("ingrese un correo ", "SIPAA", MessageBoxButtons.OK);
                txtcorreo.Focus();
            }
            else
            {
                if (verificar == true)
                {
                    DialogResult result = MessageBox.Show(@"Correo electrónico invalido, no ingrese el @ (arroba)", "SIPAA", MessageBoxButtons.OK);
                    txtcorreo.Focus();
                }
                else if (cbodominios.Text.Trim() == "" || cbodominios.SelectedIndex == -1 || cbodominios.SelectedIndex == 0)
                {
                    DialogResult result = MessageBox.Show("Selecciona un dominio", "SIPAA", MessageBoxButtons.OK);
                    cbodominios.Focus();

                }
                else
                {
                    DialogResult result = MessageBox.Show(LoginInfo.Nombre + ": esta acción actualizará el correo del empleado;" + "\r\n" + "\r\n" + "\r\n" + "¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {

                        int ivalida = 0;
                        if (!verificacorreo)  
                        {
                            ivalida = cusuarioap.cruddatos(18, lbIdTrab.Text, Convert.ToInt32(lbIdTrab.Text), "", txtcorreo.Text.Trim(),
                                       int.Parse(cbodominios.SelectedValue.ToString()), "", 0, 1, "",
                                       "", "", "", "", "",
                                       0, 3, LoginInfo.cvusuario, "", Name,
                                       cutilerias.scontrol());
                        }
                        else 
                        {
                            ivalida = cusuarioap.cruddatos(10, lbIdTrab.Text, Convert.ToInt32(lbIdTrab.Text), "", txtcorreo.Text.Trim(),
                                      int.Parse(cbodominios.SelectedValue.ToString()), "", 0, 1, "",
                                      "", "", "", "", "",
                                      0, 3, LoginInfo.cvusuario, "", Name,
                                      cutilerias.scontrol());
                        }
                        if (ivalida == 2)
                            MessageBox.Show("Se ha guardado correctamente el correo electrónico \r\n la contraseña por primera vez será el número de empleado.", "SIPAA", MessageBoxButtons.OK);

                    }
                }
            }
        }

        private void txtcorreo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
 