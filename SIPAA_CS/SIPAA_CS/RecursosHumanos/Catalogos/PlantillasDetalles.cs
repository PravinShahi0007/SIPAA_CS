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
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.Properties;
using SIPAA_CS;
using static SIPAA_CS.App_Code.Usuario;


//***********************************************************************************************
//Autor: noe alvarez marquina
//Fecha creación:28-mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: plantillas detalle
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class PlantillasDetalles : Form
    {
        public PlantillasDetalles()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        //                         V A R I A B L E S    L O C A L E S
        //-----------------------------------------------------------------------------------------------

        #region
        
        int iins;// variable permiso de insertar
        int iact;// variable permiso de actualizar
        int ielim;// variable permiso de eliminar
        int iactbtn;// actión de realizar

        int iresp;// variable de respuesta-acción realizada

        int inumcolumngrid;//número de columnas en el grid

        int iexistereg;//variable validacion existe registro
        int icvplantilla;//variable plantilla modificación
        int icvdia;//variable dia modificacion
        #endregion

        //-----------------------------------------------------------------------------------------------
        //                                   R E F E R E N C I A S
        //-----------------------------------------------------------------------------------------------

        Utilerias Util = new Utilerias();
        PlantillaDetalle PlantDet = new PlantillaDetalle();
        Perfil DatPerfil = new Perfil();

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        //combo plantilla
        private void cbplantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvrechcplantilla_d.DataSource = null;

            inumcolumngrid = dgvrechcplantilla_d.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvrechcplantilla_d.Columns.RemoveAt(0);
                //fdleccllenadogrid();
            }
            else
            {
                //if (Util.p_inicbo == 1)
                //{
                //    fdleccllenadogrid();
                //}

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //botòn guardar
        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (iactbtn == 1)
            {
                PlantDet.rechcplantilla_d_suid(1, icvplantilla, icvdia, mtbhrinijor.Text.Trim(), Int32.Parse(cbdiasalida.SelectedValue.ToString()),
                                              mtbhterminojornada.Text.Trim(), Int32.Parse(mtbmincomida.Text.Trim()), icvdia, mtbhrsalcomerd.Text.Trim(),
                                              icvdia, mtbhrsalcomerh.Text.Trim(), Int32.Parse(mtbhrjornada.Text.Trim()), LoginInfo.IdTrab, this.Name);
            }
            else if (iactbtn == 2)
            {
                PlantDet.rechcplantilla_d_suid(2, icvplantilla, icvdia, mtbhrinijor.Text.Trim(), Int32.Parse(cbdiasalida.SelectedValue.ToString()),
                                              mtbhterminojornada.Text.Trim(), Int32.Parse(mtbmincomida.Text.Trim()), icvdia, mtbhrsalcomerd.Text.Trim(),
                                              icvdia, mtbhrsalcomerh.Text.Trim(), Int32.Parse(mtbhrjornada.Text.Trim()), LoginInfo.IdTrab, this.Name);
            }
            else if (iactbtn == 3)
            {
                PlantDet.rechcplantilla_d_suid(3, icvplantilla, icvdia, mtbhrinijor.Text.Trim(), Int32.Parse(cbdiasalida.SelectedValue.ToString()),
                                              mtbhterminojornada.Text.Trim(), Int32.Parse(mtbmincomida.Text.Trim()), icvdia, mtbhrsalcomerd.Text.Trim(),
                                              icvdia, mtbhrsalcomerh.Text.Trim(), Int32.Parse(mtbhrjornada.Text.Trim()), LoginInfo.IdTrab, this.Name);
            }
            else
            {
            }


            dgvrechcplantilla_d.DataSource = null;

            inumcolumngrid = dgvrechcplantilla_d.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvrechcplantilla_d.Columns.RemoveAt(0);
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }

        }

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvrechcplantilla_d.DataSource = null;

            inumcolumngrid = dgvrechcplantilla_d.ColumnCount;

            if (inumcolumngrid == 1)
            {
                dgvrechcplantilla_d.Columns.RemoveAt(0);
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            
        }

        //boton minimizar
        private void btnminimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        //boton regresar
        private void btnregresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }
        //botòn cerrar
        private void btncerrar_Click(object sender, EventArgs e)
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
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        //evento load
        private void PlantillasDetalles_Load(object sender, EventArgs e)
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

            //función para tool tip
            ftooltip();

            //variables accesos
            DataTable Permisos = DatPerfil.accpantalla(LoginInfo.IdTrab, this.Name);
            iins = Int32.Parse(Permisos.Rows[0][3].ToString());
            iact = Int32.Parse(Permisos.Rows[0][4].ToString());
            ielim = Int32.Parse(Permisos.Rows[0][5].ToString());

            //llena combo plantilla
            Util.cargarcombo(cbplantilla,PlantDet.cbplantilla(5));
            //cbplantilla.Text = "";

            //prueba llenado de grid
            fdleccllenadogrid();

        }
        //checkbox limpiar registro
        private void cbxlimpiarreg_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxlimpiarreg.Checked == true)
            {
                btnguardar.Image = Resources.Baja;
                lbluid.Text = "     Elimina Incidencia de Nomina";
                iactbtn = 3;
            }
            else
            {
                btnguardar.Image = Resources.Editar;
                lbluid.Text = "     Modifica Incidencia de Nomina";
                iactbtn = 2;
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        //funcion para tool tip
        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuraciòn
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btncerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnminimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnregresar, "Regresar");
            toolTip1.SetToolTip(this.btnbuscar, "Busca Registro");
        }
        private void fllenacbplantilla()
        {
            //llena cbplantilla

        }

        protected void fdleccllenadogrid()
        {

            if (iins == 1 && iact == 1 && ielim == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else if (iins == 1 && iact == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else if (iins == 1 && ielim == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else if (iact == 1 && ielim == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else if (iins == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else if (iact == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else if (ielim == 1)
            {
                fformatgriduid(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }
            else
            {
                fformatgrids(4, Int32.Parse(cbplantilla.SelectedValue.ToString()));
            }

        }

        //funcion llenado y formto grid con modificación busqueda con permisos
        protected void fformatgriduid(int iopcion, int icvplantilla)
        {
            dgvrechcplantilla_d.DataSource = null;
            DataTable dtiplandet = PlantDet.dgvplantillaDet(iopcion, icvplantilla);
            dgvrechcplantilla_d.DataSource = dtiplandet;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvrechcplantilla_d.Columns.Insert(0, imgCheckUsuarios);
            dgvrechcplantilla_d.Columns[0].HeaderText = "Selección";

            dgvrechcplantilla_d.Columns[0].Width = 95;
            dgvrechcplantilla_d.Columns[1].Visible = false;
            dgvrechcplantilla_d.Columns[2].Visible = false;
            dgvrechcplantilla_d.Columns[3].Visible = false;
            dgvrechcplantilla_d.Columns[4].Width = 70;
            dgvrechcplantilla_d.Columns[5].Width = 100;
            dgvrechcplantilla_d.Columns[6].Visible = false;
            dgvrechcplantilla_d.Columns[7].Width = 110;
            dgvrechcplantilla_d.Columns[8].Width = 100;
            dgvrechcplantilla_d.Columns[9].Width = 105;
            dgvrechcplantilla_d.Columns[10].Visible = false;
            dgvrechcplantilla_d.Columns[11].Visible = false;//
            dgvrechcplantilla_d.Columns[12].Width = 100;
            dgvrechcplantilla_d.Columns[13].Visible = false;
            dgvrechcplantilla_d.Columns[14].Width = 110;
            dgvrechcplantilla_d.Columns[15].Width = 100;
            dgvrechcplantilla_d.Columns[16].Width = 80;
            dgvrechcplantilla_d.Columns[17].Visible = false;
            dgvrechcplantilla_d.ClearSelection();
            lblnotifmodif.Visible = true;

        }

        //funcion llenado y formto grid con modificación busqueda sin permisos
        protected void fformatgrids(int iopcion, int icvplantilla)
        {
            dgvrechcplantilla_d.DataSource = null;
            DataTable dtiplandet = PlantDet.dgvplantillaDet(iopcion, icvplantilla);
            dgvrechcplantilla_d.DataSource = dtiplandet;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvrechcplantilla_d.Columns.Insert(0, imgCheckUsuarios);
            dgvrechcplantilla_d.Columns[0].HeaderText = "Selección";

            dgvrechcplantilla_d.Columns[0].Visible = false;
            dgvrechcplantilla_d.Columns[1].Visible = false;
            dgvrechcplantilla_d.Columns[2].Visible = false;
            dgvrechcplantilla_d.Columns[3].Visible = false;
            dgvrechcplantilla_d.Columns[4].Width = 70;
            dgvrechcplantilla_d.Columns[5].Width = 100;
            dgvrechcplantilla_d.Columns[6].Visible = false;
            dgvrechcplantilla_d.Columns[7].Width = 110;
            dgvrechcplantilla_d.Columns[8].Width = 100;
            dgvrechcplantilla_d.Columns[9].Width = 105;
            dgvrechcplantilla_d.Columns[10].Visible = false;
            dgvrechcplantilla_d.Columns[11].Width = 110;
            dgvrechcplantilla_d.Columns[12].Width = 100;
            dgvrechcplantilla_d.Columns[13].Visible = false;
            dgvrechcplantilla_d.Columns[14].Width = 110;
            dgvrechcplantilla_d.Columns[15].Width = 100;
            dgvrechcplantilla_d.Columns[16].Width = 80;
            dgvrechcplantilla_d.Columns[17].Visible = false;
            dgvrechcplantilla_d.ClearSelection();
        }

        private void dgvrechcplantilla_d_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow row = this.dgvrechcplantilla_d.SelectedRows[0];
            //iexistereg = Convert.ToInt32(row.Cells["stexiste"].Value.ToString());
            //icvplantilla = Convert.ToInt32(row.Cells["cvplantilla"].Value.ToString());
            //icvdia = Convert.ToInt32(row.Cells["cvdia"].Value.ToString());

            //if (iins == 1 && iact == 1 && ielim == 1 && iexistereg == 1)//insertar-actualizar-eliminar EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
 
            //    cbxlimpiarreg.Visible = true;
            //    cbxlimpiarreg.Checked = false;
            //    btnguardar.Image = Resources.Editar;
            //    mtbhrinijor.Focus();
            //    iactbtn = 2;
   
            //}
            //else if(iins == 1 && iact == 1 && ielim == 1 && iexistereg == 0)//insertar-actualizar-eliminar NO EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = false;
            //    btnguardar.Image = Resources.Guardar;
            //    mtbhrinijor.Focus();
            //    iactbtn = 1;
            //}
            //else if (iins == 1 && iact == 1 && iexistereg == 1)//insertar-actualizar EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = false;
            //    btnguardar.Image = Resources.Editar;
            //    mtbhrinijor.Focus();
            //    iactbtn = 2;
            //}
            //else if (iins == 1 && iact == 1 && iexistereg == 0)//insertar-actualizar NO EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = false;
            //    btnguardar.Image = Resources.Guardar;
            //    mtbhrinijor.Focus();
            //    iactbtn = 1;
            //}
            //else if (iins == 1 && ielim == 1 && iexistereg == 1)//insertar-eliminar EXISTE registro
            //{

            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = false;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = false;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = false;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = false;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = false;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = false;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = false;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = true;
            //    cbxlimpiarreg.Checked = true;
            //    cbxlimpiarreg.Enabled = false;
            //    btnguardar.Image = Resources.Baja;
            //    btnguardar.Focus();
            //    iactbtn = 3;
            //}
            //else if (iins == 1 && ielim == 1 && iexistereg == 0)//insertar-eliminar NO EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = false;
            //    btnguardar.Image = Resources.Baja;
            //    mtbhrinijor.Focus();
            //    iactbtn = 1;
            //}
            //else if (iact == 1 && ielim == 1 && iexistereg == 1)//actualizar-eliminar EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = true;
            //    cbxlimpiarreg.Checked = false;
            //    btnguardar.Image = Resources.Baja;
            //    btnguardar.Focus();
            //    iactbtn = 3;
            //}
            //else if (iact == 1 && ielim == 1 && iexistereg == 0)//actualizar-eliminar NO EXISTE registro
            //{
            //    pnlmenssuid.Visible = false;
            //    DialogResult result = MessageBox.Show("No tienes permisos para agregar datos al registro", "SIPAA", MessageBoxButtons.OK);
            //}
            //else if (iins == 1 && iexistereg == 1)//inserta EXISTE registro
            //{
            //    pnlpland.Visible = false;
            //    DialogResult result = MessageBox.Show("No tienes permisos para agregar datos al registro", "SIPAA", MessageBoxButtons.OK);
            //}
            //else if (iins == 1 && iexistereg == 0)//inserta EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = false;
            //    btnguardar.Image = Resources.Guardar;
            //    mtbhrinijor.Focus();
            //    iactbtn = 1;
            //}
            //else if (iact == 1 && iexistereg == 1)//inserta EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = true;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = true;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = true;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = true;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = true;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = true;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = true;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = false;
            //    btnguardar.Image = Resources.Guardar;
            //    mtbhrinijor.Focus();
            //    iactbtn = 2;
            //}
            //else if (iact == 1 && iexistereg == 0)//inserta NO EXISTE registro
            //{
            //    pnlpland.Visible = false;
            //    DialogResult result = MessageBox.Show("No tienes permisos para agregar datos al registro", "SIPAA", MessageBoxButtons.OK);
            //}
            //else if (ielim == 1 && iexistereg == 1)//inserta EXISTE registro
            //{
            //    pnlpland.Visible = true;

            //    cbdia.Enabled = false;
            //    Util.cargarcombo(cbdia, PlantDet.cbdias(6));
            //    cbdia.Text = "";

            //    cbdiasalida.Enabled = false;
            //    Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));
            //    cbdiasalida.Text = "";

            //    cbdia.Text = row.Cells["Día"].Value.ToString();
            //    mtbhrinijor.Enabled = false;
            //    mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
            //    cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
            //    mtbhterminojornada.Enabled = false;
            //    mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
            //    mtbmincomida.Enabled = false;
            //    mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
            //    mtbhrjornada.Enabled = false;
            //    mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
            //    mtbhrsalcomerd.Enabled = false;
            //    mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
            //    mtbhrsalcomerh.Enabled = false;
            //    mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();

            //    cbxlimpiarreg.Visible = true;
            //    cbxlimpiarreg.Checked = true;
            //    cbxlimpiarreg.Enabled = false;
            //    btnguardar.Image = Resources.Baja;
            //    btnguardar.Focus();
            //    iactbtn = 3;
            //}
            //else if (ielim == 1 && iexistereg == 0)//inserta EXISTE registro
            //{
            //    pnlpland.Visible = false;
            //    DialogResult result = MessageBox.Show("No tienes permisos para limpiar el registro", "SIPAA", MessageBoxButtons.OK);
            //}
            //else
            //{
            //    pnlmenssuid.Visible = false;
            //    DialogResult result = MessageBox.Show("Revise sus permisos con el administrador del sistema", "SIPAA", MessageBoxButtons.OK);
            //}

        }

        private void pnlpland_Paint(object sender, PaintEventArgs e)
        {

        }

        private void actualizaGrid()
        {
            if (dgvrechcplantilla_d.SelectedRows.Count > 0)
            {
                DataGridViewRow row = this.dgvrechcplantilla_d.SelectedRows[0];

                for (int i = 0; i < dgvrechcplantilla_d.Rows.Count; i++)
                {
                    dgvrechcplantilla_d.Rows[i].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }

        private void dgvrechcplantilla_d_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            actualizaGrid();

            DataGridViewRow row = this.dgvrechcplantilla_d.SelectedRows[0];
            iexistereg = Convert.ToInt32(row.Cells["stexiste"].Value.ToString());
            icvplantilla = Convert.ToInt32(row.Cells["cvplantilla"].Value.ToString());
            icvdia = Convert.ToInt32(row.Cells["cvdia"].Value.ToString());

            if (iins == 1 && iact == 1 && ielim == 1 && iexistereg == 1)//insertar-actualizar-eliminar EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = true;
                cbxlimpiarreg.Checked = false;
                btnguardar.Image = Resources.Editar;
                mtbhrinijor.Focus();
                iactbtn = 2;

            }
            else if (iins == 1 && iact == 1 && ielim == 1 && iexistereg == 0)//insertar-actualizar-eliminar NO EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = false;
                btnguardar.Image = Resources.Guardar;
                mtbhrinijor.Focus();
                iactbtn = 1;
            }
            else if (iins == 1 && iact == 1 && iexistereg == 1)//insertar-actualizar EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = false;
                btnguardar.Image = Resources.Editar;
                mtbhrinijor.Focus();
                iactbtn = 2;
            }
            else if (iins == 1 && iact == 1 && iexistereg == 0)//insertar-actualizar NO EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = false;
                btnguardar.Image = Resources.Guardar;
                mtbhrinijor.Focus();
                iactbtn = 1;
            }
            else if (iins == 1 && ielim == 1 && iexistereg == 1)//insertar-eliminar EXISTE registro
            {

                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = false;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = false;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = false;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = false;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = false;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = false;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = false;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = true;
                cbxlimpiarreg.Checked = true;
                cbxlimpiarreg.Enabled = false;
                btnguardar.Image = Resources.Baja;
                btnguardar.Focus();
                iactbtn = 3;
            }
            else if (iins == 1 && ielim == 1 && iexistereg == 0)//insertar-eliminar NO EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = false;
                btnguardar.Image = Resources.Baja;
                mtbhrinijor.Focus();
                iactbtn = 1;
            }
            else if (iact == 1 && ielim == 1 && iexistereg == 1)//actualizar-eliminar EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = true;
                cbxlimpiarreg.Checked = false;
                btnguardar.Image = Resources.Baja;
                btnguardar.Focus();
                iactbtn = 3;
            }
            else if (iact == 1 && ielim == 1 && iexistereg == 0)//actualizar-eliminar NO EXISTE registro
            {
                pnlmenssuid.Visible = false;
                DialogResult result = MessageBox.Show("No tienes permisos para agregar datos al registro", "SIPAA", MessageBoxButtons.OK);
            }
            else if (iins == 1 && iexistereg == 1)//inserta EXISTE registro
            {
                pnlpland.Visible = false;
                DialogResult result = MessageBox.Show("No tienes permisos para agregar datos al registro", "SIPAA", MessageBoxButtons.OK);
            }
            else if (iins == 1 && iexistereg == 0)//inserta EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = false;
                btnguardar.Image = Resources.Guardar;
                mtbhrinijor.Focus();
                iactbtn = 1;
            }
            else if (iact == 1 && iexistereg == 1)//inserta EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = true;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = true;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = true;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = true;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = true;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = true;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = true;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = false;
                btnguardar.Image = Resources.Guardar;
                mtbhrinijor.Focus();
                iactbtn = 2;
            }
            else if (iact == 1 && iexistereg == 0)//inserta NO EXISTE registro
            {
                pnlpland.Visible = false;
                DialogResult result = MessageBox.Show("No tienes permisos para agregar datos al registro", "SIPAA", MessageBoxButtons.OK);
            }
            else if (ielim == 1 && iexistereg == 1)//inserta EXISTE registro
            {
                pnlpland.Visible = true;

                cbdia.Enabled = false;
                Util.cargarcombo(cbdia, PlantDet.cbdias(6));
                cbdia.Text = "";

                cbdiasalida.Enabled = false;
                Util.cargarcombo(cbdiasalida, PlantDet.cbdias(6));

                cbdia.Text = row.Cells["Día"].Value.ToString();
                mtbhrinijor.Enabled = false;
                mtbhrinijor.Text = row.Cells["HrEntTurno"].Value.ToString();
                cbdiasalida.Text = row.Cells["DíaSalTurno"].Value.ToString();
                mtbhterminojornada.Enabled = false;
                mtbhterminojornada.Text = row.Cells["HrSalTurno"].Value.ToString();
                mtbmincomida.Enabled = false;
                mtbmincomida.Text = row.Cells["TiempoComida"].Value.ToString();
                mtbhrjornada.Enabled = false;
                mtbhrjornada.Text = row.Cells["TotJornada"].Value.ToString();
                mtbhrsalcomerd.Enabled = false;
                mtbhrsalcomerd.Text = row.Cells["HrSalComer"].Value.ToString();
                mtbhrsalcomerh.Enabled = false;
                mtbhrsalcomerh.Text = row.Cells["HrRegComida"].Value.ToString();
                cbdiasalida.Text = row.Cells["Día"].Value.ToString();

                cbxlimpiarreg.Visible = true;
                cbxlimpiarreg.Checked = true;
                cbxlimpiarreg.Enabled = false;
                btnguardar.Image = Resources.Baja;
                btnguardar.Focus();
                iactbtn = 3;
            }
            else if (ielim == 1 && iexistereg == 0)//inserta EXISTE registro
            {
                pnlpland.Visible = false;
                DialogResult result = MessageBox.Show("No tienes permisos para limpiar el registro", "SIPAA", MessageBoxButtons.OK);
            }
            else
            {
                pnlmenssuid.Visible = false;
                DialogResult result = MessageBox.Show("Revise sus permisos con el administrador del sistema", "SIPAA", MessageBoxButtons.OK);
            }
        }

        private void mtbhrinijor_MouseClick(object sender, MouseEventArgs e)
        {
            mtbhrinijor.SelectAll();
        }

        private void mtbhrinijor_Enter(object sender, EventArgs e)
        {
            mtbhrinijor.SelectionStart = 0;
        }

        private void mtbhrjornada_MouseClick(object sender, MouseEventArgs e)
        {
            mtbhrjornada.SelectAll();
        }

        private void mtbhrjornada_Enter(object sender, EventArgs e)
        {
            mtbhrjornada.SelectionStart = 0;
        }

        private void mtbhterminojornada_MouseClick(object sender, MouseEventArgs e)
        {
            mtbhterminojornada.SelectAll();
        }

        private void mtbhterminojornada_Enter(object sender, EventArgs e)
        {
            mtbhterminojornada.SelectionStart = 0;
        }

        private void mtbmincomida_MouseClick(object sender, MouseEventArgs e)
        {
            mtbmincomida.SelectAll(); 
        }

        private void mtbmincomida_Enter(object sender, EventArgs e)
        {
            mtbmincomida.SelectionStart = 0;
        }

        private void mtbhrsalcomerd_MouseClick(object sender, MouseEventArgs e)
        {
            mtbhrsalcomerd.SelectAll();
        }

        private void mtbhrsalcomerd_Enter(object sender, EventArgs e)
        {
            mtbhrsalcomerd.SelectionStart = 0; 
        }

        private void mtbhrsalcomerh_MouseClick(object sender, MouseEventArgs e)
        {
            mtbhrsalcomerh.SelectAll(); 
        }

        private void mtbhrsalcomerh_Enter(object sender, EventArgs e)
        {
            mtbhrsalcomerh.SelectionStart = 0; 
        }
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
