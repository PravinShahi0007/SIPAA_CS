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

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class DatosTrabajadorPerfil : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        
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
    }
}
 