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
    public partial class DepartamentosUsuario : Form
    {
        public string cvusuario;
        public string nombre;
        public string iddepto;

        public List<string> ltDepartamentos = new List<string>();
        public List<string> ltDepartamentosxUsuario = new List<string>();

        Usuario usuario = new Usuario();
        SonaDepartamento departamento = new SonaDepartamento();
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************
        public DepartamentosUsuario()
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
                //nombre = row.Cells["nombre"].Value.ToString();


                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                AsignarDepartamentos();

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

        private void btnBuscarDepartamentos_Click(object sender, EventArgs e)
        {
            string depto = txtDepartamento.Text;
            panelPermisos.Enabled = false;
            panelTag.Enabled = true;
            dgvDepartamentos.Columns.Remove(columnName: "Seleccionar");
            llenarGridDepartamentos(1, depto.Trim());
            txtDepartamento.Text = "";
            
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            try
            {
                string usuumod = "vjiturburuv";
                string prgmod = this.Name;
                Usuario objUsuario = new Usuario();

                foreach (string id in ltDepartamentos)
                {
                    objUsuario.AsignarDepartamentosUsuario(cvusuario, id, usuumod, prgmod, 1);
                }

                ltDepartamentos.Clear();

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                timer1.Start();
                AsignarDepartamentos();


            }
            catch (Exception ex)
            {
                //Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                MessageBox.Show("" + ex);
                AsignarDepartamentos();
            }
        }
        private void dgvDepartamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //for (int iContador = 0; iContador < dgvDepartamentos.Rows.Count; iContador++)
            //{
            //    dgvDepartamentos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            //}

            if (cvusuario != null)
            {
                if (dgvDepartamentos.SelectedRows.Count != 0)
                {

                    DataGridViewRow row = this.dgvDepartamentos.SelectedRows[0];
                    iddepto = row.Cells["Clave"].Value.ToString();
                    panelPermisos.Enabled = true;
                    ltDepartamentos.Add(iddepto);


                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    //AsignarUbicaciones();

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
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void DepartamentosUsuario_Load(object sender, EventArgs e)
        {
            llenarGridUsuarios("", 0, "", "", 0, "", "", 7);
            llenarGridDepartamentos(1, "");
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
        
        private void llenarGridDepartamentos(int popc, string pbusq)
        {

            DataTable dtdepartamentos = departamento.obtdepto(popc, pbusq);
            dgvDepartamentos.DataSource = dtdepartamentos;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvDepartamentos.Columns.Insert(0, imgCheckProcesos);
            dgvDepartamentos.Columns[0].HeaderText = "Seleccionar";
            //dgvDepartamentos.Columns[1].Visible = false;
            dgvDepartamentos.ClearSelection();
        }
        private void AsignarDepartamentos()
        {

            DepartamentoUsuario objDepartamentosUsuarios = new DepartamentoUsuario();
            ltDepartamentosxUsuario = objDepartamentosUsuarios.ObtenerDepartamentosxUsuario(cvusuario,"","","",4);

            //Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);


            for (int iContador = 0; iContador < dgvDepartamentos.Rows.Count; iContador++)
            {
                string idubicacion = dgvDepartamentos.Rows[iContador].Cells[1].Value.ToString();

                if (ltDepartamentosxUsuario.Contains(idubicacion))
                {
                    dgvDepartamentos.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvDepartamentos.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvDepartamentos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvDepartamentos.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }

        }
        
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------



    }
}
