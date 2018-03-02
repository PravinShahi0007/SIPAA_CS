using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIPAA_CS.RecursosHumanos.Reportes;
using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Properties;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class VacIncPermHrEsp2 : Form
    {
        public VacIncPermHrEsp2()
        {
            InitializeComponent();
        }
        string NoTrabajador;
        string svalidacampos;
        int iprespuesta;

        SonaTrabajador contenedorempleados = new SonaTrabajador();
        Utilerias util = new Utilerias();
        ConcepInc ConceptoIncidencias = new ConcepInc();
        Incidencia TipoIncidencias = new Incidencia();
        DiasEspeciales DiasEspeciales = new DiasEspeciales();
        SonaCompania2 oCompañia = new SonaCompania2();
        SonaTipoNomina oTiponomina = new SonaTipoNomina();
        SonaUbicacion oUbicacion = new SonaUbicacion();
        SonaPuesto puestos = new SonaPuesto();
        SonaDepartamento departamentos = new SonaDepartamento();
        SonaCompania objCia = new SonaCompania();
        string sgIdTrab = "%";
        string sgIdCompania = "%";
        string sgIdArea = "%";
        string sgIdPuesto = "%";
        string sgIdDepartamento = "%";
        string sgIdUbicacion = "%";
        string sgIdTipoNomina = "%";

        //***********************************************************************************************
        //Autor: José Luis Alvarez Delgado
        //Fecha creación:14-09-2017     Última Modificacion: 
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        private void cbTiponomina_DropDown(object sender, EventArgs e)
        {
            if (cbCompania.SelectedValue.ToString() != "" | cbCompania.SelectedValue.ToString() != "Seleccionar")
            {
                //llenado de combo tipo nomina
                DataTable dtTipoNomina = oTiponomina.obtTipoNomina(5, Convert.ToInt32(cbCompania.SelectedValue.ToString()), 0, "");
                cbTiponomina.DataSource = dtTipoNomina;
                cbTiponomina.DisplayMember = "Descripción";
                cbTiponomina.ValueMember = "Clave";
                /////////////08112017
                cbTiponomina.Text = "Seleccionar";
            }
            else
            {
                MessageBox.Show("Debes elejir la Compañia", "SIPAA", MessageBoxButtons.OK);
            }
        }

        private void cbConcepto_DropDown(object sender, EventArgs e)
        {
            cbTipo.Text = string.Empty;
        }

        private void cbTipo_DropDown(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbConcepto.Text))
            {
                Incidencia objIncidencia = new Incidencia();
                objIncidencia.CVIncidencia = Int32.Parse(cbConcepto.SelectedValue.ToString());
                objIncidencia.Descripcion = "";
                objIncidencia.CVTipo = 0;
                objIncidencia.TipoIncidencia = "";
                objIncidencia.Estatus = "";
                objIncidencia.UsuuMod = "";
                objIncidencia.PrguMod = "";
                objIncidencia.Estatus = "";
                int Opcion = 8;

                DataTable dtIncidencia = TipoIncidencias.ObtenerIncidenciaxTipo(objIncidencia, Opcion);
                cbTipo.DataSource = dtIncidencia;
                cbTipo.DisplayMember = "Tipo";
                cbTipo.ValueMember = "cvtipo";
                /////////////08112017
                cbTipo.Text = "Seleccionar";
            }
        }

        private void cbAreas_DropDown(object sender, EventArgs e)
        {
            if (cbCompania.SelectedValue.ToString() != "" | cbCompania.SelectedValue.ToString() != "Seleccionar")
            {
                //llenado de combo Areas
                DataTable dtPlantel = objCia.ObtenerPlantelxCompania(9, cbCompania.SelectedValue.ToString(), "", "");
                cbAreas.DataSource = dtPlantel;
                cbAreas.DisplayMember = "Descripción";
                cbAreas.ValueMember = "Clave";
                /////////////08112017
                cbAreas.Text = "Seleccionar";
            }
            else
            {
                MessageBox.Show("Debes elejir la Compañia", "SIPAA", MessageBoxButtons.OK);
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cbConcepto.Text = "";
            cbTipo.Text = "";
            txtReferencia.Text = "";
            txtSubsidio.Text = "0";
            mtxtHoraEntrada.Text = "00:00:00";
            mtxtHoraSalida.Text = "00:00:00";
            cbConcepto.Enabled = true;
            cbTipo.Enabled = true;
            dtpFechaInical.Enabled = true;
            btnInsertar.Visible = false;

            for (int iContador = 0; iContador < dgvEmpleados.Rows.Count; iContador++)
            {
                dgvEmpleados.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvEmpleados.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvEmpleados.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                NoTrabajador = row.Cells["NoEmpleado"].Value.ToString();

                int icolumnas = dgvInc.ColumnCount;
                if (icolumnas > 9)
                {
                    dgvInc.Columns.RemoveAt(0);
                }

                llenarGridDiasEsp(NoTrabajador);
                dgvInc.Columns[0].Width = 80;
                dgvInc.Columns[1].Visible=false; //cvinc
                dgvInc.Columns[2].Width = 155;
                dgvInc.Columns[3].Visible=false; //cvtipo
                dgvInc.Columns[4].Width = 155;
                dgvInc.Columns[5].Width = 85;
                dgvInc.Columns[6].Width = 85;
                dgvInc.Columns[7].Width = 85;
                dgvInc.Columns[8].Width = 85;
                dgvInc.Columns[9].Width = 40;
                dgvInc.Columns[10].Width = 135;
                dgvInc.Columns[11].Width = 68;
            }
        }

        private void dgvInc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int iContador = 0; iContador < dgvInc.Rows.Count; iContador++)
            {
                dgvInc.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }
            // Debe cambiar de Icono
            util.ChangeButton(btnInsertar, 2, false);
            btnInsertar.Visible = true;
            ckbEliminar.Visible = true;
            ckbEliminar.Checked = false;
            btnInsertar.Text = "u";

            if (dgvInc.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvInc.SelectedRows[0];
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                txtCvInc.Text = row.Cells["CvInc"].Value.ToString();
                cbConcepto.Text = row.Cells["Incidencia"].Value.ToString();
                cbConcepto.Enabled = false;          
                txtCvTipo.Text = row.Cells["CvTipo"].Value.ToString();
                cbTipo.Text = row.Cells["Tipo"].Value.ToString();
                cbTipo.Enabled = false;
                dtpFechaInical.Text = row.Cells["Fecha Inicial"].Value.ToString();
                dtpFechaInical.Enabled = false;
                dtpFechaFinal.Text = row.Cells["Fecha Final"].Value.ToString();

                if (row.Cells["Hora Entrada"].Value.ToString()=="")
                {
                    mtxtHoraEntrada.Text = "00:00";
                }
                else
                {
                    mtxtHoraEntrada.Text = row.Cells["Hora Entrada"].Value.ToString();
                }

                if (row.Cells["Hora Salida"].Value.ToString()=="")
                {
                    mtxtHoraSalida.Text = "00:00";
                }
                else
                {
                    mtxtHoraSalida.Text = row.Cells["Hora Salida"].Value.ToString();
                }                                
                txtDias.Text = row.Cells["Días"].Value.ToString();
                txtReferencia.Text = row.Cells["Referencia"].Value.ToString();
                txtSubsidio.Text = row.Cells["Subsidio %"].Value.ToString();
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar empleados
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //btnAsignar.Text = "Asignar Datos";
            btnInsertar.Text = "a";
            btnInsertar.Visible = true;
            ckbEliminar.Visible = false;
            util.ChangeButton(btnInsertar, 1, false);
            cbConcepto.Enabled = true;
            cbTipo.Enabled = true;
            dtpFechaInical.Enabled = true;            

            if (Convert.ToInt32(cbEmpleados.SelectedIndex.ToString()) <= 0 & Convert.ToInt32(cbCompania.SelectedIndex.ToString()) <=0 & Convert.ToInt32(cbAreas.SelectedIndex.ToString()) <= 0 
                & Convert.ToInt32(cbPuestos.SelectedIndex.ToString()) <= 0 & Convert.ToInt32(cbDepartamentos.SelectedIndex.ToString()) <=0 
                & Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) <= 0 & Convert.ToInt32(cbTiponomina.SelectedIndex.ToString()) <= 0)
            {
                MessageBox.Show("Debe seleccionar por lo menos un filtro de busqueda", "SIPAA", MessageBoxButtons.OK);
                cbEmpleados.Focus();
            }
            else
            {
                ckbimprimir.Visible = true;
                ckbimprimir.Checked = false;
                dgvInc.Columns.Clear();
                //llena grid Con Filtros
                string fIdTrab = "%";
                string fIdCompania = "%";
                string fIdArea = "%";
                string fIdPuesto = "%";
                string fIdDepartamento = "%";
                string fIdUbicacion = "%";
                string fIdTipoNomina = "%";
                sgIdTrab = "%";
                sgIdCompania = "%";
                sgIdArea = "%";
                sgIdPuesto = "%";
                sgIdDepartamento = "%";
                sgIdUbicacion = "%";
                sgIdTipoNomina = "%";

                if (Convert.ToInt32(cbEmpleados.SelectedIndex.ToString()) > 0)
                {
                    fIdTrab = cbEmpleados.SelectedValue.ToString();
                    sgIdTrab = fIdTrab;
                }
                if (Convert.ToInt32(cbCompania.SelectedIndex.ToString()) > 0)
                {
                    fIdCompania = cbCompania.SelectedValue.ToString();
                    sgIdCompania = fIdCompania;
                }
                if (Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) > 0)
                {
                    fIdUbicacion = cbUbicacion.SelectedValue.ToString();
                    sgIdUbicacion = fIdUbicacion;
                }
                if (Convert.ToInt32(cbAreas.SelectedIndex.ToString()) > 0)
                {
                    fIdArea = cbAreas.SelectedValue.ToString();
                    sgIdArea = fIdArea;
                }
                if (cbTiponomina.SelectedIndex > 0 || cbTiponomina.Text != "Seleccionar")
                {
                    fIdTipoNomina = cbTiponomina.SelectedValue.ToString();
                    sgIdTipoNomina = fIdTipoNomina;
                }
                if (Convert.ToInt32(cbPuestos.SelectedIndex.ToString()) > 0)
                {
                    fIdPuesto = cbPuestos.SelectedValue.ToString();
                    sgIdPuesto = fIdPuesto;
                }
                if (Convert.ToInt32(cbDepartamentos.SelectedIndex.ToString()) > 0)
                {
                    fIdDepartamento = cbDepartamentos.SelectedValue.ToString();
                    sgIdDepartamento = fIdDepartamento;
                }

                int icolumnas =dgvEmpleados.ColumnCount;
                if (icolumnas > 2)
                {
                    dgvEmpleados.Columns.RemoveAt(0);
                }

                fgridEmpleados(1,fIdTrab,fIdCompania,fIdArea,fIdPuesto,fIdDepartamento,fIdUbicacion,fIdTipoNomina);
                dgvEmpleados.Columns[0].Width =85;
                dgvEmpleados.Columns[1].Width =100;
                dgvEmpleados.Columns[2].Width =300;
                dgvEmpleados.Columns[3].Visible = false;
                dgvEmpleados.Columns[4].Visible = false;
                dgvEmpleados.Columns[5].Visible = false;

                if (dgvEmpleados.Rows.Count==0)
                {
                    ckbimprimir.Visible = false;
                }

                //Guajolocombo Conceptos Incidencia
                /////////CbConceptoIncidencia(7, 0, "", 0, 0, 0, 0, "", "");
                txtSubsidio.Text = "0";
                //txtDias.Text = "1";
                if (Convert.ToInt32(txtDias.Text) < 1)
                {
                    txtDias.Text = "1";
                }
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            ///Aqui hay que asignar los valores en la tabla
            //para insertar registro nuevo 

            fvalidacampos();

            if (DateTime.Parse(dtpFechaInical.Text) > DateTime.Parse(dtpFechaFinal.Text))
            {
                MessageBox.Show("Error en las Fechas, Verifique.", "SIPPA", MessageBoxButtons.OK);
                dtpFechaInical.Focus();
            }

            ///Si capturaron hra de entrada y salida
            if (mtxtHoraEntrada.Text != "00:00" & mtxtHoraSalida.Text != "00:00")
            {
                DateTime HI = Convert.ToDateTime(mtxtHoraEntrada.Text);
                DateTime HF = Convert.ToDateTime(mtxtHoraSalida.Text);

                if (HI > HF)
                {
                    //MessageBox.Show("Error en las Horas, Verifique.", "SIPPA", MessageBoxButtons.OK);
                    svalidacampos = "Error en las Horas, Verifique.";
                    mtxtHoraEntrada.Focus();
                }
            }

            //Capturaron solo hra de salida 
            if (mtxtHoraEntrada.Text == "00:00" & mtxtHoraSalida.Text != "00:00")
            {
                svalidacampos = "Debe capturar una Hora de Entrada, Verifique.";
                mtxtHoraEntrada.Focus();
            }
                      
            if (svalidacampos != "0")
            {
                DialogResult result = MessageBox.Show(svalidacampos, "SIPAA", MessageBoxButtons.OK);
            }
            else
            {
                string usuumod = LoginInfo.IdTrab;
                string prgumod = this.Name;
                int iop = 0;

                if (btnInsertar.Text != "a") //"Asignar Datos"
                {
                    if (btnInsertar.Text == "u") {iop = 2; }
                    if (btnInsertar.Text == "d") {iop = 3; }

                    foreach (DataGridViewRow row in dgvEmpleados.Rows)
                    {
                        try
                        {
                            if (row.Selected)
                            {
                                if (iop==2)
                                {
                                    //Capturaron solo hra de entrada
                                    if (mtxtHoraEntrada.Text != "00:00" & mtxtHoraSalida.Text == "00:00")
                                    {
                                        int iPuesto = Convert.ToInt32(row.Cells[5].Value.ToString());
                                        if (iPuesto != 287 & iPuesto != 288 & iPuesto != 289 & iPuesto != 290)
                                        {
                                            MessageBox.Show("Por el Puesto del trabajador  " + Convert.ToInt32(row.Cells[1].Value.ToString()) + ", debe capturar una Hora de Salida, Verifique.", "SIPPA", MessageBoxButtons.OK);
                                            //mtxtHoraSalida.Focus();
                                            break;
                                        }
                                    }
                                }

                                int iIdTrab = Convert.ToInt32(row.Cells[1].Value.ToString());
                                fInsDiasEsp(iIdTrab, iop, Convert.ToInt32(txtCvInc.Text.ToString()), Convert.ToInt32(txtCvTipo.Text.ToString()), dtpFechaInical.Text.Trim(),
                                dtpFechaFinal.Text.Trim(), Convert.ToInt32(txtDias.Text), mtxtHoraEntrada.Text.Trim(), mtxtHoraSalida.Text.Trim(), txtReferencia.Text, 4,
                                Convert.ToInt32(txtSubsidio.Text), 0, usuumod.ToString(), prgumod.ToString(), 0, 0);

                                panelTag.Visible = true;
                                timer1.Start();

                                int icolumnas = dgvInc.ColumnCount;
                                if (icolumnas > 9)
                                {
                                    dgvInc.Columns.RemoveAt(0);
                                }
                                llenarGridDiasEsp(NoTrabajador);
                            }
                        }
                        catch (Exception ex)
                        {
                            // MessageBox.Show(ex.ToString());
                            MessageBox.Show("Problemas con el Empleado  "+ Convert.ToInt32(row.Cells[1].Value.ToString())+ "  " + row.Cells[2].Value.ToString() + "  Verifique los datos.", "SIPAA");
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dgvEmpleados.Rows)
                    {
                        try
                        {
                            //Capturaron solo hra de entrada
                            if (mtxtHoraEntrada.Text != "00:00" & mtxtHoraSalida.Text == "00:00")
                            {
                                int iPuesto =Convert.ToInt32(row.Cells[5].Value.ToString());
                                if (iPuesto!=287 & iPuesto!=288 & iPuesto!=289 & iPuesto!=290)
                                {
                                    MessageBox.Show ("Por el Puesto del trabajador  " + Convert.ToInt32(row.Cells[1].Value.ToString())+ ", debe capturar una Hora de Salida, Verifique.", "SIPPA", MessageBoxButtons.OK);
                                    //mtxtHoraSalida.Focus();
                                    break;
                                }
                            }

                            int iIdTrab = Convert.ToInt32(row.Cells[1].Value.ToString());
                            fInsDiasEsp(iIdTrab, 1, Convert.ToInt32(cbConcepto.SelectedValue.ToString()), Convert.ToInt32(cbTipo.SelectedValue.ToString()), dtpFechaInical.Text.Trim(),
                            dtpFechaFinal.Text.Trim(), Convert.ToInt32(txtDias.Text), mtxtHoraEntrada.Text.Trim(), mtxtHoraSalida.Text.Trim(), txtReferencia.Text, 4,
                            Convert.ToInt32(txtSubsidio.Text), 0, usuumod.ToString(), prgumod.ToString(), 0, 0);
                            panelTag.Visible = true;
                            timer1.Start();
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                            MessageBox.Show("Problemas con el Empleado  " + Convert.ToInt32(row.Cells[1].Value.ToString())+"  "+ row.Cells[2].Value.ToString()+ "  Verifique sus datos.", "SIPAA");
                        }
                    }
                    //frecargar();
                }
            }
        }

        //boton imprimir
        private void btnImprimirDetalle_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////// JLA 01/02/2018
            DataTable dtEmpleadosDiasEsp = contenedorempleados.obtenerempleadosydiasesp(sgIdTrab, sgIdCompania, 
                sgIdArea, sgIdPuesto, sgIdDepartamento, sgIdUbicacion, sgIdTipoNomina, dtpfechainicio.Text, dtpfechafin.Text);
            switch (dtEmpleadosDiasEsp.Rows.Count)
            {
                case 0:
                    DialogResult result = MessageBox.Show("Sin Resultados para el Reporte de Dias Especiales", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtEmpleadosDiasEsp, "SIPAA_CS.RecursosHumanos.Reportes", "RepEmpleadosDiasEsp.rpt");
                    ReportDoc.SetParameterValue("FechaInicial", dtpfechainicio.Value.Date);
                    ReportDoc.SetParameterValue("FechaFinal", dtpfechafin.Value.Date);
                    ReportDoc.SetParameterValue("NomCompania", cbCompania.Text);
                    form.RptDoc = ReportDoc;
                    form.Show();
                    break;
            }
            oculta();
            ckbimprimir.Visible = false;

            /////////////////////////////////////////////
        }

        //boton minimizar        
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void VacIncPermHrEsp2_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }

            oculta();
            ckbimprimir.Visible = false;

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            //Rezise de la Forma
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //llama el tooltip
            ftooltip();

            //llenado de combo compañias
            Utilerias.llenarComboxDataTable(cbCompania, oCompañia.obtCompania2(5, ""), "Clave", "Descripción");

            //llenado de combo ubicaciones
            Utilerias.llenarComboxDataTable(cbUbicacion, oUbicacion.obtenerSonaUbicacion("", 6), "Clave", "Descripción");

            //Combo Puestos
            DataTable dtpuestos = puestos.obtptos(4, "");
            cbPuestos.DataSource = dtpuestos;
            cbPuestos.DisplayMember = "Descripción";
            cbPuestos.ValueMember = "Clave";
            cbPuestos.Text = "Seleccionar";

            //Combo Departamentos
            DataTable dtdepartamentos = departamentos.obtdepto(4, "");
            cbDepartamentos.DataSource = dtdepartamentos;
            cbDepartamentos.DisplayMember = "Descripción";
            cbDepartamentos.ValueMember = "Clave";
            cbDepartamentos.Text = "Seleccionar";

            //Combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();

            //Guajolocombo Conceptos Incidencia
            CbConceptoIncidencia(7, 0, "", 0, 0, 0, 0, "", "");
            txtDias.Text = "1";

        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Buscar Registros");
            toolTip1.SetToolTip(this.btnInsertar, "Asignar/Modificar/Eliminar Datos");
        }

        //FUNCION que Llena el Grid de Empleados
        private void fgridEmpleados(int popcion, string pidtrab, string pidcompania, string pidarea, string pidpuesto,
            string piddepartamento, string pidubicacion, string pidtiponomina)
        {
            DataTable dtempleados = contenedorempleados.obtenerempleadosxfiltros(popcion, pidtrab, pidcompania, pidarea, pidpuesto,
            piddepartamento, pidubicacion, pidtiponomina);
            dgvEmpleados.DataSource = dtempleados;

            Utilerias.AgregarCheck(dgvEmpleados, 0);
            dgvEmpleados.ClearSelection();
        }

        private void llenarGridDiasEsp(string NoTrabajador)
        {
            DiasEspeciales objDia = new DiasEspeciales();

            objDia.sIdTrab = NoTrabajador;
            DataTable dtdias = objDia.ObtenerDiasEspecialesxTrabajador(objDia, 4);
            dgvInc.DataSource = dtdias;

            Utilerias.AgregarCheck(dgvInc, 0);
            dgvInc.ClearSelection();
                
        }

        private void CbConceptoIncidencia(int p_opcion, int p_cvIncidencia, string p_descripcion, int p_orden, int p_stgenera, int p_strepresenta, int p_stincidencia, string p_usuumod, string p_prgumodr)
        {
            DataTable dtIncidencia = ConceptoIncidencias.ConcepInc_S(p_opcion, p_cvIncidencia, p_descripcion, p_orden, p_stgenera, p_strepresenta, p_stincidencia, p_usuumod, p_prgumodr);
            cbConcepto.DataSource = dtIncidencia;
            cbConcepto.DisplayMember = "Descripcion";
            cbConcepto.ValueMember = "Clave";
        }

        private void fInsDiasEsp(int iIdTrab, int iOpcion, int iCvIncidencia, int iCvTipo, string fFechaInicio, string fFechaFin, int iDias,
            string tpHoraentrada, string tpHoraSalida, string sReferencia, int iOrden, int iSubsidio, int iIdtrabrys, string spusuumod, string spprgumod, int iIdCompania, int iIPlanta)
        {
            iprespuesta = DiasEspeciales.InsertarDiasEspecialesxTrabajador(iIdTrab, iOpcion, iCvIncidencia, iCvTipo, fFechaInicio, fFechaFin, iDias, tpHoraentrada, tpHoraSalida,
                sReferencia, iOrden, iSubsidio, iIdtrabrys, spusuumod, spprgumod, iIdCompania, iIPlanta);

            switch (iprespuesta.ToString())
            {
                case "1":
                    lblMensaje.Text = "La Asignación se llevo a cabo correctamente";
                    break;
                case "2":
                    lblMensaje.Text = "La Actualización se llevo a cabo correctamente";
                    break;
                case "3":
                    lblMensaje.Text = "Se Elimino el registro correctamente";
                    break;
                case "":
                    lblMensaje.Text = "Problemas al realizar la Operación, avise a Sistemas.";
                    break;
            }
        }

        //funcion validar campos
        protected string fvalidacampos()
        {
            if (cbConcepto.Text == "") { svalidacampos = "Selecione Concepto"; cbConcepto.Focus(); }

            else if (cbTipo.Text == "") { svalidacampos = "Selecione Tipo"; cbTipo.Focus(); }

            //else if (txtReferencia.Text == "") { svalidacampos = "Capture Referencia"; txtReferencia.Focus(); }

            else if (dtpFechaInical.Text == "") { svalidacampos = "Selecione Fecha Inicial"; dtpFechaInical.Focus(); }

            else { svalidacampos = "0"; }
            return svalidacampos;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void frecargar()
        {
            VacIncPermHrEsp2 recargar = new VacIncPermHrEsp2();
            recargar.Show();
            this.Close();
        }

        private void txtDias_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtDias.Text) > 1)
            {
                DateTime resultado=Convert.ToDateTime(dtpFechaInical.Text);
                dtpFechaFinal.Text =Convert.ToString(resultado.AddDays(Convert.ToInt32(txtDias.Text) - 1));
                dtpFechaFinal.Focus();
            }
            else if (Convert.ToInt32(txtDias.Text) == 1)
            {
                dtpFechaFinal.Text = dtpFechaInical.Text;
                dtpFechaFinal.Focus();
            }
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                util.ChangeButton(btnInsertar, 3, false);
                btnInsertar.Text = "d";
                //iActbtn = 3;
            }
            else
            {
                util.ChangeButton(btnInsertar, 2, false);
                btnInsertar.Text = "u";
                //iActbtn = 2;
            }
        }

        private void ckbimprimir_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbimprimir.Checked)
            {
                muestra();
            }
            else
            {
                oculta();
            }
        }

        private void cbCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void cbUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void cbAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void cbTiponomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void cbPuestos_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void cbDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void cbEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
            ckbimprimir.Checked = false;
            ckbimprimir.Visible = false;
        }

        private void oculta()
        {
            btnImprimirDetalle.Visible = false;
            lblfechas.Visible = false;
            lblfecha1.Visible = false;
            lblfecha2.Visible = false;
            dtpfechainicio.Visible = false;
            dtpfechafin.Visible = false;
            linea1.Visible = false;
            linea2.Visible = false;
        }
        private void muestra()
        {
            btnImprimirDetalle.Visible = true;
            lblfechas.Visible = true;
            lblfecha1.Visible = true;
            lblfecha2.Visible = true;
            dtpfechainicio.Visible = true;
            dtpfechafin.Visible = true;
            linea1.Visible = true;
            linea2.Visible = true;
        }

        private void dtpFechaInical_Leave(object sender, EventArgs e)
        {
            txtDias.Focus();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
