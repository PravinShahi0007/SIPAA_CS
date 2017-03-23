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

namespace SIPAA_CS.RecursosHumanos
{
    public partial class Ubicaciones : Form
    {
        public Ubicaciones()
        {
            InitializeComponent();
        }

        private void Ubicacion_Load(object sender, EventArgs e)
        {
            llenarGrid("");
        }

        private void llenarGrid(string Descripcion) {
            SonaCompania objCia = new SonaCompania();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
