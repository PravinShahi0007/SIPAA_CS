using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using static SIPAA_CS.App_Code.Usuario;

//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
//Descripción: sincroniza sica provicional
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class SincRegistrossica : Form
    {

        SincRegistrosica SincReg = new SincRegistrosica();
        SonaTrabajador InsReg = new SonaTrabajador();

        int inum;

        public SincRegistrossica()
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
            try
            {
                RechDashboard rechdb = new RechDashboard();
                rechdb.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {

                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void SincRegistrossica_Load(object sender, EventArgs e)
        {
            try
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
                //tool tip
                ftooltip();

                //usuario
                lblusuario.Text = LoginInfo.Nombre;
                Utilerias.cargaimagen(ptbimgusuario);



                DataTable dtregsica = SincReg.sincregsica();
                dgvregistros.DataSource = dtregsica;

                

            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
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
            toolTip1.SetToolTip(this.btncerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");

        }

        private void fsicasincreg()
        {
            try
            {

                textBox1.Text = "1";

                DataTable dtsincregsica = SincReg.sincregsica();

                //textBox1.Text = dtsincregsica.Rows.Count.ToString();
                //inum = 1;
                foreach (DataRow row in dtsincregsica.Rows)
                {

                    //int inum = Int32.Parse(row["idtrab"].ToString());
                    string sidtrab = row["idtrab"].ToString();
                    string sferegistro = row["fec"].ToString();
                    string shrregistro = row["hrregistro"].ToString();

                    //textBox1.Text = inum.ToString();

                    //System.Threading.Thread.Sleep(20);


                    //inum = inum + 1;

                    InsReg.Relojchecador(sidtrab, 5, DateTime.Parse(sferegistro), 0, TimeSpan.Parse(shrregistro), 100, 0, "nam", this.Name);

                }

                DialogResult result = MessageBox.Show("Registros importados con exito", "SIPAA", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(ex.Message + ex.StackTrace, "SIPAA");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fsicasincreg();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
