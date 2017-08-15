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
using static SIPAA_CS.App_Code.Usuario;

using SIPAA_CS.RecursosHumanos;

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
            //panelTag.Dispose();
            panelTag.Visible = false;
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

                if (!ltPermisos.Contains(str.Trim()))
                {
                    btn.Visible = false;
                }
                else
                {
                    if (iContador < Ultimoboton)
                    {
                        int ibtnContador = iContador + 1;
                        while (ibtnContador <= Ultimoboton && PanelMetro.Controls[ibtnContador].Visible != true)
                        {

                            Point location = PanelMetro.Controls[ibtnContador].Location;

                            btn.Location = location;

                            ibtnContador = ibtnContador + 1;


                        }



                        if (ibtnContador <= (Ultimoboton - 1))
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
                        else
                        {

                            Ultimoboton = ibtnContador - 1;
                        }

                    }
                }

            }

        }

        public static Bitmap IconosMenu(string Tipo) {

            Bitmap btImage;

            switch (Tipo) {

                case "Catalogos":
                    btImage = Resources.ic_view_carousel_white_24dp;
                    break;

                case "Procesos": btImage = Resources.ic_settings_white_24dp;
                    break;

                case "Reportes": btImage = Resources.ic_assignment_white_24dp;
                    break;

                case "Asignaciones": btImage = Resources.ic_compare_arrows_white_24dp;
                    break;

                default: btImage = Resources.ic_folder_white_24dp;
                    break;
            }

            return btImage;

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

        public static Dictionary<string, int> CrearListaPermisoxPantalla(DataTable dtPermisos)
        {
            Dictionary<string, int> dcPermisos = new Dictionary<string, int>();

            foreach (DataRow rows in dtPermisos.Rows)
            {

                if (Convert.ToInt32(rows["Crear"]) == 1)
                {
                    dcPermisos.Add("Crear", 1);
                } else
                {
                    dcPermisos.Add("Crear", 0);
                }

                if (Convert.ToInt32(rows["Eliminar"]) == 1)
                {
                    dcPermisos.Add("Eliminar", 1);
                }
                else
                {
                    dcPermisos.Add("Eliminar", 0);
                }

                if (Convert.ToInt32(rows["Actualizar"]) == 1)
                {
                    dcPermisos.Add("Actualizar", 1);
                }
                else
                {
                    dcPermisos.Add("Actualizar", 0);
                }
                if (Convert.ToInt32(rows["Imprimir"]) == 1)
                {
                    dcPermisos.Add("Imprimir", 1);
                }
                else
                {
                    dcPermisos.Add("Imprimir", 0);
                }

                //if (Convert.ToInt32(rows["Lectura"]) == 1)
                //{
                //    listPermisos.Add("Lectura");
                //}
            }

            return dcPermisos;
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

        //carga imagen
        public void cargaimagen(PictureBox pbusuario)
        {
            try
            {
                string PBA = LoginInfo.IdTrab;

                pbusuario.Image = Image.FromFile(@"\\192.168.30.238\Sistemasjs\Noe Alvarez\" + LoginInfo.IdTrab + ".jpg", true);
                pbusuario.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception)
            {
                pbusuario.Image = Image.FromFile(@"\\192.168.30.238\Sistemasjs\Noe Alvarez\USER1.jpg", true);
                pbusuario.SizeMode = PictureBoxSizeMode.StretchImage;
            }
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
                { strPixeles = "20x20"; }
                else
                { strPixeles = "30x30"; }

                if (btn.Tag != null)
                {
                    try
                    { btn.Image = (Image)Resources.ResourceManager.GetObject(btn.Tag.ToString() + strPixeles); }
                    catch (Exception ex)
                    { btn.Image = btn.Image; }
                }
                // ctrl.BackgroundImageLayout = ImageLayout.Center;
                ctrlH = ctrl.Size.Height;
                ctrlW = ctrl.Size.Width;
                nCtrlH = ctrlH - (ctrlH * (Per * .25));
                nCtrlW = ctrlW - (ctrlW * (Per * .25));
                ctrl.Size = new Size((int)nCtrlW, (int)nCtrlH);
            }


        }

        public static void AsignarBotonResize(Button btn, Size size, string Icono)
        {

            if (size.Width <= 600 || size.Height <= 600)
            { btn.Image = (Image)Resources.ResourceManager.GetObject(Icono + "20x20"); }
            else if (size.Width <= 768 || size.Height <= 768)
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

        public static void llenarComboxDataTable(ComboBox cb, DataTable dt, string sClave, string sDescripcion)
        {
            DataRow row = dt.NewRow();
            row[sClave] = "0";
            row[sDescripcion] = "Seleccionar";
            dt.Rows.InsertAt(row, 0);
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
            dgv.Columns[iPosicion].Width = 100;
        }

        public static void MultiSeleccionGridView(DataGridView dgv, int iPositionClave, List<int> ltCv, Control ctrl)
        {
            DataGridViewRow row = dgv.SelectedRows[0];
            int iCv = Convert.ToInt32(row.Cells[iPositionClave].Value.ToString());

            if (ltCv.Contains(iCv))
            { ltCv.Remove(iCv); }
            else
            { ltCv.Add(iCv); }

            if (ltCv.Count == 0)
            { ctrl.Enabled = false; }
            else
            { ctrl.Enabled = true; }

            try
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
            catch (Exception)
            {

            }

        }


        public static void SeleccionGridView(DataGridView dgv, int iPositionClave, List<int> ltCv, Control ctrl)
        {
            DataGridViewRow row = dgv.SelectedRows[0];
            int iCv = Convert.ToInt32(row.Cells[iPositionClave].Value.ToString());

            if (ltCv.Contains(iCv))
            {
                ltCv.Clear();
                ctrl.Enabled = false;

                row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                row.Cells[0].Tag = "check";
            }
            else
            {
                ltCv.Clear();
                ltCv.Add(iCv);
                row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                row.Cells[0].Tag = "uncheck";
            }

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

        public static void ImprimirAsignacionesGrid(DataGridView dgv, int iPosicionCheck, int iPosicionClave, List<string> ltcv)
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

        //multiselect con lista de strings
        public static void MultiSeleccionGridViewString(DataGridView dgv, int iPositionClave, List<string> ltCv, Control ctrl)
        {
            DataGridViewRow row = dgv.SelectedRows[0];
            string iCv = row.Cells[iPositionClave].Value.ToString();

            if (ltCv.Contains(iCv))
            { ltCv.Remove(iCv); }
            else
            { ltCv.Add(iCv); }

            if (ltCv.Count == 0)
            { ctrl.Enabled = false; }
            else
            { ctrl.Enabled = true; }

            try
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
            catch (Exception)
            {

            }

        }

        //validacion con lista de strings
        public static bool SinAsignacionesString(DataGridView dgv, int iPosicionCheck, int iPosicionClave, List<string> ltFormas)
        {
            bool bBandera = false;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string iCV = row.Cells[iPosicionClave].Value.ToString();

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

        public static CheckBox AgregarCheckboxHeader(DataGridView dgv, int iPosicionCheck) {


            Rectangle rect = dgv.GetCellDisplayRectangle(iPosicionCheck, -1, true);
            rect.X = rect.Location.X + 5;
            rect.Y = rect.Location.Y + 5;
            rect.Width = rect.Size.Width;
            rect.Height = rect.Size.Height;

            CheckBox checkheader = new CheckBox();
            checkheader.Name = "checkboxHeader";
            checkheader.Size = new Size(15, 15);
            checkheader.Location = rect.Location;
            dgv.Columns[iPosicionCheck].Width = 120;
            dgv.Columns[iPosicionCheck].HeaderText = "    Seleccionar";
            checkheader.Checked = false;
            dgv.Controls.Add(checkheader);

            return checkheader;
        }



        public static string ValidarHoras(string sText) {




            string[] arrSa = sText.Split(':');

            string sHora = arrSa[0].Replace(' ', '0');

            if (sHora == String.Empty) {
                sHora = "00";
            }

            if (Int32.Parse(sHora) > 23) {

                if (sHora.ElementAt(0) > 0)
                {
                    sHora = "0" + sHora.ElementAt(0);
                }
            }


            string sMinutos = arrSa[1].Replace(' ', '0');

            if (sMinutos == String.Empty)
            {
                sMinutos = "00";
            }

            if (Int32.Parse(sMinutos) > 60)
            {
                if (sMinutos.ElementAt(0) > 0)
                {
                    sMinutos = "0" + sMinutos.ElementAt(0);
                }
            }
            sHora = sHora + ":" + sMinutos;

            return sHora;

        }
        public static Size PantallaSistema()
        {
            int sysH = SystemInformation.PrimaryMonitorSize.Height;
            int sysW = SystemInformation.PrimaryMonitorSize.Width;

            return new Size(sysW, sysH);
        }


        public static string ObtenerNombreDiaSemana(string sDia) {

            switch (sDia) {

                case "Monday": sDia = "Lunes"; break;
                case "Thursday": sDia = "Martes"; break;
                case "Wednesday": sDia = "Miércoles"; break;
                case "Tuesday": sDia = "Jueves"; break;
                case "Friday": sDia = "Viernes"; break;
                case "Saturday": sDia = "Sábado"; break;
                case "Sunday": sDia = "Domingo"; break;

            }

            return sDia;
        }

        public static bool CompararBytes(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;

        }


        public static bool CompararMapaBits(Bitmap bmp1, Bitmap bmp2)
        {
            bool equals = true;
            bool flag = true;

            if (bmp1.Size == bmp2.Size)
            {
                for (int x = 0; x < bmp1.Width; ++x)
                {
                    for (int y = 0; y < bmp1.Height; ++y)
                    {
                        if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                        {
                            equals = false;
                            flag = false;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            else
            {
                equals = false;
            }
            return equals;
        }

        

    //MENU DINÁMICO

    public static DataTable CrearEncabezados(DataTable dt)
    {

        DataTable dtEncabezados = new DataTable();
        dtEncabezados.Clear();
        dtEncabezados.Columns.Add("Titulo");
        dtEncabezados.PrimaryKey = new DataColumn[] { dtEncabezados.Columns["Titulo"] };


        foreach (DataRow row in dt.Rows)
        {

            if (Convert.ToInt32(row["Hijos"].ToString()) > 0)
            {
                if (!dtEncabezados.Rows.Contains(row["descripcion"].ToString()))
                {
                    DataRow nwrow = dtEncabezados.NewRow();
                    nwrow["Titulo"] = row["cvmodulo"].ToString();
                    dtEncabezados.Rows.Add(nwrow);
                }
            }
            else
            {
                if (!dtEncabezados.Rows.Contains(row["Tipo"].ToString()))
                {
                    DataRow nwrow = dtEncabezados.NewRow();
                    nwrow["Titulo"] = row["Tipo"].ToString();
                    dtEncabezados.Rows.Add(nwrow);
                }

            }

        }

        return dtEncabezados;
    }

    public static void ProcesoMenu(DataTable dt, string sCvUsuario, string cvModPadre, ToolStripMenuItem MenuHijo, MenuStrip Menu,Color colorMenu)
    {

        foreach (DataRow row in dt.Rows)
        {

            Modulo objModulo = new Modulo();
            DataTable dtTipo = objModulo.ObtenerTipoModulo(5, 0, "", "", "", "", "");
            dtTipo.PrimaryKey = new DataColumn[] { dtTipo.Columns["descripcion"] };
            ToolStripMenuItem Menuitem = new ToolStripMenuItem();
            Menuitem.TextAlign = ContentAlignment.MiddleLeft;
            Menuitem.Text = row["Titulo"].ToString();
                Menuitem.BackColor = colorMenu;
            Menuitem.ForeColor = Color.White;
            Menuitem.Font = new Font("Arial", 12);
            Perfil objPer = new Perfil();

            if (!dtTipo.Rows.Contains(row["Titulo"].ToString()))
            {
                DataTable dtnew = objPer.ReportePerfilesModulos(row["Titulo"].ToString(), "%", sCvUsuario, "CS", 0, 0, 0, 0, 0, 13);
                DataTable dtnewEncabezado = CrearEncabezados(dtnew);

                DataTable dtmodulo = objModulo.ReporteModulos("%", "%", row["Titulo"].ToString(), "%", "", "%", "%", "", "", "", "", 9);

                Menuitem.Text = dtmodulo.Rows[0]["descripcion"].ToString();
                Menuitem.Image = Utilerias.IconosMenu(row["Titulo"].ToString());
                    Menuitem.ImageAlign = ContentAlignment.MiddleLeft;

                if (MenuHijo == null)
                {
                    Menu.Items.Add(Menuitem);
                }
                else
                {
                    MenuHijo.DropDownItems.Add(Menuitem);
                }
                ProcesoMenu(dtnewEncabezado, sCvUsuario, row["Titulo"].ToString(), Menuitem, Menu, colorMenu);
            }
            else
            {

                Menuitem.Image = Utilerias.IconosMenu(row["Titulo"].ToString());
                    Menuitem.ImageAlign = ContentAlignment.MiddleLeft;

                    if (MenuHijo == null)
                {
                    Menu.Items.Add(Menuitem);
                }
                else
                {
                    MenuHijo.DropDownItems.Add(Menuitem);
                }
                DataTable dtnew = objPer.ReportePerfilesModulos(cvModPadre, row["Titulo"].ToString(), sCvUsuario, "CS", 0, 0, 0, 0, 0, 13);
                ValidarHijos(dtnew, Menuitem, colorMenu);

            }
        }

    }

        private static void Menuitem_MouseHover(object sender, EventArgs e)
        {
            ToolStripMenuItem mt = (ToolStripMenuItem)sender;

            //mt.BackColor = Color.Blue;
        }

        public static void ValidarHijos(DataTable dt, ToolStripMenuItem MenuHijo,Color colorMenu)
    {

        foreach (DataRow row in dt.Rows)
        {

            if (Convert.ToInt32(row["Hijos"].ToString()) == 0)
            {

                ToolStripMenuItem Menuitem = new ToolStripMenuItem();
                Menuitem.Text = row["descripcion"].ToString();
                    Menuitem.BackColor = colorMenu;
                Menuitem.ForeColor = Color.White;
                Menuitem.TextAlign = ContentAlignment.MiddleLeft;
                Menuitem.Font = new Font("Arial", 12); Menuitem.ImageTransparentColor = Color.Blue;
                Menuitem.Image = Utilerias.IconosMenu(row["Tipo"].ToString());
                    Menuitem.ImageAlign = ContentAlignment.MiddleLeft;
                    Menuitem.Name = row["Ruta"].ToString();
                Menuitem.Click += new EventHandler(Event);
                MenuHijo.DropDownItems.Add(Menuitem);

            }
        }

    }

    private static void Event(object sender, EventArgs e)
    {
        ToolStripMenuItem ItemClick = (ToolStripMenuItem)sender;
        eclicck(ItemClick.Name);
        }

    private static void eclicck(string nombreformulario)
    {
            Form formulario;
        if (nombreformulario != "")
        {


            formulario = (Form)Activator.CreateInstance(null, nombreformulario).Unwrap();
            formulario.Show();

            }
    }


        public static string cifrarPass(string sPass, int iOpcion)
        {

            byte[] llave;
            byte[] arrCifrado = UTF8Encoding.UTF8.GetBytes(sPass);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("zkteco"));
            md5.Clear();

            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor();

            byte[] resultado = convertir.TransformFinalBlock(arrCifrado, 0, arrCifrado.Length);
            tripledes.Clear();

            string res = "";

           
           res =  Convert.ToBase64String(resultado, 0, resultado.Length);
       

            return res;
        }


        public static string descifrar(string sPass)
        {
           
            byte[] llave;
            byte[] arrCifrado = Convert.FromBase64String(sPass);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("zkteco"));
            md5.Clear();

            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateDecryptor();
            byte[] resultado = convertir.TransformFinalBlock(arrCifrado, 0, arrCifrado.Length);
            tripledes.Clear();

            string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado);
            return cadena_descifrada;
        }

    }
    
    //public class ResultadoHuella
    //{
    //    public NffvStatus engineStatus;
    //    public NffvUser engineUser;
    //}


    //public class VerificarHuella
    //{
    //    public NffvStatus engineStatus;
    //    public int score;
    //}


}
