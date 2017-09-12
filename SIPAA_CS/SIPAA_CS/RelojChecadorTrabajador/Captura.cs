using SIPAA_CS.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using zkemkeeper;
using static SIPAA_CS.App_Code.Usuario;

namespace SIPAA_CS.RelojChecadorTrabajador
{
    public partial class CapturaTrabajador : Form
    {
        public CapturaTrabajador()
        {
            InitializeComponent();
        }

        public CZKEMClass objCZKEM = new CZKEMClass();
        private void Captura_Load(object sender, EventArgs e)
        {

        }


        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (txtPass.Text == txtConfPass.Text)
            {
                prgb1.Value = 20;
                if (txtPass.Text != String.Empty || txtConfPass.Text != String.Empty)
                {
                    
                    prgb1.Value = 60;
                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Conectando con el Dispositivo.");
                    panelMensaje.Enabled = false;
                    bool bConexion = Connect_Net(RelojxUsuario.IPReloj, 4370);
                  
                   

                    if (bConexion != false)
                    {
                        string idtrab = RelojxUsuario.idtrab;
                        string Nombre = RelojxUsuario.Nombre;
                        string Pass = txtPass.Text;
                        

                        panelMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Enviando Datos.");
                        prgb1.Value = 80;
                        panelMensaje.Enabled = false;
                       
                        if (objCZKEM.SSR_SetUserInfo(1, idtrab, Nombre, Pass, 0, true))
                        {
                            Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Contraseña Asignada Correctamente");
                            this.Enabled = true;
                            string Cifrado = Utilerias.cifrarPass(Pass, 1);
                            SonaTrabajador objTrab = new SonaTrabajador();
                            try
                            {
                                objTrab.GestionIdentidad(idtrab, Cifrado, "", "0", LoginInfo.IdTrab, this.Name, 6);
                            }
                            catch (Exception ex)
                            {

                            }


                            objCZKEM.Disconnect();
                            prgb1.Value = 80;
                            timer1.Start();
                            timer2.Start();
                        }
                    }
                    else {
                        panelMensaje.Enabled = true;
                        Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue Posible Conectar con el Dispositivo. Verificar la Conexión a la Red");
                        objCZKEM.Disconnect();
                       
                    }
                }
                else
                {

                    Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "La Contraseña asignada no puede ir vacía");
                   
                }
            }
            else {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 1, "Las Contraseñas no coinciden.");
              
                
            }
            prgb1.Value = 100;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelTag.Visible = false;
            prgb1.Visible = false;
            timer1.Stop();
        }

        public bool Connect_Net(string IPAdd, int Port)
        {
            try
            {
                if (objCZKEM.Connect_Net(IPAdd, Port))
                {
                    if (objCZKEM.RegEvent(1, 65535))
                    {
                        //objCZKEM.OnConnected += ObjCZKEM_OnConnected;
                        //objCZKEM.OnDisConnected += objCZKEM_OnDisConnected;
                        //objCZKEM.OnEnrollFinger += ObjCZKEM_OnEnrollFinger;
                        //objCZKEM.OnFinger += ObjCZKEM_OnFinger;
                        //objCZKEM.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkemClient_OnAttTransactionEx);
                        objCZKEM.RegEvent(1, 32767);
                    }
                    return true;
                }
                return false;
            }
            catch {

                Utilerias.ControlNotificaciones(panelTag, lbMensaje, 3, "No fue Posible conectar con el Dispositivo.");
                timer1.Start();
                return false;

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            this.Close();
        }
    }
}
