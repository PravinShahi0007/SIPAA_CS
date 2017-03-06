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

namespace SIPAA_CS
{
    public partial class Crear_Perfil : Form
    {
        public Point formPosition;
        public Boolean mouseAction;
        int iOpcionAdmin;
        public int IdPerfil;
        public Crear_Perfil()
        {
            InitializeComponent();
        }

        private void Crear_Perfil_Load(object sender, EventArgs e)
        {
            DisableBotones(btnGuardar, 1,true);
            DisableBotones(btnEditar, 2, true);
            DisableBotones(btnEliminar, 3, true);


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            label2.Text = "       Perfil Seleccionado";
            if (dgvPerfiles.Columns.Count > 3)
            {
                dgvPerfiles.Columns.Remove(columnName: "SELECCIONAR");
            }
            panelTag.Visible = false;
            PanelEditar.Enabled = false;
            txtPerfil.Text = "Sin Selección";

            DisableBotones(btnGuardar, 1, true);
            DisableBotones(btnEditar, 2, true);
            DisableBotones(btnEliminar, 3, true);

            Perfil objPerfil = new Perfil();

            string strPerfil = "%";

            if (txtBuscarPerfil.Text != String.Empty)
            {
                strPerfil = txtBuscarPerfil.Text;
            }
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(strPerfil);
            dgvPerfiles.DataSource = dtPerfiles;
            dgvPerfiles.Columns[0].Visible = false;
            dgvPerfiles.Visible = true;

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "SELECCIONAR";
            dgvPerfiles.Columns.Insert(2, imgCheckPerfiles);
            ImageList imglt = new ImageList();

        }

        private void barraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void barraSuperior_MouseDown(object sender, MouseEventArgs e)
        {

            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void barraSuperior_MouseMove(object sender, MouseEventArgs e)
        {

            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
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

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void dgvPerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
            {
                dgvPerfiles.Rows[iContador].Cells[2].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvPerfiles.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvPerfiles.SelectedRows[0];

               IdPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                string ValorRow = row.Cells["DESCRIPCION"].Value.ToString();

                txtPerfil.Text = ValorRow;
                row.Cells[2].Value = Resources.ic_check_circle_green_400_18dp;

               // DisableBotones(btnGuardar, 1,false);
                DisableBotones(btnEditar, 2, false);
                DisableBotones(btnEliminar, 3, false);

            }
        }

        private void DisableBotones(Button btn, int iClase,Boolean Apagar)
        {

            if (Apagar == false)
            {

                switch (iClase)
                {

                    case 1:
                        //Clase Success - Color Verde
                        btn.Enabled = true;
                        btn.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        btn.ForeColor = ColorTranslator.FromHtml("#2e7d32");

                        break;
                    case 2:
                        //Clase Info - Color Azul
                        btn.Enabled = true;
                        btn.BackColor = ColorTranslator.FromHtml("#0277bd");
                        btn.ForeColor = ColorTranslator.FromHtml("#0277bd");

                        break;
                    case 3:
                        //Clase Danger - Color Rojo
                        btn.Enabled = true;
                        btn.BackColor = ColorTranslator.FromHtml("#f44336");
                        btn.ForeColor = ColorTranslator.FromHtml("#f44336");
                        break;
                    default:

                        break;
                }
            }
            else
            {
                btn.Enabled = false;
                btn.BackColor = ColorTranslator.FromHtml("#eeeeee");
                btn.ForeColor = ColorTranslator.FromHtml("#eeeeee");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            Perfil objPerfil = new Perfil();
            objPerfil.CVPerfil = IdPerfil;
            objPerfil.Descripcion = txtPerfil.Text;
            objPerfil.PrguMod = "Recursos Humanos";
            objPerfil.UsuuMod = "vjiturburuv";
            GestionarPefilesxOpcion(txtPerfil, objPerfil, "Perfil Eliminado Correctamente", 3, sender, e);
            dgvPerfiles.Visible = false;
            txtPerfil.Text = "Sin Selección";
            PanelEditar.Enabled = false;            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            PanelEditar.Enabled = true;
            iOpcionAdmin = 2;
            DisableBotones(btnGuardar, 2, false);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            dgvPerfiles.Visible = false;
            txtPerfil.Text = "";
            PanelEditar.Enabled = true;
            label2.Text = "     Nuevo Perfil";
            DisableBotones(btnGuardar, 1, false);

            iOpcionAdmin = 1;

        }

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
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        lbMensaje.Text = strMensaje;
                        //  Thread.Sleep(3000);
                        return iResponse;
                    }
                    else if (iResponse == 0)
                    {
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#f44336");
                        lbMensaje.Text = "El Perfil Ingresado ya se encuentra registrado.";
                        return iResponse;
                    }

                }
                catch (Exception ex)
                {

                    panelTag.Visible = true;
                    panelTag.BackColor = ColorTranslator.FromHtml("#f44336");
                    lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde";
                    return 0;
                }
            }
            else
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#0277bd");
                lbMensaje.Text = "El Campo Editar no puede ir Vacio";
                return 0;
            }
            return 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            label2.Text = "       Perfil Seleccionado";
            PanelEditar.Enabled = false;
            Perfil objPerfil = new Perfil();
            objPerfil.CVPerfil = IdPerfil;
            objPerfil.Descripcion = txtPerfil.Text;
            objPerfil.PrguMod = "Recursos Humanos";
            objPerfil.UsuuMod = "vjiturburuv";
            string strMensaje;

            if (iOpcionAdmin == 1)
            {
                strMensaje = "Perfil Guardado Correctamente";
            }
            else {
                strMensaje = "Perfil Actualizado Correctamente";
            }

           int iResponse =  GestionarPefilesxOpcion(txtPerfil, objPerfil,strMensaje, iOpcionAdmin, sender, e);

            if (iResponse != 0) {

                if (iOpcionAdmin != 2)
                {
                    DataTable dtPerfilxID = objPerfil.ObtenerPerfilxID(iResponse);
                    dgvPerfiles.DataSource = dtPerfilxID;
                    dgvPerfiles.Visible = true;
                    dgvPerfiles.Columns[0].Visible = false;
                }
                else {
                    DataTable dtPerfilxID = objPerfil.ObtenerPerfilxID(IdPerfil);
                    dgvPerfiles.DataSource = dtPerfilxID;
                    dgvPerfiles.Visible = true;
                    dgvPerfiles.Columns[0].Visible = false;
                }
            }

        }
    }
}
