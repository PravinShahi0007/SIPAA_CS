using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class ViewerReporte : Form
    {
        public ReportDocument RptDoc;
        public DataTable dtRpt;
        public int sysH = SystemInformation.PrimaryMonitorSize.Height;
        public int sysW = SystemInformation.PrimaryMonitorSize.Width;
      
        public ViewerReporte()
        {
            InitializeComponent();
        }

        private void ViewerReporte_Load(object sender, EventArgs e)
        {
           Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

           
            ReporteView.ReportSource = RptDoc;

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
        }

       

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

            this.Close();
        }
    }
}
