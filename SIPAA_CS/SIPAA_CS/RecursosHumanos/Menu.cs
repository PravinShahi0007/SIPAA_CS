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
using SIPAA_CS.RecursosHumanos.Catalogos;

namespace SIPAA_CS.RecursosHumanos
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        //MenuPba MenuP = new MenuPba();
        

        private void Menu_Load(object sender, EventArgs e)
        {
            cargaMenu(0, null,menuStrip1);
        }

        private void cargaMenu(Int32 IdMaster, ToolStripMenuItem MenuPadre, MenuStrip Menu)
        {
            DataTable Datos = new DataTable();
            Datos = MenuP.dtinf(1);

            DataView DatosHijos = new DataView(Datos);

            DatosHijos.RowFilter = Datos.Columns["idpadre"].ColumnName + "=" + IdMaster;

            foreach (DataRowView fila in DatosHijos)
            {
                ToolStripMenuItem MenuHijo = new ToolStripMenuItem();
                MenuHijo.Text = fila["Descripcion"].ToString();
                MenuHijo.Name = fila["cv"].ToString();
                MenuHijo.BackColor = Color.Blue;
                MenuHijo.ForeColor = Color.White;
                MenuHijo.Image = Resources.ic_view_carousel_white_24dp;
                MenuHijo.Font = new Font(MenuHijo.Font, FontStyle.Regular);

                MenuHijo.Click += new EventHandler(Event);

                if (MenuPadre ==null)
                {
                    Menu.Items.Add(MenuHijo);
                }
                else
                {
                    MenuPadre.DropDownItems.Add(MenuHijo);
                }

                cargaMenu(int.Parse(fila["id"].ToString()),MenuHijo,Menu);
            }
        }

        private void Event(object sender, EventArgs e)
        {
            ToolStripMenuItem ItemClick = (ToolStripMenuItem)sender;

            tu(ItemClick.Name);

        }

        private void tu(string NombreFormulario)
        {

            Form Frm;

            if (NombreFormulario != "0")
            {

                Frm = (Form)Activator.CreateInstance(null, "SIPAA_CS.RecursosHumanos.PlantillasDetalles").Unwrap();

                Frm.Show();
            }

            





        }


    }
}
