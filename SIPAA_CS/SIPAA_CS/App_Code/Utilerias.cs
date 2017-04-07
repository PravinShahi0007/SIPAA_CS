using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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
        public int p_inicbo;

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


        public static void CambioBoton(Button btnCentral, Button btnApagado1, Button btnApagado2, Button btnEncendido)
        {
            btnEncendido.Location = btnCentral.Location;
            btnCentral.Visible = false;
            btnApagado2.Visible = false;
            btnEncendido.Visible = true;


        }

        public static void ControlNotificaciones(Panel panelTag, Label lbMensaje, int iClase, string strMensaje)
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

        public static List<string>  CrearListaPermisoxPantalla(DataRow[] row, List<string> ltPermisos)
        {
            List<string> listPermisos = new List<string>();

            foreach (DataRow rows in row)
            {

                if (Convert.ToInt32(rows["Crear"]) == 1)
                {
                    listPermisos.Add("Crear");
                }

                if (Convert.ToInt32(rows["Eliminar"]) == 1)
                {
                    listPermisos.Add("Eliminar");
                }

                if (Convert.ToInt32(rows["Actualizar"]) == 1)
                {
                    listPermisos.Add("Actualizar");
                }
                if (Convert.ToInt32(rows["Imprimir"]) == 1)
                {
                    listPermisos.Add("Imprimir");
                }

                //if (Convert.ToInt32(rows["Lectura"]) == 1)
                //{
                //    listPermisos.Add("Lectura");
                //}
            }

            return listPermisos;
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
                        btn.Image = Resources.Guardar;

                        break;
                    case 2:
                        //Clase Info - Color Azul
                        //btn.Enabled = true;
                        btn.Image = Resources.Editar;

                        break;
                    case 3:
                        //Clase Danger - Color Rojo
                        //btn.Enabled = true;
                        btn.Image = Resources.Borrar;

                        break;
                    default:

                        break;
                }
            }

        }


        public static bool ControlPermiso(string sPermiso, List<string> ltPermisos)
        {
            bool bBandera = false;
            if (ltPermisos.Contains(sPermiso))
            {
                bBandera = true;
            }
            else
            {

                bBandera = false;
            }

            return bBandera;
        }


        public void cargarcombo(ComboBox nombre, DataTable datoscbo)
        {
            p_inicbo = 0;
            nombre.DataSource = datoscbo;
            nombre.DisplayMember = "Descripción";
            nombre.ValueMember = "Clave";
            p_inicbo = 1;
            //nombre.Text = "";
        }

        public static void ResizeForm(Form frm, Size dsize)
        {
            int widthActual = frm.Size.Width;
            int heightActual = frm.Size.Height;

            int widthSys = dsize.Width;
            int heightSys = dsize.Height;

            int heightNueva = 0;
            int widthNueva = 0;

            double dPorcentaje = 0;

            if (heightSys <= 600 || widthSys <= 600)
            {

                widthNueva = 600;
                heightNueva = 480;
                frm.AutoScroll = true;
                frm.BackgroundImageLayout = ImageLayout.Stretch;
                dPorcentaje = (100 - (((double)heightNueva / (double)heightActual) * 100)) * 1.9;

                if (heightSys > 800 || widthSys > 800)
                {
                    frm.DesktopLocation = new Point((int)(frm.Location.X + 200), 50);
                }
                else
                {
                    frm.DesktopLocation = new Point((int)(frm.Location.X + 100), 50);
                }



            }
            else if (heightSys <= 768 || widthSys <= 768)
            {

                widthNueva = 800;
                heightNueva = 600;
                frm.BackgroundImageLayout = ImageLayout.Zoom;
                dPorcentaje = 100 - (((double)heightNueva / (double)heightActual) * 100);
                frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
                frm.DesktopLocation = new Point((int)(frm.Location.X + 150), 80);
            }
            else
            {
                heightNueva = 768;
            }



            if (heightNueva <= 600)
            {


                frm.Size = new Size(widthNueva, heightNueva);

                foreach (Control ctrl in frm.Controls)
                {
                    ResizeControl(ctrl, (dPorcentaje / 100));
                }


                //double locationx = frm.Location.X

            }
            else if (widthSys <= 600)
            {
                frm.Size = new Size(640, 480);
                frm.AutoScroll = true;
            }
            else
            {
                widthNueva = 1024;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.Size = new Size(widthNueva, heightNueva);

            }





        }


        public static void ResizeControl(Control ctrl, double Per)
        {

            int cposx;
            int cposy;
            double dcposx;
            double dcposy;
            int ctrlW;
            int ctrlH;
            double nCtrlH;
            double nCtrlW;
            string TipoControl = ctrl.AccessibilityObject.ToString();
            string strPixeles;

            if (ctrl.Controls.Count != 0)
            {
                foreach (Control ctrlHijo in ctrl.Controls)
                {
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

            if (TipoControl.Contains("System.Windows.Forms.Button") && Per != 0)
            {
                Button btn = (Button)ctrl;

                if (Per > .25)
                {strPixeles = "20x20"; }
                else
                {strPixeles = "30x30"; }

                if (btn.Tag != null)
                {
                    try
                    { btn.Image = (Image)Resources.ResourceManager.GetObject(btn.Tag.ToString() + strPixeles);}
                    catch (Exception ex)
                    {  btn.Image = btn.Image;}
                }
                // ctrl.BackgroundImageLayout = ImageLayout.Center;
                ctrlH = ctrl.Size.Height;
                ctrlW = ctrl.Size.Width;
                nCtrlH = ctrlH - (ctrlH * (Per * .25));
                nCtrlW = ctrlW - (ctrlW * (Per * .25));
                ctrl.Size = new Size((int)nCtrlW, (int)nCtrlH);
            }


        }

        public static void AsignarBotonResize(Button btn, Size size,string Icono)
        {

            if (size.Width <= 600 || size.Height <= 600)
            {  btn.Image = (Image)Resources.ResourceManager.GetObject(Icono + "20x20");}
            else  if (size.Width <= 768 || size.Height <= 768)
            {
                  btn.Image = (Image)Resources.ResourceManager.GetObject(Icono + "30x30");
            }  
            else {
                btn.Image = (Image)Resources.ResourceManager.GetObject(Icono);
                }

        }


        public static ReportDocument ObtenerObjetoReporte(DataTable dtRpt, string strModulo, string NombreReporte)
        {
            ReportDocument ReportDoc = new ReportDocument();
            string path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string file = path + "\\" + strModulo + "\\Reportes\\" + NombreReporte;
            ReportDoc.Load(file);
            ReportDoc.SetDataSource(dtRpt);
            return ReportDoc;
        }

        public static void llenarComboxDataTable(ComboBox cb, DataTable dt,string sClave ,string sDescripcion)
        {
            DataRow row = dt.NewRow();
            row[sClave] = "0";
            row[sDescripcion] = "Seleccionar";
            dt.Rows.InsertAt(row,0);
            cb.DataSource = dt;
            cb.DisplayMember = sDescripcion;
            cb.ValueMember = sClave;
        }

      public enum DiasSemana { Domingo = 1, Lunes = 2, Martes = 3, Miércoles = 4, Jueves = 5, Viernes = 6, Sábado = 7 };

      public static class Botones {
            public static string Guardar = "Guardar";
            public static string Editar = "Editar";
            public static string Borrar = "Borrar";
            public static string Agregar = "Agregar";
            public static string Baja = "Baja";
            public static string Alta = "Alta";
            public static string Imprimir = "Imprimir";
        }

        public static void AgregarCheck(DataGridView dgv, int iPosicion)
        {
            DataGridViewImageColumn imgCheckUsuarios = new DataGridViewImageColumn();
            imgCheckUsuarios.Image = Resources.ic_lens_blue_grey_600_18dp;
            imgCheckUsuarios.Tag = "checkGrid";
            dgv.Columns.Insert(iPosicion, imgCheckUsuarios);
            dgv.Columns[iPosicion].HeaderText = "Seleccionar";
            dgv.Columns[iPosicion].Width = 65;
        }

        public static void MultiSeleccionGridView(DataGridView dgv, int iPositionClave, List<int> ltCv, Control ctrl)
        {
            DataGridViewRow row = dgv.SelectedRows[0];
            int iCv = Convert.ToInt32(row.Cells[iPositionClave].Value.ToString());

            if (ltCv.Contains(iCv))
            {  ltCv.Remove(iCv); }
            else
            { ltCv.Add(iCv);}

            if (ltCv.Count == 0)
            { ctrl.Enabled = false; }
            else
            { ctrl.Enabled = true; }

            switch (row.Cells[0].Tag.ToString())
            {
                case "check":
                    row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                    row.Cells[0].Tag = "uncheck";
                    break;
                case "uncheck":
                    row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                    row.Cells[0].Tag = "check";
                    break;
            }
        }

        public static void ImprimirAsignacionesGrid(DataGridView dgv,int iPosicionCheck,int iPosicionClave,List<string> ltcv)
        {

            for (int iContador = 0; iContador < dgv.Rows.Count; iContador++)
            {
                string sCv = dgv.Rows[iContador].Cells[iPosicionClave].Value.ToString();

                if (ltcv.Contains(sCv))
                {
                    dgv.Rows[iContador].Cells[iPosicionCheck].Value = Resources.ic_check_circle_green_400_18dp;
                    dgv.Rows[iContador].Cells[iPosicionCheck].Tag = "check";
                }
                else
                {
                    dgv.Rows[iContador].Cells[iPosicionCheck].Value = Resources.ic_lens_blue_grey_600_18dp;
                    dgv.Rows[iContador].Cells[iPosicionCheck].Tag = "uncheck";
                }
            }


        }
        public static bool SinAsignaciones(DataGridView dgv, int iPosicionCheck, int iPosicionClave, List<int> ltFormas)
        {
            bool bBandera = false;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                int iCV = Convert.ToInt32(row.Cells[iPosicionClave].Value.ToString());

                if (ltFormas.Contains(iCV))
                {

                    switch (row.Cells[0].Tag.ToString())
                    {
                        case "check":
                            row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                            row.Cells[0].Tag = "uncheck";
                            break;

                        case "uncheck":
                            row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                            row.Cells[0].Tag = "check";
                            break;
                    }
                }

                if (row.Cells[iPosicionCheck].Tag.ToString() != "uncheck")
                {
                    bBandera = true;
                }
            }
            return bBandera;
        }


        public static Size PantallaSistema()
        {
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;

            return new Size(sysW, sysH);
        }

    }



 
}
