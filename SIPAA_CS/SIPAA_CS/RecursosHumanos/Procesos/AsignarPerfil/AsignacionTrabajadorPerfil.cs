
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Properties;
using SIPAA_CS.RelojChecadorTrabajador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;
using static SIPAA_CS.App_Code.Utilerias;
using zkemkeeper;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class AsignacionTrabajadorPerfil : Form, DPFP.Capture.EventHandler
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public int iOpcionAdmin;
        public int Grupo = 0;
        public List<int> ltFormasReg = new List<int>();
        public List<string> ltFormasxUsuario = new List<string>();
        public List<int> ltReloj = new List<int>();
        List<Reloj> ltReloj2 = new List<Reloj>(); // esta es para hacer la prueba  
        public List<string> ltRelojxUsuario = new List<string>();
        public string sUsuuMod = LoginInfo.IdTrab;
        
        //Huella Digital
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        private DPFP.Capture.Capture Capturer;
        public Bitmap bitmap;
        public DPFP.Processing.Enrollment Enroller;
        public event OnTemplateEventHandler OnTemplate;
        
       // private DPFP.Gui.Enrollment.EnrollmentControl EnrollmentControl;
        AdministracionRelojChecador AdminRelojChecador = new AdministracionRelojChecador();


        public AsignacionTrabajadorPerfil()
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

        }
        public bool Connect_Net(string IPAdd, int Port)
        {
            if (objCZKEM.Connect_Net(IPAdd, Port))
            {
                if (objCZKEM.RegEvent(1, 65535))
                {

                    /*comente las lineas de codigo de abajo porque no se para que las pusieron, son eventos del reloj 
                     * pero no tienen nada asignado mas que unos eventos que si se invocan pero no se para que los puso
                     * ya despues vere para que sirven
                     * solo comente  las lineas 72,73,74,75,77*/
                   // objCZKEM.OnConnected += ObjCZKEM_OnConnected;
                    //objCZKEM.OnDisConnected += objCZKEM_OnDisConnected;
                    //objCZKEM.OnEnrollFinger += ObjCZKEM_OnEnrollFinger;
                    //objCZKEM.OnFinger += ObjCZKEM_OnFinger;
                    //-----objCZKEM.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                    //objCZKEM.RegEvent(1, 32767);
                }
                return true;
            }
            return false;
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

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:07-04-2017     Última Modificacion: 17-04-2017
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        private void cbPlantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            int iOut = 0;
            if (Int32.TryParse(cbPlantilla.SelectedValue.ToString(), out iOut))
            {
                LlenarGridPlantilla(4, Int32.Parse(Convert.ToString(cbPlantilla.SelectedValue)));
            }
        }

        private void cbDiaEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            //validaHorarioJornada();
            cbDiaSalida.SelectedIndex = cbDiaEntrada.SelectedIndex;
        }

        private void cbDiaSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            //validaHorarioJornada();

            if (cbDiaEntrada.SelectedIndex > cbDiaSalida.SelectedIndex && cbDiaSalida.SelectedIndex > 0)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "El dia de Entrada no puede ser un día Superior al Del Salida");
                timer1.Start();

                cbDiaSalida.SelectedIndex = cbDiaEntrada.SelectedIndex;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvHorario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvHorario.Rows.Count; iContador++)
            {
                dgvHorario.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvHorario.SelectedRows.Count != 0)
            {
                cbDiaEntrada.Enabled = false;
                panelEditar.Visible = true;
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;

                if (Permisos.dcPermisos["Actualizar"] == 1) {
                    Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Editar");
                    btnGuardar.Visible = true;
                    iOpcionAdmin = 2;
                }
                else
                    btnGuardar.Visible = false;
                

              
                DataGridViewRow row = this.dgvHorario.SelectedRows[0];
                string sDiaEntrada = row.Cells[2].Value.ToString();
                TrabajadorHorario objHorario = RecuperarGrid(row);
                mtxtEntradaTurno.Text = objHorario.sHoraEntrada;
                mtxtSalida.Text = objHorario.sHoraSalidaTurno;
                mtxtComidaInicio.Text = objHorario.sHoraComidaInicio;
                mtxtComidaFin.Text = objHorario.sHoraComidaFin;
                mtxtTiempoComida.Text = objHorario.iTiempoComida.ToString();
                mtxtTiempoTrabajo.Text = objHorario.iHorasTotalTrabajo.ToString();
                cbDiaEntrada.SelectedValue = objHorario.iCvDia;
                cbDiaSalida.SelectedValue = objHorario.iCvdiaSalidaTurno;

                //cbDiaEntrada.SelectedValue = objHorario.iCvdiaComidaInicio;
                //cbDiaEntrada.SelectedValue = objHorario.iCvdiaComidaFin;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
               
               
            }
        }


        private void dgvForReg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ltFormasReg.Clear();
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {
                panelPermisos.Enabled = true;
                foreach (DataGridViewRow fila in dgvForReg.Rows)
                {
                    fila.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    fila.Cells[0].Tag = "uncheck";
                }
                if (dgvForReg.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = dgvForReg.SelectedRows[0];
                    Grupo = Convert.ToInt32(row.Cells[1].Value);
                    ltFormasReg.Add(Grupo);
                    try
                    {
                        switch (row.Cells[0].Tag.ToString())
                        {
                            case "check":
                                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                                row.Cells[0].Tag = "uncheck";

                                break;
                            case "uncheck":
                                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                                row.Cells[0].Tag = "check";

                                break;
                        }

                    }
                    catch
                    {
                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        row.Cells[0].Tag = "check";
                    }
                }
            }

          /*  if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {

                if (dgvForReg.SelectedRows.Count != 0)
                   Utilerias.MultiSeleccionGridView(dgvForReg, 1, ltFormasReg, panelPermisos);
            }*/
        }


        private void dgvReloj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {
               

                if (dgvReloj.SelectedRows.Count != 0)
                {                
                    iOpcionAdmin = 1;
                    Utilerias.MultiSeleccionGridView(dgvReloj, 1, ltReloj, PanelReloj);
                    DataGridViewRow row = dgvReloj.SelectedRows[0];
                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                    objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                    objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                    objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                   
                }
            }
        }
        private void relojseleccionados()
        {
            
            ltReloj2.Clear();
            for (int i = 0; i < dgvReloj.RowCount; i++)
            {
                if (dgvReloj.Rows[i].Cells[0].Tag.ToString().Equals("check"))
                {
                    DataGridViewRow row = dgvReloj.Rows[i];
                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                    objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                    objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                    objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                    ltReloj2.Add(objR);
                }
            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DatosTrabajadorPerfil dattrabperf = new DatosTrabajadorPerfil();
            dattrabperf.Show();
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

        private void btnGuardarPlantilla_Click(object sender, EventArgs e)
        {
            if (cbPlantilla.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione una Plantilla", "SIPAA");
                return;
            }

            ckbEliminar.Visible = false;
            TrabajadorHorario objHorario = AsignarObjeto();
            iOpcionAdmin = 1;
           
            panelEditar.Visible = false;
            try
            {
                DataTable dtResponse = objHorario.GestionHorario(iOpcionAdmin);
                llenarGridHorario(objHorario);
                switch (dtResponse.Columns[0].ToString())
                {

                    case "INSERTAR_PLANTILLA":
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Plantilla Asignada con exito.");
                    timer1.Start();
                    break;
                }
            }
            catch (Exception ex)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentarlo más tarde.");
                timer1.Start();
            }
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            iOpcionAdmin = 1;
            panelEditar.Visible = true;
            ckbEliminar.Visible = false;
            ckbEliminar.Checked = false;
            cbDiaEntrada.Enabled = true;
            Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Guardar");
            LimpiarFormulario();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //bool bBandera = CamposVacios(panelEditar);

            if (validaHorarioJornada())
            {
                cbDiaEntrada.Enabled = true;

                if (validarHorarioComida() && cbDiaEntrada.SelectedIndex == cbDiaSalida.SelectedIndex)
                {
                    TimeSpan inicio, fin, comidaIni, comidaFin;
                    horaCampo(mtxtEntradaTurno, out inicio);
                    horaCampo(mtxtSalida, out fin);
                    horaCampo(mtxtComidaInicio, out comidaIni);
                    horaCampo(mtxtComidaFin, out comidaFin);

                    if (inicio < comidaIni && fin > comidaFin)
                    {
                        double mins = fin.TotalMinutes - comidaFin.TotalMinutes;

                        if (mins < 60)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La hora de regreso de comida debe ser una hora antes de la salida");
                            timer1.Start();
                            return;
                        }
                        
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El horario de comida debe ser entre la hora de Entrada y Salida.");
                        timer1.Start();
                        return;
                    }

                } else
                {
                    mtxtComidaInicio.Text = "";
                    mtxtComidaFin.Text = "";
                }

                    TrabajadorHorario objHorario = new TrabajadorHorario();
                    objHorario.sIdTrab = TrabajadorInfo.IdTrab;
                    objHorario.sHoraEntrada = mtxtEntradaTurno.Text;
                    objHorario.sHoraSalidaTurno = mtxtSalida.Text;
                    objHorario.sHoraComidaInicio = mtxtComidaInicio.Text;
                    objHorario.sHoraComidaFin = mtxtComidaFin.Text;
                    objHorario.iHorasTotalTrabajo = Convert.ToInt32(mtxtTiempoTrabajo.Text);
                    objHorario.iTiempoComida = Convert.ToInt32(mtxtTiempoComida.Text);
                    objHorario.iCvDia = Convert.ToInt32(cbDiaEntrada.SelectedValue);
                    objHorario.iCvdiaSalidaTurno = Convert.ToInt32(cbDiaSalida.SelectedValue);
                    objHorario.iCvdiaComidaInicio = Convert.ToInt32(cbDiaEntrada.SelectedValue);
                    objHorario.iCvdiaComidaFin = Convert.ToInt32(cbDiaEntrada.SelectedValue);
                    objHorario.sUsuumod = LoginInfo.IdTrab; //LoginInfo.IdTrab;
                    objHorario.sPrgumod = this.Name;

                    try
                    {

                        DataTable dtresponse = objHorario.GestionHorario(iOpcionAdmin);

                        switch (dtresponse.Columns[0].ToString())
                        {
                            case "EXISTE":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ese día ya se encuentra Asignado al Trabajador.");
                                timer1.Start();                                
                                break;
                            case "INSERTAR_NUEVO":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Guardado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
                                panelEditar.Visible = false;
                                break;
                            case "ACTUALIZAR":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Cambio Guardado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
                                panelEditar.Visible = false;
                                break;
                            case "ACTUALIZA_NUEVO":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Guardado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
                                panelEditar.Visible = false;
                                break;
                            case "ELIMINAR":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Eliminado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
                                panelEditar.Visible = false;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentelo más tarde.");
                        timer1.Start();
                    }
                    llenarGridHorario(objHorario);
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Horario de entrada y salida incorrecto");
                timer1.Start();
            }

        }





        private bool CamposVacios(Panel pnl)
        {
            /*bool bBandera = false;
            foreach (Control ctrl in pnl.Controls)
            {
                string sTipoCtrl = ctrl.AccessibilityObject.ToString();
                if (sTipoCtrl.Contains("TextBox"))
                {
                    MaskedTextBox txt = (MaskedTextBox)ctrl;
                    if (txt.Text.Trim() == String.Empty || txt.Text.Trim() == ":")
                       bBandera = true;
                }
                else if (sTipoCtrl.Contains("ComboBox"))
                {
                    ComboBox cb = (ComboBox)ctrl;
                    MessageBox.Show(cb.Name);
                    if (cb.SelectedIndex == 0)
                     bBandera = true;
                }
                
            }

            return bBandera;*/
            return cbDiaEntrada.SelectedIndex > 0 && (mtxtEntradaTurno.Text.Trim().Equals(string.Empty) || mtxtEntradaTurno.Text.Trim().Equals(":"));

        }


        private void btnBuscarForReg_Click(object sender, EventArgs e)
        {
            string sBusqueda = "";
            if (txtBuscarForReg.Text != String.Empty)
                sBusqueda = txtBuscarForReg.Text;
            else
                sBusqueda = "%";
            LlenarGridFormasRegistro(sBusqueda);
            AsignarFormas(TrabajadorInfo.IdTrab);
            panelPermisos.Enabled = false;
            ltFormasReg.Clear();
        }

        private void btnGuardarForReg_Click(object sender, EventArgs e)
        {

            try
            {
                string UsuuMod = LoginInfo.IdTrab;
                string PrguMod = this.Name;
                FormaReg objFr = new FormaReg();
                LlenarGridFormasRegistro("%");
                AsignarFormas(TrabajadorInfo.IdTrab);
                if (Utilerias.SinAsignaciones(dgvForReg, 0, 1, ltFormasReg) == true)
                {
                    CrearAsignaciones_FormasRegistro(UsuuMod, PrguMod);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Seguro que desea quitar todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                      CrearAsignaciones_FormasRegistro(UsuuMod, PrguMod);
                    else
                    {

                        AsignarFormas(TrabajadorInfo.IdTrab);
                        panelPermisos.Enabled = false;
                        ltFormasReg.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Utilerias.ControlNotificaciones(panelTagForReg, lbMensajeForReg, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                AsignarFormas(TrabajadorInfo.IdTrab);
                panelPermisos.Enabled = false;
                ltFormasReg.Clear();
            }
        }

        private void btnBuscarReloj_Click(object sender, EventArgs e)
        {

          /*  bool bBandera = false;
            bBandera = ConsultaReloj("Foto", "170012", 2, 4);
            */


            string sBusqueda = "";
            if (txtBuscarReloj.Text != String.Empty)
              sBusqueda = txtBuscarReloj.Text;
            else
             sBusqueda = "%";
            
            llenarGridReloj(sBusqueda);
            AsignarReloj(TrabajadorInfo.IdTrab);
            PanelReloj.Enabled = false;
            ltReloj.Clear();

        }

       
        private void btnGuardarReloj_Click(object sender, EventArgs e)
        {
          
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Comienza proceso");
            System.Threading.Thread.Sleep(1000);
            try
            {
               
                relojseleccionados();
                if (ltReloj2.Count > 0)
                {
                   bool auxiliar = tieneAsignaciones(); 
                   if (auxiliar)
                    {
                        
                        RelojChecador objReloj = new RelojChecador();
                        objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, 25, 3, sUsuuMod, Name);
                        if (this.chkAdmin.Checked==true)
                        objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, 25, 13, sUsuuMod, Name);
                        int iCont = 0;
                                  
                    
                        foreach (Reloj obj in ltReloj2)
                        {
                            iCont += 1;
                            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Guardando en registro de reloj " + iCont + " de " + ltReloj2.Count);
                            objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 1, sUsuuMod, Name);
                        }
                       // SincronizaBiometricos(ltReloj2, objReloj);
                    }
                    else
                    {
                        
                     CrearAsignaciones_Reloj(LoginInfo.IdTrab, Name, iOpcionAdmin);
                    }
                   
                }
                else
                {
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No se ha Seleccionado algún Registro.");
                    panelTagRelojCheck.Update();
                }
             }
            catch 
            {
             
                ControlNotificaciones(panelTagForReg, lbMensajeForReg, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                AsignarReloj(TrabajadorInfo.IdTrab);
                PanelReloj.Enabled = false;
                ltReloj.Clear();
            }
        }

        private bool tieneAsignaciones()
        {
            return new RelojChecador().RelojesxTrabajador(TrabajadorInfo.IdTrab, 0, 4, "", "").Rows.Count > 0;
        }


        private void btnAgregarHuella_Click(object sender, EventArgs e)
        {
            PanelGuardarHuella.Visible = true; // estaba false

            prgbar.Visible = true;
            prgbar.Style = ProgressBarStyle.Marquee;
           
          

            Utilerias.AsignarBotonResize(btnGuardarHuella, PantallaSistema(), "Guardar");
                   
                    Enroller = new DPFP.Processing.Enrollment();            // Create an enrollment.
           
                    lbHuella.Text =  String.Format("Ingresos Necesarios: {0}", Enroller.FeaturesNeeded);
                    FormInit();
                    Start();
            
           

        }

        public CZKEMClass objCZKEM = new CZKEMClass();
        private void btnGuardarHuella_Click(object sender, EventArgs e)
        {
            try
            {
              

                switch (iOpcionAdmin) {

                   // 1
                    case 4:
                        int flag = 0;
                        string huellatmp = "";
                        int tpmlong = 0;
                        string sIdTrab = "";
                        
                        SonaTrabajador objTrab = new SonaTrabajador();
                          
                            try
                            {
                            byte[] arr = Enroller.Template.Bytes;
                            

                            //DataTable dtTrab = objTrab.GestionHuella(huellaTmp: Convert.ToBase64String(arr, 0, arr.Length), iHuella: 4, sIdtrab: TrabajadorInfo.IdTrab, sUsuumod: sUsuuMod, sPrgmod: this.Name, iOpcion: 5);
                            objTrab.GestionHuella(sIdTrab, Convert.ToBase64String(arr, 0, arr.Length), 4, LoginInfo.IdTrab, this.Name, 5);

                        }
                            catch
                            {

                            }

                        //Image img = bitmap;
                        //ImageConverter converter = new ImageConverter();
                        //byte[] arrImagen = (byte[])converter.ConvertTo(img, typeof(byte[]));

                        //SonaTrabajador objTrab = new SonaTrabajador();

                        
                        //dtTrab = objTrab.GestionHuella(System.Text.Encoding.UTF8.GetString(Enroller.Template.Bytes), System.Text.Encoding.UTF8.GetString(arrImagen), Convert.ToInt32(TrabajadorInfo.IdTrab), sUsuuMod, this.Name, 1);
                        //**********************************************//
                       // if (dtTrab.Columns.Contains("INSERT"))
                        //{
                            Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 1, "Huella Asignada Correctamente");
                            Enroller.Clear();
                        //}

                        break;

                    case 3:
                        //sonatrabajador obj = new sonatrabajador();
                        //dttrab = obj.gestionhuella(enroller.template.bytes, new byte[] { }, trabajadorinfo.idtrab, susuumod, this.name, 3);
                        //if (dttrab != null)
                        //{
                        //    utilerias.controlnotificaciones(paneltaghuella, lbmensajehuella, 1, "huella eliminada correctamente");
                        //    enroller.clear();
                        //}
                        break;

                }

              

            }
            catch (Exception EX) {

                Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 3, "Error de Comunicación con el Servidor. Favor de Intentarlo más tarde.");
            }

        }


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        private bool horaCampo(MaskedTextBox mtxtb, out TimeSpan ts)
        {
            return TimeSpan.TryParse(mtxtb.Text, out ts);
        }

        private bool validarHorarioComida()
        {
            TimeSpan comidaIni, comidaFin;

            if (horaCampo(mtxtComidaInicio, out comidaIni) && horaCampo(mtxtComidaFin, out comidaFin) && comidaFin > comidaIni)
            {
                mtxtTiempoComida.Text = Convert.ToString(comidaFin.TotalMinutes - comidaIni.TotalMinutes);
                return true;
            }

            mtxtTiempoComida.Text = "0";

            return false;
        }

        private void mtxtComidaFin_Leave(object sender, EventArgs e)
        {
            validarHorarioComida();
        }

        private bool validaHorarioJornada()
        {
            TimeSpan entrada, salida;

            if (horaCampo(mtxtEntradaTurno, out entrada) && horaCampo(mtxtSalida, out salida) &&
                cbDiaSalida.SelectedIndex > 0 && cbDiaEntrada.SelectedIndex > 0 &&
                !(cbDiaSalida.SelectedIndex == cbDiaEntrada.SelectedIndex && entrada > salida))
            {

                int diasdif = (cbDiaSalida.SelectedIndex - cbDiaEntrada.SelectedIndex) * 24;
                int hours = 0;

                if (diasdif > 0)
                    hours = entrada.Hours - salida.Hours + diasdif;
                else
                    hours = salida.Hours - entrada.Hours;

                mtxtTiempoTrabajo.Text = Convert.ToString(hours);
                return true;
            }

            return false;
        }

        private void mtxtSalida_Leave(object sender, EventArgs e)
        {
            validaHorarioJornada();
        }

        private void mtxtComidaInicio_Leave(object sender, EventArgs e)
        {
            TimeSpan comidaIni;

            if (!horaCampo(mtxtComidaInicio, out comidaIni))
                mtxtComidaInicio.Text = "";
        }



        private void mtxt(object sender, EventArgs e)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            textBox.SelectAll();
        }

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void AsignacionTrabajadorPerfil_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;
            lbIdTrab.Text = TrabajadorInfo.IdTrab;
            lbNombre.Text = TrabajadorInfo.Nombre;
            PlantillaDetalle objPlantilla = new PlantillaDetalle();
            Utilerias.llenarComboxDataTable(cbPlantilla, objPlantilla.cbplantilla(5), "Clave", "Descripción");
            Utilerias.llenarComboxDataTable(cbDiaEntrada, objPlantilla.cbdias(6), "Clave", "Descripción");
            Utilerias.llenarComboxDataTable(cbDiaSalida, objPlantilla.cbdias(6), "Clave", "Descripción");
            TrabajadorHorario objHorario = AsignarObjeto();
            llenarGridHorario(objHorario);
            LimpiarFormulario();

            if (Permisos.dcPermisos["Crear"] != 1)
            {

                btnGuardarPlantilla.Visible = false;
                btnGuardar.Visible = false;
                btnAgregar.Visible = false;
                ckbEliminar.Visible = false;
            }

            PanelReloj.Visible = true;
            btnGuardarReloj.Visible = true;
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {

                Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Borrar");
                iOpcionAdmin = 3;
            }
            else
            {

                Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Editar");
                iOpcionAdmin = 2;
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            panelTagForReg.Visible = false;
            panelTagHuella.Visible = false;
            lbMensajeHuella.Text = "";
            panelTagRelojCheck.Visible = false;
            timer1.Stop();
        }


        private void tabAsignacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabAsignacion.SelectedIndex)
            {

                case 0:
                    TrabajadorHorario objHorario = AsignarObjeto();
                    llenarGridHorario(objHorario);
                    break;

                case 1:
                    LlenarGridFormasRegistro("%");
                    AsignarFormas(TrabajadorInfo.IdTrab);
                    panelPermisos.Enabled = false;
                    ltFormasReg.Clear();
                    if(Permisos.dcPermisos["Crear"] == 0) { panelPermisos.Visible = false; label24.Text = "Formas de Registro Asignadas Actualmente"; }
                    break;


                   

                case 2:
                    llenarGridReloj("%");
                    AsignarReloj(TrabajadorInfo.IdTrab);
                    PanelReloj.Enabled = false;
                    if (Permisos.dcPermisos["Crear"] == 0) { PanelReloj.Visible = false; label24.Text = "Relojes Asignados Actualmente"; }
                    break;


                case 3:
                    txtEstatus.Text = String.Empty;
                    PanelGuardarHuella.Visible = false;
                    pbHuella.Image = null;
                    PanelGuardarHuella.Visible = false;
                    panelTagHuella.Visible = false;
                    SonaTrabajador objTrab = new SonaTrabajador();
                    byte[] byteArray = new byte[] { };
                    //DataTable dtTrab = objTrab.GestionHuella(byteArray, byteArray, TrabajadorInfo.IdTrab, "", "", 5);
                    //try
                    //{
                    //    foreach (DataRow row in dtTrab.Rows)
                    //    { byteArray = (byte[])row["imgtemplate"]; }
                    //    Image imgHuella = null;
                    //    MemoryStream ms = new MemoryStream(byteArray);
                    //    imgHuella = Image.FromStream(ms);
                    //    ms.Close();
                    //    pbHuella.Image = imgHuella;
                    //}
                    //catch (Exception ex) {

                    //    pbHuella.Image = null;
                    //}
                    
                    
                    break;
                        }
                    if(Permisos.dcPermisos["Crear"] != 1) { panelHuella.Visible = false; lbHuella.Visible = false; }
                    if (Permisos.dcPermisos["Eliminar"] != 1) { checkBox1.Visible = false; }
        }


        //Eventos Huella Digital 

        public void Start()
        {
            if (null != Capturer)
            {
               
                try
                {
                    Capturer.StartCapture();
                    Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 1, "Escaneo Iniciado Correctamente. Ingrese su Huella en el Lector.");             
                }
                catch
                {
                    Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 3, "No es posible iniciar la Captura");                
                }
            }
        }

       

        public void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                  Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 3, "No es posible iniciar la Captura");
                
                }
            }
        }

      


        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }


        protected void Process(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            pbHuella.Image = new Bitmap(bitmap, pbHuella.Size);

            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (features != null) try
                {       
                    Enroller.AddFeatures(features);     // Add feature set to template.
                }
                finally
                {
                    lbHuella.Text = String.Format("Ingresos Necesarios: {0}", Enroller.FeaturesNeeded);

                    // Check if template has been created.
                    switch (Enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:  
                            Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 1, "Proceso Completado, es posible Guardar");
                            Stop();

                            prgbar.Visible = false;
                            PanelGuardarHuella.Visible = true;
                            break;

                        case DPFP.Processing.Enrollment.Status.Failed:  // report failure and restart capturing
                            Enroller.Clear();
                            Stop();
                            lbHuella.Text = String.Format("Ingresos Necesarios: {0}", Enroller.FeaturesNeeded);
                            OnTemplate(null);
                            Start();
                            break;
                    }
                }
        }


        public virtual void FormInit()
        {
            try
            {
                
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.
               if (null != Capturer)
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                else
                    Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 3, "No es posible iniciar la Captura");
            }
            catch
            {
               // MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

          
        //public void ExchangeData(bool read)
        //{
        //    if (read)
        //    {   // read values from the form's controls to the data object
        //        Data.EnrolledFingersMask = EnrollmentControl.EnrolledFingerMask;
        //        Data.MaxEnroll = EnrollmentControl.MaxEnrollFingerCount;
        //        Data.Update();
        //    }
        //    else
        //    {   // read values from the data object to the form's controls
        //        EnrollmentControl.EnrolledFingerMask = Data.EnrolledFingersMask;
        //        EnrollmentControl.MaxEnrollFingerCount = Data.MaxEnroll;
        //    }
        //}

        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            txtEstatus.AppendText("Huella CAPTURADA Correctamente." + "\r\n");        
            Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 2, "INGRESE su Huella Nuevamente");           
            Process(Sample); 
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            txtEstatus.AppendText("HUELLA REMOVIDA de Lector." + "\r\n");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            CheckForIllegalCrossThreadCalls = false;
            txtEstatus.AppendText("El Lector ha sido TOCADO." + "\r\n");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            CheckForIllegalCrossThreadCalls = false;
            txtEstatus.AppendText("El Lector CONECTADO." + "\r\n");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
   //         timer1.Start();
     //       await task
            //Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 2, "Dispositivo Desconectado");

        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
            txtEstatus.AppendText("La calidad de la Huella es CORRECTA." + "\r\n");
            else
                txtEstatus.AppendText("La calidad de la Huella es POBRE." + "\r\n");
        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------


        private void LlenarGridPlantilla(int iopcion, int icvplantilla)
        {

            if (c.Columns.Count > 1)
            {

                c.Columns.RemoveAt(0);
            }

            c.DataSource = null;
            PlantillaDetalle objPlantilla = new PlantillaDetalle();
            DataTable dtiplandet = objPlantilla.dgvplantillaDet(iopcion, icvplantilla);
            c.DataSource = dtiplandet;

            c.Columns["cvplantilla"].Visible = false;
            c.Columns["descplantilla"].Visible = false;
            c.Columns["cvdia"].Visible = false;
            c.Columns["cvddiasaltur"].Visible = false;
            c.Columns["cvdiasalcom"].Visible = false;
            c.Columns["cvdiaregcom"].Visible = false;
            c.Columns["stexiste"].Visible = false;

            c.Columns["Día"].HeaderText = "Día Entrada";
            c.Columns["HrEntTurno"].HeaderText = "Hora Entrada";
            c.Columns["DíaSalTurno"].HeaderText = "Día Salida";
            c.Columns["HrSalTurno"].HeaderText = "Hora Salida";
            c.Columns["DíaSalComer"].HeaderText = "Comida Inicio";
            c.Columns["HrSalComer"].HeaderText = "Hora Inicio";
            c.Columns["DíaRegComida"].HeaderText = "Comida Fin";
            c.Columns["HrRegComida"].HeaderText = "Hora Fin";
            c.Columns["TotJornada"].HeaderText = "Horas Trabajo";
            c.ClearSelection();

            if (Permisos.dcPermisos["Crear"] != 1) {

                c.Columns.RemoveAt(0);
            }

        }

        private void llenarGridHorario(TrabajadorHorario objHorario)
        {
            ckbEliminar.Visible = false;
            if (dgvHorario.Columns.Count > 1)
            {

                dgvHorario.Columns.RemoveAt(0);
            }

            iOpcionAdmin = 4;
            DataTable dtHorario = objHorario.GestionHorario(iOpcionAdmin);
            dgvHorario.DataSource = dtHorario;


            Utilerias.AgregarCheck(dgvHorario, 0);
            dgvHorario.Columns[1].Visible = false;

        }


        private TrabajadorHorario AsignarObjeto()
        {

            TrabajadorHorario objHorario = new TrabajadorHorario();
            objHorario.sIdTrab = TrabajadorInfo.IdTrab;
            objHorario.iCvPlantilla = Convert.ToInt32(cbPlantilla.SelectedValue.ToString());
            objHorario.iCvDia = 0;
            objHorario.sHoraEntrada = "00:00";
            objHorario.iCvdiaSalidaTurno = 0;
            objHorario.sHoraSalidaTurno = "00:00";
            objHorario.iTiempoComida = 0;
            objHorario.iCvdiaComidaInicio = 0;
            objHorario.sHoraComidaInicio = "00:00";
            objHorario.iCvdiaComidaFin = 0;
            objHorario.sHoraComidaFin = "00:00";
            objHorario.iHorasTotalTrabajo = 0;
            objHorario.sUsuumod = LoginInfo.IdTrab;
            objHorario.sPrgumod = this.Name;

            return objHorario;
        }


        private TrabajadorHorario RecuperarGrid(DataGridViewRow row)
        {

            TrabajadorHorario objHorario = new TrabajadorHorario();

            objHorario.sHoraEntrada = row.Cells[3].Value.ToString();
            objHorario.sHoraSalidaTurno = row.Cells[5].Value.ToString();
            objHorario.sHoraComidaInicio = row.Cells[8].Value.ToString();
            objHorario.sHoraComidaFin = row.Cells[10].Value.ToString();
            objHorario.iHorasTotalTrabajo = Convert.ToInt32(row.Cells[11].Value.ToString());
            objHorario.iTiempoComida = Convert.ToInt32(row.Cells[6].Value.ToString());

            objHorario.iCvDia = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[2].Value.ToString()));

            string aux = row.Cells[4].Value.ToString();
            objHorario.iCvdiaSalidaTurno = aux.Equals(string.Empty) ? 0 : Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), aux));

            aux = row.Cells[7].Value.ToString();
            objHorario.iCvdiaComidaInicio = aux.Equals(string.Empty) ? 0 : Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), aux));

            aux = row.Cells[9].Value.ToString();
            objHorario.iCvdiaComidaFin = aux.Equals(string.Empty) ? 0 : Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), aux));

            return objHorario;
        }

        public void LimpiarFormulario()
        {

            cbDiaEntrada.SelectedIndex = 0;
            cbDiaSalida.SelectedIndex = 0;

            mtxtEntradaTurno.Text = "";
            //  mtxtEntradaTurno.SelectionLength = 5;
            mtxtSalida.Text = "";
            //   mtxtSalida.SelectionLength = 5;
            mtxtComidaInicio.Text = "";
            //    mtxtComidaInicio.SelectionLength = 5;
            mtxtComidaFin.Text = "";
            //   mtxtComidaFin.SelectionLength = 5;
            mtxtTiempoComida.Text = "";
            //    mtxtTiempoComida.SelectionLength = 3;
            mtxtTiempoTrabajo.Text = "";
            //    mtxtTiempoTrabajo.SelectionLength = 3;
            

        }

        /*private void AsignarGrupo()
        {
            int Valor = 0;
            RelojChecador objReloj = new RelojChecador();
            DataTable dt = objReloj.RelojesxTrabajador(this.lbIdTrab.Text, 25, 11, "%", "%");
            DataRow row = dt.Rows[0];
            --Grupo= Valor = Convert.ToInt32(row["cvgruposreloj"].ToString());
            
            foreach (DataGridViewRow fila in dgvForReg.Rows)
            {
             int aux = Convert.ToInt32( fila.Cells[1].Value.ToString());
               if (aux==Valor)
                {
                    fila.Cells[0].Value= Resources.ic_check_circle_green_400_18dp;
                    fila.Cells[0].Tag = "check";
                    break;
                }
            }
            
        }*/


        private void AsignarReloj(string sIdtrab)
        {
            RelojChecador objReloj = new RelojChecador();
            DataTable dt =  objReloj.RelojesxTrabajador(sIdtrab, 0, 4, "", "");

            foreach (DataRow row in dt.Rows) {

                if (!ltRelojxUsuario.Contains(Convert.ToInt32(row[0]).ToString())) {

                    ltRelojxUsuario.Add(Convert.ToInt32(row[0]).ToString());
                }
            }

            Utilerias.ImprimirAsignacionesGrid(dgvReloj, 0, 1, ltRelojxUsuario);
            int admin = 0;
            DataTable dt2 = objReloj.RelojesxTrabajador(lbIdTrab.Text, 25, 14, "%", "%");
            foreach (DataRow row in dt2.Rows)
            {
                if (Convert.ToBoolean(row["administrador"].ToString()))
                    admin = 1;
            }
            if (admin != 0)
                chkAdmin.Checked = true;
        }


        private void LlenarGridFormasRegistro(string sBusqueda)
        {

            if (dgvForReg.Columns.Count > 1)
            {
                dgvForReg.Columns.RemoveAt(0);
            }

            FormaReg objFr = new FormaReg();
            DataTable dtFormasRegistro = objFr.FormaReg_S(4, 0, sBusqueda, 0, "", "");
            dgvForReg.DataSource = dtFormasRegistro;
            Utilerias.AgregarCheck(dgvForReg, 0);
            dgvForReg.Columns[1].Visible = false;
            dgvForReg.Columns[4].Visible = false;
            dgvForReg.Columns[3].Visible = false;
            dgvForReg.Columns[0].Width = 65;

            dgvForReg.ClearSelection();
        }

        private void llenarGridGrupos(string sDescripcion)
        {
            if (dgvForReg.Columns.Count > 1)
                dgvForReg.Columns.RemoveAt(0);
            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(11, 0, sDescripcion, "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
            dgvForReg.DataSource = dtRelojChecador;
            Utilerias.AgregarCheck(dgvForReg, 0);
            dgvForReg.Columns[0].Width = 65;
         
            dgvForReg.Columns[1].Visible = false; 
            dgvForReg.Columns[2].Visible = true;
            dgvForReg.ClearSelection();
          
        }


        private void llenarGridReloj(string sDescripcion)
        {
            if (dgvReloj.Columns.Count > 1)
            {
                dgvReloj.Columns.RemoveAt(0);
            }

            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(4, 0, sDescripcion, "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
            
            dgvReloj.DataSource = dtRelojChecador;

            Utilerias.AgregarCheck(dgvReloj, 0);
            //dgvRelojesChecadores.Columns[0].Width = 75;
            //dgvRelojesChecadores.Columns[1].Width = 50;
            

            dgvReloj.Columns[0].Width = 65; //65
            dgvReloj.Columns[1].Visible = false;
            dgvReloj.Columns[3].Visible = false;
            dgvReloj.Columns[4].Visible = false;
            dgvReloj.Columns[5].Visible = false;
            dgvReloj.Columns[6].Visible = false;
            dgvReloj.Columns[7].Visible = false;
            
            
            /*dgvReloj.Columns[7].Visible = false;
            dgvReloj.Columns[8].Visible = false;
            dgvReloj.Columns[9].Visible = false;*/
            dgvReloj.ClearSelection();
            
           // int admin = 0;
           // DataTable dt = objReloj.RelojesxTrabajador(lbIdTrab.Text, 25, 14, "%", "%");
           // foreach (DataRow row in dt.Rows)
           //  {
           //      if (Convert.ToBoolean(row["administrador"].ToString()))
           //         admin = 1;
           //   }
           //if (admin != 0)
           //   chkAdmin.Checked = true;
           
          }

        private void AsignarFormas(string sIdtrab)
        {

            FormaReg objfr = new FormaReg();
            ltFormasxUsuario = objfr.FormasxUsuario(sIdtrab, 0, 4, "", "");
            foreach (string grupo in ltFormasxUsuario)
            {
                Grupo =  Convert.ToInt32( grupo);
               
            }
            Utilerias.ImprimirAsignacionesGrid(dgvForReg, 0, 1, ltFormasxUsuario);

        }

        private void CrearAsignaciones_Reloj(string sUsuuMod, string sPrguMod, int iOpcion)
        {
            RelojChecador objReloj = new RelojChecador();
            SonaTrabajador objTrab = new SonaTrabajador();
            bool bConexion = false;
            int iCont = 0;
            foreach (Reloj obj in ltReloj2)
            {
                iCont += 1;
                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj2.Count);
                bConexion = Connect_Net(obj.IpReloj, 4370);
              if (bConexion != false)
                {
                    
                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, iOpcion, sUsuuMod, sPrguMod);
                     objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, Grupo, 12, sUsuuMod, Name);
                    if (chkAdmin.Checked == true)
                        objTrab.GestionIdentidad(TrabajadorInfo.IdTrab, "", "", "0", sUsuuMod, sPrguMod, 8);
                    string idtrab = lbIdTrab.Text;
                    string Nombre = lbNombre.Text;
                    bool BeginBatchUpdate = false;
                    int iUpdateFlag = 1;
                    BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, iUpdateFlag);
                    objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, "", 0, true); 
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    bool RefreshData = objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();

                     if (!BatchUpdate)
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.IpReloj);

                }
                else
                {
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                    break;
                }



                bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {

                  
                    string idtrab = lbIdTrab.Text;
                    bool BeginBatchUpdate = false;
                    int iUpdateFlag = 1;
                    BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, iUpdateFlag);
                    objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo);  
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    bool RefreshData = objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();
                    if (!BatchUpdate)
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.IpReloj);

                }
                 else
                {
                   ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                    break;
                }
   
            }
            
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Datos guardados en el servidor");
            DialogResult Resultado = MessageBox.Show("Por favor capture los biométricos del empleado, AL TERMINAR \npresione ACEPTAR para que los datos se sincronicen\nen caso de no poder tomar los biometricos, presione CANCELAR", "SIPAA", MessageBoxButtons.OKCancel);
            if (Resultado == DialogResult.OK)
            {
                foreach (Reloj obj in ltReloj2)
                {
                    
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Obteniendo los biométricos.");
                        ProcesoReloj("Face",obj);
                        ProcesoReloj("Huella", obj);
                        ProcesoReloj("Pass", obj);

                    break;
                      
                }
             
               ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Comenzando la sincronizacion.");
            //   SincronizaBiometricos(ltReloj2, objReloj);
              
                 
            }
            
           // Utilerias.ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Asignaciones Guardadas Correctamente");
            timer1.Start();
            AsignarReloj(TrabajadorInfo.IdTrab);
            PanelReloj.Enabled = false;
            ltReloj.Clear();

        }

        public void SincronizaBiometricos(List<Reloj> ltReloj2, RelojChecador objReloj)
        {
            int iCont = 0;
            bool bConexion = false;
            SonaTrabajador objTrab = new SonaTrabajador();
            foreach (Reloj obj in ltReloj2)
            {

                //******************************************************************************************************************//

                iCont += 1;
                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Conectando con Dispositivo " + iCont + " de " + ltReloj2.Count);
                //***  UserInfo ***//
                bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {

                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 2, sUsuuMod, Name); // 2
                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, Grupo, 12, sUsuuMod, Name);
                    if (chkAdmin.Checked == true)
                        objTrab.GestionIdentidad(TrabajadorInfo.IdTrab, "", "", "0", sUsuuMod, Name, 8);
                    //string idtrab = lbIdTrab.Text;
                    //string Nombre = lbNombre.Text;
                    bool BeginBatchUpdate = false;
                    int iUpdateFlag = 1;
                    BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, iUpdateFlag);
                    objCZKEM.SSR_SetUserInfo(1, lbIdTrab.Text, lbNombre.Text, "", 0, true);
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    bool RefreshData = objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();

                    if (!BatchUpdate)
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.IpReloj);

                }
                else
                {
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                    break;
                }


                //***  grupo ***//
                bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {

                    
                    bool BeginBatchUpdate = false;
                    int iUpdateFlag = 1;
                    BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, iUpdateFlag);
                    objCZKEM.SetUserGroup(1, Convert.ToInt32(lbIdTrab.Text), Grupo);
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    bool RefreshData = objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();
                    if (!BatchUpdate)
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.IpReloj);

                }
                else
                {
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                    break;
                }


                //***  Cara ***//
               DataTable dt = objReloj.RelojesxTrabajador(lbIdTrab.Text, obj.cvReloj, 6, "%", "%");
               foreach (DataRow row in dt.Rows)
                    {
                        bConexion = Connect_Net(obj.IpReloj, 4370);
                        if (bConexion != false)
                        {
                            bool BeginBatchUpdate = false;
                            int iUpdateFlag = 1;
                            BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, iUpdateFlag);

                            if (row["rostroTmp"].ToString() != String.Empty)
                            {
                                try
                                {
                                    string RostroTmp = row["rostroTmp"].ToString();
                                    int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                               if ( objCZKEM.SSR_SetUserInfo(1, lbIdTrab.Text, lbNombre.Text, "", 0, true))
                                {
                                    if (objCZKEM.SetUserFaceStr(1, lbIdTrab.Text, 50, RostroTmp, rostrolong))
                                        MessageBox.Show("Inserto cara");
                                }
                                
                                }
                                catch { }
                            }
                            bool BatchUpdate = objCZKEM.BatchUpdate(1);
                            bool RefreshData = objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();
                            if (!BatchUpdate)
                                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.IpReloj);
                        }
                        else
                        {
                            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                            break;
                        }
                        
                    }

                    

                
                


                //***********************************************************************************************************************************************//
                // DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 6, "%", "%");
                // string IP = obj.IpReloj;
                // bool tiene = obj.Huella;
                // iCont2 += 1;
                // Utilerias.ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Conectando con Dispositivo " + iCont2 + " de " + ltReloj2.Count);
                //// bConexion = Connect_Net(obj.IpReloj, 4370);
                //// if (!bConexion)
                // //{


                //     foreach (DataRow row in dt.Rows)
                //     {

                //         string idtrab = row["idtrab"].ToString();
                //         string cvreloj = row[1].ToString();
                //         string Nombre = row["Nombre"].ToString();
                //         string pass_desc = "";
                //         int Permiso = 0;
                //         if (Convert.ToBoolean(row["administrador"].ToString()))
                //             Permiso = 3;
                //         int iUpdateFlag = 1;
                //         bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, iUpdateFlag);

                //         if (obj.Teclado)
                //         {
                //             try
                //             {
                //                 bConexion = Connect_Net(obj.IpReloj, 4370);
                //                 if (bConexion)
                //                 {
                //                 ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "sincronizando biométricos. . ");


                //                 if (row["pass"].ToString() != String.Empty)
                //                         pass_desc = Utilerias.descifrar(row["pass"].ToString());
                //                     if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true))
                //                     {
                //                         if (obj.MultipleHuella)
                //                         {
                //                             if (row["huellaTmp"].ToString() != String.Empty)
                //                             {
                //                                 try
                //                             {
                //                                 ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "sincronizando biométricos. . . . ");
                //                                 int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                //                                     if (ifinger != 0 && ifinger != 10)
                //                                     {
                //                                         string tmpHuella = row["huellaTmp"].ToString();
                //                                         objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);
                //                                     }
                //                                 }
                //                                 catch { }
                //                             }
                //                         }
                //                     }
                //                     bool BatchUpdate = objCZKEM.BatchUpdate(1);
                //                     bool RefreshData = objCZKEM.RefreshData(1);
                //                     if (!BatchUpdate)
                //                         ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.IpReloj);
                //                     objCZKEM.Disconnect();

                //                 }


                //             }
                //             catch
                //             { }
                //         }

                //         //if (obj.Huella)
                //         //{
                //         //    if (obj.MultipleHuella)
                //         //    {
                //         //        if (row["huellaTmp"].ToString() != String.Empty)
                //         //        {
                //         //            try
                //         //            {
                //         //              int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                //         //              if (ifinger != 0 && ifinger != 10)
                //         //                {
                //         //                   string tmpHuella = row["huellaTmp"].ToString();
                //         //                   objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);
                //         //                 }
                //         //            }
                //         //            catch { }
                //         //        }
                //         //    }
                //         //    else
                //         //    {
                //         //        int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                //         //        string tmpHuella = "";
                //         //        if (ifinger >= 0 && ifinger <= 9)
                //         //        {
                //         //         tmpHuella = row["huellaTmp"].ToString();
                //         //         objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);
                //         //        }
                //         //    }
                //         //}

                //         ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "sincronizando. . . . . . . ");

                //     }


                // //}
                // //else
                //// {
                //  //   Utilerias.ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                //   //  break;

                //// }

                // ///***********Segunda conexion ****************************************

                // //bConexion = Connect_Net(obj.IpReloj, 4370);
                //// if (!bConexion)
                //// {

                //     foreach (DataRow row in dt.Rows)
                //     {

                //         bConexion = Connect_Net(obj.IpReloj, 4370);
                //         if (!bConexion)
                //         {
                //         string idtrab = row["idtrab"].ToString();
                //         string cvreloj = row[1].ToString();
                //         string Nombre = row["Nombre"].ToString();
                //         string pass_desc = "";
                //         int Permiso = 0;
                //         if (Convert.ToBoolean(row["administrador"].ToString()))
                //             Permiso = 3;
                //         int Grupo = Convert.ToInt32(row["cvforma"].ToString());
                //         if (obj.Rostro)
                //         {
                //             if (row["rostroTmp"].ToString() != String.Empty)
                //             {
                //                 try
                //                 {
                //                     if (row["pass"].ToString() != String.Empty)
                //                         pass_desc = Utilerias.descifrar(row["pass"].ToString());
                //                     if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true))
                //                     {
                //                         string RostroTmp = row["rostroTmp"].ToString();
                //                         int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                //                         if (objCZKEM.SetUserFaceStr(1, idtrab, 50, RostroTmp, rostrolong))
                //                         {
                //                             if (objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo))
                //                                 MessageBox.Show("No esta guardando el grupo");
                //                         }

                //                     }

                //                 }
                //                 catch { }
                //             }
                //         }
                //         }

                //     }
                // //}


                // ///**************************************

                // //else
                // //{
                // //    Utilerias.ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.IpReloj);
                // //    break;

                // //}
            }
           ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Asignaciones Guardadas Correctamente");
            
        }
        
        public void ProcesoReloj(string Opcion, Reloj obj)
        {
            int iCont = 0;
            //foreach (Reloj obj in ltReloj2)
            //{
                RelojChecador objReloj = new RelojChecador();
                DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 6, "%", "%");
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("Este Reloj no tiene Personal Asignado.", "SIPAA", MessageBoxButtons.OK);
                   //break;
                }
                iCont += 1;
                bool bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {
                    objCZKEM.ReadAllUserID(1);
                    objCZKEM.ReadAllTemplate(1);
                    bool bBandera = false;
                    int iContTrab = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        iContTrab += 1;
                        string idTrab = row["idTrab"].ToString();
                        string cvreloj = row[1].ToString();
                        bBandera = ConsultaReloj(Opcion, idTrab, iContTrab, dt.Rows.Count);
                    }
                    objCZKEM.Disconnect();

                    if (bBandera) 
                       objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                   
                   
                    else
                    {
                      
                    }
                }
                else
                {
                    MessageBox.Show("No fue posible conectarse a la IP: " + obj.IpReloj, "SIPAA", MessageBoxButtons.OK);
                    
                }
            //}

       }
        public bool ConsultaReloj(string Opcion, string idtrab, int iContTrab, int iTotal)
        {

            string sFaceTmp = "";
            int iFaceLong = 0;
            bool bBandera = false;
          
           
            switch (Opcion)
            {

                case "Face":


                    if (objCZKEM.GetUserFaceStr(1, idtrab, 50, ref sFaceTmp, ref iFaceLong))// true
                    {
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, "", sFaceTmp, iFaceLong.ToString(), LoginInfo.IdTrab, this.Name, 7);
                            bBandera = true;
                        }
                        catch
                        {
                            bBandera = false;
                        }
                    }
                    
                    break;
                    /*
                case "Foto":
                    int longitud = 0;
                    byte Foto;
                    string NombreFoto = "170014.jpg";
                    if (objCZKEM.GetPhotoByName(1,NombreFoto, out Foto, out longitud))
                        MessageBox.Show("Foto");
                    

                    break;
                    */
                case "Huella":
                    
                    int flag = 0;
                    string huellatmp = "";
                    int tpmlong = 0;
                    string sIdTrab = idtrab;
                    string sNombre = "";
                    string sPass = "";
                    int iPrivilegio = 0;
                    bool bActivo = false;
                    

                    if (objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
                    {
                        for (int iFinger = 0; iFinger < 10; iFinger++)
                        {
                            if (objCZKEM.GetUserTmpExStr(1, sIdTrab, iFinger, out flag, out huellatmp, out tpmlong))
                            {
                                SonaTrabajador objTrab = new SonaTrabajador();
                                try
                                {
                                    objTrab.GestionHuella(sIdTrab, huellatmp, iFinger, LoginInfo.IdTrab, this.Name, 5);
                                    bBandera = true;
                                }
                                catch
                                {
                                    bBandera = false; 
                                }
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
                        {
                            Cifrado = Utilerias.cifrarPass(Pass, 1);
                        }
                        SonaTrabajador objTrab = new SonaTrabajador();
                        try
                        {
                            objTrab.GestionIdentidad(idtrab, Cifrado, "", "0", LoginInfo.IdTrab, this.Name, 6);
                            bBandera = true; 
                        }
                        catch (Exception ex)
                        {
                            bBandera = false; 
                        }
                    }
                    break;


            }

            return bBandera;

        }


        private void CrearAsignaciones_FormasRegistro(string sUsuuMod, string sPrguMod)
        {
            FormaReg objFr = new FormaReg();

            foreach (int cv in ltFormasReg)
            {
                objFr.FormasxUsuario(TrabajadorInfo.IdTrab, cv, 1, sUsuuMod, sPrguMod);
            }

            Utilerias.ControlNotificaciones(panelTagForReg, lbMensajeForReg, 1, "Asignaciones Guardadas Correctamente");
            timer1.Start();
            AsignarFormas(TrabajadorInfo.IdTrab);
            panelPermisos.Enabled = false;
            ltFormasReg.Clear();

        }




        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tabPlantillaHorario_Click(object sender, EventArgs e)
        {

        }

        private void tabFormasRegistro_Click(object sender, EventArgs e)
        {
        }

        private void tabReloj_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PanelGuardarHuella.Visible = true;
            Utilerias.AsignarBotonResize(btnGuardarHuella,Utilerias.PantallaSistema(),"Borrar");
            iOpcionAdmin = 3;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void lbIdTrab_Click(object sender, EventArgs e)
        {

        }

        private void tabAsignacion_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabAsignacion.TabPages[3])
            {
                e.Cancel = true;
            }
        }

        private void dgGrupos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {/*
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {
                PanelReloj.Enabled = true; 
                 foreach (DataGridViewRow fila in dgvGrupos.Rows)
                  {
                  fila.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                  fila.Cells[0].Tag = "uncheck";
                  }
              if (dgvGrupos.SelectedRows.Count != 0)
                {
                 DataGridViewRow row = dgvGrupos.SelectedRows[0];
                   
                    Grupo = Convert.ToInt32( row.Cells[1].Value);
                   // MessageBox.Show(Grupo.ToString());
                   

                    try
                    {
                        switch (row.Cells[0].Tag.ToString())
                        {
                            case "check":
                                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                                row.Cells[0].Tag = "uncheck";

                                break;
                            case "uncheck":
                                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                                row.Cells[0].Tag = "check";

                                break;
                        }
                                 
                    }
                    catch
                    {
                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        row.Cells[0].Tag = "check";
                    }
                }
            }*/
       }

        private void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdmin.Checked==true)
            {
                relojseleccionados();
                //PanelReloj.Enabled = true;
                if (ltReloj2.Count == ltRelojxUsuario.Count)
                {
                    RelojChecador objReloj = new RelojChecador();
                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, 25, 13, sUsuuMod, Name);
                }
               
            }
            else
            {
                relojseleccionados();
               // PanelReloj.Enabled = true;
                if (ltReloj2.Count == ltRelojxUsuario.Count)
                {
                    RelojChecador objReloj = new RelojChecador();
                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, 25, 12, sUsuuMod, Name);
                }
            }

            

               

        }

        private void mtxtComidaInicio_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mtxtEntradaTurno_Leave(object sender, EventArgs e)
        {
            TimeSpan entrada;

            if (!horaCampo(mtxtEntradaTurno, out entrada))
                mtxtEntradaTurno.Text = "";
        }

        private void mtxtEntradaTurno_Enter(object sender, EventArgs e)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            textBox.SelectAll();
        }

        private void mtxtSalida_Enter(object sender, EventArgs e)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            textBox.SelectAll();
        }

        private void mtxtComidaInicio_Enter(object sender, EventArgs e)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            textBox.SelectAll();
        }

        private void mtxtComidaFin_Enter(object sender, EventArgs e)
        {
            MaskedTextBox textBox = (MaskedTextBox)sender;
            textBox.SelectAll();
        }
    }
}
