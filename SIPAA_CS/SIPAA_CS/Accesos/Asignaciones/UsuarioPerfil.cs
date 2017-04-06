using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.Accesos
{

    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017      Última Modificacion: 13-03-2017
    //Descripción: Pantalla de Asignación de Módulos a Perfiles
    //***********************************************************************************************
    public partial class UsuarioPerfil : Form
    {

        public Point formPosition;
        public Boolean mouseAction;
        public string CVUsuario;
        public int CvPerfil;
        public List<int> ltPerfiles = new List<int>();
        public List<int> ltPerfilesxUsuario = new List<int>();
        public List<string> ltPermisos = new List<string>();
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
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
                dgvUsuarios.Rows[iContador].Cells[3].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvUsuarios.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvUsuarios.SelectedRows[0];

                CVUsuario = row.Cells["CvUsuario"].Value.ToString();
                int IdTrab = Convert.ToInt32(row.Cells["IdTrab"].Value.ToString());
                string ValorRow = row.Cells["Nombre"].Value.ToString();

                row.Cells[3].Value = Resources.ic_check_circle_green_400_18dp;

              
                AsignarPerfiles();
            }
        }


        private void AsignarPerfiles() {

            Perfil objPerfil = new Perfil();
            ltPerfilesxUsuario = objPerfil.ObtenerPerfilesxUsuario(CVUsuario,0,4);

           // Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);

           
                for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
            {
                int IdPerfil = Convert.ToInt32(dgvPerfiles.Rows[iContador].Cells[1].Value.ToString());

                if (ltPerfilesxUsuario.Contains(IdPerfil))
                {
                    dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvPerfiles.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvPerfiles.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }

        }
        private void dgvPerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CVUsuario != null)
            {

                if (dgvPerfiles.SelectedRows.Count != 0)
                {
                    panelPermisos.Enabled = true;
                    DataGridViewRow row = this.dgvPerfiles.SelectedRows[0];

                    int iCVPerfil = Convert.ToInt32(row.Cells[1].Value.ToString());

                    ltPerfiles.Add(iCVPerfil);
                    if (row.Cells[0].Tag.ToString() == "check") {

                        row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                        row.Cells[0].Tag = "uncheck";     

                    } else {
                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        row.Cells[0].Tag = "check";
                        
                    }

                }

            }
            else
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado un Usuario");
                timer1.Start();
            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            try
            {
                string UsuuMod = "vjiturburuv";
                string PrguMod = this.Name;
                Usuario objUsuario = new Usuario();

                foreach (int cv in ltPerfiles)
                {
                    objUsuario.AsignarPerfilaUsuario(CVUsuario, cv, UsuuMod, PrguMod);
                }

                ltPerfiles.Clear();

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                timer1.Start();
                AsignarPerfiles();


            }
            catch (Exception ex)
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                AsignarPerfiles();
            }
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {

            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
           
            panelPermisos.Enabled = false;
            CVUsuario = null;
          

            string strUsuario = "";
            string IdTrab = "";

            if (txtUsuario.Text != String.Empty)
            {
                strUsuario = txtUsuario.Text.Trim();
            }
            else
            {
                strUsuario = "%";
            }


            if (txtIdTrab.Text != String.Empty)
            {
                IdTrab = txtIdTrab.Text;
            }
            else
            {
                IdTrab = "%";
            }


            llenarGridUsuarios(strUsuario,IdTrab);

            for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
            {
                dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            dgvPerfiles.ClearSelection();
            dgvUsuarios.ClearSelection();


        }

        private void btnBuscarPerfil_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            CVUsuario = null;
            string strPerfil = "";

            if (txtPerfil.Text != String.Empty)
            {

                strPerfil = txtPerfil.Text.Trim();
            }
            else
            {
                strPerfil = "%";
            }

            LlenarGridPerfil("%", strPerfil, 1);

            for (int iContador = 0; iContador < dgvUsuarios.Rows.Count; iContador++)
            {
                dgvUsuarios.Rows[iContador].Cells[3].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            dgvPerfiles.ClearSelection();
            dgvUsuarios.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public UsuarioPerfil()
        {
            InitializeComponent();
        }

        private void Asignar_Perfil_Load(object sender, EventArgs e)
        {


           
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            llenarGridUsuarios("%", "%");
            LlenarGridPerfil("%", "%",1);


            Modulo objModulo = new Modulo();
            string idtrab = LoginInfo.IdTrab;
            DataTable dtPermisos = objModulo.ObtenerPermisosxUsuario(idtrab);
            DataRow[] row = dtPermisos.Select("CVModulo = '" + this.Tag + "'");
            Utilerias.CrearListaPermisoxPantalla(row, ltPermisos);
          //  Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);


        }


     

        private void timer_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();

        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void llenarGridUsuarios(string Nombre, string IdTrab)
        {

            if (dgvUsuarios.Columns.Count > 1)
            {
                dgvUsuarios.Columns.RemoveAt(3);
            }

            Usuario objUsuario = new App_Code.Usuario();
            List<Usuario> ltUsuario = objUsuario.ObtenerUsuariosxBusqueda(Nombre, IdTrab);
            DataTable dtUsuarios = objUsuario.ObtenerDataTableUsuarios(ltUsuario);
            dgvUsuarios.DataSource = dtUsuarios;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "imgUsuarios";
            dgvUsuarios.Columns.Insert(3, imgCheckUsuarios);
            dgvUsuarios.Columns[3].HeaderText = "Seleccionar";
            dgvUsuarios.Columns[0].Visible = false;

            dgvUsuarios.Columns[1].ReadOnly = true;
            dgvUsuarios.Columns[2].Width = 250;
            dgvUsuarios.Columns[3].Width = 50;
  
            dgvUsuarios.ClearSelection();

        }

        private void LlenarGridPerfil(string CVPerfil, string Descripcion, int iEstatus)
        {
            if (dgvPerfiles.Columns.Count > 1) {
                dgvPerfiles.Columns.RemoveAt(0);
            }

            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(CVPerfil, Descripcion, iEstatus.ToString());
            dgvPerfiles.DataSource = dtPerfiles;

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "imgPerfiles";
            dgvPerfiles.Columns.Insert(0, imgCheckPerfiles);
            dgvPerfiles.Columns[0].HeaderText = "Seleccionar";
            ImageList imglt = new ImageList();

            dgvPerfiles.Columns[1].Visible = false;
            dgvPerfiles.Columns[3].Visible = false;
            dgvPerfiles.Columns[4].Visible = false;
            dgvPerfiles.Columns[5].Visible = false;
            dgvPerfiles.Columns[6].Visible = false;

            dgvPerfiles.Columns[0].Width = 70;
            
           
            dgvPerfiles.ClearSelection();

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------



        private void BarraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void BarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void BarraSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
            }
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_2(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
