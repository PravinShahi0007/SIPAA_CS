using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using SIPAA_CS.Recursos_Humanos.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Recursos_Humanos
{
    public partial class Asignacion_Companias_Usuario : Form
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
        public int idcompania;

        public List<int> ltCompanias = new List<int>();
        public List<int> ltCompaniasxUsuario = new List<int>();
        Usuario usuario = new Usuario();
        CompaniasSonarh companias = new CompaniasSonarh();
        public Asignacion_Companias_Usuario()
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

                    DataGridViewRow row = this.dgvCompanias.SelectedRows[0];
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    idcompania = Convert.ToInt32(row.Cells[1].Value.ToString());

                    panelPermisos.Enabled = true;

                    ltCompanias.Add(idcompania);

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
            string IdTrab = txtIdTrab.Text;
            string nombre = txtUsuario.Text;
            dgvUsuarios.Columns.Remove(columnName: "Seleccionar");

            llenarGridUsuarios(IdTrab.Trim(), 0, nombre.Trim(), "", 0, "", "", 7);

            txtUsuario.Text = "";
            txtIdTrab.Text = "";

        }
        private void btnBuscarCompanias_Click(object sender, EventArgs e)
        {
            string companias = txtCompanias.Text;
            dgvCompanias.Columns.Remove(columnName: "Seleccionar");
            llenarGridCompanias(1, companias.Trim());
            txtCompanias.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            try
            {
                string usuumod = "vjiturburuv";
                string prgmod = this.Name;
                Usuario objUsuario = new Usuario();

                foreach (int id in ltCompanias)
                {
                    objUsuario.AsignarCompaniaUsuario(cvusuario, id, usuumod, prgmod);
                }

                ltCompanias.Clear();

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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Asignacion_Companias_Usuario_Load(object sender, EventArgs e)
        {
            llenarGridUsuarios("", 0, "", "", 0, "", "", 7);
            llenarGridCompanias(1,"");
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

        private void llenarGridCompanias(int popc, string pbusq)
        {
           
            DataTable dtcompanias = companias.obtcomp(popc, pbusq);
            dgvCompanias.DataSource = dtcompanias;
            
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvCompanias.Columns.Insert(0, imgCheckProcesos);

            dgvCompanias.Columns[1].Visible = false;
            dgvCompanias.Columns[3].Visible = false;
            dgvCompanias.Columns[0].HeaderText = "Seleccionar";
            dgvCompanias.Columns[0].Width = 100;
            dgvCompanias.ClearSelection();
       
        }
        private void AsignarCompanias()
        {

            Companias objCompanias = new Companias();
            ltCompaniasxUsuario = objCompanias.ObtenerCompaniasxUsuario(cvusuario);

            //Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);


            for (int iContador = 0; iContador < dgvCompanias.Rows.Count; iContador++)
            {
                int idcompanias = Convert.ToInt32(dgvCompanias.Rows[iContador].Cells[1].Value.ToString());

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
