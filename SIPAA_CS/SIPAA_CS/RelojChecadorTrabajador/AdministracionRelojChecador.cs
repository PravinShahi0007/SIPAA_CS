using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using zkemkeeper;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class AdministracionRelojChecador : Form
    {
        public AdministracionRelojChecador()
        {
            InitializeComponent();
        }


      
        public CZKEMClass objCZKEM = new CZKEMClass();
        CheckBox ckheader = new CheckBox();
       
        public class Reloj {
            public int cvReloj;
            public  string IpReloj;
        }

        List<Reloj> ltReloj = new List<Reloj>();
        BackgroundWorker bd = new BackgroundWorker();
        public string Mensaje;
        //BarraProgreso frm = new BarraProgreso();
        //BarraProgreso frm = new BarraProgreso();

        private void SplashHuella_Load(object sender, EventArgs e)
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
            LlenarGrid(6, 0, "%", "%", "%", 0, "", "");

        }

        private void Ckheader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked == true)
            {
                ltReloj.Clear();

                foreach (DataGridViewRow row in dgvReloj.Rows) {

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    ltReloj.Add(objR);

                    panelAccion.Enabled = true;
                }

            }
            else {

                ltReloj.Clear();
                foreach (DataGridViewRow row in dgvReloj.Rows)
                {
                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";
                }
                    panelAccion.Enabled = false;

            }
        }

        public void LlenarGrid(int p_opcion, int p_cvreloj, string p_descripcion, string p_ip, string p_cvvnc, int p_stactualiza, string p_usuumod, string p_prgumodr)
        {

            if (dgvReloj.Columns.Count > 0) {
                dgvReloj.Columns.RemoveAt(0);
            }


            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr);
            dgvReloj.DataSource = dtRelojChecador;

            Utilerias.AgregarCheck(dgvReloj,0);

            ckheader = Utilerias.AgregarCheckboxHeader(dgvReloj,0);

            ckheader.CheckedChanged += Ckheader_CheckedChanged;
            dgvReloj.Columns["Clave"].Visible = false;
            dgvReloj.Columns["Actualiza"].Visible = false;
            dgvReloj.Columns["ClaveVNC"].Visible = false;
            dgvReloj.Columns[0].Width = 90;

            foreach (DataGridViewRow row in dgvReloj.Rows)
            {
                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                row.Cells[0].Tag = "uncheck";
            }
        }

        public bool Connect_Net(string IPAdd, int Port)
        {
            if (objCZKEM.Connect_Net(IPAdd, Port))
            {
                if (objCZKEM.RegEvent(1, 65535))
                {
                   
                    objCZKEM.OnConnected += ObjCZKEM_OnConnected;
                    objCZKEM.OnDisConnected += objCZKEM_OnDisConnected;
                    objCZKEM.OnEnrollFinger += ObjCZKEM_OnEnrollFinger;
                    objCZKEM.OnFinger += ObjCZKEM_OnFinger; 
                    //objCZKEM.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                    objCZKEM.RegEvent(1, 32767);
                }
                return true;
            }
            return false;
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

        private void dgvReloj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvReloj.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvReloj.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                Reloj objR = new Reloj();
                objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                objR.IpReloj = row.Cells["IP"].Value.ToString();

                ValidarExistencia(ltReloj, objR);

                if (ltReloj.Count > 0)
                {
                    panelAccion.Enabled = true;
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

        private void btnDescarga_Click(object sender, EventArgs e)
        {

            if (ltReloj.Count > 0)
            {
                Mensaje = "Descargando Registros del Dispositivo...";
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Descargando Registros...");
                bd.RunWorkerAsync();

                //frm.Dialog("Descargando Registros de Reloj...");
                
                this.Enabled = false;

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

                foreach (Reloj obj in ltReloj)
                {

                    try
                    {
                        bool bConexion = Connect_Net(obj.IpReloj, 4370);

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
                                    int cvReloj = obj.cvReloj;
                                    // Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Guardando Registros");
                                    bool bIngreso = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, cvReloj);
                                   
                                    RelojChecador objReloj = new RelojChecador();
                                    objReloj.obtrelojeschecadores(7, obj.cvReloj, "", "", "", 0, "", "");
                                }
                               
                            }

                            objCZKEM.Disconnect();
                        }
                        else
                        {

                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);

                            progressBar1.Visible = false;
                            //break;

                        }
                    }
                    catch { }
                }
                ltReloj.Clear();
                this.Enabled = true;
                progressBar1.Visible = false;
                LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
                timer1.Start();
                timer2.Start();
            }
            else {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha seleccionado algún Registro.");
            }
        }


        public bool IngresarRegistro(string sIdTrab,int Year,int Month,int Day,int Hour,int Minute,int Second,int cvReloj) {

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
            else {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error en el guardado de Datos, intente de nuevo");
                return false;
            }

        }


        private void btnSync_Click(object sender, EventArgs e)
        {

            if (ltReloj.Count > 0)
            {
                Mensaje = "Descargando Registros de la Base de Datos...";
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Descargando Registros...");
                bd.RunWorkerAsync();
                //frm.Show();
                //Thread.Sleep(4000);
                this.Enabled = false;
                foreach (Reloj obj in ltReloj)
                {

                    RelojChecador objReloj = new RelojChecador();
                    DataTable dt = objReloj.RelojesxTrabajador("", obj.cvReloj, 6, "", "");

                    bool bConexion = Connect_Net(obj.IpReloj, 4370);

                    if (bConexion != false)
                    {
                        bool bBandera = false;
                        foreach (DataRow row in dt.Rows)
                        {

                            string idtrab = row["idtrab"].ToString();
                            string cvreloj = row[1].ToString();
                            string Nombre = row[2].ToString();
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Cargando Datos de Trabajador a Dispositivo");
                            if (!objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, idtrab, 0, true))
                            {
                                bBandera = true;
                            }
                            else
                            {

                                objReloj.RelojesxTrabajador(idtrab, 0, 7, "", "");
                            }
                        }

                        if (bBandera != true)
                        {

                            objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "");
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente.");
                            timer1.Start();

                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                            timer1.Start();
                        }

                        objCZKEM.Disconnect();
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);

                        progressBar1.Visible = false;
                        //  break;
                    }
                }
                this.Enabled = true;
                ltReloj.Clear();
                //Bd_RunWorkerCompleted(sender, e);
                LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
                timer1.Start();
            }
            else {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");

            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            foreach (Reloj obj in ltReloj)
            {
                bool bConexion = Connect_Net(obj.IpReloj, 4370);

                if (bConexion != false)
                {
                    if (objCZKEM.ReadAllGLogData(1)) {

                        objCZKEM.ClearSLog(1);
                    }
                }

                objCZKEM.Disconnect();
           }

            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Eliminación correcta.");
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
        }
        private void zkemClient_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {

          
        }

        private void ObjCZKEM_OnFinger()
        {
            throw new NotImplementedException();
        }

        private void ObjCZKEM_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength)
        {

          // lbconexion.Text = "Se ha enrolado un usuario con Huella";
            // throw new NotImplementedException();
        }

        private void objCZKEM_OnDisConnected()
        {
            //throw new NotImplementedException();
        }

        private void ObjCZKEM_OnConnected()
        {
          //  throw new NotImplementedException();
        }


    



        public void BuscarTrabajador(string sIdTrab)
        {

           
            //Usuario objusuario = new Usuario();
            //objusuario = objusuario.ObtenerDatosUsuario(sIdTrab, 0, "", "", "", "", "", 7);

            ////Búsqueda en SIPPAA
            //if (objusuario.Nombre == null || objusuario.Nombre == String.Empty)
            //{
            //    objusuario = objusuario.ObtenerListaTrabajadorUsuario(5, Int32.Parse(sIdTrab));

            //    //Buscar SONARH
            //    if (objusuario.Nombre == null || objusuario.Nombre == String.Empty)
            //    {
            //        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Usuario No encontrado en Sonarh");
            //        label2.Text = string.Empty;
            //        lbNombre.Text = string.Empty;
            //        string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
            //        lbDia.Text = sDia;
            //        lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
            //        //timer1.Start();
            //    }
            //    else
            //    {
            //        label2.Text = sIdTrab.ToString();
            //        lbNombre.Text = objusuario.Nombre;
            //        string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
            //        lbDia.Text = sDia;
            //        lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
            //        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Correcto");
            //        //timer1.Start();
            //    }
            //}
            //else
            //{

            //    label2.Text = sIdTrab.ToString();
            //    lbNombre.Text = objusuario.Nombre;
            //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Correcto...");
            //    lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
            //    string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
            //    lbDia.Text = sDia;
            //    //timer1.Start();

            //}



        }

        public void Checkin(string EnrollNumber)
        {

            string sName = "";
            string sPass = "";
            int iPrivilegio = 0;
            bool bActivo = false;

            IZKEM obj = objCZKEM;
            

            if (obj.SSR_GetUserInfo(1, EnrollNumber, out sName, out sPass, out iPrivilegio, out bActivo))
            {

                //lbMensaje.Text = EnrollNumber.ToString();
                //label3.Text = sName;

                string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
                //label5.Text = sDia;

                //label4.Text = DateTime.Now.ToString("HH:mm:ss");

            }

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            try
            {

                List<UserInfo> lstFPTemplates = new List<UserInfo>();
                SonaTrabajador objTrab = new SonaTrabajador();
                DataTable dtTrab = objTrab.GestionHuella(new byte[] { }, new byte[] { }, "", "", this.Name, 4);
                bool bBandera = false;

                string sIdTrab = String.Empty;
                string sNombre = String.Empty;
                string sPass = String.Empty;
                int iPrivilegio = 0;
                bool bActivo = false;

                int iFinger = 0;
                int iFlag = 0;
                string sTemplate = String.Empty;
                int iTemplatelength = 0;

                objCZKEM.ReadAllUserID(1);
                objCZKEM.ReadAllTemplate(1);


                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("IdTrab");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Dedo");
                dt.Columns.Add("Flag");
                dt.Columns.Add("Template");
                dt.Columns.Add("Tamaño");
                


                while (objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
                        {
                            for (iFinger = 0; iFinger < 10; iFinger++)
                            {
                                if (objCZKEM.GetUserTmpExStr(1, sIdTrab, iFinger, out iFlag, out sTemplate, out iTemplatelength))
                                {


                                    DataRow row = dt.NewRow();
                                    row["IdTrab"] = sIdTrab;
                                    row["Nombre"] = sNombre;
                                    row["Dedo"] = iFinger;
                            row["Flag"] = iFlag;
                            row["Template"] = sTemplate;
                                     row["Tamaño"] = iTemplatelength;
                                     dt.Rows.Add(row);
                            // lstFPTemplates.Add(fpInfo);
                        }
                            }
                        }

             //   dgv.DataSource = dt;



              
            }
            catch (Exception ex){

               //lbconexion.Text =  ex.Message;

            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //SonaTrabajador objTrab = new SonaTrabajador();
            //DataTable dtTrab = objTrab.GestionHuella(new byte[] { }, new byte[] { }, txtIdTrab.Text, "", this.Name, 5);

            //foreach (DataRow row in dtTrab.Rows)
            //{
            //    string sIdTrab = "";
            //    byte[] byteArray = new byte[] { };
            //    byteArray = (byte[])row["template"];
            //    sIdTrab = row["idTrab"].ToString();
            //    string sTmp  = Convert.ToBase64String(byteArray);
            //    //int i = BitConverter.(byteArray, 0);
            //    string i2 = BitConverter.ToString(byteArray, 0);
            //    int bytes = byteArray.Length;
            //    foreach (DataGridViewRow dgvrow in dgv.Rows) {

            //        if (dgvrow.Cells[0].Value.ToString() == sIdTrab) {

            //           // bool b = objCZKEM.SetUserTmpExStr(1, sIdTrab, 0,1, sTmp);
            //            string TemplateZKTeco = dgvrow.Cells["Template"].Value.ToString();
            //            byte[] decBytes1 = Encoding.UTF8.GetBytes(TemplateZKTeco);
            //            //byte[] bytes = new byte[TemplateZKTeco.Length * sizeof(char)];
            //            //System.Buffer.BlockCopy(TemplateZKTeco.ToCharArray(), 0, bytes, 0, bytes.Length);

            //            //bool b = Utilerias.CompararBytes(byteArray, bytes);
            //        }


            //    }

              

            //}

            }

        private void button2_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow dgvrow in dgv.Rows)
            //{

            //    if (dgvrow.Cells[0].Value.ToString() == txtIdTrab.Text)
            //    {

            //        // bool b = objCZKEM.SetUserTmpExStr(1, sIdTrab, 0,1, sTmp);
            //        string TemplateZKTeco = dgvrow.Cells["Template"].Value.ToString();
            //        byte[] decBytes1 = Encoding.UTF8.GetBytes(TemplateZKTeco);

            //        SonaTrabajador objTrab = new SonaTrabajador();
            //        DataTable dtTrab = objTrab.GestionHuella(decBytes1, new byte[] { }, "1", "", this.Name, 3);
            //        dtTrab = objTrab.GestionHuella(decBytes1, new byte[] { }, "1", "Prueba", this.Name, 1);
            //        //byte[] bytes = new byte[TemplateZKTeco.Length * sizeof(char)];
            //        //System.Buffer.BlockCopy(TemplateZKTeco.ToCharArray(), 0, bytes, 0, bytes.Length);
            //        break;
            //        //bool b = Utilerias.CompararBytes(byteArray, bytes);
            //    }
            //}
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

        

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdministracionUsuariosReloj from = new AdministracionUsuariosReloj();
            from.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {            
            string desc = "%";
            string ip = "%";
            
            if (txtDescripcion.Text != String.Empty) { desc = txtDescripcion.Text; }
            if (txtIP.Text != String.Empty) { ip = txtIP.Text; }
            
            LlenarGrid(6, 0, desc,ip, "%", 0, "", "");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {

                //frm.Close();
            }
            catch { }
        }

        private void Bd_DoWork(object sender, DoWorkEventArgs e)
        {

            BarraProgreso.Dialog(Mensaje);


        }

        private void Bd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            BarraProgreso.Dialog();
        }

    }



    public class UserInfo
    {

        public int MachineNumber;
        public string EnrollNumber;
        public string Name;
        public int FingerIndex;
        public string TmpData;
        public int Privelage;
        public string Password;
        public bool Enabled;
        public string iFlag;


    }

}
        


    