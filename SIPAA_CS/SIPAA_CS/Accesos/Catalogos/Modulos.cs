using SIPAA_CS.App_Code;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace SIPAA_CS.Accesos
{
    public partial class Modulos : Form
    {
        Utilerias utilerias = new Utilerias();
        public string modulo;
        //***********************************************************************************************
        //Autor: Gamaliel Lobato Solis
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: Formulario de crear Modulo
        //***********************************************************************************************
        public Modulos()
        {
            InitializeComponent();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvModulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvModulos.Rows.Count; iContador++)
            {
                dgvModulos.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvModulos.SelectedRows.Count != 0)
            {
                ckbEliminar.Visible = true;
                PanelEditar.Visible = true;
                //utilerias.ChangeButton(btnGuardar,3,true);
                DataGridViewRow row = this.dgvModulos.SelectedRows[0];
                //cvusuario = row.Cells["cvusuario"].Value.ToString();
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                //AsignarPlantel();

                utilerias.ChangeButton(btnGuardar, 2, false);
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            PanelEditar.Visible = true;
        }


        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Modulos_Load(object sender, EventArgs e)
        {
            LlenarGridModulos("", "", "", 0, "", "", "", 0, "", "", 4,dgvModulos);
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------
        public void LlenarGridModulos(string cvmodulo, string descripcion, string cvmodpad, int orden, string ambiente, string modulo, string rutaaaceso, int stmodulo, string usumod, string prgumod, int opcion, DataGridView dgvModulo)
        {

            if (dgvModulo.Columns.Count > 1)
            {
                dgvModulo.Columns.RemoveAt(0);
            }
            Modulo objModulo = new Modulo();
            DataTable dtModulo = objModulo.ObtenerModulo(cvmodulo, descripcion, cvmodpad, orden, ambiente, modulo, rutaaaceso, stmodulo, usumod, prgumod, opcion);

            dgvModulo.DataSource = dtModulo;

            DataGridViewImageColumn imgCheckProcesos = new DataGridViewImageColumn();
            imgCheckProcesos.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckProcesos.Name = "Seleccionar";
            dgvModulo.Columns.Insert(0, imgCheckProcesos);
            dgvModulo.Columns[0].HeaderText = "Seleccionar";
            dgvModulos.Columns[1].Visible = false;
            //dgvModulos.Columns[2].Visible = false;
            dgvModulos.Columns[3].Visible = false;
            dgvModulos.Columns[4].Visible = false;
            dgvModulos.Columns[5].Visible = false;
            dgvModulos.Columns[6].Visible = false;
            dgvModulos.Columns[7].Visible = false;
            //dgvModulos.Columns[8].Visible = false;
            dgvModulos.Columns[9].Visible = false;
            dgvModulos.Columns[10].Visible = false;

            dgvModulo.ClearSelection();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            modulo = txtBuscarModulo.Text;

            LlenarGridModulos("", modulo.Trim(), "", 0, "", "", "", 0, "", "", 5, dgvModulos);

            txtBuscarModulo.Text = "";
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------

    }
}
