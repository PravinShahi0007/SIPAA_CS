using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.Accesos.Reportes;
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

        public ArchivoNomina4()
        {
            InitializeComponent();
        }

        //para validar inicio de combo
        bool bprimeravez = true;

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
            }
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string ubica = cbUbicacion.SelectedValue.ToString();
            if ((cbCompania.Text=="" | cbCompania.Text == "Seleccionar Compañia...") | (cbTiponomina.Text=="" | cbTiponomina.Text == "Seleccionar Tipo Nomina..."))
            {
                MessageBox.Show("Debe seleccionar la compañia y el tipo de nomina", "SIPPA");
                cbCompania.Focus();
                
            }
            else if (dtpfechainicial.Text =="" | dtpfechafinal.Text == "")
            {
                MessageBox.Show("Proporcione un rango de fechas", "SIPPA");
                dtpfechainicial.Focus();
            }

            else if (txtidtrab.Text.Trim()=="" & (cbUbicacion.SelectedValue.ToString()=="" | cbUbicacion.Text == "Seleccionar Ubicación..."))
            {
                fgridarchivonomina4(4, 0, Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), 0,
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), "JLA", "ProcArchNom");

            }
            else if (txtidtrab.Text.Trim()=="" & (cbUbicacion.SelectedValue.ToString() != "" | cbUbicacion.Text == "Seleccionar Ubicación..."))
            {
                fgridarchivonomina4(5, 0, Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), Convert.ToInt32(cbUbicacion.SelectedValue.ToString()),
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), "JLA", "ProcArchNom");
            }
            else if (txtidtrab.Text.Trim() != "" & (cbUbicacion.SelectedValue.ToString() == "" | cbUbicacion.Text == "Seleccionar Ubicación..."))
            {
                fgridarchivonomina4(6, Convert.ToInt32(txtidtrab.Text.Trim()), Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()),0,
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), "JLA", "ProcArchNom");
            }
            else
            {
                fgridarchivonomina4(7, Convert.ToInt32(txtidtrab.Text.Trim()), Convert.ToInt32(cbCompania.SelectedValue.ToString()),
                Convert.ToInt32(cbTiponomina.SelectedValue.ToString()), 0,
                dtpfechainicial.Text.Trim(), dtpfechafinal.Text.Trim(), "JLA", "ProcArchNom");
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
                string idTrab = "%";
                string cvCia = "%";
                string cvUbicacion = "%";
                string sNomina = "%";
                if (txtidtrab.Text != String.Empty) { idTrab = txtidtrab.Text; }

                if (cbCompania.SelectedIndex > 0) { cvCia = cbCompania.SelectedValue.ToString(); }
                if (cbUbicacion.SelectedIndex > 0) { cvUbicacion = cbUbicacion.SelectedValue.ToString(); }
                if (cbTiponomina.SelectedIndex > 0) { sNomina = cbTiponomina.SelectedValue.ToString(); }
                ////Prueba Reporte Incidencias pasadas a Nomina
                Incidencia objIncidencia = new Incidencia();
                DataTable dtIncidencia = objIncidencia.ReporteIncidenciasPasadasNomina(idTrab, dtpfechainicial.Value, dtpfechainicial.Value,cvCia,sNomina, cvUbicacion);

                ViewerReporte form = new ViewerReporte();
                IncidenciasPasadasNomina rptIncidencia = new IncidenciasPasadasNomina();
                ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtIncidencia, "RecursosHumanos", "IncidenciasPasadasNomina");

                ReportDoc.SetParameterValue("FechaActual", DateTime.Now.ToString("dd/MM/yyyy"));
                form.RptDoc = ReportDoc;
                form.Show();

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
            if (bprimeravez == true)
            {
                //llenado de combo compañias
                util.cargarcombo(cbCompania, oCompañia.obtCompania2(5, ""));
                //DataTable dtCompañia = oCompañia.obtCompania2(5, "");
                //cbCompania.DataSource = dtCompañia;
                //cbCompania.DisplayMember = "Descripción";
                //cbCompania.ValueMember = "Clave";
                cbCompania.Text = "Seleccionar Compañia...";

                //llenado de combo ubicaciones
                //util.cargarcombo(cbUbicacion, oUbicacion.obtenerSonaUbicacion("",6));
                Utilerias.llenarComboxDataTable(cbUbicacion, oUbicacion.obtenerSonaUbicacion("", 6), "Clave", "Descripción");
                bprimeravez = false;
            }
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
            Application.Restart();
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
                    string cadenaRegEnc = "AnoNomina,NoNomina,NoEmpleado,ClaveAfecta,Tiempo,FechaReg,Constante,Conteo";
                    Texto.WriteLine(cadenaRegEnc);

                    int ren = 0;
                    string anonomina="", fechareg="";
                    int nonomina=0, noempleado=0, claveafecta=0, duro=0, conteo=1;
                    double tiempo = 0;
                    string cadenaReg = "";

                    foreach (DataGridViewRow row in dgvArchivoNomina4.Rows)
                    {
                        //string cadenaReg = "";
                        if (ren==0)
                        {
                            anonomina = txtanonom.Text;
                            nonomina = Convert.ToInt32(txtnumnom.Text);
                            noempleado = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[0].Value.ToString());
                            claveafecta = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                            if (dgvArchivoNomina4.Rows[ren].Cells[9].Value.ToString()=="1") //cvrepresenta es Falta
                            {
                                if (dgvArchivoNomina4.Rows[ren].Cells[5].Value.ToString()=="1") //y es turno por dia
                                {
                                    tiempo = 1;
                                }
                            }
                            else
                            {
                                tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[13].Value.ToString());
                            }
                            fechareg = dgvArchivoNomina4.Rows[ren].Cells[7].Value.ToString();
                            duro = 0;
                            conteo = 1;
                        }
                        else
                        {
                            if (noempleado == Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[0].Value.ToString()) & 
                                claveafecta == Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString()))
                            {
                                conteo = conteo + 1;
                                if (dgvArchivoNomina4.Rows[ren].Cells[9].Value.ToString() == "1") //cvrepresenta es Falta
                                {
                                    if (dgvArchivoNomina4.Rows[ren].Cells[5].Value.ToString() == "1") //y es turno por dia
                                    {
                                        tiempo = tiempo + 1;
                                    }
                                }
                                else
                                {
                                    tiempo = tiempo + Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[13].Value.ToString());
                                }
                            }
                            else
                            {
                                cadenaReg = anonomina+","+nonomina+","+noempleado+","+claveafecta+","+tiempo+","+fechareg+","+duro+","+conteo;
                                Texto.WriteLine(cadenaReg);
                                //ren += 1;
                                anonomina = txtanonom.Text;
                                nonomina = Convert.ToInt32(txtnumnom.Text);
                                noempleado = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[0].Value.ToString());
                                claveafecta = Convert.ToInt32(dgvArchivoNomina4.Rows[ren].Cells[15].Value.ToString());
                                if (dgvArchivoNomina4.Rows[ren].Cells[9].Value.ToString() == "1") //cvrepresenta es Falta
                                {
                                    if (dgvArchivoNomina4.Rows[ren].Cells[5].Value.ToString() == "1") //y es turno por dia
                                    {
                                        tiempo = 1;
                                    }
                                }
                                else
                                {
                                    tiempo = Convert.ToDouble(dgvArchivoNomina4.Rows[ren].Cells[13].Value.ToString());
                                }
                                fechareg = dgvArchivoNomina4.Rows[ren].Cells[7].Value.ToString();
                                duro = 0;
                                conteo = 1;
                            }
                        }
                        ren += 1;
                    }
                    cadenaReg = anonomina + "," + nonomina + "," + noempleado + "," + claveafecta + "," + tiempo + "," + fechareg + "," + duro + "," + conteo;
                    Texto.WriteLine(cadenaReg);
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
            Application.Restart();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    }
}
