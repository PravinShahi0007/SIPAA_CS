using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class Reportes : Form
    {
        ReportDocument Report = new ReportDocument();
       
        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            Modulo objModulo = new Modulo();
            string idTrab = LoginInfo.IdTrab;
            DataTable dtPermisos = objModulo.ObtenerPermisosxUsuario("ADMIN");
            Report.SetDataSource(dtPermisos);
            ReporteCrystal.ReportSource = Report;
        }
    }
}
