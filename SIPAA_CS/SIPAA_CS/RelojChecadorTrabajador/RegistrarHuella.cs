using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Utilerias;
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;
using System.IO;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class RegistrarHuella : Form, DPFP.Capture.EventHandler
    {
        public RegistrarHuella()
        {
            InitializeComponent();
        }


        //Huella Digital
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        private DPFP.Capture.Capture Capturer;
        public Bitmap bitmap;
        public DPFP.Processing.Enrollment Enroller;
        public DPFP.Verification.Verification Verificator = new DPFP.Verification.Verification();

        private void RegistrarHuella_Load(object sender, EventArgs e)
        {
            tmHora.Start();

            try
            {
                prgBar.Visible = true;
                prgBar.Style = ProgressBarStyle.Marquee;
                pInfoTrab.Visible = false;
                Verificator = new DPFP.Verification.Verification();
                Enroller = new DPFP.Processing.Enrollment();            // Create an enrollment.                
                FormInit();
                Start();
                prgBar.Visible = true;
                prgBar.Style = ProgressBarStyle.Marquee;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Ingrese su Huella en el Lector.");
            }
            catch (Exception ex)
            {
              
            }

        }

        public void BuscarTrabajador(string sIdTrab)
        {           

                    prgBar.Visible = false;
                    Usuario objusuario = new Usuario();
                    objusuario = objusuario.ObtenerDatosUsuario(sIdTrab, 0, "", "", "", "", "", 7);

                    //Búsqueda en SIPPAA
                    if (objusuario.Nombre == null || objusuario.Nombre == String.Empty)
                    {
                        objusuario = objusuario.ObtenerListaTrabajadorUsuario(5, Int32.Parse(sIdTrab));

                        //Buscar SONARH
                        if (objusuario.Nombre == null || objusuario.Nombre == String.Empty)
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Huella NO Encontrada...");
                            pInfoTrab.Visible = false;
                            timer1.Start();
                        }
                        else
                        {
                            lbIdtrab.Text = sIdTrab.ToString();
                            lbNombre.Text = objusuario.Nombre;
                            pInfoTrab.Visible = true;
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Huella Verificada...");
                            timer1.Start();
                        }
                    }
                    else
                    {
                        lbIdtrab.Text = sIdTrab;
                        lbNombre.Text = objusuario.Nombre;
                        pInfoTrab.Visible = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Huella Verificada...");
                        timer1.Start();

                    }
                
              

        }

      

        //Eventos Huella Digital 

        public void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                   
                }
                catch(Exception ex)
                {
                    //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No es posible iniciar la Captura. Utilice Teclado ó Avise a Área de Sistemas.");
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
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No es posible iniciar la Captura");
                    prgBar.Visible = false;        
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


            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                string sIdTrabVerdadero = "0"; 
                SonaTrabajador objTrab = new SonaTrabajador();
               
                DataTable dtTrab = objTrab.GestionHuella(new byte[] { }, new byte[] { }, "", "", this.Name, 4);
                bool bBandera = false;

                foreach (DataRow row in dtTrab.Rows)
                {
                    try
                    {
                        string sIdTrab = "";
                        byte[] byteArray = new byte[] { };
                        byteArray = (byte[])row["template"];
                        sIdTrab = row["idTrab"].ToString();

                        Stream stream = new MemoryStream(byteArray);

                        DPFP.Template Tp = new DPFP.Template(stream);
                        Verificator = new DPFP.Verification.Verification();
                        // Compare the feature set with our template
                        DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                        Verificator.Verify(features, Tp, ref result);

                        if (result.Verified == true)
                        {
                            sIdTrabVerdadero = sIdTrab;
                            break;

                        }
                    }
                    catch(Exception e) {}
                }

                if (sIdTrabVerdadero != "0")
                {
                    Stop();
                    BuscarTrabajador(sIdTrabVerdadero);
                    timeReload.Start();
                }else
                {

                    prgBar.Visible = false;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Huella NO Encontrada...");
                    pInfoTrab.Visible = false;
                    timeReload.Start();

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
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No es posible iniciar la Captura");
                prgBar.Visible = false;
            }
            catch
            {
                //MessageBox.Show("Can't initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            //txtEstatus.AppendText("Huella CAPTURADA Correctamente." + "\r\n");
           // Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "INGRESE su Huella Nuevamente");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            //txtEstatus.AppendText("HUELLA REMOVIDA de Lector." + "\r\n");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            CheckForIllegalCrossThreadCalls = false;
           // txtEstatus.AppendText("El Lector ha sido TOCADO." + "\r\n");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            CheckForIllegalCrossThreadCalls = false;
            //txtEstatus.AppendText("El Lector CONECTADO." + "\r\n");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
           // txtEstatus.AppendText("El Lector ha sido DESCONECTADO." + "\r\n");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
            {
                //txtEstatus.AppendText("La calidad de la Huella es CORRECTA." + "\r\n");
            }
            else { }
              //  txtEstatus.AppendText("La calidad de la Huella es POBRE." + "\r\n");
        }
        #endregion



        private void timer1_Tick(object sender, EventArgs e)
        {
            //engine.Dispose();
            pbHuella.Image = null;
            panelTag.Visible = false;
            pInfoTrab.Visible = false;
            prgBar.Visible = false;
            // objScanner = new ResultadoHuella();
            RegistrarHuella_Load(sender, e);

            timer1.Stop();
        }

        private void tmHora_Tick(object sender, EventArgs e)
        {
            string sDia = Utilerias.ObtenerNombreDiaSemana(DateTime.Now.Date.DayOfWeek.ToString());
            lbDia.Text = sDia;
            string sFechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            lbFecha.Text = sFechaActual;
            string sSegundos = DateTime.Now.TimeOfDay.Seconds.ToString();
            if (sSegundos.Length == 1) { sSegundos = "0" + sSegundos; }
            lbHoraActual.Text = DateTime.Now.ToShortTimeString() + ":" + sSegundos;
            

        }

        private void timeReload_Tick(object sender, EventArgs e)
        {
            Capturer.Dispose();
            Capturer.StopCapture();
            pbHuella.Image = null;
            panelTag.Visible = false;
            pInfoTrab.Visible = false;
            bitmap = null;
            Stop();
            Enroller.Clear();
            RegistrarHuella_Load(sender, e);
            timeReload.Stop();
        }
    }
    }
