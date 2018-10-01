using SIPAA_CS.App_Code;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMOTIFPRINTERLib;
using ZMTGraphics;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class CredencialEmpleado : Form
    {
        SonaTrabajador CSonaTrab;
        ZMotifCard front = null;
        ZMotifCard back = null;
        byte[] bmpFront = null, bmpBack = null;
        bool isFrontSelected = false;
        bool guardar = false;
        string key = "";
        FiltrosCredencialEmpleado fcredEmp;

        public CredencialEmpleado()
        {
            InitializeComponent();

            CSonaTrab = new SonaTrabajador();
            fcredEmp = new FiltrosCredencialEmpleado(ref cboEmpleados);
            comboBox3.SelectedIndex = 0;
        }

        private void CredencialEmpleado_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != this.Name)
                {
                    f.Hide();
                }
            }

            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            // Diccionario Permisos x Pantalla
            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);

            try
            {
                if (Permisos.dcPermisos["Imprimir"] == 1)
                {
                    btnImprimir.Enabled = true;
                    button1.Enabled = true;
                }
            }
            catch { }
            //llena combo de empleados
            DataTable dtEmpleado = CSonaTrab.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cboEmpleados, dtEmpleado, "NoEmpleado", "Nombre");

            //crea tool tip
            ToolTip toolTip1 = new ToolTip();
            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(btnCerrar, "Cerrar Sistema");
            toolTip1.SetToolTip(btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(btnRegresar, "Regresar");
            toolTip1.SetToolTip(pictureBox1, "Click para editar elementos");
            toolTip1.SetToolTip(pictureBox2, "Click para editar elementos");

            button1.Image = Resources.Sync;
            button5.Image = Resources.Alta;
            button4.Image = Resources.btnAdd;
            btnImprimir.Image = Resources.Imprimir;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Application.Exit();
        }

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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (cboEmpleados.SelectedIndex <= 0 || comboBox1.SelectedIndex <= 0)
                return;

            DialogResult result = MessageBox.Show("¿Esta seguro de continuar con la impresión?", "SIPPA", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;
            
            try
            {
                paint(ref bmpFront, ref front, ref pictureBox1);
                paint(ref bmpBack, ref back, ref pictureBox2);
                panel2.Visible = false;
                Job job = new Job();
                job.Open(comboBox1.Text);
                //Preferencias de impresión
                job.JobControl.FeederSource = FeederSourceEnum.CardFeeder;
                job.JobControl.Destination = DestinationTypeEnum.Eject;//Produccion
                //job.JobControl.Destination = DestinationTypeEnum.Hold;//Desarrollo
                job.JobControl.OrientationFront = OrientationEnum.Portrait;
                job.JobControl.OrientationBack = OrientationEnum.Portrait;
                job.JobControl.RotationBack = RotationEnum.Rotate_180;

                //***
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Imprimiendo...");
                pnlMensaje.Enabled = false;
                //***
                job.BuildGraphicsLayers(SideEnum.Front, PrintTypeEnum.Color, 0, 0, 0, -1, GraphicTypeEnum.BMP, bmpFront);
                job.BuildGraphicsLayers(SideEnum.Back, PrintTypeEnum.MonoK, 0, 0, 0, -1, GraphicTypeEnum.BMP, bmpBack);

                int actionID = 0;
                job.PrintGraphicsLayers(1, out actionID);
                job.ClearGraphicsLayers();
            }
            catch
            {
                //***
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error al intentar imprimir, verificar impresora");
                pnlMensaje.Enabled = false;
                //***
            }
            finally
            {
                timer1.Start();
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object _deviceList = null;

            enableComponents(false);
            Cursor = Cursors.WaitCursor;

            try
            {
                Job job = new Job();
                //***
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Buscando impresoras disponibles...");
                pnlMensaje.Enabled = false;
                //***
                job.GetPrinters(ConnectionTypeEnum.All, out _deviceList);

                if (_deviceList != null)
                {
                    comboBox1.Items.Clear();

                    Array array = (Array)_deviceList;
                    comboBox1.Items.Add("-Seleccionar Impresora-");

                    for (int i = 0; i < array.GetLength(0); i++)
                        comboBox1.Items.Add((string)array.GetValue(i));

                    comboBox1.SelectedIndex = 0;
                    comboBox1.Enabled = true;
                    cboEmpleados.Enabled = true;
                    //***
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Se encontraron " + (comboBox1.Items.Count - 1) + " impresoras");
                    pnlMensaje.Enabled = false;
                    //***
                }
                else
                {
                    //***
                    pnlMensaje.Enabled = true;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se encontraron impresoras");
                    pnlMensaje.Enabled = false;
                    //***
                }
            }
            catch
            {
                //***
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error al buscar impresoras");
                pnlMensaje.Enabled = false;
                //***
            }
            finally
            {
                enableComponents(true);
                Cursor = Cursors.Default;
                timer1.Start();
            }
        }

        private void enableComponents(bool status)
        {
            button1.Enabled = status;
            btnImprimir.Enabled = status;
            cboEmpleados.Enabled = status;
            comboBox1.Enabled = status;
            panel2.Enabled = status;
            button6.Enabled = status;

            if (!status)
            {
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox1.Enabled = status;
                pictureBox2.Enabled = status;
                panel2.Visible = status;
                if (cboEmpleados.Items.Count > 0) cboEmpleados.SelectedIndex = 0;
            }
        }

        private void cboEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmpleados.SelectedIndex <= 0 || comboBox1.SelectedIndex <= 0) 
                return;

            panel2.Visible = false;

            if (cbuniv.CheckState == CheckState.Checked)
            {
                if (MapUniv())
                {
                    pictureBox1.Enabled = true;
                    pictureBox2.Enabled = true;
                    paint(ref bmpFront, ref front, ref pictureBox1);
                    paint(ref bmpBack, ref back, ref pictureBox2);
                }
            }
            else
            {
                if (Map())
                {
                    pictureBox1.Enabled = true;
                    pictureBox2.Enabled = true;
                    paint(ref bmpFront, ref front, ref pictureBox1);
                    paint(ref bmpBack, ref back, ref pictureBox2);
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //...
        }

        private bool Map()
        {
            bool status = false;
            try
            {
                DataTable table = CSonaTrab.ObtenerPerfilTrabajador(cboEmpleados.SelectedValue.ToString(), 21, "%", "%", 0, "%", "%");
                string IdTrab = "", Paterno = "", Materno = "", Nombre = "", Puesto = "", Plantel = "", Compania = "", Departamento = "", Calle = "", Exterior = "", Interior = "", CP = "", Colonia = "", IMSS = "" ;

                foreach (DataRow row in table.Rows)
                {
                    IdTrab = row["IdTrab"].ToString().Trim(' ');
                    Paterno = row["Paterno"].ToString().Trim(' ');
                    Materno = row["Materno"].ToString().Trim(' ');
                    Nombre = row["Nombre"].ToString().Trim(' ');
                    Puesto = row["Puesto"].ToString().Trim(' ');
                    Plantel = row["Plantel"].ToString().Trim(' ');
                    Compania = row["Compania"].ToString().Trim(' ');
                    Departamento = row["Departamento"].ToString().Trim(' ');
                    Calle = row["Calle"].ToString().Trim(' ');
                    Exterior = row["Exterior"].ToString().Trim(' ');
                    Interior = row["Interior"].ToString().Trim(' ');
                    CP = row["CP"].ToString().Trim(' ');
                    Colonia = row["Colonia"].ToString().Trim(' ');
                    IMSS = row["IMSS"].ToString().Trim(' ');
                  
                }

                ZMotifGraphics g = new ZMotifGraphics();
                //Datos Frente...
                front = new ZMotifCard(ZMotifGraphics.ImageOrientationEnum.Portrait, ZMotifGraphics.RibbonTypeEnum.Color);

                try { front.imgs.Add(ZMotifCard.background, new ZMotifImage(ImageToByteArray(frontBackground(Compania)), 0, 0, ZMotifGraphics.ImagePositionEnum.Centered, 648, 1024, 0)); }
                catch { }

                try { front.imgs.Add("FotoEmpleado", new ZMotifImage(g.ImageFileToByteArray(@"\\192.168.30.171\FotosJS\FotosEmpleados\" + cboEmpleados.SelectedValue + ".jpg"), 23f, 209f, ZMotifGraphics.ImagePositionEnum.Centered, 303, 305, 0)); }
                catch { front.imgs.Add("FotoEmpleado", new ZMotifImage(ImageToByteArray(pictureBox1.ErrorImage), 23f, 209f, ZMotifGraphics.ImagePositionEnum.Centered, 75, 75, 0)); }

                if (Compania.Equals("GMX GRUPO CONSULTOR S.A DE C.V"))
                {
                    front.txts.Add("Puesto", new ZMotifText(Puesto, 340f, 260f, "Arial Narrow", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Plantel", new ZMotifText(Plantel, 340f, 380f, "Arial Narrow", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.Black));
                    front.txts.Add("Nombre", new ZMotifText(Paterno + " " + Materno + " " + Nombre, 40f, 530f, "Arial Narrow", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("descripcion1", new ZMotifText("QUEJAS Y SUGERENCIAS", 110f, 660f, "Calibri", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.Black));
                    front.txts.Add("descripcion2", new ZMotifText("A LOS TELEFONOS", 170f, 700f, "Calibri", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.Black));
                    front.txts.Add("descripcion3", new ZMotifText("50954312 - 55977628", 150f, 740f, "Calibri", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.Black));
                    front.txts.Add("noEmpleado_d", new ZMotifText("No. Empleado:", 40f, 820f, "Arial", 9f, ZMotifGraphics.FontTypeEnum.Regular, Color.Black));
                    front.txts.Add("IdTrab", new ZMotifText(IdTrab, 300f, 815f, "Arial Narrow", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Compania", new ZMotifText(Compania, 40f, 890f, "Arial Narrow", 9f, ZMotifGraphics.FontTypeEnum.Bold, Color.White));
                    front.txts.Add("vig_d", new ZMotifText("Fecha de expedición:", 230f, 940f, "Arial Narrow", 7f, ZMotifGraphics.FontTypeEnum.Bold, Color.White));
                    front.txts.Add("Vigencia", new ZMotifText(fcredEmp.date.ToShortDateString(), 480f, 940f, "Arial Narrow", 7f, ZMotifGraphics.FontTypeEnum.Bold, Color.White));                    
                }
                else
                {
                    front.txts.Add("Paterno", new ZMotifText(Paterno, 340f, 220f, "Times New Roman", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Materno", new ZMotifText(Materno, 340f, 260f, "Times New Roman", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Nombre", new ZMotifText(Nombre, 340f, 300f, "Times New Roman", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Vigencia", new ZMotifText(fcredEmp.date.ToShortDateString(), 340f, 430f, "Times New Roman", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("IdTrab", new ZMotifText(IdTrab, 380f, 470f, "Times New Roman", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Puesto", new ZMotifText(Puesto, 40f, 550f, "Times New Roman", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                    front.txts.Add("Plantel", new ZMotifText(Plantel, 150f, 798f, "Times New Roman", 14.25f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.White));
                }

                //Datos Reverso...
                back = new ZMotifCard(ZMotifGraphics.ImageOrientationEnum.Portrait, ZMotifGraphics.RibbonTypeEnum.MonoK);
                back.txts.Add("dep_d", new ZMotifText("Departamento:", 40f, 105f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Departamento", new ZMotifText(Departamento, 40f, 140f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("dir_d", new ZMotifText("Dirección Empleado:", 40f, 260f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Calle", new ZMotifText(Calle, 40f, 295f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("ext_d", new ZMotifText("No. Exterior:", 40f, 410f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Exterior", new ZMotifText(Exterior, 250f, 410f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("int_d", new ZMotifText("No. Interior:", 40f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Interior", new ZMotifText(Interior, 250f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("cp_d", new ZMotifText("C.P.:", 400f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("CP", new ZMotifText(CP, 490f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("col_d", new ZMotifText("Colonia:", 40f, 530f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Colonia", new ZMotifText(Colonia, 40f, 580f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("imss_d", new ZMotifText("I.M.S.S.:", 40f, 740f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("IMSS", new ZMotifText(IMSS, 200f, 740f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("firm_d", new ZMotifText("FIRMA TRABAJADOR", 150f, 970f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));

                status = true;
            }
            catch
            {
                pictureBox1.Image = pictureBox1.ErrorImage;
                pictureBox2.Image = pictureBox2.ErrorImage;
                pictureBox1.Enabled = false;
                pictureBox2.Enabled = false;
                //***
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Error al obtener datos");
                pnlMensaje.Enabled = false;
                //***
            }

            return status;
        }

        private bool MapUniv()
        {
            bool status = false;
            try
            {
                DataTable table = CSonaTrab.ObtenerPerfilTrabajador(cboEmpleados.SelectedValue.ToString(), 21, "%", "%", 0, "%", "%");
                string IdTrab = "", Paterno = "", Materno = "", Nombre = "", Puesto = "", Plantel = "", Compania = "", Departamento = "", Calle = "", Exterior = "", Interior = "", CP = "", Colonia = "", IMSS = "", Longitud= "";

                foreach (DataRow row in table.Rows)
                {
                    IdTrab = row["IdTrab"].ToString().Trim(' ');
                    Paterno = row["Paterno"].ToString().Trim(' ');
                    Materno = row["Materno"].ToString().Trim(' ');
                    Nombre = row["Nombre"].ToString().Trim(' ');
                    Puesto = row["Puesto"].ToString().Trim(' ');
                    Plantel = row["Plantel"].ToString().Trim(' ');
                    Compania = row["Compania"].ToString().Trim(' ');
                    Departamento = row["Departamento"].ToString().Trim(' ');
                    Calle = row["Calle"].ToString().Trim(' ');
                    Exterior = row["Exterior"].ToString().Trim(' ');
                    Interior = row["Interior"].ToString().Trim(' ');
                    CP = row["CP"].ToString().Trim(' ');
                    Colonia = row["Colonia"].ToString().Trim(' ');
                    IMSS = row["IMSS"].ToString().Trim(' ');
                    Longitud = row["Longitud"].ToString().Trim(' ');
                }

                ZMotifGraphics g = new ZMotifGraphics();
                //Datos Frente...
                front = new ZMotifCard(ZMotifGraphics.ImageOrientationEnum.Portrait, ZMotifGraphics.RibbonTypeEnum.Color);

                try { front.imgs.Add(ZMotifCard.background, new ZMotifImage(ImageToByteArray(frontBackgroundUniv()), 0, 0, ZMotifGraphics.ImagePositionEnum.Centered, 648, 1024, 0)); }
                catch { }

                try { front.imgs.Add("FotoEmpleado", new ZMotifImage(g.ImageFileToByteArray(@"\\192.168.30.171\FotosJS\FotosEmpleados\" + cboEmpleados.SelectedValue + ".jpg"), 23f, 120f, ZMotifGraphics.ImagePositionEnum.Centered, 265, 305, 0)); }
                catch { front.imgs.Add("FotoEmpleado", new ZMotifImage(ImageToByteArray(pictureBox1.ErrorImage), 23f, 209f, ZMotifGraphics.ImagePositionEnum.Centered, 75, 75, 0)); }
                
                front.txts.Add("Paterno", new ZMotifText(Paterno, 295f, 265f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                front.txts.Add("Materno", new ZMotifText(Materno, 295f, 316f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                front.txts.Add("Nombre", new ZMotifText(Nombre, 295f, 365f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                //front.txts.Add("Vigencia", new ZMotifText(fcredEmp.date.ToShortDateString(), 340f, 340f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Regular, Color.FromArgb(255, 0, 0, 160)));
                front.txts.Add("IdTrab", new ZMotifText(IdTrab, 106f, 425f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.Red )); //192; 0; 0
                if (Longitud=="1")
                    front.txts.Add("Puesto", new ZMotifText(Puesto, 228f, 475f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                else if (Longitud=="2")
                    front.txts.Add("Puesto", new ZMotifText(Puesto, 173f, 475f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                else if (Longitud == "3")
                    front.txts.Add("Puesto", new ZMotifText(Puesto, 30f, 475f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                else
                  front.txts.Add("Puesto", new ZMotifText(Puesto, 15f, 475f, "Myriad Pro", 10f, ZMotifGraphics.FontTypeEnum.Bold, Color.FromArgb(255, 0, 0, 160)));
                
               
                //front.txts.Add("Plantel", new ZMotifText(Plantel, 150f, 798f, "Myriad Pro", 14.25f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.White));

                //Datos Reverso...
                back = new ZMotifCard(ZMotifGraphics.ImageOrientationEnum.Portrait, ZMotifGraphics.RibbonTypeEnum.MonoK);
                try { back.imgs.Add(ZMotifCard.background, new ZMotifImage(ImageToByteArray(Resources.credfirmada), 0, 0, ZMotifGraphics.ImagePositionEnum.Centered, 648, 1024, 0)); }
                catch { }

                back.txts.Add("dep_d", new ZMotifText("Departamento:", 40f, 120f, "Myriad Pro", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("Departamento", new ZMotifText(Departamento, 40f, 160f, "Myriad Pro", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("vig_d", new ZMotifText("Vigencia:", 40f, 275f, "Myriad Pro", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("Vigencia", new ZMotifText(fcredEmp.date.ToShortDateString(), 40f, 315f, "Myriad Pro", 8f, ZMotifGraphics.FontTypeEnum.Bold, Color.Black));
                /*back.txts.Add("ext_d", new ZMotifText("No. Exterior:", 40f, 410f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Exterior", new ZMotifText(Exterior, 250f, 410f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("int_d", new ZMotifText("No. Interior:", 40f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Interior", new ZMotifText(Interior, 250f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("cp_d", new ZMotifText("C.P.:", 400f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("CP", new ZMotifText(CP, 490f, 460f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("col_d", new ZMotifText("Colonia:", 40f, 530f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Regular, System.Drawing.Color.Black));
                back.txts.Add("Colonia", new ZMotifText(Colonia, 40f, 580f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));*/
                back.txts.Add("imss_d", new ZMotifText("I.M.S.S.:", 40f, 700f, "Myriad Pro", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                back.txts.Add("IMSS", new ZMotifText(IMSS, 200f, 700f, "Myriad Pro", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));
                //back.txts.Add("firm_d", new ZMotifText("FIRMA TRABAJADOR", 150f, 970f, "Arial", 8f, ZMotifGraphics.FontTypeEnum.Bold, System.Drawing.Color.Black));

                status = true;
            }
            catch
            {
                pictureBox1.Image = pictureBox1.ErrorImage;
                pictureBox2.Image = pictureBox2.ErrorImage;
                pictureBox1.Enabled = false;
                pictureBox2.Enabled = false;
                //***
                pnlMensaje.Enabled = true;
                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Error al obtener datos");
                pnlMensaje.Enabled = false;
                //***
            }

            return status;
        }

        private Bitmap frontBackground(string compania)
        {
            switch (compania)
            {
                case "ESCUELA SECUNDARIA Y PREPARATORIA JUSTO SIERRA, A.C.":
                    return Resources.ESYPJS;
                case "NUEVA ESCUELA JUSTO SIERRA, A.C.":
                    return Resources.NEJS;
                case "CENTRO CULTURAL UNIVERSITARIO JUSTO SIERRA, A.C.":
                    return Resources.CCUJS;
                case "BACHILLERATO TECNOLOGICO JUSTO SIERRA, A.C.":
                    return Resources.BTJS;
                case "GMX GRUPO CONSULTOR S.A DE C.V":
                    return Resources.GMX;
                default:
                    return Resources._default;
            }
        }   

        private Bitmap frontBackgroundUniv()
        {
            string value = comboBox3.SelectedItem.ToString();
            switch (value)
            {
                case "administrativos":
                    return Resources.administrativos;
                case "docentes":
                    return Resources.docentes;
                default:
                    return Resources._default;
            }
        }

        private void paint(ref byte[] image, ref ZMotifCard card, ref PictureBox pbox)
        {
            try
            {
                ZMotifGraphics g = new ZMotifGraphics();
                ZMotifImage img = null;
                ZMotifText text = null;
                int dataLen = 0;

                g.InitGraphics(0, 0, card.imageOrientation, card.ribbonType);                        
                //Imagenes...
                foreach (KeyValuePair<string, ZMotifImage> val in card.imgs)
                    if (card.imgs.TryGetValue(val.Key, out img) && val.Key != ZMotifCard.background)
                        g.DrawImage(ref img.img, img.x, img.y, img.width, img.height, img.opacity);
                    else if (card.imgs.TryGetValue(ZMotifCard.background, out img))
                        g.DrawImage(ref img.img, img.imagePosition, img.width, img.height, 0);
                //Textos...
                foreach (KeyValuePair<string, ZMotifText> val in card.txts)
                    if (card.txts.TryGetValue(val.Key, out text))
                        g.DrawTextString(text.x, text.y, text.value, text.fontName, text.fontSize, text.fontStyle, g.IntegerFromColor(text.color));

                image = g.CreateBitmap(out dataLen);
                pbox.Image = Image.FromStream(new MemoryStream(image));
                g.ClearGraphics();
            }
            catch
            {
                pbox.Image = pictureBox1.ErrorImage;
            }
        }

        private class ZMotifCard
        {
            public ZMotifGraphics.ImageOrientationEnum imageOrientation;
            public ZMotifGraphics.RibbonTypeEnum ribbonType;
            public Dictionary<string, ZMotifImage> imgs;
            public Dictionary<string, ZMotifText> txts;
            public static string background = "Fondo";

            public ZMotifCard(ZMotifGraphics.ImageOrientationEnum imageOrientation, ZMotifGraphics.RibbonTypeEnum ribbonType)
            {
                this.imageOrientation = imageOrientation;
                this.ribbonType = ribbonType;
                imgs = new Dictionary<string, ZMotifImage>();
                txts = new Dictionary<string, ZMotifText>();
            }
        }
        private class ZMotifImage
        {
            public byte[] img;
            public float x, y, opacity;
            public int width, height;
            public ZMotifGraphics.ImagePositionEnum imagePosition;

            public ZMotifImage(byte[] img, float x, float y, ZMotifGraphics.ImagePositionEnum imagePosition, int width, int height, float opacity)
            {
                this.img = img;
                this.x = x;
                this.y = y;
                this.imagePosition = imagePosition;
                this.width = width;
                this.height = height;
                this.opacity = opacity;
            }
        }
        private class ZMotifText
        {
            public string value, fontName;
            public float x, y, fontSize;
            public ZMotifGraphics.FontTypeEnum fontStyle;
            public Color color;
            public static float lnbreak = 40.0f;

            public ZMotifText(string value, float x, float y, string fontName, float fontSize, ZMotifGraphics.FontTypeEnum fontStyle, Color color)
            {
                this.value = value;
                this.x = x;
                this.y = y;
                this.fontName = fontName;
                this.fontSize = fontSize;
                this.fontStyle = fontStyle;
                this.color = color;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            isFrontSelected = true;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.BorderStyle = BorderStyle.None;
            showEditPanel(ref front);
        }

        private class ClaveValor
        {
            public string clave;
            public string valor;

            public ClaveValor(string clave, string valor)
            {
                this.clave = clave;
                this.valor = valor;
            }
        }

        private void showEditPanel(ref ZMotifCard card)
        {
            comboBox2.DataSource = null;
            Dictionary<string, string> source = new Dictionary<string, string>();
            source.Add("0", "-Seleccionar Elemento-");

            foreach (KeyValuePair<string, ZMotifText> val in card.txts)
                source.Add(val.Key, val.Value.value);

            foreach (KeyValuePair<string, ZMotifImage> val in card.imgs)
                source.Add(val.Key, val.Key);

            comboBox2.DataSource = new BindingSource(source, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            resetEditPanel();
            panel2.Visible = true;
        }

        private void resetEditPanel()
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
            numericUpDown2.Value = numericUpDown2.Minimum;
            numericUpDown3.Value = numericUpDown3.Minimum;
            numericUpDown4.Value = numericUpDown4.Minimum;
            trackBar1.Value = trackBar1.Minimum;
            trackBar2.Value = trackBar2.Minimum;
            trackBar3.Value = trackBar3.Minimum;
            trackBar4.Value = trackBar4.Minimum;
            linkLabel1.Text = "";
            button2.BackColor = Color.Black;
            richTextBox1.Text = "";
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            pictureBox3.Image = null;
            button4.Image = Resources.btnAdd;

            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            trackBar4.Enabled = false;
            linkLabel1.Enabled = false;
            button2.Enabled = false;
            richTextBox1.Enabled = false;
            button5.Enabled = false;
            pictureBox3.Enabled = false;
            button3.Enabled = false;
            guardar = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0)
                return;

            ZMotifText txt;
            ZMotifImage img;
            key = comboBox2.SelectedValue.ToString();
            resetEditPanel();

            if (sideSelected().txts.TryGetValue(key, out txt))
            {
                numericUpDown1.Value = (decimal)sideSelected().txts[key].x;
                numericUpDown2.Value = (decimal)sideSelected().txts[key].y;
                linkLabel1.Text = sideSelected().txts[key].fontName + ", " + sideSelected().txts[key].fontSize + "Pts.";
                button2.BackColor = sideSelected().txts[key].color;
                richTextBox1.Text = sideSelected().txts[key].value;
                //
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                linkLabel1.Enabled = true;
                button2.Enabled = true;
                richTextBox1.Enabled = true;
                button5.Enabled = true;
            }
            else if (sideSelected().imgs.TryGetValue(key, out img))
            {
                pictureBox3.Image = Image.FromStream(new MemoryStream(img.img));
                numericUpDown1.Value = (decimal)sideSelected().imgs[key].x;
                numericUpDown2.Value = (decimal)sideSelected().imgs[key].y;
                numericUpDown3.Value = sideSelected().imgs[key].height;
                numericUpDown4.Value = sideSelected().imgs[key].width;
                //
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                trackBar3.Enabled = true;
                trackBar4.Enabled = true;
                pictureBox3.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                linkLabel1.Text = fontDialog1.Font.Name + ", " + fontDialog1.Font.Size + "Pts.";

                if (comboBox2.SelectedIndex < 0)
                    return;
                
                sideSelected().txts[key].fontName = fontDialog1.Font.Name;
                sideSelected().txts[key].fontSize = fontDialog1.Font.Size;
                sideSelected().txts[key].fontStyle = getFontTypeEnum(fontDialog1.Font.Style);

                paint(ref bmpFront, ref front, ref pictureBox1);
                paint(ref bmpBack, ref back, ref pictureBox2);
            }
        }

        private ZMotifGraphics.FontTypeEnum getFontTypeEnum(FontStyle style)
        {
            switch (style)
            {
                case FontStyle.Bold:
                    return ZMotifGraphics.FontTypeEnum.Bold;
                case FontStyle.Italic:
                    return ZMotifGraphics.FontTypeEnum.Italic;
                case FontStyle.Strikeout:
                    return ZMotifGraphics.FontTypeEnum.Strikeout;
                case FontStyle.Underline:
                    return ZMotifGraphics.FontTypeEnum.Underline;
                default:
                    return ZMotifGraphics.FontTypeEnum.Regular;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0 || numericUpDown1.Value == numericUpDown1.Minimum)
                return;
            
            ZMotifText txt;
            ZMotifImage img;

            if (sideSelected().txts.TryGetValue(key, out txt))
                sideSelected().txts[key].x = (float)numericUpDown1.Value;
            else if (sideSelected().imgs.TryGetValue(key, out img))
                sideSelected().imgs[key].x = (float)numericUpDown1.Value;

            trackBar1.Value = (int)numericUpDown1.Value;
            paint(ref bmpFront, ref front, ref pictureBox1);
            paint(ref bmpBack, ref back, ref pictureBox2);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0 || numericUpDown2.Value == numericUpDown2.Minimum)
                return;

            ZMotifText txt;
            ZMotifImage img;

            if (sideSelected().txts.TryGetValue(key, out txt))
                sideSelected().txts[key].y = (float)numericUpDown2.Value;
            else if (sideSelected().imgs.TryGetValue(key, out img))
                sideSelected().imgs[key].y = (float)numericUpDown2.Value;

            trackBar2.Value = (int)numericUpDown2.Value;
            paint(ref bmpFront, ref front, ref pictureBox1);
            paint(ref bmpBack, ref back, ref pictureBox2);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            isFrontSelected = false;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BorderStyle = BorderStyle.None;
            showEditPanel(ref back);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;

                if (comboBox2.SelectedIndex < 0)
                    return;

                sideSelected().txts[key].color = colorDialog1.Color;
                paint(ref bmpFront, ref front, ref pictureBox1);
                paint(ref bmpBack, ref back, ref pictureBox2);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!guardar)
            {
                resetEditPanel();
                linkLabel1.Text = fontDialog1.Font.Name + ", " + fontDialog1.Font.Size + "Pts.";
                button2.BackColor = colorDialog1.Color;
                comboBox2.DataSource = null;
                comboBox2.DropDownStyle = ComboBoxStyle.Simple;
                comboBox2.Text = "Agregar texto...";
                comboBox2.Focus();
                button4.Image = Resources.Guardar;

                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                linkLabel1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                guardar = true;
            }
            else
            {
                string texto = comboBox2.Text.Trim(' ');
                //***************************************************************
                ZMotifCard card = sideSelected();
                //***************************************************************

                if (pictureBox3.Image == null && texto != string.Empty)
                {
                    
                    card.txts.Add(DateTime.Now.ToLongTimeString(), 
                        new ZMotifText(texto,
                            (float) numericUpDown1.Value,
                            (float) numericUpDown2.Value,
                            fontDialog1.Font.Name,
                            fontDialog1.Font.Size,
                            getFontTypeEnum(fontDialog1.Font.Style),
                            colorDialog1.Color));

                    paint(ref bmpFront, ref front, ref pictureBox1);
                    paint(ref bmpBack, ref back, ref pictureBox2);
                }
                else if (texto != string.Empty)
                {
                    ZMotifGraphics g = new ZMotifGraphics();

                    card.imgs.Add(DateTime.Now.ToLongTimeString(),
                        new ZMotifImage(g.ImageFileToByteArray(openFileDialog1.FileName),
                            (float)numericUpDown1.Value,
                            (float)numericUpDown2.Value,
                            ZMotifGraphics.ImagePositionEnum.Centered,
                            (int)numericUpDown4.Value,
                            (int)numericUpDown3.Value,
                            0.0f));

                    paint(ref bmpFront, ref front, ref pictureBox1);
                    paint(ref bmpBack, ref back, ref pictureBox2);
                }
                
                panel2.Visible = false;
            }
        }

        private void button5_Click(object sendfer, EventArgs e)
        {
            string[] lines = richTextBox1.Lines;

            if (lines.Length == 0)
                return;

            ZMotifCard card = sideSelected();
            ZMotifText text;

            if (card.txts.TryGetValue(key, out text))
            {
                card.txts.Remove(key);
                float y = text.y;

                foreach (string line in lines)
                {
                    if (line.Trim(' ').Length == 0)
                        continue;

                    card.txts.Add(line, new ZMotifText(line, text.x, y, text.fontName, text.fontSize, text.fontStyle, text.color));
                    y += ZMotifText.lnbreak;
                }

                paint(ref bmpFront, ref front, ref pictureBox1);
                paint(ref bmpBack, ref back, ref pictureBox2);

                showEditPanel(ref card);
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0 || numericUpDown4.Value == numericUpDown4.Minimum)
                return;
            
            ZMotifImage img;

            if (sideSelected().imgs.TryGetValue(key, out img))
                sideSelected().imgs[key].width = (int)numericUpDown4.Value;

            trackBar3.Value = (int)numericUpDown4.Value;
            paint(ref bmpFront, ref front, ref pictureBox1);
            paint(ref bmpBack, ref back, ref pictureBox2);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 0 || numericUpDown3.Value == numericUpDown3.Minimum)
                return;

            ZMotifImage img;

            if (sideSelected().imgs.TryGetValue(key, out img))
                sideSelected().imgs[key].height = (int)numericUpDown3.Value;

            trackBar4.Value = (int)numericUpDown3.Value;
            paint(ref bmpFront, ref front, ref pictureBox1);
            paint(ref bmpBack, ref back, ref pictureBox2);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = (int)trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream myStream = null;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {                    
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            pictureBox3.Image = Image.FromStream(myStream);
                            
                            if (comboBox2.SelectedIndex >= 0)
                            {
                                sideSelected().imgs[key] = new ZMotifImage(
                                    ImageToByteArray(Image.FromStream(myStream)),
                                    sideSelected().imgs[key].x,
                                    sideSelected().imgs[key].y,
                                    sideSelected().imgs[key].imagePosition,
                                    sideSelected().imgs[key].width,
                                    sideSelected().imgs[key].height,
                                    sideSelected().imgs[key].opacity);

                                paint(ref bmpFront, ref front, ref pictureBox1);
                                paint(ref bmpBack, ref back, ref pictureBox2);
                            }
                            else
                            {
                                linkLabel1.Text = "";
                                comboBox2.Text = "Titulo de imagen...";
                                comboBox2.Focus();
                                numericUpDown4.Value = pictureBox3.Width;
                                numericUpDown3.Value = pictureBox3.Height;
                                //
                                numericUpDown3.Enabled = true;
                                numericUpDown4.Enabled = true;
                                trackBar3.Enabled = true;
                                trackBar4.Enabled = true;
                                linkLabel1.Enabled = false;
                                button2.Enabled = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = (int)trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            numericUpDown4.Value = (int)trackBar3.Value;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            numericUpDown3.Value = (int)trackBar4.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            pnlMensaje.Enabled = panelTag.Enabled = false;
            timer1.Stop();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            fcredEmp.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int items = cboEmpleados.Items.Count;

            if (items > 0 && cboEmpleados.SelectedIndex + 1 <= items - 1)
            {
                cboEmpleados.SelectedIndex = cboEmpleados.SelectedIndex + 1;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int items = cboEmpleados.Items.Count;

            if (items > 0 && cboEmpleados.SelectedIndex - 1 > 0)
            {
                cboEmpleados.SelectedIndex = cboEmpleados.SelectedIndex - 1;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cbuniv_CheckedChanged(object sender, EventArgs e)
        {
            comboBox3.Visible = cbuniv.CheckState == CheckState.Checked;
        }

        private ZMotifCard sideSelected()
        {
            return isFrontSelected ? front : back;
        }
    }
}
