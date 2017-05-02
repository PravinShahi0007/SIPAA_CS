using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class SplashHuella : Form
    {
        public SplashHuella()
        {
            InitializeComponent();
        }

        private void SplashHuella_Load(object sender, EventArgs e)
        {

        }

        private DoWorkEventHandler callback;
        private object argument;
        private RunWorkerCompletedEventArgs results;
        private Exception error;
        protected BackgroundWorker _worker = new BackgroundWorker();
        private SplashHuella(string title, DoWorkEventHandler callback)
        : this(title, callback, false, null, null)
        {
        }

        private SplashHuella(string title, DoWorkEventHandler callback, object args) : this(title, callback, false, args, null)
        {
        }

        private SplashHuella(string title, DoWorkEventHandler callback, bool reportsProgress) : this(title, callback, reportsProgress, null, null)
        {
        }
        public SplashHuella(string title, DoWorkEventHandler callback, bool reportsProgress, object args, EventHandler cancelHandler)
        {
            InitializeComponent();
            if (!reportsProgress)
                PrgbCarga.Style = ProgressBarStyle.Marquee;

            SetExecutionText(title);
            this.callback = callback;
            argument = args;
            _worker.WorkerReportsProgress = reportsProgress;
            _worker.DoWork += BusyForm_DoWork;
            _worker.RunWorkerCompleted += BusyForm_RunWorkerCompleted;
            _worker.ProgressChanged += BusyForm_ProgressChanged;

            if (cancelHandler != null)
            {
                _worker.WorkerSupportsCancellation = true;
              
            }
        }
       
        public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback)
        {
            return RunLongTask(title, callback, false);
        }
        public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback, bool reportsProgress)
        {
            using (SplashHuella frmLongTask = new SplashHuella(title, callback, reportsProgress))
            {
                frmLongTask.ShowDialog();
                if (frmLongTask.error != null)
                {
                    throw frmLongTask.error;
                }
                return frmLongTask.results;
            }
        }
        public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback, bool reportsProgress, object args, EventHandler cancelHandler)
        {
            using (SplashHuella frmLongTask = new SplashHuella(title, callback, reportsProgress, args, cancelHandler))
            {
                frmLongTask.ShowDialog();
                if (frmLongTask.error != null)
                {
                    throw frmLongTask.error;
                }
                return frmLongTask.results;
            }
        }

        private void BusyForm_Shown(object sender, EventArgs e)
        {
            _worker.RunWorkerAsync(argument);
        }
        #region Background worker
        private void BusyForm_DoWork(object sender, DoWorkEventArgs e)
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

        private void BusyForm_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string text = e.UserState as string;
            if (text != null)
            {
                SetExecutionText(text);
            }

            SetExecutionValue(e.ProgressPercentage);
        }

        private void BusyForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            results = e;
            Close();
        }
      
        public void SetExecutionText(string text)
        {
            try
            {
                if (lbOperacion.InvokeRequired)
                {
                    lbOperacion.Invoke((MethodInvoker)delegate ()
                    {
                        lbOperacion.Text = text;
                    });
                }
                else
                {
                    lbOperacion.Text = text;
                }
            }
            finally
            {
            }
        }

        public void SetExecutionValue(int value)
        {
            try
            {
                if (PrgbCarga.InvokeRequired)
                {
                    PrgbCarga.Invoke((MethodInvoker)delegate ()
                    {
                        PrgbCarga.Value = value;
                    });
                }
                else
                {
                    PrgbCarga.Value = value;
                }
            }
            finally
            {
            }
        }
    }
}
#endregion