using System;
using System.Globalization;
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
using static SIPAA_CS.App_Code.Usuario;
using zkemkeeper;

//***********************************************************************************************
//Autor: Jose Luis Alvarez Delgado
//Fecha creación: 20-Mar-2017       Última Modificacion: 30-Mar-2017 
//Descripción: Catalogo Mensajes
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{

    #region variables


    #endregion

    public partial class Mensajes : Form
    {
        #region

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        int p_rep;

        #endregion

        Utilerias Util = new Utilerias();
        public string sUsuuMod = LoginInfo.IdTrab;
        SonaTrabajador contenedorempleados = new SonaTrabajador();
        int tag = 253;

        public Mensajes()
        {
            InitializeComponent();
        }

        //se "instancia" la clase para usar todos los metodos que contenga
        Mensaje pantallaMensajes = new Mensaje();
        SonaTrabajador oTrabajador = new SonaTrabajador();

        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvMensajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pins == 1 && pact == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1 && pact == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                pactbtn = 2;
            }
            else if (pins == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pact == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                pactbtn = 2;
            }
            else if (pact == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                pactbtn = 2;
            }
            else if (pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 3, false);
                ckbEliminar.Visible = true;
                pactbtn = 3;
            }
            else
            {

            }
        }
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cbEmpleados.Text = "";
            // cbTrabajador.Text = "Seleccionar Empleado...";

            pnlmensajes.Visible = false;
            gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
            txtMensaje.Text = "";
            txtMensaje.Focus();
            dgvMensajes.Columns.RemoveAt(0);
            dgvMensajes.Columns[0].Width = 65;
            dgvMensajes.Columns[1].Width = 80;
            dgvMensajes.Columns[2].Width = 80;
            dgvMensajes.Columns[3].Width = 80;
            dgvMensajes.Columns[4].Width = 300;
        }

        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pnldatos.Visible = true;
            btnInsertar.Image = Resources.Guardar;
            ckbEliminar.Visible = false;
            pnlmensajes.Visible = true;
            lbluid.Text = "     Agregar Mensaje";
            Util.ChangeButton(btnAgregar, 1, false);
            pactbtn = 1;
            //txtidtrab.Text = "";
            cbEmpleados.Text = "";
            txtmensajeiu.Text = "";
            dtpfechainicial.Text = "";
            dtpfechafin.Text = "";
            //cbTrabajador.Text = "Seleccionar Empleado...";
            //cbTrabajador.Focus();
        }
        public CZKEMClass objCZKEM = new CZKEMClass();
        public bool Connect_Net(string IPAdd, int Port)
        {
            if (objCZKEM.Connect_Net(IPAdd, Port))
            {
                if (objCZKEM.RegEvent(1, 65535))
                {


                }
                return true;
            }
            return false;
        }


        private void EliminaMensajesReloj()
        {
            int tagReloj = 0;
            int minutos = 0;
            string comienzo = string.Empty;
            string contenido = string.Empty;
            //obtener el maximo de los mensajes
            int inicio = pantallaMensajes.fudimensajes(5, 160452, 1, "%", "%", "%", sUsuuMod, Name);
            int max = pantallaMensajes.fudimensajes(6, 160452, 1, "%", "%", "%", sUsuuMod, Name);

            for (int i = inicio; i <= max; i++)
            {
                if (objCZKEM.GetSMS(1, i, ref tagReloj, ref minutos, ref comienzo, ref contenido))
                    if (minutos != 0)
                    {

                        int Dia_Hoy = DateTime.Today.Day;
                        int Mes_Hoy = DateTime.Today.Month;
                        string Mes_Mensaje = comienzo.Substring(5, 2);
                        string Año_Mensaje = comienzo.Substring(0, 4);
                        string Dia_Mensaje = comienzo.Substring(8, 2);
                        if (Mes_Hoy == Convert.ToInt32(Mes_Mensaje))
                        {
                            if ((minutos / 1440) + Convert.ToInt32(Dia_Mensaje) < Dia_Hoy)
                                objCZKEM.DeleteSMS(1, i);

                        }
                        else
                        {
                            // string fullMonthName = new DateTime(Convert.ToInt32(Año_Mensaje), Convert.ToInt32(Mes_Mensaje), Convert.ToInt32(Dia_Mensaje)).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
                            int DiasdelMes = System.DateTime.DaysInMonth(Convert.ToInt32(Año_Mensaje), Convert.ToInt32(Mes_Mensaje));
                            DiasdelMes = DiasdelMes - Convert.ToInt32(Dia_Mensaje);
                            if (DiasdelMes > 0)
                                DiasdelMes += 1;
                            if (DiasdelMes > (minutos / 1140))
                                objCZKEM.DeleteSMS(1, i);
                            else
                            {
                                DiasdelMes = Math.Abs(DiasdelMes - (minutos / 1140));
                                if ((1 + DiasdelMes) < Dia_Hoy)
                                    objCZKEM.DeleteSMS(1, i);

                            }


                        }
                    }


            }
            objCZKEM.ClearSMS(1);
        }


        private void GuardaMensajeReloj()
        {
            int Horas = 0;
            int DiaFinal = dtpfechafin.Value.Day;
            int DiaInicial = dtpfechainicial.Value.Day;
            int MesFinal = dtpfechafin.Value.Month;
            int MesInicial = dtpfechafin.Value.Month;
            RelojChecador objReloj = new RelojChecador();
            DataTable dt = new DataTable();

            if (MesFinal == MesInicial)
            {
                if (DiaFinal > DiaInicial)
                    Horas = DiaFinal - DiaInicial;
            }
            if (Horas == 0)
                Horas = 1;
            else
                Horas += 1;
            Horas = Horas * 1440;


            bool bConexion = false;
            if (rbPublico.Checked == true)
            {


                dt = objReloj.RelojesxTrabajador(cbEmpleados.SelectedValue.ToString(), 0, 16, "%", "%");
                foreach (DataRow row in dt.Rows)
                {
                    lblMensaje.Text = "Enviando el mensaje a los relojes ";
                    bConexion = Connect_Net(row["ip"].ToString(), 4370);
                    if (bConexion != false)
                    {
                        lblMensaje.Text = "Enviando el mensaje a los relojes ";
                        p_rep = pantallaMensajes.fudimensajes(1, 1, 0, txtmensajeiu.Text, dtpfechainicial.Text.Trim(), dtpfechafin.Text.Trim(), sUsuuMod, Name);
                        EliminaMensajesReloj();
                        int max = pantallaMensajes.fudimensajes(6, 160452, 1, "%", "%", "%", sUsuuMod, Name);
                        objCZKEM.SetSMS(1, max, tag, Horas, dtpfechainicial.Value.Year + "-" + dtpfechainicial.Value.Month + "-" + dtpfechainicial.Value.Day + " 00:01:00", txtmensajeiu.Text);
                        objCZKEM.Disconnect();

                    }
                }



            }
            if (rbPersonal.Checked == true)
            {
                dt = objReloj.RelojesxTrabajador(cbEmpleados.SelectedValue.ToString(), 0, 15, "%", "%");


                foreach (DataRow row in dt.Rows)
                {
                    bConexion = Connect_Net(row["ip"].ToString(), 4370);
                    lblMensaje.Text = "Enviando el mensaje a los relojes asignados al empleado";
                    if (bConexion != false)
                    {
                        lblMensaje.Text = "Enviando ...";
                        p_rep = pantallaMensajes.fudimensajes(1, Convert.ToInt32(cbEmpleados.SelectedValue.ToString()), 0, txtmensajeiu.Text, dtpfechainicial.Text.Trim(), dtpfechafin.Text.Trim(), sUsuuMod, Name);
                        EliminaMensajesReloj();
                        int max = pantallaMensajes.fudimensajes(6, 160452, 1, "%", "%", "%", sUsuuMod, Name);
                        if (objCZKEM.SetSMS(1, max, tag, Horas, dtpfechainicial.Value.Year + "-" + dtpfechainicial.Value.Month + "-" + dtpfechainicial.Value.Day + " 00:01:00", txtmensajeiu.Text))
                            objCZKEM.SSR_SetUserSMS(1, cbEmpleados.SelectedValue.ToString(), max);
                    }

                    objCZKEM.Disconnect();
                }
            }


            switch (p_rep.ToString())
            {
                case "99":
                    lblMensaje.Text = "Registro agregado correctamente";
                    break;
                case "2":
                    lblMensaje.Text = "Registro modificado correctamente";
                    break;
                case "3":
                    lblMensaje.Text = "Registro eliminado correctamente";
                    break;
                case "1":
                    lblMensaje.Text = "Registro ya existe";
                    break;
                default:
                    lblMensaje.Text = "";
                    break;
            }





        }


        //boton
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtmensajeiu.Text.Trim() == "" && pactbtn == 1)
                lblMensaje.Text = "Capture un dato a guardar";
            if (rbPersonal.Checked == true)
            {
                if (string.IsNullOrEmpty(cbEmpleados.SelectedValue.ToString()))
                    lblMensaje.Text = "Tiene que capturar el numero del empleado";
            }
            if (pactbtn == 1)//insertar
            {
                //inserta registro nuevo 
                GuardaMensajeReloj();

                dgvMensajes.DataSource = null;
                dgvMensajes.Columns.RemoveAt(0);
                panelTag.Visible = true;
                cbEmpleados.Text = "";
                txtmensajeiu.Text = "";
                cbEmpleados.Focus();
                gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlmensajes.Visible = false;
            }
            else if (pactbtn == 2)//actualizar
            {
                //Actualizar
                int cvmensaje = 0;
                int idTrab = 0;
                if (dgvMensajes.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = this.dgvMensajes.SelectedRows[0];
                    cvmensaje = Convert.ToInt32(row.Cells["cvmensaje"].Value.ToString());
                    idTrab = Convert.ToInt32(row.Cells["NoEmpleado"].Value.ToString());
                }

                fuidMensajes(2, idTrab, cvmensaje, txtmensajeiu.Text.Trim(), dtpfechainicial.Text.Trim(), dtpfechafin.Text.Trim(), sUsuuMod, Name);
                GuardaMensajeReloj();
                dgvMensajes.DataSource = null;
                dgvMensajes.Columns.RemoveAt(0);
                panelTag.Visible = true;
                txtmensajeiu.Text = "";

                cbEmpleados.Text = "";
                txtmensajeiu.Focus();

                gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlmensajes.Visible = false;
            }
            else if (pactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //Eliminar
                    int cvmensaje = 0;
                    if (dgvMensajes.SelectedRows.Count != 0)
                    {
                        DataGridViewRow row = this.dgvMensajes.SelectedRows[0];
                        cvmensaje = Convert.ToInt32(row.Cells["cvmensaje"].Value.ToString());
                    }
                    fuidMensajes(3, 0, cvmensaje, "%", "%", "%", sUsuuMod, Name);
                    GuardaMensajeReloj();
                    dgvMensajes.DataSource = null;
                    dgvMensajes.Columns.RemoveAt(0);
                    panelTag.Visible = true;
                    txtmensajeiu.Text = "";
                    //txtidtrab.Text = "";
                    cbEmpleados.Text = "";
                    txtmensajeiu.Focus();
                    //llena grid con datos existente
                    gridMensajes(4, 0, 0, txtMensaje.Text.Trim(), "", "", "", "");
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlmensajes.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }
            this.btnAgregar.Image = global::SIPAA_CS.Properties.Resources.Agregar;
        }
        //boton minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro, que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }
        //boton regresar
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void Mensajes_Load(object sender, EventArgs e)
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
            Utilerias.cargaimagen(ptbimgusuario);

            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            ftooltip();
            pnldatos.Visible = false;

            pins = 1;
            pact = 1;
            pelim = 1;

            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }

            gridMensajes(4, 0, 0, "", "", "", "", "");



            //Combo Empleados
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            cbEmpleados.Focus();
        }

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lbluid.Text = "     Elimina Mensaje";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lbluid.Text = "     Modifica Mensaje";
                pactbtn = 2;
            }
        }

        //Selección del combo
        private void cbTrabajador_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txtidtrab.Text = cbTrabajador.SelectedValue.ToString();
            cbEmpleados.Text = cbTrabajador.SelectedValue.ToString();
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
            toolTip1.SetToolTip(this.btnCerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Buscar Registros");
            toolTip1.SetToolTip(this.btnInsertar, "Insertar Registro");
        }

        private void gridMensajes(int popc, int pidtrab, int pcvmensaje, string pdescripcion, string pfeinicio, string pfefin, string pusuumod, string pprgumod)
        {
            if (pins == 1 && pact == 1 && pelim == 1)
            {
                DataTable dtmensajes = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtmensajes;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);

                dgvMensajes.Columns[0].HeaderText = "Selección";

                /* dgvMensajes.Columns[0].Width = 80;
                 dgvMensajes.Columns[1].Width = 80;
                 dgvMensajes.Columns[2].Width = 80;
                 //dgvMensajes.Columns[3].Width = 85;
                 dgvMensajes.Columns[4].Width = 185;
                 dgvMensajes.Columns[3].Visible = false;
                 // dgvMensajes.Columns[5].Width = 300;


                 /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 100;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                dgvMensajes.Columns[3].Visible = false;
                dgvMensajes.Columns[6].Visible = false;
                for (int i = 0; i < dgvMensajes.Columns.Count; i++)
                    dgvMensajes.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvMensajes.ClearSelection();
            }
            else if (pins == 1 && pact == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pins == 1 && pelim == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pact == 1 && pelim == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pins == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                dgvMensajes.Columns[0].Visible = false;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;

                dgvMensajes.ClearSelection();
            }
            else if (pact == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else if (pelim == 1)
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvMensajes.Columns.Insert(0, imgCheckUsuarios);
                dgvMensajes.Columns[0].HeaderText = "Selección";

                dgvMensajes.Columns[0].Width = 65;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
            else
            {
                DataTable dtDiaFestivo = pantallaMensajes.ObtenerMensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                dgvMensajes.DataSource = dtDiaFestivo;

                dgvMensajes.Columns[0].Visible = false;
                dgvMensajes.Columns[1].Width = 80;
                dgvMensajes.Columns[2].Width = 80;
                dgvMensajes.Columns[3].Width = 80;
                dgvMensajes.Columns[4].Width = 300;
                dgvMensajes.ClearSelection();
            }
        }

        private void fuidMensajes(int popc, int pidtrab, int pcvmensaje, string pdescripcion, string pfeinicio, string pfefin, string pusuumod, string pprgumod)
        {
            //agrega registro
            if (pactbtn == 1)
            {


                p_rep = pantallaMensajes.fudimensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                txtmensajeiu.Text = "";
            }
            //actualiza registro
            else if (pactbtn == 2)
            {
                p_rep = pantallaMensajes.fudimensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                txtmensajeiu.Text = "";
            }
            //elimina registro
            else if (pactbtn == 3)
            {
                p_rep = pantallaMensajes.fudimensajes(popc, pidtrab, pcvmensaje, pdescripcion, pfeinicio, pfefin, pusuumod, pprgumod);
                txtmensajeiu.Text = "";
                int a = 0;
            } // 

            switch (p_rep.ToString())
            {
                case "1":
                    lblMensaje.Text = "Registro agregado correctamente";
                    break;
                case "2":
                    lblMensaje.Text = "Registro modificado correctamente";
                    break;
                case "3":
                    lblMensaje.Text = "Registro eliminado correctamente";
                    break;
                case "99":
                    lblMensaje.Text = "Registro ya existe";
                    break;
                default:
                    lblMensaje.Text = "";
                    break;
            }
        } //


        private void factgrid()
        {
            for (int iContador = 0; iContador < dgvMensajes.Rows.Count; iContador++)
            {
                dgvMensajes.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvMensajes.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dgvMensajes.SelectedRows[0];


                pnlmensajes.Visible = true;
                lbluid.Text = "     Modifica Mensaje";
                if (row.Cells["NoEmpleado"].Value.ToString() != "")
                    rbPersonal.Checked = true;
                else
                    rbPersonal.Checked = false;
                dtpfechainicial.Text = row.Cells["FechaInicial"].Value.ToString();
                dtpfechafin.Text = row.Cells["FechaFin"].Value.ToString();

                cbEmpleados.Text = row.Cells["Nombre"].Value.ToString();
                cbEmpleados.SelectedItem = row.Cells["NoEmpleado"].Value.ToString();
                txtmensajeiu.Text = row.Cells["Mensaje"].Value.ToString();

                pnldatos.Visible = true;
                cbEmpleados.Enabled = false;

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }

        private void dtpfechainicial_ValueChanged(object sender, EventArgs e)
        {

            //DateTime Date = DateTime.Today.Date;
            //if (dtpfechainicial.Value.Date < Date)
            //{
            //    MessageBox.Show("No puede elegir una fecha anterior a la actual", "SIPPA", MessageBoxButtons.OK);
            //    dtpfechainicial.Value = DateTime.Today.Date;
            //}
            //dtpfechafin.Value = dtpfechainicial.Value;
        }

        private void rbPublico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPublico.Checked == true)
            {
                tag = 253;
                chkCaduca.Checked = false;
                chkCaduca.Visible = true;
                //dgvNombre.DataSource = new DataSet();
                //txtidtrab.Text = "";
                //txtidtrab.Enabled = false;
                cbEmpleados.Text = "";
                cbEmpleados.Enabled = false;
                txtmensajeiu.Text = "";
            }
            else
                chkCaduca.Visible = false;



            // 253 es para mensajes publicos , 254 para personales y 255 para borradores, pero esos no los recomiendo que los usen pues no se pueden
            // asignar
        }

        private void rbPersonal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPersonal.Checked == true)
            {
                tag = 254;
                chkCaduca.Checked = false;
                //txtidtrab.Enabled = true;
                cbEmpleados.Enabled = true;
                txtmensajeiu.Text = "";
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCaduca.Checked == true)
                dtpfechafin.Enabled = dtpfechainicial.Enabled = false;
            else
                dtpfechafin.Enabled = dtpfechainicial.Enabled = true;


        }

        private void dtpfechafin_Leave(object sender, EventArgs e)
        {
            if (dtpfechafin.Value.Date < dtpfechainicial.Value.Date)
            {
                MessageBox.Show("No puede elegir una fecha final menor a la de inicio, se pondra la misma fecha de inicio", "SIPPA", MessageBoxButtons.OK);
                dtpfechafin.Value = dtpfechainicial.Value.Date;
            }

        }

        private void dtpfechainicial_Validating(object sender, CancelEventArgs e)
        {

        }

        private void dtpfechainicial_Validated(object sender, EventArgs e)
        {
        }

        private void dtpfechafin_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpfechafin.Value.Date < dtpfechainicial.Value.Date)
            //{
            //    MessageBox.Show("No puede elegir una fecha final menor a la de inicio, se pondra la misma fecha de inicio", "SIPPA", MessageBoxButtons.OK);
            //    dtpfechafin.Value = dtpfechainicial.Value.Date;
            //}
        }

        private void txtidtrab_TextChanged(object sender, EventArgs e)
        {/*
            if (txtidtrab.Text!=string.Empty)
            {
                SonaTrabajador objTrab = new SonaTrabajador();
                DataTable dtTrab = objTrab.ObtenerPerfilTrabajador(txtidtrab.Text, 14, "1", "1", 0, LoginInfo.IdTrab, this.Name);
                //llenarGrid(dtTrab, dgvNombre);
                if (dgvNombre.Columns.Count > 0)
                     dgvNombre.Columns.RemoveAt(0);
                
                dgvNombre.DataSource = dtTrab;
                //Utilerias.AgregarCheck(dgv, 0);

            }*/
        }

        private void dtpfechainicial_Leave(object sender, EventArgs e)
        {

            DateTime Date = DateTime.Today.Date;
            if (dtpfechainicial.Value.Date < Date)
            {
                MessageBox.Show("No puede elegir una fecha anterior a la actual", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Value = DateTime.Today.Date;
            }
            dtpfechafin.Value = dtpfechainicial.Value;
        }
    }
}
