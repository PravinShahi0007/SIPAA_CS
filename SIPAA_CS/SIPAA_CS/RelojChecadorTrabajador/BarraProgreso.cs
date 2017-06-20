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

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class BarraProgreso : Form
    {
        public BarraProgreso(string Title)
        {
            InitializeComponent();
            label1.Text = Title;
            //bd.DoWork += Bd_DoWork;
            //bd.RunWorkerCompleted += Bd_RunWorkerCompleted;
            pbar.Style = ProgressBarStyle.Marquee;
            timer1.Interval = 5000;
            //timer1.Start();
            this.ShowDialog();
        }
        public int iContador;
        BackgroundWorker bd = new BackgroundWorker();
        private DoWorkEventHandler callback;
        private Exception error;
        private RunWorkerCompletedEventArgs results;

        private void BarraProgreso_Load(object sender, EventArgs e)
        {
            
        }

   

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }

        public static void Dialog(string Title)
        {

            using(BarraProgreso frm = new BarraProgreso(Title)) {

              
            }
            //parent.ShowDialog();

          
        }

        public static void Dialog()
        {

            using (BarraProgreso frm = new BarraProgreso("Actualizando Registros"))
            {

                frm.Close();
            }

        }


        private void Bd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            results = e;
          //  Close();
        }

        private void Bd_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (callback != null)
                {
                    callback(sender, e);
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
