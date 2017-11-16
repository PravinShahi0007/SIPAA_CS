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

        //CheckBox chk = new CheckBox();
        CheckBox ckheader = new CheckBox();
        public CZKEMClass objCZKEM = new CZKEMClass();
        public List<UsuarioReloj> ltUsuario = new List<UsuarioReloj>();
        public List<string> ltRelojxUsuario = new List<string>();
        List<Reloj> ltReloj = new List<Reloj>();
        BackgroundWorker bd = new BackgroundWorker();
       // public string Mensaje;
        string Trabajador = string.Empty;
        string idReloj = string.Empty;
        string Mensaje = string.Empty; 

       
        public string sUsuuMod = LoginInfo.IdTrab;

        private List<FaceTmp> rostros;



        public AdministracionUsuariosReloj()
        {
            InitializeComponent();
        }

        public class Reloj
        {
            public int cvReloj;
            public string IpReloj;
            public bool Teclado;
            public bool Huella;
            public bool Rostro;
            public bool MultipleHuella;
            public string UltimaDescarga;
            public string Descripcion;
            public string UsuSincChecadas;
        }

        private void AdministracionUsuariosReloj_Load(object sender, EventArgs e)
        {
            
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

            llenarGrid(obj); // primer gridview 
            LlenarGrid(6, 0, "%", "%", "%", 0, "", ""); // segundo gridview
            panelAccion.Enabled = false;
            this.btnTeclado.Image = global::SIPAA_CS.Properties.Resources.Pass;
        }

        public void LlenarGrid(int p_opcion, int p_cvreloj, string p_descripcion, string p_ip, string p_cvvnc, int p_stactualiza, string p_usuumod, string p_prgumodr)
        {

            if (dgvRelojes.Columns.Count > 0)
            {
                dgvRelojes.Columns.RemoveAt(0);
            }


            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
            dgvRelojes.DataSource = dtRelojChecador;

            Utilerias.AgregarCheck(dgvRelojes, 0);

            ckheader = Utilerias.AgregarCheckboxHeader(dgvRelojes, 0);

            ckheader.CheckedChanged += Ckheader_CheckedChanged;
            dgvRelojes.Columns["Clave"].Visible = false;
            dgvRelojes.Columns["Actualiza"].Visible = false;
            dgvRelojes.Columns["ClaveVNC"].Visible = false;
            dgvRelojes.Columns["multiplehuella"].Visible = false;
            //////////////////////// 
            // estas de aqui abajo las oculte como vista previa, porque el usuario no necesita esos datos y no sirven para nada
            dgvRelojes.Columns["teclado"].Visible = false;
            dgvRelojes.Columns["huella"].Visible = false;
            dgvRelojes.Columns["IP"].Visible = false;
            dgvRelojes.Columns["Rostro"].Visible = false;
            
         
            ///////////////////////
            dgvRelojes.Columns["Usuario Sincronizó Asistencias"].Visible = false;
            dgvRelojes.Columns["Usuario Sincronizó Usuarios"].Visible = false;
            dgvRelojes.Columns[0].Width = 90;
              

            foreach (DataGridViewRow row in dgvRelojes.Rows)
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

                foreach (DataGridViewRow row in dgvRelojes.Rows)
                {

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    ltReloj.Add(objR);

                    panelAccion.Enabled = true;
                  //btnAdmin.Enabled = false;
                }

            }
            else
            {

                ltReloj.Clear();
                foreach (DataGridViewRow row in dgvRelojes.Rows)
                {
                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";
                }
                panelAccion.Enabled = false;

            }
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

            if (dgvEmpleados.Columns.Count > 0) { dgvEmpleados.Columns.RemoveAt(0); }

            DataTable dt = obj.obtrelojeschecadores(9,obj.p_cvreloj,obj.p_descripcion,obj.p_ip,obj.p_cvvnc,0,obj.p_usuumod,obj.p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
            dgvEmpleados.DataSource = dt;
            Utilerias.AgregarCheck(dgvEmpleados,0);
            dgvEmpleados.Columns["cvreloj"].Visible = false;
            dgvEmpleados.Columns["valida_Huella"].Visible = false;
            dgvEmpleados.Columns["valida_Teclado"].Visible = false;
            dgvEmpleados.Columns["multiplehuella"].Visible = false;
            dgvEmpleados.Columns["IP"].Visible = false;
            dgvEmpleados.Columns["Enviado"].Visible = false;
            dgvEmpleados.Columns["Huella Digital"].Visible = false;
            dgvEmpleados.Columns["Teclado"].Visible = false;

         
            foreach (DataGridViewRow row in dgvEmpleados.Rows)
            {
                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                row.Cells[0].Tag = "uncheck";
            }

        }

        private void AsignarReloj(string sIdtrab)
        {
            RelojChecador objReloj = new RelojChecador();
           
            DataTable dt = objReloj.RelojesxTrabajador(sIdtrab, 0, 4, "", "");

            foreach (DataRow row in dt.Rows)
            {

                if (!ltRelojxUsuario.Contains(Convert.ToInt32(row[0]).ToString()))
                {

                    ltRelojxUsuario.Add(Convert.ToInt32(row[0]).ToString());
                }
            }

            /////
            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                fila.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                fila.Cells[0].Tag = "uncheck";
            }
            /////
          


            Utilerias.ImprimirAsignacionesGrid(dgvRelojes, 0, 1, ltRelojxUsuario);
        }




        private void dgvReloj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
           

            if (dgvEmpleados.SelectedRows.Count != 0)
            {
                ltUsuario.Clear(); 
                foreach (DataGridViewRow fila in dgvEmpleados.Rows)
                {
                    fila.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    fila.Cells[0].Tag = "uncheck";
                }
                       
                DataGridViewRow row = dgvEmpleados.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                Trabajador=row.Cells[1].Value.ToString();
                AsignarReloj(Trabajador);
                UsuarioReloj objR = new UsuarioReloj();
                objR.cvreloj = Convert.ToInt32(row.Cells["cvreloj"].Value.ToString());
                objR.ipReloj = row.Cells["IP"].Value.ToString();
                objR.idtrab = row.Cells["idtrab"].Value.ToString();
                objR.Nombre = row.Cells["Nombre"].Value.ToString();
                objR.Enviado = Convert.ToBoolean(row.Cells["Enviado"].Value);
                objR.Teclado = row.Cells["valida_Teclado"].Value.ToString();
                objR.Huella = row.Cells["valida_Huella"].Value.ToString();

                ValidarExistenciaEmpleado(ltUsuario, objR);

                if (ltUsuario.Count > 0)
                {
                    panelAccion.Enabled = true;

                    UsuarioReloj obj1 = new UsuarioReloj();
                    obj1 = ltUsuario[0];
                    RelojxUsuario.idtrab = obj1.idtrab;
                    RelojxUsuario.IPReloj = obj1.ipReloj;
                    RelojxUsuario.cvreloj = obj1.cvreloj;
                    RelojxUsuario.Nombre = obj1.Nombre;
                    
                    btnPerfil.Enabled = true;
                    btnFace.Enabled = true;
                    btnHuella.Enabled = true;
                    btnDescarga.Enabled = true;
                    btnKey.Enabled = true;
                    btnBorrar.Enabled = true;
                    btnTeclado.Enabled = true;
                    btnSync.Enabled = false;
                   
                }
                else
                {
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
                ltRelojxUsuario.Clear();
            }
        }

        public bool ValidarBorrar(List<UsuarioReloj> lt) {

            bool bBandera = true;
            foreach (UsuarioReloj obj in lt) {

                if (obj.Enviado != true) { bBandera = false; }

            }

            return bBandera;
        }
        public void ValidarExistenciaEmpleado(List<UsuarioReloj> ltReloj, UsuarioReloj Obj)
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

            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(10, TrabajadorInfo.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
            
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
                
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Eliminando Registros...");
                //pnlMensaje.Enabled = false;
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
                            RelojChecador objReloj = new RelojChecador();
                            objReloj.RelojesxTrabajador(idtrab, Convert.ToInt32(cvreloj), 3, "", "");
                            objCZKEM.SSR_DeleteEnrollData(1, idtrab, 12);

                            //if (objCZKEM.SSR_DeleteEnrollData(1, idtrab, 12))
                            //{
                                
                               // objReloj.RelojesxTrabajador(idtrab, Convert.ToInt32(cvreloj), 9, "", "");
                                objCZKEM.Disconnect();
                                objReloj.p_cvreloj = TrabajadorInfo.cvReloj;
                                objReloj.p_ip = "%";
                                objReloj.p_usuumod = "%";
                                objReloj.p_prgumodr = "%";
                                objReloj.p_cvvnc = "%";
                                llenarGrid(objReloj);
                            //}
                            //else
                            //{

                                bBandera = true;

                            //}
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
                if (bBandera)
                {
                    // progressBar1.Visible = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Borrados correctamente.");
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

            /*
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
            */
            /////////////////////////////////////

            panelAccion.Enabled = false;
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
                sVerify = iModoCheck = iAnho = iDia = iMes = iHora = iMinuto = iSegundo = iWorkCode = iCont = 0;
                string sIP = String.Empty;
                bool bBandera = false;
                try
                {
                    foreach (Reloj obj in ltReloj)
                    {


                        DialogResult Resultado = MessageBox.Show("El reloj " + obj.Descripcion.ToString() + " tuvo una descarga de asistencia  \nen la fecha:   " + obj.UltimaDescarga + " por el usuario " + obj.UsuSincChecadas + " \n¿Desea Sincronizarlo de nuevo?", "SIPPA", MessageBoxButtons.YesNo);
                        if (Resultado == DialogResult.Yes)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espere por favor. Descargando Registros...");
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
                                        pnlMensaje.Enabled = false;
                                        if (progressBar1.Value + (iCont * 5) <= progressBar1.Maximum)
                                            progressBar1.Value = progressBar1.Value + (iCont * 5);
                                        pnlMensaje.Enabled = false;
                                        bBandera = IngresarRegistro(sIdTrab, iAnho, iMes, iDia, iHora, iMinuto, iSegundo, obj.cvReloj, iModoCheck);
                                    }
                                }
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

                }
                catch (Exception ex)
                {
                    pnlMensaje.Enabled = true;
                    progressBar1.Visible = false;
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
               

            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha seleccionado algún Registro.");
                panelTag.Update();
                timer1.Start();

            }




            /////////////////////////////////////////
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
          //  panelTag.Visible = false;
          // progressBar1.Value = 0;
              progressBar1.Visible = false;
              pnlMensaje.Visible = false;
              panelTag.Visible = false;
              timer1.Stop();
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
            AdministracionRelojChecador Form = new AdministracionRelojChecador();
            Form.Show(); 
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
                pnlMensaje.Enabled = true;
                progressBar1.Visible = true;
                progressBar1.Value = 20;                   
                panelAccion.Enabled = false;
                pnlMensaje.Enabled = false;
                int iCont = 0;
                RelojChecador objReloj = new RelojChecador();
                    
                //Guarda asignaciones en bd
                foreach (Reloj obj in ltReloj)
                {
                    objReloj.RelojesxTrabajador(Trabajador, obj.cvReloj, 1, sUsuuMod, Name);
                }

                //DialogResult result = MessageBox.Show("¿Esta Seguro que desea SINCRONIZAR la información del usuario?", "SIPAA", MessageBoxButtons.YesNo);
                DialogResult result = MessageBox.Show("Asignaciones guardadas correctamente, desea sincronizar?\nEste proceso puede tardar varios minutos. ", "SIPAA", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; }

                foreach (Reloj obj in ltReloj)
                {
                    iCont += 1;
                    //contadores:
                    int regHuella = 0;
                    int regGpos = 0;
                    int regFace = 0;
                    //*********
                    pnlMensaje.Enabled = true;
                    pnlMensaje.Visible = true;
                    Utilerias.ControlNotificaciones(this.panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                    pnlMensaje.Enabled = false;

                    bool bConexion = Connect_Net(obj.IpReloj, 4370);

                    if (bConexion)
                    {

                    //int iContReg = 0;
                    DataTable dt = objReloj.RelojesxTrabajador(Trabajador, obj.cvReloj, 6, "%", "%");
                    progressBar1.Value = 40;

                    #region InsertaHuellas
                    bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);
                    int counter = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        //**************************
                        string idtrab = row["idtrab"].ToString();
                        string cvreloj = row[1].ToString();
                        string Nombre = row["Nombre"].ToString();
                        int Grupo = Convert.ToInt32(row["cvgruposreloj"].ToString());
                        int Permiso = 0;
                        string pass_desc = string.Empty;

                        if (!string.IsNullOrEmpty(row["pass"].ToString()))
                            pass_desc = Utilerias.descifrar(row["pass"].ToString());
                        if (Convert.ToBoolean(row["administrador"].ToString()))
                            Permiso = 3;
                        //**************************

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
                                        //success!
                                        counter++;
                                        regHuella++;
                                        pnlMensaje.Enabled = true;
                                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando huellas (" + counter + ")");                                                
                                        pnlMensaje.Enabled = false;
                                        System.Threading.Thread.Sleep(20);
                                    }
                                }
                            }
                        }
                    }
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    bool RefreshData = objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();
                    #endregion

                    #region InsertaGrupos
                    bConexion = Connect_Net(obj.IpReloj, 4370);                        

                    if (bConexion)
                    {
                        BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);
                        counter = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            //**************************
                            string idtrab = row["idtrab"].ToString();
                            int Grupo = Convert.ToInt32(row["cvforma"].ToString());
                            //**************************

                            if (objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo))
                            {
                                //success!
                                counter++;
                                regGpos++;
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

                    #region ObtenerRostros
                    rostros = new List<FaceTmp>();
                    counter = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        string idtrab = row["idtrab"].ToString();

                        if (row["rostroTmp"].ToString() != String.Empty)
                        {
                            //**************************
                            string RostroTmp = row["rostroTmp"].ToString();
                            int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                            //**************************

                            rostros.Add(new FaceTmp(idtrab, 50, RostroTmp, rostrolong));

                            counter++;                                    
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Obteniendo rostros (" + counter + ")");                                    
                            pnlMensaje.Enabled = false;
                            System.Threading.Thread.Sleep(20);
                        }
                    }
                    #endregion

                    #region InsertaRostros
                    bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                    if (bConexion)
                    {
                        objCZKEM.RestartDevice(1);
                        System.Threading.Thread.Sleep(60000);

                        bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                        if (bConexion)
                        {
                            counter = 0;
                            foreach (FaceTmp ft in rostros)
                            {
                                if (objCZKEM.SetUserFaceStr(1, ft.idtrab, ft.index, ft.rostroTmp, ft.rostrolong))
                                {
                                    //success!
                                    counter++;
                                    regFace++;
                                    pnlMensaje.Enabled = true;
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Insertando rostros (" + counter + ")");                                            
                                    pnlMensaje.Enabled = false;
                                    System.Threading.Thread.Sleep(20);
                                }
                            }

                            objCZKEM.Disconnect();
                        }
                    }
                        #endregion

                    #region CodigoAnterior
                        /*
                        //foreach (DataRow row in dt.Rows)
                        //{
                        //    /*string idtrab = row["idtrab"].ToString();
                        //    string cvreloj = row[1].ToString();
                        //    string Nombre = row["Nombre"].ToString();
                        //   // string aux = row["administrador"].ToString();
                        //    int Grupo = Convert.ToInt32(row["cvgruposreloj"].ToString());
                        //    int Permiso = 0;
                        //    if (Convert.ToBoolean(row["administrador"].ToString()))
                        //        Permiso = 3;
                        //    string pass_desc = "";



                            string idtrab = row["idtrab"].ToString();
                            string cvreloj = row[1].ToString();
                            string Nombre = row["Nombre"].ToString();
                            int Grupo = Convert.ToInt32(row["cvgruposreloj"].ToString());
                            int Permiso = 0;
                            string pass_desc = string.Empty;

                            if (!string.IsNullOrEmpty(row["pass"].ToString()))
                                pass_desc = Utilerias.descifrar(row["pass"].ToString());
                            if (Convert.ToBoolean(row["administrador"].ToString()))
                                Permiso = 3;

                            SonaTrabajador objTrab = new SonaTrabajador();
                            iContReg += 1;
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Cargando Datos  " + iContReg + " de " + dt.Rows.Count + " Trabajadores Encontrados");
                            progressBar1.Value = progressBar1.Value + (10 / dt.Rows.Count);
                            pnlMensaje.Enabled = false;
                            if (obj.Teclado)
                            {
                                try
                                {
                                        //if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true))
                                        //    bBanderaPass = true;
                                        //objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo);
                                    if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true)) //tenia !
                                        bBanderaPass = true;

                                    objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo);
                                }
                                catch
                                {
                                    bBanderaPass = false;
                                }
                            }
                            if (obj.Huella)
                            {
                                if (obj.MultipleHuella)
                                {

                                    if (row["huellaTmp"].ToString() != String.Empty)
                                    {
                                        try
                                        {
                                            int ifinger = Convert.ToInt32(row["indicehuella"].ToString());

                                            if (ifinger != 0 && ifinger != 10)
                                            {
                                                string tmpHuella = row["huellaTmp"].ToString();
                                                if (objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella))
                                                    bBanderaHuella = true;

                                            }
                                        }
                                        catch
                                        {
                                            bBanderaHuella = false;
                                        }
                                    }
                                }
                                else
                                {
                                    //****************************** LOS DEDOS SE CUENTAN DESDE EL MEÑIQUE IZQUIERDO HASTA EL DERECHO DE 0 A 9*************
                                    int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                                    string tmpHuella = "";
                                    if (ifinger >= 0 && ifinger <= 9)
                                    {
                                        tmpHuella = row["huellaTmp"].ToString();
                                        objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);
                                        bBanderaHuella = true;

                                    }


                                }


                            }
                            if (obj.Rostro)
                            {
                                if (row["rostroTmp"].ToString() != String.Empty)
                                {
                                    try
                                    {
                                        string RostroTmp = row["rostroTmp"].ToString();
                                        int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                                        if (objCZKEM.SetUserFaceStr(1, idtrab, 50, RostroTmp, rostrolong))
                                            bBanderaRostro = true;

                                    }
                                    catch
                                    {
                                        bBanderaRostro = false;
                                    }
                                }
                            }

                            if (bBanderaHuella || bBanderaPass || bBanderaRostro)
                            {
                                objReloj.RelojesxTrabajador(idtrab, 0, 7, "", "");
                                bBandera = true;
                                progressBar1.Enabled = true;
                                progressBar1.Value = 90;
                                progressBar1.Enabled = false;
                            }
                        }*/
                        #endregion

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

                        //if (bBandera) // es true
                        //{
                        //    pnlMensaje.Enabled = true;
                        //    objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                        //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente.");                              
                        //    pnlMensaje.Enabled = false;
                        //}
                        //else
                        //{
                        //    pnlMensaje.Enabled = true;
                        //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                        //    panelTag.Update();
                        //    pnlMensaje.Enabled = false;
                        //}
                    }
                    else
                    {
                        pnlMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                        panelTag.Update();
                        pnlMensaje.Enabled = false;
                        progressBar1.Visible = false;
                        pnlMensaje.Enabled = true;
                          
                    }
                }
            }
        }

                    


                    /////////////////////////////////////////////////////////////////////////////////////////////////////

            /*
                    panelTag.Enabled = lbMensaje.Enabled = false;
               if (ltReloj.Count > 0)
                {
                panelTag.Enabled = lbMensaje.Enabled = true;
                progressBar1.Visible = panelTag.Visible = lbMensaje.Visible = true;
                progressBar1.Value = 20;
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Espera por favor. Descargando Registros...");
                //pnlMensaje.Enabled = true;
                    int iCont = 0;


                   
                    foreach (Reloj obj in ltReloj)
                    {

                        RelojChecador objReloj = new RelojChecador();
                        
                        bool bBandera = true;
                        bool bBanderaPass = false;
                        bool bBanderaHuella = false;
                        bool bBanderaRostro = false;

                        iCont += 1;
                        pnlMensaje.Enabled = true;
                        pnlMensaje.Visible = true; 

                        Utilerias.ControlNotificaciones(this.panelTag, lbMensaje, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                        pnlMensaje.Enabled = false;

                        bool bConexion = Connect_Net(obj.IpReloj, 4370);

                        if (bConexion != false)
                        {
                           
                            int iContReg = 0;
                            objReloj.RelojesxTrabajador(Trabajador, obj.cvReloj, 1, sUsuuMod, Name);
                            DataTable dt = objReloj.RelojesxTrabajador(Trabajador, obj.cvReloj, 6, "%", "%");
                            progressBar1.Value = 40;
                            
                            foreach (DataRow row in dt.Rows)
                            {



                                string idtrab = row["idtrab"].ToString();
                                string cvreloj = row[1].ToString();
                                string Nombre = row["Nombre"].ToString();
                                string aux = row["administrador"].ToString();
                                int Grupo = Convert.ToInt32(row["cvgruposreloj"].ToString()); 
                                int Permiso = 0;
                                if (Convert.ToBoolean(row["administrador"].ToString()))
                                    Permiso = 3;
                                string pass_desc = "";
                                SonaTrabajador objTrab = new SonaTrabajador();
                             

                                iContReg += 1;
                                pnlMensaje.Enabled = true;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Cargando Datos  " + iContReg + " de " + dt.Rows.Count + " Trabajadores Encontrados");
                                progressBar1.Value = progressBar1.Value + (10 / dt.Rows.Count);
                                pnlMensaje.Enabled = false;
                            

                                if (obj.Teclado)
                                {
                                    try
                                    {
                                        if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true)) 
                                            bBanderaPass = true;
                                    objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo);
                                }
                                    catch
                                    {
                                        bBanderaPass = false;
                                    }
                                }
                                if (obj.Huella) 
                                {
                                    if (obj.MultipleHuella) 
                                    {

                                        if (row["huellaTmp"].ToString() != String.Empty)
                                        {
                                            try
                                            {
                                                int ifinger = Convert.ToInt32(row["indicehuella"].ToString());

                                                if (ifinger != 0 && ifinger != 10)
                                                {
                                                    string tmpHuella = row["huellaTmp"].ToString();
                                                    if (objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella)) 
                                                        bBanderaHuella = true;

                                                }
                                            }
                                            catch
                                            {
                                                bBanderaHuella = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //****************************** LOS DEDOS SE CUENTAN DESDE EL MEÑIQUE IZQUIERDO HASTA EL DERECHO DE 0 A 9*************
                                        int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                                        string tmpHuella = "";
                                        if (ifinger >= 0 && ifinger <= 9)
                                        {
                                            tmpHuella = row["huellaTmp"].ToString();
                                            objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);
                                            bBanderaHuella = true;

                                        }


                                    }


                                }
                                if (obj.Rostro)
                                {
                                    if (row["rostroTmp"].ToString() != String.Empty)
                                    {
                                        try
                                        {
                                            string RostroTmp = row["rostroTmp"].ToString();
                                            int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                                            if (objCZKEM.SetUserFaceStr(1, idtrab, 50, RostroTmp, rostrolong))
                                                bBanderaRostro = true;

                                        }
                                        catch
                                        {
                                            bBanderaRostro = false;
                                        }
                                    }
                                }

                                if (bBanderaHuella || bBanderaPass || bBanderaRostro)
                                {
                                    objReloj.RelojesxTrabajador(idtrab, 0, 7, "", "");
                                    bBandera = true;                                  
                                    progressBar1.Enabled = true;
                                    progressBar1.Value = 90;
                                    progressBar1.Enabled = false;
                                }
                            }

                       

                            if (bBandera) // es true
                            {
                                pnlMensaje.Enabled = true;
                                objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registros Guardados correctamente.");
                               // panelTag.Update();
                                pnlMensaje.Enabled = false;
                            }
                            else
                            {

                                pnlMensaje.Enabled = true;
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Uno o más registro no se insertaron correctamente en el dispositivo. Favor de repetir el proceso.");
                                panelTag.Update();
                                pnlMensaje.Enabled = false;
                            }

                            objCZKEM.Disconnect();
                        }
                        else
                        {
                            pnlMensaje.Enabled = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                            panelTag.Update();
                            pnlMensaje.Enabled = false;
                            progressBar1.Visible = false;
                            pnlMensaje.Enabled = true;
                            //  break;
                        }
                    }


               
                 progressBar1.Enabled = true;
                 progressBar1.Value = 100;
                 progressBar1.Enabled = false;
                 this.Enabled = true;
                    //ltReloj.Clear();
                    //LlenarGrid(6, 0, "%", "%", "%", 0, "", "");
                 AsignarReloj(Trabajador);
                 timer1.Start();
                }
                else
                {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");
                    pnlMensaje.Enabled = true;

                }
       
        }*/

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
           // ProcesoReloj("Huella");
            ProcesoReloj("Huella");
            ProcesoReloj("Face");
            ProcesoReloj("Pass");
            ltReloj.Clear();
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
            //byte tmp = new byte();
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
            this.Close();
        }

        private void dgvRelojes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           

            if (dgvEmpleados.SelectedRows.Count != 0)
            {
               
               btnSync.Enabled = true; 
               DataGridViewRow row = dgvRelojes.SelectedRows[0];
               idReloj=  row.Cells[0].Tag.ToString();
               
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

                ///
                Reloj objR = new Reloj();
                objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                objR.IpReloj = row.Cells["IP"].Value.ToString();
                objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                objR.UltimaDescarga = row.Cells["Ultima Descarga Asistencias"].Value.ToString();
                objR.Descripcion = row.Cells["Descripción"].Value.ToString();
                objR.UsuSincChecadas = row.Cells["Usuario Sincronizó Asistencias"].Value.ToString();
                ValidarExistenciaReloj(ltReloj, objR);

                ///

            }
           
        }

        public void ValidarExistenciaReloj(List<Reloj> ltReloj, Reloj Obj)
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
