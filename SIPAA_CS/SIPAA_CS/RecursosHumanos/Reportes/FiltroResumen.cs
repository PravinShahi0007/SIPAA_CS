using CrystalDecisions.CrystalReports.Engine;
using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.Usuario;




namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltroResumen : Form
    {
        int sysH = SystemInformation.PrimaryMonitorSize.Height;
        int sysW = SystemInformation.PrimaryMonitorSize.Width;
        SonaTrabajador contenedorempleados = new SonaTrabajador();
       

        public FiltroResumen()
        {
            InitializeComponent();
        }
        ConcepInc ConceptoIncidencias = new ConcepInc();
        Incidencia TipoIncidencias = new Incidencia();

        //***********************************************************************************************
        //Autor: Victor Jesús Iturburu Vergara
        //Fecha creación:04-04-2017     Última Modificacion: 22-09-2017 Rafael Lemus
        //Descripción: -------------------------------
        //***********************************************************************************************



        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        public void llenarCombo(ComboBox cb, DataTable dt, string sValor)
        {

            List<string> ltvalores = new List<string>();
            foreach (DataRow row in dt.Rows)
            {

                ltvalores.Add(row[sValor].ToString());
            }

            ltvalores.Insert(0, "Seleccionar");

            cb.DataSource = ltvalores;
        }

        private void cbCia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCia.SelectedIndex != 0)
            {

                cbTipoNomina.Enabled = true;
                SonaTipoNomina objTnom = new SonaTipoNomina();
              
                DataTable dtTnom = objTnom.obtTipoNomina(5, cbCia.SelectedIndex, 0, "");
                llenarCombo(cbTipoNomina, dtTnom, "Descripción"); // quite el acento

                //cbArea.Enabled = true;
                SonaCompania objCia = new SonaCompania();
                DataTable dtPlanta = objCia.ObtenerPlantel(4, 0, cbCia.SelectedItem.ToString(), "");
                //llenarCombo(cbArea, dtPlanta, "Descripción"); // quite el acento

                cbArea.Enabled = true;
              
                DataTable dtArea = objCia.ObtenerPlantel(4, 0, cbCia.SelectedItem.ToString(), "");
                llenarCombo(cbArea, dtArea, "Descripción"); // quite el acento



            }
            else
            {
                cbTipoNomina.Enabled = false;
                //cbArea.Enabled = false;
            }


        }


        //-----------------------------------------------------------------------------------------------
        //                                      G R I D // S
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        //                                     B O T O N E S
        //-----------------------------------------------------------------------------------------------

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            RechDashboard rechdb = new RechDashboard();
            rechdb.Show();
            this.Close();
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

        private void btnImprimirResumen_Click(object sender, EventArgs e)
        {
          
            DateTime dtFechaInicio = dpFechaInicio.Value;
            DateTime dtFechaFin = dpFechaFin.Value;
            string sCia = AsignarVariableCombo(cbCia);
            string sArea = AsignarVariableCombo(cbArea);
            string sUbicacion = AsignarVariableCombo(cbUbicacion);
            string sTipoNom = AsignarVariableCombo(cbTipoNomina);
            string sDepto = AsignarVariableCombo(cbDepartamento);

            string sIdtrab = "";
            if (cbEmpleados.Text == String.Empty)
               sIdtrab = "%";
            else
              sIdtrab = cbEmpleados.SelectedValue.ToString();
            
            Incidencia objInc = new Incidencia();

            if (sIdtrab == "0")
                sIdtrab = "%";

            DataTable dtRpt = objInc.ReporteResumen(sIdtrab, dtFechaInicio, dtFechaFin, sDepto, sCia, sTipoNom, sUbicacion, sArea);

            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                    ViewerReporte form = new ViewerReporte();
                    Resumen dtrpt = new Resumen();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, this.CompanyName, dtrpt.ResourceName);

                    ReportDoc.SetParameterValue("TotalRegistros", dtRpt.Rows.Count.ToString());
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaTermino", dpFechaFin.Value);
                    ReportDoc.SetParameterValue("Comp", sCia);
                    ReportDoc.SetParameterValue("Ubicacion", sUbicacion);
                   // ReportDoc.SetParameterValue("Area", sArea);
                    ReportDoc.SetParameterValue("TipoNomina", sTipoNom);
                    form.RptDoc = ReportDoc;
                    form.Show();

                    DialogResult Resultado = MessageBox.Show("¿Desea crear el archivo en formato .csv para abrirlo con excel?", "SIPAA", MessageBoxButtons.YesNo);
                    if (Resultado==DialogResult.Yes)
                        creacsv(dtRpt);


                    break;

            }

        }



     

        //-----------------------------------------------------------------------------------------------
        //                           C A J A S      D E      T E X T O   
        //-----------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------
        //                                     E V E N T O S
        //-----------------------------------------------------------------------------------------------


        private void FiltroResumen_Load(object sender, EventArgs e)
        {
            //cierra formularios abiertos
            FormCollection formulariosApp = Application.OpenForms;
            foreach (Form f in formulariosApp)
            {
                if (f.Name != "FiltroResumen.cs")
                {
                    f.Hide();
                }
            }
   
            //llena etiqueta de usuario
            lblusuario.Text = LoginInfo.Nombre;
            Utilerias.cargaimagen(ptbimgusuario);

            Utilerias.ResizeForm(this, new Size(new Point(sysH, sysW)));
            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(5, "%");
            llenarCombo(cbCia, dtCia, "Descripción");
            llenarCombo(comboBox4, dtCia, "Descripción");
            llenarCombo(cbCompañia2, dtCia, "Descripción"); 

            DataTable dtUbicaciones = objCia.ObtenerUbicacionPlantel(5, "%");
            llenarCombo(cbUbicacion, dtUbicaciones, "Descripción");
            llenarCombo(comboBox3, dtUbicaciones, "Descripción");
            llenarCombo(cbUbicacion2, dtUbicaciones, "Descripción");

            SonaDepartamento objDepto = new SonaDepartamento();
            DataTable dtDepto = objDepto.obtdepto(5, "%");
            llenarCombo(cbDepartamento, dtDepto, "Descripción");
            llenarCombo(comboBox2, dtDepto, "Descripción");
            llenarCombo(cbDepartamento2, dtDepto, "Descripción");
         //cbNombre2
            cbTipoNomina.Enabled = false;
         
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
            Utilerias.llenarComboxDataTable(cbNombre2, dtempleados, "NoEmpleado", "Nombre");
            //llenarCombo(cbNombre2, dtDepto, "NoEmpleado");

            cbEmpleados.Focus();
      
            CbConceptoIncidencia(8, 0, "", 0, 0, 0, 0, "", "");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //panelTag.Visible = false;
            //timer1.Stop();
        }


        //-----------------------------------------------------------------------------------------------
        //                                      F U N C I O N E S 
        //-----------------------------------------------------------------------------------------------

        private void ValidarFechaDataPicker(object sender, EventArgs e)
        {
            if (dpFechaInicio.Value > dpFechaFin.Value)
            {

               // Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La fecha de Inicio no puede ser MAYOR a la de Término");

                timer1.Start();

                //  dpFechaFin.Value = dpFechaInicio.Value;
                btnImprimirResumen.Enabled = false;

            }
            else
            {
                btnImprimirResumen.Enabled = true;

            }


        }


        public string AsignarVariableCombo(ComboBox cb)
        {

            string sAsignacion;

            if (cb.SelectedIndex == 0)
                sAsignacion = "%";
            
            

                //if(cb.SelectedItem.ToString() == "System.Data.DataRowView")
                //sAsignacion = cb.ite
            else
              sAsignacion = cb.SelectedItem.ToString();
          
               
                 

          

            return sAsignacion;

        }



        private void creacsv(DataTable dtRpt)
        {

            


            ///////////////////////////////////////////////////////////


            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                bool bandera = false; 

                try
                {
                   // FileInfo archt = new FileInfo(saveFileDialog.FileName);
                    StreamWriter Texto  = new StreamWriter(saveFileDialog.FileName, false,Encoding.UTF8);
                    //archt.CreateText();
                    
                    string cadenaReg = "";
                    cadenaReg = "Reporte Anual del periodo "+dpFechaInicio.Value.ToString("dd/MM/yy")+" al "+dpFechaFin.Value.ToString("dd/MM/yy");
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);
                    cadenaReg = "IdTrab, Nombre,  Incapacidad , Permiso_Sin_Goce_De_Sueldo , Minutos_Permiso_Sin_Goce_De_Sueldo , Sin_Registros ,"+
                        " Minutos_Sin_Registros, Falta_Autorizada , Minutos_Falta_Autorizada ,Omisión_Entrada, Minutos_Omision_Entrada,  Omisión_Entrada_Autorizada,"+
                        " Minutos_Omision_Entrada_Autorizada ,Omisión_Salida, Minutos_Omision_Salida, Omisión_Salida_Autorizada, Minutos_Omision_Salida_Autorizada ,"+
                        " Retardo, Minutos_Retardo, Retardo_Autorizado , Minutos_Retardo_Autorizado ,Salida_Anticipada," +
                        "Minutos_Salida_Anticipada,  Salida_Anticipada_Autorizada , Minutos_Salida_Anticipada_Autorizada ,Retardo_Comida "+
                        "  ,Minutos_Retardo_Comida,   Retardo_Comida_Autorizado, Minutos_Retardo_Comida_Autorizado ,  Suspension,  IdDirector, Director, "+
                        "  Id_Supervisor, Supervisor, Departamento, Tipo_Nómina, Ubicación, Compañia";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    foreach(DataRow row in dtRpt.Rows)
                    {
                        cadenaReg = row[0].ToString() + "," + row[1].ToString() + ","+row[2].ToString()+","+row[3].ToString()+","+row[4].ToString()+
                            ","+row[5].ToString()+","+row[6].ToString()+","+row[7].ToString()+","+row[8].ToString()+","+row[9].ToString()+
                            ","+row[10].ToString()+","+row[11].ToString()+","+row[12].ToString()+","+row[13].ToString()+","+row[14].ToString()+
                            ","+row[15].ToString()+","+row[16].ToString()+","+row[17].ToString()+","+row[18].ToString()+","+row[19].ToString()+","+
                            row[20].ToString()+","+row[21].ToString() + "," + row[22].ToString() + "," + row[23].ToString() + "," + row[24].ToString() + "," + 
                            row[25].ToString() + "," + row[26].ToString() + "," + row[27].ToString() + "," + row[28].ToString() + "," + row[29].ToString() + "," 
                            + row[30].ToString() + "," + row[31].ToString() + "," + row[32].ToString() + "," + row[33].ToString() + "," + row[34].ToString() + "," 
                            + row[35].ToString() + "," + row[36].ToString() + "," + row[37].ToString();

                        Texto.WriteLine(cadenaReg);
                    }
                    
                    Texto.Close();
                }
                catch 
                {
                    
                    bandera = true; 
                }
                if (!bandera)
                     MessageBox.Show("El archivo " + saveFileDialog.FileName + "ha sido creado Correctamente, ahora puede abrirlo con excel");
                 else
                     MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");
                  
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Incidencia objInc = new Incidencia();
            DateTime dtFechaInicio = dateTimePicker1.Value;
            DateTime dtFechaFin = dateTimePicker2.Value;
            string idCompañia, idUbicacion, idPlanta, idDepto, Incidencia, Tipo;

            idCompañia = AsignarVariableCombo(comboBox4);
            idUbicacion = AsignarVariableCombo(comboBox3);
            idPlanta = AsignarVariableCombo(comboBox1);
            idDepto = AsignarVariableCombo(comboBox2);
            Incidencia = AsignarVariableCombo(cbConcepto);
            Tipo = AsignarVariableCombo(cbTipo);


            //idCompañia = comboBox4.SelectedValue.ToString();
            //idUbicacion = comboBox3.SelectedValue.ToString();
            //idPlanta = comboBox1.SelectedValue.ToString();
            //idDepto = comboBox2.SelectedValue.ToString();
            //Incidencia = cbConcepto.SelectedValue.ToString();
            //Tipo = cbTipo.SelectedValue.ToString();

            //if (idUbicacion == "" || idUbicacion == "Seleccionar")
            //    idUbicacion = "%";
            //if (idCompañia == "" || idCompañia == "Seleccionar")
            //    idCompañia = "%";
            //if (idPlanta == "" || idPlanta == "Seleccionar")
            //    idPlanta = "%";
            //if (idDepto == "" || idDepto == "Seleccionar")
            //    idDepto = "%";

            //if (Incidencia == ""||Incidencia=="Seleccionar")
            //    Incidencia = "%";
            //if (Tipo == ""|| Tipo== "Seleccionar" )
            //    Tipo = "%";


            DataTable dtRpt = objInc.ReporteConceptos (idCompañia, idUbicacion, idPlanta, idDepto, dtFechaInicio, dtFechaFin, Incidencia, Tipo);
            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:

                    ViewerReporte form = new ViewerReporte();
                    Conceptos dtrpt = new Conceptos();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, this.CompanyName, dtrpt.ResourceName);
                    ReportDoc.SetParameterValue("FechaInicio", dateTimePicker1.Value);
                    ReportDoc.SetParameterValue("FechaFin", dateTimePicker2.Value);
                    form.RptDoc = ReportDoc;
                    form.Show();

                    // crear CSV
                    DialogResult Resultado = MessageBox.Show("¿Desea crear el archivo en formato .csv para abrirlo con excel?", "SIPAA", MessageBoxButtons.YesNo);
                    if (Resultado == DialogResult.Yes)
                        creacsv3(dtRpt);
                    
                    break;

            }


        }

        private void cbConcepto_DropDown(object sender, EventArgs e)
        {
            cbTipo.Text = string.Empty;
            
        }

        private void CbConceptoIncidencia(int p_opcion, int p_cvIncidencia, string p_descripcion, int p_orden, int p_stgenera, int p_strepresenta, int p_stincidencia, string p_usuumod, string p_prgumodr)
        {
            DataTable dtIncidencia = ConceptoIncidencias.ConcepInc_S(p_opcion, p_cvIncidencia, p_descripcion, p_orden, p_stgenera, p_strepresenta, p_stincidencia, p_usuumod, p_prgumodr);
             llenarCombo(cbConcepto, dtIncidencia, "Descripcion");
            llenarCombo(cbConcepto2, dtIncidencia, "Descripcion");

            //cbConcepto.DataSource = dtIncidencia;
            // cbConcepto.DisplayMember = "Descripcion";
            //cbConcepto.ValueMember = "Clave";
        }

        private void cbTipo_DropDown(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(cbConcepto.Text))
            //{
            //    Incidencia objIncidencia = new Incidencia();
            //    objIncidencia.CVIncidencia = Int32.Parse(cbConcepto.SelectedValue.ToString());
            //    objIncidencia.Descripcion = "";
            //    objIncidencia.CVTipo = 0;
            //    objIncidencia.TipoIncidencia = "";
            //    objIncidencia.Estatus = "";
            //    objIncidencia.UsuuMod = "";
            //    objIncidencia.PrguMod = "";
            //    objIncidencia.Estatus = "";
            //    int Opcion = 8;

            //    DataTable dtIncidencia = TipoIncidencias.ObtenerIncidenciaxTipo(objIncidencia, Opcion);
            //    cbTipo.DataSource = dtIncidencia;
            //    cbTipo.DisplayMember = "Tipo";
            //    cbTipo.ValueMember = "cvtipo";
              
            //    cbTipo.Text = "Seleccionar";
            //}
        }

        private void cbConcepto_DropDownClosed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbConcepto.Text))
            {
                Incidencia objIncidencia = new Incidencia();
                objIncidencia.CVIncidencia = 0; 
                objIncidencia.Descripcion = AsignarVariableCombo(cbConcepto);
                objIncidencia.CVTipo = 0;
                objIncidencia.TipoIncidencia = "";
                objIncidencia.Estatus = "";
                objIncidencia.UsuuMod = "";
                objIncidencia.PrguMod = "";
                objIncidencia.Estatus = "";
                int Opcion = 9;

                DataTable dtIncidencia = TipoIncidencias.ObtenerIncidenciaxTipo(objIncidencia, Opcion);
                llenarCombo(cbTipo, dtIncidencia, "Tipo");
                //cbTipo.DataSource = dtIncidencia;
                //cbTipo.DisplayMember = "Tipo";
                //cbTipo.ValueMember = "cvtipo";
                //cbTipo.Text = "Seleccionar";
            }
        }

        private void pnlBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != 0)
            {
                SonaCompania objCia = new SonaCompania();
                cbArea.Enabled = true;
                DataTable dtArea = objCia.ObtenerPlantel(4, 0, comboBox4.SelectedItem.ToString(), "");
                llenarCombo(comboBox1, dtArea, "Descripción"); 



            }
          
        }

        private void cbCompañia2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SonaCompania objCia = new SonaCompania();
            cbPlanta2.Enabled = true;
            DataTable dtArea = objCia.ObtenerPlantel(4, 0, cbCompañia2.SelectedItem.ToString(), "");
            llenarCombo(cbPlanta2 , dtArea, "Descripción");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string sIdtrab = AsignarVariableCombo(cbNombre2);
            string sCia = AsignarVariableCombo(cbCompañia2);
            string sUbicacion = AsignarVariableCombo(cbUbicacion2);
            string sArea = AsignarVariableCombo(cbPlanta2);
            string sDepto = AsignarVariableCombo(cbDepartamento2);
            string sIncidencia = AsignarVariableCombo(cbConcepto2);
            DateTime dtFechaInicio = dtinicio2.Value;
            DateTime dtFechaFin = dtfin2.Value;
            Incidencia objInc = new Incidencia();


            string sIdtrab = "";
            if (cbEmpleados.Text == String.Empty)
                sIdtrab = "%";
            else
                sIdtrab = cbEmpleados.SelectedValue.ToString();
            if (sIdtrab == "0")
                sIdtrab = "%";

            DataTable dtRpt = objInc.ReporteGenerico(sIdtrab, sCia, sUbicacion, sArea, sDepto,dtFechaInicio, dtFechaFin,sIncidencia);
            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                    //DialogResult resulta = MessageBox.Show("Trae Datos", "SIPAA");
                    ViewerReporte form = new ViewerReporte();
                    Generico dtrpt = new Generico();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, this.CompanyName, dtrpt.ResourceName);

                   // ReportDoc.SetParameterValue("TotalRegistros", dtRpt.Rows.Count.ToString());
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaFin", dpFechaFin.Value);


                    form.RptDoc = ReportDoc;
                    form.Show();
                    DialogResult Resultado = MessageBox.Show("¿Desea crear el archivo en formato .csv para abrirlo con excel?", "SIPAA", MessageBoxButtons.YesNo);
                    if (Resultado == DialogResult.Yes)
                       // creacsv(dtRpt);
                        creacsv2(dtRpt);



                    break;

            }

        }


        private void creacsv2(DataTable dtRpt)
        {

            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                bool bandera = false;

                try
                {
              
                    StreamWriter Texto = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                 

                    string cadenaReg = "";
                    cadenaReg = "Reporte Genérico del Periodo " + dtinicio2.Value.ToString("dd/MM/yy") + " al " + dtfin2.Value.ToString("dd/MM/yy");
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    cadenaReg = "IdTrab, Nombre, Puesto, Compañia , Ubicación, Fecha de Registro, Incidencia , Representa, Tiempo Empleado, Tiempo Profesor," +
                        "Area, Departamento, Estatus, Autorizada, Descontada, Falta";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    foreach (DataRow row in dtRpt.Rows)
                    {
                        cadenaReg = row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," + row[3].ToString() + "," + row[4].ToString() +
                            "," + row[5].ToString() + "," + row[6].ToString() + "," + row[7].ToString() + "," + row[8].ToString() + "," + row[9].ToString() +
                            "," + row[10].ToString() + "," + row[11].ToString() + "," + row[12].ToString() + "," + row[13].ToString() + "," + row[14].ToString() +
                            "," + row[15].ToString() ;
                        Texto.WriteLine(cadenaReg);
                    }

                    Texto.Close();
                }
                catch
                {

                    bandera = true;
                }
                if (!bandera)
                    MessageBox.Show("El archivo " + saveFileDialog.FileName + "ha sido creado Correctamente, ahora puede abrirlo con excel");
                else
                    MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");


            }
           
        }

        private void creacsv3(DataTable dtRpt)
        {



            saveFileDialog.Filter = "csv files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                bool bandera = false;

                try
                {

                    StreamWriter Texto = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);

                    string cadenaReg = "";
                    cadenaReg = "Reporte por Conceptos";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);
                    cadenaReg = "Idtrab, Nombre, Incidencia, Tipo, Fecha_Inicio, Fecha_Fin, Dias Naturales, Dias Efectivos, Referencia, Compañia, Ubicación, Departamento, Tipo_Nomina, Plantel ";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    foreach (DataRow row in dtRpt.Rows)
                    {
                        cadenaReg = row[0].ToString() + "," + row[1].ToString() + "," + row[2].ToString() + "," + row[3].ToString() + ","+
                            "" + row[4].ToString() + "," + row[5].ToString() + "," + row[6].ToString() + "," + row[7].ToString() + ","+
                            "" + row[8].ToString() + "," + row[9].ToString() + "," + row[10].ToString() + "," + row[11].ToString() + "," + row[12].ToString() + "," + row[13].ToString();
                        Texto.WriteLine(cadenaReg);
                    }

                    Texto.Close();
                }
                catch
                {

                    bandera = true;
                }
                if (!bandera)
                    MessageBox.Show("El archivo " + saveFileDialog.FileName + "ha sido creado Correctamente, ahora puede abrirlo con excel");
                else
                    MessageBox.Show("No se pudo crear el archivo. Intente de Nuevo");


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------------------------------
        //                                      R E P O R T E
        //-----------------------------------------------------------------------------------------------






    }
}
