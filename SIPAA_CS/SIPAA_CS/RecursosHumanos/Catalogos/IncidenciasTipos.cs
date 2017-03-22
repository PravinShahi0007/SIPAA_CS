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

namespace SIPAA_CS.RecursosHumanos
{
    public partial class IncidenciasTipo : Form
    {
        public int cvIncidencia;
        public int cvTipo;
        public int iOpcionAdmin;
        public string strIncidencia;
        public string strTipoIncidencia;
        public IncidenciasTipo()
        {
            InitializeComponent();
        }

        private void Incapacidad_Tipo_Load(object sender, EventArgs e)
        {
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            LLenarGridIncapacidad(dgvIncidencia,"", "");
            LlenarComboTipoIncidencia(cbTipo,"Tipo","cvtipo",5);
        }

        private void lbMensaje_Click(object sender, EventArgs e)
        {

        }

        private void LLenarGridIncapacidad(DataGridView dgvIncidencia,string Incidencia,string Tipo) {

            Incidencia objIncidencia = new Incidencia();
            objIncidencia.Descripcion = Incidencia;
            objIncidencia.TipoIncidencia = Tipo;
            objIncidencia.UsuuMod = "vjiturburuv";
            objIncidencia.PrguMod = this.Name;
            DataTable dtIncidencia = objIncidencia.ObtenerIncidenciaxTipo(objIncidencia, 4,"");
            dgvIncidencia.DataSource = dtIncidencia;


            DataGridViewImageColumn imgCheck = new DataGridViewImageColumn();
            imgCheck.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheck.Name = "Seleccionar";
            //imgCheckPerfiles.HeaderText = "";
            dgvIncidencia.Columns.Insert(0, imgCheck);

            dgvIncidencia.Columns[0].Width = 40;
            dgvIncidencia.Columns[1].Visible = false;
            dgvIncidencia.Columns[2].Visible = false;
            dgvIncidencia.ClearSelection();

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
                cvTipo = Convert.ToInt32(row.Cells["cvtipo"].Value.ToString());
                strIncidencia = row.Cells["Incidencia"].Value.ToString();
                strTipoIncidencia = row.Cells["Tipo"].Value.ToString();
                LlenarComboTipoIncidencia(cbTipoEditar,"Tipo","cvtipo",5);
                LlenarComboTipoIncidencia(cbIncidencia, "Incidencia", "cvincidencia",6);
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                cbTipoEditar.SelectedItem = strTipoIncidencia;
                //   LlenarComboRepresenta(cbRepresentaEditar,5);
                cbIncidencia.SelectedItem = strIncidencia;
                lblAccion.Text = "      Editar Incidencia";
                cbIncidencia.Enabled = false;
                PanelEditar.Visible = true;
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                btnGuardar.Image = Resources.b3;
                //Utilerias.CambioBoton(btnGuardar, btnEliminar,btnGuardar, btnEditar);

                iOpcionAdmin = 2;


            }
        }


        private void LlenarComboTipoIncidencia(ComboBox cb,string Display,string Clave,int Opcion)
        {
            Incidencia objIncidencia = new Incidencia();
            objIncidencia.Descripcion = "";
            objIncidencia.TipoIncidencia = "";
            objIncidencia.UsuuMod = "";
            objIncidencia.PrguMod = "";
            //int iOpcion = 5;
            DataTable dtIncidencia = objIncidencia.ObtenerIncidenciaxTipo(objIncidencia, Opcion,"");
           
            List<string> ltCombo = new List<string>();
            foreach (DataRow row in dtIncidencia.Rows)
            {
                ltCombo.Add(row[Display].ToString());
            }

            ltCombo.Insert(0, "Seleccionar");
            cb.DataSource = ltCombo;
            cb.DisplayMember = Display;
            if (cb.Items.Count == 0)
            {
                cb.Enabled = false;
                cb.SelectedText = "Sin datos para Asignar";
            }

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dgvIncidencia.ClearSelection();

            lblAccion.Text = "      Nueva Asignación";
            panelTag.Visible = false;
            ckbEliminar.Checked = false;

            cbIncidencia.Enabled = true;
            LlenarComboTipoIncidencia(cbIncidencia,"Incidencia","cvincidencia",6);
            LlenarComboTipoIncidencia(cbTipoEditar, "Tipo", "cvtipo", 5);
            //  LlenarComboRepresenta(cbRepresentaEditar, 5);
            txtBuscarIncidencia.Text = "";
            PanelEditar.Visible = true;
            iOpcionAdmin = 1;
            //btnEditar.Visible = false;
            btnGuardar.Image = Resources.b8;

            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            strIncidencia = cbIncidencia.SelectedItem.ToString();


            lblAccion.Text = "       Asignar Tipo Incapacidad";

            Incidencia objIncidencia = new Incidencia();
            //objIncidencia.CVIncidencia = cbIncidencia.SelectedIndex;
            objIncidencia.Descripcion = strIncidencia;
            objIncidencia.TipoIncidencia = cbTipoEditar.SelectedItem.ToString();
           
            objIncidencia.PrguMod = this.Name;
            // objIncidencia.FhuMod = DateTime.Now;
            objIncidencia.UsuuMod = "vjiturburuv";
            try
            {
                DataTable response = objIncidencia.ObtenerIncidenciaxTipo(objIncidencia, iOpcionAdmin,strTipoIncidencia);
                ckbEliminar.Checked = false;

                if (response.Columns.Contains("INSERTAR"))
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignación Correcta");
                    timer1.Start();
                }
                else if (response.Columns.Contains("EXISTE"))
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Esta Asignación ya existe");
                    timer1.Start();
                }
                else if (response.Columns.Contains("ACTUALIZAR"))
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Cambio de Asignación Correcto");
                    timer1.Start();
                }
                else if (iOpcionAdmin == 4)
                {
                    panelTag.Visible = true;
                    panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                    lbMensaje.Text = "Registro Completo.";
                    timer1.Start();
                }

                // LlenarComboRepresenta(cbIncidencia, 6);


                LLenarGridIncapacidad(dgvIncidencia,"%","%");
            }
            catch (Exception ex)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentarlo más tarde.");
                timer1.Start();
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string Incidencia;
            string Tipo;

            if (txtBuscarIncidencia.Text == String.Empty)
            {

                Incidencia = "%";
            }
            else {
                Incidencia = txtBuscarIncidencia.Text;
            }

            if (cbTipo.SelectedIndex == 0)
            {
                Tipo = "%";
            }
            else {
                Tipo = cbTipo.SelectedItem.ToString();
            }

            LLenarGridIncapacidad(dgvIncidencia, Incidencia, Tipo);
           // LlenarComboTipoIncidencia(cbTipo, "Tipo", "cvtipo", 5);
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
