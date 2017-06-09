using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SIPAA_CS.App_Code.SonaCompania;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class AutorizarIncidencias : Form
    {
        public AutorizarIncidencias()
        {
            InitializeComponent();
        }

        CheckBox ckbheader = new CheckBox();
        public int iIdTrab = 0;
        public int iStdir = 0;
        public int ultimaseleccion = 0;
        public DateTime fFechareg;
        public int iCvincidencia;
        List<Trab_Fecha> ltUsuario = new List<Trab_Fecha>(); 

        private void AutorizarIncidencias_Load(object sender, EventArgs e)
        {

            DataTable dtPermisos = Modulo.ObtenerPermisosxUsuario(LoginInfo.IdTrab, this.Name);
            Permisos.dcPermisos = Utilerias.CrearListaPermisoxPantalla(dtPermisos);
            //////////////////////////////////////////////////////
            // resize 
            Utilerias.ResizeForm(this, Utilerias.PantallaSistema());
            //////////////////////////////////////////////////////////////////////////////////
            lblusuario.Text = LoginInfo.Nombre;

            lbFechaI.Text = String.Empty;
            lbFechaT.Text = String.Empty;
            PeriodoProcesoIncidencia objInc = new PeriodoProcesoIncidencia();

            DataTable dtInc = objInc.obtPeriodosProcesoIncidencia(10, 0, "", "", "%", 0, "", "");

            if (dtInc != null)
            {
                Utilerias.llenarComboxDataTable(cbTipoNomina, dtInc, "idformapago", "descripcion");
            }
            else {
                cbTipoNomina.Items.Insert(0, "Sin Periodos Abiertos");
                cbTipoNomina.Enabled = false;
            }

            if (Permisos.dcPermisos["Crear"] != 1 && Permisos.dcPermisos["Actualizar"] != 1) { label2.Text = "";  }
        }

        private void cbTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iOut = 0;
            string sFechai = "";
            string sFechat = "";
            if (Int32.TryParse(cbTipoNomina.SelectedValue.ToString(), out iOut))
            {
                
                PeriodoProcesoIncidencia objInc = new PeriodoProcesoIncidencia();
                DataTable dtInc = objInc.obtPeriodosProcesoIncidencia(9, Convert.ToInt32(cbTipoNomina.SelectedValue.ToString()),"", "", "", 0, "", "");

                foreach (DataRow row in dtInc.Rows) {
                     sFechai = row["FecIni"].ToString();
                     sFechat = row["FecFin"].ToString();
                }


             lbFechaI.Text = sFechai; 
             lbFechaT.Text = sFechat;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ckbheader.Checked = false;
           
            if (cbTipoNomina.SelectedIndex > 0)
            {

                IncCalificacion objInc = new IncCalificacion();
                objInc.fFechaInicio = DateTime.Parse(lbFechaI.Text);
                objInc.fFechaTermino = DateTime.Parse(lbFechaT.Text);
                if (txtIdTrab.Text != String.Empty)
                {
                    objInc.sIdtrab = txtIdTrab.Text;
                }
                else
                {
                    objInc.sIdtrab = "%";
                }

                  objInc.iStinc = Convert.ToInt32(cbTipoNomina.SelectedValue);
                

                LlenarGrid(objInc);
            }
            else if(cbTipoNomina.SelectedText.Contains("Sin Periodos Abiertos")){

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No Existen Periodo Abiertos");
                timer1.Start();
            }
            else {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "No se ha Seleccionado Algún Periodo Abierto");
                timer1.Start();

            }
        }


        private void LlenarGrid(IncCalificacion objInc)
        {

            if (dgvInc.Columns.Count > 1)
            {
                dgvInc.Controls.Remove(ckbheader);
                dgvInc.Columns.RemoveAt(0);
            }


            DataTable dtInc = objInc.ObtenerCalificacionIncidenciaDetalle(objInc, 12);

            dgvInc.DataSource = dtInc;

            Utilerias.AgregarCheck(dgvInc, 0);
            ckbheader = Utilerias.AgregarCheckboxHeader(dgvInc, 0);
            ckbheader.CheckedChanged += Ckbheader_CheckedChanged;

            dgvInc.Columns["stdir"].Visible = false;
            dgvInc.Columns["cvincidencia"].Visible = false;
            dgvInc.Columns["Nombre Trabajador"].Width = 300;
            dgvInc.Columns["Fecha Registro"].Width = 150;
            dgvInc.Columns["Incidencia"].Width = 150;

            if (dgvInc.Rows.Count <= 0) {

                dgvInc.Controls.Remove(ckbheader);
                //Utilerias.ControlNotificaciones(panelTag,lbMensaje,2,"Consulta sin Resultados");
                //timer1.Start();
            }

            foreach (DataGridViewRow row in dgvInc.Rows) {
                row.Cells[0].Tag = "uncheck";
            }

            if (Permisos.dcPermisos["Crear"] != 1 && Permisos.dcPermisos["Actualizar"] != 1) { dgvInc.Columns.RemoveAt(0); }

        }

        private void Ckbheader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {

                for (int iContador = 0; iContador < dgvInc.Rows.Count; iContador++)
                {
                    dgvInc.Rows[iContador].Cells[0].Value = Resources.ic_check_circle_green_400_18dp;

                    iIdTrab = Convert.ToInt32(dgvInc.Rows[iContador].Cells["IdTrab"].Value.ToString());
                    iStdir = Convert.ToInt32(dgvInc.Rows[iContador].Cells["stdir"].Value.ToString());
                    fFechareg = DateTime.Parse(dgvInc.Rows[iContador].Cells["Fecha Registro"].Value.ToString());
                    iCvincidencia = Convert.ToInt32(dgvInc.Rows[iContador].Cells["cvincidencia"].Value.ToString());

                    Trab_Fecha objTrab = new Trab_Fecha();
                    objTrab.IdTrab = iIdTrab.ToString();
                    objTrab.fFechaRegistro = fFechareg;
                    objTrab.cvincidencia = iCvincidencia;

                    ltUsuario.Add(objTrab);

                }

                pnlAsig.Visible = true;
                cbJusficacion.SelectedIndex = 0;

            }
            else {

                for (int iContador = 0; iContador < dgvInc.Rows.Count; iContador++)
                {
                    dgvInc.Rows[iContador].Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                }
                ltUsuario.Clear();
                pnlAsig.Visible = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbJusficacion.SelectedIndex != 0)
            {

                int iStDir = 0;
                DataTable dt= new DataTable();
               
                if (cbJusficacion.SelectedIndex == 1) { iStDir = 0; } else { iStDir = 1; }

                try
                {

                    for (int iCont = 0; iCont < ltUsuario.Count; iCont++)
                    {

                        Trab_Fecha obj = ltUsuario[iCont];
                        IncCalificacion objCalif = new IncCalificacion();

                        objCalif.fFechaRegistro = obj.fFechaRegistro;
                        objCalif.sIdtrab = obj.IdTrab.ToString();
                        objCalif.iCvincidencia = obj.cvincidencia;
                        objCalif.iStDirector = iStDir;

                        dt = objCalif.ObtenerCalificacionIncidenciaDetalle(objCalif, 13);
                        
                        
                    }
                    ltUsuario.Clear();
                    
                    if (dt.Columns.Contains("UPDATE"))
                    {

                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Asignaciones Correctas.");
                        timer1.Start();
                        btnBuscar_Click(sender, e);
                    }
                }
                catch {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "Error de Comunicación. Favor de Intentarlo más tarde.");
                    timer1.Start();
                }

            }
            else {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No se ha Seleccinado un Tipo de Asignación");
                timer1.Start();
            }

            pnlAsig.Visible = false;
            ckbheader.Checked = false;
        }


        private void dgvInc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Permisos.dcPermisos["Crear"] != 0 && Permisos.dcPermisos["Actualizar"] != 0)
            {

                if (dgvInc.SelectedRows.Count != 0)
                {

                    DataGridViewRow row = this.dgvInc.SelectedRows[0];

                    iIdTrab = Convert.ToInt32(row.Cells["IdTrab"].Value.ToString());
                    iStdir = Convert.ToInt32(row.Cells["stdir"].Value.ToString());
                    fFechareg = DateTime.Parse(row.Cells["Fecha Registro"].Value.ToString());
                    iCvincidencia = Convert.ToInt32(row.Cells["cvincidencia"].Value.ToString());

                    Trab_Fecha objTrab = new Trab_Fecha();
                    objTrab.IdTrab = iIdTrab.ToString();
                    objTrab.fFechaRegistro = fFechareg;
                    objTrab.cvincidencia = iCvincidencia;

                    ValidarExistencia(dgvInc, ltUsuario, objTrab);


                    if (row.Cells[0].Tag.ToString() == "check")
                    {

                        row.Cells[0].Value = Resources.ic_lens_blue_grey_600_18dp;
                        row.Cells[0].Tag = "uncheck";
                    }
                    else
                    {
                        row.Cells[0].Value = Resources.ic_check_circle_green_400_18dp;
                        row.Cells[0].Tag = "check";
                    }

                    if (ltUsuario.Count() != 0)
                    {
                        pnlAsig.Visible = true;
                    }
                    else
                    {

                        pnlAsig.Visible = false;
                    }




                }

            }

        }

        public void ValidarExistencia(DataGridView dgv, List<Trab_Fecha> ltTrab, Trab_Fecha objtrab)
        {
            bool bBandera = false;
            int iCont = 0;
            DataGridViewRow row = dgv.SelectedRows[0];
            if (ltTrab.Count != 0)
            {
                
                        while(iCont <= (ltTrab.Count - 1)) {

                         Trab_Fecha objComp = ltTrab[iCont];

                    if (CompararObj(objComp, objtrab))
                    {

                        bBandera = true;
                        break;
                    }
                    else {
                        iCont += 1;

                    }

                }

                if (bBandera == true)
                {

                    ltTrab.Remove(ltTrab[iCont]);
                }
                else {

                    ltTrab.Add(objtrab);
                }
            }
            else {
                ltTrab.Add(objtrab);

            }

        }


        public bool CompararObj(Trab_Fecha obj1, Trab_Fecha obj2) {
            bool bBandera = false;

            if (obj1.fFechaRegistro == obj2.fFechaRegistro)
            {
                if (obj1.IdTrab == obj2.IdTrab)
                { bBandera = true; }
                else {
                    bBandera = false;
                }
            }
            else {

                bBandera = false;
            }

            return bBandera;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
        }
    }

 
    public class Trab_Fecha {

        public string IdTrab;
        public DateTime fFechaRegistro;
        public int cvincidencia;


    }
}
