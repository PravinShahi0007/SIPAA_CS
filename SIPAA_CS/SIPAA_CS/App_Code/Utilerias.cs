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

namespace SIPAA_CS.App_Code
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
                    if (iContador < Ultimoboton)
                    {
                        int ibtnContador = iContador + 1;
                        while (PanelMetro.Controls[ibtnContador].Visible != true && ibtnContador < Ultimoboton)
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

        public static void ResizeForm(Form frm, Size dsize)
        {
            int widthActual = frm.Size.Width;
            int heightActual = frm.Size.Height;

            int widthSys = dsize.Width;
            int heightSys = dsize.Height;

            int heightNueva;
            int widthNueva;

            if (heightSys <= 768 || widthSys <= 768) {
                heightNueva = 600;
            } else {
                heightNueva = 768;
            }

           

            if (heightNueva <= 600)
            {
                widthNueva = 800;
                double dPorcentaje = 100-(((double)heightNueva / (double)heightActual) * 100);
                frm.Size = new Size(widthNueva, heightNueva);
                frm.BackgroundImageLayout = ImageLayout.Zoom;
                foreach (Control ctrl in frm.Controls)
                {
                    ResizeControl(ctrl, (dPorcentaje/100));
                }


                //double locationx = frm.Location.X
                frm.StartPosition = FormStartPosition.CenterScreen; 
                frm.DesktopLocation = new Point(frm.Location.X, 50);
            }
            else if (heightSys <= 600 || widthSys <= 600)
            {
                frm.Size = new Size(640, 480);
                frm.AutoScroll = true;
            }
            else
            {
                widthNueva = 1024;
                frm.Size = new Size(widthNueva, heightNueva);

            } 





        }


        public static void ResizeControl(Control ctrl, double Per) {

            int cposx;
            int cposy;
            double dcposx;
            double dcposy;
            int ctrlW;
            int ctrlH;
            double nCtrlH;
            double nCtrlW;
            string TipoControl = ctrl.AccessibilityObject.ToString();


            if (ctrl.Controls.Count != 0) {

                foreach (Control ctrlHijo in ctrl.Controls) {

                    ResizeControl(ctrlHijo, Per);
                }     
           }


            float fsize = ctrl.Font.Size;
            double dsize = fsize - (fsize * Per);
            ctrl.Font = new Font(ctrl.Font.FontFamily, (float)dsize, ctrl.Font.Style);

            cposx = ctrl.Location.X;
            cposy = ctrl.Location.Y;
            dcposx = cposx - (cposx * Per);
            dcposy = cposy - (cposy * Per);
            ctrl.Location = new Point((int)dcposx, (int)dcposy);

            ctrlH = ctrl.Size.Height;
            ctrlW = ctrl.Size.Width;
            nCtrlH = ctrlH - (ctrlH * Per);
            nCtrlW = ctrlW - (ctrlW * Per);
            ctrl.Size = new Size((int)nCtrlW, (int)nCtrlH);

            if (TipoControl.Contains("Button") && Per != 0 ) {

               // ctrl.BackgroundImageLayout = ImageLayout.Center;
                ctrlH = ctrl.Size.Height;
                ctrlW = ctrl.Size.Width;
                nCtrlH = ctrlH - (ctrlH * (Per*.25));
                nCtrlW = ctrlW - (ctrlW * (Per*.25));
                ctrl.Size = new Size((int)nCtrlW, (int)nCtrlH);
            }
         
            

           

        }
        
    }

 
}
