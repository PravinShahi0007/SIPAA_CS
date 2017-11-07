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
using static SIPAA_CS.App_Code.Usuario;
using static SIPAA_CS.App_Code.Utilerias;

namespace SIPAA_CS.Accesos.Catalogos
{
    public partial class Procesos : Form
    {
        Proceso proceso = new Proceso();
        Utilerias utilerias = new Utilerias();
        public int variable = 0;
        public string cvproceso;
        public string stproceso;
        public string descripcion;
        public string buscar;
        public string usuumod = LoginInfo.IdTrab;
        

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

                DataGridViewRow row = this.dgvProceso.SelectedRows[0];
                cvproceso = row.Cells["cvproceso"].Value.ToString();
                stproceso = row.Cells["Estatus"].Value.ToString();
                string descricpion = row.Cells["Descripción"].Value.ToString();
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                if (Permisos.dcPermisos["Eliminar"] == 1 && Permisos.dcPermisos["Actualizar"] == 1)
                {
                    variable = 2;
                    lblActividad.Text = "     Editar Proceso";
                    pnlAct.Visible = true;
                    cbEliminar.Checked = false;
                    txtDescripcion.Text = descricpion;
                    cbEliminar.Visible = true;
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Editar");

                    if (stproceso == "Inactivo")
                    {
                        cbEliminar.Text = "Alta";
                    }
                    else if (stproceso == "Activo")
                    {
                        cbEliminar.Text = "Baja";
                    }
                }
                else if (Permisos.dcPermisos["Actualizar"] == 1)
                {
                    variable = 2;
                    cbEliminar.Visible = false;
                    lblActividad.Text = "     Editar Proceso";
                    pnlAct.Visible = true;
                    txtDescripcion.Text = descricpion;
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Editar");
                }
                else if (Permisos.dcPermisos["Eliminar"] == 1)
                {
                    variable = 3;
                    cbEliminar.Visible = false;
                    //lblActividad.Text = "     Editar Proceso";
                    pnlAct.Visible = true;
                    txtDescripcion.Text = descricpion;
                    //Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Editar");

                    if (stproceso == "Inactivo")
                    {
                       
                        lblActividad.Text = "      Alta Usuario SIPAA";
                        Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Alta");
                    }
                    else if (stproceso == "Activo")
                    {
                      
                        lblActividad.Text = "      Baja Usuario SIPAA";
                        Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), "Baja");
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        
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
                    int regreso = proceso.AgregarProceso("", descripcion.Trim(), 0, usuumod, prgmod, 1);
                    pnlAct.Visible = false;
                    if (regreso == 1)
                    {
                        //MessageBox.Show("Se agrego proceso");
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se agregó Proceso");
                        timer1.Start();
                        Crear_Procesos_Load(sender, e);
                        txtDescripcion.Text = "";
                        //txtDescripcion.Focus();
                    }
                    else
                    {
                        //MessageBox.Show("El proceso ya existe");
                        //txtDescripcion.Focus();
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El Proceso ya existe");
                        timer1.Start();
                    }
                }
                else
                {
                    //MessageBox.Show("Ingresa una descripcion");
                    //txtDescripcion.Focus();
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa una Descripción");
                    timer1.Start();
                }
            }
            //editar
            if (variable == 2)
            {
                //MessageBox.Show("Ingresa una 2");
                if (descripcion.Trim() != String.Empty)
                {
                    string prgmod = this.Name;
                    int regreso = proceso.AgregarProceso(cvproceso, descripcion.Trim(), 0, usuumod, prgmod, 2);
                    pnlAct.Visible = false;
                    if (regreso == 1)
                    {
                        //MessageBox.Show("Sea actualizo proceso");
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se actualizó Proceso");
                        timer1.Start();
                        Crear_Procesos_Load(sender, e);
                        txtDescripcion.Text = "";
                        txtDescripcion.Focus();
                    }
                    else
                    {
                        //MessageBox.Show("El proceso no se actualizo");
                        //txtDescripcion.Focus();
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El Proceso no se actualizó");
                        timer1.Start();
                    }
                }
                else
                {
                    //MessageBox.Show("Ingresa una descripcion");
                    //txtDescripcion.Focus();
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa una Descripción");
                    timer1.Start();
                }
            }
            //eliminar
            if (variable == 3)
            {
                //MessageBox.Show("Ingresa una 3");
                string prgmod = this.Name;
                int regreso = proceso.AgregarProceso(cvproceso, "", 0, usuumod, prgmod, 3);
                pnlAct.Visible = false;
                if (regreso == 1)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Proceso Activo");
                    timer1.Start();
                    //MessageBox.Show("Sea activo proceso");
                    Crear_Procesos_Load(sender, e);
                    txtDescripcion.Text = "";
                    txtDescripcion.Focus();
                    cbEliminar.Checked = false;

                }
                else if (regreso == 0)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Proceso Inactivo");
                    timer1.Start();
                    //MessageBox.Show("Sea desactivo proceso");
                    Crear_Procesos_Load(sender, e);
                    txtDescripcion.Text = "";
                    txtDescripcion.Focus();
                    cbEliminar.Checked = false;
                }
            }

        }
        private void cbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEliminar.Checked == true)
            {
                
                if (stproceso == "Inactivo")
                {
                    variable = 3;
                    lblActividad.Text = "      Alta Proceso";
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Alta);
                }
                else if (stproceso == "Activo")
                {
                    variable = 2;
                    lblActividad.Text = "      Baja Proceso";
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Baja);
                }

            }
            else
            {
                
                lblActividad.Text = "      Editar Proceso";
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
            LlenaGrid(buscar, "", 0, "", "", 5);
            txtBuscar.Text = "";
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            AcceDashboard accedb = new AcceDashboard();
            accedb.Show();
            this.Close();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
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
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Crear_Procesos_Load(object sender, EventArgs e)
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

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            
            LlenaGrid("", "", 0, "", "", 4);

            if (Permisos.dcPermisos["Crear"] == 0)
            {
                btnAgregar.Visible = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        private void LlenaGrid(string cvproceso,string descripcion,int stproceso,string usuumod,string prgumod, int opcion)
        {
            DataTable dtFormasRegistro = proceso.ObtenerProceso(cvproceso,descripcion,stproceso,usuumod,prgumod,opcion);
            dgvProceso.DataSource = dtFormasRegistro;
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvProceso.Columns.Insert(0, imgCheckProcesos);
            dgvProceso.Columns[0].Width = 90;
            dgvProceso.Columns[0].HeaderText = "Seleccionar";
            dgvProceso.Columns[1].Visible = false;
            dgvProceso.Columns[2].Width = 410;
            dgvProceso.Columns[3].Width = 90;
            dgvProceso.ClearSelection();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
