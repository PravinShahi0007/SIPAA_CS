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

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class AsignacionIncidenciasTrabajador : Form
    {

         List<DateTime> ltFechasRegistro = new List<DateTime>();
        List<int> ltCvIncidencia = new List<int>();
        List<int> ltcvTipo = new List<int>();

        public int iOpcionAdmin;

        public AsignacionIncidenciasTrabajador()
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



        internal class Asignacion
        {

            public DateTime fFechaReg;
            public int iCvincidencia;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ltCvIncidencia.Count != 0)
            {

                if (cbIncidencia.SelectedValue.ToString() != "20") {

                    IncCaptura objinc = new IncCaptura();

                    

                    for (int iCont = 0;iCont < ltCvIncidencia.Count();iCont++)
                    {


                        

                    }
                   

                    //objinc.ExtrañamientoRetroactivo();
                    


                }

            }
            else
            {



            }
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


            if (dgvInc.SelectedRows.Count != 0)
            {
                lbAsignacion.Text = "       Asignar Extrañamiento o Retroactivo";
                DataGridViewRow row = this.dgvInc.SelectedRows[0];

                //CVPerfil = Convert.ToInt32(row.Cells["CVPERFIL"].Value.ToString());
                int icvIncidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());
                int icvTipo = Convert.ToInt32(row.Cells["cvtipo"].Value.ToString());
                DateTime fFechaReg = DateTime.Parse(row.Cells["Fecha Registro"].Value.ToString());


                Asignacion objAsig = new Asignacion();
                objAsig.iCvincidencia = icvIncidencia;
                objAsig.fFechaReg = fFechaReg;

                if (!ltFechasRegistro.Contains(fFechaReg) && !ltCvIncidencia.Contains(icvIncidencia))
                {
                    ltFechasRegistro.Add(fFechaReg);
                    ltCvIncidencia.Add(icvIncidencia);
                    ltcvTipo.Add(icvTipo);
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    row.Cells[0].Tag = "check";
                }
                else
                {

                    ltFechasRegistro.Remove(fFechaReg);
                    ltCvIncidencia.Remove(icvIncidencia);
                    ltcvTipo.Remove(icvTipo);
                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";
                }

                llenarComboIncidencia();




                //    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                //cbIncidencia.Enabled = true;

                //  DatosTrabajadorPerfil form = new DatosTrabajadorPerfil();
                // form.Show();
            }
            else
            {
                cbIncidencia.SelectedValue = 20;
                cbIncidencia.Enabled = false;

            }
        }

        private void AsignacionIncidenciasTrabajador_Load(object sender, EventArgs e)
        {
            lblusuario.Text = LoginInfo.Nombre;
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////

            lbNombre.Text = TrabajadorInfo.Nombre;
            lbIdTrab.Text = TrabajadorInfo.IdTrab;
            llenarComboIncidencia();
            cbTipo.Enabled = false;

            // IncCalificacion objInc = new IncCalificacion();
            //  LlenarGrid(objInc);

           
            llenarComboTipo(20);
            ltCvIncidencia.Clear();
            ltFechasRegistro.Clear();
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

        private void llenarComboIncidencia()
        {

            ConcepInc objIncidencia = new ConcepInc();
            DataTable dtIncidencia = objIncidencia.ConcepInc_S(6, 0, "", 0, 0, 0, 0, "", "");
            if (ltCvIncidencia.Count != 0)
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
        }






        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
    }
}
