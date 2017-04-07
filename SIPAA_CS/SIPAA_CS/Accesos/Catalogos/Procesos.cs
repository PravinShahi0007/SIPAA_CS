using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Utilerias;

namespace SIPAA_CS.Accesos
{
    public partial class Procesos : Form
    {
        Proceso proceso = new Proceso();
        Utilerias utilerias = new Utilerias();
        public int variable = 0;
        public int cvproceso;
        public int stproceso;
        public string descripcion;
        public string buscar;

        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public Procesos()
        {
            InitializeComponent();
        }


        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvProceso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvProceso.Rows.Count; iContador++)
            {
                dgvProceso.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            //sHabilitaPermisos();

            if (dgvProceso.SelectedRows.Count != 0)
            {
                variable = 2;
                pnlAct.Visible = true;
                cbEliminar.Checked = false;
                DataGridViewRow row = this.dgvProceso.SelectedRows[0];
                cvproceso = Convert.ToInt32(row.Cells["cvproceso"].Value.ToString());
                stproceso = Convert.ToInt32(row.Cells["stproceso"].Value.ToString());
                string ValorRow = row.Cells["descripcion"].Value.ToString();
                txtDescripcion.Text = ValorRow;
                cbEliminar.Visible = true;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                //utilerias.ChangeButton(btnGuardar, 2, false);
                Utilerias.AsignarBotonResize(btnGuardar,Utilerias.PantallaSistema(),Botones.Editar);

                //txtDescripcion.Focus();

                if (stproceso == 0)
                {
                    cbEliminar.Text = "Alta";

                }
                else if (stproceso == 1)
                {
                    cbEliminar.Text = "Baja";

                }

            }
        
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void cbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            //variable = 3;

            //if (stproceso == 1)
            //{
            //    variable = 3;
            //    //   utilerias.ChangeButton(btnGuardar, 3, false);
            //    Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), Botones.Baja);
            //}
            //else if (stproceso == 0)
            //{
            //    variable = 2;
            //    // utilerias.ChangeButton(btnGuardar, 2, false);
            //    Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), Botones.Editar);
            //}


            if (cbEliminar.Checked == true)
            {

                
                if (stproceso == 0)
                {
                    variable = 3;
                    lblActividad.Text = "      Alta Usuario SIPAA";
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Alta);
                }
                else if (stproceso== 1)
                {
                    variable = 2;
                    lblActividad.Text = "      Baja Usuario SIPAA";
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Baja);
                }

            }
            else
            {
                
                lblActividad.Text = "      Editar Usuario SIPAA";
                Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Editar);

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            variable = 1;
            pnlAct.Visible = true;
            lblActividad.Text = "     Crear Proceso";
            cbEliminar.Visible = false;
            txtDescripcion.Text = "";
            txtDescripcion.Focus();
            Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Guardar");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar = txtBuscar.Text;
            buscar.Trim();

            if (buscar.Trim() != String.Empty)
            {
                LlenaGrid(0, buscar.Trim(), 0, "0", "", 2);
            }
            else
            {
                MessageBox.Show("Asigna un busqueda");
                //DataTable dtFormasRegistro = proceso.ObtenerProceso(0, buscar.Trim(), 0, "0", "", 2);
                //dgvProceso.DataSource = dtFormasRegistro;



                //DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
                //imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
                //imgCheckProcesos.Name = "Seleccionar";
                //dgvProceso.Columns.Insert(0, imgCheckProcesos);
                //dgvProceso.Columns[0].HeaderText = "";

                //dgvProceso.Columns[1].Visible = false;
                //dgvProceso.Columns[0].Width = 55;
                //dgvProceso.Columns[2].Width = 302;

                //dgvProceso.ClearSelection();
            }
        }
        

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Minimized;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Crear_Procesos_Load(object sender, EventArgs e)
        {
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            LlenaGrid(0, "", 0, "0", "", 5);
            txtBuscar.Focus();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        private void LlenaGrid(int cvproceso,string descripcion,int stproceso,string usuumod,string prgumod, int opcion)
        {
            DataTable dtFormasRegistro = proceso.ObtenerProceso(cvproceso,descripcion,stproceso,usuumod,prgumod,opcion);
            dgvProceso.DataSource = dtFormasRegistro;
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvProceso.Columns.Insert(0, imgCheckProcesos);
            dgvProceso.Columns[0].HeaderText = "Seleccionar";
            dgvProceso.Columns[1].Visible = false;
            dgvProceso.ClearSelection();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
