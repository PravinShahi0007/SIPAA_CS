
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
        List<Reloj> ltElimina = new List<Reloj>(); //para eliminar las asignaciones
        List<Reloj> ltAgrega = new List<Reloj>(); //para agregar nuevos relojes
        List<Reloj> ltCambiaAsociacion = new List<Reloj>(); //para Cambiar asociacion
        public List<string> ltRelojxUsuario = new List<string>();
        public List<string> ltRelojxUsuario2 = new List<string>();
        public string sUsuuMod = LoginInfo.IdTrab;
        SonaTrabajador contenedorempleados = new SonaTrabajador();

        //Huella Digital
        public delegate void OnTemplateEventHandler(DPFP.Template template);
       // private DPFP.Capture.Capture Capturer;
        public Bitmap bitmap;
        public DPFP.Processing.Enrollment Enroller;
       // public event OnTemplateEventHandler OnTemplate;
        private LinkedList<FaceTmp> faces;

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
            public string Descripcion; 

        }
        public bool Connect_Net(string IPAdd, int Port)
        {
            if (objCZKEM.Connect_Net(IPAdd, Port))
            {
                if (objCZKEM.RegEvent(1, 65535))
                {

                    /*comente las lineas de codigo de abajo porque no se para que las pusieron, son eventos del reloj 
                     * pero no tienen nada asignado 
                     * ya despues vere para que sirven
                     */
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

                if (Permisos.dcPermisos["Actualizar"] == 1)
                {
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
                    objR.Descripcion = row.Cells["Descripción"].Value.ToString();
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

            if (validaHorarioJornada(false))
            {
                cbDiaEntrada.Enabled = true;

                if (validarHorarioComida(false) && cbDiaEntrada.SelectedIndex == cbDiaSalida.SelectedIndex)
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

                }
                else
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
                objHorario.iHorasTotalTrabajo = tiempoCampo(mtxtTiempoTrabajo);//
                objHorario.iTiempoComida = tiempoCampo(mtxtTiempoComida);//
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

        private double tiempoCampo(MaskedTextBox campo)
        {
            string val = campo.Text.Trim();
            return val.Equals(string.Empty) ? val.Length : Convert.ToDouble(val);
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
                //string UsuuMod = LoginInfo.IdTrab;
                //string PrguMod = this.Name;
                FormaReg objFr = new FormaReg();
                LlenarGridFormasRegistro("%");
                AsignarFormas(TrabajadorInfo.IdTrab);
                if (SinAsignaciones(dgvForReg, 0, 1, ltFormasReg) == true)
                {
                    CrearAsignaciones_FormasRegistro(LoginInfo.IdTrab, Name);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Seguro que desea quitar todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                        CrearAsignaciones_FormasRegistro(LoginInfo.IdTrab, Name);
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


            //string sBusqueda = "";
            //if (txtBuscarReloj.Text != String.Empty)
            //    sBusqueda = txtBuscarReloj.Text;
            //else
            //    sBusqueda = "%";

            //llenarGridReloj(sBusqueda);
            AsignarReloj(TrabajadorInfo.IdTrab);
            PanelReloj.Enabled = false;
            ltReloj.Clear();

        }


        private void btnGuardarReloj_Click(object sender, EventArgs e)
        {

            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Comienza proceso");
            
            try
            {

                relojseleccionados();

                if (ltReloj2.Count > 0)
                     CrearAsignaciones_Reloj(LoginInfo.IdTrab, Name, iOpcionAdmin);
                
                else
                {
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No ha seleccionado ningún reloj.");
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
          /*  PanelGuardarHuella.Visible = true; // estaba false

            prgbar.Visible = true;
            prgbar.Style = ProgressBarStyle.Marquee;



            Utilerias.AsignarBotonResize(btnGuardarHuella, PantallaSistema(), "Guardar");

            Enroller = new DPFP.Processing.Enrollment();            // Create an enrollment.

            lbHuella.Text = String.Format("Ingresos Necesarios: {0}", Enroller.FeaturesNeeded);
            FormInit();
            Start();


            */
        }

        public CZKEMClass objCZKEM = new CZKEMClass();
        private void btnGuardarHuella_Click(object sender, EventArgs e)
        {
          //try
          //  {


          //      switch (iOpcionAdmin)
          //      {

          //          // 1
          //          case 4:
          //              int flag = 0;
          //              string huellatmp = "";
          //              int tpmlong = 0;
          //              string sIdTrab = "";

          //              SonaTrabajador objTrab = new SonaTrabajador();

          //              try
          //              {
          //                  byte[] arr = Enroller.Template.Bytes;


          //                  //DataTable dtTrab = objTrab.GestionHuella(huellaTmp: Convert.ToBase64String(arr, 0, arr.Length), iHuella: 4, sIdtrab: TrabajadorInfo.IdTrab, sUsuumod: sUsuuMod, sPrgmod: this.Name, iOpcion: 5);
          //                  objTrab.GestionHuella(sIdTrab, Convert.ToBase64String(arr, 0, arr.Length), 4, LoginInfo.IdTrab, this.Name, 5);

          //              }
          //              catch
          //              {

          //              }

          //              //Image img = bitmap;
          //              //ImageConverter converter = new ImageConverter();
          //              //byte[] arrImagen = (byte[])converter.ConvertTo(img, typeof(byte[]));

          //              //SonaTrabajador objTrab = new SonaTrabajador();


          //              //dtTrab = objTrab.GestionHuella(System.Text.Encoding.UTF8.GetString(Enroller.Template.Bytes), System.Text.Encoding.UTF8.GetString(arrImagen), Convert.ToInt32(TrabajadorInfo.IdTrab), sUsuuMod, this.Name, 1);
          //              //**********************************************//
          //              // if (dtTrab.Columns.Contains("INSERT"))
          //              //{
          //              Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 1, "Huella Asignada Correctamente");
          //              Enroller.Clear();
          //              //}

          //              break;

          //          case 3:
          //              //sonatrabajador obj = new sonatrabajador();
          //              //dttrab = obj.gestionhuella(enroller.template.bytes, new byte[] { }, trabajadorinfo.idtrab, susuumod, this.name, 3);
          //              //if (dttrab != null)
          //              //{
          //              //    utilerias.controlnotificaciones(paneltaghuella, lbmensajehuella, 1, "huella eliminada correctamente");
          //              //    enroller.clear();
          //              //}
          //              break;

          //      }



          //  }
          //  catch (Exception EX)
          //  {

          //      Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 3, "Error de Comunicación con el Servidor. Favor de Intentarlo más tarde.");
          //  }

        }


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        private bool horaCampo(MaskedTextBox mtxtb, out TimeSpan ts)
        {
            return TimeSpan.TryParse(mtxtb.Text, out ts);
        }

        private bool validarHorarioComida(bool isLeave)
        {
            TimeSpan comidaIni, comidaFin;

            if (horaCampo(mtxtComidaInicio, out comidaIni) && horaCampo(mtxtComidaFin, out comidaFin) && comidaFin > comidaIni)
            {
                if (isLeave)
                    mtxtTiempoComida.Text = Convert.ToString(comidaFin.TotalMinutes - comidaIni.TotalMinutes);
                return true;
            }

            mtxtTiempoComida.Text = "0";

            return false;
        }

        private void mtxtComidaFin_Leave(object sender, EventArgs e)
        {
            validarHorarioComida(true);
        }

        private bool validaHorarioJornada(bool isLeave)
        {
            TimeSpan entrada, salida;

            //if (horaCampo(mtxtEntradaTurno, out entrada) && horaCampo(mtxtSalida, out salida) &&
            //    cbDiaSalida.SelectedIndex > 0 && cbDiaEntrada.SelectedIndex > 0 &&
            //    !(cbDiaSalida.SelectedIndex == cbDiaEntrada.SelectedIndex && entrada > salida))
            //{
            if (horaCampo(mtxtEntradaTurno, out entrada) && cbDiaEntrada.SelectedIndex > 0)
            {
                int hours = 0;

                if (cbDiaSalida.SelectedIndex > 0 && horaCampo(mtxtSalida, out salida))
                {
                    int diasdif = (cbDiaSalida.SelectedIndex - cbDiaEntrada.SelectedIndex) * 24;

                    if (diasdif > 0)
                        hours = entrada.Hours - salida.Hours + diasdif;
                    else
                        hours = salida.Hours - entrada.Hours;
                }

                if (isLeave)
                {
                    mtxtTiempoTrabajo.Text = Convert.ToString(hours);
                }

                return true;
            }

            return false;
        }

        private void mtxtSalida_Leave(object sender, EventArgs e)
        {
            validaHorarioJornada(true);
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
            Utilerias.cargaimagen(ptbimgusuario);

            lbIdTrab.Text = TrabajadorInfo.IdTrab;
            lbNombre.Text = TrabajadorInfo.Nombre;
            PlantillaDetalle objPlantilla = new PlantillaDetalle();
            llenarComboxDataTable(cbPlantilla, objPlantilla.cbplantilla(5), "Clave", "Descripción");
            llenarComboxDataTable(cbDiaEntrada, objPlantilla.cbdias(6), "Clave", "Descripción");
            llenarComboxDataTable(cbDiaSalida, objPlantilla.cbdias(6), "Clave", "Descripción");
            AsignarBotonResize(btnEliminar, new Size(sysW, sysH), "Borrar");

            DataTable dtempleadosInactivos = contenedorempleados.obtenerempleados(4, "");
            DataTable dtempleadosActivos = contenedorempleados.obtenerempleados(7, "");

            llenarComboxDataTable(cbEmpleadosInactivos, dtempleadosInactivos, "NoEmpleado", "Nombre");
            //llenarComboxDataTable(cbEmpleadosActivos, dtempleadosActivos, "NoEmpleado", "Nombre");

            TrabajadorHorario objHorario = AsignarObjeto();
            llenarGridHorario(objHorario);
            LimpiarFormulario();

            if (Permisos.dcPermisos["Crear"] != 1)
            {

                btnGuardarPlantilla.Visible = false;
                btnGuardar.Visible = false;
                btnAgregar.Visible = false;
                ckbEliminar.Visible = false;
                btnEliminar.Visible = false; 
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
           // panelTagHuella.Visible = false;
            //lbMensajeHuella.Text = "";
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
                    if (Permisos.dcPermisos["Crear"] == 0) { panelPermisos.Visible = false; label24.Text = "Formas de Registro Asignadas Actualmente"; }
                    break;
                    
                case 2:

                  
                    llenarGridReloj("%");
                    AsignarReloj(TrabajadorInfo.IdTrab);
                    if (Permisos.dcPermisos["Crear"] == 0) { PanelReloj.Visible = false; label24.Text = "Relojes Asignados Actualmente"; }
                    break;


                case 3:

                    llenarGridElimina("%", dgvAgrega);
                    AsignaParaEliminar(dgvAgrega);
                    if (Permisos.dcPermisos["Crear"] == 0) { pnlAgrega.Visible = false; }
                    break;
                case 4:
                    llenarGridElimina("%", dgvElimina);
                    AsignaParaEliminar(dgvElimina);
                    if (Permisos.dcPermisos["Eliminar"] == 0) { pnlElimina.Visible = false; }
                    break;
                case 5:
                    llenarGridElimina("%", dgvCambiaAsociacion);
                    AsignaParaEliminar(dgvCambiaAsociacion);
                    if (Permisos.dcPermisos["Eliminar"] == 0) { pnlCambiarAsociacion.Visible = false; }
                    break;
            }
          
        }


        //Eventos Huella Digital 
        private void AsignaParaEliminar(DataGridView dgv)
        {
            RelojChecador objReloj = new RelojChecador();
            DataTable dt = objReloj.RelojesxTrabajador("0", 0, 4, "", "");
            foreach (DataRow row in dt.Rows)
            {
                if (!ltRelojxUsuario2.Contains(Convert.ToInt32(row[0]).ToString()))
                    ltRelojxUsuario2.Add(Convert.ToInt32(row[0]).ToString());
            }

            Utilerias.ImprimirAsignacionesGrid(dgv, 0, 1, ltRelojxUsuario2);
        }


      


        public void Start()
        {
           
        }



        public void Stop()
        {
          
        }




        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            /*DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
             DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
             DPFP.FeatureSet features = new DPFP.FeatureSet();
             Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
             if (feedback == DPFP.Capture.CaptureFeedback.Good)
                 return features;
             else*/
            return null;
        }


        protected void Process(DPFP.Sample Sample)
        {
          
        }


        public virtual void FormInit()
        {
            
        }


       
        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
        
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
          
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            CheckForIllegalCrossThreadCalls = false;
            
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            CheckForIllegalCrossThreadCalls = false;
            
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            

        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
           
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

            if (Permisos.dcPermisos["Crear"] != 1)
            {

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
            objHorario.iHorasTotalTrabajo = Convert.ToDouble(row.Cells[11].Value.ToString());
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
            mtxtSalida.Text = "";
            mtxtComidaInicio.Text = "";
            mtxtComidaFin.Text = "";
            mtxtTiempoComida.Text = "";
            mtxtTiempoTrabajo.Text = "";
       
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
            dgvReloj.Columns[0].Width = 15; 
            dgvReloj.Columns[1].Width = 65; 
           
            dgvReloj.Columns[1].Visible = false;
            dgvReloj.Columns[3].Visible = false;
            dgvReloj.Columns[4].Visible = false;
            dgvReloj.Columns[5].Visible = false;
            dgvReloj.Columns[6].Visible = false;
            dgvReloj.Columns[7].Visible = false;

            dgvReloj.ClearSelection();

         

        }
        private void llenarGridElimina(string sDescripcion, DataGridView dgv)
        {
            if (dgv.Columns.Count > 1)
            {
                dgv.Columns.RemoveAt(0);
            }

            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(4, 0, sDescripcion, "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);

            dgv.DataSource = dtRelojChecador;

           

            Utilerias.AgregarCheck(dgv, 0);
            dgv.Columns[0].Width = 15; 
            dgv.Columns[1].Width = 65; 
            dgv.Columns[1].Visible = false;
            dgv.Columns[3].Visible = false;
            dgv.Columns[4].Visible = false;
            dgv.Columns[5].Visible = false;
            dgv.Columns[6].Visible = false;
            dgv.Columns[7].Visible = false;
            dgv.ClearSelection();
        }

        private void AsignarFormas(string sIdtrab)
        {

            FormaReg objfr = new FormaReg();
            ltFormasxUsuario = objfr.FormasxUsuario(sIdtrab, 0, 4, "", "");
            foreach (string grupo in ltFormasxUsuario)
            {
                Grupo = Convert.ToInt32(grupo);

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
                objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, iOpcion, sUsuuMod, sPrguMod);
               
            }

            

            objTrab.GestionIdentidad(TrabajadorInfo.IdTrab,"", "", "0", LoginInfo.IdTrab, this.Name, 10); //6
            objTrab.GestionHuella(TrabajadorInfo.IdTrab, "", 3, LoginInfo.IdTrab, this.Name, 6);//5
            if (chkAdmin.Checked == true)
                objTrab.GestionIdentidad(TrabajadorInfo.IdTrab, "", "", "0", sUsuuMod, sPrguMod, 8);

            foreach (Reloj obj in ltReloj2)
            {
                
                iCont += 1;
                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Conectando con Dispositivo "+ obj.Descripcion + " "  + iCont + " de " + ltReloj2.Count);

                bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {
                    
                    string idtrab = lbIdTrab.Text;
                    string Nombre = lbNombre.Text;
                    objCZKEM.BeginBatchUpdate(1,1);
                    objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, "", 0, true);
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();

                    if (!BatchUpdate)
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.Descripcion);

                }
                else
                {
                    MessageBox.Show("No fue posible conectarse al reloj : " + obj.Descripcion + " por favor hable al area de sistemas", "SIPAA"); 
                  
                }
                    
                    
               



                bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {


                    string idtrab = lbIdTrab.Text;
                    objCZKEM.BeginBatchUpdate(1, 1);
                    objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo);
                    bool BatchUpdate = objCZKEM.BatchUpdate(1);
                    objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();
                    if (!BatchUpdate)
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.Descripcion);

                }
                else
                {
                    
                    MessageBox.Show("No fue posible conectarse al reloj : " + obj.Descripcion + " por favor hable al area de sistemas", "SIPAA");
                }


            }
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Datos guardados en el reloj, capture los biométricos del empleado ");
            System.Threading.Thread.Sleep(3000);



            timer1.Start();
            AsignarReloj(TrabajadorInfo.IdTrab);
          
            ltReloj.Clear();

        }


        public void SincronizaBiometricos(List<Reloj> ltReloj2, RelojChecador objReloj, Panel Pnl, Label Lbl)
        {
            int iCont = 0; 
            bool bConexion = false;
            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dt = new DataTable(); 
            foreach (Reloj obj in ltReloj2)
            {

                iCont += 1;
                ControlNotificaciones(Pnl, Lbl, 2, "Conectando con Dispositivo " + obj.Descripcion + " " + iCont + " de " + ltReloj2.Count);
                bConexion = Connect_Net(obj.IpReloj, 4370);
                if (bConexion != false)
                {

                    string Nombre, Password;
                    Nombre = Password = string.Empty;
                    objCZKEM.BeginBatchUpdate(1, 1);
                    dt = objReloj.RelojesxTrabajador(lbIdTrab.Text, obj.cvReloj, 17, "%", "%");
                    ControlNotificaciones(Pnl, Lbl, 2, "Insertando huellas.. ");
                    foreach (DataRow row in dt.Rows)
                    {
                        string idtrab = row["idtrab"].ToString();
                        string cvreloj = row[1].ToString();
                        Nombre = row["Nombre"].ToString();
                        int Permiso = 0;
                        string pass_desc = string.Empty;

                        if (!string.IsNullOrEmpty(row["pass"].ToString()))
                            pass_desc = Utilerias.descifrar(row["pass"].ToString());
                        if (Convert.ToBoolean(row["administrador"].ToString()))
                            Permiso = 3;

                        if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true))
                        {
                            if (row["huellaTmp"].ToString() != String.Empty)
                                objCZKEM.SetUserTmpExStr(1, idtrab, Convert.ToInt32(row["indicehuella"].ToString()), 1, row["huellaTmp"].ToString());

                        }



                        if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, pass_desc, Permiso, true))
                        {
                            if (row["huellaTmp"].ToString() != String.Empty)
                            {
                                int ifinger = Convert.ToInt32(row["indicehuella"].ToString());
                                string tmpHuella = "";

                                if (ifinger >= 0 && ifinger <= 9)
                                {
                                    tmpHuella = row["huellaTmp"].ToString();
                                    objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);

                                }
                            }


                        }

                    }


                    objCZKEM.BatchUpdate(1);
                    objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();
                }
                else
                {
                    ControlNotificaciones(Pnl, Lbl, 3, "No fue posible conectarse al reloj: " + obj.Descripcion);
                    continue;
                }



                bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                if (bConexion)
                {
                    objCZKEM.BeginBatchUpdate(1, 1);
                    dt = objReloj.RelojesxTrabajador(lbIdTrab.Text, obj.cvReloj, 18, "%", "%");
                    ControlNotificaciones(Pnl, Lbl, 2, "Insertando grupos... ");
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
                            catch { }


                        }


                    }

                    objCZKEM.BatchUpdate(1);
                    objCZKEM.RefreshData(1);
                    objCZKEM.Disconnect();
                }
                else
                {
                    ControlNotificaciones(Pnl, Lbl, 3, "No fue posible conectarse al reloj: " + obj.Descripcion);
                    continue;
                }

                faces = new LinkedList<FaceTmp>();
                 dt = objReloj.RelojesxTrabajador(lbIdTrab.Text, obj.cvReloj, 19, "%", "%");
                    foreach (DataRow row in dt.Rows)
                        {
                            string idtrab = row["idtrab"].ToString();

                            if (row["rostroTmp"].ToString() != String.Empty)
                            {
                                string RostroTmp = row["rostroTmp"].ToString();
                                int rostrolong = Convert.ToInt32(row["rostrolong"].ToString());
                                faces.AddLast(new FaceTmp(idtrab, 50, RostroTmp, rostrolong));
                                
                            }
                        }

                bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                if (bConexion)
                {
                    //Reiniciando dispositivo...
                    objCZKEM.RestartDevice(1);
                    ControlNotificaciones(Pnl, Lbl, 2, "Espere un momento por favor");
                    System.Threading.Thread.Sleep(60000);
                }

                bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                if (bConexion)
                {
                    ControlNotificaciones(Pnl, Lbl, 2, "Insertando rostro...");
                    foreach (FaceTmp ft in faces)
                    {
                        objCZKEM.SetUserFaceStr(1, ft.idtrab, ft.index, ft.rostroTmp, ft.rostrolong);
                    }

                }

                objCZKEM.BatchUpdate(1);
                objCZKEM.RefreshData(1);
                objCZKEM.Disconnect();

                ControlNotificaciones(Pnl, Lbl, 1, "Biométricos guardados correctamente en el reloj: "+obj.Descripcion+  " " + iCont + " de " + ltReloj2.Count);
                System.Threading.Thread.Sleep(1000);
               }
            
        }

        public void ProcesoReloj(string Opcion, Reloj obj)
        {
           
            RelojChecador objReloj = new RelojChecador();
            
            bool bConexion = Connect_Net(obj.IpReloj, 4370);
            if (bConexion != false)
            {
                objCZKEM.ReadAllUserID(1);
                objCZKEM.ReadAllTemplate(1);
                bool bBandera = false;
               
                string idTrab = lbIdTrab.Text;
                string cvreloj = obj.cvReloj.ToString();
                bBandera = ConsultaReloj(Opcion, idTrab);
                //}
                objCZKEM.Disconnect();

                if (bBandera)
                    objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);
                
            }
            else
            {
                MessageBox.Show("No fue posible conectarse al reloj: " + obj.Descripcion, "SIPAA", MessageBoxButtons.OK);
                System.Threading.Thread.Sleep(1000);

            }
            

        }
        public bool ConsultaReloj(string Opcion, string idtrab)
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
           
                case "Huella":

                    int flag = 0;
                    string huellatmp = "";
                    int tpmlong = 0;
                    string sIdTrab = lbIdTrab.Text;
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
          
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void lbIdTrab_Click(object sender, EventArgs e)
        {

        }

        private void tabAsignacion_Selecting(object sender, TabControlCancelEventArgs e)
        {
         
        }

        private void dgGrupos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdmin.Checked == true)
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

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Comienza proceso");
            relojseleccionados();
           // System.Threading.Thread.Sleep(50);
            ProcesoReloj();
            AsignarReloj(TrabajadorInfo.IdTrab);
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Los Biométricos se obtuvieron con exito");
            System.Threading.Thread.Sleep(3000);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Comienza proceso");
            RelojChecador objReloj = new RelojChecador();
            SonaTrabajador objTrab = new SonaTrabajador();


            relojseleccionados();
            foreach (Reloj obj in ltReloj2)
            {
                objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 1, sUsuuMod, Name);

            }
            
            SincronizaBiometricos(ltReloj2, objReloj, panelTagRelojCheck, lbMensajeRelojCheck);
            AsignarReloj(TrabajadorInfo.IdTrab);
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Proceso Terminado" );
            System.Threading.Thread.Sleep(1000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Reenviando Datos, aguarde ");
           // System.Threading.Thread.Sleep(1000);
            try
            {

                relojseleccionados();

                if (ltReloj2.Count > 0)

                {
                    int iCont = 0;
                    bool bConexion = false;
                    foreach (Reloj obj in ltReloj2)
                    {

                        iCont += 1;
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Conectando con Dispositivo " + obj.Descripcion + " " + iCont + " de " + ltReloj2.Count);

                        bConexion = Connect_Net(obj.IpReloj, 4370);
                        if (bConexion != false)
                        {

                            string idtrab = lbIdTrab.Text;
                            string Nombre = lbNombre.Text;
                            objCZKEM.BeginBatchUpdate(1, 1);
                            objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, "", 0, true);
                            bool BatchUpdate = objCZKEM.BatchUpdate(1);
                            objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();

                            if (!BatchUpdate)
                                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.Descripcion);

                        }
                        else
                        {
                            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.Descripcion);
                            System.Threading.Thread.Sleep(1000);
                        }






                        bConexion = Connect_Net(obj.IpReloj, 4370);
                        if (bConexion != false)
                        {   string idtrab = lbIdTrab.Text;
                            objCZKEM.BeginBatchUpdate(1, 1);
                            objCZKEM.SetUserGroup(1, Convert.ToInt32(idtrab), Grupo);
                            bool BatchUpdate = objCZKEM.BatchUpdate(1);
                            objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();
                            if (!BatchUpdate)
                                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Error en el BatchUpdate del reloj " + obj.Descripcion);

                        }
                        else
                        {
                            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No fue posible conectarse a la IP: " + obj.Descripcion);
                            System.Threading.Thread.Sleep(1000);
                        }


                    }
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Datos guardados en el reloj, capture los biométricos del empleado ");
                    System.Threading.Thread.Sleep(3000);
                }
                else
                {
                    ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "No ha seleccionado ningún reloj.");
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

        //////////////


        public void ProcesoReloj()
        {
           
            ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Obteniendo los biométricos.");

            if (ltReloj2.Count > 0)
            {

               
                foreach (Reloj obj in ltReloj2)
                {

                    RelojChecador objReloj = new RelojChecador();
                    DataTable dt = objReloj.RelojesxTrabajador(lbIdTrab.Text, obj.cvReloj, 11, "%", "%");

                    int iCont = 0; 
                    bool bConexion = Connect_Net(obj.IpReloj, 4370);
                    if (bConexion != false)
                    {

                        iCont += 1;
                        ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 2, "Conectando con Dispositivo " + obj.Descripcion + " " + iCont + " de " + ltReloj2.Count);
                        objCZKEM.ReadAllUserID(1);
                        objCZKEM.ReadAllTemplate(1);
                       

                        int iContTrab = 0;
                       
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                            //string cvreloj = row[1].ToString();
                            ConsultaReloj("Pass", idTrab, iContTrab, dt.Rows.Count);
                          
                        }
                        iContTrab = 0;
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                           // string cvreloj = row[1].ToString();
                           ConsultaReloj("Huella", idTrab, iContTrab, dt.Rows.Count);
                           
                        }
                        iContTrab = 0;
                       
                        foreach (DataRow row in dt.Rows)
                        {
                            iContTrab += 1;
                            string idTrab = row["idTrab"].ToString();
                            //string cvreloj = row[1].ToString();
                           ConsultaReloj("Face", idTrab, iContTrab, dt.Rows.Count);
                           
                        }


                       
                        objCZKEM.Disconnect();
                       

                    }
                    else
                   
                        MessageBox.Show("No fue posible conectarse al reloj, contacte al personal del área de sistemas", "SIPAA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                   
                }
             
         
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado algún Registro.");
            }


        }
        ///    ****************
        ///    
        ///  *************************

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
                  
                    while(objCZKEM.SSR_GetAllUserInfo(1, out sIdTrab, out sNombre, out sPass, out iPrivilegio, out bActivo))
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

        private void mtxtTiempoTrabajo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            relojseleccionados();
            DialogResult result = MessageBox.Show("¿Seguro que desea eliminar "+ltReloj2.Count+" relojes asignados?", "SIPAA", MessageBoxButtons.YesNo);
            if (result==DialogResult.Yes)
            {
                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 3, "Eliminando Asignaciones");
                RelojChecador objReloj = new RelojChecador();
                SonaTrabajador objTrab = new SonaTrabajador();
               // relojseleccionados();
                foreach (Reloj obj in ltReloj2)
                {
                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 24, sUsuuMod, Name);

                }
                llenarGridReloj("%");
                ltRelojxUsuario.Clear(); 
                AsignarReloj(TrabajadorInfo.IdTrab);
                ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Asignaciones Eliminadas");


            }
           
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
           
          
            for (int i = 0; i < dgvElimina.RowCount; i++)
            {
                if (dgvElimina.Rows[i].Cells[0].Tag.ToString().Equals("check"))
                {
                    DataGridViewRow row = dgvElimina.Rows[i];
                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                    objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                    objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                    objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                    ltElimina.Add(objR);
                }
            }

            if(ltElimina.Count>0)
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea eliminar " + ltElimina.Count + " relojes asignados?", "SIPAA", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ControlNotificaciones(pnlEliminaAsignaciones, lblEliminaAsignaciones, 3, "Eliminando Asignaciones");
                    RelojChecador objReloj = new RelojChecador();
                                  
                    foreach (Reloj obj in ltElimina)
                    {
                        objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 24, sUsuuMod, Name);

                    }
                   
                   
                    dgvElimina.ClearSelection();
                    AsignaParaEliminar(dgvElimina);
                    ControlNotificaciones(pnlEliminaAsignaciones, lblEliminaAsignaciones, 1, "Asignaciones Eliminadas");
                    DialogResult result2 = MessageBox.Show("Relojes Eliminados", "SIPAA", MessageBoxButtons.OK);
                    if (result2== DialogResult.OK)
                    {
                        AsignacionTrabajadorPerfil form = new AsignacionTrabajadorPerfil();
                        form.Show();
                        Close();
                    }


                }
            }
            else
                ControlNotificaciones(pnlEliminaAsignaciones, lblEliminaAsignaciones, 3, "Seleccione al menos un reloj para eliminar");

           


        }

        private void dgvElimina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {
                
                    iOpcionAdmin = 1;
                    Utilerias.MultiSeleccionGridView(dgvElimina, 1, ltReloj, PanelReloj);
                    DataGridViewRow row = dgvElimina.SelectedRows[0];
                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.Descripcion = row.Cells["Descripción"].Value.ToString();
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                    objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                    objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                    objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);

              }
            }

        private void dgvAgrega_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {


                MultiSeleccionGridView(dgvAgrega, 1, ltReloj, PanelReloj);
                DataGridViewRow row = dgvAgrega.SelectedRows[0];
                Reloj objR = new Reloj();
                objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                objR.Descripcion = row.Cells["Descripción"].Value.ToString();
                objR.IpReloj = row.Cells["IP"].Value.ToString();
                objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);

            }
        }

        private void btnAgrega_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvAgrega.RowCount; i++)
            {
                if (dgvAgrega.Rows[i].Cells[0].Tag.ToString().Equals("check"))
                {
                    DataGridViewRow row = dgvAgrega.Rows[i];
                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                    objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                    objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                    objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                    ltAgrega.Add(objR);
                }
            }

            if (ltAgrega.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea asignar " + ltAgrega.Count + " relojes?", "SIPAA", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ControlNotificaciones(pnlAgregaRelojes, lblAgregaRelojes, 2, "Comienza Proceso");

                    RelojChecador objReloj = new RelojChecador();

                    foreach (Reloj obj in ltAgrega)
                    {
                        objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 1, sUsuuMod, Name);

                    }

                    SincronizaBiometricos(ltAgrega, objReloj, pnlAgregaRelojes, lblAgregaRelojes);
                    ControlNotificaciones(pnlAgregaRelojes, lblAgregaRelojes, 1, "Relojes Agregados Correctamente");
                    DialogResult result2 = MessageBox.Show("Relojes Agregados Correctamente ", "SIPAA", MessageBoxButtons.OK);
                    if (result2==DialogResult.OK)
                    {
                        AsignacionTrabajadorPerfil form = new AsignacionTrabajadorPerfil();
                        form.Show();
                        Close();
                    }


                }
            }
            else
                ControlNotificaciones(pnlEliminaAsignaciones, lblEliminaAsignaciones, 3, "Seleccione al menos un reloj para Agregar");

          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString()=="Si")
               dpFechaInicio.Visible = dpFechaFin.Visible = lbFechaInicio.Visible = lbFechaFin.Visible = true; 
            else
                dpFechaInicio.Visible = dpFechaFin.Visible = lbFechaInicio.Visible = lbFechaFin.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvCambiaAsociacion.RowCount; i++)
            {
                if (dgvCambiaAsociacion.Rows[i].Cells[0].Tag.ToString().Equals("check"))
                {
                    DataGridViewRow row = dgvCambiaAsociacion.Rows[i];
                    Reloj objR = new Reloj();
                    objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                    objR.IpReloj = row.Cells["IP"].Value.ToString();
                    objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                    objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                    objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                    objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);
                    ltCambiaAsociacion.Add(objR);
                }
            }

            if (ltCambiaAsociacion.Count > 0)
            {
                DateTime FechaVacia = new DateTime();

                RelojChecador objReloj = new RelojChecador();
                if (comboBox1.SelectedItem.ToString() == "Si")
                    objReloj.CambiaAsociacion(TrabajadorInfo.IdTrab, cbEmpleadosInactivos.SelectedValue.ToString(), 0, 27, dpFechaInicio.Value, dpFechaFin.Value, sUsuuMod, Name);
                else
                    objReloj.CambiaAsociacion(TrabajadorInfo.IdTrab, cbEmpleadosInactivos.SelectedValue.ToString(), 0, 27, FechaVacia, FechaVacia, sUsuuMod, Name);


                DialogResult result = MessageBox.Show("¿Desea enviar los biométricos del empleado a los nuevos relojes seleccionados ", "SIPAA", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ControlNotificaciones(pnlCambia, lblCambia, 2, "Comienza Proceso");
                    
                    foreach (Reloj obj in ltCambiaAsociacion)
                    {
                        objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, obj.cvReloj, 1, sUsuuMod, Name);

                    }
                    
                    foreach (Reloj obj in ltCambiaAsociacion)
                    {
                        #region InsertaBiometricos
                        int iCont = 0;

                        DataTable dt = objReloj.RelojesxTrabajador("%", obj.cvReloj, 17, "%", "%");
                        ControlNotificaciones(pnlCambia, lblCambia, 2, "Conectando con Dispositivo " + iCont + " de " + ltCambiaAsociacion.Count);
                        bool bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                        if (bConexion)
                        {
                            bool ClearData = objCZKEM.ClearData(1, 5);
                            bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);

                            #region InsertaHuellas


                            ControlNotificaciones(pnlCambia, lblCambia, 2, "Insertando huellas.. ");

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

                                            objCZKEM.SetUserTmpExStr(1, idtrab, ifinger, 1, tmpHuella);

                                        }
                                    }


                                }
                            }

                            #endregion
                            bool BatchUpdate = objCZKEM.BatchUpdate(1);
                            bool RefreshData = objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();

                        }
                        else
                            ControlNotificaciones(pnlCambia, lblCambia, 3, "No fue posible conectarse al reloj : " + obj.Descripcion);



                        #region InsertaGrupos
                        bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);

                        if (bConexion)
                        {
                            bool BeginBatchUpdate = objCZKEM.BeginBatchUpdate(1, 1);
                            ControlNotificaciones(pnlCambia, lblCambia, 2, "Insertando grupos..");
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
                                }
                            }

                            objCZKEM.BatchUpdate(1);
                            objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();
                        }

                        else
                            ControlNotificaciones(pnlCambia, lblCambia, 3, "No fue posible conectarse al reloj : " + obj.Descripcion);

                        #endregion


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
                            }
                        }


                        bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                        if (bConexion)
                        {

                            objCZKEM.RestartDevice(1);
                            ControlNotificaciones(pnlCambia, lblCambia, 2, "Espere un momento por favor");
                            System.Threading.Thread.Sleep(60000);
                        }

                        bConexion = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                        if (bConexion)
                        {
                            ControlNotificaciones(pnlCambia, lblCambia, 2, "Insertando rostros...");
                            foreach (FaceTmp ft in faces)
                            {
                                objCZKEM.SetUserFaceStr(1, ft.idtrab, ft.index, ft.rostroTmp, ft.rostrolong);
                            }
                            objCZKEM.BatchUpdate(1);
                            objCZKEM.RefreshData(1);
                            objCZKEM.Disconnect();
                        }
                        ControlNotificaciones(pnlCambia, lblCambia, 2, "Proceso finalizado para el reloj: " + obj.Descripcion);
                        objReloj.obtrelojeschecadores(8, obj.cvReloj, "", "", "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);

#endregion
                    }


                    ControlNotificaciones(pnlCambia, lblCambia, 1, "Proceso terminado correctamente");
                    DialogResult result2 = MessageBox.Show("Proceso terminado correctamente ", "SIPAA", MessageBoxButtons.OK);
                    if (result2 == DialogResult.OK)
                    {
                        AsignacionTrabajadorPerfil form = new AsignacionTrabajadorPerfil();
                        form.Show();
                        Close();
                    }
                    
                }
                
                }

                else
                ControlNotificaciones(pnlCambia, lblCambia, 3, "No le ha asignado ningún reloj al empleado.");
               
           
        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void dgvCambiaAsociacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {

                //iOpcionAdmin = 1;
                MultiSeleccionGridView(dgvCambiaAsociacion, 1, ltReloj, PanelReloj);
                DataGridViewRow row = dgvCambiaAsociacion.SelectedRows[0];
                Reloj objR = new Reloj();
                objR.cvReloj = Convert.ToInt32(row.Cells["Clave"].Value.ToString());
                objR.Descripcion = row.Cells["Descripción"].Value.ToString();
                objR.IpReloj = row.Cells["IP"].Value.ToString();
                objR.Teclado = Convert.ToBoolean(row.Cells["Teclado"].Value);
                objR.Huella = Convert.ToBoolean(row.Cells["Huella"].Value);
                objR.Rostro = Convert.ToBoolean(row.Cells["Rostro"].Value);
                objR.MultipleHuella = Convert.ToBoolean(row.Cells["multiplehuella"].Value);

            }
        }
    }
    }
//}
