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
using zkemkeeper;
using static SIPAA_CS.RelojChecadorTrabajador.AdministracionRelojChecador;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class CapturaHora : Form
    {
       

        List<Reloj> ltReloj =  new List<Reloj>();
        public CZKEMClass objCZKEM = new CZKEMClass();
        public int iOpcionAdmin;
        public CapturaHora(List<Reloj> ltRelojPadre)
        {
            InitializeComponent();
            ltReloj = ltRelojPadre;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ltReloj.Count > 0) {
                panelMensaje.Visible = true;
                int iCont = 0;
                prgb1.Value = 20;
                bool bBandera = false;
                foreach (Reloj obj in ltReloj) {
                    iCont += 1;
                    panelMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag,lbMensaje,2,"Conectando con Dispositivo " + iCont + " de " + ltReloj.Count);
                    prgb1.Value = 40;
                    panelMensaje.Enabled = false;
                    bool bConect = objCZKEM.Connect_Net(obj.IpReloj, 4370);
                  
                    if (bConect != false) {
                        prgb1.Value = prgb1.Value + (10 / ltReloj.Count);
                        switch (iOpcionAdmin) {

                            case 0:
                                int iAnio = Convert.ToInt32(cbAnio.SelectedItem.ToString());
                                int iMes = cbMes.SelectedIndex + 1;
                                int iDia = cbdia.SelectedIndex + 1;
                                int iHora = cbHora.SelectedIndex;
                                int iMinuto = cbMinutos.SelectedIndex;
                                if (!objCZKEM.SetDeviceTime2(1, iAnio, iMes, iDia, iHora, iMinuto, 0)) { bBandera = true; }
                                break;

                            case 1:
                                if (!objCZKEM.SetDeviceTime(1)) { bBandera = true; }
                                break;
                        }

                        prgb1.Value = 90;
                    }
                }
                panelMensaje.Enabled = true;

                if (bBandera == true)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Uno o más registros no pudieron ser Enviados. Repetir el proceso");
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Hora Asignada Correctamente.");
                }
                prgb1.Value = 100;

            }
            timer1.Start();
        }

        private void CapturaHora_Load(object sender, EventArgs e)
        {
            Cargarminutos();
            CargarMeses();
            CargarAnio();
            CargaDias(DateTime.Now.Month);
            cargarHora();

            cbMes.SelectedIndex = (DateTime.Now.Month - 1);
            cbAnio.SelectedItem = DateTime.Now.Year;
            cbdia.SelectedIndex = (DateTime.Now.Day - 1);

            cbHora.SelectedIndex = (DateTime.Now.Hour);
            cbMinutos.SelectedIndex = (DateTime.Now.Minute - 1);

            label8.Text = DateTime.Now.ToShortDateString();
            label9.Text = DateTime.Now.ToShortTimeString();

            checkBox1.Checked = true;
        }


        public void Cargarminutos() {

            List<string> ltMin = new List<string>();
            string sMinuto = "";
            for (int iMin = 0; iMin <= 60; iMin++) {
                sMinuto = iMin.ToString();
                if (sMinuto.Length == 1) {
                    sMinuto = "0" + sMinuto;
                }

                ltMin.Add(sMinuto);   
            }
            cbMinutos.DataSource = ltMin;
        }


        public void CargarMeses()
        {

            List<string> ltMeses = new List<string>();

            ltMeses.Add("Enero");
            ltMeses.Add("Febrero");
            ltMeses.Add("Marzo");
            ltMeses.Add("Abril");
            ltMeses.Add("Mayo");
            ltMeses.Add("Junio");
            ltMeses.Add("Julio");
            ltMeses.Add("Agosto");
            ltMeses.Add("Septiembre");
            ltMeses.Add("Octubre");
            ltMeses.Add("Noviembre");
            ltMeses.Add("Diciembre");

            cbMes.DataSource = ltMeses;
        }

        public void CargarAnio() {

            List<int> ltAnio = new List<int>();

            for (int iAnio = DateTime.Now.Year; iAnio >= 1999; iAnio--) {

                ltAnio.Add(iAnio);
            }

            cbAnio.DataSource = ltAnio;

        }

        public void CargaDias(int iMes)
        {
            int diasTotal = 0;

            if (iMes == 1 || iMes == 3 || iMes == 5 || iMes == 7 || iMes == 8 || iMes == 10 || iMes == 12)
            {
                diasTotal = 31;
            }
            else if (iMes == 4 || iMes == 6 || iMes == 9 || iMes == 11)
            {
                diasTotal = 30;
            }
            else if (iMes == 2) {

                diasTotal = 28;
            }
            List<int> ltdias = new List<int>();

            for (int iDias = 1;iDias <= diasTotal;iDias ++)
            {

                ltdias.Add(iDias);
            }

            cbdia.DataSource = ltdias;

        }

        public void cargarHora() {

            List<string> ltHora = new List<string>();
            string sHora = "";
            for (int iCont = 1; iCont <= 23; iCont++) {
                sHora = iCont.ToString();

                if (sHora.Length == 1) {

                    sHora = "0" + sHora;
                }

                ltHora.Add(sHora);

            }

            ltHora.Insert(0,"00");

            cbHora.DataSource = ltHora;

        }

        private void cbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaDias(cbMes.SelectedIndex + 1);
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            AdministracionRelojChecador Form = new AdministracionRelojChecador();
            Form.Show(); 
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelMensaje.Visible = false;
            timer1.Stop();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                panelAccion.Enabled = false;
                label8.Enabled = true;
                label9.Enabled = true;
                iOpcionAdmin = 1;

            }
            else {

                panelAccion.Enabled = true;
                label8.Enabled = false;
                label9.Enabled = false;
                iOpcionAdmin = 0;

            }
        }
    }
}
