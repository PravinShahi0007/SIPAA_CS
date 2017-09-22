using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                cbTipo.ValueMember = "Clave";
            }
            else
            {
                MessageBox.Show("Debes elejir la Compañia", "SIPAA", MessageBoxButtons.OK);
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
            for (int iContador = 0; iContador < dgvEmpleados.Rows.Count; iContador++)
            {
                dgvEmpleados.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvEmpleados.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvEmpleados.SelectedRows[0];

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                NoTrabajador = row.Cells["NoEmpleado"].Value.ToString();

                llenarGridDiasEsp(NoTrabajador);

                dgvInc.Columns[0].Width = 190;
                dgvInc.Columns[1].Width = 190;
                dgvInc.Columns[2].Width = 95;
                dgvInc.Columns[3].Width = 95;
                dgvInc.Columns[4].Width = 95;
                dgvInc.Columns[5].Width = 95;
                dgvInc.Columns[6].Width = 35;
                dgvInc.Columns[7].Width = 110;
                dgvInc.Columns[8].Width = 68;
                ////Guajolocombo Conceptos Incidencia
                //CbConceptoIncidencia(7, 0, "", 0, 0, 0, 0, "", "");
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar empleados
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //if (txtEmpleado.Text !="" & cbCompania.SelectedValue.ToString() != "" & cbAreas.SelectedValue.ToString() != "" & cbPuestos.SelectedValue.ToString() != "" & 
            //    cbDepartamentos.SelectedValue.ToString() != "" & cbUbicacion.SelectedValue.ToString() != "" & cbTiponomina.SelectedValue.ToString() != "")
            //{
            //    MessageBox.Show("Debe seleccionar por lo menos un filtro de busqueda", "SIPAA", MessageBoxButtons.OK);
            //    txtEmpleado.Focus();
            //}
            //else
            //{ 
                //llena grid Con Filtros
                string fIdTrab = "%";
                string fIdCompania = "%";
                string fIdArea = "%";
                string fIdPuesto = "%";
                string fIdDepartamento = "%";
                string fIdUbicacion = "%";
                string fIdTipoNomina = "%";

                if (Convert.ToInt32(cbEmpleados.SelectedIndex.ToString()) > 0)
                {
                    fIdTrab = cbEmpleados.SelectedValue.ToString();
                }
                
                //if (cbCompania.SelectedValue.ToString() != "" & cbCompania.SelectedValue.ToString() != "Seleccionar")
                if (Convert.ToInt32(cbCompania.SelectedIndex.ToString()) > 0)
                {
                    fIdCompania = cbCompania.SelectedValue.ToString();
                }
                //if (cbAreas.SelectedValue.ToString() != "" & cbAreas.SelectedValue.ToString() != "Seleccionar")
                //{
                //    fIdArea = cbAreas.SelectedValue.ToString();
                //}
                //if (cbPuestos.SelectedValue.ToString() != "" & cbPuestos.SelectedValue.ToString() != "Seleccionar")
                if (Convert.ToInt32(cbPuestos.SelectedIndex.ToString()) > 0)
                {
                    fIdPuesto = cbPuestos.SelectedValue.ToString();
                }
                //if (cbDepartamentos.SelectedValue.ToString() != "")
                if (Convert.ToInt32(cbDepartamentos.SelectedIndex.ToString()) > 0)
                {
                    fIdDepartamento = cbDepartamentos.SelectedValue.ToString();
                }
                //if (cbUbicacion.SelectedValue.ToString() != "")
                if (Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) > 0)
                {
                    fIdUbicacion = cbUbicacion.SelectedValue.ToString();
                }
                //if (cbTiponomina.SelectedValue.ToString() != "" & cbTiponomina.SelectedValue.ToString() != "Seleccionar")
                //{
                //    fIdTipoNomina = cbTiponomina.SelectedValue.ToString();
                //}
                
                //fgridEmpleados(3, txtEmpleado.Text.Trim()); //todos los activos x Num
                fgridEmpleados(1,fIdTrab,fIdCompania,fIdArea,fIdPuesto,fIdDepartamento,fIdUbicacion,fIdTipoNomina);
                dgvEmpleados.Columns[0].Width =85;
                dgvEmpleados.Columns[1].Width =100;
                dgvEmpleados.Columns[2].Width =300;
                dgvEmpleados.Columns[3].Visible = false;
                dgvEmpleados.Columns[4].Visible = false;
                //Guajolocombo Conceptos Incidencia
                CbConceptoIncidencia(7, 0, "", 0, 0, 0, 0, "", "");
                txtSubsidio.Text = "0";
                txtDias.Text = "0";
            //}
        }

        private void cbConcepto_DropDown(object sender, EventArgs e)
        {
            cbTipo.Text=string.Empty;
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
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            ///Aqui hay que asignar los valores en la tabla
            //para insertar registro nuevo 

            fvalidacampos();

            if (svalidacampos != "0")
            {
                DialogResult result = MessageBox.Show(svalidacampos, "SIPAA", MessageBoxButtons.OK);
            }
            else
            {

                string usuumod = LoginInfo.IdTrab;
                string prgumod = this.Name;

                foreach (DataGridViewRow row in dgvEmpleados.Rows)
                {
                    try
                    {
                       int iIdTrab = Convert.ToInt32(row.Cells[1].Value.ToString());
                       fInsDiasEsp(iIdTrab, 1, Convert.ToInt32(cbConcepto.SelectedValue.ToString()), Convert.ToInt32(cbTipo.SelectedValue.ToString()), dtpFechaInical.Text.Trim(),
                       dtpFechaFinal.Text.Trim(), Convert.ToInt32(txtDias.Text), mtxtHoraEntrada.Text.Trim(), mtxtHoraSalida.Text.Trim(), txtReferencia.Text, 4,
                       Convert.ToInt32(txtSubsidio.Text), 0, usuumod, prgumod, 0, 0);
                        panelTag.Visible = true;
                        timer1.Start();
                        //this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
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

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

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
                case "":
                    lblMensaje.Text = "Problemas en la Asignación, avise a Sistemas.";
                    break;
            }
            //txtmensajeiu.Text = "";
            //txtMensaje.Focus();

            /*
            //agrega registro
            if (ipactbtn == 1)
            {
                ip_rep = oPlantillas.fuid_sp_plantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                txtmensajeiu.Text = "";
                txtMensaje.Focus();
            }
            //actualiza registro
            else if (ipactbtn == 2)
            {
                ip_rep = oPlantillas.fuid_sp_plantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                txtmensajeiu.Text = "";
                txtMensaje.Focus();
            }
            //elimina registro
            else if (ipactbtn == 3)
            {
                ip_rep = oPlantillas.fuid_sp_plantillas(ipopcion, ipcvplantilla, spdescripcion, spusuumod, spprgumod);
                txtmensajeiu.Text = "";
                txtMensaje.Focus();
            } */
        }

        //funcion validar campos
        protected string fvalidacampos()
        {
            if (cbConcepto.Text == "") { svalidacampos = "Selecione Concepto"; cbConcepto.Focus(); }

            else if (cbTipo.Text == "") { svalidacampos = "Selecione Tipo"; cbTipo.Focus(); }

            else if (txtReferencia.Text == "") { svalidacampos = "Capture Referencia"; txtReferencia.Focus(); }

            else if (dtpFechaInical.Text == "") { svalidacampos = "Selecione Fecha Inicial"; dtpFechaInical.Focus(); }

            else { svalidacampos = "0"; }
            return svalidacampos;
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
