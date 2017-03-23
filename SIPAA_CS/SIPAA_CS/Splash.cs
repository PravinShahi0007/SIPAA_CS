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

namespace SIPAA_CS
{
    public partial class Splash : Form
    {
        public int iContador;
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PanelCarga.Width < PanelBarra.Width)
            {

                int iSuma = (PanelBarra.Width / 100) * 5;

                PanelCarga.Width = PanelCarga.Width + iSuma;
                iContador = iContador + (iSuma / 4);
                lbCarga.Text = iContador + "%";
            }
            else
            {
                timer1.Enabled = false;
                //MessageBox.Show("Finalizo");

                Acceso fAcceso = new Acceso();
                fAcceso.Show();
                this.Hide();
            }
        }

        private void CompaniasUsuario_Load(object sender, EventArgs e)
        {
           
        }
    }
}
