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

        #endregion

        SonaFormaPago oSonaFormaPago = new SonaFormaPago();
        ReasignaSupyDirector oPeriodoProcesoIncidencia = new ReasignaSupyDirector();
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
    }
}
