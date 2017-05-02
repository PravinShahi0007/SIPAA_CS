using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.Accesos
{
    public partial class UsuarioProceso : Form
    {
        Usuario usuario = new Usuario();
        Proceso procesos = new Proceso();
        Utilerias utilerias = new Utilerias();
        List<string> ltUsuarioxProceso = new List<string>();
        List<string> ltProceso = new List<string>();
        //public int CVPerfil = 0;

        public string CVUsuario;
        public string CvProceso;

        public string buscar;
        public string descripcion;
        public string pass;

        public UsuarioProceso()
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
        private void dgvUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbAsignaPassword.Checked = false;
            txtPassword.Text = "";
            pnlPassword.Visible = false;
            for (int iContador = 0; iContador < dgvUsuario.Rows.Count; iContador++)
            {
                dgvUsuario.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvUsuario.SelectedRows.Count != 0)
            {
                cbAsignaPassword.Visible = true;
                DataGridViewRow row = this.dgvUsuario.SelectedRows[0];
                CVUsuario = row.Cells["cvusuario"].Value.ToString();
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                AsignarProcesos();
                
            }
        }
        private void dgvProceso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pass = txtPassword.Text;
            if (dgvUsuario.SelectedRows.Count != 0)
            {

                if (dgvProceso.SelectedRows.Count != 0)
                {
                    //check palomeado
                    if (cbAsignaPassword.Checked == true)
                    {
                        if (dgvProceso.SelectedRows.Count != 0)
                        {
                            AsignarProcesos();
                            Utilerias.MultiSeleccionGridViewString(dgvProceso, 1, ltProceso, panelPermisos);
                        }
                    }
                    //checkbox vacio
                    else if (cbAsignaPassword.Checked == false)
                    {
                        DataGridViewRow row = this.dgvProceso.SelectedRows[0];
                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        string cvpro = row.Cells[1].Value.ToString();

                        panelPermisos.Enabled = true;

                        ltProceso.Add(cvpro);

                        if (row.Cells[0].Tag.ToString() == "check")
                        {

                            row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                            row.Cells[0].Tag = "uncheck";

                        }
                        else
                        {
                            row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                            row.Cells[0].Tag = "check";

                        }
                        
                    }
                }

            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona primero un Usuario");
                timer1.Start();
            }
        }
        
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
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
            else if (result == DialogResult.No)
            {

            }
        }
       

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            buscar = txtUsuario.Text;
            //buscar.Trim();

            //dgvUsuario.Columns.Remove(columnName: "Seleccionar");

            //LlenaGridUsuarios(buscar.Trim(), 0, "", "", 0, "", "",11);
            txtUsuario.Text = "";

            if (buscar.Trim() != "")
            {
                LlenaGridUsuarios(buscar.Trim(), 0, "", "", 0, "", "", 11);
                LlenaGridProcesos("", "%", 0, "0", "", 8);
            }
            else
            {
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 11);
                LlenaGridProcesos("", "%", 0, "0", "", 8);
            }
        }

        private void btnBuscarProceso_Click(object sender, EventArgs e)
        {
            descripcion = txtDescripcion.Text;

            //dgvProceso.Columns.Remove(columnName: "Seleccionar");

            
            txtDescripcion.Text = "";

            if (descripcion != "")
            {
                LlenaGridProcesos("", descripcion.Trim(), 0, "", "", 8);
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 11);
            }
            else
            {
                LlenaGridProcesos("", "%", 0, "", "", 8);
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 11);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (dgvUsuario.SelectedRows.Count != 0 && dgvProceso.SelectedRows.Count != 0  )
            {
                AsignarProcesos();
                if (ltProceso.Count > 0)
                {
                   
                    // valida si es el unico a eliminar
                    if (Utilerias.SinAsignacionesString(dgvProceso, 0, 1, ltProceso) == true)
                    {
                        //asigna mismo password
                        if (cbAsignaPassword.Checked == false)
                        {
                            //int idcompania = cbCompania.SelectedIndex;
                            panelPermisos.Visible = true;
                            try
                            {
                                DataGridViewRow rowusu = this.dgvUsuario.SelectedRows[0];
                                pass = rowusu.Cells[5].Value.ToString();
                                string usuumod = LoginInfo.IdTrab;
                                string prgumod = this.Name;
                                Proceso objProceso = new Proceso();

                                foreach (string proceso in ltProceso)
                                {
                                    //string proc = Convert.ToString(proceso);
                                    objProceso.AsignarUsuarioProceso(CVUsuario, proceso, pass, usuumod, prgumod, 1);
                                }
                                panelPermisos.Enabled = false;
                                ltProceso.Clear();
                                AsignarProcesos();
                                cbAsignaPassword.Checked = false;
                                txtPassword.Text = "";
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                                timer1.Start();
                            }
                            catch (Exception ex)
                            {
                                timer1.Start();
                                MessageBox.Show("" + ex);
                            }
                        }

                        //asigna diferente password
                        else if (cbAsignaPassword.Checked == true)
                        {
                            panelPermisos.Visible = true;
                            pass = txtPassword.Text;
                            if (pass != "")
                            {
                                try
                                {
                                    pass = txtPassword.Text;
                                    Utilerias u = new Utilerias();
                                    string p = u.cifradoMd5(pass);
                                    string usuumod = LoginInfo.IdTrab;
                                    string prgumod = this.Name;
                                    Proceso objProceso = new Proceso();

                                    foreach (string proceso in ltProceso)
                                    {
                                        //string proc = Convert.ToString(proceso);
                                        objProceso.AsignarUsuarioProceso(CVUsuario, proceso, p, usuumod, prgumod, 1);
                                    }

                                    ltProceso.Clear();
                                    AsignarProcesos();
                                    cbAsignaPassword.Checked = false;
                                    txtPassword.Text = "";
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                                    timer1.Start();
                                }
                                catch (Exception ex)
                                {
                                    timer1.Start();
                                    MessageBox.Show("" + ex);
                                }
                            }
                            else {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ingresa un Password");
                                timer1.Start();
                            }
                            
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("¿Seguro que desea quitar todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            //asigna mismo password
                            if (cbAsignaPassword.Checked == true)
                            {
                                //int idcompania = cbCompania.SelectedIndex;
                                panelPermisos.Visible = true;
                                try
                                {
                                    DataGridViewRow rowusu = this.dgvUsuario.SelectedRows[0];
                                    pass = rowusu.Cells[5].Value.ToString();
                                    string usuumod = LoginInfo.IdTrab;
                                    string prgumod = this.Name;
                                    Proceso objProceso = new Proceso();

                                    foreach (string proceso in ltProceso)
                                    {
                                        //string proc = Convert.ToString(proceso);
                                        objProceso.AsignarUsuarioProceso(CVUsuario, proceso, pass, usuumod, prgumod, 1);
                                    }

                                    ltProceso.Clear();
                                    AsignarProcesos();
                                    cbAsignaPassword.Checked = false;
                                    txtPassword.Text = "";
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                                    timer1.Start();
                                }
                                catch (Exception ex)
                                {
                                    timer1.Start();
                                    MessageBox.Show("" + ex);
                                }
                            }

                            //asigna diferente password
                            else if (cbAsignaPassword.Checked == false)
                            {
                                panelPermisos.Visible = true;
                                try
                                {
                                    pass = txtPassword.Text;
                                    Utilerias u = new Utilerias();
                                    string p = u.cifradoMd5(pass);
                                    string usuumod = LoginInfo.IdTrab;
                                    string prgumod = this.Name;
                                    Proceso objProceso = new Proceso();

                                    foreach (string proceso in ltProceso)
                                    {
                                        //string proc = Convert.ToString(proceso);
                                        objProceso.AsignarUsuarioProceso(CVUsuario, proceso, p, usuumod, prgumod, 1);
                                    }

                                    ltProceso.Clear();
                                    AsignarProcesos();
                                    cbAsignaPassword.Checked = false;
                                    txtPassword.Text = "";
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                                    timer1.Start();
                                }
                                catch (Exception ex)
                                {
                                    timer1.Start();
                                    MessageBox.Show("" + ex);
                                }
                            }
                        }
                        else
                        {

                            AsignarProcesos();
                            //panelPermisos.Enabled = false;
                            ltProceso.Clear();
                        }
                    }
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un proceso");
                    timer1.Start();
                }
                
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Selecciona un Usuario y Proceso");
                timer1.Start();
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Asignar_Proceso_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            LlenaGridUsuarios("%", 0, "", "", 0, "", "",11);

            LlenaGridProcesos("", "%", 0, "0", "", 8);

            cbAsignaPassword.Visible = false;

            cbAsignaPassword.Checked = false;

            pnlPassword.Visible = false;
            
        }
        private void cbAsignaPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAsignaPassword.Checked == true)
            {
                pnlPassword.Visible = true;
            }
            else if (cbAsignaPassword.Checked == false)
            {
                pnlPassword.Visible = false;
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
        private void LlenaGridUsuarios(string cvusuario,int idtrab, string nombre, string pass,int stusuario,string usuumod,string prgmod, int opcion)
        {
            if (dgvUsuario.Columns.Count > 1)
            {
                dgvUsuario.Columns.RemoveAt(0);
            }

            DataTable dtFormasRegistro = usuario.ObtenerListaUsuarios(cvusuario, idtrab, nombre, pass, stusuario, usuumod, prgmod, opcion);
            dgvUsuario.DataSource = dtFormasRegistro;
            
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvUsuario.Columns.Insert(0, imgCheckProcesos);
            dgvUsuario.Columns[0].HeaderText = "Seleccionar";
            dgvUsuario.Columns[1].HeaderText = "Clave Usuario";
            dgvUsuario.Columns[3].HeaderText = "Nombre";
            //dgvUsuario.Columns[4].HeaderText = "Estatus";

            //dgvUsuario.Columns[1].Visible = true;
            dgvUsuario.Columns[2].Visible = false;
            dgvUsuario.Columns[4].Visible = false;
            dgvUsuario.Columns[5].Visible = false;
            dgvUsuario.ClearSelection();
        }

        private void LlenaGridProcesos(string cvproceso, string descripcion, int stproceso, string usuumod, string prgumod, int opcion)
        {

            if (dgvProceso.Columns.Count > 1)
            {
                dgvProceso.Columns.RemoveAt(0);
            }
            DataTable dtProcesos = procesos.ObtenerProceso(cvproceso, descripcion, stproceso, usuumod, prgumod, opcion);
            dgvProceso.DataSource = dtProcesos;
            
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvProceso.Columns.Insert(0, imgCheckProcesos);
            dgvProceso.Columns[0].HeaderText = "Seleccionar";
            dgvProceso.Columns[1].HeaderText = "Clave Proceso";
            dgvProceso.Columns[3].Visible = false;
            dgvProceso.ClearSelection();
        }

        private void AsignarProcesos()
        {

            Proceso objProceso = new Proceso();
            ltUsuarioxProceso = objProceso.obtenerUsuariosxProceso(CVUsuario,"","","","",5);

            for (int iContador = 0; iContador < dgvProceso.Rows.Count; iContador++)
            {
                string cvproce = dgvProceso.Rows[iContador].Cells[1].Value.ToString();

                if (ltUsuarioxProceso.Contains(cvproce))
                {
                    dgvProceso.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvProceso.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvProceso.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvProceso.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }

        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
