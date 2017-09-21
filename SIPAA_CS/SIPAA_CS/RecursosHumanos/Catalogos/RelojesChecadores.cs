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
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;

using SIPAA_CS.RecursosHumanos;
using static SIPAA_CS.App_Code.Usuario;
using SIPAA_CS.Accesos;


//***********************************************************************************************
//Autor: Jaime Avendaño Vargas
//Fecha creación: 28-Mar-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Días Festivos
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Catalogos
{
    public partial class RelojesChecadores : Form
    {
        #region Variables

        int pins;
        int pact;
        int pelim;
        int pactbtn;
        int pcvreloj;
        int p_rep;
        string ipc;
        #endregion

        RelojChecador oRelojesChecadores = new RelojChecador();
        Utilerias Util = new Utilerias();
        System.Net.IPAddress ip;

        public RelojesChecadores()
        {
            InitializeComponent();
            lblMensaje.Text = string.Empty;
        }

        private void RelojesChecadores_Load(object sender, EventArgs e)
        {
            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //lblMensaje.Text = string.Empty;

            /* JAV
            Usuario objUsuario = new Usuario();
            string idtrab = LoginInfo.IdTrab;
            ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(idtrab, 4);
            Utilerias.DashboardDinamico(PanelMetro, ltModulosxUsuario);
            //LoginInfo.Nombre = lblusuario.Text;
            string NomUsu = LoginInfo.Nombre;
            lblusuario.Text = NomUsu;
            JAV */
        }


        //***********************************************************************************************
        //Autor: Jaime Avendaño Vargas
        //Fecha creación: 28-Mar-2017       Última Modificacion: dd-mm-aaaa
        //Descripción: Administra Días Festivos
        //***********************************************************************************************

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        private void dgvRelojesChecadores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //cellclick();
        }

        private void cellclick()
        {
            ckbEliminar.Checked = false;

            if (pins == 1 && pact == 1 && pelim == 1)
            {
                factgrid();
                Util.ChangeButton(btnInsertar, 2, false);
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1 && pact == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pins == 1 && pelim == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pact == 1 && pelim == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 2;
            }
            else if (pins == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pact == 1)
            {
                Util.ChangeButton(btnInsertar, 2, false);
                factgrid();
                pactbtn = 2;
            }
            else if (pelim == 1)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                factgrid();
                ckbEliminar.Visible = true;
                pactbtn = 3;
            }
            else
            {

            }
        }
        // private void dgvRelojesChecadores_CellContentClick(object sender, DataGridViewCellEventArgs e)

        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlActRelojesChecadores.Visible = false;
            dgvRelojesChecadores.DataSource = null;
            //llena grid con datos existente
            fgRelojesChecadores(4, 0, txtBuscarRC.Text.Trim(), "", "", 0, LoginInfo.IdTrab, this.Name);
            txtDescripcionRC.Text = "";
            txtDescripcionRC.Focus();
            if (dgvRelojesChecadores.Columns.Count > 5)
            {
                dgvRelojesChecadores.Columns.RemoveAt(0);
            }
        }
        //boton agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ipc = null;
            clearBoxes();
            ckbEliminar.Visible = false;
            pnlActRelojesChecadores.Visible = true;
            lblActRelojesChecadores.Text = "     Agregar Reloj Checador";
            Util.ChangeButton(btnInsertar, 1, false);
            pactbtn = 1;
            TxtiRelojChecador.Text = "";
            txtDescripcionRC.Text = "";
            txtDescripcionRC.Focus();
            TxtcIP.Text = "";
            TxtiStActualiza.Text = "";
        }

        private bool validaIP()
        {
            bool status = false;
            string ipval = TxtcIP.Text.Trim();

            if (System.Net.IPAddress.TryParse(ipval, out ip))
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(12, 0, "", ipval, "", 0, "", "", LoginInfo.IdTrab, LoginInfo.IdTrab);

                if (dtRelojChecador.Rows.Count > 0 && ipval != ipc)
                {
                    MessageBox.Show("La dirección IP ingresada ya se encuentra asignada\nFavor de verificar", "SIPAA");
                }
                else
                {
                    status = true;
                }
            }
            else
            {
                panelTag.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lblMensaje, 3, "La dirección IP no es válida.");
                panelTag.Enabled = false;
                timer1.Start();
            }

            return status;
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if ((txtDescripcionRC.Text.Trim() == string.Empty || TxtcIP.Text.Trim() == string.Empty) && pactbtn != 3)
            {
                panelTag.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lblMensaje, 3, "Capture todos los campos");
                panelTag.Enabled = false;
                timer1.Start();
            }
            else if (pactbtn == 1 && validaIP())//insertar
            {
                //inserta registro nuevo
                fuidRelojesChecadores(1, 9999, txtDescripcionRC.Text, TxtcIP.Text, string.Empty, 0, LoginInfo.IdTrab, this.Name);                
                dgvRelojesChecadores.DataSource = null;
                dgvRelojesChecadores.Columns.RemoveAt(0);
                //panelTag.Visible = true;
                txtDescripcionRC.Text = "";
                txtDescripcionRC.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgRelojesChecadores(4, 0, "", "", "", 0, LoginInfo.IdTrab, this.Name);
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActRelojesChecadores.Visible = false;
            }
            else if (pactbtn == 2 && validaIP())//actualizar
            {
                //inserta registro nuevo
                fuidRelojesChecadores(2, pcvreloj, txtDescripcionRC.Text.Trim(), TxtcIP.Text, string.Empty, 0, LoginInfo.IdTrab, this.Name);                
                dgvRelojesChecadores.DataSource = null;
                dgvRelojesChecadores.Columns.RemoveAt(0);
                //panelTag.Visible = true;
                txtDescripcionRC.Text = "";
                txtDescripcionRC.Focus();
                timer1.Start();
                //llena grid con datos existente
                fgRelojesChecadores(4, 0, "", "", "", 0, LoginInfo.IdTrab, this.Name);
                ckbEliminar.Checked = false;
                ckbEliminar.Visible = false;
                pnlActRelojesChecadores.Visible = false;
            }
            else if (pactbtn == 3)//eliminar
            {
                DialogResult result = MessageBox.Show("Esta acción elimina el registro, ¿Desea Continuar?", "SIPAA", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //inserta registro nuevo
                    fuidRelojesChecadores(3, pcvreloj, txtDescripcionRC.Text.Trim(), TxtcIP.Text, string.Empty, 0, LoginInfo.IdTrab, this.Name);                    
                    dgvRelojesChecadores.DataSource = null;
                    dgvRelojesChecadores.Columns.RemoveAt(0);
                    //panelTag.Visible = true;
                    txtDescripcionRC.Text = "";
                    txtDescripcionRC.Focus();
                    timer1.Start();
                    //llena grid con datos existente
                    fgRelojesChecadores(4, 0, "", "", "", 0, LoginInfo.IdTrab, this.Name);
                    ckbEliminar.Checked = false;
                    ckbEliminar.Visible = false;
                    pnlActRelojesChecadores.Visible = false;
                }
                else if (result == DialogResult.No)
                {

                }
            }

        } // private void btnInsertar_Click(object sender, EventArgs e)

        //Boton minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
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
                // No hace nada, se queda en la pantalla
            }
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void frmRelojesChecadores_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "Companias.cs")
                {
                    f.Hide();
                }
            }

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;

            //LLAMA TOOL TIP
            fTooltip();

            //LLAMA METODO LLENAR GRID
            //SLlenaGrid(1, "");

            pins = 1;
            pact = 1;
            pelim = 1;

            //HABILITA BOTON AGREGAR
            if (pins == 1)
            {
                btnAgregar.Visible = true;
            }
            //
            fgRelojesChecadores(4, 0, "", "", "", 0, LoginInfo.IdTrab, this.Name);
            //txtBuscarDF.Focus();
        } // private void frmRelojesChecadores_Load(object sender, EventArgs e)

        private void ckbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEliminar.Checked == true)
            {
                Util.ChangeButton(btnInsertar, 3, false);
                lblActRelojesChecadores.Text = "     Elimina Reloj Checador";
                pactbtn = 3;
            }
            else
            {
                Util.ChangeButton(btnInsertar, 2, false);
                lblActRelojesChecadores.Text = "     Modifica Reloj Checador";
                pactbtn = 2;
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

        //TOOL TIP PARA OBJETOS
        private void fTooltip()
        {

            //CREA TOOL TIP
            ToolTip toolTip1 = new ToolTip();

            //CONFIGURACION
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            // CONFIGURA EL TEXTO POR OBJETO
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnAgregar, "Agrega Registro");
            toolTip1.SetToolTip(this.btnBuscar, "Busca Registro");
            //            toolTip1.SetToolTip(this.btnGuardar, "Guarda Registro");
            //            toolTip1.SetToolTip(this.btnEditar, "Edita Registro");
            toolTip1.SetToolTip(this.btnInsertar, "Insertar Registro");

        } // private void fTooltip()

        private void fgRelojesChecadores(int p_opcion, int p_cvreloj, string p_descripcion, string p_ip, string p_cvvnc, int p_stactualiza, string p_usuumod, string p_prgumodr)
        {

            if (pins == 1 && pact == 1 && pelim == 1)
            {

                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvRelojesChecadores.Columns.Insert(0, imgCheckUsuarios);
                dgvRelojesChecadores.Columns[0].HeaderText = "Selección";
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 100;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



                dgvRelojesChecadores.ClearSelection();
            }
            else if (pins == 1 && pact == 1)
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvRelojesChecadores.Columns.Insert(0, imgCheckUsuarios);
                dgvRelojesChecadores.Columns[0].HeaderText = "Selección";
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();

            }
            else if (pins == 1 && pelim == 1)
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvRelojesChecadores.Columns.Insert(0, imgCheckUsuarios);
                dgvRelojesChecadores.Columns[0].HeaderText = "Selección";
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();
            }
            else if (pact == 1 && pelim == 1)
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvRelojesChecadores.Columns.Insert(0, imgCheckUsuarios);
                dgvRelojesChecadores.Columns[0].HeaderText = "Selección";
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();
            }
            else if (pins == 1)
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                dgvRelojesChecadores.Columns[0].Visible = false;
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();
            }
            else if (pact == 1)
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvRelojesChecadores.Columns.Insert(0, imgCheckUsuarios);
                dgvRelojesChecadores.Columns[0].HeaderText = "Selección";
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();
            }
            else if (pelim == 1)
            {
                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
                imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
                imgCheckUsuarios.Name = "img";
                dgvRelojesChecadores.Columns.Insert(0, imgCheckUsuarios);
                dgvRelojesChecadores.Columns[0].HeaderText = "Selección";
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();
            }
            else
            {

                DataTable dtRelojChecador = oRelojesChecadores.obtrelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, LoginInfo.IdTrab, LoginInfo.IdTrab);
                dgvRelojesChecadores.DataSource = dtRelojChecador;

                dgvRelojesChecadores.Columns[0].Visible = false;
                /*
                dgvRelojesChecadores.Columns[0].Width = 75;
                dgvRelojesChecadores.Columns[1].Width = 50;
                dgvRelojesChecadores.Columns[2].Width = 200;
                dgvRelojesChecadores.Columns[3].Width = 120;
                dgvRelojesChecadores.Columns[4].Width = 80;
                dgvRelojesChecadores.Columns[5].Width = 70;*/

                /*Se ajusta tamaño de columna de acuerdo al contenido*/
                for (int i = 0; i < dgvRelojesChecadores.Columns.Count; i++)
                    dgvRelojesChecadores.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgvRelojesChecadores.ClearSelection();
            }
        }

        private void fuidRelojesChecadores(int p_opcion, int p_cvreloj, string p_descripcion, string p_ip, string p_cvvnc, int p_stactualiza, string p_usuumod, string p_prgumodr)
        {
            bool teclado = ckbxTeclado.Checked;
            bool huella = ckbxHuella.Checked;
            bool mhuella = ckbxMultHuella.Checked;
            bool rostro = ckbxRostro.Checked;
            
            //agrega registro
            if (pactbtn == 1)
            {
                p_rep = oRelojesChecadores.udirelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, teclado, huella, mhuella, rostro);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionRC.Text = "";
            }
            //actualiza registro
            else if (pactbtn == 2)
            {
                p_rep = oRelojesChecadores.udirelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, teclado, huella, mhuella, rostro);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionRC.Text = "";
            }
            //elimina registro
            else if (pactbtn == 3)
            {
                p_rep = oRelojesChecadores.udirelojeschecadores(p_opcion, p_cvreloj, p_descripcion, p_ip, p_cvvnc, p_stactualiza, p_usuumod, p_prgumodr, teclado, huella, mhuella, rostro);
                //lbMensaje.Text = p_rep.ToString();
                txtDescripcionRC.Text = "";
            } // 

            switch (p_rep.ToString())
            {
                case "1":
                    panelTag.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lblMensaje, 1, "Registro agregado correctamente");
                    panelTag.Enabled = false;
                    break;
                case "2":
                    panelTag.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lblMensaje, 1, "Registro modificado correctamente");
                    panelTag.Enabled = false;
                    break;
                case "3":
                    panelTag.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lblMensaje, 1, "Registro eliminado correctamente");
                    panelTag.Enabled = false;
                    break;
                case "99":
                    panelTag.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lblMensaje, 3, "Registro ya existe");
                    panelTag.Enabled = false;
                    break;
                default:
                    lblMensaje.Text = "";
                    break;
            } // switch (p_rep.ToString())
        } // 

        private void clearBoxes()
        {
            setValuesCheckBox(
                new CheckBox[]{ ckbxTeclado, ckbxHuella, ckbxMultHuella, ckbxRostro },
                new bool[]{ false, false, false, false } );
        }

        private void factgrid()
        {
            for (int iContador = 0; iContador < dgvRelojesChecadores.Rows.Count; iContador++)
            {
                dgvRelojesChecadores.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            if (dgvRelojesChecadores.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvRelojesChecadores.SelectedRows[0];

                pcvreloj = int.Parse(row.Cells["Clave"].Value.ToString());
                string ValorRow = row.Cells["Descripción"].Value.ToString();
                string ValorIp = ipc = row.Cells["IP"].Value.ToString();

                clearBoxes();

                setValuesCheckBox(
                    new CheckBox[] { ckbxTeclado, ckbxHuella, ckbxMultHuella, ckbxRostro },
                    new bool[] {
                        (bool) row.Cells["teclado"].Value,
                        (bool) row.Cells["huella"].Value,
                        (bool) row.Cells["multiplehuella"].Value,
                        (bool) row.Cells["rostro"].Value } );

                string ValorCvvnc = string.Empty;
                int ValorStactualiza = 0;
                pnlActRelojesChecadores.Visible = true;
                lblActRelojesChecadores.Text = "     Modifica Reloj Checador";
                TxtiRelojChecador.Text = pcvreloj.ToString();
                TxtiRelojChecador.Focus();
                txtDescripcionRC.Text = ValorRow;
                TxtcIP.Text = ValorIp;
                TxtiStActualiza.Text = ValorStactualiza.ToString();
                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
            }
        }

        private void setValuesCheckBox(CheckBox[] ckboxes, bool[] values)
        {
            if(ckboxes.Length == values.Length)
                for (int i = 0; i < ckboxes.Length; i++)
                    ckboxes[i].Checked = values[i];
        }

        private void pnlActRelojesChecadores_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtiStActualiza_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void dgvRelojesChecadores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cellclick();
        }

        private void lblMensaje_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    } // public partial class frmRelojesChecadores : Form
} // namespace SIPAA_CS.Recursos_Humanos.Administracion
