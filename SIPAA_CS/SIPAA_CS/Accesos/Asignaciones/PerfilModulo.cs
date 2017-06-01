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
using static SIPAA_CS.App_Code.Utilerias;

namespace SIPAA_CS.Accesos.Asignaciones
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
        public List<string> ltAsignacionModulos = new List<string>();
        public DataTable dtPermisos;
        CheckBox ckbheader = new CheckBox();

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void llenarGridPerfiles(Perfil objPerfil) {

         
            if (dgvPerfil.Columns.Count == 0)
            {
                Utilerias.AgregarCheck(dgvPerfil, 0);
                
            }

            string cvPerfil = "%";
            string strEstatus = "%";
            if (objPerfil.CVPerfil != 0) { cvPerfil = objPerfil.CVPerfil.ToString(); }
            if (objPerfil.Estatus != 0) { strEstatus = objPerfil.Estatus.ToString(); }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(cvPerfil, objPerfil.Descripcion, strEstatus);
            dgvPerfil.DataSource = dtPerfiles;

   
       
       
           
            dgvPerfil.Columns[1].Width = 160;
            dgvPerfil.Columns["usuumod"].Visible = false;
            dgvPerfil.Columns["fhumod"].Visible = false;
            dgvPerfil.Columns["prgumod"].Visible = false;
            dgvPerfil.Columns["Estatus"].Visible = false;
            dgvPerfil.ClearSelection();


            
        }



        private void llenarGridModulos(Modulo objModulo) {


            if (dgvModulos.Columns.Count == 0)
            {
                Utilerias.AgregarCheck(dgvModulos, 0);
                ckbheader = Utilerias.AgregarCheckboxHeader(dgvModulos, 0);
                ckbheader.CheckedChanged += Ckbheader_CheckedChanged;

            }
          
            


            string strEstatus = "%";
            if (objModulo.Estatus != 0) { strEstatus = objModulo.Estatus.ToString(); }



            DataTable dtModulo = objModulo.ObtenerListModulos(objModulo.CVModulo, objModulo.Descripcion
                                                                , objModulo.Ambiente
                                                                , objModulo.strModulo
                                                                , strEstatus);
            dgvModulos.DataSource = dtModulo;

            

         

            dgvModulos.ClearSelection();
            dgvPerfil.Columns["CVPERFIL"].Visible = false;
            dgvModulos.Columns["IdModulo"].Visible = false;
            dgvModulos.Columns["Orden"].Visible = false;
            //dgvModulos.Columns["Ambiente"].Visible = false;
            dgvModulos.Columns["cvmodulo"].Visible = false;
            dgvModulos.Columns["cvindmodulo"].Visible = false;
            dgvModulos.Columns["rutaacceso"].Visible = false;
            dgvModulos.Columns["Estatus"].Visible = true;

          



        }
        private void dgvPerfil_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ckbheader.Checked = false;
            ckbActualizar.Checked = false;
            ckbAgregar.Checked = false;
            ckbEliminar.Checked = false;
            ckbEliminarAsig.Checked = false;
            ckbImprimir.Checked = false;
            ckbLectura.Checked = true;
            panelPermisos.Enabled = false;
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
                //List<string> ltPerfilesxUsuario = 
                 dtPermisos = objModulo.obtenerModulosxCvPerfil(CVPerfil);
                AsignarPermisos();                


            }
        }

     
        private void dgvModulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvModulos.Enabled != false)
            {
                ckbheader.Checked = false;
                if (CVPerfil != 0)
                {

                    dgvModulos.Rows[ultimaseleccion].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvPerfil_CellContentClick(sender, e);


                    if (dgvModulos.SelectedRows.Count != 0)
                    {
                        Modulo objModulo = new Modulo();
                        dtPermisos = objModulo.obtenerModulosxCvPerfil(CVPerfil);

                        DataGridViewRow row = this.dgvModulos.SelectedRows[0];
                        ultimaseleccion = row.Index;
                        CVModulo = row.Cells[1].Value.ToString();

                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        BotonActual(dtPermisos);

                    }
                    else
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado un Perfil");
                        dgvModulos.ClearSelection();
                        dgvPerfil.ClearSelection();
                        timer1.Start();
                    }
                }
                else
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado un Perfil");
                    dgvModulos.ClearSelection();
                    dgvPerfil.ClearSelection();
                    timer1.Start();

                }
            }
        }


        private void BotonActual(DataTable dtPermisos) {
       
            panelPermisos.Enabled = true;
            DataRow[] rows = dtPermisos.Select("IdModulo = '" + CVModulo + "'");
            

            if (rows.Count() == 0)
            {

                if (Permisos.dcPermisos["Crear"] == 1)
                {
                    ckbEliminarAsig.Visible = false;

                    iOpcionAdmin = 1;
                    // panelPermisos.Enabled = false;
                    ckbActualizar.Checked = false;
                    ckbAgregar.Checked = false;
                    ckbEliminar.Checked = false;
                    ckbEliminarAsig.Checked = false;
                    ckbImprimir.Checked = false;
                    ckbLectura.Checked = true;

                    panelPermisos.Visible = true;
                    AsignarBotonResize(btnGuardar, PantallaSistema(), Botones.Guardar);

                }
            }
            else
            {

                if (Permisos.dcPermisos["Actualizar"] == 1 && Permisos.dcPermisos["Eliminar"] == 1)
                {
                    iOpcionAdmin = 1;
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Editar);
                    //btnGuardar.Image = Resources.Editar;
                    ckbEliminarAsig.Visible = true;
                    ckbEliminarAsig.Checked = false;
                    if (rows[0].ItemArray[3].ToString() == "1") { ckbLectura.Checked = true; } else { ckbLectura.Checked = false; }
                    if (rows[0].ItemArray[4].ToString() == "1") { ckbActualizar.Checked = true; } else { ckbActualizar.Checked = false; }
                    if (rows[0].ItemArray[5].ToString() == "1") { ckbEliminar.Checked = true; } else { ckbEliminar.Checked = false; }
                    if (rows[0].ItemArray[6].ToString() == "1") { ckbImprimir.Checked = true; } else { ckbImprimir.Checked = false; }
                    if (rows[0].ItemArray[7].ToString() == "1") { ckbAgregar.Checked = true; } else { ckbAgregar.Checked = false; }
                }
                else {
                    iOpcionAdmin = 3;

                    btnGuardar.Visible = true;
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Borrar);
                }
            }
            
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnBuscarPerfil_Click(object sender, EventArgs e)
        {
            ltAsignacionModulos.Clear();
            ckbheader.Checked = false;
            panelPermisos.Enabled = false;
            panelTag.Visible = false;
            CVPerfil = 0;
            ultimaseleccion = 0;
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
            objPerfil.CVPerfil = 0;
            objPerfil.Estatus = 0;
            objPerfil.Descripcion = strPerfil;
            //DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, "1");
            //dgvPerfil.DataSource = dtPerfiles;

            llenarGridPerfiles(objPerfil);
            
            for (int iContador = 0; iContador < dgvModulos.Rows.Count; iContador++)
            {
                dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            dgvModulos.ClearSelection();
            dgvPerfil.ClearSelection();
        }

        private void btnBuscarModulo_Click(object sender, EventArgs e)
        {
            ltAsignacionModulos.Clear();
            // ckbheader.Checked = false;
            panelPermisos.Enabled = false;
            CVPerfil = 0;
            //dgvModulos.Columns.RemoveAt(0);
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



            //if (txtModulo.Text != String.Empty)
            //{
            //    strModulo = txtModulo.Text;
            //}
            //else
            //{
            //    strModulo = "%";
            //}
            if (cbAmbiente.SelectedIndex > 0)
            {
                strAmbiente = cbAmbiente.SelectedItem.ToString();
            }
            else
            {
                strAmbiente = "%";
            }

           

            Modulo objModulo = new Modulo();
            objModulo.CVModulo = strNombreModulo;
            objModulo.Descripcion = "%";
            objModulo.Ambiente = strAmbiente;
            objModulo.strModulo = strModulo;
            objModulo.Estatus = 1;
            llenarGridModulos(objModulo);


            for (int iContador = 0; iContador < dgvPerfil.Rows.Count; iContador++)
            {
                dgvPerfil.Rows[iContador].Cells[2].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            dgvModulos.ClearSelection();
         //   dgvPerfil.ClearSelection();
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
                objModulo.UsuuMod = LoginInfo.IdTrab;
                objModulo.PrguMod = this.Name;
                AsignarPermisosObjeto(objModulo);
                int response= 0;

                objModulo = asignarObjeto(objModulo);

                switch (iOpcionAdmin)
                {

                    case 1:
                        response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, iOpcionAdmin);
                        break;

                    case 3:
                        DialogResult result = MessageBox.Show("¿Seguro que desea Eliminar la Asignación?", this.Name, MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, iOpcionAdmin);
                            iOpcionAdmin = 1;
                            ckbEliminarAsig.Visible = false;
                            Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Guardar);
                            ckbEliminar.Checked = false;
                            ckbActualizar.Checked = false;
                            ckbAgregar.Checked = false;
                            ckbImprimir.Checked = false;
                            ckbLectura.Checked = true;

                        }
                        else if(result == DialogResult.No) {
                            response = 10;
                        }
                        break;

                    case 7:
                         result = MessageBox.Show("¿Seguro que desea Eliminar Todas las Asignaciones?", this.Name, MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            objModulo.CVModulo = "";
                            response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, iOpcionAdmin);

                        }
                        else if (result == DialogResult.No)
                        {
                            response = 10;
                        }
                       
                        break;

                    case 8:
                        objModulo.CVModulo = "";
                         objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, 7);

                        foreach (string sObj in ltAsignacionModulos) {

                            objModulo.CVModulo = sObj;

                            response = objPerfil.AsignarModuloAPerfil(objModulo, CVPerfil, iOpcionAdmin);
                        }
                        ltAsignacionModulos.Clear();
                        break;
                }

              

                //
                //timer1.Start();
                if (response == 0)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Actualización Correcta");
                    timer1.Start();

                }
                else if (response == 1)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignación Correcta");
                    lbMensaje.Text = "Asignación Correcta";
                   
                    timer1.Start();
                }
                else if (response == 2)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignación eliminada");
                    timer1.Start();

                }

                else if (response == 7)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones eliminadas");
                    timer1.Start();

                }

                ltAsignacionModulos.Clear();
                //string idtrab = LoginInfo.IdTrab;


                dtPermisos = objModulo.obtenerModulosxCvPerfil(CVPerfil);
                AsignarPermisos();
                BotonActual(dtPermisos);

                if (ckbheader.Checked == true) {

                    Ckbheader_CheckedChanged(sender, e);
                }
            }
            catch (Exception ex)
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#ef5350");
                lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.";
                timer1.Start();
            }
        }

        private Modulo asignarObjeto(Modulo objModulo) {

            if (ckbActualizar.Checked) { objModulo.stact = 1; } else { objModulo.stact = 0; }
            if (ckbAgregar.Checked) { objModulo.stcre = 1; } else { objModulo.stcre = 0; }
            if (ckbEliminar.Checked) { objModulo.steli = 1; } else { objModulo.steli = 0; }
            if (ckbImprimir.Checked) { objModulo.stimp = 1; } else { objModulo.stimp = 0; }
            if (ckbLectura.Checked) { objModulo.stlec = 1; } else { objModulo.stlec = 0; }

            return objModulo;
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------



        private void Ckbheader_CheckedChanged(object sender, EventArgs e)
        {
            
                if (CVPerfil != 0)
               {
                panelPermisos.Enabled = true;
                Bitmap btImagen;
                string stag= "";
                ckbEliminarAsig.Visible = false;
                if (ckbheader.Checked == true)
                {
                    iOpcionAdmin = 8;
                    btImagen = Resources.ic_check_circle_green_400_18dp;
                    stag = "check";
                    ckbLectura.Checked = true;
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Guardar);
                    ckbheader.Checked = true;
                    foreach (DataGridViewRow row in dgvModulos.Rows)
                    {
                        row.Cells[0].Value = btImagen;
                        row.Cells[0].Tag = stag;
                        if (!ltAsignacionModulos.Contains(row.Cells[1].Value.ToString())) { 
                            ltAsignacionModulos.Add(row.Cells[1].Value.ToString());}
                    }
                }
                else
                {
                    iOpcionAdmin = 7;
                    btImagen = Resources.ic_lens_blue_grey_600_18dp;
                        stag = "uncheck";
                    Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Borrar);
                    ckbEliminarAsig.Visible = false;
                    ckbheader.Checked = false;

                    ltAsignacionModulos.Clear();

                    ckbActualizar.Checked = false;
                    ckbAgregar.Checked = false;
                    ckbEliminar.Checked = false;
                    ckbEliminarAsig.Checked = false;
                    ckbImprimir.Checked = false;
                    ckbLectura.Checked = false;


                    foreach (DataGridViewRow row in dgvModulos.Rows)
                    {
                        row.Cells[0].Value = btImagen;
                        row.Cells[0].Tag = stag;
                    }
                }




            }
            else
            {
                ckbheader.Checked = false;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado un Perfil");
                dgvModulos.ClearSelection();
                dgvPerfil.ClearSelection();
                timer1.Start();

            }
        }

        public PerfilModulo()
        {
            InitializeComponent();
        }

        private void Asignar_Modulo_Load(object sender, EventArgs e)
        {
            string idtrab = LoginInfo.IdTrab;
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;

            Perfil objPerfil = new Perfil();
            objPerfil.CVPerfil = 0;
            objPerfil.Descripcion = "%";
            objPerfil.Estatus = 0;
            llenarGridPerfiles(objPerfil);

            Modulo objModulo = new Modulo();
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



            if (Permisos.dcPermisos["Crear"] != 1 && Permisos.dcPermisos["Eliminar"] != 1 && Permisos.dcPermisos["Actualizar"] != 1)
            {
                panelPermisos.Visible = false;
                dgvModulos.Enabled = false;
            }
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminarAsig.Checked != false)
            {
                //btnGuardar.Image = Resources.Borrar;
                Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Borrar);
                iOpcionAdmin = 3;
            }
            else
            {
                //btnGuardar.Image = Resources.Editar;
                Utilerias.AsignarBotonResize(btnGuardar, Utilerias.PantallaSistema(), Botones.Editar);
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
                DataRow[] rows = dtPermisos.Select("IdModulo = '" + cvModulo + "'");
                if (rows.Count() != 0)
                {
                    dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_blue_grey_600_18dp;
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
