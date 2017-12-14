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
using SIPAA_CS.App_Code.Sistemas.Procesos;
using SIPAA_CS.App_Code;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:27/10/2017     Última Modificacion: dd-mm-aaaa
//Descripción: monitores trabajos sql
//***********************************************************************************************

namespace SIPAA_CS.Sistemas.Procesos
{

    public partial class MonitoreosProcesos : Form
    {

        int iverifprocesos;

        MonitoreoProceso monitproc = new MonitoreoProceso();
        public MonitoreosProcesos()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnregresar_Click(object sender, EventArgs e)
        {
            SistDashboard sistdasb = new SistDashboard();
            sistdasb.Show();
            this.Close();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();

            }
        }
        private void btnactualizar_Click(object sender, EventArgs e)
        {
            facttrab();
            timer1.Start();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void MonitoreosProcesos_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }
            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //tool tip
            ftooltip();
            //usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            facttrab();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            facttrab();
            timer1.Start();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvmonitoreo.Rows.Count; i++)
            {
                int val = Int32.Parse(dgvmonitoreo.Rows[i].Cells[1].Value.ToString());
                if (val == 0)
                {
                    dgvmonitoreo.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
            timer2.Stop();
            timer3.Start();

        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            fformatgrid();
            timer3.Stop();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        //funcion para tool tip
        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btncerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
            toolTip1.SetToolTip(this.btnactualizar, "Actualizar");
        }

        private void facttrab()
        {
            lblact.Text = "Última Actualización :  " + DateTime.Now.ToString();

            DataTable dtmonit = monitproc.dtdgvcb(1);
            dgvmonitoreo.DataSource = dtmonit;

            dgvmonitoreo.Columns[0].Width = 160;
            dgvmonitoreo.Columns[1].Visible = false; ;
            dgvmonitoreo.Columns[2].Width = 65;
            dgvmonitoreo.Columns[3].Width = 420;
            dgvmonitoreo.Columns[4].Width = 155;
            dgvmonitoreo.Columns[5].Width = 155;
            dgvmonitoreo.ClearSelection();

            iverifprocesos = monitproc.iverifproc(2);
            if (iverifprocesos >= 1)
            {
                fformatgrid();
            }
            
        }
        private void fformatgrid()
        {
            for (int i = 0; i < dgvmonitoreo.Rows.Count; i++)
            {
                int val = Int32.Parse(dgvmonitoreo.Rows[i].Cells[1].Value.ToString());
                if (val == 0)
                {
                    dgvmonitoreo.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f44336");
                    
                }
            }
            timer2.Start();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
