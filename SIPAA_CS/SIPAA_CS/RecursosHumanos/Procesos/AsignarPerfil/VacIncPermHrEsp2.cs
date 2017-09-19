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

        SonaTrabajador contenedorempleados = new SonaTrabajador();
        Utilerias util = new Utilerias();
        ConcepInc ConceptoIncidencias = new ConcepInc();
        Incidencia TipoIncidencias = new Incidencia();
        DiasEspeciales DiasEspeciales = new DiasEspeciales();

        //***********************************************************************************************
        //Autor: José Luis Alvarez Delgado
        //Fecha creación:14-09-2017     Última Modificacion: 
        //Descripción: -------------------------------
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
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
            //llena grid
            if (txtEmpleado.Text == String.Empty)
            {
                fgridEmpleados(3, txtEmpleado.Text.Trim()); //todos los activos x Num
                dgvEmpleados.Columns[0].Width =85;
                dgvEmpleados.Columns[1].Width =100;
                dgvEmpleados.Columns[2].Width =140;
                dgvEmpleados.Columns[3].Width =140;
                dgvEmpleados.Columns[4].Width =150;
                dgvEmpleados.Columns[5].Visible = false;
                dgvEmpleados.Columns[6].Visible =false;
                txtEmpleado.Text = "";
                txtEmpleado.Focus();
                //Guajolocombo Conceptos Incidencia
                CbConceptoIncidencia(7, 0, "", 0, 0, 0, 0, "", "");
            }
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

            foreach (DataGridViewRow row in dgvEmpleados.Rows)
            {
                try
                {
                    int iIdTrab = Convert.ToInt32(row.Cells[1].Value.ToString());
                    fInsDiasEsp(iIdTrab, 1, Convert.ToInt32(cbConcepto.SelectedValue.ToString()), Convert.ToInt32(cbTipo.SelectedValue.ToString()), dtpFechaInical.Text.Trim(),
                                dtpFechaFinal.Text.Trim(), Convert.ToInt32(txtDias.Text), txtHoraEntrada.Text, txtHoraSalida.Text, txtReferencia.Text, 4,
                                Convert.ToInt32(txtSubsidio.Text), 0, LoginInfo.IdTrab, this.Name, 0, 0);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(""+ex);
                }

            }

            /*
            dgvPlantillas.DataSource = null;
            dgvPlantillas.Columns.RemoveAt(0);
            panelTaga.Visible = false;
            pnlnotif.Visible = true;
            pnlnotif.BackColor = ColorTranslator.FromHtml("#2e7d32");
            pnlnotif.BackColor = ColorTranslator.FromHtml("#2e7d32");
            lblnotif.Text = "Registro Guardado Correctamente";
            timer1.Start();
            txtmensajeiu.Text = "";
            txtmensajeiu.Focus();
            */
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

            //pone el foco en el campo de busqueda
            txtEmpleado.Focus();

            //llena grid
            //fgridEmpleados(1,"");
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        /////////funciones. FUNCION tooltip
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
        private void fgridEmpleados(int popc, string pbusq)
        {
            DataTable dtempleados = contenedorempleados.obtenerempleados(popc, pbusq);
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

            //dgvIncidencia.ClearSelection();
            //sHabilitaPermisos();
        }

        private void fInsDiasEsp(int iIdTrab, int iOpcion, int iCvIncidencia, int iCvTipo, string fFechaInicio, string fFechaFin, int iDias,
            string tpHoraentrada, string tpHoraSalida, string sReferencia, int iOrden, int iSubsidio, int iIdtrabrys, string spusuumod, string spprgumod, int iIdCompania, int iIPlanta)
        {
            DiasEspeciales.InsertarDiasEspecialesxTrabajador(iIdTrab, iOpcion, iCvIncidencia, iCvTipo, fFechaInicio, fFechaFin, iDias, tpHoraentrada, tpHoraSalida,
                sReferencia, iOrden, iSubsidio, iIdtrabrys, spusuumod, spprgumod, iIdCompania, iIPlanta);

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

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


    }
}
