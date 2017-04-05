using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
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
using static SIPAA_CS.App_Code.SonaCompania;

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class AsignacionTrabajadorPerfil : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public int iOpcionAdmin;
        public AsignacionTrabajadorPerfil()
        {
            InitializeComponent();
        }

        private void AsignacionTrabajadorPerfil_Load(object sender, EventArgs e)
        {

            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            lbIdTrab.Text = TrabajadorInfo.IdTrab;
            lbNombre.Text = TrabajadorInfo.Nombre;

            PlantillaDetalle objPlantilla = new PlantillaDetalle();
            Utilerias.llenarComboxDataTable(cbPlantilla, objPlantilla.cbplantilla(5), "Clave", "Descripción");
            Utilerias.llenarComboxDataTable(cbDiaEntrada, objPlantilla.cbdias(6), "Clave", "Descripción");
            Utilerias.llenarComboxDataTable(cbDiaSalida, objPlantilla.cbdias(6), "Clave", "Descripción");
            Utilerias.llenarComboxDataTable(cbComidaInicio, objPlantilla.cbdias(6), "Clave", "Descripción");
            Utilerias.llenarComboxDataTable(cbComidaFin, objPlantilla.cbdias(6), "Clave", "Descripción");


            TrabajadorHorario objHorario = AsignarObjeto();

            llenarGridHorario(objHorario);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }




        private void cbPlantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            int iOut = 0;
            if (Int32.TryParse(cbPlantilla.SelectedValue.ToString(), out iOut))
            {
                LlenarGridPlantilla(4, Int32.Parse(Convert.ToString(cbPlantilla.SelectedValue)));
            }
        }

        private void LlenarGridPlantilla(int iopcion, int icvplantilla)
        {

            if (dgvPlantilla.Columns.Count > 1) {

                dgvPlantilla.Columns.RemoveAt(0);
            }

            dgvPlantilla.DataSource = null;
            PlantillaDetalle objPlantilla = new PlantillaDetalle();
            DataTable dtiplandet = objPlantilla.dgvplantillaDet(iopcion, icvplantilla);
            dgvPlantilla.DataSource = dtiplandet;

            dgvPlantilla.Columns["cvplantilla"].Visible = false;
            dgvPlantilla.Columns["descplantilla"].Visible = false;
            dgvPlantilla.Columns["cvdia"].Visible = false;
            dgvPlantilla.Columns["cvddiasaltur"].Visible = false;
            dgvPlantilla.Columns["cvdiasalcom"].Visible = false;
            dgvPlantilla.Columns["cvdiaregcom"].Visible = false;
            dgvPlantilla.Columns["stexiste"].Visible = false;

            dgvPlantilla.Columns["Día"].HeaderText = "Día Entrada";
            dgvPlantilla.Columns["HrEntTurno"].HeaderText = "Hora Entrada";
            dgvPlantilla.Columns["DíaSalTurno"].HeaderText = "Día Salida";
            dgvPlantilla.Columns["HrSalTurno"].HeaderText = "Hora Salida";
            dgvPlantilla.Columns["DíaSalComer"].HeaderText = "Comida Inicio";
            dgvPlantilla.Columns["HrSalComer"].HeaderText = "Hora Inicio";
            dgvPlantilla.Columns["DíaRegComida"].HeaderText = "Comida Fin";
            dgvPlantilla.Columns["HrRegComida"].HeaderText = "Hora Fin";
            dgvPlantilla.Columns["TotJornada"].HeaderText = "Horas Trabajo";
            dgvPlantilla.ClearSelection();

        }

        private void llenarGridHorario(TrabajadorHorario objHorario) {
            ckbEliminar.Visible = false;
            if (dgvHorario.Columns.Count > 1) {

                dgvHorario.Columns.RemoveAt(0);
            }

            iOpcionAdmin = 4;
            DataTable dtHorario = objHorario.GestionHorario(iOpcionAdmin, objHorario);
            dgvHorario.DataSource = dtHorario;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvHorario.Columns.Insert(0, imgCheckUsuarios);
            dgvHorario.Columns[0].HeaderText = "Selección";

            dgvHorario.Columns[1].Visible = false;
        }
        private void btnGuardarPlantilla_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            TrabajadorHorario objHorario = AsignarObjeto();
            iOpcionAdmin = 1;
            objHorario.iCvPlantilla = Convert.ToInt32(cbPlantilla.SelectedValue.ToString());

            try
            {
                DataTable dtResponse = objHorario.GestionHorario(iOpcionAdmin, objHorario);
                llenarGridHorario(objHorario);


                if (dtResponse.Columns.Contains("PLANTILLA"))
                {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Plantilla Asignada con exito.");
                    timer1.Start();

                }
                else if (dtResponse.Columns.Contains("NUEVO")) {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Guardado con Exito.");
                    timer1.Start();
                }


            }
            catch (Exception ex) {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentarlo más tarde.");
                timer1.Start();
            }

        }

        private TrabajadorHorario AsignarObjeto() {

            TrabajadorHorario objHorario = new TrabajadorHorario();
            objHorario.sIdTrab = TrabajadorInfo.IdTrab;
            objHorario.iCvPlantilla = 0;
            objHorario.iCvDia = 0;
            objHorario.sHoraEntrada = "00:00";
            objHorario.iCvdiaSalidaTurno = 0;
            objHorario.sHoraSalidaTurno = "00:00";
            objHorario.iTiempoComida = 0;
            objHorario.iCvdiaComidaInicio = 0;
            objHorario.sHoraComidaInicio = "00:00";
            objHorario.iCvdiaComidaFin = 0;
            objHorario.sHoraComidaFin = "00:00";
            objHorario.iHorasTotalTrabajo = 0;
            objHorario.sUsuumod = "vjiturburuv";
            objHorario.sPrgumod = this.Name;

            return objHorario;
        }

        private void dgvHorario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvHorario.Rows.Count; iContador++)
            {
                dgvHorario.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvHorario.SelectedRows.Count != 0)
            {
                panelEditar.Visible = true;
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Editar");
                DataGridViewRow row = this.dgvHorario.SelectedRows[0];

                string sDiaEntrada = row.Cells[2].Value.ToString();
                //string Desc = row.Cells["DESCRIPCION"].Value.ToString();
                TrabajadorHorario objHorario = RecuperarGrid(row);

                mtxtEntradaTurno.Text = objHorario.sHoraEntrada;
                mtxtSalida.Text = objHorario.sHoraSalidaTurno;
                mtxtComidaInicio.Text = objHorario.sHoraComidaInicio;
                mtxtComidaFin.Text = objHorario.sHoraComidaFin;
                mtxtTiempoComida.Text = objHorario.iTiempoComida.ToString();
                mtxtTiempoTrabajo.Text = objHorario.iHorasTotalTrabajo.ToString();

                cbDiaEntrada.SelectedValue = objHorario.iCvDia;
                cbDiaSalida.SelectedValue = objHorario.iCvdiaSalidaTurno;
                cbComidaInicio.SelectedValue = objHorario.iCvdiaComidaInicio;
                cbComidaFin.SelectedValue = objHorario.iCvdiaComidaFin;

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                // TrabajadorInfo.IdTrab = row.Cells["idtrab"].Value.ToString();

            }
        }

        private TrabajadorHorario RecuperarGrid(DataGridViewRow row) {

            TrabajadorHorario objHorario = new TrabajadorHorario();

            objHorario.sHoraEntrada = row.Cells[3].Value.ToString();
            objHorario.sHoraSalidaTurno = row.Cells[5].Value.ToString();
            objHorario.sHoraComidaInicio = row.Cells[8].Value.ToString();
            objHorario.sHoraComidaFin = row.Cells[10].Value.ToString();
            objHorario.iHorasTotalTrabajo = Convert.ToInt32(row.Cells[11].Value.ToString());
            objHorario.iTiempoComida = Convert.ToInt32(row.Cells[6].Value.ToString());

            objHorario.iCvDia = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[2].Value.ToString()));
            objHorario.iCvdiaSalidaTurno = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[4].Value.ToString()));
            objHorario.iCvdiaComidaInicio = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[7].Value.ToString()));
            objHorario.iCvdiaComidaFin = Convert.ToInt32(Enum.Parse(typeof(Utilerias.DiasSemana), row.Cells[9].Value.ToString()));

            return objHorario;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true) {

                Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Borrar");
                iOpcionAdmin = 3;
            }
            else {

                Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Editar");
                iOpcionAdmin = 2;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            iOpcionAdmin = 1;
            panelEditar.Visible = true;
            ckbEliminar.Visible = false;
            ckbEliminar.Checked = false;
            Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Guardar");
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            TrabajadorHorario objHorario = new TrabajadorHorario();

            objHorario.sHoraEntrada = mtxtEntradaTurno.Text;
            objHorario.sHoraSalidaTurno = mtxtSalida.Text;
            objHorario.sHoraComidaInicio = mtxtComidaInicio.Text;
            objHorario.sHoraComidaFin = mtxtComidaFin.Text;
            objHorario.iHorasTotalTrabajo = Convert.ToInt32(mtxtTiempoTrabajo.Text);
            objHorario.iTiempoComida = Convert.ToInt32(mtxtTiempoComida);
            objHorario.iCvDia = Convert.ToInt32(cbDiaEntrada.SelectedValue);
            objHorario.iCvdiaSalidaTurno = Convert.ToInt32(cbDiaSalida.SelectedValue);
            objHorario.iCvdiaComidaInicio = Convert.ToInt32(cbComidaInicio.SelectedValue);
            objHorario.iCvdiaComidaFin = Convert.ToInt32(cbComidaFin.SelectedValue);



        }

        private bool CamposVacios(Panel pnl){

            bool bBandera = false;


            foreach (Control ctrl in pnl.Controls) {

                string sTipoCtrl = ctrl.AccessibilityObject.ToString();

                if (sTipoCtrl.Contains("TextBox")) {

                    TextBox txt = (TextBox)ctrl;
                   

                }

            }

            return bBandera;
            
            }

        private void tabPlantillaHorario_Click(object sender, EventArgs e)
        {

        }
    }
}
