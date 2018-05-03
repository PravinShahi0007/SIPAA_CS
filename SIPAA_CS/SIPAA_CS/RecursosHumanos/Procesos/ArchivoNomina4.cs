using CrystalDecisions.CrystalReports.Engine;
//using SIPAA_CS.Accesos.Reportes;
using SIPAA_CS.RecursosHumanos.Reportes;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.RecursosHumanos.DataSets;
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
using static SIPAA_CS.App_Code.Usuario;

//***********************************************************************************************
//Autor: Jose Luis Alvarez Delgado
//Fecha creación: 03-Abr-2017       Última Modificacion: 
//Descripción: Proceso de generación de archivo para nomina 4.0
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class ArchivoNomina4 : Form
    {
        //////instanciamos los objetos (segun san lucas)
        ArchNomina4 oArchivoNomina4 = new ArchNomina4();
        SonaCompania2 oCompañia = new SonaCompania2();
        SonaTipoNomina oTiponomina = new SonaTipoNomina();
        SonaUbicacion oUbicacion = new SonaUbicacion();
        Utilerias util = new Utilerias();
        SaveFileDialog saveFileDialogArchivo = new SaveFileDialog();
        SonaTrabajador contenedorempleados = new SonaTrabajador();
        ProcesaIncidencia ProcesaIncidencias = new ProcesaIncidencia();
        Incidencia objIncidencia = new Incidencia();
        IncCalificacion objActualizaIncidencia = new IncCalificacion();

        public ArchivoNomina4()
        {
            InitializeComponent();
        }

        //para validar inicio de combo
        bool bprimeravez = true;
        bool bprimeracb = true;
        int iprespuesta;

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        //seleccionan compañia
        private void cbCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bprimeravez)
            {
                int iseleccion;
                iseleccion =Int32.Parse(cbCompania.SelectedValue.ToString());
                DataTable dtTipoNomina = oTiponomina.obtTipoNomina(5,iseleccion, 0, "");
                cbTiponomina.DataSource = dtTipoNomina;
                cbTiponomina.DisplayMember = "Descripción";
                cbTiponomina.ValueMember = "Clave";
                bprimeracb = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string ubica = cbUbicacion.SelectedValue.ToString();
            if ((cbCompania.Text == "" | cbCompania.Text == "Seleccionar Compañia...") | (cbTiponomina.Text == "" | cbTiponomina.Text == "Seleccionar Tipo Nomina..."))
            {
                MessageBox.Show("Debe seleccionar la compañia y el tipo de nomina", "SIPPA", MessageBoxButtons.OK);
                cbCompania.Focus();

            }
            else if (dtpfechainicial.Text == "" | dtpfechafinal.Text == "")
            {
                MessageBox.Show("Proporcione un rango de fechas", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }

            else if (cbEmpleados.Text == "" | cbEmpleados.Text == "Seleccionar" & (cbUbicacion.SelectedValue.ToString() == "" | Convert.ToInt32(cbUbicacion.SelectedValue.ToString()) == 0
                | cbUbicacion.Text == "Seleccionar Ubicación..."))
            {
                fgridarchivonomina4(4, 0, Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), 0,
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), LoginInfo.IdTrab, "ProcArchNom");
            }
            else if (cbEmpleados.Text == "" | cbEmpleados.Text == "Seleccionar" & (cbUbicacion.SelectedValue.ToString() != "" | cbUbicacion.Text == "Seleccionar Ubicación..."))
            {
                fgridarchivonomina4(5, 0, Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), Convert.ToInt32(cbUbicacion.SelectedValue.ToString()),
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), LoginInfo.IdTrab, "ProcArchNom");
            }
            else if (cbEmpleados.Text != "" & (cbUbicacion.SelectedValue.ToString() == "" | cbUbicacion.Text == "Seleccionar Ubicación..."))
            {
                fgridarchivonomina4(6, Convert.ToInt32(cbEmpleados.SelectedValue.ToString()), Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), 0,
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), LoginInfo.IdTrab, "ProcArchNom");
            }
            else
            {
                fgridarchivonomina4(7, Convert.ToInt32(cbEmpleados.SelectedValue.ToString()), Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), 0,
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), LoginInfo.IdTrab, "ProcArchNom");
            }
        }

        private void btngenerararchivo_Click(object sender, EventArgs e)
        {
            if (txtanonom.Text=="" | txtnumnom.Text=="")
            {
                MessageBox.Show("Debe proporcionar el Año de Nomina y Número de Nomina", "SIPPA");
                txtanonom.Focus();
            }
            else
            {
                creacsvcorto();
                
                string idTrab = "%";
                string cvCia = "%";
                string cvUbicacion = "%";
                string sNomina = "%";

                if (cbEmpleados.Text != String.Empty && cbEmpleados.Text != "Seleccionar")
                {
                    idTrab = cbEmpleados.SelectedValue.ToString();
                }
                if (cbCompania.Text != "" & cbCompania.Text != "Seleccionar Compañia...")
                {
                    cvCia = cbCompania.SelectedValue.ToString();
                }
                if (Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) > 0 & cbUbicacion.Text != "Seleccionar...")
                {
                    cvUbicacion = cbUbicacion.SelectedValue.ToString();
                }
                if (cbTiponomina.Text != "" & cbTiponomina.Text != "Seleccionar Tipo Nomina...")
                {
                    sNomina = cbTiponomina.SelectedValue.ToString();
                }

                DataTable dtIncidencia = objIncidencia.ReporteIncidenciasPasadasNomina(idTrab, dtpfechainicial.Value.Date,  dtpfechafinal.Value.Date, cvCia, sNomina, cvUbicacion);
                switch (dtIncidencia.Rows.Count)
                {
                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtIncidencia, "SIPAA_CS.RecursosHumanos.Reportes", "IncidenciasPasadasNomina.rpt");
                        ReportDoc.SetParameterValue("TotalRegistros", dtIncidencia.Rows.Count.ToString());
                        ReportDoc.SetParameterValue("FechaActual", DateTime.Now.ToString("dd/MM/yyyy"));
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;
                }

                //Marcar archivo como Generado=2=AG JLA 13/11/2017 22/01/2018 
                foreach (DataGridViewRow renglon in dgvArchivoNomina4.Rows)
                {
                    try
                    {
                        string sClave = renglon.Cells["Clave"].Value.ToString();
                        DateTime dtFechaReg = DateTime.Parse(renglon.Cells["FechaReg"].Value.ToString());
                        int iCvInc = Convert.ToInt32(renglon.Cells["CvInc"].Value.ToString());
                        string sUsuUmod = LoginInfo.IdTrab;
                        string sPrgUmod = this.Name;

                        iprespuesta=objActualizaIncidencia.ActualizaStatusInc(sClave, dtFechaReg, 9, iCvInc,0,0,0,0,DateTime.Now,0,0,DateTime.Now,0,0,0,"AG",
                            DateTime.Now,DateTime.Now,sUsuUmod,sPrgUmod);
                        switch (iprespuesta.ToString())
                        {
                            case "9":
                                lblMensaje.Text = "La Actualización se llevo a cabo correctamente";
                                break;
                            case "":
                                lblMensaje.Text = "Problemas al realizar la Operación, avise a Sistemas.";
                                break;
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.ToString());
                    }
                }
            }
        }

        private void btngeneralinc_Click(object sender, EventArgs e) //Reporte general de incidencias
        {
            if ((cbCompania.Text == "" | cbCompania.Text == "Seleccionar Compañia...") | (cbTiponomina.Text == "" | cbTiponomina.Text == "Seleccionar Tipo Nomina..."))
            {
                MessageBox.Show("Debe seleccionar la compañia y el tipo de nomina", "SIPPA", MessageBoxButtons.OK);
                cbCompania.Focus();

            }
            else if (dtpfechainicial.Text == "" | dtpfechafinal.Text == "")
            {
                MessageBox.Show("Proporcione un rango de fechas", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }
            else if (DateTime.Parse(dtpfechainicial.Text) > DateTime.Parse(dtpfechafinal.Text))
            {
                MessageBox.Show("Error en las Fechas, Verifique.", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }
            else
            {
                string idTrab = "%";
                string cvCia = "%";
                string cvUbicacion = "%";
                string sNomina = "%";

                if (cbEmpleados.Text != String.Empty && cbEmpleados.Text != "Seleccionar")
                {
                    idTrab = cbEmpleados.SelectedValue.ToString();
                }
                if (cbCompania.Text != "" & cbCompania.Text != "Seleccionar Compañia...")
                {
                    cvCia = cbCompania.SelectedValue.ToString();
                }
                if (Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) > 0 & cbUbicacion.Text != "Seleccionar...")
                {
                    cvUbicacion = cbUbicacion.SelectedValue.ToString();
                }
                if (cbTiponomina.Text != "" & cbTiponomina.Text != "Seleccionar Tipo Nomina...")
                {
                    sNomina = cbTiponomina.SelectedValue.ToString();
                }

                DataTable dtIncidencia = objIncidencia.ReporteIncidenciasPendientesAutorizar(2, idTrab, dtpfechainicial.Value.Date, dtpfechafinal.Value.Date, cvCia, sNomina, cvUbicacion);
                switch (dtIncidencia.Rows.Count)
                {
                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados para el Reporte", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtIncidencia, "SIPAA_CS.RecursosHumanos.Reportes", "GeneraldeIncidencias.rpt");
                        ReportDoc.SetParameterValue("FechaInicial", dtpfechainicial.Value.Date);
                        ReportDoc.SetParameterValue("FechaFinal", dtpfechafinal.Value.Date);
                        ReportDoc.SetParameterValue("NomCompania", cbCompania.SelectedValue.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;
                }
            }
        }

        private void btnpendientesaut_Click(object sender, EventArgs e)
        {
            if ((cbCompania.Text == "" | cbCompania.Text == "Seleccionar Compañia...") | (cbTiponomina.Text == "" | cbTiponomina.Text == "Seleccionar Tipo Nomina..."))
            {
                MessageBox.Show("Debe seleccionar la compañia y el tipo de nomina", "SIPPA", MessageBoxButtons.OK);
                cbCompania.Focus();

            }
            else if (dtpfechainicial.Text == "" | dtpfechafinal.Text == "")
            {
                MessageBox.Show("Proporcione un rango de fechas", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }
            else if (DateTime.Parse(dtpfechainicial.Text) > DateTime.Parse(dtpfechafinal.Text))
            {
                MessageBox.Show("Error en las Fechas, Verifique.", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }
            else
            {
                string idTrab = "%";
                string cvCia = "%";
                string cvUbicacion = "%";
                string sNomina = "%";
                if (cbEmpleados.Text != String.Empty && cbEmpleados.Text != "Seleccionar")
                {
                    idTrab = cbEmpleados.SelectedValue.ToString();
                }
                if (cbCompania.Text != "" & cbCompania.Text != "Seleccionar Compañia...")
                {
                    cvCia = cbCompania.SelectedValue.ToString();
                }
                if (Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) > 0 & cbUbicacion.Text != "Seleccionar...")
                {
                    cvUbicacion = cbUbicacion.SelectedValue.ToString();
                }
                if (cbTiponomina.Text != "" & cbTiponomina.Text != "Seleccionar Tipo Nomina...")
                {
                    sNomina = cbTiponomina.SelectedValue.ToString();
                }

                DataTable dtIncidencia = objIncidencia.ReporteIncidenciasPendientesAutorizar(1, idTrab, dtpfechainicial.Value.Date, dtpfechafinal.Value.Date, cvCia, sNomina, cvUbicacion);
                switch (dtIncidencia.Rows.Count)
                {
                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados para el Reporte", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtIncidencia, "SIPAA_CS.RecursosHumanos.Reportes", "IncidenciasPendientesAutorizar.rpt");
                        ReportDoc.SetParameterValue("FechaInicial", dtpfechainicial.Value.Date);
                        ReportDoc.SetParameterValue("FechaFinal", dtpfechafinal.Value.Date);
                        ReportDoc.SetParameterValue("NomCompania", cbCompania.SelectedValue.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;
                }
            }
        }

        private void btnfechashrreg_Click(object sender, EventArgs e)
        {
            if ((cbCompania.Text == "" | cbCompania.Text == "Seleccionar Compañia...") | (cbTiponomina.Text == "" | cbTiponomina.Text == "Seleccionar Tipo Nomina..."))
            {
                MessageBox.Show("Debe seleccionar la compañia y el tipo de nomina", "SIPPA", MessageBoxButtons.OK);
                cbCompania.Focus();

            }
            else if (dtpfechainicial.Text == "" | dtpfechafinal.Text == "")
            {
                MessageBox.Show("Proporcione un rango de fechas", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }
            else if (DateTime.Parse(dtpfechainicial.Text) > DateTime.Parse(dtpfechafinal.Text))
            {
                MessageBox.Show("Error en las Fechas, Verifique.", "SIPPA", MessageBoxButtons.OK);
                dtpfechainicial.Focus();
            }
            else
            {
                string idTrab = "%";
                string cvCia = "%";
                string cvUbicacion = "%";
                string sNomina = "%";

                if (cbEmpleados.Text != String.Empty && cbEmpleados.Text != "Seleccionar")
                {
                    idTrab = cbEmpleados.SelectedValue.ToString();
                }
                if (cbCompania.Text != "" & cbCompania.Text != "Seleccionar Compañia...")
                {
                    cvCia = cbCompania.SelectedValue.ToString();
                }
                if (Convert.ToInt32(cbUbicacion.SelectedIndex.ToString()) > 0 & cbUbicacion.Text != "Seleccionar...")
                {
                    cvUbicacion = cbUbicacion.SelectedValue.ToString();
                }
                if (cbTiponomina.Text != "" & cbTiponomina.Text != "Seleccionar Tipo Nomina...")
                {
                    sNomina = cbTiponomina.SelectedValue.ToString();
                }

                DataTable dtIncidencia = objIncidencia.ReporteFechasHorasRegistro(idTrab, dtpfechainicial.Value.Date, dtpfechafinal.Value.Date, cvCia, sNomina, cvUbicacion);
                switch (dtIncidencia.Rows.Count)
                {
                    case 0:
                        DialogResult result = MessageBox.Show("Consulta Sin Resultados para el Reporte", "SIPAA");
                        break;

                    default:
                        ViewerReporte form = new ViewerReporte();
                        ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtIncidencia, "SIPAA_CS.RecursosHumanos.Reportes", "FechasHorasRegistro.rpt");
                        ReportDoc.SetParameterValue("FechaInicial", dtpfechainicial.Value.Date);
                        ReportDoc.SetParameterValue("FechaFinal", dtpfechafinal.Value.Date);
                        ReportDoc.SetParameterValue("NomCompania", cbCompania.SelectedValue.ToString());
                        form.RptDoc = ReportDoc;
                        form.Show();
                        break;
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro, que desea abandonar la aplicación?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------
        private void ArchivoNomina4_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "ArchivoNomina4.cs")
                {
                    f.Hide();
                }
            }

            ftooltip();

            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            if (bprimeravez == true)
            {
                Utilerias.ResizeForm(this, Utilerias.PantallaSistema());

                //llenado de combo compañias
                util.cargarcombo(cbCompania, oCompañia.obtCompania2(5, ""));

                cbCompania.Text = "Seleccionar Compañia...";

                //llenado de combo ubicaciones
                Utilerias.llenarComboxDataTable(cbUbicacion, oUbicacion.obtenerSonaUbicacion("", 6), "Clave", "Descripción");
                bprimeravez = false;

                //llenado de combo Empleados
                DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
                Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            timer1.Stop();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void fgridarchivonomina4(int ipopcion, int ipidtrab, int ipidcompania, int ipidtiponomina, int ipidubicacion,
            string spfecinicio, string spfecfin, string spusuumod, string spprgumod)
        {
            DataTable dtarchivonomina4 = oArchivoNomina4.ObtenerArchivoNomina4(ipopcion, ipidtrab, ipidcompania,
                ipidtiponomina, ipidubicacion, spfecinicio, spfecfin, spusuumod, spprgumod);
            dgvArchivoNomina4.DataSource = dtarchivonomina4;
            if (dtarchivonomina4.Rows.Count==0)
            {
                DialogResult result = MessageBox.Show("NO hay información que coincida con los filtros de búsqueda, NO ES NECESARIO GENERAR EL ARCHIVO CSV.", "SIPAA");
            }
            else
            {
                dgvArchivoNomina4.Columns[0].Width = 60;
                dgvArchivoNomina4.Columns[4].Width = 20;
                dgvArchivoNomina4.Columns[5].Width = 20;
                dgvArchivoNomina4.Columns[6].Width = 40;
                dgvArchivoNomina4.Columns[7].Width = 80;
                dgvArchivoNomina4.Columns[8].Width = 25;
                dgvArchivoNomina4.Columns[9].Width = 20;
                dgvArchivoNomina4.Columns[10].Width = 20;
                dgvArchivoNomina4.Columns[12].Width = 20;
                dgvArchivoNomina4.Columns[13].Width = 20;
                dgvArchivoNomina4.Columns[14].Width = 20;
                dgvArchivoNomina4.Columns[15].Width = 20;
                dgvArchivoNomina4.Columns[16].Width = 20;
                dgvArchivoNomina4.Columns[17].Width = 30;
                dgvArchivoNomina4.Columns[18].Width = 80;
            }
        }

        private void creacsv()
        {
            saveFileDialogArchivo.Filter = "csv files (*.csv)|*.csv";

            if (saveFileDialogArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK
                && saveFileDialogArchivo.FileName.Length > 0)
            {
                int creado = 0;

                try
                {
                    FileInfo archt = new FileInfo(saveFileDialogArchivo.FileName);
                    StreamWriter Texto = archt.CreateText();
                    //variable entera que llevara el control del numero de renglones que contiene el DataGridView//
                    //Es un ciclo con un numero de iteraciones que variara dependiendo del numero de columnas del Grid
                    int ren = 0;
                    //Encabezados
                    string cadenaRegEnc = "NoEmpleado,Nombre,Paterno,Materno,TipoNomina,TipoHora,FechaReg,CvInc,Inc,StDir,Punt,Asist,Mint,Docente,StInc";
                    Texto.WriteLine(cadenaRegEnc);
                    foreach (DataGridViewRow row in dgvArchivoNomina4.Rows)
                    {
                        string cadenaReg = "";
                        int tcol=Convert.ToInt32(row.Cells.Count.ToString());
                        for (int col = 0; col < tcol; col++)
                        {
                            if (col==0)
                            {
                                cadenaReg = dgvArchivoNomina4.Rows[ren].Cells[col].Value.ToString() + ",";
                            }
                            else
                            {
                                cadenaReg = cadenaReg + dgvArchivoNomina4.Rows[ren].Cells[col].Value.ToString() + ",";
                                if (col==tcol-1)
                                {
                                    cadenaReg = cadenaReg + dgvArchivoNomina4.Rows[ren].Cells[col].Value.ToString();
                                }
                            }
                        }
                        Texto.WriteLine(cadenaReg);
                        ren += 1;
                    }
                    Texto.Write(Texto.NewLine);
                    Texto.Close();
                }
                catch (Exception ex)
                {
                    creado = 1;
                }
                if (creado == 0)
                    MessageBox.Show("El Archivo " + saveFileDialogArchivo.FileName + " ha sido creado");
                else
                    MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");
            }
        }

        private void creacsvcorto()
        {
            saveFileDialogArchivo.Filter = "csv files (*.csv)|*.csv";

            if (saveFileDialogArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK
                && saveFileDialogArchivo.FileName.Length > 0)
            {
                int creado = 0;

                try
                {
                    FileInfo archt = new FileInfo(saveFileDialogArchivo.FileName);
                    StreamWriter Texto = archt.CreateText();
                    //variable entera que llevara el control del numero de renglones que contiene el DataGridView//
                    //Es un ciclo con un numero de iteraciones que variara dependiendo del numero de columnas del Grid
                    //Encabezados
                    //string cadenaRegEnc = "AnoNomina,NoNomina,NoEmpleado,ClaveAfecta,Tiempo,FechaReg,Constante,Conteo";
                    //Texto.WriteLine(cadenaRegEnc);

                    int ren = 0;
                    string anonomina="", fechareg="";
                    int nonomina=0, noempleado=0, claveafecta=0, duro=0, conteo=1;
                    double tiempo = 0;
                    double TH = 0, TF = 0, TR = 0;
                    string cadenaReg = "";
                    bool divide =true;
                    int tiponomina = 0;
                    bool docente = false;

                    foreach (DataGridViewRow row in dgvArchivoNomina4.Rows)
                    {                         
                        if (ren==0)
                        {
                            anonomina = txtanonom.Text;
                            nonomina = Convert.ToInt32(txtnumnom.Text);                           
                            tiponomina = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[20].Value.ToString());
                            if (tiponomina == 11 | tiponomina ==13 | tiponomina == 14 | tiponomina == 15)
                            {
                                docente = true;
                            }
                            noempleado = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[0].Value.ToString());
                            claveafecta = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[17].Value.ToString()); //15 mas 2

                            if (dgvArchivoNomina4.Rows[ren].Cells[10].Value.ToString()=="1") //cvrepresenta es Falta 
                            {
                                if (dgvArchivoNomina4.Rows[ren].Cells[5].Value.ToString()=="1") //y es turno por dia
                                {
                                    tiempo = 1;
                                    divide = false;
                                }
                                else ////es turno por hora, es profesor
                                {
                                    if (docente)
                                    {
                                        tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[16].Value.ToString());
                                    }
                                    else
                                    {
                                        //tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                                        tiempo = 1;
                                        divide = false;
                                    }                         

                                }
                            }
                            else 
                            {
                                if (docente)
                                {
                                    tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[16].Value.ToString());
                                }
                                else
                                {
                                    tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                                }                                
                                //antes del cambio a 2 columnas tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());                                
                            }
                            fechareg = dgvArchivoNomina4.Rows[ren].Cells[7].Value.ToString();
                            fechareg = fechareg.Substring(0,10);
                            duro = 0;
                            conteo = 1;
                        }
                        else
                        {
                            if (noempleado == Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[0].Value.ToString()) & 
                                claveafecta == Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[17].Value.ToString()))
                            {
                                conteo = conteo + 1;
                                if (dgvArchivoNomina4.Rows[ren].Cells[10].Value.ToString() == "1") //cvrepresenta es Falta
                                {
                                    if (dgvArchivoNomina4.Rows[ren].Cells[5].Value.ToString() == "1") //y es turno por dia
                                    {
                                        tiempo = tiempo + 1;
                                        divide = false;
                                    }
                                    else ////es turno por hora, es profesor
                                    {
                                        if (docente)
                                        {
                                            tiempo = tiempo + Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[16].Value.ToString());
                                        }
                                        else
                                        {
                                            //tiempo = tiempo + Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                                            tiempo = 1;
                                            divide = false;
                                        }                                        
                                    }
                                }
                                else 
                                {
                                    if (docente)
                                    {
                                        tiempo = tiempo + Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[16].Value.ToString());
                                    }
                                    else
                                    {
                                        tiempo = tiempo + Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                                    }
                                }
                            }
                            else
                            {
                                if (tiempo >= 60)
                                {                                   
                                    TH = tiempo / 60;
                                    //int THR = Convert.ToInt32(Math.Round(TH));
                                    int THR = Convert.ToInt32(Math.Truncate(TH));
                                    TF = tiempo % 60;
                                    int TFR = Convert.ToInt32(Math.Round(TF));
                                    TF = TF / 100;
                                    TR = THR + TF;
                                    tiempo = TR;
                                }
                                else
                                {
                                    if (divide)
                                    {
                                        tiempo = tiempo / 100;
                                    }
                                    else
                                    {
                                        divide = true;
                                    }
                                }
                                cadenaReg = anonomina+","+nonomina+","+noempleado+","+claveafecta+","+tiempo+","+fechareg+","+duro+","+conteo;
                                Texto.WriteLine(cadenaReg);
                                anonomina = txtanonom.Text;
                                nonomina = Convert.ToInt32(txtnumnom.Text);
                                noempleado = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[0].Value.ToString());
                                claveafecta = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[17].Value.ToString());
                                if (dgvArchivoNomina4.Rows[ren].Cells[10].Value.ToString() == "1") //cvrepresenta es Falta
                                {
                                    if (dgvArchivoNomina4.Rows[ren].Cells[5].Value.ToString() == "1") //y es turno por dia
                                    {
                                        tiempo = 1;
                                        divide = false;
                                    }
                                    else ////es turno por hora, es profesor
                                    {
                                        if (docente)
                                        {
                                            tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[16].Value.ToString());
                                        }
                                        else
                                        {
                                            tiempo = 1;
                                            divide = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (docente)
                                    {
                                        tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[16].Value.ToString());
                                    }
                                    else
                                    {
                                        tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                                    }
                                }
                                fechareg = dgvArchivoNomina4.Rows[ren].Cells[7].Value.ToString();
                                fechareg = fechareg.Substring(0,10);
                                duro = 0;
                                conteo = 1;
                            }
                        }
                        ren += 1;
                    }
                    if (tiempo >= 60)
                    {
                        TH = tiempo / 60;
                        //int THR = Convert.ToInt32(Math.Round(TH));
                        int THR = Convert.ToInt32(Math.Truncate(TH));
                        TF = tiempo % 60;
                        int TFR = Convert.ToInt32(Math.Round(TF));
                        TF = TF / 100;
                        TR = THR + TF;
                        tiempo = TR;
                    }
                    else
                    {
                        if (divide)
                        {
                            tiempo = tiempo / 100;
                        }
                        else
                        {
                            divide = true;
                        }
                    }
                    cadenaReg = anonomina + "," + nonomina + "," + noempleado + "," + claveafecta + "," + tiempo + "," + fechareg + "," + duro + "," + conteo;
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);
                    Texto.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    creado = 1;
                }
                if (creado == 0)
                {
                    lblMensaje.Text = "El Archivo " + saveFileDialogArchivo.FileName + " ha sido creado";
                    panelTag.Visible = true;
                    timer1.Start();
                    //MessageBox.Show("El Archivo " + saveFileDialogArchivo.FileName + " ha sido creado");
                }
                else
                {
                    lblMensaje.Text = "No se pudo crear el archivo. Intente de Nuevo";
                    panelTag.Visible = true;
                    timer1.Start();
                    //MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");                    
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
        }

        private void ftooltip()
        {
            //crea tool tip
            ToolTip toolTip1 = new ToolTip();

            //configuracion
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            //configura texto del objeto
            toolTip1.SetToolTip(this.btnCerrar, "Cierrar Sistema");
            toolTip1.SetToolTip(this.btnMinimizar, "Minimizar Sistema");
            toolTip1.SetToolTip(this.btnRegresar, "Regresar");
            toolTip1.SetToolTip(this.btnBuscar, "Buscar Registros");
            toolTip1.SetToolTip(this.btnpendientesaut, "Lanzar Reporte");
            toolTip1.SetToolTip(this.btnfechashrreg, "Lanzar Reporte");
        }

        private void frecargar()
        {
            ArchivoNomina4 recargar = new ArchivoNomina4();
            recargar.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------
    }
}
