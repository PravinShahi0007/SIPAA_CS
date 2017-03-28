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
        public int idubicacion;
        

        public List<int> ltUbicaciones = new List<int>();
        public List<int> ltUbicacionesxUsuario = new List<int>();
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
            //for (int iContador = 0; iContador < dgvUbicaciones.Rows.Count; iContador++)
            //{
            //    dgvUbicaciones.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            //}

            if (cvusuario != null)
            {   
                if (dgvUbicaciones.SelectedRows.Count != 0)
                {
                    
                    DataGridViewRow row = this.dgvUbicaciones.SelectedRows[0];
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    idubicacion = Convert.ToInt32(row.Cells["idubicacion"].Value.ToString());

                    panelPermisos.Enabled = true;

                    ltUbicaciones.Add(idubicacion);


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
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a un Usuario");
                timer1.Start();
            }

}
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            cvusuario = null;
            string IdTrab = txtIdTrab.Text;
            string nombre = txtUsuario.Text;
            dgvUsuarios.Columns.Remove(columnName: "Seleccionar");

            llenarGridUsuarios(IdTrab.Trim(), 0, nombre.Trim(), "", 0, "", "", 7);

            txtUsuario.Text = "";
            txtIdTrab.Text = "";
        }

        private void btnBuscarUbicaciones_Click(object sender, EventArgs e)
        {   
            string ubi = txtUbicacion.Text;
            dgvUbicaciones.Columns.Remove(columnName: "Seleccionar");
            llenarGridUbicaciones(ubi.Trim());
            txtUbicacion.Text = "";
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
        private void UbicacionesUsuario_Load(object sender, EventArgs e)
        {
            llenarGridUsuarios("", 0, "", "", 0, "", "", 7);
            llenarGridUbicaciones("");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        private void llenarGridUsuarios(string cvusuario, int idtrab, string nombre, string pass, int stusuario, string usuumod, string prgmod, int opcion)
        {

            DataTable dtFormasRegistro = usuario.ObtenerListaUsuarios(cvusuario, idtrab, nombre, pass, stusuario, usuumod, prgmod, opcion);
            dgvUsuarios.DataSource = dtFormasRegistro;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvUsuarios.Columns.Insert(0, imgCheckProcesos);
            dgvUsuarios.Columns[0].HeaderText = "Seleccionar";
            dgvUsuarios.Columns[3].Visible = false;
            dgvUsuarios.ClearSelection();
        }
        private void llenarGridUbicaciones(string Descripcion)
        {
            SonaCompania objCia = new SonaCompania();
            DataTable dtUbicacion = objCia.ObtenerUbicacionPlantel(Descripcion);
            dgvUbicaciones.DataSource = dtUbicacion;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvUbicaciones.Columns.Insert(0, imgCheckProcesos);
            dgvUbicaciones.Columns[0].HeaderText = "Seleccionar";
            dgvUbicaciones.Columns[1].Visible = false;
            dgvUbicaciones.ClearSelection();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            try
            {
                string usuumod = "vjiturburuv";
                string prgmod = this.Name;
                Usuario objUsuario = new Usuario();

                foreach (int id in ltUbicaciones)
                {
                    objUsuario.AsignarUbicacionUsuario(cvusuario, id, usuumod, prgmod, 1);
                }

                ltUbicaciones.Clear();

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                timer1.Start();
                AsignarUbicaciones();


            }
            catch (Exception ex)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                MessageBox.Show("" + ex);
                AsignarUbicaciones();
            }
        }

        private void AsignarUbicaciones()
        {

            UbicacionUsuario objUbicacionesUsuarios = new UbicacionUsuario();
            ltUbicacionesxUsuario = objUbicacionesUsuarios.ObtenerUbicacionesxUsuario(cvusuario,0,"","",4);

            //Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);


            for (int iContador = 0; iContador < dgvUbicaciones.Rows.Count; iContador++)
            {
                int idubicacion = Convert.ToInt32(dgvUbicaciones.Rows[iContador].Cells[1].Value.ToString());

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
