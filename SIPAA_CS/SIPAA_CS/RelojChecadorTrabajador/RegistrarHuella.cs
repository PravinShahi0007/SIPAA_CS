using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neurotec.Biometrics;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Utilerias;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class RegistrarHuella : Form
    {
        public RegistrarHuella()
        {
            InitializeComponent();
        }

        public Nffv engine;
        public ResultadoHuella objScanner = new ResultadoHuella();
        BackgroundWorker task = new BackgroundWorker();




        private void RegistrarHuella_Load(object sender, EventArgs e)
        {
            tmHora.Start();
            string dbName = "FingerprintsDatabase.dat";
            string password = "";
            string scanners = "UareU";
            //string UserDatabase = "Datos.xml";
            try
            {
                
                engine = new Nffv(dbName, password, scanners);
                engine.Users.Clear();
                 task = new BackgroundWorker();

                task.DoWork += Task_DoWork;
                task.RunWorkerCompleted += Task_RunWorkerCompleted;
                task.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                //RegistrarHuella_Load(sender, e);
            }

        }

        private void Task_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (objScanner.engineStatus == NffvStatus.TemplateCreated)
            {
                NffvUser engineUser = objScanner.engineUser;
                pbHuella.Image = engineUser.GetBitmap();
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Huella Verificada...");
                timer1.Start();

            }
         
        }

        private void Task_DoWork(object sender, DoWorkEventArgs e)
        {
                doEnroll(sender, e);
                CancelScanningHandler(sender, e);
            
        }

        private void doEnroll(object sender, DoWorkEventArgs args)
        {
            
            objScanner.engineUser = engine.Enroll(20000, out objScanner.engineStatus);
            //args.Result = objScanner;

        }
        private void CancelScanningHandler(object sender, EventArgs e)
        {
            engine.Cancel();
        }



      

        private void timer1_Tick(object sender, EventArgs e)
        {
            engine.Dispose();
            pbHuella.Image = null;
            panelTag.Visible = false;
            objScanner = new ResultadoHuella();
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
    }
    }
