using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.Properties;
using SIPAA_CS.App_Code;
using SIPAA_CS.RecursosHumanos.Asignaciones;


//***********************************************************************************************
//Autor: Noe Alvarez Marquina
//Fecha creación: 15-mar-2017      Última Modificacion: dd-mm-aaaa
//Descripción: administra incidencias de nomina
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos
{
    public partial class IncidenciasNomimas : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        
        int p_rep;

        int icvincidencia;
        int icvrepresenta;
        int istdir;
        int iidformapago;
        int istpremio;
        int icvtipohr;
        int iverifpk;

        string svalidacampos;

        #endregion

        IncNomina IncNom = new IncNomina();
        Utilerias Util = new Utilerias();

        public IncidenciasNomimas()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        // combo incidencia busqueda
        private void cboincnombusq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Util.p_inicbo == 1)
            {
                Util.cargarcombo(cborepbusq, IncNom.cboRep(7, cboincnombusq.SelectedValue.ToString()));
            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        //accion al tocar grid conforme a permisos del usuario
        private void dgvincnomia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pins == 1 && pact == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btninsertar, 2, false);
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                pactbtn = 2;
            }
            else if (pins == 1 && pact == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pins == 1 && pelim == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                pactbtn = 2;
            }
            else if (pact == 1 && pelim == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                pactbtn = 2;
            }
            else if (pins == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pact == 1)
            {
                Util.ChangeButton(btninsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pelim == 1)
            {
                Util.ChangeButton(btninsertar, 3, false);
                factgrid();
                ckbEliminar.Visible = true;
                ckbEliminar.Checked = false;
                pactbtn = 3;
            }
            else
            {

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ckbEliminar.Visible = false;
            pnlincnom.Visible = true;
            lbluid.Text = "     Agregar Incidencia Nomina";
            btninsertar.Image = Resources.Guardar;
            fcargarcbo();
            cbotipohr.Enabled = true;
            cbotipohr.Text = "";
            cbostdir.Enabled = true;
            cbostdir.Text = "";
            cbopremio.Enabled = true;
            cbopremio.Text = "";
            cbopasanom.Text = "";
            cboafect.Text = "";
            cbformapago.Text = "";
            cbformapago.Enabled = true;
            txtcampo.Text = "";
            ckbEliminar.Checked = false;
            cbostdir.Focus();
            pactbtn = 1;
        }

        //boton guardar
        private void btninsertar_Click(object sender, EventArgs e)
        {
            if (cboincnombusq.SelectedIndex == -1)
            {
                lbMensaje.Text = "Capture un dato a guardar";
            }
            else if (pactbtn == 1)//insertar
            {
                //valida campos
                fvalidacampos();

                if (svalidacampos != "0")
                {
                    DialogResult result = MessageBox.Show(svalidacampos, "SIPAA", MessageBoxButtons.OK);
                }
                else
                {

                    //verifica llave primaria
                    iverifpk = IncNom.rechincnominapk(6, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()),
                                                     Int32.Parse(cbostdir.SelectedValue.ToString()), Int32.Parse(cbformapago.SelectedValue.ToString()),
                                                     Int32.Parse(cbopremio.SelectedValue.ToString()), Int32.Parse(cboafect.SelectedValue.ToString()),
                                                     Int32.Parse(cbopasanom.SelectedValue.ToString()), Int32.Parse(cbotipohr.SelectedValue.ToString()),
                                                     txtcampo.Text.Trim(), "null", 1, "nam", "IncidenciasNominas");

                    if (iverifpk > 0)
                    {
                        DialogResult result = MessageBox.Show("El registro uque intenta guardar ya existe", "SIPAA", MessageBoxButtons.OK);
                    }
                    else
                    {

                        //inserta registro nuevo
                        p_rep = IncNom.rechincnominasuid(1, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()),
                                                         Int32.Parse(cbostdir.SelectedValue.ToString()), Int32.Parse(cbformapago.SelectedValue.ToString()),
                                                         Int32.Parse(cbopremio.SelectedValue.ToString()), Int32.Parse(cboafect.SelectedValue.ToString()),
                                                         Int32.Parse(cbopasanom.SelectedValue.ToString()), Int32.Parse(cbotipohr.SelectedValue.ToString()),
                                                         txtcampo.Text.Trim(), "null", 1, "nam", "IncidenciasNominas");
                        dgvincnomia.DataSource = null;

                        if (pins == 1 && pact == 0 && pelim == 0)
                        {

                        }
                        else
                        {
                            dgvincnomia.Columns.RemoveAt(0);
                        }

                        pnlincnom.Visible = false;
                        panelTag.Visible = true;
                        timer1.Start();
                        //llena grid con datos existente
                        fllenagridbusqueda();

                    }
                }

            }
            else if (pactbtn == 2)//actualizar
            {
                //actualizar registro nuevo
                p_rep = IncNom.rechincnominasuid(2, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()),
                                                Int32.Parse(cbostdir.SelectedValue.ToString()), Int32.Parse(cbformapago.SelectedValue.ToString()),
                                                Int32.Parse(cbopremio.SelectedValue.ToString()), Int32.Parse(cboafect.SelectedValue.ToString()),
                                                Int32.Parse(cbopasanom.SelectedValue.ToString()), Int32.Parse(cbotipohr.SelectedValue.ToString()),
                                                txtcampo.Text.Trim(), "null", 1, "nam", "IncidenciasNominas");
                dgvincnomia.DataSource = null;

                if (pins == 1 && pact == 0 && pelim == 0)
                {

                }
                else
                {
                    dgvincnomia.Columns.RemoveAt(0);
                }

                pnlincnom.Visible = false;
                panelTag.Visible = true;
                timer1.Start();
                //llena grid con datos existente
                fllenagridbusqueda();
            }
            else if (pactbtn == 3)//eliminar
            {
                //eliminar registro
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    p_rep = IncNom.rechincnominasuid(3, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()),
                                                     Int32.Parse(cbostdir.SelectedValue.ToString()), Int32.Parse(cbformapago.SelectedValue.ToString()),
                                                     Int32.Parse(cbopremio.SelectedValue.ToString()), Int32.Parse(cboafect.SelectedValue.ToString()),
                                                     Int32.Parse(cbopasanom.SelectedValue.ToString()), Int32.Parse(cbotipohr.SelectedValue.ToString()),
                                                     txtcampo.Text.Trim(), "null", 1, "nam", "IncidenciasNominas");
                    dgvincnomia.DataSource = null;

                    if (pins == 1 && pact == 0 && pelim == 0)
                    {

                    }
                    else
                    {
                        dgvincnomia.Columns.RemoveAt(0);
                    }

                    pnlincnom.Visible = false;
                    panelTag.Visible = true;
                    timer1.Start();
                    //llena grid con datos existente
                    fllenagridbusqueda();
                }
                else if (result == DialogResult.No)
                {
                    ckbEliminar.Checked = false;
                    Util.ChangeButton(btninsertar, 2, false);
                }  
            }

            switch (p_rep.ToString())
            {
                case "1":
                    lbMensaje.Text = "Registro agregado correctamente";
                    break;
                case "2":
                    lbMensaje.Text = "Registro modificado correctamente";
                    break;
                case "3":
                    lbMensaje.Text = "Registro eliminado correctamente";
                    break;
                default:
                    lbMensaje.Text = "";
                    break;
            }

        }

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvincnomia.DataSource = null;
            if (pins == 1 && pact == 0 && pelim == 0)
            {

            }
            else
            {
                dgvincnomia.Columns.RemoveAt(0);
            }

            //llama funcion para llenar el grid
            fllenagridbusqueda();

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
        private void IncidenciasNom_Load(object sender, EventArgs e)
        {
            //habilita tool tip
            ftooltip();

            //variables accesos
            pins = 1;
            pact = 1;
            pelim =1;

            pactbtn = 0;
            p_rep = 0;

            Util.cargarcombo(cboincnombusq, IncNom.cboInc(4));
            Util.cargarcombo(cborepbusq, IncNom.cboRep(7, cboincnombusq.SelectedValue.ToString()));

            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }
            //llama funcion para llenar el grid
            fllenagridbusqueda();
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
                Util.ChangeButton(btninsertar, 3, false);
                lbluid.Text = "     Elimina Incidencia de Nomina";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btninsertar, 2, false);
                lbluid.Text = "     Modifica Incidencia de Nomina";
                pactbtn = 2;
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

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
        }
        protected void fllenagridbusqueda()
        {
            Util.p_inicbo = 0;
            if (cboincnombusq.SelectedIndex != -1 && cborepbusq.SelectedIndex != -1)
            {
                if (pins == 1 && pact == 1 && pelim == 1)
                {
                    fformatgrididb(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else if (pins == 1 && pact == 1)
                {
                    fformatgrididb(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else if (pins == 1 && pelim == 1)
                {
                    fformatgrididb(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else if (pact == 1 && pelim == 1)
                {
                    fformatgrididb(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else if (pins == 1)
                {
                    fformatgrididbsm(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else if (pact == 1)
                {
                    fformatgrididbsm(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else if (pelim == 1)
                {
                    fformatgrididb(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }
                else
                {
                    fformatgrididbsm(4, Int32.Parse(cboincnombusq.SelectedValue.ToString()), Int32.Parse(cborepbusq.SelectedValue.ToString()));
                }

            }
            else
            {
                //llena grid con datos existente
                //fgtphr(4);
            }

            Util.p_inicbo = 1;


        }


        //funcion formto grid con modificación busqueda con permisos
        protected void fformatgrididb(int p_opcion, int p_cvincidencia, int p_cvrepresenta)
        {
            DataTable dtinnom = IncNom.obtincnominair(p_opcion, p_cvincidencia, p_cvrepresenta);
            dgvincnomia.DataSource = dtinnom;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "img";
            dgvincnomia.Columns.Insert(0, imgCheckUsuarios);
            dgvincnomia.Columns[0].HeaderText = "Selección";

            dgvincnomia.Columns[0].Width = 75;
            dgvincnomia.Columns[1].Visible = false;
            dgvincnomia.Columns[2].Visible = false;
            dgvincnomia.Columns[3].Visible = false;
            dgvincnomia.Columns[4].Visible = false;
            dgvincnomia.Columns[5].Visible = false;
            dgvincnomia.Columns[6].Visible = false;
            dgvincnomia.Columns[7].Visible = false;
            dgvincnomia.Columns[8].Visible = false;
            dgvincnomia.Columns[9].Width = 75;
            dgvincnomia.Columns[10].Width = 100;
            dgvincnomia.Columns[11].Width = 65;
            dgvincnomia.Columns[12].Width = 180;
            dgvincnomia.Columns[13].Width = 90;
            dgvincnomia.Columns[14].Width = 90;
            dgvincnomia.Columns[15].Width = 120;
            dgvincnomia.Columns[16].Visible = false;
            dgvincnomia.Columns[17].Visible = false;
            dgvincnomia.ClearSelection();
            lblModif.Visible = true;
        }

        //funcion formto grid sin modificación busqueda
        protected void fformatgrididbsm(int p_opcion, int p_cvincidencia, int p_cvrepresenta)
        {
            DataTable dtinnom = IncNom.obtincnominair(p_opcion, p_cvincidencia, p_cvrepresenta);
            dgvincnomia.DataSource = dtinnom;

            dgvincnomia.Columns[0].Visible = false;
            dgvincnomia.Columns[1].Visible = false;
            dgvincnomia.Columns[2].Visible = false;
            dgvincnomia.Columns[3].Visible = false;
            dgvincnomia.Columns[4].Visible = false;
            dgvincnomia.Columns[5].Visible = false;
            dgvincnomia.Columns[6].Width = 75;
            dgvincnomia.Columns[7].Width = 75;
            dgvincnomia.Columns[8].Width = 75;
            dgvincnomia.Columns[9].Width = 120;
            dgvincnomia.Columns[10].Width =90;
            dgvincnomia.Columns[11].Width = 90;
            dgvincnomia.Columns[12].Width = 140;
            dgvincnomia.Columns[13].Visible = false;
            dgvincnomia.Columns[14].Visible = false;
            dgvincnomia.ClearSelection();
            lblModif.Visible = false;
        }


        //llena los combos para guardar nuevo registro
        private void fcargarcbo()
        {
            Util.cargarcombo(cbostdir, IncNom.cboEsNoPr(5));
            Util.cargarcombo(cbformapago, IncNom.cboPeriodoTipo(4));
            Util.cargarcombo(cbopremio, IncNom.cboEsNoPr(5));
            Util.cargarcombo(cboafect, IncNom.cboAfec(4));
            Util.cargarcombo(cbopasanom, IncNom.cboEsNoPr(5));
            Util.cargarcombo(cbotipohr, IncNom.cboTipoHr(4));
        }

        //llena datos al dar click en registro
        private void factgrid()
        {
            if (pins == 1 && pact == 0 && pelim == 0)
            {
            }
            else
            {
                for (int iContador = 0; iContador < dgvincnomia.Rows.Count; iContador++)
                {
                    dgvincnomia.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }

                if (dgvincnomia.SelectedRows.Count != 0)
                {
                    pnlincnom.Visible = true;
                    DataGridViewRow row = this.dgvincnomia.SelectedRows[0];
                    icvincidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());
                    icvrepresenta = Convert.ToInt32(row.Cells["cvrepresenta"].Value.ToString());
                    istdir = Convert.ToInt32(row.Cells["stdir"].Value.ToString());
                    iidformapago = Convert.ToInt32(row.Cells["stpremio"].Value.ToString());
                    istpremio = Convert.ToInt32(row.Cells["stpremio"].Value.ToString());
                    icvtipohr = Convert.ToInt32(row.Cells["cvtipohr"].Value.ToString());
                    lbluid.Text = "     Modifica Incidencia de Nomina";
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    Util.cargarcombo(cbostdir, IncNom.cboEsNoPr(5));
                    cbostdir.Text = row.Cells["AutDir"].Value.ToString();
                    cbostdir.Enabled = false;

                    Util.cargarcombo(cbformapago, IncNom.cboPeriodoTipo(4));
                    cbformapago.Text = row.Cells["FormaPago"].Value.ToString();
                    cbformapago.Enabled = false;

                    Util.cargarcombo(cbopremio, IncNom.cboEsNoPr(5));
                    cbopremio.Text = row.Cells["Premio"].Value.ToString();
                    cbopremio.Enabled = false;

                    Util.cargarcombo(cboafect, IncNom.cboAfec(4));
                    cboafect.Text = row.Cells["ConceptoNomina"].Value.ToString();

                    Util.cargarcombo(cbopasanom, IncNom.cboEsNoPr(5));
                    cbopasanom.Text = row.Cells["PasaNomina"].Value.ToString();

                    Util.cargarcombo(cbotipohr, IncNom.cboTipoHr(4));
                    cbotipohr.Text = row.Cells["TipoHorario"].Value.ToString();
                    cbotipohr.Enabled = false;

                    txtcampo.Text = row.Cells["Campo"].Value.ToString();
                    cboafect.Focus();
                }
            }
        }

        //funcion validar campos
        protected string fvalidacampos()
        {

            if (cboincnombusq.Text == "") {svalidacampos = "Selecione una incidencia"; cboincnombusq.Focus(); }
            else if (cboincnombusq.SelectedIndex == -1) { svalidacampos = "Selecione una incidencia"; cboincnombusq.Focus(); }

            else if(cborepbusq.Text == "") { svalidacampos = "Selecione que representa"; cborepbusq.Focus(); }
            else if (cborepbusq.SelectedIndex == -1) { svalidacampos = "Selecione que representa"; cborepbusq.Focus(); }

            else if (cbostdir.Text == "") { svalidacampos = "Selecione si autoriza Director"; cbostdir.Focus(); }
            else if (cbostdir.SelectedIndex == -1) { svalidacampos = "Selecione si autoriza Director"; cbostdir.Focus(); }

            else if (cbformapago.Text == "") { svalidacampos = "Selecione un periodo"; cbformapago.Focus(); }
            else if (cbformapago.SelectedIndex == -1) { svalidacampos = "Selecione un periodo"; cbformapago.Focus(); }

            else if (cbopremio.Text == "") { svalidacampos = "Selecione si afecta premio"; cbopremio.Focus(); }
            else if (cbopremio.SelectedIndex == -1) { svalidacampos = "Selecione si afecta premio"; cbopremio.Focus(); }

            else if (cboafect.Text == "") { svalidacampos = "Selecione concepto que afecta"; cboafect.Focus(); }
            else if (cboafect.SelectedIndex == -1) { svalidacampos = "Selecione concepto que afecta"; cboafect.Focus(); }

            else if (cbopasanom.Text == "") { svalidacampos = "Selecione si pasa a nomina"; cbopasanom.Focus(); }
            else if (cbopasanom.SelectedIndex == -1) { svalidacampos = "Selecione si pasa a nomina"; cbopasanom.Focus(); }

            else if (cbotipohr.Text == "") { svalidacampos = "Selecione tipo de horario"; cbotipohr.Focus(); }
            else if (cbotipohr.SelectedIndex == -1) { svalidacampos = "Selecione tipo de horario"; cbotipohr.Focus(); }

            //else if (txtcampo.Text == "") { svalidacampos = "Capture un comentario"; txtcampo.Focus(); }

            else { svalidacampos = "0";}
            return svalidacampos;
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
