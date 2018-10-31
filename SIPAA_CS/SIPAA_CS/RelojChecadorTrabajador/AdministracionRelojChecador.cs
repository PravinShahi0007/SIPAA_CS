using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using SIPAA_CS.RecursosHumanos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zkemkeeper;
using static SIPAA_CS.App_Code.SonaCompania;
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
        public CZKEMClass objCZKEM2 = new CZKEMClass();

        private LinkedList<FaceTmp> faces;
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
        BackgroundWorker bd = new BackgroundWorker();

        public string Mensaje;
        //BarraProgreso frm = new BarraProgreso();
        //BarraProgreso frm = new BarraProgreso();

        private void SplashHuella_Load(object sender, EventArgs e)
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

            if (Valor==1)
            {
                btnHuella.Enabled = true;
                btnSync.Enabled = true;
            }
           
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            bd.DoWork += Bd_DoWork;
            bd.RunWorkerCompleted += Bd_RunWorkerCompleted;
            LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
            btnGuardar.Image = global::SIPAA_CS.Properties.Resources.Reloj;
            button1.Image = global::SIPAA_CS.Properties.Resources.Persona;
            btnReloj.Image = global::SIPAA_CS.Properties.Resources.RelojSync;
            button2.Image = global::SIPAA_CS.Properties.Resources.RelojSync;
            button2.Enabled = true;
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

                    //panelAccion.Enabled = true;
                    Deshabilita_Botones(true);
                    btnAdmin.Enabled = false;
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
                // panelAccion.Enabled = false;
                Deshabilita_Botones(false);

            }
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
            //////////////////////// 
            // estas de aqui abajo las oculte como vista previa, porque el usuario no necesita esos datos y no sirven para nada
            dgvReloj.Columns["teclado"].Visible = false;
            dgvReloj.Columns["huella"].Visible = false;
            dgvReloj.Columns["IP"].Visible = false;
            dgvReloj.Columns["Rostro"].Visible = false;
            ///////////////////////
            dgvReloj.Columns["Usuario Sincronizó Asistencias"].Visible = false;
            dgvReloj.Columns["Usuario Sincronizó Usuarios"].Visible = false;
            for (int i = 0; i < dgvReloj.Columns.Count; i++)
                dgvReloj.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

           /* dgvReloj.Columns[0].Width = 90;
            dgvReloj.Columns[2].Width = 220;
            dgvReloj.Columns[10].Width = 150;
            dgvReloj.Columns[11].Width = 150;
            dgvReloj.ClearSelection();
            */

            foreach (DataGridViewRow row in dgvReloj.Rows)
            {
                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                row.Cells[0].Tag = "uncheck";
            }
        }

        public bool Connect_Net(string IPAdd, int Port)
        {
            //objCZKEM.PullMode = 1;
            if (objCZKEM.Connect_Net(IPAdd, Port))
            {
                //if (objCZKEM.RegEvent(1, 65535))
                //{

                //    objCZKEM.OnConnected += ObjCZKEM_OnConnected;
                //    objCZKEM.OnDisConnected += objCZKEM_OnDisConnected;
                //    objCZKEM.OnEnrollFinger += ObjCZKEM_OnEnrollFinger;
                //    objCZKEM.OnFinger += ObjCZKEM_OnFinger;
                //    //objCZKEM.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                //    objCZKEM.RegEvent(1, 32767);
                //}
                objCZKEM.RegEvent(1, 65535);

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

                if (ltReloj.Count > 0)
                {
                    //panelAccion.Enabled = true;
                    Deshabilita_Botones(true);
                    Reloj obj1 = ltReloj.ElementAt(0);
                    TrabajadorInfo.cvReloj = obj1.cvReloj;

                    ValidarBotones(ValidarObjectos(ltReloj, "Teclado"), "Teclado");
                    ValidarBotones(ValidarObjectos(ltReloj, "Rostro"), "Rostro");
                    ValidarBotones(ValidarObjectos(ltReloj, "Huella"), "Huella");

                    if (ltReloj.Count > 1)
                    {
                        btnAdmin.Enabled = false;
                    }
                    else
                    {
                        btnAdmin.Enabled = true;
                    }
                }
                else
                   Deshabilita_Botones(false);
               




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

        private void Deshabilita_Botones(bool Bandera)
        {
            if (Bandera)

                btnDescarga.Enabled =  btnAdmin.Enabled = button1.Enabled = true;

            else

                btnDescarga.Enabled =  btnAdmin.Enabled = button1.Enabled = false;

        }




        public void ValidarBotones(bool bBandera, string Opcion)
        {


            switch (Opcion)
            {

                case "Teclado": if (bBandera != true) { btnKey.Enabled = false; } else { btnKey.Enabled = true; } break;

                case "Rostro": if (bBandera != true) { btnFace.Enabled = false; } else { btnFace.Enabled = true; } break;

               
            }

        }

        public bool ValidarObjectos(List<Reloj> ltReloj, string Boton)
        {

            bool bBandera = true;

            foreach (Reloj obj in ltReloj)
            {

                switch (Boton)
                {

                    case "Teclado":
                        if (obj.Teclado != true) { bBandera = false; };
                        break;

                    case "Rostro":
                        if (obj.Rostro != true) { bBandera = false; };
                        break;

                    case "Huella":
                        if (obj.Huella != true) { bBandera = false; };
                        break;
                }


            }

            return bBandera;


        }

        private void btnDescarga_Click(object sender, EventArgs e)
        {
          
            Deshabilita_Botones(false);
            pnlMensaje.Visible = true;
          
            panelTag.Visible = false;

            if (ltReloj.Count > 0)
            {
                
                pnlMensaje.Enabled = true;
               
                pnlMensaje.Enabled = false;
                string sIdTrab;
                sIdTrab = String.Empty;
                int sVerify, iModoCheck, iAnho, iDia, iMes, iHora, iMinuto, iSegundo, iWorkCode, iCont;
                sVerify = iModoCheck = iAnho = iDia = iMes = iHora = iMinuto = iSegundo = iWorkCode = iCont = 0;
                
                bool bBandera = false;
                try
                {
                    foreach (Reloj obj in ltReloj)
                    {
                        DialogResult Resultado = MessageBox.Show("El reloj " + obj.Descripcion.ToString() + " tuvo una descarga de asistencia  \nen la fecha:   " + obj.UltimaDescargaAsistencia + " por el usuario " + obj.UsuDescargaAsistencia + " \n¿Desea Sincronizarlo de nuevo?", "SIPPA", MessageBoxButtons.YesNo);
                        if (Resultado == DialogResult.Yes)
                        {
                          
                            iCont += 1;
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                       
                            pnlMensaje.Enabled = false;
                            bool bConexion = Connect_Net(obj.IpReloj, 4370);
                            objCZKEM.ReadAllUserID(1);
                            objCZKEM.ReadAllTemplate(1);
                            if (bConexion)
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando Asistencias");
                                if (objCZKEM.ReadAllGLogData(1))
                                {
                                    //int iContReg = 0;
                                    while (objCZKEM.SSR_GetGeneralLogData(1, out sIdTrab, out sVerify, out iModoCheck, out iAnho
                                                                                                         , out iMes, out iDia, out iHora, out iMinuto, out iSegundo
                                                                                                         , ref iWorkCode))
                                    {
                                       // iContReg += 1;
                                        bBandera = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, obj.cvReloj, iModoCheck);
                                       // if (!bBandera)
                                        //    MessageBox.Show(" Error en  "+sIdTrab+" "+iAnho+" "+ iMes+ " "+iDia +" "+iHora+" "+ obj.cvReloj+ " "+iModoCheck );

                                    }
                                }
                                //ELimina automaticamente los registros de la asistencia en el reloj checador 
                                objCZKEM.ClearData(1, 1);
                                objCZKEM.Disconnect();
                               // progressBar1.Value = 90;
                                if (bBandera)
                                {
                                    pnlMensaje.Enabled = true;
                                    RelojChecador objReloj = new RelojChecador();
                                    objReloj.obtrelojeschecadores(7, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros de asistencia guardados correctamente");
                                   
                                    pnlMensaje.Enabled = false;

                                }
                                else
                                {
                                    pnlMensaje.Enabled = true;
                                    
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se guardaron correctamente repita el proceso");
                                    pnlMensaje.Enabled = false;

                                }


                            }
                            else
                            {
                                pnlMensaje.Enabled = true;
                               
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse al reloj : " + obj.Descripcion);
                                pnlMensaje.Enabled = false;
                               
                            }
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descarga de asistencia cancelada para el reloj " + obj.Descripcion);
                        }

                    }
                    // limpiarReloj("Reloj");
                }
                catch (Exception ex)
                {
                    pnlMensaje.Enabled = true;
                   
                    MessageBox.Show(ex.ToString());
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, ex.Message);
                    pnlMensaje.Enabled = false;
                }
                finally
                {
                    //objCZKEM.Disconnect();
                    timer1.Start();
                }
                ltReloj.Clear();
                LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
             

            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha seleccionado algún Registro.");
                panelTag.Update();
                timer1.Start();

            }

        }


        public bool IngresarRegistro(string sIdTrab, int Year, int Month, int Day, int Hour, int Minute, int Second, int cvReloj, int iTipoCheck)
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


            switch (iTipoCheck)
            {
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
                case 4:
                    iTipoCheck += 1;
                    break;
                case 5:
                    iTipoCheck += 1;
                    break;

            }

            DataTable dt = objTrab.Relojchecador(sIdTrab, 5, DateTime.Parse(sFecha), iTipoCheck, tpHora, cvReloj, 2, LoginInfo.IdTrab, Name);

            if (dt != null)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando...");
                panelTag.Update();
                return true;
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error en el guardado de Datos, intente de nuevo");
                panelTag.Update();
                return false;
            }

        }

        private class FaceTmp
        {
            public string idtrab;
            public int index;
            public string rostroTmp;
            public int rostrolong;

            public FaceTmp(string Idtrab, int Index, string RostroTmp, int Rostrolong)
            {
                this.idtrab = Idtrab;
                this.index = Index;
                this.rostroTmp = RostroTmp;
                this.rostrolong = Rostrolong;
            }
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            if (ltReloj.Count > 0)
            {
                foreach (Reloj obj in ltReloj)
                {



                    ////////////////////////////////////////////////////////////////////////
                    #region InsertaBiometricos
                    DialogResult result = MessageBox.Show("Reloj " + obj.Descripcion + "\nÚltima sincronización: " + obj.UltimaSincronizacion + "\nUsuario sincronizó: " + obj.UltimoUsuarioSinc + " \n\n¿Esta Seguro que desea SINCRONIZAR la información del sistema?\nEsto eliminará la información del dispositivo y será sustituida con la información del sistema", "SIPPA", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                        continue;


                    Cursor = Cursors.WaitCursor;



                    int iCont = 0;
                    RelojChecador objReloj = new RelojChecador();
                    DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 17, "%", "%");
                    iCont += 1;
                    //contadores:
                    int regHuella = 0;
                    int regGpos = 0;
                    int regFace = 0;
                    int counter = 0;


                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                    pnlMensaje.Enabled = false;
                   // System.Threading.Thread.Sleep(20);

                    bool bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                    if (bConexion)
                    {


                         bool ClearData = objCZKEM.ClearData(1, 5);
                        bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);

                        #region InsertaHuellas

                        // progressBar1.Maximum = dt.Rows.Count;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando huellas.. ");

                        foreach (DataRow row in dt.Rows)
                        {
                            string idtrab = row["idtrab"].ToString();
                            string Nombre = row["Nombre"].ToString();
                            int Permiso = 0;
                            string pass_desc = string.Empty;


                            if (!string.IsNullOrEmpty(row["pass"].ToString()))
                                pass_desc = Utilerias.descifrar(row["pass"].ToString());
                            if (Convert.ToBoolean(row["administrador"].ToString()))
                                Permiso = 3;

                            if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true))
                            {
                                if (row["huellaTmp"].ToString() != String.Empty)
                                {
                                    int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                                    string tmpHuella = "";

                                    if (ifinger >= 0 && ifinger <= 9)
                                    {
                                        tmpHuella = row["huellaTmp"].ToString();

                                        if (objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella))
                                        {
                                            regHuella += 1;
                                            counter += 1;

                                        }
                                    }
                                }
                                else
                                    counter += 1;

                            }
                        }
                        // System.Threading.Thread.Sleep(20);
                        #endregion

                        bool BatchUpdate = objCZKEM.BatchUpdate(1);
                        bool RefreshData = objCZKEM.RefreshData(1);
                        objCZKEM.Disconnect();

                    }
                    else
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse al reloj : " + obj.Descripcion);
                        pnlMensaje.Enabled = false;
                        progressBar1.Visible = false;
                        pnlMensaje.Enabled = true;
                    }

                    #region InsertaGrupos
                    bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                    if (bConexion)
                    {
                        bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando grupos..");
                        System.Threading.Thread.Sleep(20);
                        dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 18, "%", "%");
                        foreach (DataRow row in dt.Rows)
                        {
                            string idtrab = row["idtrab"].ToString();
                            int Grupo = Convert.ToInt32(row["cvforma"].ToString());

                            if (objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo))
                            {

                                try
                                {
                                    bool bandera = objCZKEM.SendFile(1, @"\\192.168.30.171\FotosJS\FotosRelojChecador\" + idtrab + ".jpg");

                                }
                                catch
                                {
                                }
                                regGpos += 1;
                                counter += 1;

                            }
                        }


                        objCZKEM.BatchUpdate(1);
                        objCZKEM.RefreshData(1);
                        objCZKEM.Disconnect();
                    }

                    else
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse al reloj : " + obj.Descripcion);
                        pnlMensaje.Enabled = false;
                        progressBar1.Visible = false;
                        pnlMensaje.Enabled = true;
                    }




                    #endregion

                    //Obteniendo rostros...
                    counter = 0;
                    faces = new LinkedList<FaceTmp>();
                    dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 19, "%", "%");

                    foreach (DataRow row in dt.Rows)
                    {
                        string idtrab = row["idtrab"].ToString();
                        if (row["rostroTmp"].ToString() != String.Empty)
                        {
                            string RostroTmp = row["rostroTmp"].ToString();
                            int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                            faces.AddLast(new FaceTmp(idtrab, 50, RostroTmp, rostrolong));
                            counter += 1;

                        }
                        else
                            counter += 1;
                    }

                    //Insertando rostros...
                    bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                    if (bConexion)
                    {
                        //Reiniciando dispositivo...
                        objCZKEM.RestartDevice(1);
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espere un momento por favor");
                        pnlMensaje.Enabled = false;
                        System.Threading.Thread.Sleep(60000);


                    }

                    bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                    if (bConexion)
                    {
                        counter = 0;
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando rostros...");
                        foreach (FaceTmp ft in faces)
                        {
                            if (objCZKEM.SetUserFaceStr(1, ft.idtrab, ft.index, ft.rostroTmp, ft.rostrolong))
                            {

                                regFace += 1;
                                counter += 1;

                            }

                        }

                        objCZKEM.BatchUpdate(1);
                        objCZKEM.RefreshData(1);
                        objCZKEM.Disconnect();


                    }


                    if (regHuella > 0 && regGpos > 0 && regFace > 0)
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Proceso finalizado para el reloj: " + obj.Descripcion);
                        pnlMensaje.Enabled = false;
                        objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);//guarda usuario última sincronización
                        System.Threading.Thread.Sleep(1000);
                        MessageBox.Show("Reloj: " + obj.Descripcion + "\nProceso terminado\nHuellas: " + (regHuella) + "\nGrupos: " + (regGpos) + "\nRostros: " + (regFace), "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error en la inserción de datos");
                        pnlMensaje.Enabled = false;
                        System.Threading.Thread.Sleep(1000);
                        MessageBox.Show("Reloj: " + obj.Descripcion + "\nError en la inserción de datos\nHuellas: " + (regHuella > 0 ? "OK" : "Falló") + "\nGrupos: " + (regGpos > 0 ? "OK" : "Falló") + "\nRostros: " + (regFace > 0 ? "OK" : "Falló"), "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }


                    #endregion
                }


                Enabled = true;
                ltReloj.Clear();
                LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
                timer1.Start();
                Cursor = Cursors.Default;
            }
            else
            {
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");
                pnlMensaje.Enabled = false;
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            // panelAccion.Enabled = true;
            pnlMensaje.Visible = progressBar1.Visible = panelTag.Visible = false;
            pnlMensaje.Enabled = progressBar1.Enabled = panelTag.Enabled = false;
            progressBar1.Visible = false;
            pnlMensaje.Visible = false;
            timer1.Stop();
        }


        public void limpiarReloj(string sOpcion)
        {

            pnlMensaje.Visible = true;
            int iCont = 0;
            //panelAccion.Enabled = false;
            Deshabilita_Botones(false);
            progressBar1.Visible = true;
            progressBar1.Value = 20;
            bool bBandera = false;
            foreach (Reloj obj in ltReloj)
            {
                iCont += 1;
                // pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                // panelTag.Update();
                progressBar1.Value = 40;
                //pnlMensaje.Enabled = false;
                bool bConexion = Connect_Net(obj.IpReloj, 4370);

                if (bConexion != false)
                {
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Eliminando Registros");
                    // panelTag.Update();
                    // pnlMensaje.Enabled = false;
                    progressBar1.Value = progressBar1.Value + (10 / ltReloj.Count);

                    switch (sOpcion)
                    {

                        case "Huella":
                            if (!objCZKEM.ClearData(1, 2)) { bBandera = true; }

                            break;

                        case "Reloj":
                            if (!objCZKEM.ClearData(1, 1)) { bBandera = true; }
                            break;

                        case "Trab":
                            if (ltReloj.Count > 0)
                            {

                                if (!objCZKEM.ClearData(1, 5)) { bBandera = true; }
                                else
                                {

                                    foreach (Reloj objR in ltReloj)
                                    {
                                        try
                                        {
                                            RelojChecador objReloj = new RelojChecador();
                                            objReloj.RelojesxTrabajador("", objR.cvReloj, 10, "", "");

                                        }
                                        catch { }
                                    }

                                }

                            }
                            break;

                    }

                }

                objCZKEM.Disconnect();
                progressBar1.Value = 90;
            }

            if (bBandera != false)
            {
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "En menos un Dispositivo no fue posible realizar la limpieza. Repetir Proceso");
                pnlMensaje.Enabled = false;
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Eliminación correcta.");
            }
            progressBar1.Value = 100;
            pnlMensaje.Enabled = true;
            timer1.Start();
            // panelAccion.Enabled = true;
            Deshabilita_Botones(true);
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
            //try
            //{

            //    List<UserInfo> lstFPTemplates = new List<UserInfo>();
            //    SonaTrabajador objTrab = new SonaTrabajador();
            //    DataTable dtTrab = objTrab.GestionHuella(new byte[] { }, new byte[] { }, "", "", this.Name, 4);
            //    bool bBandera = false;

            //    string sIdTrab = String.Empty;
            //    string sNombre = String.Empty;
            //    string sPass = String.Empty;
            //    int iPrivilegio = 0;
            //    bool bActivo = false;
            //    int iFinger = 0;
            //    int iFlag = 0;
            //    string sTemplate = String.Empty;
            //    int iTemplatelength = 0;
            //    objCZKEM.ReadAllUserID(1);
            //    objCZKEM.ReadAllTemplate(1);
            //    DataTable dt = new DataTable();
            //    dt.Clear();
            //    dt.Columns.Add("IdTrab");
            //    dt.Columns.Add("Nombre");
            //    dt.Columns.Add("Dedo");
            //    dt.Columns.Add("Flag");
            //    dt.Columns.Add("Template");
            //    dt.Columns.Add("Tamaño");
            //    while (objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
            //    {
            //        for (iFinger = 0; iFinger < 10; iFinger++)
            //        {
            //            if (objCZKEM.GetUserTmpExStr(1, sIdTrab, iFinger, out iFlag, out sTemplate, out iTemplatelength))
            //            {


            //                DataRow row = dt.NewRow();
            //                row["IdTrab"] = sIdTrab;
            //                row["Nombre"] = sNombre;
            //                row["Dedo"] = iFinger;
            //                row["Flag"] = iFlag;
            //                row["Template"] = sTemplate;
            //                row["Tamaño"] = iTemplatelength;
            //                dt.Rows.Add(row);
            //                lstFPTemplates.Add(fpInfo);
            //            }
            //        }
            //    }

            //    dgv.DataSource = dt;




            //}
            //catch (Exception ex)
            //{

            //    lbconexion.Text = ex.Message;

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
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
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

            LlenarGrid(6, 0, desc, ip, "%", 0, "", "");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void Bd_DoWork(object sender, DoWorkEventArgs e)
        {

            BarraProgreso.Dialog(Mensaje);


        }

        private void Bd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            BarraProgreso.Dialog();
        }

        private void btnAdmin_Click_1(object sender, EventArgs e)
        {
            AdministracionUsuariosReloj form = new AdministracionUsuariosReloj();
            form.Show();
            this.Close();
        }

        private void btnFace_Click(object sender, EventArgs e)
        {


            string Ubicacion = @"\\172.165.1.10\FotosJS\FotosRelojChecador\";
            EnviaRecursosCarpeta(Ubicacion, false);

        }

        private void btnHuella_Click(object sender, EventArgs e)
        {
            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Comienza proceso, no apriete otra vez el boton " );
            ProcesoReloj();
            // ProcesoReloj("Face");
            //ProcesoReloj("Pass");
            ltReloj.Clear();
            LlenarGrid(6, 0, "%", "%", "%", 0, "", "");

        }
        private void btnKey_Click(object sender, EventArgs e)
        {
            //ProcesoReloj("Pass");
            ltReloj.Clear();
        }


        public static byte[] digitArr(byte n)
        {
            if (n == 0) return new byte[1] { 0 };

            var digits = new List<byte>();

            for (; n != 0; n /= 10)
                digits.Add(n);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }


        public void ProcesoReloj()
        {

            if (ltReloj.Count > 0)
            {
               
                objCZKEM = new CZKEMClass();
                int iCont = 0;
                pnlMensaje.Visible  = panelTag.Visible = true;
                pnlMensaje.Enabled  = panelTag.Enabled = true;

                foreach (Reloj obj in ltReloj)
                {

                    RelojChecador objReloj = new RelojChecador();
                    DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 11, "%", "%");// tenia la opcion6
                    if (dt.Rows.Count < 1)
                    {

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Este Reloj no tiene Personal Asignado.");

                        break;
                    }
                    iCont += 1;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                 
                    bool bConexion = Connect_Net(obj.IpReloj, 4370);
                    if (bConexion != false)
                    {
                        objCZKEM.ReadAllUserID(1);
                        objCZKEM.ReadAllTemplate(1);
                        bool bBanderaPass, bBanderaHuella, bBanderaFace;
                        bBanderaPass = bBanderaHuella = bBanderaFace = false;
                        int iContTrab = 0;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo contraseñas de  los trabajadores  " );
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();  
                            bBanderaPass = ConsultaReloj("Pass", idTrab, iContTrab, dt.Rows.Count);

                           

                        }
                        iContTrab = 0;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo huellas de  los trabajadores  ");
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                           
                            bBanderaHuella = ConsultaReloj("Huella", idTrab, iContTrab, dt.Rows.Count);
                           
                        }
                        iContTrab = 0;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo rostros de  los trabajadores  ");
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                          
                            bBanderaFace = ConsultaReloj("Face", idTrab, iContTrab, dt.Rows.Count);
                          
                        }
                        objCZKEM.Disconnect();
                   

                    }
                    else
                    {
                       
                        MessageBox.Show("No fue posible conectarse al reloj, contacte al personal del área de sistemas", "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        timer1.Start();
                    }
                     MessageBox.Show("Proceso terminado", "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
               
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
            int iFaceLong = 0;
            bool bBandera = false;

            switch (Opcion)
            {

                case "Face":


                    if (objCZKEM.GetUserFaceStr(1, idtrab, 50, ref sFaceTmp, ref iFaceLong))
                    {
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, "", sFaceTmp, iFaceLong.ToString(), LoginInfo.IdTrab, Name, 7);
                            bBandera = true;
                        }
                        catch { }


                    }

                    break;

                case "Huella":
                    int flag = 0;
                    string huellatmp = "";
                    int tpmlong = 0;
                    string sIdTrab = idtrab;
                    string sNombre = "";
                    string sPass = "";
                    int iPrivilegio = 0;
                    bool bActivo = false;

                    while (objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
                    {
                        for (int iFinger = 0; iFinger < 10; iFinger++)
                        {
                            if (objCZKEM.GetUserTmpExStr(1, sIdTrab, iFinger, out flag, out huellatmp, out tpmlong))
                            {
                                SonaTrabajador objTrab = new SonaTrabajador();
                                try
                                {
                                    objTrab.GestionHuella(sIdTrab, huellatmp, iFinger, LoginInfo.IdTrab, Name, 5);
                                    bBandera = true;
                                }
                                catch { }
                            }
                        }
                    }
                    break;



                case "Pass":

                    string Nombre = "";
                    string Pass = "";
                    iPrivilegio = 0;
                    bActivo = false;


                    string Cifrado = "";
                    if (objCZKEM.SSR_GetUserInfo(1, idtrab, out Nombre, out Pass, out iPrivilegio, out bActivo))
                    {
                        if (Pass != String.Empty)
                            Cifrado = Utilerias.cifrarPass(Pass, 1);
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, Cifrado, "", "0", LoginInfo.IdTrab, this.Name, 6);
                            bBandera = true;
                        }
                        catch { }

                    }
                    break;
            }

            return bBandera;
        }

        private void btnReloj_Click(object sender, EventArgs e)
        {
            CapturaHora frm = new CapturaHora(ltReloj);
            frm.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea Eliminar los Registros de Asistencia?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                limpiarReloj("Reloj");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea Eliminar los Registros de los Trabajadores?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                limpiarReloj("Trab");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea Eliminar los Registros de Huella de los Trabajadores?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                limpiarReloj("Huella");
            }
        }

        private void panelTag_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // DateTime now = DateTime.Now;
            DialogResult Resultado = MessageBox.Show("Este proceso sincronizara todos los dispositivos con la misma hora ¿Desea continuar? ", "SIPAA", MessageBoxButtons.YesNo);
            if (Resultado == DialogResult.Yes)
                SincronizaHora();
        }

        private void SincronizaHora()
        {
            RelojChecador objReloj = new RelojChecador();
            DataTable dt = objReloj.obtrelojeschecadores(6, 0, "%", "%", "%", 0, "%", "%", LoginInfo.IdTrab, LoginInfo.IdTrab);


            if (ltReloj.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo  " + row[1].ToString());
                    progressBar1.Value = 40;
                    bool bBandera = false;
                    bool bConect = objCZKEM.Connect_Net(row[2].ToString(), 4370);
                    if (bConect)
                    {

                        DataTable dt2 = objReloj.obtrelojeschecadores(13, 0, "%", "%", "%", 0, "%", "%", LoginInfo.IdTrab, LoginInfo.IdTrab);
                        string HoraServidor = string.Empty;
                        string FechaServidor = string.Empty;
                        foreach (DataRow Row in dt2.Rows)
                        {
                            FechaServidor = Row[0].ToString();
                            HoraServidor = Row[1].ToString();
                        }

                        progressBar1.Value = progressBar1.Value + (10 / dt.Columns.Count);
                        int Año, Mes, Dia, Hora, Minuto, Segundo;
                        Año = Mes = Dia = Hora = Minuto = Segundo = 0;
                        Año = Convert.ToInt32(FechaServidor.Substring(6, 2));
                        Mes = Convert.ToInt32(FechaServidor.Substring(3, 2));
                        Dia = Convert.ToInt32(FechaServidor.Substring(0, 2));
                        Hora = Convert.ToInt32(HoraServidor.Substring(0, 2));
                        Minuto = Convert.ToInt32(HoraServidor.Substring(3, 2));
                        Segundo = Convert.ToInt32(HoraServidor.Substring(6, 2));
                        if (!objCZKEM.SetDeviceTime2(1, Año, Mes, Dia, Hora, Minuto, Segundo))
                            bBandera = true;
                        if (bBandera)
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error al sincronizar la hora con el dispositivo: " + row[1].ToString());
                        else
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Hora sincronizada correctamente en el dispositivo: " + row[1].ToString());
                        objCZKEM.Disconnect();
                    }
                }



            }
            else
              Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No existen relojes para sincronizar.");



        }

        private void button4_Click(object sender, EventArgs e)
        {



            string Ubicacion = @"\\172.165.1.10\FotosJS\FotosEmpleados\";
            EnviaRecursosCarpeta(Ubicacion, true);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Ubicacion = @"\\172.165.1.10\FotosJS\NOI\";
            EnviaRecursosCarpeta(Ubicacion, false); 

           
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string Ruta = @"\\172.165.1.10\FotosJS\FotosEmpleados";
            ObtieneRecursosCarpeta(Ruta);
           // EnviaRecursosCarpeta(Ruta, false);

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            string Ruta = @"\\172.165.1.10\FotosJS\FotosEmpleados\NOI";
            ObtieneRecursosCarpeta(Ruta);
            
        }


        public void EnviaRecursosCarpeta(string Ubicacion, bool bandera)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.Title = "Seleccione las fotografias";
            openFileDialog1.Multiselect = true;
            DialogResult Resultado = openFileDialog1.ShowDialog();

            if (Resultado == DialogResult.OK)
            {
                int Contador = 0;
                int ContadorRepetidas = 0;

                foreach (String file in openFileDialog1.FileNames)
                {
                    string Origen = Path.GetFullPath(file);
                    string Nombre = Path.GetFileName(file);
                    string Cadena1 = @"\\172.165.1.10\FotosJS\FotosEmpleados\";
                    string Cadena2 = Ubicacion;
                   // string Cadena3 = @"\\172.165.1.10\sipaa_web\img\Fotos\" + Nombre; 
                    string Destino = Ubicacion + Nombre;
                    

                    if (File.Exists(Destino))
                    {
                        if (ContadorRepetidas >= 1)
                        {
                            DialogResult Result2 = MessageBox.Show("¿Desea sobreescribir el resto de los archivos? ", "SIPAA", MessageBoxButtons.YesNo);
                            if (Result2 == DialogResult.Yes)
                            {
                                foreach (String file2 in openFileDialog1.FileNames)
                                {
                                    string Origen2 = Path.GetFullPath(file2);
                                    string Nombre2 = Path.GetFileName(file2);
                                    string Destino2 = Ubicacion + Nombre2;
                                    File.Copy(Origen2, Destino2, true);
                                    if(Cadena1== Cadena2)
                                    {
                                        EnviaSicea(Nombre2, Origen2);
                                        //File.Copy(Origen2,Cadena3, true);
                                    }
                               
                                    Contador += 1;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Subiendo la fotografía " + Contador + " de " + openFileDialog1.FileNames.Length.ToString());

                                }
                                break;
                            }
                            else
                            {
                                DialogResult Result = MessageBox.Show("La fotografía " + Nombre + " ya existe \n ¿Desea sobrescribir el archivo?", "SIPAA", MessageBoxButtons.YesNo);
                                if (Result == DialogResult.Yes)
                                {

                                    File.Copy(Origen, Destino, true);
                                    if (Cadena1 == Cadena2)
                                    {
                                        EnviaSicea(Nombre, Origen);
                                        //File.Copy(Origen, Cadena3, true);
                                    }
                                   
                                    Contador += 1;
                                    ContadorRepetidas += 1;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Subiendo la fotografía " + Contador + " de " + openFileDialog1.FileNames.Length.ToString());

                                }
                                else
                                {
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "La fotografía " + Nombre + "  no se sobrescribio ");
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            DialogResult Result = MessageBox.Show("La fotografía " + Nombre + " ya existe \n ¿Desea sobrescribir el archivo?", "SIPAA", MessageBoxButtons.YesNo);
                            if (Result == DialogResult.Yes)
                            {
                                File.Copy(Origen, Destino, true);
                                if (Cadena1 == Cadena2)
                                {
                                    EnviaSicea(Nombre, Origen);
                                    //File.Copy(Origen, Cadena3, true);
                                }
                                Contador += 1;
                                ContadorRepetidas += 1;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Subiendo la fotografía " + Contador + " de " + openFileDialog1.FileNames.Length.ToString());

                            }
                            else
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "La fotografía " + Nombre + "  no se sobrescribio ");
                                continue;
                            }
                        }

                    }
                    else
                    {
                        File.Copy(Origen, Destino);
                        if (Cadena1 == Cadena2)
                        {
                            EnviaSicea(Nombre, Origen);
                            //File.Copy(Origen, Cadena3);
                        }
                        Contador += 1;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Subiendo la fotografía " + Contador + " de " + openFileDialog1.FileNames.Length.ToString());
                    }

                }
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Fotografias subidas exitosamente");
                MessageBox.Show("Fotografias subidas exitosamente");
            }
        }

        public void ObtieneRecursosCarpeta(string Ruta)
        {
            openFileDialog2.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog2.InitialDirectory = Ruta;
            openFileDialog2.Title = "Seleccione";
            openFileDialog2.Multiselect = true;
            DialogResult Resultado = openFileDialog2.ShowDialog();

            if (Resultado == DialogResult.OK)
            {
                int Contador = 0;


                DialogResult Resultado2 = folderBrowserDialog.ShowDialog();

                if (Resultado2 == DialogResult.OK)
                {
                    string Ubicacion = folderBrowserDialog.SelectedPath;
                    int ContadorRepetidas = 0;
                    foreach (String file in openFileDialog2.FileNames)
                    {

                        string Origen = Path.GetFullPath(file);
                        string Nombre = Path.GetFileName(file);
                        string Destino = Ubicacion + @"\" + Nombre;
                        if (File.Exists(Destino))
                        {
                            if (ContadorRepetidas >= 1)
                            {
                                DialogResult Result2 = MessageBox.Show("¿Desea sobreescribir el resto de los archivos? ", "SIPAA", MessageBoxButtons.YesNo);
                                if (Result2 == DialogResult.Yes)
                                {
                                    foreach (String file2 in openFileDialog2.FileNames)
                                    {
                                        string Origen2 = Path.GetFullPath(file2);
                                        string Nombre2 = Path.GetFileName(file2);
                                        string Destino2 = Ubicacion + Nombre2;
                                        File.Copy(Origen2, Destino2, true);
                                        Contador += 1;
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando la fotografía " + Contador + " de " + openFileDialog2.FileNames.Length.ToString());

                                    }
                                    break;
                                }
                                else
                                {
                                    DialogResult Result = MessageBox.Show("La fotografía " + Nombre + " ya existe \n ¿Desea sobrescribir el archivo?", "SIPAA", MessageBoxButtons.YesNo);
                                    if (Result == DialogResult.Yes)
                                    {

                                        File.Copy(Origen, Destino, true);
                                        Contador += 1;
                                        ContadorRepetidas += 1;
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando la fotografía " + Contador + " de " + openFileDialog2.FileNames.Length.ToString());

                                    }
                                    else
                                    {
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "La fotografía " + Nombre + "  no se sobrescribio ");
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                DialogResult Result = MessageBox.Show("La fotografía " + Nombre + " ya existe \n ¿Desea sobrescribir el archivo?", "SIPAA", MessageBoxButtons.YesNo);
                                if (Result == DialogResult.Yes)
                                {
                                    File.Copy(Origen, Destino, true);
                                    Contador += 1;
                                    ContadorRepetidas += 1;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando la fotografía " + Contador + " de " + openFileDialog2.FileNames.Length.ToString());

                                }
                                else
                                {
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "La fotografía " + Nombre + "  no se sobrescribio ");
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            File.Copy(Origen, Destino);
                            Contador += 1;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando la Fotografía " + Contador + " de " + openFileDialog2.FileNames.Length.ToString());
                        }
                    }
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Fotografias descargadas exitosamente");
                    MessageBox.Show("Fotografias descargadas exitosamente");

                }
            }
        }
        /*********************************************/
        public void EnviaSicea( string Nombre, string Origen)
        {
            DateTime Datetime = DateTime.Now;
            TimeSpan Ts = TimeSpan.FromHours(1);
            SonaTrabajador objTrab = new SonaTrabajador();
            string NombreConcatenado = string.Empty;

            int index = Nombre.IndexOf('.');
            DataTable dt = objTrab.Relojchecador(Nombre.Substring(0, index), 6, Datetime, 0, Ts, 0, 0, LoginInfo.IdTrab, Name);


            string Descripcion = dt.Rows[0].ItemArray[0].ToString();
            if (Descripcion == "PROFESOR" || Descripcion == "PROFESOR DE ESPECIALIDAD" || Descripcion == "PROFESOR DE MAESTRIA" || Descripcion == "PROFESOR DE SEMINARIO" || Descripcion == "PROFESOR MEDIO TIEMPO" || Descripcion == "PROFESOR TIEMPO COMPLETO" || Descripcion == "TUTOR DE LICENCIATURA")
            {
                string UbicacionSicea = @"\\192.168.11.2\sicea\Proccujs\Empleados\Fotos";
                if (Nombre.Substring(0, index).Length == 1)
                    NombreConcatenado = "00000" + Nombre.Substring(0, index) + ".jpg";
                else if (Nombre.Substring(0, index).Length == 2)
                    NombreConcatenado = "0000" + Nombre.Substring(0, index) + ".jpg";
                else if (Nombre.Substring(0, index).Length == 3)
                    NombreConcatenado = "000" + Nombre.Substring(0, index) + ".jpg";
                else if (Nombre.Substring(0, index).Length == 4)
                    NombreConcatenado = "00" + Nombre.Substring(0, index) + ".jpg";
                else if (Nombre.Substring(0, index).Length == 5)
                    NombreConcatenado = "0" + Nombre.Substring(0, index) + ".jpg";
                else
                    NombreConcatenado = Nombre;
                string DestinoSicea = UbicacionSicea + @"\" + NombreConcatenado;
                File.Copy(Origen, DestinoSicea, true);
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



