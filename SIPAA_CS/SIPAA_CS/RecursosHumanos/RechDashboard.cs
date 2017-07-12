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

        private void RechDashboard_Load(object sender, EventArgs e)
        {
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;
            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));

            Dashboard form = new Dashboard();
            form.Enabled = false;

            lblusuario.Text = LoginInfo.Nombre;

            Usuario objUsuario = new Usuario();

            // cargaMenu(0, null, MsMenu);

            CrearMenu();
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
            Dashboard dasb = new Dashboard();
            dasb.Show();
            this.Close();
        }

        //menu strip dinamico
        //private void cargaMenu(Int32 IdMaster, ToolStripMenuItem MenuPadre, MenuStrip Menu)
        //{

        //    DataTable menudin = new DataTable();
        //    menudin = Perf.dtmenudinamicocs(6,LoginInfo.IdTrab,"RECH");
        //    DataView DatosHijos = new DataView(menudin);
            
        //    DatosHijos.RowFilter = menudin.Columns["cvindmodulo"].ColumnName + "=" + IdMaster;

        //    foreach (DataRowView fila in DatosHijos)
        //    {
        //        ToolStripMenuItem MenuHijo = new ToolStripMenuItem();
        //        MenuHijo.Text = fila["descripcion"].ToString();
        //        MenuHijo.Name = fila["rutaaaceso"].ToString();
        //        MenuHijo.BackColor = Color.FromArgb(10, 62, 120);
        //        MenuHijo.ForeColor = Color.White;
        //        MenuHijo.Font = new Font("Arial", 12);
        //        MenuHijo.Image = Resources.ic_view_carousel_white_24dp;

        //        //modifica iconos
        //        if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Catalogos")
        //        {
        //            MenuHijo.Image = Resources.ic_view_carousel_white_24dp;
        //        }
        //        else if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Asignaciones")
        //        {
        //            MenuHijo.Image = Resources.ic_compare_arrows_white_24dp;
        //        }
        //        else if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Reportes")
        //        {
        //            MenuHijo.Image = Resources.ic_assignment_white_24dp;
        //        }
        //        else if ((MenuHijo.Text = fila["descripcion"].ToString()) == "Procesos")
        //        {
        //            MenuHijo.Image = Resources.ic_assignment_white_24dp;
        //        }
        //        else
        //        {
        //            MenuHijo.Image = Resources.ic_view_carousel_white_24dp;
        //        }


        //        MenuHijo.Font = new Font(MenuHijo.Font, FontStyle.Regular);

        //        MenuHijo.Click += new EventHandler(Event);

        //        if (MenuPadre == null)
        //        {
        //            Menu.Items.Add(MenuHijo);
        //        }
        //        else
        //        {
        //            MenuPadre.DropDownItems.Add(MenuHijo);
        //        }

        //        cargaMenu(int.Parse(fila["idmodulo"].ToString()), MenuHijo, Menu);
        //    }
        //}


    

        public void CrearMenu()
        {
            Perfil objPer = new Perfil();
            DataTable dt = objPer.ReportePerfilesModulos("RECH", "%", "ADMIN", "CS", 0, 0, 0, 0, 0, 13);
            DataTable dtEncabezados = Utilerias.CrearEncabezados(dt);
            Utilerias.ProcesoMenu(dtEncabezados, "ADMIN", "RECH", null, MsMenu,paneltitulo.BackColor);
        }

     
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
