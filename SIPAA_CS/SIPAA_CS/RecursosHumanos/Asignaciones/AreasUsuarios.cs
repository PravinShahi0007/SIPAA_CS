using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.Accesos.Asignaciones;
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

namespace SIPAA_CS.RecursosHumanos.Asignaciones
{
    public partial class AreasUsuarios : Form
    {
        public string cvusuario = "";
        public string nombre;
        public string temporal;

        public string idplanta;
        public string idcompania;
        public List<string> ltArea = new List<string>();
        public List<string> ltAreaxUsuario = new List<string>();

        Usuario usuario = new Usuario();
        Utilerias utilerias = new Utilerias();
        public AreasUsuarios()
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
        private void cbCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string comp1;
            int compania = cbCompania.SelectedIndex;

            

            if (ltArea.Count == 0)
            {
                if (compania == 0)
                {
                    LlenarGridPlanteles("",cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
                    AsignarPlantel();
                }
                else
                {

                    LlenarGridPlanteles("", cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
                    AsignarPlantel();
                }
            }
            else {
                
                DialogResult result = MessageBox.Show("Se perderan tus asignaciones,¿Seguro que deseas cambiar de Compañia?", "SIPAA", MessageBoxButtons.YesNo);
                

                if (result == DialogResult.Yes)
                {
                    LlenarGridPlanteles(cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text,"", dgvPlantel);
                    
                    AsignarPlantel();
                    ltArea.Clear();
                }
                else if (result == DialogResult.No)
                {
                    ltArea.Clear();
                    cbCompania.SelectedIndex = cbCompania.FindStringExact(temporal);
                    //LlenarGridPlanteles(cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
                    AsignarPlantel();
                    
                }
                
            }


        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvUsuarios.Rows.Count; iContador++)
            {
                dgvUsuarios.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvUsuarios.SelectedRows.Count != 0)
            {
                cbCompania.Enabled = true;
                DataGridViewRow row = this.dgvUsuarios.SelectedRows[0];
                cvusuario = row.Cells["cvusuario"].Value.ToString();
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                AsignarPlantel();
            }
        }

        private void dgvPlantel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Actualizar"] == 1)
            {


                if (cvusuario != null)
                {
                    if (cbCompania.SelectedIndex != 0 && dgvUsuarios.SelectedRows.Count != 0)
                    {
                        if (dgvPlantel.SelectedRows.Count != 0)
                        {
                            //AsignarPlantel();
                            Utilerias.MultiSeleccionGridViewString(dgvPlantel, 3, ltArea, panelPermisos);

                            //DataGridViewRow row = this.dgvPlantel.SelectedRows[0];
                            //row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                            //idplanta = row.Cells[3].Value.ToString();

                            //panelPermisos.Enabled = true;

                            //ltArea.Add(idplanta);

                            //if (cbCompania.SelectedIndex > 0)
                            //{
                            //    temporal = cbCompania.SelectedValue.ToString();
                            //}

                            //if (ltArea.Count == 0)
                            //{ panelPermisos.Enabled = false; }
                            //else
                            //{ panelPermisos.Enabled = true; }

                            //if (row.Cells[0].Tag.ToString() == "check")
                            //{

                            //    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                            //    row.Cells[0].Tag = "uncheck";

                            //}
                            //else
                            //{
                            //    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                            //    row.Cells[0].Tag = "check";

                            //}
                        }
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a un Usuario y Compañia");
                        timer1.Start();
                    }

                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a un Usuario");
                    timer1.Start();

                }
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            //cvusuario = null;
            cbCompania.Enabled = false;
            
            string clave = txtIdTrab.Text;
            
           
            if (txtIdTrab.Text != "")
            {
                //llenaCombo();
                LlenaGridUsuarios(clave.Trim(), 0, "", "", 0, "", "", 8);
                //llenaCombo();
                LlenarGridPlanteles("", "", "", dgvPlantel);
                //llenaCombo();
            }
            else
            {
                llenaCombo();
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
                LlenarGridPlanteles("", "", "", dgvPlantel);
                
            }
            
            txtIdTrab.Text = "";
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AsignarPlantel();
            
            if (Utilerias.SinAsignacionesString(dgvPlantel, 0, 3, ltArea) == true)
            {
                //MessageBox.Show("true");
                if (cbCompania.SelectedIndex != 0)
                {
                    int idcompania = cbCompania.SelectedIndex;
                    string idcom = Convert.ToString(idcompania);
                    //panelPermisos.Enabled = false;
                    try
                    {
                        string usuumod = LoginInfo.IdTrab;
                        string prgmod = this.Name;
                        Usuario objUsuario = new Usuario();

                        foreach (string id in ltArea)
                        {
                            objUsuario.AsignarAreaUsuario(cvusuario, idcom, id, usuumod, prgmod, 1);
                        }

                        ltArea.Clear();

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                        timer1.Start();
                        llenaCombo();
                        cbCompania.Enabled = false;
                        panelPermisos.Enabled = false;
                        LlenarGridPlanteles("", "", "", dgvPlantel);
                        LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);

                    }
                    catch (Exception ex)
                    {
                        timer1.Start();
                        MessageBox.Show("" + ex);
                    }
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a una Compañia");
                    timer1.Start();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea quitar todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    if (cbCompania.SelectedIndex != 0)
                    {
                        int idcompania = cbCompania.SelectedIndex;
                        string idcom = Convert.ToString(idcompania);
                        //panelPermisos.Enabled = false;
                        try
                        {
                            string usuumod = LoginInfo.IdTrab;
                            string prgmod = this.Name;
                            Usuario objUsuario = new Usuario();

                            foreach (string id in ltArea)
                            {
                                objUsuario.AsignarAreaUsuario(cvusuario, idcom, id, usuumod, prgmod, 1);
                            }

                            ltArea.Clear();

                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                            timer1.Start();
                            llenaCombo();
                            cbCompania.Enabled = false;
                            panelPermisos.Enabled = false;
                            LlenarGridPlanteles("", "", "", dgvPlantel);
                            LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);

                        }
                        catch (Exception ex)
                        {
                            timer1.Start();
                            MessageBox.Show("" + ex);
                        }
                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a una Compañia");
                        timer1.Start();
                    }
                }
                else
                {

                    
                }
            }



            
                
        }

        private void btnBuscarPlantel_Click(object sender, EventArgs e)
        {
            cbCompania.Enabled = false;
            string plantel = txtBuscarPlantel.Text;
            if (plantel != "")
            {
                llenaCombo();
                LlenarGridPlanteles("", "", plantel, dgvPlantel);
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            }
            else
            {
                llenaCombo();
                LlenarGridPlanteles("", "", plantel, dgvPlantel);
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            }
            
            txtBuscarPlantel.Text="";
            
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
        private void Asignacion_Area_Usuario_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            cbCompania.Enabled = false;
            LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);

            llenaCombo();
            LlenarGridPlanteles("","", "", dgvPlantel);
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        public void llenaCombo()
        {
            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(5, "");


            List<string> ltCia = new List<string>();

            ltCia.Insert(0, "Selecciona una Compañia");
            foreach (DataRow row in dtCia.Rows)
            {
                ltCia.Add(row["Descripción"].ToString());
            }

            cbCompania.DataSource = ltCia;
        }

        private void LlenaGridUsuarios(string cvusuario, int idtrab, string nombre, string pass, int stusuario, string usuumod, string prgmod, int opcion)
        {
            if (dgvUsuarios.Columns.Count > 1)
            {
                dgvUsuarios.Columns.RemoveAt(0);
            }
            DataTable dtFormasRegistro = usuario.ObtenerListaUsuarios(cvusuario, idtrab, nombre, pass, stusuario, usuumod, prgmod, opcion);
            dgvUsuarios.DataSource = dtFormasRegistro;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvUsuarios.Columns.Insert(0, imgCheckProcesos);
            dgvUsuarios.Columns[0].HeaderText = "Seleccionar";
            dgvUsuarios.Columns[1].HeaderText = "Clave Usuario";
            dgvUsuarios.Columns[3].HeaderText = "Nombre";
            dgvUsuarios.Columns[2].Visible = false;
            dgvUsuarios.Columns[4].Visible = false;
            dgvUsuarios.ClearSelection();
        }

        public void LlenarGridPlanteles(string idcompania, string descripcion, string planta, DataGridView dgvPlantel)
        {

            if (dgvPlantel.Columns.Count >1)
            {
                dgvPlantel.Columns.RemoveAt(0);
            }
            SonaCompania objCia = new SonaCompania();
            DataTable dtPlantel = objCia.ObtenerPlantelxCompania(6,"", descripcion,planta);

            dgvPlantel.DataSource = dtPlantel;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvPlantel.Columns.Insert(0, imgCheckProcesos);
            dgvPlantel.Columns[0].HeaderText = "Seleccionar";

            dgvPlantel.Columns[1].Visible = false;
            dgvPlantel.Columns[2].Visible = false;
            dgvPlantel.Columns[3].Visible = false;
            dgvPlantel.ClearSelection();
            
        }

        public void LlenarGridAreas(string cvusuario, int compania, DataGridView dgvPlantel)
        {

            if (dgvPlantel.Columns.Count > 1)
            {
                dgvPlantel.Columns.RemoveAt(0);
            }
            AreaUsuario objAreaUsuario = new AreaUsuario();
            DataTable dtArea = objAreaUsuario.ObtenerAreaUsuario(compania);

            dgvPlantel.DataSource = dtArea;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvPlantel.Columns.Insert(0, imgCheckProcesos);
            dgvPlantel.Columns[0].HeaderText = "Seleccionar";
            dgvPlantel.Columns["IdPlanta"].Visible = true;
            dgvPlantel.ClearSelection();
        }

        private void AsignarPlantel()
        {
            //idcompania = cbCompania.SelectedIndex;
            idcompania = cbCompania.SelectedIndex.ToString();
            AreaUsuario objAreasUsu = new AreaUsuario();
            ltAreaxUsuario = objAreasUsu.ObtenerAreaxUsuario(cvusuario, idcompania, "","","",6);
            for (int iContador = 0; iContador < dgvPlantel.Rows.Count; iContador++)
            {
                string idplanta = dgvPlantel.Rows[iContador].Cells[3].Value.ToString();

                if (ltAreaxUsuario.Contains(idplanta))
                {
                    dgvPlantel.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvPlantel.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvPlantel.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvPlantel.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }

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
