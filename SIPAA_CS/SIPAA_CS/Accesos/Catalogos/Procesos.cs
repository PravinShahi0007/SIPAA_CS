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

namespace SIPAA_CS.Recursos_Humanos.Administracion
{
    public partial class Crear_Procesos : Form
    {
        Proceso proceso = new Proceso();
        Utilerias utilerias = new Utilerias();
        public int variable = 0;
        public int cvproceso;

        public string descripcion;
        public string buscar;
        public Crear_Procesos()
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

                DataGridViewRow row = this.dgvProceso.SelectedRows[0];

                cvproceso = Convert.ToInt32(row.Cells["cvproceso"].Value.ToString());
                string ValorRow = row.Cells["descripcion"].Value.ToString();

                txtDescripcion.Text = ValorRow;
                cbEliminar.Visible = true;
                //txtCapFR.Focus();

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                utilerias.ChangeButton(btnGuardar, 2, false);

                txtDescripcion.Focus();

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlAct.Visible = true;
            lblActividad.Text = "     Crear Proceso";
            txtDescripcion.Focus();

            cbEliminar.Visible = false;
            txtDescripcion.Text = "";
            

            variable = 1;

            utilerias.ChangeButton(btnGuardar,1,false);


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


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            descripcion = txtDescripcion.Text;
            descripcion.Trim();

            //cvproceso;


            //agregar
            if (variable == 1)
            {
                //MessageBox.Show("Ingresa una 1");
                if (descripcion.Trim() != String.Empty)
                {
                    string prgmod = this.Name;
                    int regreso = proceso.AgregarProceso(0, descripcion.Trim(), 0, "", prgmod, 1);
                    if (regreso == 1)
                    {
                        MessageBox.Show("Sea agrego proceso");
                        Crear_Procesos_Load(sender, e);
                        txtDescripcion.Text = "";
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El proceso ya existe");
                        txtDescripcion.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa una descripcion");
                    txtDescripcion.Focus();
                }
            }
            //editar
            if (variable == 2)
            {
                //MessageBox.Show("Ingresa una 2");
                if (descripcion.Trim() != String.Empty)
                {
                    int regreso =  proceso.AgregarProceso(cvproceso, descripcion.Trim(),0,"","",3);
                    if (regreso == 1)
                    {
                        MessageBox.Show("Sea actualizo proceso");
                        Crear_Procesos_Load(sender, e);
                        txtDescripcion.Text = "";
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El proceso no se actualizo");
                        txtDescripcion.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa una descripcion");
                    txtDescripcion.Focus();
                }
            }
            //eliminar
            if (variable == 3)
            {
                //MessageBox.Show("Ingresa una 3");
                int regreso = proceso.AgregarProceso(cvproceso, "", 0, "", "", 4);
                if (regreso == 1)
                {
                    MessageBox.Show("Sea activo proceso");
                    Crear_Procesos_Load(sender, e);
                    txtDescripcion.Text = "";
                    txtDescripcion.Focus();
                    cbEliminar.Checked = false;

                }
                else if(regreso == 0)
                {
                    MessageBox.Show("Sea desactivo proceso");
                    Crear_Procesos_Load(sender, e);
                    txtDescripcion.Text = "";
                    txtDescripcion.Focus();
                    cbEliminar.Checked = false;
                }
            }


        }
        private void cbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            //variable = 3;

            if (cbEliminar.Checked == true)
            {
                variable = 3;
                utilerias.ChangeButton(btnGuardar, 3, false);
               
            }
            else if (cbEliminar.Checked == false)
            {
                variable = 2;
                utilerias.ChangeButton(btnGuardar, 2, false);
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Crear_Procesos_Load(object sender, EventArgs e)
        {
           
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
            dgvProceso.Columns[0].HeaderText = "";

            dgvProceso.Columns[1].Visible = false;
            dgvProceso.Columns[0].Width = 55;
            dgvProceso.Columns[2].Width = 302;

            dgvProceso.ClearSelection();
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
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
