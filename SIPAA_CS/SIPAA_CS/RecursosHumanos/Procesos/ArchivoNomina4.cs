using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
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

            }
            //txtcolumnas.Text = dgvArchivoNomina4.ColumnCount.ToString();
        }

        private void btngenerararchivo_Click(object sender, EventArgs e)
        {
            creacsv();
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
                util.cargarcombo(cbUbicacion, oUbicacion.obtenerSonaUbicacion("",4));
                //DataTable dtUbicacion = oUbicacion.obtenerubicaciones(4, "");
                //cbUbicacion.DataSource = dtUbicacion;
                //cbUbicacion.DisplayMember = "Descripción";
                //cbUbicacion.ValueMember = "Clave";
                cbUbicacion.Text = "Seleccionar Ubicación...";
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

        //private void creacsv()
        //{
        //    saveFileDialogArchivo.Filter = "csv files (*.csv)|*.csv";

        //    if (saveFileDialogArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK
        //        && saveFileDialogArchivo.FileName.Length > 0)
        //    {
        //        int creado = 0;
        //        try
        //        {
        //            FileInfo archt = new FileInfo(saveFileDialogArchivo.FileName);
        //            StreamWriter Texto = archt.CreateText();
        //            //variable entera que llevara el control del numero de renglones que contiene el DataGridView//
        //            //Es un ciclo con un numero de iteraciones que variara dependiendo del numero de columnas del Grid
        //            int n = 0;
        //            foreach (DataGridViewRow row in dgvArchivoNomina4.Rows)
        //            {
        //                int tcol = Convert.ToInt32(row.Cells.Count.ToString());

        //                string cadenaReg = dgvArchivoNomina4.Rows[n].Cells[0].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[1].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[2].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[3].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[4].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[5].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[6].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[7].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[8].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[9].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[10].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[11].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[12].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[13].Value.ToString() + "," +
        //                    dgvArchivoNomina4.Rows[n].Cells[14].Value.ToString();
        //                Texto.WriteLine(cadenaReg);
        //                n += 1;
        //            }
        //            Texto.Write(Texto.NewLine);
        //            Texto.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            creado = 1;
        //        }
        //        if (creado == 0)
        //            MessageBox.Show("El Archivo " + saveFileDialogArchivo.FileName + " ha sido creado");
        //        else
        //            MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");
        //    }
        //}
        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E S
        //-----------------------------------------------------------------------------------------------

    }
}
