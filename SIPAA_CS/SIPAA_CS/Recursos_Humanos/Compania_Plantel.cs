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
    public partial class Compania_Plantel : Form
    {
        public Compania_Plantel()
        {
            InitializeComponent();
        }

        private void PanelPlantilla_Paint(object sender, PaintEventArgs e)
        {
            CompaniasSonarh objCia = new CompaniasSonarh();
            DataTable dtCia = objCia.obtcomp(1,"");
            List<string> ltCia = new List<string>();
            foreach (DataRow row in dtCia.Rows) {
                ltCia.Add(row["Descripción"].ToString());
            }
            cbCia.DataSource = ltCia;
            LlenarGridPlanteles(1, "", dgvPlantel);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LlenarGridPlanteles((cbCia.SelectedIndex + 1), txtBuscarPerfil.Text, dgvPlantel);
        }

        private void cbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarGridPlanteles((cbCia.SelectedIndex + 1), txtBuscarPerfil.Text, dgvPlantel);
        }

        public void LlenarGridPlanteles(int idCia,string busqueda, DataGridView dgvPlantel) {


            CompaniasSonarh objCia = new CompaniasSonarh();
            DataTable dtPlantel = objCia.ObtenerPlantelxCompania(idCia, busqueda);

            dgvPlantel.DataSource = dtPlantel;

            dgvPlantel.Columns[0].Visible = false;
            dgvPlantel.Columns[1].Visible = false;
            dgvPlantel.Columns[2].Visible = false;
            dgvPlantel.ClearSelection();


        }

        private void txtBuscarPerfil_KeyUp(object sender, KeyEventArgs e)
        {
            LlenarGridPlanteles((cbCia.SelectedIndex + 1), txtBuscarPerfil.Text, dgvPlantel);
        }
    }
}
