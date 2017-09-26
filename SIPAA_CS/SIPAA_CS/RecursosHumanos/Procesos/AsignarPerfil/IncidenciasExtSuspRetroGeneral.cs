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

        public AsignacionIncidenciasTrabajador2()
        {
            InitializeComponent();
        }


        
        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:17-05-04      Última Modificacion: 17-05-04    
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

        private void ObtieneEmpleado(object sender, EventArgs e)
        {
            // Obtiene Nombre del Empleado

            DataTable dtNombreEmpleado = oNombreEmpleado.obtNombreEmpleado(TxtIdEmp.Text, 14);

         if (dtNombreEmpleado.Rows.Count > 0)
           {
               TxtNombreEmpleado.Text = dtNombreEmpleado.Rows[0][2].ToString();
            }
            else
            {
                TxtNombreEmpleado.Text = "Empleado No EXISTE";
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
            objInc.fFechaInicio = dpFechaInicio.Value;
            objInc.fFechaTermino = dpFechaFin.Value;

            LlenarGrid(objInc);

            cbIncidencia.SelectedValue = 20;
            cbIncidencia.Enabled = false;
            llenarComboTipo(20);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            DatosTrabajadorPerfil dattrabperf = new DatosTrabajadorPerfil();
            dattrabperf.Show();
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

        private void pnlBusqueda_Paint(object sender, PaintEventArgs e)
        {
            ///////////////////////////////////////////////


        }



      
        private void btnGuardar_Click(object sender, EventArgs e)
        {
           

                switch (ValidarFecha(DateTime.Parse(dtimeFechaInicioAsig.Text), DateTime.Parse(dtimeFechaFinAsig.Text))){


                    case 0:

                    if (cbIncidencia.SelectedValue.ToString() != "20")
                    {
                        if (ltTrab.Count != 0)
                        {
                            IncCaptura objinc = new IncCaptura();

                            bool bBandera = false;
                            bool bExists = false;
                            for (int iCont = 0; iCont < ltTrab.Count(); iCont++)
                            {


                                Captura2 obj = ltTrab.ElementAt(iCont);

                                int idTrabActual = Convert.ToInt32(TrabajadorInfo.IdTrab);
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
                                objinc.sPrgumod = this.Name;

                                DataTable dt = objinc.ExtrañamientoRetroactivo(objinc, 1);

                                if (dt.Columns.Contains("INSERT")|| dt.Columns.Contains("EXISTS"))
                                {
                                    bBandera = true;
                                    IncCalificacion objInc = new IncCalificacion();
                                    objInc.fFechaInicio = dpFechaInicio.Value;
                                    objInc.fFechaTermino = dpFechaFin.Value;

                                    LlenarGrid(objInc);
                                }
                                
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

                        objDias.sIdTrab = TrabajadorInfo.IdTrab;
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
                    
                        break;


                    case 1:
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La Fecha de Inicio no puede ser Superior a la de Término");
                    timer1.Start();
                    break;


                }


            

        }

        public int ValidarFecha(DateTime fFechaInicio, DateTime fFechaTermino) {

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
            //for (int iContador = 0; iContador < dgvInc.Rows.Count; iContador++)
            //{
            //    dgvInc.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            //}

            if (Permisos.dcPermisos["Crear"] == 1 && Permisos.dcPermisos["Actualizar"] == 1)
            {
                if (dgvInc.SelectedRows.Count != 0)
                {
                    lbAsignacion.Text = "       Asignar Extrañamiento o Retroactivo";
                    DataGridViewRow row = this.dgvInc.SelectedRows[0];

                    //CVPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                    int icvIncidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());
                    int icvTipo = Convert.ToInt32(row.Cells["cvtipo"].Value.ToString());
                    DateTime fFechaReg = DateTime.Parse(row.Cells["Fecha Registro"].Value.ToString());


                    Captura2 objAsig = new Captura2();
                    objAsig.cvincidencia = icvIncidencia;
                    objAsig.cvtipo = icvTipo;
                    objAsig.FechaReg = fFechaReg;

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

                            cbIncidencia.SelectedValue = 20;
                            cbIncidencia.Enabled = false;
                            lbAsignacion.Text = "       Asignar Suspensión";

                        }
                        else
                        {

                            pnlAsig.Visible = false;

                        }
                    }
                    //    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    //cbIncidencia.Enabled = true;

                    //  DatosTrabajadorPerfil form = new DatosTrabajadorPerfil();
                    // form.Show();
                }
            }
         
        }

        private void AsignacionIncidenciasTrabajador2_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "IncidenciasExtSuspRetroGeneral.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////

            // lbNombre.Text = TrabajadorInfo.Nombre;
            // lbIdTrab.Text = TrabajadorInfo.IdTrab;
            llenarComboIncidencia();
            //cbTipo.Enabled = false;

            // IncCalificacion objInc = new IncCalificacion();
            //LlenarGrid(objInc);

           
            llenarComboTipo(20);
            ltCvIncidencia.Clear();
            ltFechasRegistro.Clear();


            if (Permisos.dcPermisos["Crear"] != 1) {

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
                dtIncidencia.Rows.RemoveAt(1);
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

            // IncCalificacion objInc = new IncCalificacion();

            objInc.sIdtrab = TrabajadorInfo.IdTrab;
            DataTable dtInc = objInc.ObtenerCalificacionIncidenciaDetalle(objInc, 5);

            dgvInc.DataSource = dtInc;

            Utilerias.AgregarCheck(dgvInc, 0);
           // Utilerias.AgregarCheckboxHeader(dgvInc, 0);

            dgvInc.Columns[1].Visible = false;
            dgvInc.Columns["cvincidencia"].Visible = false;
            dgvInc.Columns["cvtipo"].Visible = false;
            dgvInc.Columns["Tiempo Prof"].Width = 40;
            dgvInc.Columns["Tiempo Emp"].Width = 40;
            foreach (DataGridViewRow row in dgvInc.Rows)
            {
                row.Cells[0].Tag = "uncheck";
            }


            if (Permisos.dcPermisos["Crear"] != 1 && Permisos.dcPermisos["Actualizar"] != 1) {
                dgvInc.Columns.RemoveAt(0);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        private void label13_Click(object sender, EventArgs e)
        {

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

    }
}
