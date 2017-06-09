using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zkemkeeper;


namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class SplashHuella : Form
    {
        public SplashHuella()
        {
            InitializeComponent();
        }


      
        public CZKEMClass objCZKEM = new CZKEMClass();

        private void SplashHuella_Load(object sender, EventArgs e)
        {

            try
            {
             bool bConexion =  Connect_Net("192.168.9.91", 4370);

                if (bConexion != false)
                {

                    lbconexion.Text = "Conectado a Lector";
                    lbconexion.ForeColor = Color.Green;

                    //objsdk.OnConnected += Objsdk_OnConnected;

                    //objsdk.get

                    //List<UserInfo> ltUsers = GetAllUserInfo(objCZKEM, 1);

                    //UserInfo User = new UserInfo();
                    //User = ltUsers.ElementAt(2);

                }
                else {

                    lbconexion.Text = "Sin Conexión";
                    lbconexion.ForeColor = Color.Red;
                }




            }
            catch(Exception ex) {

                lbconexion.Text = ex.Message;
                lbconexion.ForeColor = Color.Red;

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
                    objCZKEM.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                    objCZKEM.RegEvent(1, 32767);
                }
                return true;
            }
            return false;
        }



        private void zkemClient_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {


           
            SonaTrabajador objTrab = new SonaTrabajador();
            string sMes = "";
            string sDia = "";

            TimeSpan tpHora = new TimeSpan(Hour, Minute, Second);
            if (Month.ToString().Length == 1)
            {
                sMes = "0" + Month.ToString();
            }
            else {
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
            string sFecha = Year.ToString() +"-"+ sMes + "-" + Day;

            DataTable dt = objTrab.Relojchecador(EnrollNumber,1,DateTime.Parse(sFecha),tpHora,1,2, EnrollNumber, this.Name);

            if (dt.Columns.Contains("INSERT")) {

                BuscarTrabajador(EnrollNumber);
            }
        }

        private void ObjCZKEM_OnFinger()
        {
            throw new NotImplementedException();
        }

        private void ObjCZKEM_OnEnrollFinger(int EnrollNumber, int FingerIndex, int ActionResult, int TemplateLength)
        {

            lbconexion.Text = "Se ha enrolado un usuario con Huella";
            // throw new NotImplementedException();
        }

        private void objCZKEM_OnDisConnected()
        {
            throw new NotImplementedException();
        }

        private void ObjCZKEM_OnConnected()
        {
            throw new NotImplementedException();
        }


    



        public void BuscarTrabajador(string sIdTrab)
        {

           
            Usuario objusuario = new Usuario();
            objusuario = objusuario.ObtenerDatosUsuario(sIdTrab, 0, "", "", "", "", "", 7);

            //Búsqueda en SIPPAA
            if (objusuario.Nombre == null || objusuario.Nombre == String.Empty)
            {
                objusuario = objusuario.ObtenerListaTrabajadorUsuario(5, Int32.Parse(sIdTrab));

                //Buscar SONARH
                if (objusuario.Nombre == null || objusuario.Nombre == String.Empty)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Usuario No encontrado en Sonarh");
                    label2.Text = string.Empty;
                    lbNombre.Text = string.Empty;
                    string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
                    lbDia.Text = sDia;
                    lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
                    //timer1.Start();
                }
                else
                {
                    label2.Text = sIdTrab.ToString();
                    lbNombre.Text = objusuario.Nombre;
                    string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
                    lbDia.Text = sDia;
                    lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Correcto");
                    //timer1.Start();
                }
            }
            else
            {

                label2.Text = sIdTrab.ToString();
                lbNombre.Text = objusuario.Nombre;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Correcto...");
                lbHora.Text = DateTime.Now.ToString("HH:mm:ss");
                string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
                lbDia.Text = sDia;
                //timer1.Start();

            }



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

                lbMensaje.Text = EnrollNumber.ToString();
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

                dgv.DataSource = dt;



              
            }
            catch (Exception ex){

               lbconexion.Text =  ex.Message;

            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dtTrab = objTrab.GestionHuella(new byte[] { }, new byte[] { }, txtIdTrab.Text, "", this.Name, 5);

            foreach (DataRow row in dtTrab.Rows)
            {
                string sIdTrab = "";
                byte[] byteArray = new byte[] { };
                byteArray = (byte[])row["template"];
                sIdTrab = row["idTrab"].ToString();
                string sTmp  = Convert.ToBase64String(byteArray);
                //int i = BitConverter.(byteArray, 0);
                string i2 = BitConverter.ToString(byteArray, 0);
                int bytes = byteArray.Length;
                foreach (DataGridViewRow dgvrow in dgv.Rows) {

                    if (dgvrow.Cells[0].Value.ToString() == sIdTrab) {

                       // bool b = objCZKEM.SetUserTmpExStr(1, sIdTrab, 0,1, sTmp);
                        string TemplateZKTeco = dgvrow.Cells["Template"].Value.ToString();
                        byte[] decBytes1 = Encoding.UTF8.GetBytes(TemplateZKTeco);
                        //byte[] bytes = new byte[TemplateZKTeco.Length * sizeof(char)];
                        //System.Buffer.BlockCopy(TemplateZKTeco.ToCharArray(), 0, bytes, 0, bytes.Length);

                        //bool b = Utilerias.CompararBytes(byteArray, bytes);
                    }


                }

              

            }

            }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvrow in dgv.Rows)
            {

                if (dgvrow.Cells[0].Value.ToString() == txtIdTrab.Text)
                {

                    // bool b = objCZKEM.SetUserTmpExStr(1, sIdTrab, 0,1, sTmp);
                    string TemplateZKTeco = dgvrow.Cells["Template"].Value.ToString();
                    byte[] decBytes1 = Encoding.UTF8.GetBytes(TemplateZKTeco);

                    SonaTrabajador objTrab = new SonaTrabajador();
                    DataTable dtTrab = objTrab.GestionHuella(decBytes1, new byte[] { }, "1", "", this.Name, 3);
                    dtTrab = objTrab.GestionHuella(decBytes1, new byte[] { }, "1", "Prueba", this.Name, 1);
                    //byte[] bytes = new byte[TemplateZKTeco.Length * sizeof(char)];
                    //System.Buffer.BlockCopy(TemplateZKTeco.ToCharArray(), 0, bytes, 0, bytes.Length);
                    break;
                    //bool b = Utilerias.CompararBytes(byteArray, bytes);
                }
            }
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
        


    