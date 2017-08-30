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
    public partial class UbicacionesUsuario : Form
    {
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public string cvusuario;
        public string nombre;
        public string ubicacion;
        public string idubicacion;
        

        public List<string> ltUbicaciones = new List<string>();
        public List<string> ltUbicacionesxUsuario = new List<string>();
        Usuario usuario = new Usuario();
        public UbicacionesUsuario()
        {
            InitializeComponent();
        }
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
                AsignarUbicaciones();
            }
        }

        private void dgvUbicaciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Actualizar"] == 1)
            {
                if (cvusuario != null)
                {   
                    if (dgvUbicaciones.SelectedRows.Count != 0)
                    {
                        Utilerias.MultiSeleccionGridViewString(dgvUbicaciones, 1, ltUbicaciones, panelPermisos);
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
            string cvUsuario = txtCvUsuario.Text;
            if (cvUsuario != "")
            {
                LlenaGridUsuarios(cvUsuario.Trim(), 0, "", "", 0, "", "", 8);
                llenarGridUbicaciones(6, "");
            }
            else
            {
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
                llenarGridUbicaciones(6, "");
            }

            txtCvUsuario.Text = "";
        }

        private void btnBuscarUbicaciones_Click(object sender, EventArgs e)
        {   
            string ubi = txtUbicacion.Text;
            panelPermisos.Enabled = false;
            //dgvUbicaciones.Columns.Remove(columnName: "Seleccionar");
           
            if (ubi.Trim() != "")
            {
                llenarGridUbicaciones(6, ubi.Trim());
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            }
            else
            {
                llenarGridUbicaciones(6, ubi.Trim());
                LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            }
            txtUbicacion.Text = "";
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

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            AsignarUbicaciones();

            if (Utilerias.SinAsignacionesString(dgvUbicaciones, 0, 1, ltUbicaciones) == true)
            {
                try
                {
                    string usuumod = LoginInfo.IdTrab;
                    string prgmod = this.Name;
                    Usuario objUsuario = new Usuario();

                    foreach (string id in ltUbicaciones)
                    {
                        objUsuario.AsignarUbicacionUsuario(cvusuario, id, usuumod, prgmod, 1);
                    }

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                    timer1.Start();
                    ltUbicaciones.Clear();
                    AsignarUbicaciones();


                }
                catch (Exception ex)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                    timer1.Start();
                    MessageBox.Show("" + ex);
                    AsignarUbicaciones();
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

                        foreach (string id in ltUbicaciones)
                        {
                            objUsuario.AsignarUbicacionUsuario(cvusuario, id, usuumod, prgmod, 1);
                        }

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                        timer1.Start();
                        ltUbicaciones.Clear();
                        AsignarUbicaciones();


                    }
                    catch (Exception ex)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                        timer1.Start();
                        MessageBox.Show("" + ex);
                        AsignarUbicaciones();
                    }
                }
                else
                {
                    AsignarUbicaciones();
                    ltUbicaciones.Clear();
                }
            }
           
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void UbicacionesUsuario_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Companias.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            LlenaGridUsuarios("%", 0, "", "", 0, "", "", 8);
            llenarGridUbicaciones(6,"");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
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
        private void llenarGridUbicaciones(int opcion,string Descripcion)
        {
            if (dgvUbicaciones.Columns.Count > 1)
            {
                dgvUbicaciones.Columns.RemoveAt(0);
            }

            SonaCompania objCia = new SonaCompania();
            DataTable dtUbicacion = objCia.ObtenerUbicacionPlantel(6,Descripcion);
            dgvUbicaciones.DataSource = dtUbicacion;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvUbicaciones.Columns.Insert(0, imgCheckProcesos);
            dgvUbicaciones.Columns[0].HeaderText = "Seleccionar";
            dgvUbicaciones.Columns[1].Visible = false;
            dgvUbicaciones.ClearSelection();
        }

        

        private void AsignarUbicaciones()
        {

            UbicacionUsuario objUbicacionesUsuarios = new UbicacionUsuario();
            ltUbicacionesxUsuario = objUbicacionesUsuarios.ObtenerUbicacionesxUsuario(cvusuario,"","","",5);

            //Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);


            for (int iContador = 0; iContador < dgvUbicaciones.Rows.Count; iContador++)
            {
                string idubicacion = dgvUbicaciones.Rows[iContador].Cells[1].Value.ToString();

                if (ltUbicacionesxUsuario.Contains(idubicacion))
                {
                    dgvUbicaciones.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvUbicaciones.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvUbicaciones.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvUbicaciones.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }

        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
        
    }
}
