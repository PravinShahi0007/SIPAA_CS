using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.Accesos.Reportes;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;
using SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil;
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
using System.IO;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class AsignacionIncidenciasTrabajador2 : Form
    {

        #region Variables

        int iIdFormaPago;
        bool bClickPrimeraVezFormaPago = true;

        #endregion


        List<DateTime> ltFechasRegistro = new List<DateTime>();
        List<int> ltCvIncidencia = new List<int>();
        List<int> ltcvTipo = new List<int>();
        Captura2 objCap = new Captura2();
        List<Captura2> ltTrab = new List<Captura2>();

        public int iOpcionAdmin;
        AsignacionIncidenciaTrabajador2 oNombreEmpleado = new AsignacionIncidenciaTrabajador2();
        Utilerias Util = new Utilerias();
        SonaTrabajador contenedorempleados = new SonaTrabajador();

        public AsignacionIncidenciasTrabajador2()
        {
            InitializeComponent();
        }
        
        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:17-05-04 Última Modificacion: 28-OCT-2017 JLA  20-FEB-2018 JLA  
        //Descripción:
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        private void cbIncidencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIncidencia.SelectedIndex != 0)
            {
                cbTipo.Enabled = true;
                llenarComboTipo(Convert.ToInt32(cbIncidencia.SelectedValue));
            }
            else
            {
                cbTipo.Enabled = false;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            IncCalificacion objInc = new IncCalificacion();
            objInc.fFechaInicio = DateTime.Parse(dpFechaInicio.Text);
            objInc.fFechaTermino = DateTime.Parse(dpFechaFin.Text);
            objInc.sIdtrab = cbEmpleados.SelectedValue.ToString();

            LlenarGrid(objInc);

            cbIncidencia.SelectedValue = 17; //Suspension
            cbIncidencia.Enabled = false;
            llenarComboTipo(17);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
                switch (ValidarFecha(DateTime.Parse(dtimeFechaInicioAsig.Text), DateTime.Parse(dtimeFechaFinAsig.Text))){

                    case 0:

                        if (cbIncidencia.SelectedValue.ToString() != "17") //Suspension
                        {
                            if (ltTrab.Count != 0)
                            {
                                IncCaptura objinc = new IncCaptura();

                                DataTable dtReporte = new DataTable();
                                dtReporte.Columns.Add("FechaInc", typeof(string));
                                dtReporte.Columns.Add("Incidencia", typeof(string));
                                dtReporte.Columns.Add("TiempoEmp", typeof(string));
                                dtReporte.Columns.Add("TiempoProf", typeof(string));
                            
                                bool bBandera = false;
                                for (int iCont = 0; iCont < ltTrab.Count(); iCont++)
                                {
                                    Captura2 obj = ltTrab.ElementAt(iCont);

                                    int idTrabActual = Int32.Parse(cbEmpleados.SelectedValue.ToString());
                                    int cvincidenciaActual = obj.cvincidencia;
                                    int cvtipoActual = obj.cvtipo;

                                    objinc.fFecharegistro = obj.FechaReg;
                                    objinc.iIdtrab = idTrabActual;
                                    objinc.iCvIncidencia = cvincidenciaActual;
                                    objinc.iCvTipo = cvtipoActual;
                                    objinc.iCvIncidencia2 = Convert.ToInt32(cbIncidencia.SelectedValue.ToString());
                                    objinc.iCvTipo2 = Convert.ToInt32(cbTipo.SelectedValue.ToString());

                                    objinc.fFechaFin = DateTime.Parse(dtimeFechaFinAsig.Text);
                                    objinc.fFechaInicio = DateTime.Parse(dtimeFechaInicioAsig.Text);
                                    objinc.sUsuumod = LoginInfo.IdTrab;
                                    objinc.sPrgumod = "IncidenciasExtSuspRetroGeneral";   //this.Name;
                                    if (ckbaplica.Checked)
                                    {
                                        objinc.iAplica = 1;
                                    }
                                    else
                                    {
                                        objinc.iAplica = 0;
                                    }

                                    DataTable dt = objinc.ExtrañamientoRetroactivo(objinc, 1);
                                    dtReporte.Rows.Add(Convert.ToString(objinc.fFecharegistro.ToString("dd/MM/yyyy")), obj.sIncidencia, obj.iTiempoEmp, obj.iTiempoProf);

                                    if (dt.Columns.Contains("INSERT") || dt.Columns.Contains("EXISTS"))
                                    {
                                        bBandera = true;
                                        IncCalificacion objInc = new IncCalificacion();
                                        objInc.fFechaInicio = dpFechaInicio.Value;
                                        objInc.fFechaTermino = dpFechaFin.Value;

                                        LlenarGrid(objInc);
                                    }
                                }

                                if (cbIncidencia.SelectedValue.ToString() == "19")  //Extraña
                                {
                                    //Se lanza la carta
                                    ViewerReporte form = new ViewerReporte();
                                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtReporte, "SIPAA_CS.RecursosHumanos.Reportes", "CartaExtrañamiento.rpt");
                                    ReportDoc.SetParameterValue("NombreEmpleado", cbEmpleados.Text);
                                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Text);
                                    ReportDoc.SetParameterValue("FechaFin", dpFechaFin.Text);
                                    form.RptDoc = ReportDoc;
                                    form.Show();
                                }
                            
                                ltCvIncidencia.Clear();
                                ltcvTipo.Clear();
                                ltFechasRegistro.Clear();
                                ltTrab.Clear();

                               if (bBandera == true)
                                {
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Guardadas Correctamente");
                                    txtReferencia.Text = String.Empty;
                                    cbTipo.SelectedIndex = 0;
                                    dtimeFechaInicioAsig.Value = DateTime.Now;
                                    dtimeFechaFinAsig.Value = DateTime.Now;
                                    cbTipo.SelectedIndex = 0;
                                    cbTipo.Enabled = false;
                                    cbIncidencia.SelectedIndex = 0;
                                    timer1.Start();
                                }
                                else
                                {
                                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Una o más asignaciones no se lograron guardar.");
                                    timer1.Start();
                                }
                            }
                        }
                        else
                        {
                            DiasEspeciales objDias = new DiasEspeciales();

                            objDias.sIdTrab = cbEmpleados.SelectedValue.ToString();
                            objDias.iCvIncidencia = Convert.ToInt32(cbIncidencia.SelectedValue.ToString());
                            objDias.iCvTipo = Convert.ToInt32(cbTipo.SelectedValue.ToString());
                            objDias.fFechaInicio = DateTime.Parse(dtimeFechaInicioAsig.Text);
                            objDias.fFechaFin = DateTime.Parse(dtimeFechaFinAsig.Text);
                            objDias.sReferencia = txtReferencia.Text;
                            objDias.iOrden = 6;
                            objDias.sUsuumod = LoginInfo.IdTrab;
                            objDias.sPrgumod = this.Name;
                            DataTable dt = objDias.ObtenerDiasEspecialesxTrabajador(objDias, 1);

                            if (dt.Columns.Contains("INSERT"))
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Suspensión Guardada Correctamente");
                                txtReferencia.Text = String.Empty;
                                cbTipo.SelectedIndex = 0;
                                dtimeFechaInicioAsig.Value = DateTime.Now;
                                dtimeFechaFinAsig.Value = DateTime.Now;
                                cbTipo.SelectedIndex = 0;
                                cbTipo.Enabled = false;
                                cbIncidencia.SelectedIndex = 0;
                                timer1.Start();
                            }
                            else if (dt.Columns.Contains("EXISTS"))
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Este Tipo de Suspensión ya fue Asignado a este trabajador en esa fecha");
                                timer1.Start();
                            }
                            else
                            {
                                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación. Favor de Repetir el proceso");
                                timer1.Start();
                            }
                        }
                        frecargar();
                        break;

                    case 1:
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La Fecha de Inicio no puede ser Superior a la de Término");
                        timer1.Start();
                        break;
                }
        }

        public int ValidarFecha(DateTime fFechaInicio, DateTime fFechaTermino)
        {
            int iResponse = 0;
            if (fFechaInicio > fFechaTermino)
            {
                iResponse = 1;
            }
            return iResponse;
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        private void dgvTipoHr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (dgvInc.SelectedRows.Count != 0)
                {
                    //lbAsignacion.Text = "       Asignar Extrañamiento o Retroactivo";
                    DataGridViewRow row = this.dgvInc.SelectedRows[0];

                    int icvIncidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());
                    int icvTipo = Convert.ToInt32(row.Cells["cvtipo"].Value.ToString());
                    DateTime fFechaReg = DateTime.Parse(row.Cells["Fecha Registro"].Value.ToString());
                    string iTiempoE = row.Cells["Tiempo Emp"].Value.ToString();
                    string iTiempoP = row.Cells["Tiempo Prof"].Value.ToString();

                    Captura2 objAsig = new Captura2();
                    objAsig.cvincidencia = icvIncidencia;
                    objAsig.cvtipo = icvTipo;
                    objAsig.FechaReg = fFechaReg;
                    //JLA
                    objAsig.sIncidencia = row.Cells["Incidencia"].Value.ToString();
                    objAsig.iTiempoEmp = iTiempoE;
                    objAsig.iTiempoProf = iTiempoP;

                    ValidarExistencia(ltTrab, objAsig);

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

                    llenarComboIncidencia();
                    if (ltTrab.Count() == 0)
                    {
                        if (Permisos.dcPermisos["Crear"] == 1)
                        {
                            cbIncidencia.SelectedValue = 17; //tenia 20 porque no se JLA 25oct17
                            cbIncidencia.Enabled = false;
                            //lbAsignacion.Text = "       Asignar Suspensión, Extrañamiento o Retroactivo";
                        }
                        else
                        {
                            pnlAsig.Visible = false;
                        }
                    }
                }
        }

        private void AsignacionIncidenciasTrabajador2_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "IncidenciasExtSuspRetroGeneral")
                {
                    f.Hide();
                }
            }

            ckbaplica.Visible = false;
            ckbaplica.Checked = false;

            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);
            lbAsignacion.Text = "Asignar Suspensión, Extrañamiento o Retroactivo";

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, "IncidenciasExtSuspRetroGeneral");
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            llenarComboIncidencia();
          
            llenarComboTipo(17);  ///////////
            ltCvIncidencia.Clear();
            ltFechasRegistro.Clear();

            //llenado de combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");

            if (Permisos.dcPermisos["Crear"] != 1)
            {
                labelGrid.Text = "Incidencias Registradas";
                pnlAsig.Visible = false;
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void llenarComboTipo(int iCvIncidencia)
        {
            Incidencia objIncidencia = new Incidencia();
            objIncidencia.CVIncidencia = iCvIncidencia;
            DataTable dtIncidencia = objIncidencia.ObtenerIncidenciaxTipo(objIncidencia, 8);
            Utilerias.llenarComboxDataTable(cbTipo, dtIncidencia, "cvTipo", "Tipo");
        }

        public void ValidarExistencia(List<Captura2> ltTrab, Captura2 objtrab)
        {
            bool bBandera = false;
            int iCont = 0;
            if (ltTrab.Count != 0)
            {
                while (iCont <= (ltTrab.Count - 1))
                {
                    Captura2 objComp = ltTrab[iCont];

                    if (CompararObj(objComp, objtrab))
                    {
                        bBandera = true;
                        break;
                    }
                    else
                    {
                        iCont += 1;
                    }
                }

                if (bBandera == true)
                {
                    ltTrab.Remove(ltTrab[iCont]);
                }
                else
                {
                    ltTrab.Add(objtrab);
                }
            }
            else
            {
                ltTrab.Add(objtrab);
            }
        }

        public bool CompararObj(Captura2 obj1, Captura2 obj2)
        {
            bool bBandera = false;

            if (obj1.FechaReg == obj2.FechaReg)
            {
                if (obj1.cvincidencia == obj2.cvincidencia)
                {
                    if (obj1.cvtipo == obj2.cvtipo)
                    {
                        bBandera = true;
                    }
                   
                }
            }
            return bBandera;
        }

        private void llenarComboIncidencia()
        {
            ConcepInc objIncidencia = new ConcepInc();
            DataTable dtIncidencia = objIncidencia.ConcepInc_S(6, 0, "", 0, 0, 0, 0, "", "");
            if (ltTrab.Count != 0)
            {
                Utilerias.llenarComboxDataTable(cbIncidencia, dtIncidencia, "cvincidencia", "Descripcion");
                cbIncidencia.Enabled = true;
            }
            else {
                Utilerias.llenarComboxDataTable(cbIncidencia, dtIncidencia, "cvincidencia", "Descripcion");
                cbIncidencia.SelectedValue = 20;
                cbIncidencia.Enabled = false;
            }
        }

        private void LlenarGrid(IncCalificacion objInc)
        {
            if (dgvInc.Columns.Count > 1)
            {
                dgvInc.Columns.RemoveAt(0);
            }

            DataTable dtInc = objInc.ObtenerCalificacionIncidenciaDetalle(objInc, 5);
            dgvInc.DataSource = dtInc;
            Utilerias.AgregarCheck(dgvInc, 0);

            dgvInc.Columns[1].Visible = false;
            dgvInc.Columns["cvincidencia"].Visible = false;
            dgvInc.Columns["cvtipo"].Visible = false;
            foreach (DataGridViewRow row in dgvInc.Rows)
            {
                row.Cells[0].Tag = "uncheck";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

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
            toolTip1.SetToolTip(this.btnGuardar, "Guardar Registros");
        }

        private void frecargar()
        {
            AsignacionIncidenciasTrabajador2 recargar = new AsignacionIncidenciasTrabajador2();
            recargar.Show();
            this.Close();
        }

        private void cbIncidencia_Leave(object sender, EventArgs e)
        {
            if (cbIncidencia.SelectedValue.ToString() == "19") //Extrañamiento
            {
                ckbaplica.Visible = true;
                ckbaplica.Checked = true;
            }
            else
            {
                ckbaplica.Checked = false;
                ckbaplica.Visible = false;                
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }

    public class Captura2 {

        public string Idtrab;
        public DateTime FechaReg;
        public int cvtipo;
        public int cvincidencia;
        //JLA 27/10/17
        public string sFechaInc;
        public string sIncidencia;
        public string iTiempoEmp;
        public string iTiempoProf;
    }
}
