using SIPAA_CS.App_Code;
using SIPAA_CS.RecursosHumanos.Asignaciones;
using SIPAA_CS.RecursosHumanos.Catalogos;
using SIPAA_CS.RecursosHumanos.Procesos;
using SIPAA_CS.RecursosHumanos.Reportes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;




using System.Data;
using SIPAA_CS.Properties;
using SIPAA_CS.RecursosHumanos.Catalogos;

namespace SIPAA_CS.RecursosHumanos
{
    public partial class RechDashboard : Form
    {
        public RechDashboard()
        {
            InitializeComponent();
        }

        Perfil Perf = new Perfil();
        Usuario usuario = new Usuario();
        Utilerias Util = new Utilerias();
        MenuPba MenuP = new MenuPba();

        private void RechDashboard_Load(object sender, EventArgs e)
        {
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            Dashboard form = new Dashboard();
            form.Enabled = false;

            lblusuario.Text = LoginInfo.Nombre;

            Usuario objUsuario = new Usuario();
            //string IdTrab = LoginInfo.IdTrab;
            //List<string> ltModulosxUsuario = objUsuario.ObtenerListaModulosxUsuario(IdTrab, 5);

            //Utilerias.MenuDinamico(MenuAccesos, ltModulosxUsuario);

            //carga imagen
            //Util.cargaimagen(pictureBox1);

            cargaMenu(0, null, mstrechum);


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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //menu strip dinamico
        private void cargaMenu(Int32 IdMaster, ToolStripMenuItem MenuPadre, MenuStrip Menu)
        {

            DataTable menudin = new DataTable();
            menudin = Perf.dtmenudinamicocs(6,"ADMIN","RECH");
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
