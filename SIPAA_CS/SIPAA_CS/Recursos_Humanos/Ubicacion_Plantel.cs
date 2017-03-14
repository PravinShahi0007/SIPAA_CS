using SIPAA_CS.Recursos_Humanos.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Recursos_Humanos
{
    public partial class Ubicacion_Plantel : Form
    {
        public Ubicacion_Plantel()
        {
            InitializeComponent();
        }

        private void Ubicacion_Load(object sender, EventArgs e)
        {
            llenarGrid("");
        }

        private void llenarGrid(string Descripcion) {
            CompaniasSonarh objCia = new CompaniasSonarh();
            DataTable dtUbicacion = objCia.ObtenerUbicacionPlantel(Descripcion);
            dgvUbicacion.DataSource = dtUbicacion;
            dgvUbicacion.Columns[0].Visible = false;
            dgvUbicacion.ClearSelection();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid(txtBuscarUbicacion.Text);
        }

        private void txtBuscarUbicacion_KeyUp(object sender, KeyEventArgs e)
        {
            llenarGrid(txtBuscarUbicacion.Text);
        }
    }
}
