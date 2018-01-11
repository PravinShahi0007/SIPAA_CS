﻿using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using SIPAA_CS.RecursosHumanos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public class Reloj {
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

                foreach (DataGridViewRow row in dgvReloj.Rows) {

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
            else {

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

            if (dgvReloj.Columns.Count > 0) {
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
            dgvReloj.Columns[0].Width = 90;
            dgvReloj.Columns[2].Width = 220;
            dgvReloj.Columns[10].Width = 150;
            dgvReloj.Columns[11].Width = 150;
            dgvReloj.ClearSelection();


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
                else {

                   // panelAccion.Enabled = false;
                    Deshabilita_Botones(false); 
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

        private void Deshabilita_Botones(bool Bandera)
        {
            if (Bandera)
            
                btnHuella.Enabled = btnDescarga.Enabled = btnSync.Enabled = btnAdmin.Enabled = button1.Enabled= true; 
            
            else
            
                btnHuella.Enabled = btnDescarga.Enabled = btnSync.Enabled = btnAdmin.Enabled = button1.Enabled = false;
            
        }




        public void ValidarBotones(bool bBandera, string Opcion) {


            switch (Opcion) {

                case "Teclado": if (bBandera != true) { btnKey.Enabled = false; } else { btnKey.Enabled = true; } break;

                case "Rostro": if (bBandera != true) { btnFace.Enabled = false; } else { btnFace.Enabled = true; } break;

                case "Huella": if (bBandera != true) { btnHuella.Enabled = false; } else { btnHuella.Enabled = true; } break;
            }

        }

        public bool ValidarObjectos(List<Reloj> ltReloj, string Boton) {

            bool bBandera = true;

            foreach (Reloj obj in ltReloj) {

                switch (Boton) {

                    case "Teclado": if (obj.Teclado != true) { bBandera = false; };
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
           // panelAccion.Enabled = false;
            Deshabilita_Botones(false); 
            pnlMensaje.Visible = true;
            progressBar1.Visible = true;
            panelTag.Visible = false;

            if (ltReloj.Count > 0)
            {
                /*Panel de mensajes*/
                pnlMensaje.Enabled = true;
                progressBar1.Value = 20;
                pnlMensaje.Enabled = false;
                string sIdTrab = String.Empty;
                int sVerify, iModoCheck, iAnho, iDia, iMes, iHora, iMinuto, iSegundo, iWorkCode, iCont;
                sVerify= iModoCheck= iAnho= iDia= iMes= iHora= iMinuto=  iSegundo= iWorkCode= iCont = 0;
                string sIP = String.Empty;
                bool bBandera = false;
                try
                {
                    foreach (Reloj obj in ltReloj)
                    {
                        DialogResult Resultado = MessageBox.Show("El reloj " + obj.Descripcion.ToString() + " tuvo una descarga de asistencia  \nen la fecha:   " + obj.UltimaDescargaAsistencia +" por el usuario "+obj.UsuDescargaAsistencia+ " \n¿Desea Sincronizarlo de nuevo?", "SIPPA", MessageBoxButtons.YesNo);
                        if (Resultado == DialogResult.Yes)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espere por favor. Descargando Registros...");
                            iCont += 1;
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                            progressBar1.Value = 40;
                            pnlMensaje.Enabled = false;
                            bool bConexion = Connect_Net(obj.IpReloj, 4370);
                            objCZKEM.ReadAllUserID(1);
                            objCZKEM.ReadAllTemplate(1);
                            if (bConexion != false)
                            {
                               if (objCZKEM.ReadAllGLogData(1))
                                {
                                    int iContReg = 0;
                                    while (objCZKEM.SSR_GetGeneralLogData(1, out sIdTrab, out sVerify, out iModoCheck, out iAnho
                                                                                                         , out iMes, out iDia, out iHora, out iMinuto, out iSegundo
                                                                                                         , ref iWorkCode))
                                    {
                                        iContReg += 1;
                                        pnlMensaje.Enabled = true;
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando Registro " + iContReg);
                                        pnlMensaje.Enabled = false;
                                        if (progressBar1.Value + (iCont * 5) <= progressBar1.Maximum)
                                            progressBar1.Value = progressBar1.Value + (iCont * 5);
                                        // string sIdTrabCifrado = Utilerias.cifrarPass(sIdTrab, 1); AQUI DEBERIA DE IR EL CIFRADO DEL NUMERO DE EMPLEADO
                                        pnlMensaje.Enabled = false;
                                        bBandera = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, obj.cvReloj, iModoCheck);
                                        
                                    }
                                }
                              //ELimina automaticamente los registros de la asistencia en el reloj checador 
                                objCZKEM.ClearData(1, 1);
                                objCZKEM.Disconnect();
                                progressBar1.Value = 90;
                                if (bBandera)
                                {
                                    pnlMensaje.Enabled = true;
                                    RelojChecador objReloj = new RelojChecador();
                                    objReloj.obtrelojeschecadores(7, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente");
                                    progressBar1.Value = 100;
                                    pnlMensaje.Enabled = false;
                                                             
                                }
                                else
                                {
                                    pnlMensaje.Enabled = true;
                                    progressBar1.Visible = false;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se guardaron correctamente. Por Favor Repite el Proceso");
                                    pnlMensaje.Enabled = false;
                                   
                                 }


                            }
                            else
                            {
                                pnlMensaje.Enabled = true;
                                progressBar1.Visible = false;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                                pnlMensaje.Enabled = false;
                                progressBar1.Visible = false;
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
                    progressBar1.Visible = false;
                    MessageBox.Show(ex.ToString());
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, ex.Message);
                    pnlMensaje.Enabled = false;
                }
                finally
                {
                    objCZKEM.Disconnect();
                    timer1.Start();
                }
                ltReloj.Clear();
                LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
          /*

                foreach (Reloj obj in ltReloj)
                {
                    DialogResult Resultado = MessageBox.Show("El reloj " + obj.Descripcion.ToString() + " tuvo una descarga de asistencia  \nen la fecha:   " + obj.UltimaDescarga + " \n¿Desea Sincronizarlo de nuevo?", "SIPPA", MessageBoxButtons.YesNo);

                    if (Resultado == DialogResult.Yes)
                    {
                        panelTag.Update();
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espere por favor. Descargando Registros...");
                        panelTag.Update();
                        progressBar1.Visible = true;
                        progressBar1.Value = 20;
                        panelAccion.Enabled = false;
                        pnlMensaje.Enabled = false;
                        int sVerify, iModoCheck, iAnho, iDia, iMes, iHora, iMinuto, iSegundo, iWorkCode, iCont;
                        sVerify = iModoCheck = iAnho = iDia = iMes = iHora = iMinuto = iSegundo = iWorkCode = iCont = 0;
                        string sIP, sIdTrab;
                        sIP=sIdTrab = String.Empty;
                        bool bBandera = false;
                        try
                        {
                            iCont += 1;
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                            panelTag.Update();
                            progressBar1.Value = 40;
                            pnlMensaje.Enabled = false;
                            bool bConexion = Connect_Net(obj.IpReloj, 4370);
                            objCZKEM.ReadAllUserID(1);
                            objCZKEM.ReadAllTemplate(1);


                            if (bConexion != false)
                            {

                                if (objCZKEM.ReadAllGLogData(1))
                                {
                                    int iContReg = 0;
                                    while (objCZKEM.SSR_GetGeneralLogData(1, out sIdTrab, out sVerify, out iModoCheck, out iAnho
                                                                                                         , out iMes, out iDia, out iHora, out iMinuto, out iSegundo
                                                                                                         , ref iWorkCode))
                                    {
                                        iContReg += 1;
                                        pnlMensaje.Enabled = true;
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descargando Registro " + iContReg);
                                        panelTag.Update();
                                        pnlMensaje.Enabled = false;
                                        if (progressBar1.Value + (iCont * 5) <= progressBar1.Maximum)
                                            progressBar1.Value = progressBar1.Value + (iCont * 5);
                                        // string sIdTrabCifrado = Utilerias.cifrarPass(sIdTrab, 1);
                                        bBandera = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, obj.cvReloj, iModoCheck);
                                    }
                                }
                                objCZKEM.Disconnect();
                                progressBar1.Value = 90;
                                if (bBandera)
                                {
                                    pnlMensaje.Enabled = true;
                                    RelojChecador objReloj = new RelojChecador();
                                    objReloj.obtrelojeschecadores(7, obj.cvReloj, "", "", "", 0, "", "");
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente");
                                    panelTag.Update();
                                    pnlMensaje.Enabled = false;
                                    timer1.Start();
                                    objCZKEM.Disconnect();
                                }
                                else
                                {
                                    pnlMensaje.Enabled = true;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no se guardaron correctamente. Por Favor Repite el Proceso");
                                    panelTag.Update();
                                    pnlMensaje.Enabled = false;
                                    timer1.Start();
                                    objCZKEM.Disconnect();
                                }


                            }
                            else
                            {
                                pnlMensaje.Enabled = true;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                                panelTag.Update();
                                progressBar1.Visible = false;
                                

                            }
                        }
                        catch (Exception ex)
                        { 
                            //  Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, ex.Message);
                        }

                    }
                   else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Descarga de asistencia cancelada para el reloj "+obj.Descripcion);
                      
                    }
                }

                ltReloj.Clear();
                panelAccion.Enabled = false;
                progressBar1.Value = 100;
                progressBar1.Visible = false;
                LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
                timer1.Start();
                timer2.Start();
                panelTag.Update();  */

            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha seleccionado algún Registro.");
                panelTag.Update();
                timer1.Start();
                
            }
           
        }


        public bool IngresarRegistro(string sIdTrab, int Year, int Month, int Day, int Hour, int Minute, int Second, int cvReloj, int iTipoCheck) {

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
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Correcto");
                panelTag.Update();
                return true;
            }
            else {
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
                    #region InsertaBiometricos
                    DialogResult result = MessageBox.Show("Reloj " + obj.Descripcion + "\nÚltima sincronización: " + obj.UltimaSincronizacion + "\nUsuario sincronizó: " + obj.UltimoUsuarioSinc + " \n\n¿Esta Seguro que desea SINCRONIZAR la información del sistema?\nEsto eliminará la información del dispositivo y será sustituida con la información del sistema", "SIPPA", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        System.Threading.Thread.Sleep(500);
                        continue;
                    }                        
                    
                    Cursor = Cursors.WaitCursor;
                   // panelAccion.Enabled = false;
                    Deshabilita_Botones(false);
                    pnlMensaje.Visible = true;
                    pnlMensaje.Enabled = true;
                    progressBar1.Visible = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espere por favor. Descargando Registros...");
                    pnlMensaje.Enabled = false;
                    int iCont = 0;//Cuenta los relojes
                    RelojChecador objReloj = new RelojChecador();
                    DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 6, "%", "%");
                    iCont += 1;
                    //contadores:
                    int regHuella = 0;
                    int regGpos = 0;
                    int regFace = 0;
                    //*********
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);                        
                    pnlMensaje.Enabled = false;
                    System.Threading.Thread.Sleep(20);

                    bool bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);                        

                    if (bConexion)
                    {
                        bool ClearData = objCZKEM.ClearData(1, 5);
                        bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);

                        #region InsertaHuellas
                        int counter = 0;
                        progressBar1.Maximum = dt.Rows.Count;
                        foreach (DataRow row in dt.Rows)
                        {
                            string idtrab = row["idtrab"].ToString();
                            string cvreloj = row[1].ToString();
                            string Nombre = row["Nombre"].ToString();
                            int Grupo = Convert.ToInt32(row["cvforma"].ToString());
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
                                            progressBar1.Value = counter;
                                            pnlMensaje.Enabled = true;
                                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando huellas (" + counter + ")");                                                
                                            pnlMensaje.Enabled = false;
                                            System.Threading.Thread.Sleep(20);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        bool BatchUpdate = objCZKEM.BatchUpdate(1);
                        bool RefreshData = objCZKEM.RefreshData(1);
                        objCZKEM.Disconnect();

                        #region InsertaGrupos
                        bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                        if (bConexion)
                        {
                            BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);
                            counter = 0;

                            foreach (DataRow row in dt.Rows)
                            {
                                string idtrab = row["idtrab"].ToString();
                                int Grupo = Convert.ToInt32(row["cvforma"].ToString());

                                if (objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo))
                                {
                                    regGpos += 1;
                                    counter += 1;
                                    progressBar1.Value = counter;
                                    pnlMensaje.Enabled = true;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando grupos (" + counter + ")");
                                    pnlMensaje.Enabled = false;
                                    System.Threading.Thread.Sleep(20);
                                }
                            }
                                
                            BatchUpdate = objCZKEM.BatchUpdate(1);
                            RefreshData = objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();
                        }
                        #endregion

                        //Obteniendo rostros...
                        counter = 0;
                        faces = new LinkedList<FaceTmp>();
                        foreach (DataRow row in dt.Rows)
                        {
                            string idtrab = row["idtrab"].ToString();

                            if (row["rostroTmp"].ToString() != String.Empty)
                            {
                                string RostroTmp = row["rostroTmp"].ToString();
                                int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());

                                faces.AddLast(new FaceTmp(idtrab, 50, RostroTmp, rostrolong));

                                counter += 1;
                                progressBar1.Value = counter;
                                pnlMensaje.Enabled = true;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo rostros (" + counter + ")");                                    
                                pnlMensaje.Enabled = false;
                                System.Threading.Thread.Sleep(20);
                            }
                        }

                        //Insertando rostros...
                        bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                        if (bConexion)
                        {
                            //Reiniciando dispositivo...
                            objCZKEM.RestartDevice(1);
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando rostros...");
                            pnlMensaje.Enabled = false;
                            System.Threading.Thread.Sleep(60000);
                            //**************************

                            bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                            if (bConexion)
                            {
                                counter = 0;
                                foreach (FaceTmp ft in faces)
                                {
                                    if (objCZKEM.SetUserFaceStr(1, ft.idtrab, ft.index, ft.rostroTmp, ft.rostrolong))
                                    {
                                        regFace += 1;
                                        counter += 1;
                                        progressBar1.Value = counter;
                                        pnlMensaje.Enabled = true;
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando rostros (" + counter + ")");                                            
                                        pnlMensaje.Enabled = false;
                                        System.Threading.Thread.Sleep(20);
                                    }
                                }
                                    
                                objCZKEM.Disconnect();                                  
                            }
                        }

                        if (regHuella > 0 && regGpos > 0 && regFace > 0)
                        {
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Proceso finalizado para el reloj: " + obj.Descripcion);
                            pnlMensaje.Enabled = false;
                            objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);//guarda usuario última sincronización
                            System.Threading.Thread.Sleep(1000);
                            MessageBox.Show("Reloj: " + obj.Descripcion + "\nProceso terminado\nHuellas: " + (regHuella) + "\nGrupos: " + (regGpos) + "\nRostros: " + (regFace), "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        } else
                        {
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error en la inserción de datos");
                            pnlMensaje.Enabled = false;
                            System.Threading.Thread.Sleep(1000);
                            MessageBox.Show("Reloj: " + obj.Descripcion + "\nError en la inserción de datos\nHuellas: " + (regHuella > 0 ? "OK" : "Falló") + "\nGrupos: " + (regGpos > 0 ? "OK" : "Falló") + "\nRostros: " + (regFace > 0 ? "OK" : "Falló"), "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);                            
                        pnlMensaje.Enabled = false;
                        progressBar1.Visible = false;
                        pnlMensaje.Enabled = true;
                    }
                    #endregion
                }

                progressBar1.Enabled = true;
                progressBar1.Maximum = progressBar1.Maximum;
                progressBar1.Enabled = false;
                this.Enabled = true;
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


        public void limpiarReloj(string sOpcion) {

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

                                if (!objCZKEM.ClearData(1, 5)) { bBandera = true; } else {

                                    foreach (Reloj objR in ltReloj) {
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
            //ProcesoReloj("Face");
            //ltReloj.Clear();        
        }

        private void btnHuella_Click(object sender, EventArgs e)
        {
          
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


        public void ProcesoReloj() {

            if (ltReloj.Count > 0)
            {
                progressBar1.Visible = true;
                progressBar1.Value = 20;
                objCZKEM = new CZKEMClass();
                int iCont = 0;
                pnlMensaje.Visible  = progressBar1.Visible = panelTag.Visible = true;
                pnlMensaje.Enabled  = progressBar1.Enabled = panelTag.Enabled = true; 

                foreach (Reloj obj in ltReloj)
                {
                    
                    RelojChecador objReloj = new RelojChecador();
                    DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 6, "%", "%");
                    if (dt.Rows.Count < 1)
                    {
                      
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Este Reloj no tiene Personal Asignado.");
                       
                        break;
                    }
                    iCont += 1;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                    progressBar1.Value = 40;
                    bool bConexion = Connect_Net(obj.IpReloj, 4370);
                    if (bConexion != false)
                    {
                        objCZKEM.ReadAllUserID(1);
                        objCZKEM.ReadAllTemplate(1);
                        bool bBanderaPass, bBanderaHuella, bBanderaFace; 
                        bBanderaPass= bBanderaHuella= bBanderaFace= false;
                        int iContTrab  = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                            string cvreloj = row[1].ToString();
                            bBanderaPass= ConsultaReloj("Pass", idTrab, iContTrab, dt.Rows.Count);
                            progressBar1.Value = progressBar1.Value + (10 / dt.Rows.Count);
                        }
                        iContTrab = 0; 
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                            string cvreloj = row[1].ToString();
                            bBanderaHuella= ConsultaReloj("Huella", idTrab, iContTrab, dt.Rows.Count);
                            progressBar1.Value = progressBar1.Value + (10 / dt.Rows.Count);
                        }
                        iContTrab = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                            string cvreloj = row[1].ToString();
                            bBanderaFace= ConsultaReloj("Face", idTrab, iContTrab, dt.Rows.Count);
                            progressBar1.Value = progressBar1.Value + (10 / dt.Rows.Count);
                        }
                        objCZKEM.Disconnect();
                        progressBar1.Value = 90;
                        if (bBanderaHuella)
                        {
                            
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Huellas guardadas correctamente");
                            System.Threading.Thread.Sleep(500);
                            objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                            timer1.Start();
                        }
                        if (bBanderaFace)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Rostros guardados correctamente");
                            System.Threading.Thread.Sleep(500);
                            objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                            timer1.Start();
                        }
                        if (bBanderaPass)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Contraseñas guardadas correctamente");
                            System.Threading.Thread.Sleep(500);
                            objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                        }
                        else
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                            timer1.Start();
                        }
                    
                    }
                    else
                    {
                     Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse al reloj: " + obj.Descripcion);
                     timer1.Start();
                    }
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Termino el proceso para el reloj: " + obj.Descripcion);
                    System.Threading.Thread.Sleep(500);//1000
                }
              progressBar1.Value = 100;
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

            bool bBandera= false;
         
            switch (Opcion)
            {

                case "Face":
                   Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Obteniendo Rostro de Trabajador " + iContTrab + " de " + iTotal);
                    if (objCZKEM.GetUserFaceStr(1, idtrab, 50, ref sFaceTmp, ref iFaceLong))
                    {
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, "", sFaceTmp, iFaceLong.ToString(), LoginInfo.IdTrab, Name, 7);
                            bBandera = true;
                        }
                        catch{}

                       
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
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Obteniendo Huella de Trabajador " + iContTrab + " de " + iTotal);
                    
                    if (objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
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
                                catch{}
                            }
                        }
                    }
                    break;


                case "Pass":

                    string Nombre = "";
                    string Pass = "";
                    iPrivilegio = 0;
                    bActivo = false;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo Contraseña de Trabajador " + iContTrab + " de " + iTotal);
                  
                    string Cifrado = "";
                    if(objCZKEM.SSR_GetUserInfo(1,idtrab, out Nombre, out Pass, out iPrivilegio, out bActivo))
                    {
                        if (Pass != String.Empty)
                            Cifrado = Utilerias.cifrarPass(Pass, 1);
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, Cifrado, "", "0", LoginInfo.IdTrab, this.Name, 6);
                            bBandera = true;
                        }
                        catch {}
                        
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
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo  "+row[1].ToString());
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

                        progressBar1.Value = progressBar1.Value + (10 / dt.Columns.Count );
                        int Año, Mes, Dia, Hora, Minuto, Segundo;
                        Año = Mes = Dia = Hora = Minuto = Segundo = 0;
                        Año = Convert.ToInt32( FechaServidor.Substring(6, 2));
                        Mes = Convert.ToInt32(FechaServidor.Substring(3, 2));
                        Dia = Convert.ToInt32(FechaServidor.Substring(0,2));
                        Hora = Convert.ToInt32(HoraServidor.Substring(0,2));
                        Minuto = Convert.ToInt32(HoraServidor.Substring(3, 2));
                        Segundo = Convert.ToInt32(HoraServidor.Substring(6,2)); 
                        if (!objCZKEM.SetDeviceTime2(1, Año, Mes, Dia, Hora, Minuto, Segundo))
                            bBandera = true; 
                        if(bBandera)
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error al sincronizar la hora con el dispositivo: " + row[1].ToString());
                        else
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Hora sincronizada correctamente en el dispositivo: " + row[1].ToString());
                        objCZKEM.Disconnect(); 
                    }
                }
                   
                    

            }
            else
            {
                
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No existen relojes para sincronizar.");
                
            }


            //foreach (Reloj obj in ltReloj)
            //{
            //    iCont += 1;
            //    panelMensaje.Enabled = true;
            //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
            //    prgb1.Value = 40;
            //    panelMensaje.Enabled = false;
            //    bool bConect = objCZKEM.Connect_Net(obj.IpReloj, 4370);

            //    if (bConect != false)
            //    {
            //        prgb1.Value = prgb1.Value + (10 / ltReloj.Count);
            //        switch (iOpcionAdmin)
            //        {

            //            case 0:
            //                int iAnio = Convert.ToInt32(cbAnio.SelectedItem.ToString());
            //                int iMes = cbMes.SelectedIndex + 1;
            //                int iDia = cbdia.SelectedIndex + 1;
            //                int iHora = cbHora.SelectedIndex;
            //                int iMinuto = cbMinutos.SelectedIndex;
            //                if (!objCZKEM.SetDeviceTime2(1, iAnio, iMes, iDia, iHora, iMinuto, 0)) { bBandera = true; }
            //                break;

            //            case 1:
            //                if (!objCZKEM.SetDeviceTime(1)) { bBandera = true; }
            //                break;
            //        }

            //        prgb1.Value = 90;
            //    }
            //}
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
        


    