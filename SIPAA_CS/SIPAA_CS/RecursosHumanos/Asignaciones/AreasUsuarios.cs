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
    public partial class AreasUsuarios : Form
    {
        public string cvusuario = "";
        public string nombre;

        public int idplanta;
        public int idcompania;
        public List<int> ltArea = new List<int>();
        public List<int> ltAreaxUsuario = new List<int>();

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
            int compania = cbCompania.SelectedIndex;

            string comp = cbCompania.SelectedItem.ToString();

            if (ltArea.Count == 0)
            {

                if (compania == 0)
                {
                    LlenarGridPlanteles(cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
                    AsignarPlantel();
                }
                else
                {
                   
                    LlenarGridPlanteles(cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
                    
                    AsignarPlantel();
                }
            }
            else {

                DialogResult result = MessageBox.Show("Se perderan tus asignaciones,¿Seguro que deseas cambiar de Compañia?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    LlenarGridPlanteles(cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
                    
                    AsignarPlantel();
                }
                else if (result == DialogResult.No)
                {
                    cbCompania.SelectedItem = comp;
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
            if (cvusuario != null)
            {
                if (cbCompania.SelectedIndex != 0)
                {
                    if (dgvPlantel.SelectedRows.Count != 0)
                    {
                        DataGridViewRow row = this.dgvPlantel.SelectedRows[0];
                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        idplanta = Convert.ToInt32(row.Cells[3].Value.ToString());
                        
                        panelPermisos.Enabled = true;

                        ltArea.Add(idplanta);
                        
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
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccionado a una Compañia");
                    timer1.Start();
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbCompania.SelectedIndex != 0)
            {
                int idcompania = cbCompania.SelectedIndex;
                panelPermisos.Enabled = false;
                try
                {
                    string usuumod = "vjiturburuv";
                    string prgmod = this.Name;
                    Usuario objUsuario = new Usuario();

                    foreach (int id in ltArea)
                    {
                        objUsuario.AsignarAreaUsuario(cvusuario, idcompania, id, usuumod, prgmod);
                    }

                    ltArea.Clear();

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                    timer1.Start();
                    llenaCombo();
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

        private void btnBuscarPlantel_Click(object sender, EventArgs e)
        {
            
            LlenarGridPlanteles(cbCompania.SelectedItem.ToString(), txtBuscarPlantel.Text, dgvPlantel);
            txtBuscarPlantel.Text="";
            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Asignacion_Area_Usuario_Load(object sender, EventArgs e)
        {
            cbCompania.Enabled = false;
            llenarGridUsuarios("", 0, "", "", 0, "", "", 7);
            
            llenaCombo();
            LlenarGridPlanteles("", "", dgvPlantel);
            
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
            DataTable dtCia = objCia.obtcomp(1, "");


            List<string> ltCia = new List<string>();

            ltCia.Insert(0, "Selecciona una Compañia");
            foreach (DataRow row in dtCia.Rows)
            {
                ltCia.Add(row["Descripción"].ToString());
            }

            cbCompania.DataSource = ltCia;
        }

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

        public void LlenarGridPlanteles(string idCia, string busqueda, DataGridView dgvPlantel)
        {

            if (dgvPlantel.Columns.Count >1)
            {
                dgvPlantel.Columns.RemoveAt(0);
            }
            SonaCompania objCia = new SonaCompania();
            DataTable dtPlantel = objCia.ObtenerPlantelxCompania(idCia, busqueda);

            dgvPlantel.DataSource = dtPlantel;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvPlantel.Columns.Insert(0, imgCheckProcesos);
            dgvPlantel.Columns[0].HeaderText = "Seleccionar";
            
            dgvPlantel.Columns[1].Visible = false;
            dgvPlantel.Columns[2].Visible = false;
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
            
            dgvPlantel.ClearSelection();
        }

        private void AsignarPlantel()
        {
            idcompania = cbCompania.SelectedIndex;
            AreaUsuario objAreasUsu = new AreaUsuario();
            ltAreaxUsuario = objAreasUsu.ObtenerAreaxUsuario(idcompania);
            for (int iContador = 0; iContador < dgvPlantel.Rows.Count; iContador++)
            {
                int idplanta = Convert.ToInt32(dgvPlantel.Rows[iContador].Cells[3].Value.ToString());

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
        
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
