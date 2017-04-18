using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIPAA_CS.App_Code;
using SIPAA_CS.App_Code.RecursosHumanos.Catalogos;
using SIPAA_CS.App_Code.RecursosHumanos.Procesos;
using SIPAA_CS.Conexiones;
using SIPAA_CS.Properties;

//***********************************************************************************************
//Autor: Jaime Avendaño Vargas
//Fecha creación: 04-Abril-2017       Última Modificacion: dd-mm-aaaa
//Descripción: Reasignación de Supervisor y Director
//***********************************************************************************************

namespace SIPAA_CS.RecursosHumanos.Procesos
{
    public partial class ReasignaSupyDirectores : Form
    {
        #region Variables

        int iIdFormaPago;
        int iOpcion;
        bool bClickPrimeraVezFormaPago = true;
        string sFechaInicioPeriodoIncidencia;
        string sFechaFinPeriodoIncidencia;
        string sDescripcion;
        int iStPeriodoIncidencia;
        int iResp; // iResp -> iResp

        #endregion

        SonaFormaPago oSonaFormaPago = new SonaFormaPago();
        ReasignaSupyDirector oPeriodoProcesoIncidencia = new ReasignaSupyDirector();
        ReasignaSupyDirector oNombreEmpleado = new ReasignaSupyDirector();
        ReasignaSupyDirector oObtieneSupyDir = new ReasignaSupyDirector();
        Utilerias Util = new Utilerias();
                
        public ReasignaSupyDirectores()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------------------------
        //                                      C O M B O S
        //-----------------------------------------------------------------------------------------------

        //
        // Llena Combo Box de Forma Pago de la tabla de SONARH
        private void cbFormaPago_Click(object sender, EventArgs e)
        {
            if (bClickPrimeraVezFormaPago)
            {
                DataTable dtFormaPago = oSonaFormaPago.FormaPago_S(4, 0, "");
                cbFormaPago.DataSource = dtFormaPago;
                cbFormaPago.DisplayMember = "Descripción";
                cbFormaPago.ValueMember = "Clave";
                bClickPrimeraVezFormaPago = false;
            } // if (bClickPrimeraVezFormaPago)
        } // private void cbFormaPago_Click(object sender, EventArgs e)

        private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iIdFormaPagoBuscar = 0;

            // JAV MessageBox.Show(cbFormaPago.SelectedIndex.ToString(), "Mensaje X", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (!bClickPrimeraVezFormaPago)
            {
                // JAV pnlActPeriodoIncidencia.Visible = false;
                /*
                 * if (cbFormaPago.SelectedIndex == 0)
                                {
                                    iIdFormaPagoBuscar = 1;
                                }
                                else
                                {
                                    if (cbFormaPago.SelectedIndex == 1)
                                    {
                                        iIdFormaPagoBuscar = 2;
                                    }
                              }
                */

                iIdFormaPagoBuscar = Convert.ToInt32(cbFormaPago.SelectedValue.ToString());
                iIdFormaPago = iIdFormaPagoBuscar;

                string sUsuumod;
                string sPrgumod;

                sUsuumod = "";
                sPrgumod = "";
                
                // Obtiene Fecha de Inicio y Fin de Periodo Activo

                DataTable dtPeriodosProcesoIncidencias = oPeriodoProcesoIncidencia.obtPeriodosProcesoIncidencia(7, iIdFormaPago, "", "", "", 1, "", "");

                if (dtPeriodosProcesoIncidencias.Rows.Count > 0)
                {
                    TxtFeIni.Text = dtPeriodosProcesoIncidencias.Rows[0][1].ToString();
                    TxtFeFin.Text = dtPeriodosProcesoIncidencias.Rows[0][2].ToString();
                }
                else
                {
                    TxtFeIni.Text = "";
                    TxtFeFin.Text = "";
                }


                // JAV txtDescripcionPeriodoIncidencia.Text = cbFormaPago.SelectedItem.ToString();
                // JAV fgPeriodosProcesoIncidencia(6, iIdFormaPagoBuscar, "", "", "", iStPeriodoIncidencia, "bhb", "PeriodosProcesoIncidencia");
            } // if (!bClickPrimeraVezFormaPago)
        } // private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)




        //Boton minimizar
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //boton cerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro que desea salir?", "SIPAA", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                // No hace nada, se queda en la pantalla
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ObtieneEmpleado(object sender, EventArgs e)
        {
            // Obtiene Nombre del Empleado

            DataTable dtNombreEmpleado = oNombreEmpleado.obtNombreEmpleado(TxtIdEmp.Text, 14/*, 0, 0, ""*/);
            //TxtFeIni.Text = dtPeriodosProcesoIncidencias.Container.ToString();
            if (dtNombreEmpleado.Rows.Count > 0)
            {
                TxtNombreEmpleado.Text = dtNombreEmpleado.Rows[0][2].ToString();
            }
            else
            {
                TxtNombreEmpleado.Text = "Empleado No EXISTE";
            }
        }

        private void ObtieneSupyDir(object sender, EventArgs e)
        {
            // Obtiene Nombre del Empleado

            DataTable dtObtieneSupyDir = oObtieneSupyDir.obtObtieneSupyDir(4, Convert.ToInt16(TxtIdEmp.Text), TxtFeIni.Text.Substring(0,10), TxtFeFin.Text.Substring(0,10)/*, Convert.ToInt16(TxtIdSupOri.Text), Convert.ToInt16(TxtIdDirOri.Text), Convert.ToInt16(TxtIdSupFin.Text), Convert.ToInt16(TxtIdDirFin.Text)*/);

            //TxtFeIni.Text = dtPeriodosProcesoIncidencias.Container.ToString();
            if (dtObtieneSupyDir.Rows.Count > 0)
            {
                TxtIdSupOri.Text = dtObtieneSupyDir.Rows[0][0].ToString();
                TxtIdDirOri.Text = dtObtieneSupyDir.Rows[0][1].ToString();
            }
            else
            {
                    TxtIdSupOri.Text = "";
                    TxtIdDirOri.Text = "";
            }
        }



    }
}
