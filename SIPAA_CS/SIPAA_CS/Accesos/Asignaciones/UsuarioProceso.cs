using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using SIPAA_CS.Recursos_Humanos.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Accesos
{
    public partial class Asignar_Proceso : Form
    {
        Usuario usuario = new Usuario();
        Proceso procesos = new Proceso();
        Utilerias utilerias = new Utilerias();
        //public int CVPerfil = 0;

        public string CVUsuario;
        public int CvProceso;

        public string buscar;
        public string descripcion;
        public string pass;

        public Asignar_Proceso()
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

                

                List<string> ltUsuarioxProceso= procesos.obtenerUsuariosxProceso(CVUsuario);

                for (int iContador = 0; iContador < dgvProceso.Rows.Count; iContador++)
                {
                    string CvProceso = dgvProceso.Rows[iContador].Cells[1].Value.ToString();

                    if (ltUsuarioxProceso.Contains(CvProceso))
                    {
                        dgvProceso.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    }
                    else
                    {
                        dgvProceso.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    }
                }

            }
        }

        private void dgvProceso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pass = txtPassword.Text;


            if (CVUsuario != "")
            {
                
                if (dgvProceso.SelectedRows.Count != 0)
                {
                    //check palomeado
                    if (cbAsignaPassword.Checked == true)
                    {
                        //MessageBox.Show("mismo pwd");
                        DataGridViewRow rowusu = this.dgvUsuario.SelectedRows[0];

                        pass = rowusu.Cells[3].Value.ToString();
                        //MessageBox.Show(pass);
                        DataGridViewRow row = this.dgvProceso.SelectedRows[0];
                        
                        CvProceso = Convert.ToInt32(row.Cells[1].Value.ToString());
                        string UsuuMod = "vjiturburuv";
                        string PrguMod = this.Name;


                        try
                        {
                            
                            procesos.AsignarUsuarioProceso(CVUsuario, CvProceso, pass, UsuuMod, PrguMod);

                            dgvUsuario_CellContentClick(sender, e);

                        }
                        catch (Exception ex)
                        {


                            MessageBox.Show("" + ex);
                        }
                    }
                    //checkbox vacio
                    if (cbAsignaPassword.Checked == false)
                    {
                        //MessageBox.Show("Asigna password");
                        if (txtPassword.Text != String.Empty)
                        {
                            //MessageBox.Show("esta lleno el pass");
                            DataGridViewRow row = this.dgvProceso.SelectedRows[0];
                            
                            CvProceso = Convert.ToInt32(row.Cells[1].Value.ToString());
                            string UsuuMod = "vjiturburuv";
                            string PrguMod = this.Name;


                            try
                            {
                                pass = txtPassword.Text;
                                Utilerias u = new Utilerias();
                                string p = u.cifradoMd5(pass);

                                procesos.AsignarUsuarioProceso(CVUsuario, CvProceso, p, UsuuMod, PrguMod);

                                txtPassword.Text = "";

                                dgvUsuario_CellContentClick(sender, e);

                            }
                            catch (Exception ex)
                            {


                                MessageBox.Show("" + ex);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Asigna password");
                        }
                    }


                    
                }

            }
            else
            {

                //panelTag.Visible = true;
                //panelTag.BackColor = ColorTranslator.FromHtml("#29b6f6");
                //lbMensaje.Text = "No se ha Seleccionado a un Usuario";
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            buscar = txtUsuario.Text;
            buscar.Trim();
            
           dgvUsuario.Columns.Remove(columnName: "Seleccionar");
           LlenaGridUsuarios(buscar.Trim(), 0, "", "", 0, "", "", 7);
            
        }
        
        private void btnBuscarProceso_Click(object sender, EventArgs e)
        {
            descripcion = txtDescripcion.Text;

            dgvProceso.Columns.Remove(columnName: "Seleccionar");
            LlenaGridProcesos(0, descripcion, 0, "", "", 6);
           
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Asignar_Proceso_Load(object sender, EventArgs e)
        {
            LlenaGridUsuarios("", 0, "", "", 0, "", "", 8);

            LlenaGridProcesos(0, "", 0, "0", "", 5);

            cbAsignaPassword.Visible = false;

            cbAsignaPassword.Checked = true;
            
        }
        private void cbAsignaPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAsignaPassword.Checked == true)
            {
                pnlPassword.Visible = false;

            }
            else if (cbAsignaPassword.Checked == false)
            {
                pnlPassword.Visible = true;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        private void LlenaGridUsuarios(string cvusuario,int idtrab, string nombre, string pass,int stusuario,string usuumod,string prgmod, int opcion)
        {

            DataTable dtFormasRegistro = usuario.ObtenerListaUsuarios(cvusuario, idtrab, nombre, pass, stusuario, usuumod, prgmod, opcion);
            dgvUsuario.DataSource = dtFormasRegistro;
            
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvUsuario.Columns.Insert(0, imgCheckProcesos);
            dgvUsuario.Columns[0].HeaderText = "Seleccionar";

            //dgvUsuario.Columns[1].Visible = true;
            dgvUsuario.Columns[3].Visible = false;
            dgvUsuario.ClearSelection();
        }

        private void LlenaGridProcesos(int cvproceso, string descripcion, int stproceso, string usuumod, string prgumod, int opcion)
        {
         
            DataTable dtProcesos = procesos.ObtenerProceso(cvproceso, descripcion, stproceso, usuumod, prgumod, opcion);
            dgvProceso.DataSource = dtProcesos;
            
            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvProceso.Columns.Insert(0, imgCheckProcesos);
            dgvProceso.Columns[0].HeaderText = "Seleccionar";
            dgvProceso.Columns[3].Visible = false;
            dgvProceso.ClearSelection();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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
