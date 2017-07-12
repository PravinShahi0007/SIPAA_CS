using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zkemkeeper;
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class AdministracionUsuariosReloj : Form
    {

        CheckBox chk = new CheckBox();
        public CZKEMClass objCZKEM = new CZKEMClass();
        public List<UsuarioReloj> ltUsuario = new List<UsuarioReloj>();
        BackgroundWorker bd = new BackgroundWorker();
        public string Mensaje;

        public AdministracionUsuariosReloj()
        {
            InitializeComponent();
        }

        private void AdministracionUsuariosReloj_Load(object sender, EventArgs e)
        {
            LoginInfo.IdTrab = "ADMIN";
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;

            bd.DoWork += Bd_DoWork;
            bd.RunWorkerCompleted += Bd_RunWorkerCompleted;

            RelojChecador obj = new RelojChecador();

            obj.p_cvreloj = TrabajadorInfo.cvReloj;
            obj.p_ip = "%";
            obj.p_usuumod = "%";
            obj.p_prgumodr = "%";
            obj.p_cvvnc = "%";

            llenarGrid(obj);


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

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void llenarGrid(RelojChecador obj) {

            if (dgvReloj.Columns.Count > 0) { dgvReloj.Columns.RemoveAt(0); }

            DataTable dt = obj.obtrelojeschecadores(9,obj.p_cvreloj,obj.p_descripcion,obj.p_ip,obj.p_cvvnc,0,obj.p_usuumod,obj.p_prgumodr);

            dgvReloj.DataSource = dt;

            Utilerias.AgregarCheck(dgvReloj,0);
            
            dgvReloj.Columns["cvreloj"].Visible = false;

            foreach (DataGridViewRow row in dgvReloj.Rows)
            {
                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                row.Cells[0].Tag = "uncheck";
            }

        }

      
        private void dgvReloj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvReloj.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvReloj.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                UsuarioReloj objR = new UsuarioReloj();
                objR.cvreloj = Convert.ToInt32(row.Cells["cvreloj"].Value.ToString());
                objR.ipReloj = row.Cells["IP"].Value.ToString();
                objR.idtrab = row.Cells["idtrab"].Value.ToString();
                objR.Nombre = row.Cells["Nombre"].Value.ToString(); 
              

                ValidarExistencia(ltUsuario, objR);

                if (ltUsuario.Count > 0)
                {
                    panelAccion.Enabled = true;

                    UsuarioReloj obj1 = new UsuarioReloj();
                    obj1 = ltUsuario[0];
                    RelojxUsuario.idtrab = obj1.idtrab;
                    RelojxUsuario.IPReloj = obj1.ipReloj;
                    RelojxUsuario.cvreloj = obj1.cvreloj;

                    if (ltUsuario.Count > 1) {
                        btnTeclado.Enabled = false;
                     }

                }

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

        public void ValidarExistencia(List<UsuarioReloj> ltReloj, UsuarioReloj Obj)
        {
            bool bBandera = false;
            int iCont = 0;
            if (ltReloj.Count != 0)
            {
                while (iCont <= (ltReloj.Count - 1))
                {
                    UsuarioReloj objComp = ltReloj[iCont];

                    if (objComp.idtrab == Obj.idtrab)
                    {
                        if (objComp.cvreloj == Obj.cvreloj)
                        {
                            bBandera = true;
                            break;
                        }
                        iCont += 1;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RelojChecador obj = new RelojChecador();

            int cvreloj = 0;
            string desc = "%";
            string ip = "%";
            string cvvnc = "%";
            int actualiza = 0;
            string usumod = "%";
            string prgumod = "%";

           
            if (txtidtrab.Text != String.Empty) { obj.p_usuumod = txtidtrab.Text; } 
            if (txtNombre.Text != String.Empty) { obj.p_prgumodr = txtNombre.Text; } 

            if (cbEstatus.SelectedIndex != 0) { obj.p_cvvnc = cbEstatus.SelectedIndex.ToString(); } 

            llenarGrid(obj);

        }


        public bool Connect_Net(string IPAdd, int Port)
        {
            if (objCZKEM.Connect_Net(IPAdd, Port))
            {
                if (objCZKEM.RegEvent(1, 65535))
                {
                    //objCZKEM.OnConnected += ObjCZKEM_OnConnected;
                    //objCZKEM.OnDisConnected += objCZKEM_OnDisConnected;
                    //objCZKEM.OnEnrollFinger += ObjCZKEM_OnEnrollFinger;
                    //objCZKEM.OnFinger += ObjCZKEM_OnFinger;
                    //objCZKEM.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                    objCZKEM.RegEvent(1, 32767);
                }
                return true;
            }
            return false;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("Las asignaciones e información dentro del Dispositivo serán eliminadas ¿Esta Seguro que desea ELIMINAR la información del Trabajador?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Enabled = false;
                Mensaje = "Analizando Registros...";
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Eliminando Registros...");
                bd.RunWorkerAsync();
                bool bBandera = false;

                foreach (UsuarioReloj obj in ltUsuario)
                {

                    string cvreloj = obj.cvreloj.ToString();
                    string idtrab = obj.idtrab;
                    string ip = obj.ipReloj;

                    if (Connect_Net(ip, 4370))
                    {

                        if (objCZKEM.DeleteUserInfoEx(1, Convert.ToInt32(idtrab)))
                        {
                            RelojChecador objReloj = new RelojChecador();
                            objReloj.RelojesxTrabajador(idtrab, Convert.ToInt32(cvreloj), 1, "", "");
                            objCZKEM.Disconnect();
                        }
                        else
                        {
                            bBandera = true;
                            objCZKEM.Disconnect();
                        }
                    }
                    else
                    {

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible Conectarse con el Dispositivo.");
                        // progressBar1.Visible = false;
                        this.Enabled = true;
                    }
                }

                if (bBandera != true)
                {
                    // progressBar1.Visible = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Registros Borrados correctamente.");
                    timer1.Start();
                    this.Enabled = true;
                }
                else
                {
                    //progressBar1.Visible = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se han logrado borrar.");
                    this.Enabled = true;
                }
            }
        }

        private void btnDescarga_Click(object sender, EventArgs e)
        {
            if (ltUsuario.Count > 0)
            {
                Mensaje = "Descargando Registros de la Base de Datos...";
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Descargando Registros...");
                bd.RunWorkerAsync();
                string sIdTrab = String.Empty;
                int sVerify = 0;
                int iModoCheck = 0;
                int iAnho = 0;
                int iDia = 0;
                int iMes = 0;
                int iHora = 0;
                int iMinuto = 0;
                int iSegundo = 0;
                int iWorkCode = 0;
                string sIP = String.Empty;
                bool bBandera = false;
                foreach (UsuarioReloj obj in ltUsuario)
                {

                    try
                    {
                        bool bConexion = Connect_Net(obj.ipReloj, 4370);

                        objCZKEM.ReadAllUserID(1);
                        objCZKEM.ReadAllTemplate(1);


                        if (bConexion != false)
                        {

                            if (objCZKEM.ReadAllGLogData(1))
                            {

                                while (objCZKEM.SSR_GetGeneralLogData(1, out sIdTrab, out sVerify, out iModoCheck, out iAnho
                                                                      , out iMes, out iDia, out iHora, out iMinuto, out iSegundo
                                                                      , ref iWorkCode))
                                {
                                    if (obj.idtrab == sIdTrab)
                                    {
                                        int cvReloj = obj.cvreloj;
                                        bool bIngreso = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, cvReloj);
                                        if (!bIngreso) { bBandera = true; }
                                    }
                                }
                                RelojChecador objReloj = new RelojChecador();
                                //objReloj.obtrelojeschecadores(8, obj.cvreloj, "", "", "", 0, "", "");
                            }

                            if (bBandera != true)
                            {

                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Registros Guardados correctamente");
                                timer1.Start();
                                objCZKEM.Disconnect();
                            }
                            else {

                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se guardaron correctamente. Por Favor Repite el Proceso");
                                timer1.Start();
                                objCZKEM.Disconnect();
                            }
                        }
                        else {

                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse al Dispositivo con IP: " + obj.ipReloj );
                            timer1.Start();

                        }
                    }
                    catch {


                    }
                }

              
            }
        }


        public bool IngresarRegistro(string sIdTrab, int Year, int Month, int Day, int Hour, int Minute, int Second, int cvReloj)
        {

            SonaTrabajador objTrab = new SonaTrabajador();
            string sMes = "";
            string sDia = "";

            TimeSpan tpHora = new TimeSpan(Hour, Minute, Second);
            if (Month.ToString().Length == 1)
            {
                sMes = "0" + Month.ToString();
            }
            else
            {
                sMes = Month.ToString();
            }

            if (Day.ToString().Length == 1)
            {
                sDia = "0" + Day.ToString();
            }
            else
            {
                sDia = Day.ToString();
            }
            string sFecha = Year.ToString() + "-" + sMes + "-" + Day;

            DataTable dt = objTrab.Relojchecador(sIdTrab, 5, DateTime.Parse(sFecha), tpHora, cvReloj, 2, LoginInfo.IdTrab, this.Name);

            if (dt != null)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Correcto");
                //BuscarTrabajador(EnrollNumber);
                return true;
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error en el guardado de Datos, intente de nuevo");
                return false;
            }

        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            bool bBandera = false;
            foreach (UsuarioReloj obj in ltUsuario)
            {

                string cvreloj = obj.cvreloj.ToString();
                string idtrab = obj.idtrab;
                string ip = obj.ipReloj;

                if (Connect_Net(ip, 4370))
                {

                    if (objCZKEM.DeleteUserInfoEx(1, Convert.ToInt32(idtrab)))
                    {
                        RelojChecador objReloj = new RelojChecador();
                        objReloj.RelojesxTrabajador(idtrab, Convert.ToInt32(cvreloj), 1, "", "");
                        objCZKEM.Disconnect();
                    }
                    else
                    {
                        bBandera = true;
                        objCZKEM.Disconnect();
                    }
                }
                else
                {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible Conectarse con el Dispositivo.");
                    //progressBar1.Visible = false;
                }
            }

            if (bBandera != false) { Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registro no pudieron ser eliminados del dispositivo"); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
        }


        private void Bd_DoWork(object sender, DoWorkEventArgs e)
        {

            BarraProgreso.Dialog(Mensaje);


        }

        private void Bd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            BarraProgreso.Dialog();
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnHuellas_Click(object sender, EventArgs e)
        {
                  }

        private void btnTeclado_Click(object sender, EventArgs e)
        {
            Captura frm = new Captura();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    public class UsuarioReloj {

        public string idtrab;
        public string Nombre;
        public int cvreloj;
        public string ipReloj;
        
   
    }


    public static class RelojxUsuario {

        public static string idtrab;
        public static string Nombre;
        public static int cvreloj;
        public static string IPReloj;
        public static string Pass;

    }
}
