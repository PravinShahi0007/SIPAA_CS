using System;
using System.Windows.Forms;
using zkemkeeper;
using SIPAA_CS.RecursosHumanos;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Usuario;
using System.ComponentModel;
using System.Data;
using SIPAA_CS.Properties;
using System.Collections.Generic;

namespace SIPAA_CS.Sistemas.Procesos
{
    public partial class ReseteaRelojes : Form
    {
        public ReseteaRelojes()
        {
            InitializeComponent();
        }
        public CZKEMClass objCZKEM = new CZKEMClass();
        public CZKEMClass objCZKEM2 = new CZKEMClass();
        BackgroundWorker bd = new BackgroundWorker();
        CheckBox ckheader = new CheckBox();
        public class Reloj
        {
            public int cvReloj;
            public string IpReloj;
            public bool Teclado;
            public bool Huella;
            public bool Rostro;
            public bool MultipleHuella;
            public string UltimaDescargaAsistencia;
            public string Descripcion;
            public string UsuDescargaAsistencia;
            public string UltimaSincronizacion;
            public string UltimoUsuarioSinc;
        }

        List<Reloj> ltReloj = new List<Reloj>();


        private void ReseteaRelojes_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "AdministracionRelojChecador.cs")
                {
                    f.Hide();
                }
            }

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);

            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            int Valor = 0;

            Permisos.dcPermisos.TryGetValue("Actualizar", out Valor);

            if (Valor == 1)
            {
                btnReset.Enabled = true;
        
            }

            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

           // bd.DoWork += Bd_DoWork;
            //bd.RunWorkerCompleted += Bd_RunWorkerCompleted;
            LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
          /*  btnGuardar.Image = global::SIPAA_CS.Properties.Resources.Reloj;
            button1.Image = global::SIPAA_CS.Properties.Resources.Persona;
            btnReloj.Image = global::SIPAA_CS.Properties.Resources.RelojSync;
            button2.Image = global::SIPAA_CS.Properties.Resources.RelojSync;
            button2.Enabled = true;*/
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                 Application.Exit();
            
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            SistDashboard sistdasb = new SistDashboard();
            sistdasb.Show();
            this.Close();
        }



        public void LlenarGrid(int p_opcion, int p_cvreloj, string p_descripcion, string p_ip, string p_cvvnc, int p_stactualiza, string p_usuumod, string p_prgumodr)
        {

            if (dgvReloj.Columns.Count > 0)
            {
                dgvReloj.Columns.RemoveAt(0);
            }


            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
            dgvReloj.DataSource = dtRelojChecador;

            Utilerias.AgregarCheck(dgvReloj, 0);

            ckheader = Utilerias.AgregarCheckboxHeader(dgvReloj, 0);

            ckheader.CheckedChanged += Ckheader_CheckedChanged;
            dgvReloj.Columns["Clave"].Visible = false;
            dgvReloj.Columns["Actualiza"].Visible = false;
            dgvReloj.Columns["ClaveVNC"].Visible = false;
            dgvReloj.Columns["multiplehuella"].Visible = false;
           
            dgvReloj.Columns["teclado"].Visible = false;
            dgvReloj.Columns["huella"].Visible = false;
            dgvReloj.Columns["IP"].Visible = false;
            dgvReloj.Columns["Rostro"].Visible = false;
            ///////////////////////
            dgvReloj.Columns["Usuario Sincronizó Asistencias"].Visible = false;
            dgvReloj.Columns["Usuario Sincronizó Usuarios"].Visible = false;
            for (int i = 0; i < dgvReloj.Columns.Count; i++)
                dgvReloj.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

        

            foreach (DataGridViewRow row in dgvReloj.Rows)
            {
                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                row.Cells[0].Tag = "uncheck";
            }
        }


        private void Ckheader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked == true)
            {
                ltReloj.Clear();

                foreach (DataGridViewRow row in dgvReloj.Rows)
                {

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    ltReloj.Add(objR);

                  
                }

            }
            else
            {

                ltReloj.Clear();
                foreach (DataGridViewRow row in dgvReloj.Rows)
                {
                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";
                }
              
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (ltReloj.Count > 0)
            {
                foreach (Reloj obj in ltReloj)
                {
                    bool bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Comenzando el procezo ");
                    if (bConexion)
                    {
                        //Reiniciando dispositivo...
                        objCZKEM.RestartDevice(1);
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Reiniciando el dispositivo");
                        pnlMensaje.Enabled = false;
                        System.Threading.Thread.Sleep(60000);
                    }
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "El Reloj "+obj.Descripcion+" se ha reiniciado exitosamente");
                }

            }
            else
            {
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");
                pnlMensaje.Enabled = false;
            }
        }

        private void dgvReloj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvReloj.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvReloj.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                Reloj objR = new Reloj();
                objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                objR.IpReloj = row.Cells["IP"].Value.ToString();
                objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                objR.UltimaDescargaAsistencia = row.Cells["Última Descarga Asistencias"].Value.ToString();
                objR.Descripcion = row.Cells["Descripción"].Value.ToString();
                objR.UsuDescargaAsistencia = row.Cells["Usuario Sincronizó Asistencias"].Value.ToString();
                objR.UltimaSincronizacion = row.Cells["Última Sincronización Usuarios"].Value.ToString();
                objR.UltimoUsuarioSinc = row.Cells["Usuario Sincronizó Usuarios"].Value.ToString();

                ValidarExistencia(ltReloj, objR);

               





                if (row.Cells[0].Tag.ToString() == "check")
                {

                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";
                }
                else if (row.Cells[0].Tag.ToString() == "uncheck")
                {
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    row.Cells[0].Tag = "check";
                }
            }
        }

        public void ValidarExistencia(List<Reloj> ltReloj, Reloj Obj)
        {
            bool bBandera = false;
            int iCont = 0;
            if (ltReloj.Count != 0)
            {
                while (iCont <= (ltReloj.Count - 1))
                {
                    Reloj objComp = ltReloj[iCont];

                    if (objComp.cvReloj == Obj.cvReloj)
                    {
                        bBandera = true;
                        break;
                    }
                    else
                    {
                        iCont += 1;

                    }
                }

                if (bBandera == true)
                {
                    ltReloj.Remove(ltReloj[iCont]);
                }
                else
                {
                    ltReloj.Add(Obj);
                }
            }
            else
            {
                ltReloj.Add(Obj);

            }

        }
    }
}
