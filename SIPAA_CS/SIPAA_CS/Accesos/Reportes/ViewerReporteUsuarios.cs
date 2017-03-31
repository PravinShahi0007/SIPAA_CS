using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;

namespace SIPAA_CS.Accesos.Reportes
{
    public partial class ViewerReporteUsuarios : Form
    {
        public ReportDocument RptDoc;
        public DataTable dtRpt;
        public int sysH = SystemInformation.PrimaryMonitorSize.Height;
        public int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public ViewerReporteUsuarios()
        {
            InitializeComponent();
        }
        
        private void ViewerReporteUsuarios_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            ReporteView.ReportSource = RptDoc;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
