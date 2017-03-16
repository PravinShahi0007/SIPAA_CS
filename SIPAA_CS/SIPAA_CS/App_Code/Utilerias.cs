using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.Recursos_Humanos.App_Code
{
    class Utilerias
    {
        public void DisableBotones(Button btn, int iClase, Boolean Apagar)
        {

            if (Apagar == false)
            {

                switch (iClase)
                {

                    case 1:
                        //Clase Success - Color Verde
                        btn.Enabled = true;
                        btn.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        btn.ForeColor = ColorTranslator.FromHtml("#2e7d32");

                        break;
                    case 2:
                        //Clase Info - Color Azul
                        btn.Enabled = true;
                        btn.BackColor = ColorTranslator.FromHtml("#0277bd");
                        btn.ForeColor = ColorTranslator.FromHtml("#0277bd");

                        break;
                    case 3:
                        //Clase Danger - Color Rojo
                        btn.Enabled = true;
                        btn.BackColor = ColorTranslator.FromHtml("#f44336");
                        btn.ForeColor = ColorTranslator.FromHtml("#f44336");
                        break;
                    default:

                        break;
                }
            }
            else
            {
                btn.Enabled = false;
                btn.BackColor = ColorTranslator.FromHtml("#eeeeee");
                btn.ForeColor = ColorTranslator.FromHtml("#eeeeee");
            }

        }


        public static void CambioBoton(Button btnCentral,Button btnApagado1 ,Button btnApagado2, Button btnEncendido)
        {
            btnEncendido.Location = btnCentral.Location;
             btnCentral.Visible = false;
            btnApagado2.Visible = false;
            btnEncendido.Visible = true;
           

        }

        public static void ControlNotificaciones(Panel panelTag,Label lbMensaje, int iClase,string strMensaje)
        {


                switch (iClase)
                {

                    case 1:
                        //Clase Success - Color Verde
     
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        lbMensaje.BackColor = ColorTranslator.FromHtml("#2e7d32");
                        lbMensaje.Text = strMensaje;

                        break;
                    case 2:
                        //Clase Info - Color Azul
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#0277bd");
                    lbMensaje.BackColor = ColorTranslator.FromHtml("#0277bd");
                    lbMensaje.Text = strMensaje;
                        break;
                    case 3:
                        //Clase Danger - Color Rojo    
                        panelTag.Visible = true;
                        panelTag.BackColor = ColorTranslator.FromHtml("#f44336");
                    lbMensaje.BackColor = ColorTranslator.FromHtml("#f44336");
                    lbMensaje.Text = strMensaje;
                        break;
                    default:

                        break;
                }
            }
           
        public string cifradoMd5(string pass)
        {
            
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            byte[] hash = md5.ComputeHash(inputBytes);
            pass = BitConverter.ToString(hash).Replace("-", "");
            return pass;
        }

        public bool IsNumber(string inputvalue)
        {
            Regex isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(inputvalue);
        }

        public static void DashboardDinamico(Panel PanelMetro, List<string> ltPermisos)
        {
            int Ultimoboton = (PanelMetro.Controls.Count - 1);

            for (int iContador = Ultimoboton; iContador > -1; iContador--)
            {
                Button btn = (Button)PanelMetro.Controls[iContador];
                string str = Convert.ToString(btn.Tag);

                if (!ltPermisos.Contains(str))
                {
                    btn.Visible = false;
                }
                else
                {
                    if (iContador < (PanelMetro.Controls.Count - 1))
                    {
                        int ibtnContador = iContador + 1;
                        while (PanelMetro.Controls[ibtnContador].Visible != true)
                        {
                            
                           Point location = PanelMetro.Controls[ibtnContador].Location;

                            btn.Location = location;

                            ibtnContador = ibtnContador + 1;

                        }

                       

                        if (ibtnContador < (Ultimoboton - 1))
                        {

                            ibtnContador = ibtnContador + 1;

                            while (ibtnContador < (Ultimoboton))
                            {

                                Point location = PanelMetro.Controls[ibtnContador].Location;

                                btn.Location = location;

                                ibtnContador = ibtnContador + 1;

                            }

                            Ultimoboton = ibtnContador;
                        }
                        else {

                            Ultimoboton = ibtnContador - 1;
                        }

                    }
                }

            }

        }


        public static void MenuDinamico(MenuStrip MenuAccesos, List<string> ltPermisos)
        {

            for (int iContador = 0; iContador < MenuAccesos.Items.Count; iContador++)
            {

                ToolStripMenuItem item = (ToolStripMenuItem)MenuAccesos.Items[iContador];

                if (item.DropDownItems.Count > 0)
                {
                    bool bandera = false;

                    foreach (ToolStripDropDownItem dpitem in item.DropDownItems)
                    {
                        if (!ltPermisos.Contains(Convert.ToString(dpitem.Tag)))
                        {
                            dpitem.Visible = false;

                        }
                        else
                        {
                            bandera = true;
                        }
                    }

                    if (bandera != true)
                    {
                        item.Visible = false;
                    }
                }
                else
                {
                    if (!ltPermisos.Contains(Convert.ToString(item.Tag)))
                    {
                        item.Visible = false;
                    }

                }

            }

        }

        public static void CrearListaPermisoxPantalla(DataRow[] row, List<string> ltPermisos)
        { 
            foreach (DataRow rows in row)
            {

                if (Convert.ToInt32(rows["Crear"]) == 1)
                {
                    ltPermisos.Add("Crear");
                }

                if (Convert.ToInt32(rows["Eliminar"]) == 1)
                {
                    ltPermisos.Add("Eliminar");
                }

                if (Convert.ToInt32(rows["Actualizar"]) == 1)
                {
                    ltPermisos.Add("Actualizar");
                }
                if (Convert.ToInt32(rows["Imprimir"]) == 1)
                {
                    ltPermisos.Add("Imprimir");
                }

                if (Convert.ToInt32(rows["Lectura"]) == 1)
                {
                    ltPermisos.Add("Lectura");
                }
            }

        }

        public void ChangeButton(Button btn, int iClase, Boolean Apagar)
        {

            if (Apagar == false)
            {

                switch (iClase)
                {

                    case 1:
                        //Clase Success - Color Verde
                        //btn.Enabled = true;
                        btn.Image = Resources.btnAdd;
                        
                        break;
                    case 2:
                        //Clase Info - Color Azul
                        //btn.Enabled = true;
                        btn.Image = Resources.btnEdit;

                        break;
                    case 3:
                        //Clase Danger - Color Rojo
                        //btn.Enabled = true;
                        btn.Image = Resources.btnRemove2;
                        
                        break;
                    default:

                        break;
                }
            }

        }
    

        public static void ApagarControlxPermiso(Control ctrl, string Permiso, List<string> ltPermisos)
        {

            switch (Permiso)
            {
                case "Crear":
                    if (!ltPermisos.Contains(Permiso))
                    {
                        ctrl.Visible = false;
                    }

                    break;
                case "Actualizar":
                    if (!ltPermisos.Contains(Permiso))
                    {
                        ctrl.Visible = false;
                    }

                    break;

                case "Lectura":
                    if (!ltPermisos.Contains(Permiso))
                    {
                        ctrl.Visible = false;
                    }

                    break;

                case "Eliminar":
                    if (!ltPermisos.Contains(Permiso))
                    {
                       ctrl.Visible = false;
                    }

                    break;

                case "Imprimir":
                    if (!ltPermisos.Contains(Permiso))
                    {
                        ctrl.Visible = false;
                    }

                    break;
            }
        }

        public void cargarcombo(ComboBox nombre, DataTable datoscbo)
        {

            nombre.DataSource = datoscbo;
            nombre.DisplayMember = "Descripción";
            nombre.ValueMember = "Clave";
            nombre.Text = "";
        }

    }

}
