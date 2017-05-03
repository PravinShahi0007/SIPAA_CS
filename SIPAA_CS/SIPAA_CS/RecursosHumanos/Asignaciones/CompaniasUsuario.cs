using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos
{
    public partial class CompaniasUsuario : Form
    {

        public string CVUsuario;

        public string cvusuario;
   
        public string nombre;
        public string passw;
        public string pass;
        public int idtrab;
        public int status;
        public string usumod;
        public string prgmod;
        public int response;
        public string buscar;
        public string stusuario;
        public string idcompania;

        public List<string> ltCompanias = new List<string>();
        public List<string> ltCompaniasxUsuario = new List<string>();
        Usuario usuario = new Usuario();
        SonaCompania companias = new SonaCompania();
        public CompaniasUsuario()
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
        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvUsuarios.Rows.Count; iContador++)
            {
                dgvUsuarios.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvUsuarios.SelectedRows.Count != 0)
            {
               
                DataGridViewRow row = this.dgvUsuarios.SelectedRows[0];
                cvusuario = row.Cells["cvusuario"].Value.ToString();
                nombre = row.Cells["NOMBRE"].Value.ToString();
                

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                
                AsignarCompanias();

            }
        }
        private void dgvCompanias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (cvusuario != null)
            {

                if (dgvCompanias.SelectedRows.Count != 0)
                {
                    Utilerias.MultiSeleccionGridViewString(dgvCompanias, 1, ltCompanias, panelPermisos);

                    //DataGridViewRow row = this.dgvCompanias.SelectedRows[0];
                    //row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    //idcompania = row.Cells[1].Value.ToString();

                    //panelPermisos.Enabled = true;

                    //ltCompanias.Add(idcompania);

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

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a un Usuario");
                timer1.Start();
            }

        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            cvusuario = null;
            string cvUsuario = txtCvUsuario.Text;
            //dgvUsuarios.Columns.Remove(columnName: "Seleccionar");

            if (cvUsuario != "")
            {
                LlenaGridUsuarios(cvUsuario.Trim(), 0, "", "", 0, "", "", 8);
                llenarGridCompanias(9, "%");
            }
            else
            {
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
                llenarGridCompanias(9, "%");
            }

            txtCvUsuario.Text = "";

        }
        private void btnBuscarCompanias_Click(object sender, EventArgs e)
        {
            string companias = txtCompanias.Text;
            dgvCompanias.Columns.Remove(columnName: "Seleccionar");
            //llenarGridCompanias(1, companias.Trim());
            txtCompanias.Text = "";

            if (companias != "")
            {
                llenarGridCompanias(9, companias);
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            }
            else
            {
                llenarGridCompanias(9, "%");
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            AsignarCompanias();
            if (Utilerias.SinAsignacionesString(dgvCompanias, 0, 1, ltCompanias) == true)
            {
                try
                {
                    string usuumod = "vjiturburuv";
                    string prgmod = this.Name;
                    Usuario objUsuario = new Usuario();

                    foreach (string id in ltCompanias)
                    {
                        objUsuario.AsignarCompaniaUsuario(cvusuario, id, usuumod, prgmod, 1);
                    }

                    ltCompanias.Clear();
                   
                   
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                    timer1.Start();
                    //AsignarCompanias();
                    LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
                    llenarGridCompanias(9, "%");


                }
                catch (Exception ex)
                {
                    //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                    timer1.Start();
                    MessageBox.Show("" + ex);
                    AsignarCompanias();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Seguro que desea quitar todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string usuumod = "vjiturburuv";
                        string prgmod = this.Name;
                        Usuario objUsuario = new Usuario();

                        foreach (string id in ltCompanias)
                        {
                            objUsuario.AsignarCompaniaUsuario(cvusuario, id, usuumod, prgmod, 1);
                        }

                        ltCompanias.Clear();
                        llenarGridCompanias(9, "%");
                        LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                        timer1.Start();
                        AsignarCompanias();


                    }
                    catch (Exception ex)
                    {
                        //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                        timer1.Start();
                        MessageBox.Show("" + ex);
                        AsignarCompanias();
                    }
                }
                else
                {
                    AsignarCompanias();
                    ltCompanias.Clear();
                }
            }
            
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Asignacion_Companias_Usuario_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            llenarGridCompanias(8,"");
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
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

        private void llenarGridCompanias(int popc, string pbusq)
        {
            if (dgvCompanias.Columns.Count > 1)
            {
                dgvCompanias.Columns.RemoveAt(0);
            }

            DataTable dtcompanias = companias.obtcomp(popc, pbusq);
            dgvCompanias.DataSource = dtcompanias;
            
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvCompanias.Columns.Insert(0, imgCheckProcesos);

            dgvCompanias.Columns[1].Visible = false;
            dgvCompanias.Columns[0].HeaderText = "Seleccionar";
            dgvCompanias.Columns[2].HeaderText = "Descripción";
            dgvCompanias.ClearSelection();
       
        }
        private void AsignarCompanias()
        {
            CompaniaUsuario objCompanias = new CompaniaUsuario();
            ltCompaniasxUsuario = objCompanias.ObtenerCompaniasxUsuario(cvusuario,"","","",5);

            //Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);
            
            for (int iContador = 0; iContador < dgvCompanias.Rows.Count; iContador++)
            {
                string idcompanias = dgvCompanias.Rows[iContador].Cells[1].Value.ToString();

                if (ltCompaniasxUsuario.Contains(idcompanias))
                {
                    dgvCompanias.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvCompanias.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvCompanias.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvCompanias.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }

        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


    }
}
