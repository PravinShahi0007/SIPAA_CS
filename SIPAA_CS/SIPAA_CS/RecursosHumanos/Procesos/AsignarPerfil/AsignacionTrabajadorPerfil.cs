
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

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class AsignacionTrabajadorPerfil : Form, DPFP.Capture.EventHandler
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public int iOpcionAdmin;
        public List<int> ltFormasReg = new List<int>();
        public List<string> ltFormasxUsuario = new List<string>();
        public List<int> ltReloj = new List<int>();
        public List<string> ltRelojxUsuario = new List<string>();
        public string sUsuuMod = LoginInfo.IdTrab;


        //Huella Digital
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        private DPFP.Capture.Capture Capturer;
        public Bitmap bitmap;
        public DPFP.Processing.Enrollment Enroller;
        public event OnTemplateEventHandler OnTemplate;
        //private AppData Data;
        private DPFP.Gui.Enrollment.EnrollmentControl EnrollmentControl;

        public AsignacionTrabajadorPerfil()
        {
            InitializeComponent();
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
            cbDiaSalida.SelectedIndex = cbDiaEntrada.SelectedIndex;
        }

        private void cbDiaSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                else{
                    btnGuardar.Visible = false;
                }

              
                DataGridViewRow row = this.dgvHorario.SelectedRows[0];

                string sDiaEntrada = row.Cells[2].Value.ToString();
                //string Desc = row.Cells["DESCRIPCION"].Value.ToString();
                TrabajadorHorario objHorario = RecuperarGrid(row);

                mtxtEntradaTurno.Text = objHorario.sHoraEntrada;
                mtxtSalida.Text = objHorario.sHoraSalidaTurno;
                mtxtComidaInicio.Text = objHorario.sHoraComidaInicio;
                mtxtComidaFin.Text = objHorario.sHoraComidaFin;
                mtxtTiempoComida.Text = objHorario.iTiempoComida.ToString();
                mtxtTiempoTrabajo.Text = objHorario.iHorasTotalTrabajo.ToString();

                cbDiaEntrada.SelectedValue = objHorario.iCvDia;
                cbDiaSalida.SelectedValue = objHorario.iCvdiaSalidaTurno;
                cbDiaEntrada.SelectedValue = objHorario.iCvdiaComidaInicio;
                cbDiaEntrada.SelectedValue = objHorario.iCvdiaComidaFin;

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                // TrabajadorInfo.IdTrab = row.Cells["idtrab"].Value.ToString();
               
            }
        }


        private void dgvForReg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {

                if (dgvForReg.SelectedRows.Count != 0)
                {
                    Utilerias.MultiSeleccionGridView(dgvForReg, 1, ltFormasReg, panelPermisos);



                }
            }
        }


        private void dgvReloj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {
                if (dgvReloj.SelectedRows.Count != 0)
                {
                    iOpcionAdmin = 1;
                    Utilerias.MultiSeleccionGridView(dgvReloj, 1, ltReloj, PanelReloj);
                }
            }
        }





        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnRegresar_Click(object sender, EventArgs e)
        {
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
            ckbEliminar.Visible = false;
            TrabajadorHorario objHorario = AsignarObjeto();
            iOpcionAdmin = 1;
            objHorario.iCvPlantilla = Convert.ToInt32(cbPlantilla.SelectedValue.ToString());
            panelEditar.Visible = false;
            try
            {
                DataTable dtResponse = objHorario.GestionHorario(iOpcionAdmin, objHorario);
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
            bool bBandera = CamposVacios(panelEditar);

            if (bBandera != true)
            {
                cbDiaEntrada.Enabled = true;
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

                TimeSpan tsEntrada = TimeSpan.Parse(objHorario.sHoraEntrada);
                TimeSpan tsSalida = TimeSpan.Parse(objHorario.sHoraSalidaTurno);
                TimeSpan horasTrabajo = new TimeSpan();
                if (tsSalida > tsEntrada)
                {
                    horasTrabajo = tsSalida - tsEntrada;
                }
                else
                {
                    horasTrabajo = tsEntrada - tsSalida;
                }

                TimeSpan tsComidaInicio = TimeSpan.Parse(objHorario.sHoraComidaInicio);
                TimeSpan tsComidaFin = TimeSpan.Parse(objHorario.sHoraComidaFin);
                TimeSpan MinComida = tsComidaFin - tsComidaInicio;
                string MinutosComida = (60 * MinComida.Hours).ToString();

                if (horasTrabajo.Hours.ToString() != mtxtTiempoTrabajo.Text)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El número total de horas no concuerda con la hora de Entrada y Salida.");
                    timer1.Start();
                }
                else if (tsComidaInicio < tsEntrada || tsComidaFin < tsEntrada
                   || tsComidaInicio > tsSalida || tsComidaFin > tsSalida)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El horario de Comida debe ser entre la hora de Entrada y Salida.");
                    timer1.Start();

                }
                //else if (MinutosComida != mtxtTiempoComida.Text)
                //{

                //    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El número total de tiempo de Comida no concuerda con la hora de Inicio y Fin.");
                //    timer1.Start();

                //}
                else
                {
                    try
                    {
                        DataTable dtresponse = objHorario.GestionHorario(iOpcionAdmin, objHorario);

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
                                break;
                            case "ACTUALIZAR":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Cambio Guardado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
                                break;
                            case "ACTUALIZA_NUEVO":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Guardado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
                                break;
                            case "ELIMINAR":
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Eliminado con Exito.");
                                timer1.Start();
                                LimpiarFormulario();
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
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Todos los Campos son Obligatorios. No pueden quedar vacios");
                timer1.Start();
            }

        }





        private bool CamposVacios(Panel pnl)
        {
            bool bBandera = false;
            foreach (Control ctrl in pnl.Controls)
            {
                string sTipoCtrl = ctrl.AccessibilityObject.ToString();
                if (sTipoCtrl.Contains("TextBox"))
                {
                    MaskedTextBox txt = (MaskedTextBox)ctrl;
                    if (txt.Text.Trim() == String.Empty || txt.Text.Trim() == ":")
                    {
                        bBandera = true;
                    }
                }
                else if (sTipoCtrl.Contains("ComboBox"))
                {
                    ComboBox cb = (ComboBox)ctrl;

                    if (cb.SelectedIndex == 0)
                    {

                        bBandera = true;
                    }
                }

            }

            return bBandera;

        }


        private void btnBuscarForReg_Click(object sender, EventArgs e)
        {
            string sBusqueda = "";
            if (txtBuscarForReg.Text != String.Empty)
            {
                sBusqueda = txtBuscarForReg.Text;
            }
            else
            {
                sBusqueda = "%";
            }

            LlenarGridFormasRegistro(sBusqueda);
            AsignarFormas(TrabajadorInfo.IdTrab);
            panelPermisos.Enabled = false;
            ltFormasReg.Clear();
        }

        private void btnGuardarForReg_Click(object sender, EventArgs e)
        {

            try
            {
                string UsuuMod = "vjiturburuv";
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
                    {
                        CrearAsignaciones_FormasRegistro(UsuuMod, PrguMod);
                    }
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
                Utilerias.ControlNotificaciones(panelTagForReg, lbMensajeForReg, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                AsignarFormas(TrabajadorInfo.IdTrab);
                panelPermisos.Enabled = false;
                ltFormasReg.Clear();
            }
        }

        private void btnBuscarReloj_Click(object sender, EventArgs e)
        {

            string sBusqueda = "";
            if (txtBuscarReloj.Text != String.Empty)
            {
                sBusqueda = txtBuscarReloj.Text;
            }
            else
            {
                sBusqueda = "%";
            }

            llenarGridReloj(sBusqueda);
            AsignarReloj(TrabajadorInfo.IdTrab);
            PanelReloj.Enabled = false;
            ltReloj.Clear();

        }

        private void btnGuardarReloj_Click(object sender, EventArgs e)
        {
            try
            {
                string UsuuMod = "vjiturburuv";
                string PrguMod = this.Name;
                FormaReg objFr = new FormaReg();
                llenarGridReloj("%");
                AsignarReloj(TrabajadorInfo.IdTrab);

                if (Utilerias.SinAsignaciones(dgvReloj, 0, 1, ltReloj) == true)
                {
                    CrearAsignaciones_Reloj(UsuuMod, PrguMod, iOpcionAdmin);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Seguro que desea quitar todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        CrearAsignaciones_Reloj(UsuuMod, PrguMod, iOpcionAdmin);
                    }
                    else
                    {
                        AsignarReloj(TrabajadorInfo.IdTrab);
                        PanelReloj.Enabled = false;
                        ltReloj.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                Utilerias.ControlNotificaciones(panelTagForReg, lbMensajeForReg, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                AsignarReloj(TrabajadorInfo.IdTrab);
                PanelReloj.Enabled = false;
                ltReloj.Clear();
            }
        }

     
        private void btnAgregarHuella_Click(object sender, EventArgs e)
        {
            PanelGuardarHuella.Visible = false;
            prgbar.Visible = true;
            prgbar.Style = ProgressBarStyle.Marquee;
            Utilerias.AsignarBotonResize(btnGuardarHuella, PantallaSistema(), "Guardar");
                   
                    Enroller = new DPFP.Processing.Enrollment();            // Create an enrollment.
                    lbHuella.Text =  String.Format("Ingresos Necesarios: {0}", Enroller.FeaturesNeeded);
                    FormInit();
                    Start();

        }

        private void btnGuardarHuella_Click(object sender, EventArgs e)
        {
            try
            {

                switch (iOpcionAdmin) {

                    case 1:
                        Image img = bitmap;
                        ImageConverter converter = new ImageConverter();
                        byte[] arrImagen = (byte[])converter.ConvertTo(img, typeof(byte[]));

                        SonaTrabajador objTrab = new SonaTrabajador();
                        DataTable dtTrab = objTrab.GestionHuella(Enroller.Template.Bytes, arrImagen, TrabajadorInfo.IdTrab, sUsuuMod, this.Name, 3);
                        dtTrab = objTrab.GestionHuella(Enroller.Template.Bytes, arrImagen, TrabajadorInfo.IdTrab, sUsuuMod, this.Name, 1);

                        if (dtTrab.Columns.Contains("INSERT"))
                        {
                            Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 1, "Huella Asignada Correctamente");
                            Enroller.Clear();
                        }

                        break;

                    case 3:
                        SonaTrabajador obj = new SonaTrabajador();
                        dtTrab = obj.GestionHuella(Enroller.Template.Bytes, new byte[] { }, TrabajadorInfo.IdTrab, sUsuuMod, this.Name, 3);
                        if (dtTrab != null)
                        {
                            Utilerias.ControlNotificaciones(panelTagHuella, lbMensajeHuella, 1, "Huella Eliminada Correctamente");
                            Enroller.Clear();
                        }
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

        private void mtxtComidaFin_Leave(object sender, EventArgs e)
        {
            string s1 = mtxtComidaInicio.Text + "00";
            string s2 = mtxtComidaFin.Text + "00";

            TimeSpan ts = new TimeSpan();
            TimeSpan tsComidaInicio = new TimeSpan();
            TimeSpan tsComidaFin = new TimeSpan();
            TimeSpan tsMinutos = new TimeSpan();
            if (TimeSpan.TryParse(mtxtComidaInicio.Text, out ts) && TimeSpan.TryParse(mtxtComidaFin.Text, out ts))
            {
                tsComidaInicio = TimeSpan.Parse(mtxtComidaInicio.Text);
                tsComidaFin = TimeSpan.Parse(mtxtComidaFin.Text);
            }
            else
            {
                string sComidaInicio = Utilerias.ValidarHoras(s1);
                string sComidaFin = Utilerias.ValidarHoras(s2);

                mtxtComidaInicio.Text = sComidaInicio;
                mtxtComidaFin.Text = sComidaFin;

                tsComidaInicio = TimeSpan.Parse(sComidaInicio);
                tsComidaFin = TimeSpan.Parse(sComidaFin);

            }

            tsMinutos = tsComidaFin - tsComidaInicio;


            mtxtTiempoComida.Text = (60 * (tsMinutos.Hours)).ToString();
        }

        private void mtxtSalida_Leave(object sender, EventArgs e)
        {

            string s1 = mtxtSalida.Text + "00";
            string s2 = mtxtEntradaTurno.Text + "00";


            TimeSpan ts = new TimeSpan();
            TimeSpan tsEntrada = new TimeSpan();
            TimeSpan tsSalida = new TimeSpan();
            if (TimeSpan.TryParse(mtxtSalida.Text, out ts) && TimeSpan.TryParse(mtxtEntradaTurno.Text, out ts))
            {
                tsEntrada = TimeSpan.Parse(mtxtEntradaTurno.Text);
                tsSalida = TimeSpan.Parse(mtxtSalida.Text);

            }
            else
            {
                string sEntrada = Utilerias.ValidarHoras(mtxtEntradaTurno.Text);
                string sSalida = Utilerias.ValidarHoras(mtxtSalida.Text);

                mtxtEntradaTurno.Text = sEntrada;
                mtxtSalida.Text = sSalida;

                tsEntrada = TimeSpan.Parse(sEntrada);
                tsSalida = TimeSpan.Parse(sSalida);

            }

            if (tsEntrada.ToString() != "00:00" && tsSalida.ToString() != "00:00")
            {

                if (tsEntrada > tsSalida)
                {
                    int diasdif = (cbDiaEntrada.SelectedIndex + 1) - (cbDiaSalida.SelectedIndex);
                    TimeSpan horasTrabajo = tsEntrada - tsSalida;
                    mtxtTiempoTrabajo.Text = ((12 * diasdif) + horasTrabajo.Hours).ToString();
                    //  cbDiaSalida.SelectedValue = Convert.ToInt32(cbDiaEntrada.SelectedValue) + 1;
                }
                else
                {
                    TimeSpan horasTrabajo = tsSalida - tsEntrada;
                    mtxtTiempoTrabajo.Text = horasTrabajo.Hours.ToString();
                    cbDiaSalida.SelectedValue = cbDiaEntrada.SelectedValue;
                }

            }

        }

        private void mtxtComidaInicio_Leave(object sender, EventArgs e)
        {
            string s1 = mtxtComidaInicio.Text + "00";
            string s2 = mtxtComidaFin.Text + "00";

            TimeSpan ts = new TimeSpan();
            TimeSpan tsComidaInicio = new TimeSpan();
            TimeSpan tsComidaFin = new TimeSpan();
            TimeSpan tsMinutos = new TimeSpan();
            if (TimeSpan.TryParse(mtxtComidaInicio.Text, out ts) && TimeSpan.TryParse(mtxtComidaFin.Text, out ts))
            {
                tsComidaInicio = TimeSpan.Parse(mtxtComidaInicio.Text);
                tsComidaFin = TimeSpan.Parse(mtxtComidaFin.Text);


            }
            else
            {
                string sComidaInicio = Utilerias.ValidarHoras(s1);
                string sComidaFin = Utilerias.ValidarHoras(s2);

                mtxtComidaInicio.Text = sComidaInicio;
                mtxtComidaFin.Text = sComidaFin;

                tsComidaInicio = TimeSpan.Parse(sComidaInicio);
                tsComidaFin = TimeSpan.Parse(sComidaFin);

            }

            // Utilerias.ControlNotificaciones(panelTagForReg,lbMensajeForReg,2,"La fecha de Comida Inicio no puede ser Mayor a la de Fin");

            string[] horaInicio = mtxtComidaInicio.Text.Split(':');
            string sHoraInicio;
            string sDecenas = horaInicio[0].ElementAt(0).ToString();
            string sUnidad = horaInicio[0].ElementAt(1).ToString();

            if (Int32.Parse(sDecenas) < 2)
            {
                sHoraInicio = sDecenas + (Convert.ToInt32(sUnidad) + 1).ToString();
            }
            else
            {
                if (Int32.Parse(sUnidad) == 3)
                {
                    sHoraInicio = "00";
                }
                else
                {
                    sHoraInicio = sDecenas + (Convert.ToInt32(sUnidad + 1)).ToString();
                }
            }


            mtxtComidaFin.Text = sHoraInicio + ":" + horaInicio[1];
            tsComidaFin = TimeSpan.Parse(mtxtComidaFin.Text);
            //mtxtComidaInicio.Text = tsComidaFin.Hours.ToString() + ":" + (tsComidaFin.Minutes + 60).ToString();
            tsMinutos = tsComidaFin - tsComidaInicio;
            mtxtTiempoComida.Text = (60 * (tsMinutos.Hours)).ToString();
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
                    ltReloj.Clear();
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
                    DataTable dtTrab = objTrab.GestionHuella(byteArray, byteArray, TrabajadorInfo.IdTrab, "", "", 5);
                    try
                    {
                        foreach (DataRow row in dtTrab.Rows)
                        { byteArray = (byte[])row["imgtemplate"]; }
                        Image imgHuella = null;
                        MemoryStream ms = new MemoryStream(byteArray);
                        imgHuella = Image.FromStream(ms);
                        ms.Close();
                        pbHuella.Image = imgHuella;
                    }
                    catch (Exception ex) {

                        pbHuella.Image = null;
                    }
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
                //MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DataTable dtHorario = objHorario.GestionHorario(iOpcionAdmin, objHorario);
            dgvHorario.DataSource = dtHorario;


            Utilerias.AgregarCheck(dgvHorario, 0);
            dgvHorario.Columns[1].Visible = false;

        }


        private TrabajadorHorario AsignarObjeto()
        {

            TrabajadorHorario objHorario = new TrabajadorHorario();
            objHorario.sIdTrab = TrabajadorInfo.IdTrab;
            objHorario.iCvPlantilla = 0;
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
            objHorario.sUsuumod = "vjiturburuv";
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
            objHorario.iCvdiaSalidaTurno = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[4].Value.ToString()));
            objHorario.iCvdiaComidaInicio = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[7].Value.ToString()));
            objHorario.iCvdiaComidaFin = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[9].Value.ToString()));

            return objHorario;
        }

        public void LimpiarFormulario()
        {

            cbDiaEntrada.SelectedIndex = 0;
            cbDiaSalida.SelectedIndex = 0;

            mtxtEntradaTurno.Text = "00:00";
            //  mtxtEntradaTurno.SelectionLength = 5;
            mtxtSalida.Text = "00:00";
            //   mtxtSalida.SelectionLength = 5;
            mtxtComidaInicio.Text = "00:00";
            //    mtxtComidaInicio.SelectionLength = 5;
            mtxtComidaFin.Text = "00:00";
            //   mtxtComidaFin.SelectionLength = 5;
            mtxtTiempoComida.Text = "00:00";
            //    mtxtTiempoComida.SelectionLength = 3;
            mtxtTiempoTrabajo.Text = "00:00";
            //    mtxtTiempoTrabajo.SelectionLength = 3;


        }


        private void AsignarReloj(string sIdtrab)
        {
            RelojChecador objReloj = new RelojChecador();
            ltRelojxUsuario = objReloj.RelojesxTrabajador(sIdtrab, 0, 4, "", "");
            Utilerias.ImprimirAsignacionesGrid(dgvReloj, 0, 1, ltRelojxUsuario);
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


        private void llenarGridReloj(string sDescripcion)
        {
            if (dgvReloj.Columns.Count > 1)
            {
                dgvReloj.Columns.RemoveAt(0);
            }

            RelojChecador objReloj = new RelojChecador();
            DataTable dtRelojChecador = objReloj.obtrelojeschecadores(4, 0, sDescripcion, "", "", 0, "", "");
            dgvReloj.DataSource = dtRelojChecador;

            Utilerias.AgregarCheck(dgvReloj, 0);
            //dgvRelojesChecadores.Columns[0].Width = 75;
            //dgvRelojesChecadores.Columns[1].Width = 50;
            dgvReloj.Columns[1].Visible = false;
            dgvReloj.Columns[3].Visible = false;
            dgvReloj.Columns[4].Visible = false;
            dgvReloj.Columns[5].Visible = false;
            dgvReloj.Columns[0].Width = 65;


            dgvReloj.ClearSelection();


            //CheckBox ckbHeader = Utilerias.AgregarCheckboxHeader(dgvReloj, 0);
            // ckbHeader.CheckedChanged += Checkheader_CheckedChanged;
        }

        private void AsignarFormas(string sIdtrab)
        {

            FormaReg objfr = new FormaReg();
            ltFormasxUsuario = objfr.FormasxUsuario(sIdtrab, 0, 4, "", "");
            Utilerias.ImprimirAsignacionesGrid(dgvForReg, 0, 1, ltFormasxUsuario);

        }

        private void CrearAsignaciones_Reloj(string sUsuuMod, string sPrguMod, int iOpcion)
        {
            RelojChecador objReloj = new RelojChecador();

            switch (iOpcion)
            {

                case 5:
                    objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, 0, 3, sUsuuMod, sPrguMod);
                    break;
            }

            foreach (int cv in ltReloj)
            {
                objReloj.RelojesxTrabajador(TrabajadorInfo.IdTrab, cv, iOpcion, sUsuuMod, sPrguMod);
            }

            Utilerias.ControlNotificaciones(panelTagRelojCheck, lbMensajeRelojCheck, 1, "Asignaciones Guardadas Correctamente");
            timer1.Start();
            AsignarReloj(TrabajadorInfo.IdTrab);
            PanelReloj.Enabled = false;
            ltReloj.Clear();

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
    }
}
