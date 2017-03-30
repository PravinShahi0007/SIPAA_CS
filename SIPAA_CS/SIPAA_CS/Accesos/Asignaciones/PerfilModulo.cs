using SIPAA_CS.App_Code;
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

namespace SIPAA_CS.Accesos
{


    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017      Última Modificacion: 13-03-2017
    //Descripción: Pantalla de Asignación de Módulos a Perfiles
    //***********************************************************************************************
    public partial class PerfilModulo : Form
    {
        public int CVPerfil = 0;
        public string CVModulo;
        public int ultimaseleccion = 0;
        public int iOpcionAdmin;
        public List<string> ltPermisos = new List<string>();
        public DataTable dtPermisos;

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void llenarGridPerfiles(Perfil objPerfil) {


            if (dgvPerfil.Columns.Count > 0) {

                dgvPerfil.Columns.RemoveAt(2);
            }
            string cvPerfil = "%";
            string strEstatus = "%";
            if (objPerfil.CVPerfil != 0) { cvPerfil = objPerfil.CVPerfil.ToString(); }
            if (objPerfil.Estatus != 0) { strEstatus = objPerfil.Estatus.ToString(); }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(cvPerfil
                                                                    , objPerfil.Descripcion
                                                                    , strEstatus);
            dgvPerfil.DataSource = dtPerfiles;


            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "imgPerfiles";
            dgvPerfil.Columns.Insert(2, imgCheckPerfiles);
            dgvPerfil.Columns[2].HeaderText = "Seleccionar";
            dgvPerfil.Columns[2].Width = 40;


            dgvPerfil.Columns[3].Visible = false;
            dgvPerfil.Columns[4].Visible = false;
            dgvPerfil.Columns[5].Visible = false;
            dgvPerfil.Columns[6].Visible = false;
            dgvPerfil.ClearSelection();


        }


        private void llenarGridModulos(Modulo objModulo) {



            if (dgvModulos.Columns.Count > 0)
            {

                dgvModulos.Columns.RemoveAt(0);
            }
            string strEstatus = "%";
            if (objModulo.Estatus != 0) { strEstatus = objModulo.Estatus.ToString(); }
           
            List<Modulo> ltModulo = objModulo.ObtenerListModulos(objModulo.CVModulo, objModulo.Descripcion
                                                                , objModulo.Ambiente
                                                                , objModulo.strModulo
                                                                , strEstatus);

            DataTable dtModulo = objModulo.ObtenerDataTableModulo(ltModulo);
            dgvModulos.DataSource = dtModulo;

            DataGridViewImageColumn imgCheckModulos = new DataGridViewImageColumn();
            imgCheckModulos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckModulos.Name = "imgModulos";
            dgvModulos.Columns.Insert(0, imgCheckModulos);
            dgvModulos.Columns[0].HeaderText = "Seleccionar";
            dgvModulos.Columns[0].Width = 40;

            dgvModulos.ClearSelection();
            dgvPerfil.Columns["CVPERFIL"].Visible = false;
            dgvModulos.Columns["Orden"].Visible = false;
            dgvModulos.Columns["Ambiente"].Visible = false;
            dgvModulos.Columns["Descripción"].Visible = false;
            dgvModulos.Columns["Estatus"].Visible = true;



        }
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
                //List<string> ltPerfilesxUsuario = objModulo.obtenerModulosxPerfil(CVPerfil);

                AsignarPermisos();                


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


                    //string cvModulo = dgvModulos.Rows[iContador].Cells[1].Value.ToString();
                    DataRow[] rows = dtPermisos.Select("CVMODULO = '" + CVModulo + "'");

                    // Modulo objModulo = new Modulo();
                    // objModulo = objModulo.ObtenerPermisosxModulo(CVModulo, CVPerfil);

                    iOpcionAdmin = 1;

                    if (rows.Count() == 0)
                    {
                        btnGuardar.Image = Resources.Guardar;
                        ckbEliminarAsig.Visible = false;


                        // panelPermisos.Enabled = false;
                        ckbActualizar.Checked = false;
                        ckbAgregar.Checked = false;
                        ckbEliminar.Checked = false;
                        ckbEliminarAsig.Checked = false;
                        ckbImprimir.Checked = false;
                        ckbLectura.Checked = true;
                    }
                    else
                    {
                        /* 0 CVModulo
                         * 1 CREAR 
                         * 2 ELIMINAR 
                         * 3 ACTUALIZAR 
                         * 4IMPRIMIR 
                         * 5 LECTURA
                    * */
                        btnGuardar.Image = Resources.Editar;
                        ckbEliminarAsig.Visible = true;
                        ckbEliminarAsig.Checked = false;
                        if (rows[0].ItemArray[1].ToString() == "1") { ckbAgregar.Checked = true; } else { ckbAgregar.Checked = false; }
                        if (rows[0].ItemArray[2].ToString() == "1") { ckbEliminar.Checked = true; } else { ckbEliminar.Checked = false; }
                        if (rows[0].ItemArray[3].ToString() == "1") { ckbActualizar.Checked = true; } else { ckbActualizar.Checked = false; }
                        if (rows[0].ItemArray[4].ToString() == "1") { ckbImprimir.Checked = true; } else { ckbImprimir.Checked = false; }
                        if (rows[0].ItemArray[5].ToString() == "1") { ckbLectura.Checked = true; } else { ckbLectura.Checked = false; }
                    }

                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado un Perfil");
                    dgvModulos.ClearSelection();
                    dgvPerfil.ClearSelection();
                }
            }
            else {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado un Perfil");
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
                int response= 0;



                switch (iOpcionAdmin)
                {

                    case 2:
                        DialogResult result = MessageBox.Show("¿Seguro que desea Eliminar la Asignación?", this.Name, MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                             response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, iOpcionAdmin);
                            iOpcionAdmin = 1;
                            ckbEliminarAsig.Visible = false;
                            btnGuardar.Image = Resources.Guardar;
                            ckbEliminar.Checked = false;
                            ckbActualizar.Checked = false;
                            ckbAgregar.Checked = false;
                            ckbImprimir.Checked = false;
                            ckbLectura.Checked = true;

                        }
                        break;

                    default:
                        response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, iOpcionAdmin);

                        break;
                }


               
                panelTag.Visible = true;
                timer1.Start();
                if (response == 0)
                {
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Actualización Correcta";

                }
                else if (response == 1)
                {
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Asignación Correcta";

                }
                else if (response == 2)
                {
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Asignación eliminada";

                }


                string idtrab = LoginInfo.IdTrab;
                dtPermisos = objModulo.ObtenerPermisosxUsuario(idtrab);
                DataRow[] row = dtPermisos.Select("CVModulo = 'frmCrear_Perfil'");
                Utilerias.CrearListaPermisoxPantalla(row, ltPermisos);

                AsignarPermisos();

                //Asignar_Modulo_Load(sender, e);
                //dgvPerfil_CellContentClick(sender, e);

            }
            catch (Exception ex)
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#ef5350");
                lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.";

            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public PerfilModulo()
        {
            InitializeComponent();
        }

        private void Asignar_Modulo_Load(object sender, EventArgs e)
        {


            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            string idtrab = LoginInfo.IdTrab;
            Modulo objModulo = new Modulo();
             dtPermisos = objModulo.ObtenerPermisosxUsuario(idtrab);
            DataRow[] row = dtPermisos.Select("CVModulo = 'frmCrear_Perfil'");
            Utilerias.CrearListaPermisoxPantalla(row, ltPermisos);
 


            Perfil objPerfil = new Perfil();
            objPerfil.CVPerfil = 0;
            objPerfil.Descripcion = "%";
            objPerfil.Estatus = 0;
            llenarGridPerfiles(objPerfil);

            objModulo.CVModulo = "%";
            objModulo.Descripcion = "%";
            objModulo.Ambiente = "%";
            objModulo.strModulo = "%";
            objModulo.Estatus = 0;
            llenarGridModulos(objModulo);



            panelPermisos.Enabled = false;
            ckbActualizar.Checked = false;
            ckbAgregar.Checked = false;
            ckbEliminar.Checked = false;
            ckbEliminarAsig.Checked = false;
            ckbImprimir.Checked = false;
            ckbLectura.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminarAsig.Checked != false)
            {
                btnGuardar.Image = Resources.Borrar;
                iOpcionAdmin = 2;
            }
            else
            {
                btnGuardar.Image = Resources.Editar;
                iOpcionAdmin = 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void AsignarPermisos()
        {


            for (int iContador = 0; iContador < dgvModulos.Rows.Count; iContador++)
            {
                string cvModulo = dgvModulos.Rows[iContador].Cells[1].Value.ToString();
                DataRow[] rows = dtPermisos.Select("CVMODULO = '" + cvModulo + "'");
                if (rows.Count() != 0)
                {

                    dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                }
                else
                {
                    dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }
            }


        }

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

   
     
    }
}
