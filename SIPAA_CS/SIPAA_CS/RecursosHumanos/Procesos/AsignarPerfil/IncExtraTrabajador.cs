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
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Properties;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class IncExtraTrabajador : Form
    {
        public IncExtraTrabajador()
        {
            InitializeComponent();
        }

        SonaTrabajador empleados = new SonaTrabajador();
        IncExtraSuspRetro extrañamientos = new IncExtraSuspRetro();
        Utilerias util = new Utilerias();

        //***********************************************************************************************
        //Autor: José Luis Alvarez Delgado
        //Fecha creación:20-02-2018     Última Modificacion: 
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void IncExtraTrabajador_Load(object sender, EventArgs e)
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

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //llama el tooltip
            ftooltip();

            //Combo Empleados
            DataTable dtempleados = empleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvIncExtTrab_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvIncExtTrab.Rows.Count; iContador++)
            {
                dgvIncExtTrab.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvIncExtTrab.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvIncExtTrab.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                if (Convert.ToString(row.Cells[9].Value) == "No")
                {
                    MessageBox.Show("El Extrañamiento seleccionado ya esta Condonado.", "SIPAA", MessageBoxButtons.OK);
                    //frecargar();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Esta Seguro de Condonar el Extrañamiento para este Empleado ?", "SIPAA", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //mandar la actualizacion
                        DataTable extra = extrañamientos.ExtraSuspRetroxTrabajador(1, Convert.ToString(row.Cells[1].Value),
                            Convert.ToDateTime(row.Cells[10].Value), Convert.ToInt32(row.Cells[11].Value),
                            Convert.ToInt32(row.Cells[12].Value), Convert.ToInt32(row.Cells[3].Value),
                            Convert.ToInt32(row.Cells[5].Value), Convert.ToDateTime(row.Cells[7].Value),
                            Convert.ToDateTime(row.Cells[8].Value), LoginInfo.IdTrab, this.Name, 0);

                        if (extra.Rows.Count != 0)
                        {
                            lblMensaje.Text = "La Incidencia fue Condonada Satisfactoriamente.";
                            panelTag.Visible = true;
                            timer1.Start();
                        }
                        else
                        {
                            lblMensaje.Text = "Problemas al Condonadar la Incidencia.";
                            panelTag.Visible = true;
                            timer1.Start();
                        }
                        frecargar();
                    }
                    else if (result == DialogResult.No)
                    {
                        row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    }
                }
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbEmpleados.Text != "Seleccionar")
            {
                if (dgvIncExtTrab.Rows.Count>=1)
                {
                    dgvIncExtTrab.Columns.RemoveAt(0);
                }
                llenaGridExtrañamientos(cbEmpleados.SelectedValue.ToString());
            }
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        //boton minimizar        
        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btnCerrar_Click_1(object sender, EventArgs e)
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


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        
        // llena el grid de Captura extrañamientos
        private void llenaGridExtrañamientos(string NoTrabajador)
        {
            DataTable extra = extrañamientos.ExtraSuspRetroxTrabajador(2, NoTrabajador, DateTime.Now, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", 0);
            dgvIncExtTrab.DataSource = extra;
            if (dgvIncExtTrab.SelectedRows.Count == 0) 
            {
                MessageBox.Show("No se encontro Información para ese Empleado.", "SIPAA", MessageBoxButtons.OK);
                frecargar();
                this.Close();                
            }
            else
            {
                Utilerias.AgregarCheck(dgvIncExtTrab, 0);
                dgvIncExtTrab.ClearSelection();
                //dgvIncExtTrab.Columns[0].Width = 70;
                //dgvIncExtTrab.Columns[1].Width = 40;
                //dgvIncExtTrab.Columns[2].Width = 180;
                dgvIncExtTrab.Columns[3].Visible = false;
                //dgvIncExtTrab.Columns[4].Width = 120;
                dgvIncExtTrab.Columns[5].Visible = false;
                //dgvIncExtTrab.Columns[6].Width = 170;
                //dgvIncExtTrab.Columns[7].Width = 70;
                //dgvIncExtTrab.Columns[8].Width = 70;
                //dgvIncExtTrab.Columns[9].Width = 60;
                //dgvIncExtTrab.Columns[10].Width = 70;
                dgvIncExtTrab.Columns[11].Visible = false;
                dgvIncExtTrab.Columns[12].Visible = false;
            }
        }
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
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Buscar Registros");
            //toolTip1.SetToolTip(this.btnInsertar, "Asignar/Modificar/Eliminar Datos");
        }

        private void frecargar()
        {
            IncExtraTrabajador recargar = new IncExtraTrabajador();
            recargar.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
