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
    public partial class Asignar_Modulo : Form
    {
        public int CVPerfil = 0;
        public Asignar_Modulo()
        {
            InitializeComponent();
        }

        private void Asignar_Modulo_Load(object sender, EventArgs e)
        {
            
            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%");
            dgvPerfil.DataSource = dtPerfiles;

            Modulo objModulo = new Modulo();
            List<Modulo> ltModulo = objModulo.ObtenerListModulos("%", "%", "%", "%");
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

            dgvModulos.Columns["Orden"].Visible = false;
            dgvModulos.Columns["Descripción"].Visible = false;
            dgvPerfil.Columns["CVPERFIL"].Visible = false;
           
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

                if (dgvModulos.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = this.dgvModulos.SelectedRows[0];

                    string CVModulo = row.Cells[1].Value.ToString();
                    string UsuuMod = "vjiturburuv";
                    string PrguMod = "Recursos_Humanos";

                    try
                    {
                        Perfil objPerfil = new Perfil();
                        objPerfil.AsignarModuloAPerfil(CVModulo, CVPerfil, UsuuMod, PrguMod);
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                        lbMensaje.Text = "Cambio Hecho Correctamente";
                        dgvPerfil_CellContentClick(sender, e);

                    }
                    catch (Exception ex)
                    {

                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#ef5350");
                        lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.";

                    }
                }

            }
            else
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#29b6f6");
                lbMensaje.Text = "No se ha Seleccionado a un Usuario";
            }
        }

        private void btnBuscarPerfil_Click(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            CVPerfil = 0;
            dgvPerfil.Columns.Remove(columnName: "imgPerfiles");

            string strPerfil = "";
            string IdTrab = "";

            if (txtPerfil.Text != String.Empty)
            {

                strPerfil = txtPerfil.Text;
            }
            else
            {
                strPerfil = "%";
            }

            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda(strPerfil);
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


        }

        private void btnBuscarModulo_Click(object sender, EventArgs e)
        {
            CVPerfil = 0;
            dgvModulos.Columns.Remove(columnName: "imgModulos");
            panelTag.Visible = false;

            string strNombreModulo = "";
            string strDescripcion = "";
            string strAmbiente = "";
            string strModulo = "";

            if (txtNombreModulo.Text != String.Empty)
            {
                strModulo = txtNombreModulo.Text;
            }
            else
            {
                strModulo = "%";
            }

            if (txtDescripcion.Text != String.Empty)
            {
                strDescripcion = txtNombreModulo.Text;
            }
            else
            {
                strDescripcion = "%";
            }

            if (txtModulo.Text != String.Empty)
            {
                strModulo = txtNombreModulo.Text;
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
            List<Modulo> ltModulo = objModulo.ObtenerListModulos(strNombreModulo,strDescripcion,strAmbiente,strModulo);
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
        }
    }
}
