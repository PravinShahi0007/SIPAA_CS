using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            panelAccion.Enabled = false;
            //ValidarBotones();
            //prgb1.Maximum = 100;
            //prgb1.Value = 0;


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
            dgvReloj.Columns["valida_Huella"].Visible = false;
            dgvReloj.Columns["valida_Teclado"].Visible = false;
            dgvReloj.Columns["multiplehuella"].Visible = false;
            dgvReloj.Columns["IP"].Visible = false;

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
                objR.Enviado = Convert.ToBoolean(row.Cells["Enviado"].Value);

                objR.Teclado = row.Cells["valida_Teclado"].Value.ToString();
                objR.Huella = row.Cells["valida_Huella"].Value.ToString();

                ValidarExistencia(ltUsuario, objR);

                if (ltUsuario.Count > 0)
                {
                    panelAccion.Enabled = true;

                    UsuarioReloj obj1 = new UsuarioReloj();
                    obj1 = ltUsuario[0];
                    RelojxUsuario.idtrab = obj1.idtrab;
                    RelojxUsuario.IPReloj = obj1.ipReloj;
                    RelojxUsuario.cvreloj = obj1.cvreloj;
                    RelojxUsuario.Nombre = obj1.Nombre;

                    //btnTeclado.Enabled = true;

                    btnPerfil.Enabled = true;


                    if (!ValidarBorrar(ltUsuario))
                    {
                        btnFace.Enabled = false;
                        btnHuella.Enabled = false;
                        btnDescarga.Enabled = false;
                        btnKey.Enabled = false;
                        btnBorrar.Enabled = false;
                        btnTeclado.Enabled = false;
                        btnSync.Enabled = true;
                    }
                    else
                    {
                        btnFace.Enabled = true;
                        btnHuella.Enabled = true;
                        btnDescarga.Enabled = true;
                        btnKey.Enabled = true;
                        btnBorrar.Enabled = true;
                        btnTeclado.Enabled = true;
                        btnSync.Enabled = true;
                        ValidarBotones();
                    }

                   
                    if (ltUsuario.Count > 1)
                    {
                        btnTeclado.Enabled = false;
                        btnPerfil.Enabled = false;
                    }
                }
                else {
                    panelAccion.Enabled = false;
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

        public bool ValidarBorrar(List<UsuarioReloj> lt) {

            bool bBandera = true;
            foreach (UsuarioReloj obj in lt) {

                if (obj.Enviado != true) { bBandera = false; }

            }

            return bBandera;
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

        public void ValidarBotones() {

            RelojChecador objReloj = new RelojChecador();

            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(10, TrabajadorInfo.cvReloj, "", "", "", 0, "", "");
            
            if (dtRelojChecador.Rows[0]["Teclado"].ToString() != "True") { btnTeclado.Enabled = false; } else { btnTeclado.Enabled = true; }

            if (dtRelojChecador.Rows[0]["Rostro"].ToString() != "True") { btnFace.Enabled = false; } else { btnFace.Enabled = true; }

            if (dtRelojChecador.Rows[0]["Huella"].ToString() != "True") { btnHuella.Enabled = false; } else { btnHuella.Enabled = true; }


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RelojChecador obj = new RelojChecador();

            obj.p_cvreloj = TrabajadorInfo.cvReloj;
            obj.p_ip = "%";
            obj.p_usuumod = "%";
            obj.p_prgumodr = "%";
            obj.p_cvvnc = "%";


            if (txtidtrab.Text != String.Empty) { obj.p_usuumod = txtidtrab.Text; } 
            if (txtNombre.Text != String.Empty) { obj.p_prgumodr = txtNombre.Text; } 

            if (cbEstatus.SelectedIndex != 0) {

                if (cbEstatus.SelectedIndex != 2)
                {
                    obj.p_cvvnc = cbEstatus.SelectedIndex.ToString();
                }
                else {
                    obj.p_cvvnc = "0";
                }
            }

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
                progressBar1.Value = 20;
                Mensaje = "Analizando Registros...";
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Eliminando Registros...");
                pnlMensaje.Enabled = false;
                //bd.RunWorkerAsync();
                bool bBandera = false;
                progressBar1.Value = 20;
                foreach (UsuarioReloj obj in ltUsuario)
                {

                    string cvreloj = obj.cvreloj.ToString();
                    string idtrab = obj.idtrab;
                    string ip = obj.ipReloj;
                    if (obj.Enviado != false)
                    {
                        pnlMensaje.Enabled = true;
                        progressBar1.Value = progressBar1.Value + (10 / ltUsuario.Count);
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo.");
                        pnlMensaje.Enabled = false;
                        if (Connect_Net(ip, 4370))
                        {
                            if (objCZKEM.SSR_DeleteEnrollData(1, idtrab, 12))
                            {
                                RelojChecador objReloj = new RelojChecador();
                                objReloj.RelojesxTrabajador(idtrab, Convert.ToInt32(cvreloj), 9, "", "");
                                objCZKEM.Disconnect();

                                objReloj.p_cvreloj = TrabajadorInfo.cvReloj;
                                objReloj.p_ip = "%";
                                objReloj.p_usuumod = "%";
                                objReloj.p_prgumodr = "%";
                                objReloj.p_cvvnc = "%";
                                llenarGrid(objReloj);
                            }
                            else
                            {
                                bBandera = true;

                            }
                        }
                        else
                        {

                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible Conectarse con el Dispositivo.");
                            // progressBar1.Visible = false;
                            pnlMensaje.Enabled = true;
                        }
                    }
                }
                objCZKEM.Disconnect();
                progressBar1.Value = 80;
                if (bBandera != true)
                {
                    // progressBar1.Visible = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Registros Borrados correctamente.");
                }
                else
                {
                    //progressBar1.Visible = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se han logrado borrar. ");
                  
                }
                pnlMensaje.Enabled = true;
                timer1.Start();
                progressBar1.Value = 100;
            }
        }

        private void btnDescarga_Click(object sender, EventArgs e)
        {
            int iCont = 0;
            if (ltUsuario.Count > 0)
            {
                iCont += 1;
                progressBar1.Value = 20;
                panelAccion.Enabled = false;
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

                      
                        progressBar1.Value = progressBar1.Value + (50 / ltUsuario.Count);
                        Mensaje = "Descargando Registros de la Base de Datos...";
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Descargando datos " + iCont + "de" + ltUsuario.Count + "Trabajadores");
                        pnlMensaje.Enabled = false;


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
                                        bool bIngreso = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, cvReloj,iModoCheck);
                                        if (!bIngreso) { bBandera = true; }
                                    }
                                }
                                RelojChecador objReloj = new RelojChecador();
                                //objReloj.obtrelojeschecadores(8, obj.cvreloj, "", "", "", 0, "", "");
                            }
                            objCZKEM.Disconnect();
                            progressBar1.Value = 90;
                            if (bBandera != true)
                            {
                                pnlMensaje.Enabled = true;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente");
                                pnlMensaje.Enabled = false;
                                Thread.Sleep(2000);
                                timer1.Start();
                                objCZKEM.Disconnect();
                                
                            }
                            else {

                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se guardaron correctamente. Por Favor Repite el Proceso");
                                timer1.Start();
                                
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

                progressBar1.Value = 100;
                panelAccion.Enabled = true;
                timer1.Start();

            }
        }


        public bool IngresarRegistro(string sIdTrab, int Year, int Month, int Day, int Hour, int Minute, int Second, int cvReloj,int iTipoCheck)
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
           

            switch (iTipoCheck) {

                case 0:
                    iTipoCheck += 1;
                    break;

                case 1:
                    iTipoCheck += 1;
                    break;

                case 2:
                    iTipoCheck += 1;
                    break;

                case 3:
                    iTipoCheck += 1;
                    break;
            }

            DataTable dt = objTrab.Relojchecador(sIdTrab, 5, DateTime.Parse(sFecha), iTipoCheck, tpHora, cvReloj, 2, LoginInfo.IdTrab, this.Name);

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
            progressBar1.Value = 0;
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
            progressBar1.Value = 20;
            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con el dispositivo.");
            this.Enabled = false;
            bool bBandera = false;
           
            foreach (UsuarioReloj obj in ltUsuario)
            {
                
                string cvreloj = obj.cvreloj.ToString();
                string idtrab = obj.idtrab;
                string ip = obj.ipReloj;

                if (Connect_Net(ip, 4370))
                {
                    objCZKEM.ReadAllGLogData(1);
                    objCZKEM.ReadAllTemplate(1);

                    this.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Conectado.");
                    this.Enabled = false;
                    this.Enabled = true;
                    progressBar1.Value = progressBar1.Value + (50 / ltUsuario.Count);
                    this.Enabled = false;
                    this.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Eliminando Datos.");
                    
                    this.Enabled = false;
                    if (objCZKEM.SSR_DelUserTmp(1, idtrab,0))
                    {  
                        //RelojChecador objReloj = new RelojChecador();
                        //objReloj.RelojesxTrabajador(idtrab, Convert.ToInt32(cvreloj), 1, "", "");
                        objCZKEM.Disconnect(); 
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Datos Eliminados Correctamente.");
                        timer1.Start();
                    }
                    else
                    {
                        bBandera = true;
                        objCZKEM.Disconnect();
                    }
                }
                else
                {
                    this.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible Conectarse con el Dispositivo.");
                    progressBar1.Value = 0;
                    //progressBar1.Visible = false;
                }
            }
            this.Enabled = true;
            if (bBandera != false) {
                this.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registro no pudieron ser eliminados del dispositivo");
                progressBar1.Value = 0;
            }

        }
    

        private void btnTeclado_Click(object sender, EventArgs e)
        {
            CapturaTrabajador frm = new CapturaTrabajador();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSync_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Esta Seguro que desea SINCRONIZAR la información del Trabajador? Esto eliminara la información del Trabajador y sera sustituida"
                                                    +"con la información del Sistema", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                progressBar1.Value = 20;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con el dispositivo.");
                pnlMensaje.Enabled = false;
                int iCont = 0;
                if (ltUsuario.Count > 0)
                {

                    pnlMensaje.Enabled = true;

                    Mensaje = "Descargando Registros de la Base de Datos...";
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Conectando con el Dispositivo...");
                    iCont += 1;
                    progressBar1.Value = 20;
                    pnlMensaje.Enabled = false;
                    //bd.RunWorkerAsync();
                    //frm.Show();
                    //Thread.Sleep(4000);

                    foreach (UsuarioReloj obj in ltUsuario)
                    {
                        SonaTrabajador objTrab = new SonaTrabajador();
                        //objTrab.GestionIdentidad(obj.idtrab, "", "", "0", LoginInfo.IdTrab, this.Name, 1);

                        bool bConexion = Connect_Net(obj.ipReloj, 4370);
                        //this.Enabled = false;
                        if (bConexion != false)
                        {
                            objCZKEM.SSR_DeleteEnrollData(1, obj.idtrab, 12);
                            bool bBandera = false;

                            if (obj.Enviado != true || obj.Enviado != false)
                            {
                                pnlMensaje.Enabled = true;
                                progressBar1.Value = progressBar1.Value + (50 / ltUsuario.Count);
                                Mensaje = "Descargando Registros de la Base de Datos...";
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Cargando " + iCont + "de" + ltUsuario.Count + " Registros");
                                pnlMensaje.Enabled = false;

                                string idtrab = obj.idtrab;
                                string cvreloj = obj.cvreloj.ToString();
                                string Nombre = obj.Nombre;
                                // Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Cargando Datos de Trabajador a Dispositivo");
                                RelojChecador objReloj = new RelojChecador();
                                int iCv = TrabajadorInfo.cvReloj;
                                DataTable dt = objReloj.RelojesxTrabajador(idtrab, iCv, 6, "%", "%");
                                DataTable dtRelojChecador = objReloj.obtrelojeschecadores(10, TrabajadorInfo.cvReloj, "", "", "", 0, "", "");

                                // bool bBandera = true;
                                bool bBanderaPass = false;
                                bool bBanderaHuella = false;
                                bool bBanderaRostro = false;
                                string pass_desc = "";

                                foreach (DataRow row in dt.Rows)
                                {

                                    if (dtRelojChecador.Rows[0]["Teclado"].ToString() != "False")
                                    {
                                        try
                                        {
                                            if (row["pass"].ToString() != String.Empty)
                                            {
                                                pass_desc = Utilerias.descifrar(row["pass"].ToString());
                                            }
                                            if (!objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, 0, true))
                                            {
                                                bBanderaPass = true;
                                            }
                                        }
                                        catch
                                        {
                                            bBanderaPass = true;
                                        }
                                    }
                                    if (dtRelojChecador.Rows[0]["Huella"].ToString() != "False")
                                    {
                                        if (dtRelojChecador.Rows[0]["multiplehuella"].ToString() != "False")
                                        {

                                            if (row["huellaTmp"].ToString() != String.Empty)
                                            {
                                                try
                                                {
                                                    int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                                                    string tmpHuella = row["huellaTmp"].ToString();
                                                    if (!objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella))
                                                    {
                                                        bBanderaRostro = true;
                                                    }
                                                }
                                                catch
                                                {
                                                    bBanderaRostro = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                                            string tmpHuella = "";
                                            if (ifinger == 3)
                                            {
                                                tmpHuella = row["huellaTmp"].ToString();
                                            }
                                            else if (ifinger == 6)
                                            {
                                                tmpHuella = row["huellaTmp"].ToString();
                                            }
                                            else if (ifinger == 4)
                                            {
                                                tmpHuella = row["huellaTmp"].ToString();
                                            }
                                            else if (ifinger == 5)
                                            {
                                                tmpHuella = row["huellaTmp"].ToString();
                                            }
                                            else if (ifinger != 0 && ifinger != 9)
                                            {
                                                tmpHuella = row["huellaTmp"].ToString();
                                            }

                                            if (tmpHuella != String.Empty && ifinger != 0)
                                            {
                                                if (!objCZKEM.SetUserTmpExStr(1, idtrab, 0, 1, tmpHuella))
                                                {
                                                    bBanderaRostro = true;
                                                }
                                            }
                                        }

                                    }
                                    if (dtRelojChecador.Rows[0]["Rostro"].ToString() != "False")
                                    {
                                        if (row["rostroTmp"].ToString() != String.Empty)
                                        {
                                            try
                                            {
                                                string RostroTmp = row["rostroTmp"].ToString();
                                                int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                                                if (!objCZKEM.SetUserFaceStr(1, idtrab, 50, RostroTmp, rostrolong))
                                                {
                                                    bBanderaRostro = true;
                                                }
                                            }
                                            catch
                                            {
                                                bBanderaHuella = true;
                                            }
                                        }
                                    }

                                    if (!bBanderaHuella && !bBanderaPass && !bBanderaRostro)
                                    {
                                        objReloj.RelojesxTrabajador(idtrab, 0, 7, "", "");
                                        bBandera = false;
                                        progressBar1.Enabled = true;

                                        progressBar1.Enabled = false;


                                        objReloj.RelojesxTrabajador(idtrab, 0, 7, "", "");
                                        objReloj.p_cvreloj = TrabajadorInfo.cvReloj;
                                        objReloj.p_ip = "%";
                                        objReloj.p_usuumod = "%";
                                        objReloj.p_prgumodr = "%";
                                        objReloj.p_cvvnc = "%";

                                        llenarGrid(objReloj);
                                    }
                                    else
                                    {
                                        bBandera = true;
                                    }
                                }

                                objCZKEM.Disconnect();
                                progressBar1.Value = 89;


                            }

                            if (bBandera != true)
                            {

                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente.");

                            }
                            else
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                            }

                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.ipReloj);

                        }
                    }
                    progressBar1.Value = 89;
                    pnlMensaje.Enabled = true;
                    ltUsuario.Clear();
                    //Bd_RunWorkerCompleted(sender, e);
                    timer1.Start();
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");
                    timer1.Start();
                }
            }
        }

        private void btnFace_Click(object sender, EventArgs e)
        {
            ProcesoReloj("Face");
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            ProcesoReloj("Pass");
        }

        private void btnHuella_Click(object sender, EventArgs e)
        {
            ProcesoReloj("Huella");
        }


        public void ProcesoReloj(string Opcion)
        {

            if (ltUsuario.Count > 0)
            {
                progressBar1.Visible = true;
                progressBar1.Value = 20;
                panelAccion.Enabled = false;
                pnlMensaje.Enabled = false;
                objCZKEM = new CZKEMClass();

                int iCont = 0;
                foreach (UsuarioReloj obj in ltUsuario)
                {

                    
                    iCont += 1;
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltUsuario.Count);
                    progressBar1.Value = 40;
                    pnlMensaje.Enabled = false;

                    bool bConexion = Connect_Net(obj.ipReloj, 4370);
                    //this.Enabled = false;
                    if (bConexion != false)
                    {
                        objCZKEM.ReadAllUserID(1);
                        objCZKEM.ReadAllTemplate(1);
                        bool bBandera = false;
                        int iContTrab = 0;
                      
                            iContTrab += 1;
                            string idTrab = obj.idtrab; 
                            
                            bBandera = ConsultaReloj(Opcion, idTrab, iContTrab, ltUsuario.Count);
                            progressBar1.Value = progressBar1.Value + (10 / ltUsuario.Count);
                        
                        objCZKEM.Disconnect();
                        progressBar1.Value = 90;
                        if (bBandera != true)
                        {
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente.");
                            pnlMensaje.Enabled = false;
                            timer1.Start();

                        }
                        else
                        {
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                            pnlMensaje.Enabled = false;
                            timer1.Start();
                        }


                    }
                    else
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.ipReloj);
                        pnlMensaje.Enabled = false;
                        timer1.Start();
                        progressBar1.Visible = false;
                        //  break;
                    }
                }

                pnlMensaje.Enabled = true;
                panelAccion.Enabled = true;
                //ltUsuario.Clear();
                progressBar1.Value = 100;
                //Bd_RunWorkerCompleted(sender, e);
                //llenarGrid();
                timer1.Start();
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");
            }


        }

        public bool ConsultaReloj(string Opcion, string idtrab, int iContTrab, int iTotal)
        {


            string sFaceTmp = "";
            byte tmp = new byte();
            int iFaceLong = 0;
            bool bBandera = false;
            switch (Opcion)
            {

                case "Face":

                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Obteniendo Imagen de Trabajador " + iContTrab + " de " + iTotal);
                    pnlMensaje.Enabled = false;
                    if (!objCZKEM.GetUserFaceStr(1, idtrab, 50, ref sFaceTmp, ref iFaceLong)) { bBandera = true; }
                    else
                    {
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, "", sFaceTmp, iFaceLong.ToString(), LoginInfo.IdTrab, this.Name, 7);
                        }
                        catch
                        {

                        }
                    }
                    break;

                case "Huella":

                    pnlMensaje.Enabled = true;
                    int iDedo = 0;
                    int flag = 0;
                    string huellatmp = "";
                    int tpmlong = 0;
                    string sIdTrab = "";
                    string sNombre = "";
                    string sPass = "";
                    int iPrivilegio = 0;
                    bool bActivo = false;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Obteniendo Template de Trabajador " + iContTrab + " de " + iTotal);
                    pnlMensaje.Enabled = false;

                    while (objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
                    {
                        for (int iFinger = 0; iFinger < 10; iFinger++)
                        {
                            if (objCZKEM.GetUserTmpExStr(1, sIdTrab, iFinger, out flag, out huellatmp, out tpmlong))
                            {
                                SonaTrabajador objTrab = new SonaTrabajador();
                                try
                                {
                                    objTrab.GestionHuella(sIdTrab, huellatmp, iFinger, LoginInfo.IdTrab, this.Name, 5);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }

                    //if(sIdTrab)


                    break;


                case "Pass":

                    string Nombre = "";
                    string Pass = "";
                    iPrivilegio = 0;
                    bActivo = false;
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo Contraseña de Trabajador " + iContTrab + " de " + iTotal);
                    pnlMensaje.Enabled = false;
                    string Cifrado = "";
                    if (!objCZKEM.SSR_GetUserInfo(1, idtrab, out Nombre, out Pass, out iPrivilegio, out bActivo)) { bBandera = true; }
                    else
                    {
                        if (Pass != String.Empty)
                        {
                            Cifrado = Utilerias.cifrarPass(Pass, 1);
                        }
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            if (Cifrado != String.Empty)
                            {
                                objTrab.GestionIdentidad(idtrab, Cifrado, "", "0", LoginInfo.IdTrab, this.Name, 6);
                            }else
                            {
                                pnlMensaje.Enabled = true;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Uno o Varios usuarios no tiene Contraseña en el Dispositivo.");
                                pnlMensaje.Enabled = false;

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    break;


            }

            return bBandera;
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            UsuarioReloj obj = ltUsuario[0];
            TrabajadorInfo.IdTrab = obj.idtrab;
            AsignacionTrabajadorPerfil frm = new AsignacionTrabajadorPerfil();
            frm.Show();
        }
    }

    public class UsuarioReloj {

        public string idtrab;
        public string Nombre;
        public int cvreloj;
        public string ipReloj;
        public bool Enviado;
        public string Teclado;
        public string Huella;
        
   
    }


    public static class RelojxUsuario {

        public static string idtrab;
        public static string Nombre;
        public static int cvreloj;
        public static string IPReloj;
        public static string Pass;

    }
}
