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
using static SIPAA_CS.App_Code.SonaCompania;

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class DatosTrabajadorPerfil : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public DatosTrabajadorPerfil()
        {
            InitializeComponent();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void DatosTrabajadorPerfil_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));


            string sIdtrab = TrabajadorInfo.IdTrab;
            List<string> ltTnom = new List<string>();
            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(sIdtrab, 5, "%", "%");

            foreach (DataRow row in dtTrab.Rows) {

                lbIdTrab.Text = row["idtrab"].ToString();
                lbNombre.Text = row["Nombre"].ToString();
                TrabajadorInfo.Nombre = row["Nombre"].ToString();
                lbCia.Text = row["Compañia"].ToString();
                lbUbicacion.Text = row["Ubicación"].ToString();
                if (row["Estatus"].ToString() == "1") { lbEstatus.Text = "Activo"; } else { lbEstatus.Text = "Inactivo"; }
                lbArea.Text = row["Área"].ToString();
                lbPuesto.Text = row["Puesto"].ToString();
                lbFechaIngreso.Text = row["Fecha_Ingreso"].ToString();

                if (!ltTnom.Contains(row["Tipo_Nomina"].ToString())) {

                    ltTnom.Add(row["Tipo_Nomina"].ToString());

                    ltvTnom.Items.Add(row["Tipo_Nomina"].ToString());
                    ltvTnom.View = View.List;
                }

                lbHorario.Text = row["Tipo_Horario"].ToString();

                switch (Convert.ToInt32(row["Checa"].ToString())) {
                        
                    case 0: panelAsignacionTrabajador.Visible = false; 
                        lbCheca.Text = "NO";
                        lbCheca.ForeColor = ColorTranslator.FromHtml("#f44336");
                        break;
                    case 1:
                            lbCheca.Text = "SI";
                            lbCheca.ForeColor = ColorTranslator.FromHtml("#2e7d32");
                            panelAsignacionTrabajador.Visible = true;
                            lbSupervisor.Text = row["Supervisor"].ToString();
                            lbDirector.Text = row["Director"].ToString();
                        break;
                }

              
                lbDepto.Text = row["Depto"].ToString();
               
            }

            ltTnom.Clear();
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

            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnAsignaciones_Click(object sender, EventArgs e)
        {
            AsignacionTrabajadorPerfil form = new AsignacionTrabajadorPerfil();
            form.Show();
        }
    }
}
