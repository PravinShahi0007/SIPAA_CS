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
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Procesos.AsignarPerfil
{
    public partial class AsignacionTipoHorarioTrabajador : Form
    {

        public List<string> ltTipoHr = new List<string>();
        public string sCVTipohr;
        public List<int> ltcvTipoHorario = new List<int>();
        public AsignacionTipoHorarioTrabajador()
        {
            InitializeComponent();
        }




        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:7-04-2017     Última Modificacion: 17-04-2017
        //Descripción: -------------------------------
        //***********************************************************************************************


        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvTipoHr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Actualizar"] != 0 && Permisos.dcPermisos["Eliminar"] != 0)
            {
                for (int iContador = 0; iContador < dgvTipoHr.Rows.Count; iContador++)
                {
                    dgvTipoHr.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }


                if (dgvTipoHr.SelectedRows.Count != 0)
                {

                    DataGridViewRow row = this.dgvTipoHr.SelectedRows[0];

                    sCVTipohr = row.Cells["clave"].Value.ToString();

                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    //AsignarTipoHr();
                    Utilerias.SeleccionGridView(dgvTipoHr, 1, ltcvTipoHorario, panelPermisos);

                    if (ltTipoHr.Count > 0)
                    {
                        panelPermisos.Enabled = true;
                    }

                }
            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SonaTrabajador objTrab = new SonaTrabajador();
            bool bBandera = Utilerias.SinAsignaciones(dgvTipoHr, 0, 1, ltcvTipoHorario);

            if (ltcvTipoHorario.Count > 0)
            {
                try
                {
                    DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(TrabajadorInfo.IdTrab, 8, "", "", Convert.ToInt32(sCVTipohr), LoginInfo.IdTrab, this.Name);

                    if (dtTrab.Columns.Contains("Actualizar"))
                    {

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignación Guardada Correctamente");
                        panelPermisos.Enabled = false;
                        ltTipoHr.Clear();
                        AsignarTipoHr();
                        panelPermisos.Enabled = false;
                        timer1.Start();
                    }
                }
                catch (Exception ex)
                {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentelo más tarde.");
                    ltTipoHr.Clear();
                    panelPermisos.Enabled = false;
                    timer1.Start();
                }
            }
            else
            {

                DialogResult result = MessageBox.Show("¿Seguro que desea quitar Todas las Asignaciones?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(TrabajadorInfo.IdTrab, 8, "", "", Convert.ToInt32(sCVTipohr), LoginInfo.IdTrab, this.Name);

                        if (dtTrab.Columns.Contains("Actualizar"))
                        {

                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignación Guardada Correctamente");
                            panelPermisos.Enabled = false;
                            ltTipoHr.Clear();
                            AsignarTipoHr();
                            panelPermisos.Enabled = false;
                            timer1.Start();
                        }
                    }
                    catch (Exception ex)
                    {

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación con el Servidor. Intentelo más tarde.");
                        ltTipoHr.Clear();
                        panelPermisos.Enabled = false;
                        timer1.Start();
                    }
                }
                else
                {
                    ltTipoHr.Clear();
                    AsignarTipoHr();
                    panelPermisos.Enabled = false;
                }


            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string sDesc = "";
            if (txtBuscar.Text != String.Empty)
            {
                sDesc = txtBuscar.Text;
            }
            else
            {
                sDesc = "%";
            }

            TipoHr objTiposHr = new TipoHr();
            DataTable dttipohr = objTiposHr.obttipohr(4, 0, sDesc, "", "");
            llenarGrid(dttipohr);
            AsignarTipoHr();
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

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------


        private void AsignacionTipoHorarioTrabajador_Load(object sender, EventArgs e)
        {

            lblusuario.Text = LoginInfo.Nombre;
         
            lbNombre.Text = TrabajadorInfo.Nombre;
            lbIdTrab.Text = TrabajadorInfo.IdTrab;

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            TipoHr objTiposHr = new TipoHr();
            DataTable dttipohr = objTiposHr.obttipohr(4, 0, "", "", "");
            llenarGrid(dttipohr);
            AsignarTipoHr();


            if (Permisos.dcPermisos["Actualizar"] != 1 && Permisos.dcPermisos["Eliminar"] != 1) {

                panelPermisos.Visible = false;

            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void llenarGrid(DataTable dt)
        {

            if (dgvTipoHr.Columns.Count > 1)
            {
                dgvTipoHr.Columns.RemoveAt(0);
            }

            dgvTipoHr.DataSource = dt;
            Utilerias.AgregarCheck(dgvTipoHr, 0);

            dgvTipoHr.Columns[1].Visible = false;
            dgvTipoHr.Columns[3].Visible = false;


            if (Permisos.dcPermisos["Actualizar"] != 1 && Permisos.dcPermisos["Eliminar"] != 1)
            {
                dgvTipoHr.Columns.RemoveAt(0);
            }
        }

        private void AsignarTipoHr()
        {
            SonaTrabajador objTrab = new SonaTrabajador();
            DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(TrabajadorInfo.IdTrab, 7, "", "", 0, LoginInfo.IdTrab, this.Name);
            foreach (DataRow row in dtTrab.Rows)
            {
                ltTipoHr.Add(row[0].ToString());
            }
            Utilerias.ImprimirAsignacionesGrid(dgvTipoHr, 0, 1, ltTipoHr);
        }


        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------
        private void txtBuscarForReg_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


    }
}
