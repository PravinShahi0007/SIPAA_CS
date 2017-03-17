using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIPAA_CS.Recursos_Humanos.App_Code;
using SIPAA_CS.Properties;

namespace SIPAA_CS.Recursos_Humanos
{
    public partial class Incapacidad_Representa : Form
    {
        public int cvIncidencia;
        public int cvRepresenta;
        public int iOpcionAdmin;
        public string strIncidencia;

        //***********************************************************************************************
        //Autor: -------------------------------------
        //Fecha creación:dd-mm-aaaa       Última Modificacion: dd-mm-aaaa
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void LLenarGrid(int Opcion, Incidencia objIncidencia)
        {

            switch (Opcion)
            {

                case 1: //Busqueda

                    // objIncidencia.Descripcion = "%";
                    // objIncidencia.Representa = "%";
                    objIncidencia.UsuuMod = "";
                    objIncidencia.FhuMod = DateTime.Now;
                    objIncidencia.PrguMod = "";
                    int iOpcion = 1;

                    DataTable dtIncidencia = objIncidencia.ObtenerRepresentaxIncidencia(objIncidencia, iOpcion);
                    dgvIncidencia.DataSource = dtIncidencia;

                    DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
                    imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
                    imgCheckPerfiles.Name = "Seleccionar";
                    //imgCheckPerfiles.HeaderText = "";
                    dgvIncidencia.Columns.Insert(0, imgCheckPerfiles);
                    ImageList imglt = new ImageList();

                    dgvIncidencia.Columns[1].Visible = false;
                    dgvIncidencia.Columns[3].Visible = false;
                    dgvIncidencia.ClearSelection();

                    break;

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvIncidencia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvIncidencia.Rows.Count; iContador++)
            {
                dgvIncidencia.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvIncidencia.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvIncidencia.SelectedRows[0];
                PanelEditar.Visible = true;
                cvIncidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());
                cvRepresenta = Convert.ToInt32(row.Cells["cvrepresenta"].Value.ToString());
                strIncidencia = row.Cells["Incidencia"].Value.ToString();
                string strRepresenta = row.Cells["Representa"].Value.ToString();
                LlenarComboRepresenta(cbIncidencia, 1);
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                cbIncidencia.SelectedItem = strIncidencia;
                //   LlenarComboRepresenta(cbRepresentaEditar,5);
                cbRepresentaEditar.SelectedItem = strRepresenta;
                lblAccion.Text = "     Editar Incidencia";
                cbIncidencia.Enabled = false;
                PanelEditar.Visible = true;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                btnGuardar.Image = Resources.b3;
                //Utilerias.CambioBoton(btnGuardar, btnEliminar,btnGuardar, btnEditar);

                iOpcionAdmin = 2;


            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            lblAccion.Text = "     Nueva Asignación";
            panelTag.Visible = false;
            ckbEliminar.Checked = false;

            cbIncidencia.Enabled = true;
            LlenarComboRepresenta(cbIncidencia, 6);
            //  LlenarComboRepresenta(cbRepresentaEditar, 5);
            txtBuscarIncidencia.Text = "";
            PanelEditar.Visible = true;
            iOpcionAdmin = 4;
            //btnEditar.Visible = false;
            btnGuardar.Image = Resources.b8;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            strIncidencia = cbIncidencia.SelectedItem.ToString();


            lblAccion.Text = "       Selección";

            Incidencia objIncidencia = new Incidencia();
            //objIncidencia.CVIncidencia = cbIncidencia.SelectedIndex;
            objIncidencia.Descripcion = strIncidencia;
            objIncidencia.CVRepresenta = cbRepresentaEditar.SelectedIndex;
            objIncidencia.Representa = cbRepresentaEditar.SelectedItem.ToString();
            objIncidencia.PrguMod = this.Name;
            // objIncidencia.FhuMod = DateTime.Now;
            objIncidencia.UsuuMod = "vjiturburuv";
            try
            {
                DataTable reponse = objIncidencia.ObtenerRepresentaxIncidencia(objIncidencia, iOpcionAdmin);
                ckbEliminar.Checked = false;


                LlenarComboRepresenta(cbIncidencia, 6);

                btnBuscar_Click_1(sender, e);
            }
            catch (Exception ex)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentarlo más tarde.");
            }

        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ckbEliminar.Checked = false;
            ckbEliminar.Visible = false;
            lblAccion.Text = "       Perfil Seleccionado";
            if (dgvIncidencia.Columns.Count > 3)
            {
                dgvIncidencia.Columns.Remove(columnName: "SELECCIONAR");
            }
            panelTag.Visible = false;
            PanelEditar.Visible = false;

            Incidencia objIncidencia = new Incidencia();
            objIncidencia.Descripcion = txtBuscarIncidencia.Text;

            if (cbRepresenta.SelectedIndex < 1)
            {
                objIncidencia.Representa = "%";
            }
            else
            {
                objIncidencia.Representa = cbRepresenta.SelectedItem.ToString();
            }
            LLenarGrid(1, objIncidencia);
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public Incapacidad_Representa()
        {
            InitializeComponent();
        }

        private void Incapacidad_Representa_Load(object sender, EventArgs e)
        {

            Incidencia objIncidencia = new Incidencia();
            objIncidencia.Descripcion = "%";
            objIncidencia.Representa = "%";
            LLenarGrid(1, objIncidencia);
            txtBuscarIncidencia.Focus();
            //LlenarComboRepresenta(cbRepresenta,5);
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                btnGuardar.Image = Resources.b6;

                iOpcionAdmin = 3;
                //Utilerias.CambioBoton(btnGuardar, btnEditar, btnGuardar, btnEliminar);
            }
            else
            {
                iOpcionAdmin = 2;
                btnGuardar.Image = Resources.b3;
                //Utilerias.CambioBoton(btnGuardar, btnEliminar, btnGuardar, btnEditar);

            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------


        private void LlenarComboRepresenta(ComboBox cb, int Opcion)
        {
            Incidencia objIncidencia = new Incidencia();
            objIncidencia.Descripcion = "";
            objIncidencia.Representa = "";
            objIncidencia.UsuuMod = "";
            objIncidencia.FhuMod = DateTime.Now;
            objIncidencia.PrguMod = "";
            //int iOpcion = 5;
            DataTable dtIncidencia = objIncidencia.ObtenerRepresentaxIncidencia(objIncidencia, Opcion);
            List<string> ltRepresenta = new List<string>();
            foreach (DataRow row in dtIncidencia.Rows)
            {
                ltRepresenta.Add(row[1].ToString());
            }
            cb.DataSource = ltRepresenta;

            if (cb.Items.Count == 0)
            {
                cb.Enabled = false;
                cb.SelectedText = "Sin datos para Asignar";
            }

        }




        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------







      

    

  
    


    }
}
