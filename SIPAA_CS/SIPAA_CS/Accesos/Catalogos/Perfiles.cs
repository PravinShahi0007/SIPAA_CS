using SIPAA_CS.Properties;
using SIPAA_CS.Recursos_Humanos.Administracion;
using SIPAA_CS.Recursos_Humanos.App_Code;
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
using static SIPAA_CS.Recursos_Humanos.App_Code.Usuario;

namespace SIPAA_CS
{


    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017       Última Modificacion: 13-03-2017
    //Descripción: Pantalla que permite la gestión de Perfiles de usuario
    //***********************************************************************************************

    public partial class Crear_Perfil : Form
    {
        public Point formPosition;
        public Boolean mouseAction;
        int iOpcionAdmin;
        public int IdPerfil;
        string strEstatus;
        public List<string> ltPermisos = new List<string>();



        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void LlenarGridPerfiles(DataTable dtPerfiles, bool Seleccion)
        {

            dgvPerfiles.DataSource = dtPerfiles;
            dgvPerfiles.Columns["CVPERFIL"].Visible = false;
            dgvPerfiles.Columns["USUUMOD"].Visible = false;
            dgvPerfiles.Columns["FHUMOD"].Visible = false;
            dgvPerfiles.Columns["PRGUMOD"].Visible = false;
            dgvPerfiles.Columns["stperfil"].Visible = false;
            dgvPerfiles.Visible = true;

            if (Seleccion != true)
            {
                DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
                imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckPerfiles.Name = "Seleccionar";
                //imgCheckPerfiles.HeaderText = "";
                dgvPerfiles.Columns.Insert(0, imgCheckPerfiles);
                ImageList imglt = new ImageList();
            }
            dgvPerfiles.ClearSelection();


        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Checked = false;
            ckbEliminar.Visible = false;
            lblAccion.Text = "       Perfil Seleccionado";
            if (dgvPerfiles.Columns.Count > 3)
            {
                dgvPerfiles.Columns.Remove(columnName: "SELECCIONAR");
            }
          
            PanelEditar.Visible = false;
            txtPerfil.Text = "Sin Selección";

            Perfil objPerfil = new Perfil();

            string strPerfil = "%";

            if (txtBuscarPerfil.Text != String.Empty)
            {
                strPerfil = txtBuscarPerfil.Text.Trim();
            }

            string strEstatus = "%";

            if (cbEstatus.SelectedIndex > 0)
            {
                if (cbEstatus.SelectedIndex == 1)
                {
                    strEstatus = "1";
                }
                else if (cbEstatus.SelectedIndex == 2)
                {
                    strEstatus = "0";
                }
            }
            else
            {
                strEstatus = "%";
            }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, strEstatus);
            LlenarGridPerfiles(dtPerfiles, false);

        }



        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            ckbEliminar.Checked = false;
           
            txtPerfil.Text = "";
            PanelEditar.Visible = true;
            lblAccion.Text = "     Nuevo Perfil";

            iOpcionAdmin = 1;
            //btnEditar.Visible = false;
            btnGuardar.Image = Resources.b8;
            //Utilerias.CambioBoton(btnGuardar,btnEliminar ,btnEditar, btnGuardar);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            lblAccion.Text = "       Perfil Seleccionado";

            Perfil objPerfil = new Perfil();
            objPerfil.CVPerfil = IdPerfil;
            objPerfil.Descripcion = txtPerfil.Text.Trim();
            objPerfil.PrguMod = this.Name;
            objPerfil.UsuuMod = "vjiturburuv";
            string strMensaje = "";

            if (iOpcionAdmin == 1)
            {
                strMensaje = "Perfil Guardado Correctamente";
            }
            else if (iOpcionAdmin == 2)
            {
                strMensaje = "Perfil Actualizado Correctamente";
            }
            else if (iOpcionAdmin == 3)
            {
                strMensaje = "Cambio de Estatus hecho Correctamente";
            }

            int iResponse = GestionarPefilesxOpcion(txtPerfil, objPerfil, strMensaje, iOpcionAdmin, sender, e);
           

            ckbEliminar.Checked = false;
            if (iResponse != 0)
            {

                btnBuscar_Click(sender, e);

            }

        }



        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public Crear_Perfil()
        {
            InitializeComponent();
        }

        private void Crear_Perfil_Load(object sender, EventArgs e)
        {

            //Validar permisos x Pantalla
            Modulo objModulo = new Modulo();
            string idTrab = LoginInfo.IdTrab;
            DataTable dtPermisos = objModulo.ObtenerPermisosxUsuario(idTrab);
            DataRow[] row = dtPermisos.Select("CVModulo = '"+this.Tag +"'");
            Utilerias.CrearListaPermisoxPantalla(row, ltPermisos);
            Utilerias.ApagarControlxPermiso(btnAgregar, "Crear", ltPermisos);


            lblAccion.Text = "       Perfil Seleccionado";
            txtPerfil.Text = "Sin Selección";


            Perfil objPerfil = new Perfil();

            string strPerfil = "%";

            if (txtBuscarPerfil.Text != String.Empty)
            {
                strPerfil = txtBuscarPerfil.Text.Trim();
            }

            string strEstatus = "%";

            if (cbEstatus.SelectedIndex > 0)
            {
                if (cbEstatus.SelectedIndex == 1)
                {
                    strEstatus = "1";
                }
                else if (cbEstatus.SelectedIndex == 2)
                {
                    strEstatus = "0";
                }
            }
            else
            {
                strEstatus = "%";
            }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, strEstatus);
            LlenarGridPerfiles(dtPerfiles, false);

        }


       
        private void dgvPerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



            ckbEliminar.Visible = true;
            ckbEliminar.Checked = false;
            Utilerias.ApagarControlxPermiso(ckbEliminar, "Eliminar", ltPermisos);
            for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
            {
                dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvPerfiles.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvPerfiles.SelectedRows[0];

                IdPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                string ValorRow = row.Cells["DESCRIPCION"].Value.ToString();
                strEstatus = row.Cells["stPerfil"].Value.ToString();
                txtPerfil.Text = ValorRow;
                PanelEditar.Visible = true;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                btnGuardar.Image = Resources.b3;
                //Utilerias.CambioBoton(btnGuardar, btnEliminar,btnGuardar, btnEditar);

                iOpcionAdmin = 2;

                Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);
                if (btnGuardar.Visible == false) {
                    PanelEditar.Enabled = false;
                }
                if (strEstatus == "0")
                {
                    ckbEliminar.Text = "Alta";

                }
                else if (strEstatus == "1")
                {
                    ckbEliminar.Text = "Baja";

                }

            }
        }

     

        private void PanelEditar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbEliminar.Checked == true)
            {

                if (strEstatus == "0")
                {

                    btnGuardar.Image = Resources.btalta;
                }
                else if (strEstatus == "1")
                {

                    btnGuardar.Image = Resources.b6;
                }

                iOpcionAdmin = 3;
                //Utilerias.CambioBoton(btnGuardar, btnEditar, btnGuardar, btnEliminar);
            }
            else
            {
                iOpcionAdmin = 2;
                btnGuardar.Image = Resources.btnEdit;
                //Utilerias.CambioBoton(btnGuardar, btnEliminar, btnGuardar, btnEditar);

            }


        }

        private void PanelPlantilla_Paint(object sender, PaintEventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

      

        private int GestionarPefilesxOpcion(TextBox txt, Perfil objPerfil,
                     string strMensaje, int iOpcion, object sender, EventArgs e)
        {
            if (txt.Text != String.Empty || txt.Text != "Sin Selección")
            {
                try
                {
                    int iResponse = objPerfil.GestionarPerfiles(objPerfil, iOpcion);
                    if (iResponse != 0)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, strMensaje);
                        timer1.Start();
                        return iResponse;
                    }
                    else if (iResponse == 0)
                    {
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "El Perfil Ingresado ya se encuentra registrado.");
                        timer1.Start();
                        return iResponse;
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde");
                    timer1.Start();
                    return 0;
                }
            }
            else
            {
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "El Campo Editar no puede ir Vacio");
                timer1.Start();
                return 0;
            }
            return 0;
        }

    
        private void btnRegresar_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void txtBuscarPerfil_KeyUp(object sender, KeyEventArgs e)
        {
            ckbEliminar.Checked = false;
            ckbEliminar.Visible = false;
            lblAccion.Text = "       Perfil Seleccionado";
            if (dgvPerfiles.Columns.Count > 3)
            {
                dgvPerfiles.Columns.Remove(columnName: "SELECCIONAR");
            }
           
            PanelEditar.Visible = false;
            txtPerfil.Text = "Sin Selección";

            Perfil objPerfil = new Perfil();

            string strPerfil = "%";

            if (txtBuscarPerfil.Text != String.Empty)
            {
                strPerfil = txtBuscarPerfil.Text.Trim();
            }

            string strEstatus = "%";

            if (cbEstatus.SelectedIndex > 0)
            {
                if (cbEstatus.SelectedIndex == 1)
                {
                    strEstatus = "1";
                }
                else if (cbEstatus.SelectedIndex == 2)
                {
                    strEstatus = "0";
                }
            }
            else
            {
                strEstatus = "%";
            }

            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, strEstatus);
            LlenarGridPerfiles(dtPerfiles, false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}



//private void barraSuperior_MouseUp(object sender, MouseEventArgs e)
//{
//    mouseAction = false;
//}

//private void barraSuperior_MouseDown(object sender, MouseEventArgs e)
//{

//    formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
//    mouseAction = true;
//}

//private void barraSuperior_MouseMove(object sender, MouseEventArgs e)
//{

//    if (mouseAction == true)
//    {
//        Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
//    }
//}