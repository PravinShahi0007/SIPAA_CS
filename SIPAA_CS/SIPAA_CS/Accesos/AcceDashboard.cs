using SIPAA_CS.Accesos;
using SIPAA_CS.Accesos.Catalogos;
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
using static SIPAA_CS.App_Code.Usuario;

using SIPAA_CS.App_Code.Accesos.Catalogos;

namespace SIPAA_CS.Accesos
{

    //***********************************************************************************************
    //Autor: Victor Jesús Iturburu Vergara
    //Fecha creación:13-03-2017       Última Modificacion: 13-03-2017 
    //Descripción: Pantalla de Inicio de SIPAA
    //***********************************************************************************************
    public partial class AcceDashboard : Form
    {

        Usuarioap cusuarioap = new Usuarioap();

        string sultacceso;

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------
        private void tsmiUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios cau = new Usuarios();
            cau.Show();
        }

        private void tsmiPerfiles_Click(object sender, EventArgs e)
        {
            //Perfiles cp = new Perfiles();
            //cp.Show();
        }

        private void tsmiModulos_Click(object sender, EventArgs e)
        {
            Modulos cm = new Modulos();
            cm.Show();
        }

        private void tsmiProcesos_Click(object sender, EventArgs e)
        {
            Procesos cp = new Procesos();
            cp.Show();
        }

        private void msAsignacionPerfil_Click(object sender, EventArgs e)
        {
            //UsuarioPerfil ap = new UsuarioPerfil();
            //ap.Show();
        }

        private void msAsignacionModulo_Click(object sender, EventArgs e)
        {
            //PerfilModulo am = new PerfilModulo();
            //am.Show();
        }

        private void msAsignacionProceso_Click(object sender, EventArgs e)
        {
            //UsuarioProceso aproc = new UsuarioProceso();
            //aproc.Show();
        }
        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------

        public AcceDashboard()
        {
            InitializeComponent();
        }


        private void AccesosDashboard_Load(object sender, EventArgs e)
        {
            //variables datos del usuario
            DataTable datosusuario = cusuarioap.dtdatos(4, LoginInfo.cvusuario, 0, "", "", 0, "", 0, 0, "", "", "", "", "", "", 0, 0, "", "", "", "");
            sultacceso = datosusuario.Rows[0][5].ToString();

            lblacceso.Text = sultacceso;

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////

            Dashboard form = new Dashboard();
            form.Enabled = false;

            lblusuario.Text = LoginInfo.Nombre;

            Usuario objUsuario = new Usuario();
            //string IdTrab = LoginInfo.IdTrab;
            //List<string> ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(IdTrab, 5);

            //Utilerias.MenuDinamico(MenuAccesos, ltModulosxUsuario);

            //carga imagen
            //Util.cargaimagen(pictureBox1);

            //cargaMenu(0, null, MenuAccesos);
            CrearMenu();

            Utilerias.cargaimagen(ptbimgusuario);

            if (LoginInfo.iconexion == 1) { lblconexion.Visible = true; } else { lblconexion.Visible = false; }
        }
        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------



        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void CrearMenu()
        {
            Perfil objPer = new Perfil();
            DataTable dt = objPer.ReportePerfilesModulos("ACCE", "%", LoginInfo.IdTrab, "CS", 0, 0, 0, 0, 0, 13);
            DataTable dtEncabezados = Utilerias.CrearEncabezados(dt);
            Utilerias.ProcesoMenu(dtEncabezados, LoginInfo.IdTrab, "ACCE", null, MenuAccesos, paneltitulo.BackColor);
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que deseas salir?", "SIPAA", MessageBoxButtons.YesNo);

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
            Dashboard d = new Dashboard();
            d.Show();
            this.Close();
        }


        private void cargaMenu(Int32 IdMaster, ToolStripMenuItem MenuPadre, MenuStrip Menu)
        {
            Perfil objPerfil = new Perfil();
            DataTable menudin = new DataTable();
            menudin = objPerfil.dtmenudinamicocs(6, LoginInfo.IdTrab, "ACCE");
            DataView DatosHijos = new DataView(menudin);

            DatosHijos.RowFilter = menudin.Columns["cvindmodulo"].ColumnName + "=" + IdMaster;

            foreach (DataRowView fila in DatosHijos)
            {
                ToolStripMenuItem MenuHijo = new ToolStripMenuItem();
                MenuHijo.Text = fila["descripcion"].ToString();
                MenuHijo.Name = fila["rutaaaceso"].ToString();
                MenuHijo.BackColor = Color.FromArgb(10, 62, 120);
                MenuHijo.ForeColor = Color.White;
                MenuHijo.Font = new Font("Arial", 12);
                MenuHijo.Image = Resources.ic_view_carousel_white_24dp;

                //modifica iconos
                if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Catalogos")
                {
                    MenuHijo.Image = Resources.ic_view_carousel_white_24dp;
                }
                else if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Asignaciones")
                {
                    MenuHijo.Image = Resources.ic_compare_arrows_white_24dp;
                }
                else if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Reportes")
                {
                    MenuHijo.Image = Resources.ic_assignment_white_24dp;
                }
                else if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Procesos")
                {
                    MenuHijo.Image = Resources.ic_assignment_white_24dp;
                }
                else
                {
                    MenuHijo.Image = Resources.ic_view_carousel_white_24dp;
                }


                MenuHijo.Font = new Font(MenuHijo.Font, FontStyle.Regular);

                MenuHijo.Click += new EventHandler(Event);

                if (MenuPadre == null)
                {
                    Menu.Items.Add(MenuHijo);
                }
                else
                {
                    MenuPadre.DropDownItems.Add(MenuHijo);
                }

                cargaMenu(int.Parse(fila["idmodulo"].ToString()), MenuHijo, Menu);
            }
        }

        private void Event(object sender, EventArgs e)
        {
            ToolStripMenuItem ItemClick = (ToolStripMenuItem)sender;
            eclicck(ItemClick.Name);
        }

        private void eclicck(string NombreFormulario)
        {
            Form Frm;
            if (NombreFormulario != "")
            {
                //Frm = (Form)Activator.CreateInstance(null, "SIPAA_CS.RecursosHumanos.Catalogos.PlantillasDetalles").Unwrap();
                //Frm.Show();

                Frm = (Form)Activator.CreateInstance(null, NombreFormulario).Unwrap();
                Frm.Show();
            }
        }
    }
}
