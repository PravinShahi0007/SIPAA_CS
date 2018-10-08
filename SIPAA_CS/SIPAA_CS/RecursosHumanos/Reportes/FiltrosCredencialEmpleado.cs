using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPAA_CS.RecursosHumanos.Reportes
{
    public partial class FiltrosCredencialEmpleado : Form
    {
        ComboBox cb;
        public DateTime date;
        public FiltrosCredencialEmpleado(ref ComboBox cb)
        {
            InitializeComponent();

            this.cb = cb;
            date = desSerializeClass();
            dpFechaInicio.Value = date;
        }

        private void FiltrosCredencialEmpleado_Load(object sender, EventArgs e)
        {
            SonaCompania objCia = new SonaCompania();
            DataTable dtCia = objCia.obtcomp(5, "%");
            Utilerias.llenarComboxDataTable(comboBox4, dtCia, "Clave", "Descripción");

            DataTable dtUbicaciones = objCia.ObtenerUbicacionPlantel(5, "%");
            Utilerias.llenarComboxDataTable(comboBox3, dtUbicaciones, "IdUbicacion", "Descripción");

            SonaDepartamento objDepto = new SonaDepartamento();
            DataTable dtDepto = objDepto.obtdepto(5, "%");
            Utilerias.llenarComboxDataTable(comboBox2, dtDepto, "Clave", "Descripción");

            comboBox5.SelectedIndex = 0;
        }

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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex > 0)
            {
                SonaTipoNomina objTnom = new SonaTipoNomina();

                DataTable dtTnom = objTnom.obtTipoNomina(5, Convert.ToInt32(comboBox4.SelectedValue), 0, "");
                Utilerias.llenarComboxDataTable(cbTipoNomina, dtTnom, "Clave", "Descripción");
                
                SonaCompania objCia = new SonaCompania();
                DataTable dtArea = objCia.ObtenerPlantel(9, Convert.ToInt32(comboBox4.SelectedValue), "", "");
                Utilerias.llenarComboxDataTable(comboBox1, dtArea, "Clave", "Descripción");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string compania = comboBox4.SelectedIndex > 0 ? comboBox4.SelectedValue.ToString() : "%";
            string tnomina = cbTipoNomina.SelectedIndex > 0 ? cbTipoNomina.SelectedValue.ToString() : "%";
            string ubicacion = comboBox3.SelectedIndex > 0 ? comboBox3.SelectedValue.ToString() : "%";
            string area = comboBox1.SelectedIndex > 0 ? comboBox1.SelectedValue.ToString() : "%";
            string depto = comboBox2.SelectedIndex > 0 ? comboBox2.SelectedValue.ToString() : "%";
            int status = comboBox5.SelectedIndex == 0 ? 1 : 0;
            date = dpFechaInicio.Value;
            serializeClass(date);

            //llena combo de empleados
            SonaTrabajador CSonaTrab = new SonaTrabajador();
            DataTable dtEmpleado = CSonaTrab.obtenerempleadosxfiltros(9, "%", compania, area, "%", "%", ubicacion, tnomina, status, 1);
            Utilerias.llenarComboxDataTable(cb, dtEmpleado, "NoEmpleado", "Nombre");
            
            //***
            pnlMensaje.Enabled = true;
            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 2, "Se encontraron " + (cb.Items.Count - 1) + " registros");
            pnlMensaje.Enabled = false;
            timer1.Start();
            //***
        }

        private void serializeClass(DateTime data)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Path.GetTempPath() + this.CompanyName,
                                         FileMode.Create,
                                         FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, new Vigencia(data));
                stream.Close();
            } catch (Exception ex) { }
        }

        private DateTime desSerializeClass()
        {
            date = DateTime.Now;

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Path.GetTempPath() + this.CompanyName,
                                          FileMode.Open,
                                          FileAccess.Read,
                                          FileShare.Read);
                Vigencia obj = (Vigencia)formatter.Deserialize(stream);
                stream.Close();
                date = obj.date;
            } catch (Exception ex) { }

            return date;
        }

        [Serializable]
        private class Vigencia
        {
            public DateTime date;

            public Vigencia(DateTime date)
            {
                this.date = date;
            }
        }

        private void FiltrosCredencialEmpleado_Shown(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = comboBox4.Items.Count > 0 ? 0 : -1;
            cbTipoNomina.SelectedIndex = cbTipoNomina.Items.Count > 0 ? 0 : -1;
            comboBox3.SelectedIndex = comboBox3.Items.Count > 0 ? 0 : -1;
            comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;
            comboBox2.SelectedIndex = comboBox2.Items.Count > 0 ? 0 : -1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            pnlMensaje.Enabled = panelTag.Enabled = false;
            timer1.Stop();

            if ((cb.Items.Count - 1) > 0)
            {
                this.Close();
            }
        }
    }
}
