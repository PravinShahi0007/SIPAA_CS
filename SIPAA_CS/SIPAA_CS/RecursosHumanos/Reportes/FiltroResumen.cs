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
            if (sIdtrab == "0")
                sIdtrab = "%";
            

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
           

            DataTable dtUbicaciones = objCia.ObtenerUbicacionPlantel(5, "%");
            llenarCombo(cbUbicacion, dtUbicaciones, "Descripción");
          

            SonaDepartamento objDepto = new SonaDepartamento();
            DataTable dtDepto = objDepto.obtdepto(5, "%");
            llenarCombo(cbDepartamento, dtDepto, "Descripción");
          

            cbTipoNomina.Enabled = false;
         
            DataTable dtempleados = contenedorempleados.obtenerempleados(7, "");
            Utilerias.llenarComboxDataTable(cbEmpleados, dtempleados, "NoEmpleado", "Nombre");
           
          
       
            cbEmpleados.Focus();
      
            CbConceptoIncidencia(8, 0, "", 0, 0, 0, 0, "", "");

            List<string> ltRepresenta = new List<string>();
            ltRepresenta.Insert(0, "Seleccionar");
            ltRepresenta.Insert(1,"Falta");
            ltRepresenta.Insert(2,"Retardo");
            cbRepresenta.DataSource = ltRepresenta;

            List<string> ltActivo = new List<string>();
            ltActivo.Insert(0, "Todos");
            ltActivo.Insert(1, "Activos");
            ltActivo.Insert(2, "Inactivos");
            cbActivo.DataSource = ltActivo;

            List<string> ltEstatus = new List<string>();
            ltEstatus.Insert(0, "Todas");
            ltEstatus.Insert(1, "Descontadas");
            ltEstatus.Insert(2, "Justificadas");
            cbEstatus.DataSource = ltEstatus;





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
            DateTime dtFechaInicio = dpFechaInicio.Value;
            DateTime dtFechaFin = dpFechaFin.Value;
            string idCompañia, idUbicacion, idPlanta, idDepto, Incidencia, Tipo, Nomina;

            idCompañia = AsignarVariableCombo(cbCia);
            idUbicacion = AsignarVariableCombo(cbUbicacion);
            idPlanta = AsignarVariableCombo(cbArea);
            idDepto = AsignarVariableCombo(cbDepartamento);
            Incidencia = AsignarVariableCombo(cbConcepto);
            Tipo = AsignarVariableCombo(cbTipo);
            Nomina = AsignarVariableCombo(cbTipoNomina);
            //string idTrab = AsignarVariableCombo(cbEmpleados);


            string sIdtrab = "";
            if (cbEmpleados.Text == String.Empty)
                sIdtrab = "%";
            else
                sIdtrab = cbEmpleados.SelectedValue.ToString();

           

            if (sIdtrab == "0")
                sIdtrab = "%";





            DataTable dtRpt = objInc.ReporteConceptos (sIdtrab, idCompañia, idUbicacion, idPlanta, idDepto, dtFechaInicio, dtFechaFin, Incidencia, Tipo, Nomina);
            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:

                    ViewerReporte form = new ViewerReporte();
                    Conceptos dtrpt = new Conceptos();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, this.CompanyName, dtrpt.ResourceName);
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaFin", dpFechaFin.Value);
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
             
            }
        }

        private void pnlBusqueda_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
          /*  if (comboBox4.SelectedIndex != 0)
            {
                SonaCompania objCia = new SonaCompania();
                cbArea.Enabled = true;
                DataTable dtArea = objCia.ObtenerPlantel(4, 0, comboBox4.SelectedItem.ToString(), "");
                llenarCombo(comboBox1, dtArea, "Descripción"); 



            }*/
          
        }

        private void cbCompañia2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*SonaCompania objCia = new SonaCompania();
            cbPlanta2.Enabled = true;
            DataTable dtArea = objCia.ObtenerPlantel(4, 0, cbCompañia2.SelectedItem.ToString(), "");
            llenarCombo(cbPlanta2 , dtArea, "Descripción");*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string sCia = AsignarVariableCombo(cbCia);
            string sUbicacion = AsignarVariableCombo(cbUbicacion);
            string sArea = AsignarVariableCombo(cbArea);
            string sDepto = AsignarVariableCombo(cbDepartamento);
            string sIncidencia = AsignarVariableCombo(cbIncidencia);
            DateTime dtFechaInicio = dpFechaInicio.Value;
            DateTime dtFechaFin = dpFechaFin.Value;
            Incidencia objInc = new Incidencia();
            string Nomina = AsignarVariableCombo(cbTipoNomina);

          

            string activo;
            if (cbActivo.SelectedIndex == 0)
                activo = "%";
            else if (cbActivo.SelectedIndex == 1)
                activo = "1";
            else
                activo = "0"; 




            string status_dir;
            if (cbEstatus.SelectedIndex == 0)
                status_dir = "%";
            else
                status_dir = cbEstatus.SelectedIndex.ToString();






            string sIdtrab = "";
            if (cbEmpleados.Text == String.Empty)
                sIdtrab = "%";
            else
                sIdtrab = cbEmpleados.SelectedValue.ToString();
            if (sIdtrab == "0")
                sIdtrab = "%";

            DataTable dtRpt = objInc.ReporteGenerico(sIdtrab, sCia, sUbicacion, sArea, sDepto,dtFechaInicio, dtFechaFin,sIncidencia,activo, status_dir, Nomina);
            switch (dtRpt.Rows.Count)
            {

                case 0:
                    DialogResult result = MessageBox.Show("Consulta Sin Resultados", "SIPAA");
                    break;

                default:
                   
                    ViewerReporte form = new ViewerReporte();
                    Generico dtrpt = new Generico();
                    ReportDocument ReportDoc = Utilerias.ObtenerObjetoReporte(dtRpt, this.CompanyName, dtrpt.ResourceName);

                  
                    ReportDoc.SetParameterValue("FechaInicio", dpFechaInicio.Value);
                    ReportDoc.SetParameterValue("FechaFin", dpFechaFin.Value);


                    form.RptDoc = ReportDoc;
                    form.Show();
                    DialogResult Resultado = MessageBox.Show("¿Desea crear el archivo en formato .csv para abrirlo con excel?", "SIPAA", MessageBoxButtons.YesNo);
                    if (Resultado == DialogResult.Yes)
                      
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
                    int TiempoProfesor, TiempoEmpleado, Justificada, Descontada, Falta;
                    TiempoProfesor = TiempoEmpleado = Justificada = Descontada = Falta = 0;


                    string cadenaReg = "";
                    cadenaReg = "Reporte Genérico del Periodo " + dpFechaInicio.Value.ToString("dd/MM/yy") + " al " + dpFechaFin.Value.ToString("dd/MM/yy");
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);

                    cadenaReg = "IdTrab, Estado del Empleado, Nombre, Puesto, Fecha de Registro, Representa, Incidencia, Tiempo_Empleado, Tiempo_Profesor,"+
                        " Estatus, Justificada, Descontada, Falta, Retroactivo ,Tipo de Nómina, Departamento, Plantel, Ubicación, Compañia";
                    Texto.WriteLine(cadenaReg);
                    Texto.Write(Texto.NewLine);


                    for (int i = 0; i < dtRpt.Rows.Count - 1; ++i)
                    {
                        DataRow Fila = dtRpt.Rows[i];
                        DataRow SiguienteFila = dtRpt.Rows[i + 1];                      
                        if (!SiguienteFila.IsNull(2))
                        {
                            
                            cadenaReg = Fila[0].ToString() + "," + Fila[1].ToString() + "," + Fila[2].ToString() + "," + Fila[3].ToString() + "," + Fila[4].ToString() +
                            "," + Fila[5].ToString() + "," + Fila[6].ToString() + "," + Fila[7].ToString() + "," + Fila[8].ToString() + "," + Fila[9].ToString() + "," + Fila[10].ToString() +
                            "," + Fila[11].ToString() + "," + Fila[12].ToString() + "," + Fila[13].ToString() + "," + Fila[14].ToString() + "," + Fila[15].ToString() + "," + Fila[16].ToString() + "," + Fila[17].ToString()+","+Fila[18].ToString();
                            Texto.WriteLine(cadenaReg);

                            TiempoEmpleado = TiempoEmpleado + Convert.ToInt32(Fila[7].ToString());
                            TiempoProfesor = TiempoProfesor + Convert.ToInt32(Fila[8].ToString());
                            Justificada = Justificada + Convert.ToInt32(Fila[10].ToString());
                            Descontada = Descontada + Convert.ToInt32(Fila[11].ToString());
                            Falta = Falta + Convert.ToInt32(Fila[12].ToString());


                            if (Fila[2].ToString() != SiguienteFila[2].ToString())
                            {
                                cadenaReg = " , , , , , , ," + TiempoEmpleado + "," + TiempoProfesor + ", ," + Justificada + "," + Descontada + "," + Falta + ", , , , ,  ";
                                Texto.WriteLine(cadenaReg);
                                Texto.Write(Texto.NewLine);
                                TiempoEmpleado = TiempoProfesor = Justificada = Descontada = Falta = 0; 

                            }
                          
                        }

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
                    cadenaReg = "Reporte por Conceptos del periodo " + dpFechaInicio.Value.ToString("dd/MM/yy") + " al " + dpFechaFin.Value.ToString("dd/MM/yy");
                    Texto.WriteLine(cadenaReg);
                  
                    Texto.Write(Texto.NewLine);
                    cadenaReg = "Idtrab, Nombre, Incidencia, Tipo, Fecha_Inicio, Fecha_Fin, Dias Naturales, Dias Efectivos, Referencia, Tipo_Nómina, Departamento, Ubicacion, Plantel, Compañia ";
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

        private void cbRepresenta_DropDown(object sender, EventArgs e)
        {
            cbIncidencia.Text = string.Empty;
        }

        private void cbRepresenta_DropDownClosed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbRepresenta.Text))
            {

                Incidencia objIncidencia = new Incidencia();
                objIncidencia.CVIncidencia = 0;

                string sAsignacion;
                if (cbRepresenta.SelectedIndex == 0)
                    sAsignacion = "%";
                else
                    sAsignacion = cbRepresenta.SelectedIndex.ToString();
                objIncidencia.Descripcion = sAsignacion;
                objIncidencia.CVTipo = 1;
                objIncidencia.TipoIncidencia = "";
                objIncidencia.Estatus = "";
                objIncidencia.UsuuMod = "";
                objIncidencia.PrguMod = "";
                objIncidencia.Estatus = "";
                int Opcion = 10;

                DataTable dtIncidencia = TipoIncidencias.ObtenerIncidenciaxTipo(objIncidencia, Opcion);
                llenarCombo(cbIncidencia, dtIncidencia, "descripcion");

            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            //cbEmpleados.Text = cbCia.Text = cbUbicacion.Text = cbArea.Text = cbDepartamento.Text = cbTipoNomina.Text = cbConcepto.Text = cbTipo.Text = cbEstatus.Text = cbRepresenta.Text = cbIncidencia.Text = cbActivo.Text = string.Empty;
            /*
            if (comboBox1.Text== "Reporte Anual")
            {
                label12.Visible = label17.Visible = false;
                button5.Visible = button4.Visible = false;
                cbEstatus.Visible = cbRepresenta.Visible = cbIncidencia.Visible = cbActivo.Visible = lbConcepto.Visible = lbTipo.Visible =  false;
                lbEstatus.Visible = lbRepresenta.Visible = lbIncidencia.Visible = lbActivo.Visible = cbConcepto.Visible = cbTipo.Visible = false;
             
                btnImprimirResumen.Visible = label10.Visible = true;
            }
            else if (comboBox1.Text== "Reporte General")
            {
                button5.Visible = true;
                button4.Visible = btnImprimirResumen.Visible = false;
                //cbConcepto.Visible = cbTipo.Visible = false;
                cbEstatus.Visible = cbRepresenta.Visible = cbIncidencia.Visible = cbActivo.Visible = true;
                lbEstatus.Visible = lbRepresenta.Visible = lbIncidencia.Visible = lbActivo.Visible = true;

                lbConcepto.Visible = lbTipo.Visible = false;
                cbConcepto.Visible = cbTipo.Visible = false; 
               
            }
            else if (comboBox1.Text== "Reporte por Conceptos")
            {
                button4.Visible = true;
                button5.Visible = btnImprimirResumen.Visible = false;
                lbConcepto.Visible = lbTipo.Visible = true;
                cbConcepto.Visible = cbTipo.Visible = true;
                cbEstatus.Visible = cbRepresenta.Visible = cbIncidencia.Visible = cbActivo.Visible = false;
                lbEstatus.Visible = lbRepresenta.Visible = lbIncidencia.Visible = lbActivo.Visible = false;
            }*/
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
      
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEmpleados.SelectedIndex = cbCia.SelectedIndex = cbUbicacion.SelectedIndex = cbArea.SelectedIndex = cbDepartamento.SelectedIndex = cbTipoNomina.SelectedIndex = 0;
            cbEmpleados.Visible = lbEmpleado.Visible = cbCia.Visible = lbCia.Visible = cbUbicacion.Visible = lbUbicacion.Visible = cbArea.Visible = lbArea.Visible = cbDepartamento.Visible = lbDepto.Visible = cbTipoNomina.Visible = lbTipoNomina.Visible = lbFechaInicio.Visible= lbFechaFin.Visible= dpFechaFin.Visible= dpFechaInicio.Visible= true ;

            if (comboBox1.SelectedIndex==0)
            {
               
                btnImprimirResumen.Visible = label10.Visible = true;
                button5.Visible = label12.Visible = false;
                label17.Visible = button4.Visible = false;

                cbConcepto.Visible = cbTipo.Visible = lbConcepto.Visible = lbTipo.Visible = false;
                cbEstatus.Visible = lbEstatus.Visible= cbIncidencia.Visible = lbIncidencia.Visible= cbRepresenta.Visible = lbRepresenta.Visible= cbActivo.Visible = lbActivo.Visible= false; 
                
            }
            else if (comboBox1.SelectedIndex==1)
            {
                button5.Visible = label12.Visible = true;
                btnImprimirResumen.Visible = label10.Visible =  false;
                label17.Visible = button4.Visible = false;
                cbConcepto.Visible = cbTipo.Visible = lbConcepto.Visible = lbTipo.Visible = false;
                cbEstatus.Visible = lbEstatus.Visible = cbIncidencia.Visible = lbIncidencia.Visible = cbRepresenta.Visible = lbRepresenta.Visible = cbActivo.Visible = lbActivo.Visible = true;

            }
            else if(comboBox1.SelectedIndex==2)
            {
                label17.Visible = button4.Visible = true;
                button5.Visible = label12.Visible = false;
                btnImprimirResumen.Visible = label10.Visible = false;
                cbConcepto.Visible = cbTipo.Visible = lbConcepto.Visible = lbTipo.Visible = true;
                cbEstatus.Visible = lbEstatus.Visible = cbIncidencia.Visible = lbIncidencia.Visible = cbRepresenta.Visible = lbRepresenta.Visible = cbActivo.Visible = lbActivo.Visible = false;
             
            }
        }
    }
}
