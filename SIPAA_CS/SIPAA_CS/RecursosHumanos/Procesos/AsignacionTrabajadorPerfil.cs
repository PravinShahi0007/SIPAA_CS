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
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class AsignacionTrabajadorPerfil : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        public int iOpcionAdmin;
        public List<int> ltFormasReg = new List<int>();
        public List<string> ltFormasxUsuario = new List<string>();
        public List<string> ltPermisos = new List<string>();

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

            if (dgvPlantilla.Columns.Count > 1)
            {

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

        private void llenarGridHorario(TrabajadorHorario objHorario)
        {
            ckbEliminar.Visible = false;
            if (dgvHorario.Columns.Count > 1)
            {

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
            panelEditar.Visible = false;
            try
            {
                DataTable dtResponse = objHorario.GestionHorario(iOpcionAdmin, objHorario);
                llenarGridHorario(objHorario);


                switch (dtResponse.Columns[0].ToString())
                {

                    case "INSERTAR_PLANTILLA":
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Plantilla Asignada con exito.");
                        timer1.Start();
                        break;

                }
            }
            catch (Exception ex)
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentarlo más tarde.");
                timer1.Start();
            }

        }

        private TrabajadorHorario AsignarObjeto()
        {

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
                cbDiaEntrada.Enabled = false;
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
                iOpcionAdmin = 2;
            }
        }

        private TrabajadorHorario RecuperarGrid(DataGridViewRow row)
        {

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
            if (ckbEliminar.Checked == true)
            {

                Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Borrar");
                iOpcionAdmin = 3;
            }
            else
            {

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
            cbDiaEntrada.Enabled = true;
            Utilerias.AsignarBotonResize(btnGuardar, new Size(sysW, sysH), "Guardar");
            LimpiarFormulario();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            bool bBandera = CamposVacios(panelEditar);

            if (bBandera != true)
            {

                cbDiaEntrada.Enabled = true;
                TrabajadorHorario objHorario = new TrabajadorHorario();
                objHorario.sIdTrab = TrabajadorInfo.IdTrab;
                objHorario.sHoraEntrada = mtxtEntradaTurno.Text;
                objHorario.sHoraSalidaTurno = mtxtSalida.Text;
                objHorario.sHoraComidaInicio = mtxtComidaInicio.Text;
                objHorario.sHoraComidaFin = mtxtComidaFin.Text;
                objHorario.iHorasTotalTrabajo = Convert.ToInt32(mtxtTiempoTrabajo.Text);
                objHorario.iTiempoComida = Convert.ToInt32(mtxtTiempoComida.Text);
                objHorario.iCvDia = Convert.ToInt32(cbDiaEntrada.SelectedValue);
                objHorario.iCvdiaSalidaTurno = Convert.ToInt32(cbDiaSalida.SelectedValue);
                objHorario.iCvdiaComidaInicio = Convert.ToInt32(cbComidaInicio.SelectedValue);
                objHorario.iCvdiaComidaFin = Convert.ToInt32(cbComidaFin.SelectedValue);
                objHorario.sUsuumod = "vjiturburuv"; //LoginInfo.IdTrab;
                objHorario.sPrgumod = this.Name;

                //TimeSpan tsEntrada = TimeSpan.Parse(objHorario.sHoraEntrada);
                //TimeSpan tsSalida = TimeSpan.Parse(objHorario.sHoraSalidaTurno);
                //TimeSpan horasTrabajo = tsSalida - tsEntrada;

                //TimeSpan tsComidaInicio = TimeSpan.Parse(objHorario.sHoraComidaInicio);
                //TimeSpan tsComidaFin = TimeSpan.Parse(objHorario.sHoraComidaFin);
                //TimeSpan MinComida = tsComidaInicio - tsComidaFin;

                //objHorario.iHorasTotalTrabajo = horasTrabajo.Hours;
                //objHorario.iTiempoComida = MinComida.Minutes;

                try
                {
                    DataTable dtresponse = objHorario.GestionHorario(iOpcionAdmin, objHorario);
                    LimpiarFormulario();
                    switch (dtresponse.Columns[0].ToString())
                    {

                        case "EXISTE":
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ese día ya se encuentra Asignado al Trabajador.");
                            timer1.Start();
                            break;
                        case "INSERTAR_NUEVO":
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Guardado con Exito.");
                            timer1.Start();
                            break;
                        case "ACTUALIZAR":
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Cambio Guardado con Exito.");
                            timer1.Start();
                            break;
                        case "ACTUALIZA_NUEVO":
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Guardado con Exito.");
                            timer1.Start();
                            break;
                        case "ELIMINAR":
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Registro Eliminado con Exito.");
                            timer1.Start();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentelo más tarde.");
                    timer1.Start();
                }
                llenarGridHorario(objHorario);

            }
            else
            {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Todos los Campos son Obligatorios. No pueden quedar vacios");
                timer1.Start();
            }

        }

        private bool CamposVacios(Panel pnl)
        {

            bool bBandera = false;


            foreach (Control ctrl in pnl.Controls)
            {

                string sTipoCtrl = ctrl.AccessibilityObject.ToString();

                if (sTipoCtrl.Contains("TextBox"))
                {

                    MaskedTextBox txt = (MaskedTextBox)ctrl;

                    if (txt.Text.Trim() == String.Empty || txt.Text.Trim() == ":")
                    {

                        bBandera = true;
                    }
                }
                else if (sTipoCtrl.Contains("ComboBox"))
                {

                    ComboBox cb = (ComboBox)ctrl;

                    if (cb.SelectedIndex == 0)
                    {

                        bBandera = true;
                    }
                }

            }

            return bBandera;

        }

        public void LimpiarFormulario()
        {

            cbDiaEntrada.SelectedIndex = 0;
            cbDiaSalida.SelectedIndex = 0;
            cbComidaInicio.SelectedIndex = 0;
            cbComidaFin.SelectedIndex = 0;

            mtxtEntradaTurno.Text = String.Empty;
            mtxtSalida.Text = String.Empty;
            mtxtComidaInicio.Text = String.Empty;
            mtxtComidaFin.Text = String.Empty;
            mtxtTiempoComida.Text = String.Empty;
            mtxtTiempoTrabajo.Text = String.Empty;
        }


        private void tabPlantillaHorario_Click(object sender, EventArgs e)
        {

        }

        private void cbDiaEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int iOut = 0;
            //if (Int32.TryParse(cbDiaEntrada.SelectedValue.ToString(), out iOut))
            //{

            //    TrabajadorHorario objTrabHr = AsignarObjeto();

            //    objTrabHr.sIdTrab = TrabajadorInfo.IdTrab;
            //    objTrabHr.iCvDia = Convert.ToInt32(cbDiaEntrada.SelectedValue);

            //    try
            //    {
            //        DataTable dtResponse = objTrabHr.GestionHorario(5, objTrabHr);

            //        switch (dtResponse.Columns[0].ToString())
            //        {

            //            case "EXISTE":
            //                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Ese día ya se encuentra Asignado al Trabajador.");
            //                cbDiaEntrada.SelectedIndex = 0;
            //                timer1.Start();
            //                break;

            //        }

            //    }
            //    catch (Exception ex)
            //    {

            //        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se pudo validar la información con el servidor. Intentelo más tarde.");
            //        timer1.Start();
            //    }

            //}
        }


        private void mtxtSalida_Leave(object sender, EventArgs e)
        {

            string s1 = mtxtSalida.Text + "00";
            string s2 = mtxtEntradaTurno.Text + "00";

            mtxtSalida.Text = s1;
            mtxtEntradaTurno.Text = s2;
            TimeSpan ts = new TimeSpan();
            if (TimeSpan.TryParse(mtxtSalida.Text, out ts) && TimeSpan.TryParse(mtxtEntradaTurno.Text, out ts))
            {
                TimeSpan tsEntrada = TimeSpan.Parse(mtxtEntradaTurno.Text);
                TimeSpan tsSalida = TimeSpan.Parse(mtxtSalida.Text);

                if (tsEntrada > tsSalida)
                {
                    int diasdif = (cbDiaEntrada.SelectedIndex + 1) - (cbDiaSalida.SelectedIndex);
                    TimeSpan horasTrabajo = tsEntrada - tsSalida;
                    mtxtTiempoTrabajo.Text = ((12 * diasdif) + horasTrabajo.Hours).ToString();
                    cbDiaSalida.SelectedValue = Convert.ToInt32(cbDiaEntrada.SelectedValue) + 1;
                }
                else
                {
                    TimeSpan horasTrabajo = tsSalida - tsEntrada;
                    mtxtTiempoTrabajo.Text = horasTrabajo.Hours.ToString();
                    cbDiaSalida.SelectedValue = cbDiaEntrada.SelectedValue;
                }
            }
            else
            {
                mtxtSalida.Text = "00:00";
                mtxtEntradaTurno.Text = "00:00";
            }

        }

        private void mtxtComidaInicio_Leave(object sender, EventArgs e)
        {
            string s1 = mtxtComidaInicio.Text + "00";
            string s2 = mtxtComidaFin.Text + "00";

            mtxtComidaInicio.Text = s1;
            mtxtComidaFin.Text = s2;


        }

        private void tabFormasRegistro_Click(object sender, EventArgs e)
        {
        }

        private void LlenarGridFormasRegistro(string sBusqueda)
        {

            if (dgvForReg.Columns.Count > 1)
            {
                dgvForReg.Columns.RemoveAt(0);
            }

            FormaReg objFr = new FormaReg();
            DataTable dtFormasRegistro = objFr.FormaReg_S(4, 0, sBusqueda, 0, "", "");
            dgvForReg.DataSource = dtFormasRegistro;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvForReg.Columns.Insert(0, imgCheckUsuarios);
            dgvForReg.Columns[0].HeaderText = "Selección";

            dgvForReg.Columns[0].Width = 55;
            dgvForReg.Columns[1].Visible = false;
            dgvForReg.Columns[2].Visible = true;
            dgvForReg.Columns[3].Visible = false;

            dgvForReg.ClearSelection();
        }

        private void dgvForReg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //for (int iContador = 0; iContador < dgvForReg.Rows.Count; iContador++)
            //{
            //    dgvForReg.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            //}


            if (dgvForReg.SelectedRows.Count != 0)
            {

                panelPermisos.Enabled = true;
                DataGridViewRow row = this.dgvForReg.SelectedRows[0];

                int iCvforma = Convert.ToInt32(row.Cells[1].Value.ToString());

                ltFormasReg.Add(iCvforma);
                if (row.Cells[0].Tag.ToString() == "check")
                {

                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";

                }
                else
                {
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    row.Cells[0].Tag = "check";

                }
            }

        }


        private void AsignarFormas(string sIdtrab)
        {

            FormaReg objfr = new FormaReg();
            ltFormasxUsuario = objfr.FormasxUsuario(sIdtrab, 0, 4,"","");

            // Utilerias.ApagarControlxPermiso(btnGuardar, "Actualizar", ltPermisos);


            for (int iContador = 0; iContador < dgvForReg.Rows.Count; iContador++)
            {
                string sCvForma = dgvForReg.Rows[iContador].Cells[1].Value.ToString();

                if (ltFormasxUsuario.Contains(sCvForma))
                {
                    dgvForReg.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    dgvForReg.Rows[iContador].Cells[0].Tag = "check";
                }
                else
                {
                    dgvForReg.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgvForReg.Rows[iContador].Cells[0].Tag = "uncheck";
                }
            }
        }

        private void tabAsignacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabAsignacion.SelectedIndex) {

                case 1:
                    LlenarGridFormasRegistro("%");
                    AsignarFormas(TrabajadorInfo.IdTrab);
                    break;
            }

            
        }

        private void btnBuscarForReg_Click(object sender, EventArgs e)
        {
            string sBusqueda = "";
            if (txtBuscarForReg.Text != String.Empty)
            {
                 sBusqueda = txtBuscarForReg.Text;
            }
            else
            {
                 sBusqueda = "%";
            }

            LlenarGridFormasRegistro(sBusqueda);
        }

        private void btnGuardarForReg_Click(object sender, EventArgs e)
        {
            panelPermisos.Enabled = false;
            try
            {
                string UsuuMod = "vjiturburuv";
                string PrguMod = this.Name;
                FormaReg objFr = new FormaReg();

                foreach (int cv in ltFormasReg)
                {
                    objFr.FormasxUsuario(TrabajadorInfo.IdTrab,cv,1, UsuuMod, PrguMod);
                }

                ltFormasReg.Clear();

                Utilerias.ControlNotificaciones(panelTagForReg, lbMensajeForReg, 1, "Asignaciones Guardadas Correctamente");
                timer1.Start();
                AsignarFormas(TrabajadorInfo.IdTrab);


            }
            catch (Exception ex)
            {
                Utilerias.ControlNotificaciones(panelTagForReg, lbMensajeForReg, 3, "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.");
                timer1.Start();
                AsignarFormas(TrabajadorInfo.IdTrab);
            }
        }
    }
}
