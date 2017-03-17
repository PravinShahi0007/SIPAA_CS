﻿using SIPAA_CS.Properties;
using SIPAA_CS.Recursos_Humanos.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Recursos_Humanos.Administracion
{

    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017      Última Modificacion: 13-03-2017
    //Descripción: Pantalla de Asignación de Módulos a Perfiles
    //***********************************************************************************************
    public partial class Asignar_Perfil : Form
    {

        public Point formPosition;
        public Boolean mouseAction;
        public string CVUsuario;
        public int CvPerfil;

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (panelTag.Visible == true)
            {

             //   Thread.Sleep(3000);
                panelTag.Visible = false;
            }

            for (int iContador = 0; iContador < dgvUsuarios.Rows.Count; iContador++)
            {
                dgvUsuarios.Rows[iContador].Cells[3].Value = Resources.ic_lens_blue_grey_600_18dp;
            }


            if (dgvUsuarios.SelectedRows.Count != 0)
            {

                DataGridViewRow row = this.dgvUsuarios.SelectedRows[0];

                CVUsuario = row.Cells["CvUsuario"].Value.ToString();
                int IdTrab = Convert.ToInt32(row.Cells["IdTrab"].Value.ToString());
                string ValorRow = row.Cells["Nombre"].Value.ToString();

                row.Cells[3].Value = Resources.ic_check_circle_green_400_18dp;

                Perfil objPerfil = new Perfil();
                List<int> ltPerfilesxUsuario = objPerfil.ObtenerPerfilesxUsuario(CVUsuario);

                for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
                {
                    int IdPerfil = Convert.ToInt32(dgvPerfiles.Rows[iContador].Cells[1].Value.ToString());

                    if (ltPerfilesxUsuario.Contains(IdPerfil))
                    {
                        dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    }
                    else
                    {
                        dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    }
                }


            }
        }

        private void dgvPerfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CVUsuario != null)
            {

                if (dgvPerfiles.SelectedRows.Count != 0)
                {
                    DataGridViewRow row = this.dgvPerfiles.SelectedRows[0];

                    int iCVPerfil = Convert.ToInt32(row.Cells[1].Value.ToString());
                    string UsuuMod = "vjiturburuv";
                    string PrguMod = "Recursos_Humanos";

                    try
                    {
                        Usuario objUsuario = new Usuario();
                        objUsuario.AsignarPerfilaUsuario(CVUsuario, iCVPerfil, UsuuMod, PrguMod);
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#439047");
                        lbMensaje.Text = "Cambio Hecho Correctamente";

                        dgvUsuarios_CellContentClick(sender, e);


                    }
                    catch (Exception ex)
                    {

                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#ef5350");
                        lbMensaje.Text = "Error de Comunicación con el servidor. Favor de Intentarlo más tarde.";
                        timer.Interval = 2000;
                        timer_Tick(sender, e);
                        timer.Enabled = true;
                        dgvUsuarios_CellContentClick(sender, e);
                    }
                }

            }
            else
            {

                panelTag.Visible = true;
                panelTag.BackColor = ColorTranslator.FromHtml("#29b6f6");
                lbMensaje.Text = "No se ha Seleccionado a un Usuario";
            }
        }


        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que dese salir?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Cancel)
            {

            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            CVUsuario = null;
            dgvUsuarios.Columns.Remove(columnName: "imgUsuarios");

            string strUsuario = "";
            string IdTrab = "";

            if (txtUsuario.Text != String.Empty)
            {

                strUsuario = txtUsuario.Text.Trim();
            }
            else
            {
                strUsuario = "%";
            }


            if (txtIdTrab.Text != String.Empty)
            {

                IdTrab = txtIdTrab.Text;
            }
            else
            {
                IdTrab = "%";
            }


            Usuario objUsuario = new Usuario();

            List<Usuario> ltUsuario = objUsuario.ObtenerUsuariosxBusqueda(strUsuario, IdTrab);
            DataTable dtUsuario = objUsuario.ObtenerDataTableUsuarios(ltUsuario);

            dgvUsuarios.DataSource = dtUsuario;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "imgUsuarios";
            dgvUsuarios.Columns.Insert(3, imgCheckUsuarios);
            dgvUsuarios.Columns[3].HeaderText = "";
            for (int iContador = 0; iContador < dgvPerfiles.Rows.Count; iContador++)
            {
                dgvPerfiles.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
            }



        }

        private void btnBuscarPerfil_Click(object sender, EventArgs e)
        {

            CVUsuario = null;
            dgvPerfiles.Columns.Remove(columnName: "imgPerfiles");
            panelTag.Visible = false;
            string strPerfil = "";

            if (txtPerfil.Text != String.Empty)
            {

                strPerfil = txtPerfil.Text.Trim();
            }
            else
            {
                strPerfil = "%";
            }
            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", strPerfil, "1");

            dgvPerfiles.DataSource = dtPerfiles;

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "imgPerfiles";
            dgvPerfiles.Columns.Insert(0, imgCheckPerfiles);
            dgvPerfiles.Columns[0].HeaderText = "";

            for (int iContador = 0; iContador < dgvUsuarios.Rows.Count; iContador++)
            {
                dgvUsuarios.Rows[iContador].Cells[3].Value = Resources.ic_lens_blue_grey_600_18dp;
            }

            // dgvUsuarios_CellContentClick(sender, e);
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public Asignar_Perfil()
        {
            InitializeComponent();
        }

        private void Asignar_Perfil_Load(object sender, EventArgs e)
        {
            Usuario objUsuario = new App_Code.Usuario();
            List<Usuario> ltUsuario = objUsuario.ObtenerUsuariosxBusqueda("%", "%");
            DataTable dtUsuarios = objUsuario.ObtenerDataTableUsuarios(ltUsuario);
            dgvUsuarios.DataSource = dtUsuarios;


            Perfil objPerfil = new Perfil();
            DataTable dtPerfiles = objPerfil.ObtenerPerfilesxBusqueda("%", "%", "1");
            dgvPerfiles.DataSource = dtPerfiles;

            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Name = "imgUsuarios";
            dgvUsuarios.Columns.Insert(3, imgCheckUsuarios);
            dgvUsuarios.Columns[3].HeaderText = "";

            DataGridViewImageColumn imgCheckPerfiles = new DataGridViewImageColumn();
            imgCheckPerfiles.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckPerfiles.Name = "imgPerfiles";
            dgvPerfiles.Columns.Insert(0, imgCheckPerfiles);
            dgvPerfiles.Columns[0].HeaderText = "";
            ImageList imglt = new ImageList();




            dgvPerfiles.Columns[1].Visible = false;
            dgvPerfiles.Columns[3].Visible = false;
            dgvPerfiles.Columns[4].Visible = false;
            dgvPerfiles.Columns[5].Visible = false;
            dgvPerfiles.Columns[6].Visible = false;

            dgvUsuarios.Columns[1].ReadOnly = true;
            dgvUsuarios.Columns[0].ReadOnly = true;
            dgvUsuarios.Columns[0].Visible = false;

            dgvPerfiles.ClearSelection();
            dgvUsuarios.ClearSelection();

        }


        private void timer_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;


        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------



        private void BarraSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            mouseAction = false;
        }

        private void BarraSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            formPosition = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            mouseAction = true;
        }

        private void BarraSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseAction == true)
            {
                Location = new Point(Cursor.Position.X - formPosition.X, Cursor.Position.Y - formPosition.Y);
            }
        }

   
    

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}