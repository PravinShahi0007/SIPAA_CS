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

namespace SIPAA_CS.Recursos_Humanos.Administracion
{


    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017      Última Modificacion: 13-03-2017
    //Descripción: Pantalla de Asignación de Módulos a Perfiles
    //***********************************************************************************************
    public partial class Asignar_Modulo : Form
    {
        public int CVPerfil = 0;
        public string CVModulo;
        public int ultimaseleccion = 0;
        public int iOpcionAdmin;


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------


        private void dgvPerfil_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvPerfil.Rows.Count; iContador++)
            {
                dgvPerfil.Rows[iContador].Cells[2].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvPerfil.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvPerfil.SelectedRows[0];

                CVPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                string Desc = row.Cells["DESCRIPCION"].Value.ToString();

                row.Cells[2].Value = Resources.ic_check_circle_green_400_18dp;

                Modulo objModulo = new Modulo();
                List<string> ltPerfilesxUsuario = objModulo.obtenerModulosxPerfil(CVPerfil);

                for (int iContador = 0; iContador < dgvModulos.Rows.Count; iContador++)
                {
                    string cvModulo = dgvModulos.Rows[iContador].Cells[1].Value.ToString();

                  
                        if (ltPerfilesxUsuario.Contains(cvModulo))
                        {
                            dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                        }
                        else
                        {
                            dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                        }
                    

                  
                }


            }
        }

        private void dgvModulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

           
               

            if (CVPerfil != 0)
            {

                dgvModulos.Rows[ultimaseleccion].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;

                dgvPerfil_CellContentClick(sender, e);


                if (dgvModulos.SelectedRows.Count != 0)
                {

                    panelPermisos.Enabled = true;
                    DataGridViewRow row = this.dgvModulos.SelectedRows[0];

                    ultimaseleccion = row.Index;

                    CVModulo = row.Cells[1].Value.ToString();

                    row.Cells[0].Value = Resources.ic_check_circle_blue_grey_600_18dp;

                    Modulo objModulo = new Modulo();
                    objModulo = objModulo.ObtenerPermisosxModulo(CVModulo, CVPerfil);

                    iOpcionAdmin = 1;

                    if (objModulo.CVModulo == "0")
                    {
                       btnGuardar.Image = Resources.b8;
                        ckbEliminarAsig.Visible = false;


                       // panelPermisos.Enabled = false;
                        ckbActualizar.Checked = false;
                        ckbAgregar.Checked = false;
                        ckbEliminar.Checked = false;
                        ckbEliminarAsig.Checked = false;
                        ckbImprimir.Checked = false;
                        ckbLectura.Checked = false;
                    }
                    else {
                        btnGuardar.Image = Resources.Lb2;
                        ckbEliminarAsig.Visible = true;
                        if (objModulo.steli == 1) { ckbEliminar.Checked = true; } else { ckbEliminar.Checked = false; }
                        if (objModulo.stimp == 1) { ckbImprimir.Checked = true; } else { ckbImprimir.Checked = false; }
                        if (objModulo.stact == 1) { ckbActualizar.Checked = true; } else { ckbActualizar.Checked = false; }
                        if (objModulo.stlec == 1) { ckbLectura.Checked = true; } else { ckbLectura.Checked = false; }
                        if (objModulo.stcre == 1) { ckbAgregar.Checked = true; } else { ckbAgregar.Checked = false; }
                    }

                   
                }

            }
            else
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#29b6f6");
                lbMensaje.Text = "No se ha Seleccionado a un Usuario";
                dgvModulos.ClearSelection();
                dgvPerfil.ClearSelection();
            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnBuscarPerfil_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            panelTag.Visible = false;
            CVPerfil = 0;
            dgvPerfil.Columns.Remove(columnName: "imgPerfiles");

            string strPerfil = "";
            string IdTrab = "";

            if (txtPerfil.Text != String.Empty)
            {

                strPerfil = txtPerfil.Text.Trim();
            }
            else
            {
                strPerfil = "%";
            }

            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, "1");
            dgvPerfil.DataSource = dtPerfiles;



            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "imgPerfiles";
            dgvPerfil.Columns.Insert(2, imgCheckPerfiles);
            dgvPerfil.Columns[2].HeaderText = "";

            for (int iContador = 0; iContador < dgvModulos.Rows.Count; iContador++)
            {
                dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            dgvModulos.ClearSelection();
            dgvPerfil.ClearSelection();
        }

        private void btnBuscarModulo_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            CVPerfil = 0;
            dgvModulos.Columns.Remove(columnName: "imgModulos");
            panelTag.Visible = false;

            string strNombreModulo = "";
            string strDescripcion = "";
            string strAmbiente = "";
            string strModulo = "";

            if (txtNombreModulo.Text != String.Empty)
            {
                strNombreModulo = txtNombreModulo.Text.Trim();
            }
            else
            {
                strNombreModulo = "%";
            }



            if (txtModulo.Text != String.Empty)
            {
                strModulo = txtModulo.Text;
            }
            else
            {
                strModulo = "%";
            }
            if (cbAmbiente.SelectedIndex > 0)
            {
                strAmbiente = cbAmbiente.SelectedItem.ToString();
            }
            else
            {
                strAmbiente = "%";
            }

            Modulo objModulo = new Modulo();
            List<Modulo> ltModulo = objModulo.ObtenerListModulos(strNombreModulo, "%", strAmbiente, strModulo, "1");
            DataTable dtModulo = objModulo.ObtenerDataTableModulo(ltModulo);
            dgvModulos.DataSource = dtModulo;

            DataGridViewImageColumn imgCheckModulos = new DataGridViewImageColumn();
            imgCheckModulos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckModulos.Name = "imgModulos";
            dgvModulos.Columns.Insert(0, imgCheckModulos);
            dgvModulos.Columns[0].HeaderText = "";

            for (int iContador = 0; iContador < dgvPerfil.Rows.Count; iContador++)
            {
                dgvPerfil.Rows[iContador].Cells[2].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            dgvModulos.ClearSelection();
            dgvPerfil.ClearSelection();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public Asignar_Modulo()
        {
            InitializeComponent();
        }

        private void Asignar_Modulo_Load(object sender, EventArgs e)
        {

            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", "%", "1");
            dgvPerfil.DataSource = dtPerfiles;

            Modulo objModulo = new Modulo();
            List<Modulo> ltModulo = objModulo.ObtenerListModulos("%", "%", "%", "%", "1");
            DataTable dtModulo = objModulo.ObtenerDataTableModulo(ltModulo);
            dgvModulos.DataSource = dtModulo;



            DataGridViewImageColumn imgCheckModulos = new DataGridViewImageColumn();
            imgCheckModulos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckModulos.Name = "imgModulos";
            dgvModulos.Columns.Insert(0, imgCheckModulos);
            dgvModulos.Columns[0].HeaderText = "";

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "imgPerfiles";
            dgvPerfil.Columns.Insert(2, imgCheckPerfiles);
            dgvPerfil.Columns[2].HeaderText = "";

            dgvModulos.ClearSelection();
            dgvPerfil.ClearSelection();

            dgvPerfil.Columns[3].Visible = false;
            dgvPerfil.Columns[4].Visible = false;
            dgvPerfil.Columns[5].Visible = false;
            dgvPerfil.Columns[6].Visible = false;

            dgvModulos.Columns["Orden"].Visible = false;
            dgvModulos.Columns["Descripción"].Visible = false;
            dgvPerfil.Columns["CVPERFIL"].Visible = false;
            dgvModulos.Columns["Estatus"].Visible = false;



            panelPermisos.Enabled = false;
            ckbActualizar.Checked = false;
            ckbAgregar.Checked = false;
            ckbEliminar.Checked = false;
            ckbEliminarAsig.Checked = false;
            ckbImprimir.Checked = false;
            ckbLectura.Checked = false;
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void AsignarPermisosObjeto(Modulo objModulo) {

            if (ckbActualizar.Checked == true) { objModulo.stact = 1; } else { objModulo.stact = 0; }
            if (ckbAgregar.Checked == true) { objModulo.stcre = 1; } else { objModulo.stcre = 0; }
            if (ckbEliminar.Checked == true) { objModulo.steli = 1; } else { objModulo.steli = 0; }
            if (ckbImprimir.Checked == true) { objModulo.stimp = 1; } else { objModulo.stimp = 0; }
          

        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


     

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
                    }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                Perfil objPerfil = new Perfil();
                Modulo objModulo = new Modulo();
                objModulo.CVModulo = CVModulo;
                objModulo.UsuuMod = "vjiturburuv";
                objModulo.PrguMod = this.Name;
                AsignarPermisosObjeto(objModulo);
                int response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil,iOpcionAdmin);
                panelTag.Visible = true;
                timer1.Start();
                if (response == 0) {
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Actualización Correcta";
                    
                }else if(response == 1)
                {
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Asignación Correcta";

                }
                else if (response == 2)
                {
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Asignación eliminada";

                }


                panelPermisos.Enabled = false;
                ckbActualizar.Checked = false;
                ckbAgregar.Checked = false;
                ckbEliminar.Checked = false;
                ckbEliminarAsig.Checked = false;
                ckbImprimir.Checked = false;
                ckbLectura.Checked = false;
            }
            catch (Exception ex)
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#ef5350");
                lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.";

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminarAsig.Checked != false)
            {
                btnGuardar.Image = Resources.b7;
                iOpcionAdmin = 2;
            }
            else {
                btnGuardar.Image = Resources.b8;
                iOpcionAdmin = 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
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
    }
}
